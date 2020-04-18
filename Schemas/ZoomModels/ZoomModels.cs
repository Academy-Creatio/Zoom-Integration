using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ZoomIntegration {
	
	[JsonObject]
	public class AccountUsers
	{

		[JsonProperty("page_count")]
		public int PageCount { get; set; }

		[JsonProperty("page_number")]
		public int PageNumber { get; set; }

		[JsonProperty("page_size")]
		public int PageSize { get; set; }

		[JsonProperty("total_records")]
		public int TotalRecords { get; set; }

		[JsonProperty("users")]
		public List<User> Users { get; set; }
	}

	[JsonObject]
	public class User
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("type")]
		public Int32 Type { get; set; }

		[JsonProperty("pmi")]
		public string Pmi { get; set; }

		[JsonProperty("timezone")]
		public string TimeZone { get; set; }

		[JsonProperty("verified")]
		public Int32 Verified { get; set; }

		[JsonProperty("dept")]
		public string Department { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("last_login_time")]
		public DateTime LastLoginTime { get; set; }

		[JsonProperty("last_client_version")]
		public string LastClientVersion { get; set; }
	}

	[JsonObject]
	public class ZoomErrorResponse
	{
		[JsonProperty("code")]
		public String Code { get; set; }

		[JsonProperty("message")]
		public String Message { get; set; }
	}

	[JsonObject]
	public class MeetingRequest
	{
		public MeetingRequest() { }
		public MeetingRequest(PredifinedMeetings P, string topic, string agenda, string start_time, int duration, string time_zone)
		{
			if (P == PredifinedMeetings.Certification)
			{
				Topic = topic;
				Agenda = agenda;
				StartTime = start_time;
				TimeZone = (string.IsNullOrEmpty(time_zone)) ? "Europe/Kiev" : time_zone;
				Type = MeetingType.ScheduledMeeting;
				Duration = duration;
				Settings = new Settings
				{
					HostVideo = false,
					ParticipantVideo = true,
					JoinBeforeHost = true,
					MuteUponEntry = false,
					Watermark = false,
					UsePmi = false,
					ApprovalType = SettingsApprovalType.AutoApprove,
					RegistrationType = SettingsRegistrationType.Once,
					Audio = "both",
					AudioRecording = "cloud",
					AlternativeHosts = string.Empty,
					CnMeeting = false,
					InMeeting = false,
					EnforceLogin = false
				};
			}
		}

		[JsonProperty("topic")]
		public string Topic { get; set; }

		[JsonProperty("type")]
		public MeetingType Type { get; set; }

		[JsonProperty("start_time")]
		public string StartTime { get; set; }

		[JsonProperty("duration")]
		public Int32 Duration { get; set; }

		[JsonProperty("timezone")]
		public string TimeZone { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("agenda")]
		public string Agenda { get; set; }

		[JsonProperty("recurrance")]
		public Recurrance Recurrance { get; set; }

		[JsonProperty("settings")]
		public Settings Settings { get; set; }
	}

	[JsonObject]
	public class Recurrance
	{
		[JsonProperty("type")]
		public RecurrenceType Type { get; set; }

		[JsonProperty("repeat_interval")]
		public Int16 RepeatInterval { get; set; }

		[JsonProperty("weekly_days")]
		public WeeklyDays WeeklyDays { get; set; }

		/// <summary>
		/// Day of the month for the meeting to be scheduled. The value range is from 1 to 31
		/// </summary>
		[JsonProperty("monthly_day")]
		public Int16 MonthlyDay { get; set; }

		[JsonProperty("monthly_week")]
		public MonthlyWeek MonthlyWeek { get; set; }

		[JsonProperty("monthly_week_day")]
		public WeeklyDays MonthlyWeekDay { get; set; }

		[JsonProperty("end_times")]
		public Int16 EndTimes { get; set; }

		[JsonProperty("end_date_time")]
		public string EndDayTime { get; set; }
	}

	[JsonObject]
	public class Settings
	{
		[JsonProperty("host_video")]
		public bool HostVideo { get; set; }

		[JsonProperty("participant_video")]
		public bool ParticipantVideo { get; set; }

		[JsonProperty("cn_meeting")]
		public bool CnMeeting { get; set; }

		[JsonProperty("in_meeting")]
		public bool InMeeting { get; set; }

		[JsonProperty("join_before_host")]
		public bool JoinBeforeHost { get; set; }

		[JsonProperty("mute_upon_entry")]
		public bool MuteUponEntry { get; set; }

		[JsonProperty("watermark")]
		public bool Watermark { get; set; }

		[JsonProperty("use_pmi")]
		public bool UsePmi { get; set; }

		[JsonProperty("approval_type")]
		public SettingsApprovalType ApprovalType { get; set; }

		[JsonProperty("registration_type")]
		public SettingsRegistrationType RegistrationType { get; set; }

		[JsonProperty("audio")]
		public string Audio { get; set; }

		[JsonProperty("audio_recording")]
		public string AudioRecording { get; set; }

		[JsonProperty("enforce_login")]
		public bool EnforceLogin { get; set; }

		[JsonProperty("enforce_login_domains")]
		public string EnforceLoginDomains { get; set; }

		[JsonProperty("alternative_hosts")]
		public string AlternativeHosts { get; set; }
	}

	public class MeetingResponse : MeetingRequest
	{
		[JsonProperty("uuid")]
		public string Uuid { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("host_id")]
		public string HostId { get; set; }

		[JsonProperty("created_at")]
		public string CreatedAt { get; set; }

		[JsonProperty("start_url")]
		public string StartUrl { get; set; }

		[JsonProperty("join_url")]
		public string JoinUrl { get; set; }

		[JsonProperty("registration_url")]
		public string RegistrationUrl { get; set; }

		[JsonProperty("occurences")]
		public List<Occurance> Occurances { get; set; }

	}

	[JsonObject]
	public class Occurance
	{
		[JsonProperty("occurence_id")]
		public Int32 OccurenceId { get; set; }

		[JsonProperty("start_time")]
		public string StartTime { get; set; }

		[JsonProperty("duration")]
		public Int16 Duration { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }
	}

	[JsonObject]
	public class MeetingParticipants
	{
		[JsonProperty("page_count")]
		public int Page_Count { get; set; }

		[JsonProperty("page_size")]
		public int Page_Size { get; set; }

		[JsonProperty("total_records")]
		public int Totoal_Records { get; set; }

		[JsonProperty("next_page_token")]
		public string Next_Page_Token { get; set; }

		[JsonProperty("participants")]
		public List<Participant> Participants { get; set; }
	}
	
	[JsonObject]
	public class Participant
	{

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("user_id")]
		public string User_id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("user_email")]
		public string User_Email { get; set; }

		[JsonProperty("join_time")]
		public DateTime Join_Time { get; set; }

		[JsonProperty("leave_time")]
		public DateTime Leave_Time { get; set; }

		[JsonProperty("duration")]
		public int Duration { get; set; }

		[JsonProperty("attentiveness_score")]
		public string Attentiveness_Score { get; set; }


		public TimeSpan TimeDuration
		{
			get
			{
				return new TimeSpan(0, 0, Duration);
			}

		}
	}

	[JsonObject]
	public class MeetingRecordings
	{

		[JsonProperty("uuid")]
		public string Uuid { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("account_id")]
		public string AccountId { get; set; }

		[JsonProperty("host_id")]
		public string HostId { get; set; }

		[JsonProperty("topic")]
		public string Topic { get; set; }

		[JsonProperty("start_time")]
		public DateTime StartTime { get; set; }

		[JsonProperty("timezone")]
		public string Timezone { get; set; }

		[JsonProperty("duration")]
		public Int32 Duration { get; set; }

		[JsonProperty("total_size")]
		public string TotalSize { get; set; }

		[JsonProperty("recording_count")]
		public Int32 RecordingCount { get; set; }

		[JsonProperty("recording_files")]
		public List<RecordingFile> RecordingFiles { get; set; }


	}

	[JsonObject]
	public class RecordingFile
	{

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("meeting_id")]
		public string MeetingId { get; set; }

		[JsonProperty("recording_start")]
		public DateTime RecordingStart { get; set; }

		[JsonProperty("recording_end")]
		public DateTime RecordingEnd { get; set; }

		[JsonProperty("file_type")]
		public string FileType { get; set; }

		[JsonProperty("file_size")]
		public Int32 FileSize { get; set; }


		[JsonProperty("play_url")]
		public string PlayUrl { get; set; }

		[JsonProperty("download_url")]
		public string DownloadUrl { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("recording_type")]
		public string RecordingType { get; set; }

	}
}