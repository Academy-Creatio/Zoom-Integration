using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using Terrasoft.Common;
using Terrasoft.Core;
using Terrasoft.Core.DB;

namespace ZoomIntegration
{
	public class ZoomMeetingApi {

		private UserConnection _userConnection;
		private string ApiKey;
		private string ApiSecret;
		private string HostId;
		private string MyToken;

		//public int Code { get; set; }
		public MeetingResponse mResponse { get; set; }
		public MeetingResponse retrieveMeetingResponse { get; set; }
		public MeetingRecordings rResponse { get; set; }
		public MeetingParticipants pResponse { get; set; }
		public ZoomErrorResponse ZoomException { get; set; }
		public int ErrorCode { get; set; }
		public string MeetingId { get; set; }


		public ZoomMeetingApi(UserConnection userConnection) {
			_userConnection = userConnection;
			Guid CUC = _userConnection.CurrentUser.ContactId;

			Select select = new Select(_userConnection)
				.Column("ApiKey")
				.Column("ApiSecret")
				.Column("ZoomAccountUser", "UserId").As("UserId")
				.From("ZoomAccounts")
				.LeftOuterJoin("ZoomAccountUser").On("ZoomAccounts", "Id").IsEqual("ZoomAccountUser", "ZoomAccountId")
				.Where("ZoomAccountUser", "ContactId").IsEqual(Column.Parameter(_userConnection.CurrentUser.ContactId.ToString())) as Select;

			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection())
			{
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
				{
					while (dataReader.Read())
					{
						this.ApiKey = dataReader.GetColumnValue<string>("ApiKey");
						this.ApiSecret = dataReader.GetColumnValue<string>("ApiSecret");
						this.HostId = dataReader.GetColumnValue<string>("UserId");
					}
				}
			}
			ZoomToken zt = new ZoomToken(ApiKey, ApiSecret);
			//1. Find user who is creating the meeting
			//2. Find account that he/she relates to and grab Key and Secret
			//3. Generate Token
			this.MyToken = zt.Token;
		}

		public void CreateZoomMeeting(MeetingRequest mRequest)
		{
			try
			{
				//Create new Request
				string BaseUrl = String.Format("https://api.zoom.us/v2/users/{0}/meetings", HostId);
				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);
				myHttpWebRequest.Method = "POST";
				myHttpWebRequest.ContentType = "application/json;";
				myHttpWebRequest.Accept = "application/json;";
				myHttpWebRequest.Headers.Add("Authorization", String.Format("Bearer {0}", MyToken));
				using (StreamWriter sw = new StreamWriter(myHttpWebRequest.GetRequestStream()))
				{
					JsonSerializerSettings jss = new JsonSerializerSettings();
					jss.Formatting = Formatting.Indented;
					jss.MissingMemberHandling = MissingMemberHandling.Ignore;
					string json = JsonConvert.SerializeObject(mRequest, jss);

					sw.Write(json);
					sw.Flush();
					sw.Close();
				}

				//Get the associated response for the above request
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				using (StreamReader MyStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), true))
				{
					mResponse = JsonConvert.DeserializeObject<MeetingResponse>(MyStreamReader.ReadToEnd());
				}
				myHttpWebResponse.Close();
				myHttpWebResponse.Dispose();

			}
			catch (WebException ex)
			{
				int errorCode = (int)((HttpWebResponse)ex.Response).StatusCode;
				if (errorCode != 0)
				{
					ErrorCode = errorCode;
					Stream MyStream = ex.Response.GetResponseStream();
					StreamReader MyStreamReader = new StreamReader(MyStream, true);
					ZoomErrorResponse zm = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					this.ZoomException = zm;
				}
			}
		}

		public void DeleteZoomMeeting(string MeetingId)
		{

			try
			{
				//Create new Request
				string BaseUrl = String.Format("https://api.zoom.us/v2/meetings/{0}", MeetingId);
				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);
				myHttpWebRequest.Method = "DELETE";
				myHttpWebRequest.ContentType = "application/json;";
				myHttpWebRequest.Accept = "application/json;";
				myHttpWebRequest.Headers.Add("Authorization", String.Format("Bearer {0}", MyToken));


				//Get the associated response for the above request
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				using (StreamReader MyStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), true))

					myHttpWebResponse.Close();
				myHttpWebResponse.Dispose();

			}
			catch (WebException ex)
			{
				int errorCode = (int)((HttpWebResponse)ex.Response).StatusCode;
				if (errorCode != 0)
				{
					ErrorCode = errorCode;
					Stream MyStream = ex.Response.GetResponseStream();
					StreamReader MyStreamReader = new StreamReader(MyStream, true);
					ZoomErrorResponse zm = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					this.ZoomException = zm;
				}
			}

		}

		public void UpdateZoomMeeting(MeetingRequest mRequest, string MeetingId)
		{
			try
			{
				//Create new Request
				string BaseUrl = String.Format("https://api.zoom.us/v2/meetings/{0}", MeetingId);
				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);
				myHttpWebRequest.Method = "PATCH";
				myHttpWebRequest.ContentType = "application/json;";
				myHttpWebRequest.Accept = "application/json;";
				myHttpWebRequest.Headers.Add("Authorization", String.Format("Bearer {0}", MyToken));
				using (StreamWriter sw = new StreamWriter(myHttpWebRequest.GetRequestStream()))
				{
					JsonSerializerSettings jss = new JsonSerializerSettings();
					jss.Formatting = Formatting.Indented;
					jss.MissingMemberHandling = MissingMemberHandling.Ignore;
					string json = JsonConvert.SerializeObject(mRequest, jss);

					sw.Write(json);
					sw.Flush();
					sw.Close();
				}

				//Get the associated response for the above request
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				using (StreamReader MyStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), true))
				{
					mResponse = JsonConvert.DeserializeObject<MeetingResponse>(MyStreamReader.ReadToEnd());
				}
				myHttpWebResponse.Close();
				myHttpWebResponse.Dispose();

			}
			catch (WebException ex)
			{
				int errorCode = (int)((HttpWebResponse)ex.Response).StatusCode;
				if (errorCode != 0)
				{
					ErrorCode = errorCode;
					Stream MyStream = ex.Response.GetResponseStream();
					StreamReader MyStreamReader = new StreamReader(MyStream, true);
					ZoomErrorResponse zm = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					this.ZoomException = zm;
				}
			}
		}

		public void RetrieveMeeting(string MeetingId)
		{

			try
			{
				//Create new Request
				string BaseUrl = String.Format("https://api.zoom.us/v2/meetings/{0}", MeetingId);
				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);
				myHttpWebRequest.Method = "GET";
				myHttpWebRequest.ContentType = "application/json;";
				myHttpWebRequest.Accept = "application/json;";
				myHttpWebRequest.Headers.Add("Authorization", String.Format("Bearer {0}", MyToken));

				//Get the associated response for the above request
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				using (StreamReader MyStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), true))
				{
					retrieveMeetingResponse = JsonConvert.DeserializeObject<MeetingResponse>(MyStreamReader.ReadToEnd());
				}

				myHttpWebResponse.Close();
				myHttpWebResponse.Dispose();

			}
			catch (WebException ex)
			{
				int errorCode = (int)((HttpWebResponse)ex.Response).StatusCode;
				if (errorCode != 0)
				{
					ErrorCode = errorCode;
					Stream MyStream = ex.Response.GetResponseStream();
					StreamReader MyStreamReader = new StreamReader(MyStream, true);
					ZoomErrorResponse zm = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					this.ZoomException = zm;
				}
			}
		}
		
		public void GetMeetingParticipants(string MeetingId) 
		{
			try
			{
				//Create new Request
				string CBaseUrl = String.Format("https://api.zoom.us/v2/report/meetings/{0}/participants?page_size=300", MeetingId);
				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(CBaseUrl);
				myHttpWebRequest.Method = "GET";
				myHttpWebRequest.ContentType = "application/json;";
				myHttpWebRequest.Accept = "application/json;";
				myHttpWebRequest.Headers.Add("Authorization", String.Format("Bearer {0}", MyToken));

				//Get the associated response for the above request
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				using (StreamReader MyStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), true))
				{
					pResponse = JsonConvert.DeserializeObject<MeetingParticipants>(MyStreamReader.ReadToEnd());
				}
		
				myHttpWebResponse.Close();
				myHttpWebResponse.Dispose();
		
			}
			catch (WebException ex)
			{
				int errorCode = (int)((HttpWebResponse)ex.Response).StatusCode;
				if (errorCode != 0)
				{
					ErrorCode = errorCode;
					Stream MyStream = ex.Response.GetResponseStream();
					StreamReader MyStreamReader = new StreamReader(MyStream, true);
					ZoomErrorResponse zm = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					this.ZoomException = zm;
				}
			}
		}
		
		public void GetRecordings(string MeetingId) 
		{
			try
			{
				//Create new Request
				string CBaseUrl = String.Format("https://api.zoom.us/v2/meetings/{0}/recordings", MeetingId);
				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(CBaseUrl);
				myHttpWebRequest.Method = "GET";
				myHttpWebRequest.ContentType = "application/json;";
				myHttpWebRequest.Accept = "application/json;";
				myHttpWebRequest.Headers.Add("Authorization", String.Format("Bearer {0}", MyToken));
		

				//Get the associated response for the above request
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				using (StreamReader MyStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), true))
				{
					rResponse = JsonConvert.DeserializeObject<MeetingRecordings>(MyStreamReader.ReadToEnd());
				}
		
				myHttpWebResponse.Close();
				myHttpWebResponse.Dispose();
		
			}
			catch (WebException ex)
			{
				int errorCode = (int)((HttpWebResponse)ex.Response).StatusCode;
				if (errorCode != 0)
				{
					ErrorCode = errorCode;
					Stream MyStream = ex.Response.GetResponseStream();
					StreamReader MyStreamReader = new StreamReader(MyStream, true);
					ZoomErrorResponse zm = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					this.ZoomException = zm;
				}
			}
		}
		
		public string LValue(string Table, string SearchColumn, Guid recordId)
		{
			
			Select select = new Select(_userConnection)
				.Column("Id")
				.Column(SearchColumn)
				.From(Table)
				.Where("Id").IsEqual(Column.Parameter(recordId)) as Select;
			
			var result = new Dictionary<Guid, string>();
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection()) {
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor)) {
					while (dataReader.Read()) {
						Guid key = dataReader.GetColumnValue<Guid>("Id");
						string value = dataReader.GetColumnValue<string>(SearchColumn);
						result.Add(key, value);
					}
				}
			}
			return result[recordId];
		}
		
		public Guid IdValue(string Table, string SearchColumn, string SearchValue)
		{
			Select select = new Select(_userConnection)
				.Column("Id")
				.Column(SearchColumn)
				.From(Table)
				.Where(SearchColumn).IsEqual(Column.Parameter(SearchValue)) as Select;

			var result = new Dictionary<Guid, string>();
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection())
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
		
		public Guid ColumnIdValue(string Table, string IdColumn, string SearchColumn, string SearchValue)
		{
			Select select = new Select(_userConnection)
				.Column(IdColumn)
				.Column(SearchColumn)
				.From(Table)
				.Where(SearchColumn).IsEqual(Column.Parameter(SearchValue)) as Select;

			var result = new Dictionary<Guid, string>();
			using (DBExecutor dbExecutor = _userConnection.EnsureDBConnection())
			{
				using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
				{
					while (dataReader.Read())
					{
						Guid key = dataReader.GetColumnValue<Guid>(IdColumn);
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
	}
}