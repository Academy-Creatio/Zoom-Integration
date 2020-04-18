namespace ZoomIntegration 
{
	public enum MeetingType
	{
		InstantMeeting = 1,
		ScheduledMeeting = 2,
		RecurringMeetingNoFixedTime = 3,
		RecurringMeetingFixedTime = 8
	}

	public enum RecurrenceType
	{
		Daily = 1,
		Weekly = 2,
		Monthly = 3
	}

	public enum WeeklyDays
	{
		Sunday = 1,
		Monday = 2,
		Tuesday = 3,
		Wednesday = 4,
		Thursday = 5,
		Friday = 6,
		Saturday = 7
	}

	public enum MonthlyWeek
	{
		LastWeek = -1,
		FirstWeek = 1,
		SecondWeek = 2,
		ThirdWeek = 3,
		FourthWeek = 4
	}

	public enum SettingsRegistrationType
	{
		Once = 1,
		ForEveryMeeting = 2,
		OnceAndChoose = 3
	}

	public enum SettingsApprovalType
	{
		AutoApprove = 0,
		ManuallyApprove = 1,
		NoRegistrationRequired = 3
	}

	public enum PredifinedMeetings
	{
		Certification = 1,
		Recurring = 2,
		Instant = 3,
		UpdateMeeting = 11
	}

}