define("ZoomMeetingPage", [], function() {
	return {
		entitySchemaName: "ZoomMeeting",
		attributes: {},
		modules: /**SCHEMA_MODULES*/{}/**SCHEMA_MODULES*/,
		details: /**SCHEMA_DETAILS*/{
			"Files": {
				"schemaName": "FileDetailV2",
				"entitySchemaName": "ZoomMeetingFile",
				"filter": {
					"masterColumn": "Id",
					"detailColumn": "ZoomMeeting"
				}
			}
		}/**SCHEMA_DETAILS*/,
		businessRules: /**SCHEMA_BUSINESS_RULES*/{}/**SCHEMA_BUSINESS_RULES*/,
		methods: {},
		dataModels: /**SCHEMA_DATA_MODELS*/{}/**SCHEMA_DATA_MODELS*/,
		diff: /**SCHEMA_DIFF*/[
			{
				"operation": "insert",
				"name": "Name",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "ProfileContainer"
					},
					"bindTo": "Name"
				},
				"parentName": "ProfileContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabel",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Taba449ce59TabLabelTabCaption"
					},
					"items": []
				},
				"parentName": "Tabs",
				"propertyName": "tabs",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGroupdc4974fc",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Taba449ce59TabLabelGroupdc4974fcGroupCaption"
					},
					"itemType": 15,
					"markerValue": "added-group",
					"items": []
				},
				"parentName": "Taba449ce59TabLabel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"values": {
					"itemType": 0,
					"items": []
				},
				"parentName": "Taba449ce59TabLabelGroupdc4974fc",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "STRING992ddd44-b605-4099-b650-a264a0654091",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "Taba449ce59TabLabelGridLayoutabf4c62e"
					},
					"bindTo": "MeetingId",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "STRING3d5a3bce-bf13-4f68-86e2-e018c140bf9a",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 0,
						"layoutName": "Taba449ce59TabLabelGridLayoutabf4c62e"
					},
					"bindTo": "MeetingUuid",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "STRING603cb4a4-aff7-43aa-bfda-e52adfb5d98f",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayoutabf4c62e"
					},
					"bindTo": "HostId",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "DATE7c5599a1-4ec4-4d64-8402-903d289bc404",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayoutabf4c62e"
					},
					"bindTo": "CreatedAt",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "STRING207c65fb-8536-447c-a915-68ac4728d238",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayoutabf4c62e"
					},
					"bindTo": "JoinUrl",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "STRING31b45738-604c-4f48-97b6-1c1597986418",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayoutabf4c62e"
					},
					"bindTo": "RegistrationUrl",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "STRING6f29ddb3-e9a7-4667-a717-4160aec124d7",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 3,
						"layoutName": "Taba449ce59TabLabelGridLayoutabf4c62e"
					},
					"bindTo": "StartUrl",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutabf4c62e",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGroup08a79718",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Taba449ce59TabLabelGroup08a79718GroupCaption"
					},
					"itemType": 15,
					"markerValue": "added-group",
					"items": []
				},
				"parentName": "Taba449ce59TabLabel",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGridLayout7adcc809",
				"values": {
					"itemType": 0,
					"items": []
				},
				"parentName": "Taba449ce59TabLabelGroup08a79718",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "STRING643f1cdc-1171-43b7-9fb7-b894e740d83e",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "Taba449ce59TabLabelGridLayout7adcc809"
					},
					"bindTo": "Topic",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayout7adcc809",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "STRINGba5a80e3-87bb-4390-8aeb-ada55ce791a9",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 0,
						"layoutName": "Taba449ce59TabLabelGridLayout7adcc809"
					},
					"bindTo": "Password",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayout7adcc809",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "LOOKUP0af2972c-31fe-4b6a-96e8-665e158cc628",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayout7adcc809"
					},
					"bindTo": "MeetingType",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayout7adcc809",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "LOOKUP20bfbef8-c48e-41fc-aa88-8cfcd6acbc7e",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayout7adcc809"
					},
					"bindTo": "TimeZone",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayout7adcc809",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "DATE55bba59d-64fd-43f0-a775-08ebe8785a14",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayout7adcc809"
					},
					"bindTo": "StartTime",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayout7adcc809",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "INTEGER7e8906a3-26e4-4043-93ab-5c57d00fa2df",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayout7adcc809"
					},
					"bindTo": "Duration",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayout7adcc809",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "STRING0c359126-ce2f-4475-82a1-5b55224f8e23",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 3,
						"layoutName": "Taba449ce59TabLabelGridLayout7adcc809"
					},
					"bindTo": "Agenda",
					"enabled": true,
					"contentType": 0
				},
				"parentName": "Taba449ce59TabLabelGridLayout7adcc809",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGroup15a7dbe2",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Taba449ce59TabLabelGroup15a7dbe2GroupCaption"
					},
					"itemType": 15,
					"markerValue": "added-group",
					"items": []
				},
				"parentName": "Taba449ce59TabLabel",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGridLayoutdb81293e",
				"values": {
					"itemType": 0,
					"items": []
				},
				"parentName": "Taba449ce59TabLabelGroup15a7dbe2",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "LOOKUPbdcf9b51-94c2-4129-85a1-91c76a801a85",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "Taba449ce59TabLabelGridLayoutdb81293e"
					},
					"bindTo": "RecurrenceType",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayoutdb81293e",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "LOOKUPefdb144e-25dc-48c2-bfd8-c386ad015e94",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayoutdb81293e"
					},
					"bindTo": "WeeklyDays",
					"enabled": true,
					"contentType": 5
				},
				"parentName": "Taba449ce59TabLabelGridLayoutdb81293e",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "INTEGER16cea6fa-88b3-4844-b6f8-b5212486e83c",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayoutdb81293e"
					},
					"bindTo": "MonthlyDay",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutdb81293e",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "LOOKUPf69d7e6d-a9d3-41f4-b645-37e676f8766e",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayoutdb81293e"
					},
					"bindTo": "MonthlyWeek",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayoutdb81293e",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "LOOKUP2a3cf7e3-a04b-4a83-9f63-6287d42cbf94",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayoutdb81293e"
					},
					"bindTo": "MonthlyWeekDay",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayoutdb81293e",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "INTEGER72cd7b96-3835-4254-90b7-fedbc012245e",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 3,
						"layoutName": "Taba449ce59TabLabelGridLayoutdb81293e"
					},
					"bindTo": "EndTimes",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutdb81293e",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "DATEef15654b-c151-47c0-a705-ec401effec2e",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 3,
						"layoutName": "Taba449ce59TabLabelGridLayoutdb81293e"
					},
					"bindTo": "EndDateTime",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutdb81293e",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGroup6e968f57",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Taba449ce59TabLabelGroup6e968f57GroupCaption"
					},
					"itemType": 15,
					"markerValue": "added-group",
					"items": []
				},
				"parentName": "Taba449ce59TabLabel",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"values": {
					"itemType": 0,
					"items": []
				},
				"parentName": "Taba449ce59TabLabelGroup6e968f57",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "BOOLEAN5ecad42f-7d9a-4ec6-8cdf-7faa108d869d",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "HostVideo",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "BOOLEANf66dd24e-2916-4043-acdc-423d231e9d65",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 0,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "ParticipantVideo",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "BOOLEAN9d813a48-97cc-43a8-9ed8-b6317fa3a591",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "ChinaMeeting",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "BOOLEANebd48208-d59c-4544-a55d-77809f88a725",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 1,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "IndiaMeeting",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "BOOLEAN89d0e920-5f2f-45e4-9127-a07484a1b53d",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "JoinBeforeHost",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "BOOLEAN7b06ac9e-aa29-4c41-af24-66f0b422d677",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 2,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "MuteUponEntry",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "BOOLEANff406126-71a6-4981-b7b7-2f9b3e4b0e56",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 3,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "Watermark",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "BOOLEAN5f58deb7-8426-4fd3-829d-adcd347f7ab3",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 3,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "UsePmi",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "insert",
				"name": "LOOKUPc3246cda-86b4-46e5-b702-2c20f66648bd",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 4,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "ApprovalType",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 8
			},
			{
				"operation": "insert",
				"name": "LOOKUPfdf03c42-9521-4bb8-86b7-30ba1c1626f4",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 4,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "RegistrationType",
					"enabled": true,
					"contentType": 5
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 9
			},
			{
				"operation": "insert",
				"name": "LOOKUPa5a4f3a0-7cc4-4327-bceb-d1ccdc288446",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 5,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "Audio",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 10
			},
			{
				"operation": "insert",
				"name": "LOOKUP260aa145-6491-407a-b1c3-eace262895ea",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 5,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "AutoRecording",
					"enabled": true,
					"contentType": 3
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 11
			},
			{
				"operation": "insert",
				"name": "BOOLEAN3f44a390-c9be-4d14-a027-839577d3c6e1",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 6,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "EnforceLogin",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 12
			},
			{
				"operation": "insert",
				"name": "STRING49200ba8-7942-44bb-be02-f4ce04ae0854",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 6,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "EnforceLoginDomains",
					"enabled": true
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 13
			},
			{
				"operation": "insert",
				"name": "STRING693bc364-b37a-4a8b-9280-d900069c6077",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 7,
						"layoutName": "Taba449ce59TabLabelGridLayoutf9f8cbc5"
					},
					"bindTo": "AlternativeHosts",
					"enabled": true,
					"contentType": 0
				},
				"parentName": "Taba449ce59TabLabelGridLayoutf9f8cbc5",
				"propertyName": "items",
				"index": 14
			},
			{
				"operation": "insert",
				"name": "NotesAndFilesTab",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.NotesAndFilesTabCaption"
					},
					"items": []
				},
				"parentName": "Tabs",
				"propertyName": "tabs",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Files",
				"values": {
					"itemType": 2
				},
				"parentName": "NotesAndFilesTab",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NotesControlGroup",
				"values": {
					"itemType": 15,
					"caption": {
						"bindTo": "Resources.Strings.NotesGroupCaption"
					},
					"items": []
				},
				"parentName": "NotesAndFilesTab",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "Notes",
				"values": {
					"bindTo": "Notes",
					"dataValueType": 1,
					"contentType": 4,
					"layout": {
						"column": 0,
						"row": 0,
						"colSpan": 24
					},
					"labelConfig": {
						"visible": false
					},
					"controlConfig": {
						"imageLoaded": {
							"bindTo": "insertImagesToNotes"
						},
						"images": {
							"bindTo": "NotesImagesCollection"
						}
					}
				},
				"parentName": "NotesControlGroup",
				"propertyName": "items",
				"index": 0
			}
		]/**SCHEMA_DIFF*/
	};
});
