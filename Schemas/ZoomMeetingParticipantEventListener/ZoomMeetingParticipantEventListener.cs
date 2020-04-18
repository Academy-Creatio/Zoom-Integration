using System;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Entities.Events;

namespace ZoomIntegration
{
	//https://academy.bpmonline.com/documents/technic-sdk/7-13/entity-event-layer
	[EntityEventListener(SchemaName = "ZoomMeetingParticipant")]
	public class ZoomMeetingParticipantEventListener : BaseEntityEventListener
	{
		private Entity _certificationParticipant;
		private UserConnection _userConnection;

		public override void OnInserting(object sender, EntityBeforeEventArgs e)
		{
			base.OnInserting(sender, e);
			_certificationParticipant = (Entity)sender;
			_userConnection = _certificationParticipant.UserConnection;
			
			string hash = GetStringSha256Hash(StringToEncode());
			_certificationParticipant.SetColumnValue("RecordHash", hash);
			
			
			//check if record with this has already exists
			string RecordId = (new Select(_userConnection)
				.Column("Id")
				.From("ZoomMeetingParticipant")
				.Where("RecordHash").IsEqual(Column.Parameter(hash)) as Select)
				.ExecuteScalar<string>();
				
			if (!string.IsNullOrEmpty(RecordId)){
				e.IsCanceled = true;
			}
		}
		
		internal static string GetStringSha256Hash(string text)
		{
			if (String.IsNullOrEmpty(text))
				return String.Empty;

			using (var sha = new System.Security.Cryptography.SHA256Managed())
			{
				byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
				byte[] hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).Replace("-", String.Empty);
			}
		}
		
		private string  StringToEncode()
		{
			return string.Format("Property:{0}, Value:{1}", "ParticipantName", _certificationParticipant.GetTypedColumnValue<String>("ParticipantName"))+"|"+
			string.Format("Property:{0}, Value:{1}", "ParticipantEmail", _certificationParticipant.GetTypedColumnValue<String>("ParticipantEmail"))+"|"+
			string.Format("Property:{0}, Value:{1}", "JoinTime", _certificationParticipant.GetTypedColumnValue<String>("JoinTime"))+"|"+
			string.Format("Property:{0}, Value:{1}", "LeaveTime", _certificationParticipant.GetTypedColumnValue<String>("LeaveTime"))+"|"+
			string.Format("Property:{0}, Value:{1}", "Duration", _certificationParticipant.GetTypedColumnValue<String>("Duration"))+"|"+
			string.Format("Property:{0}, Value:{1}", "Attentiveness", _certificationParticipant.GetTypedColumnValue<String>("Attentiveness"))+"|"+
			string.Format("Property:{0}, Value:{1}", "ZoomParticipantId", _certificationParticipant.GetTypedColumnValue<String>("ZoomParticipantId"))+"|"+
			string.Format("Property:{0}, Value:{1}", "ParticipantUserId", _certificationParticipant.GetTypedColumnValue<String>("ParticipantUserId"));
		}

	}
}