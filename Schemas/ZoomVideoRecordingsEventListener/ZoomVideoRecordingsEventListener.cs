using System;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Entities.Events;

namespace ZoomIntegration
{
	[EntityEventListener(SchemaName = "ZoomVideoRecordings")]
	public class ZoomVideoRecordingsEventListener : BaseEntityEventListener
	{

		private Entity ZoomVideoRecordings;
		private UserConnection _userConnection;

		public override void OnInserting(object sender, EntityBeforeEventArgs e)
		{
			base.OnInserting(sender, e);
			ZoomVideoRecordings = (Entity)sender;
			_userConnection = ZoomVideoRecordings.UserConnection;
			
			string hash = GetStringSha256Hash(StringToEncode());
			ZoomVideoRecordings.SetColumnValue("RecordHash", hash);
			
			//check if record with this has already exists
			string RecordId = (new Select(_userConnection)
				.Column("Id")
				.From("ZoomVideoRecordings")
				.Where("RecordHash").IsEqual(Column.Parameter(hash)) as Select)
				.ExecuteScalar<string>();
				
			if (!string.IsNullOrEmpty(RecordId)){
				e.IsCanceled = true;
			}
		}

		internal static string GetStringSha256Hash(string text)
		{
			if (string.IsNullOrEmpty(text))
				return string.Empty;

			using (var sha = new System.Security.Cryptography.SHA256Managed())
			{
				byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
				byte[] hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).Replace("-", String.Empty);
			}
		}
		
		private string  StringToEncode()
		{
			return string.Format("Property:{0}, Value:{1}", "FileId", ZoomVideoRecordings.GetTypedColumnValue<String>("FileId"))+"|"+
			string.Format("Property:{0}, Value:{1}", "MeetingId", ZoomVideoRecordings.GetTypedColumnValue<String>("MeetingId"))+"|"+
			string.Format("Property:{0}, Value:{1}", "RecordingStart", ZoomVideoRecordings.GetTypedColumnValue<String>("RecordingStart"))+"|"+
			string.Format("Property:{0}, Value:{1}", "RecordingEnd", ZoomVideoRecordings.GetTypedColumnValue<String>("RecordingEnd"))+"|"+
			string.Format("Property:{0}, Value:{1}", "FileType", ZoomVideoRecordings.GetTypedColumnValue<String>("FileType"))+"|"+
			string.Format("Property:{0}, Value:{1}", "FileSize", ZoomVideoRecordings.GetTypedColumnValue<String>("FileSize"))+"|"+
			string.Format("Property:{0}, Value:{1}", "PlayUrl", ZoomVideoRecordings.GetTypedColumnValue<String>("PlayUrl"))+"|"+
			string.Format("Property:{0}, Value:{1}", "DownloadUrl", ZoomVideoRecordings.GetTypedColumnValue<String>("DownloadUrl"))+"|"+
			string.Format("Property:{0}, Value:{1}", "Status", ZoomVideoRecordings.GetTypedColumnValue<String>("Status"))+"|"+
			string.Format("Property:{0}, Value:{1}", "RecordingType", ZoomVideoRecordings.GetTypedColumnValue<String>("RecordingType"));
		}

	}
}