using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Terrasoft.Common;
using Terrasoft.Configuration;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Entities.Events;

namespace ZoomIntegration
{
	[EntityEventListener(SchemaName = "Activity")]
	public class ActivityEventListener : BaseEntityEventListener
	{
		private UserConnection UserConnection;
		public override void OnSaving(object sender, EntityBeforeEventArgs e)
		{
			base.OnSaving(sender, e);
			Entity activity = (Entity)sender;
			UserConnection = activity.UserConnection;
			
			
			bool IsZoom = activity.GetTypedColumnValue<bool>("CreateZoomMeeting");
			string M = activity.GetTypedColumnValue<String>("MeetingId");
			
			if (IsZoom && String.IsNullOrEmpty(M)){
				ZoomMeetingApi zm = new ZoomMeetingApi(UserConnection);

				DateTime startDate = activity.GetTypedColumnValue<DateTime>("StartDate");
				TimeSpan offset = UserConnection.CurrentUser.TimeZone.GetUtcOffset(startDate);

				/*
				 * DateTime Return in Users LocalTime this need to convert to UTC
				 */
				string zoomStart = startDate.Add(-offset).ToString("yyyy-MM-ddTHH:mm:ssZ");
				DateTime Due = activity.GetTypedColumnValue<DateTime>("DueDate");
				int Duration = (int)Due.Subtract(startDate).TotalMinutes;

				Guid TimeZoneId = activity.GetTypedColumnValue<Guid>("TimeZoneId");

				if (TimeZoneId == Guid.Empty) {
					// _userConnection.CurrentUser.TimeZoneId = "Eastern"
					Guid g = IdValue("TimeZone", "Code", UserConnection.CurrentUser.TimeZoneId);
					TimeZoneId = g;
				}

				string ZoomTimeZone = FindZoomTimeZone(TimeZoneId);
				string topic = activity.GetTypedColumnValue<String>("Title");
				string agenda = activity.GetTypedColumnValue<String>("Notes");
				
				MeetingRequest mr = new MeetingRequest(PredifinedMeetings.Certification, topic, agenda, zoomStart, Duration, ZoomTimeZone);
				zm.CreateZoomMeeting(mr);
				
				/*
				 * Update activity after creating a meeting
				 */
				activity.SetColumnValue("MeetingId", zm.mResponse.Id);
				activity.SetColumnValue("MeetingUUID", zm.mResponse.Uuid);
				activity.SetColumnValue("StartUrl", zm.mResponse.StartUrl);
				activity.SetColumnValue("JoinUrl", zm.mResponse.JoinUrl);
				activity.SetColumnValue("RegistrationUrl", zm.mResponse.RegistrationUrl);
				activity.SetColumnValue("HostId", zm.mResponse.HostId);
				activity.SetColumnValue("AlternativeHosts", zm.mResponse.Settings.AlternativeHosts);
				SendMessageToUi(activity.GetTypedColumnValue<Guid>("Id"),"Meeting Created");
			}
		}

		public override void OnUpdated(object sender, EntityAfterEventArgs e)
		{
			base.OnUpdated(sender, e);
			Entity activity = (Entity)sender;
			UserConnection = activity.UserConnection;

			bool NeedUpdate = false;

			bool IsZoom = activity.GetTypedColumnValue<bool>("CreateZoomMeeting");
			if (IsZoom){
				
				//Columns that triger Meeting Update
				string[] ZoomUpdateColums = { "Title", "Notes", "AlternativeHosts", "StartDate", "DueDate", "TimeZoneId" };
				foreach (EntityColumnValue c in e.ModifiedColumnValues)
				{
					if (ZoomUpdateColums.Contains(c.Name))
					{
						NeedUpdate = true;
						break;
					}
				}
				if (NeedUpdate)
				{
					UpdateZoomMeeting(activity);
				}
			}
		}

		public override void OnDeleting(object sender, EntityBeforeEventArgs e)
		{
			base.OnDeleting(sender, e);
			Entity activity = (Entity)sender;
			UserConnection = activity.UserConnection;

			bool IsZoom = activity.GetTypedColumnValue<bool>("CreateZoomMeeting");
			if (IsZoom)
			{
				ZoomMeetingApi zm = new ZoomMeetingApi(UserConnection);
				string MeetingId = activity.GetTypedColumnValue<String>("MeetingId");
				zm.DeleteZoomMeeting(MeetingId);
			}
		}

		private void UpdateZoomMeeting(Entity activity)
		{
			ZoomMeetingApi zm = new ZoomMeetingApi(UserConnection);
			string MeetingId = activity.GetTypedColumnValue<string>("MeetingId");

			zm.RetrieveMeeting(MeetingId);
			MeetingResponse Original = zm.retrieveMeetingResponse;
			

			Original.Topic = (activity.GetTypedColumnValue<String>("Title") == Original.Topic) ? Original.Topic : activity.GetTypedColumnValue<String>("Title");
			Original.Agenda = (activity.GetTypedColumnValue<String>("Notes") == Original.Topic) ? Original.Agenda : activity.GetTypedColumnValue<String>("Notes");
			Original.Settings.AlternativeHosts = activity.GetTypedColumnValue<String>("AlternativeHosts");

			DateTime startDate = activity.GetTypedColumnValue<DateTime>("StartDate");
			TimeSpan offset = UserConnection.CurrentUser.TimeZone.GetUtcOffset(startDate);
			
			//DateTime Return in Users LocalTime this need to convert to UTC
			string zoomStart = startDate.Add(-offset).ToString("yyyy-MM-ddTHH:mm:ssZ");

			DateTime Due = activity.GetTypedColumnValue<DateTime>("DueDate");

			Guid TimeZoneId = activity.GetTypedColumnValue<Guid>("TimeZoneId");
			string ZoomTimeZone = FindZoomTimeZone(TimeZoneId);
			Original.TimeZone = ZoomTimeZone;

			Original.StartTime = zoomStart;
			Int32 Duration = (Int32)Due.Subtract(startDate).TotalMinutes;
			Original.Duration = Duration;

			zm.UpdateZoomMeeting(Original, MeetingId);

		}

#region HelperMethods
		static void SendMessageToUi(Guid recordId, string MyEvent){
			string senderName = "ActivityEventListener";
			// Example for message
			string message = JsonConvert.SerializeObject(new {
				RecordID = recordId,
				Event = MyEvent
			});
			// For all users
			MsgChannelUtilities.PostMessageToAll(senderName, message);
		}

		public string FindZoomTimeZone(Guid TimeZoneId) {

			Select select = new Select(UserConnection)
				.Column("Id")
				.Column("Name")
				.From("ZoomTimeZone")
				.Where("TimeZoneId").IsEqual(Column.Parameter(TimeZoneId)) as Select;

			var result = new Dictionary<Guid, string>();
			String res = string.Empty;

			using (DBExecutor dbExecutor = UserConnection.EnsureDBConnection())
			{
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
				{
					while (dataReader.Read())
					{
						Guid key = dataReader.GetColumnValue<Guid>("Id");
						string value = dataReader.GetColumnValue<string>("Name");
						result.Add(key, value);
						res = value;
						break;
					}
				}
			}

			return res;
		}

		public Guid IdValue(string Table, string SearchColumn, string SearchValue)
		{
			Select select = new Select(UserConnection)
				.Column("Id")
				.Column(SearchColumn)
				.From(Table)
				.Where(SearchColumn).IsEqual(Column.Parameter(SearchValue)) as Select;

			var result = new Dictionary<Guid, string>();
			using (DBExecutor dbExecutor = UserConnection.EnsureDBConnection())
			{
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
				{
					while (dataReader.Read())
					{
						Guid key = dataReader.GetColumnValue<Guid>("Id");
						string value = dataReader.GetColumnValue<string>(SearchColumn);
						result.Add(key, value);
					}
				}
			}
			Guid MyKey = Guid.Empty;
			foreach (KeyValuePair<Guid, string> pair in result)
			{
				if (pair.Value == SearchValue)
				{
					MyKey = pair.Key;
					break;
				}
			}
			return MyKey;
		}

		public string LValue(string Table, string SearchColumn, Guid recordId)
		{

			Select select = new Select(UserConnection)
				.Column("Id")
				.Column(SearchColumn)
				.From(Table)
				.Where("Id").IsEqual(Column.Parameter(recordId)) as Select;

			var result = new Dictionary<Guid, string>();
			using (DBExecutor dbExecutor = UserConnection.EnsureDBConnection())
			{
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
				{
					while (dataReader.Read())
					{
						Guid key = dataReader.GetColumnValue<Guid>("Id");
						string value = dataReader.GetColumnValue<string>(SearchColumn);
						result.Add(key, value);
					}
				}
			}
			return result[recordId];
		}
		#endregion
	}
}