using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Terrasoft.Common;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Entities.Events;

namespace ZoomIntegration
{
	//https://academy.bpmonline.com/documents/technic-sdk/7-13/entity-event-layer
	[EntityEventListener(SchemaName = "ZoomAccounts")]
	public class ZoomAccountsEventListener : BaseEntityEventListener
	{
		public override void OnInserted(object sender, EntityAfterEventArgs e) {
			base.OnInserted(sender, e);
			
			_zoomAccounts = (Entity)sender;
			_userConnection = _zoomAccounts.UserConnection;
			string ApiKey = _zoomAccounts.GetTypedColumnValue<String>("ApiKey");
			string ApiSecret =_zoomAccounts.GetTypedColumnValue<String>("ApiSecret");
			if (ApiSecret.Length < 17){
			
				LocalizableString s = GetLocalizableString("ExceptionErrorMessage");
				throw new Exception(s);
			} else {
				Guid Id = _zoomAccounts.GetTypedColumnValue<Guid>("Id");
				GetAccountUsers(ApiKey, ApiSecret);
				InsertZoomUsers(Id);
				if (UsersInserted==0){
					throw new Exception(GetLocalizableString("NoNewUsersWereAdded"));
					
				}
			}
		}

		public override void OnUpdating (object sender, EntityBeforeEventArgs e){
			base.OnUpdating(sender, e);
			
			_zoomAccounts = (Entity)sender;
			_userConnection = _zoomAccounts.UserConnection;
			string ApiKey = _zoomAccounts.GetTypedColumnValue<String>("ApiKey");
			string ApiSecret =_zoomAccounts.GetTypedColumnValue<String>("ApiSecret");
			if (ApiSecret.Length < 17){
			
				LocalizableString s = GetLocalizableString("ExceptionErrorMessage");
				throw new Exception(s);
			} else {
				Guid Id = _zoomAccounts.GetTypedColumnValue<Guid>("Id");
				GetAccountUsers(ApiKey, ApiSecret);
				
				
				var delete = new Delete(_userConnection)
					.From("ZoomAccountUser")
					.Where("ZoomAccountId").IsEqual(Column.Parameter(Id))
					.Execute();

				
				InsertZoomUsers(Id);
				if (ErrorCode !=0){
					e.IsCanceled = true;
					//throw new Exception(GetLocalizableString("NoNewUsersWereAdded"));
					
				}
			}
		}


		private void InsertZoomUsers(Guid ZoomAccountId){
			var schema = _userConnection.EntitySchemaManager.GetInstanceByName("ZoomAccountUser");
			UsersInserted = 0;
			foreach (User i in AccountUsers.Users){

				var entity = schema.CreateEntity(_userConnection);
				entity.SetColumnValue("ZoomAccountId", ZoomAccountId);
				entity.SetColumnValue("UserId", i.Id);
				entity.SetColumnValue("FirstName", i.FirstName);
				entity.SetColumnValue("LastName", i.LastName);
				entity.SetColumnValue("Email", i.Email);
				entity.SetColumnValue("Type", i.Type);
				entity.SetColumnValue("PMI", i.Pmi);
				entity.SetColumnValue("Department", i.Department);
				entity.SetColumnValue("CreatedAt", i.CreatedAt);
				entity.SetColumnValue("LastLoginTime", i.LastLoginTime);
				entity.SetColumnValue("LastClientVersion", i.LastClientVersion);
				entity.SetColumnValue("TimeZone", i.TimeZone);
				entity.SetColumnValue("Verified", i.Verified);

				string hash = GetStringSha256Hash(StringToEncode(entity));
				entity.SetColumnValue("RowHash", hash);
				
				
				//check if record with this hash already exists
				string RecordId = (new Select(_userConnection)
					.Column("Id")
					.From("ZoomAccountUser")
					.Where("RowHash").IsEqual(Column.Parameter(hash)) as Select)
					.ExecuteScalar<string>();
					
				if (string.IsNullOrEmpty(RecordId)){
					entity.SetDefColumnValues();
					entity.Save(false);
					UsersInserted+=1;
				}
			}
		}
		private void GetAccountUsers(string ZoomApiKey, string ZoomApiSecret) {
			try
			{
				
				ZoomToken zt = new ZoomToken(ZoomApiKey, ZoomApiSecret);
				string Token = zt.Token;
				
				//Create new Request
				string BaseUrl = "https://api.zoom.us/v2/users?status=active&page_size=300&page_number=1";
				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);
				myHttpWebRequest.Method = "GET";
				myHttpWebRequest.ContentType = "application/json;";
				myHttpWebRequest.Accept = "application/json;";
				myHttpWebRequest.Headers.Add("Authorization", String.Format("Bearer {0}", Token));

				//Get the associated response for the above request
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				using (StreamReader MyStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), true))
				{
					this.AccountUsers = JsonConvert.DeserializeObject<AccountUsers>(MyStreamReader.ReadToEnd());
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
					//ZoomErrorResponse zm = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					//this.ZoomException = zm;
					this.ZoomException = JsonConvert.DeserializeObject<ZoomErrorResponse>(MyStreamReader.ReadToEnd());
					
					LocalizableString s = GetLocalizableString("ZoomApiErrorMessage");
					throw new Exception(s);
				}
			}
		}
		internal static string GetStringSha256Hash(string text) {
			if (String.IsNullOrEmpty(text))
				return String.Empty;

			using (var sha = new System.Security.Cryptography.SHA256Managed())
			{
				byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
				byte[] hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).Replace("-", String.Empty);
			}
		}
		private string  StringToEncode(Entity _zoomAccountUser) {
			return string.Format("Property:{0}, Value:{1}", "UserId", _zoomAccountUser.GetTypedColumnValue<String>("UserId"))+"|"+
			string.Format("Property:{0}, Value:{1}", "FirstName", _zoomAccountUser.GetTypedColumnValue<String>("FirstName"))+"|"+
			string.Format("Property:{0}, Value:{1}", "LastName", _zoomAccountUser.GetTypedColumnValue<String>("LastName"))+"|"+
			string.Format("Property:{0}, Value:{1}", "Email", _zoomAccountUser.GetTypedColumnValue<String>("Email"))+"|"+
			string.Format("Property:{0}, Value:{1}", "Type", _zoomAccountUser.GetTypedColumnValue<String>("Type"))+"|"+
			string.Format("Property:{0}, Value:{1}", "PMI", _zoomAccountUser.GetTypedColumnValue<String>("PMI"))+"|"+
			string.Format("Property:{0}, Value:{1}", "Department", _zoomAccountUser.GetTypedColumnValue<String>("Department"))+"|"+
			string.Format("Property:{0}, Value:{1}", "CreatedAt", _zoomAccountUser.GetTypedColumnValue<String>("CreatedAt"))+"|"+
			string.Format("Property:{0}, Value:{1}", "PMI", _zoomAccountUser.GetTypedColumnValue<String>("PMI"))+"|"+
			string.Format("Property:{0}, Value:{1}", "TimeZone", _zoomAccountUser.GetTypedColumnValue<String>("TimeZone"))+"|"+
			string.Format("Property:{0}, Value:{1}", "Verified", _zoomAccountUser.GetTypedColumnValue<String>("Verified"));
		}
		private Int32 UsersInserted;
		private Entity _zoomAccounts;
		private UserConnection _userConnection;
		private Int32 ErrorCode;
		private AccountUsers AccountUsers;
		private ZoomErrorResponse ZoomException;
		private LocalizableString GetLocalizableString(string name) {
			var stringName = string.Format("LocalizableStrings.{0}.Value", name);
			return new LocalizableString(_userConnection.Workspace.ResourceStorage, "ZoomAccountsEventListener", stringName);
		}
	}
}