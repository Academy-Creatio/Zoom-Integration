define("ActivityPageV2", ["ActivityPageV2","ext-base", "terrasoft","BaseMessageBridge"], 
	function(resources, Ext, Terrasoft, BaseMessageBridge) {
	return {
		entitySchemaName: "Activity",
		attributes: {
			"IsZoomable": {
				dataValueType: Terrasoft.DataValueType.BOOLEAN,
				value: false
			},
			
			"ShowZoomTab": {
				dataValueType: Terrasoft.DataValueType.BOOLEAN,
				dependencies: [
					{
						columns: ["CreateZoomMeeting"],
						methodName: "isZoomTabVisible"
					}
				]
			},
			"editMeeting": {
				dataValueType: this.Terrasoft.DataValueType.TEXT,
				dependencies: [
					{
						columns: ["MeetingId"],
						methodName: "BuildEditUrl"
					}
				]
			}
		},
		modules: /**SCHEMA_MODULES*/{}/**SCHEMA_MODULES*/,
		details: /**SCHEMA_DETAILS*/{
			"ZoomMeetingParticipantDetail": {
				"schemaName": "ZoomMeetingParticipantDetail",
				"entitySchemaName": "ZoomMeetingParticipant",
				"filter": {
					"detailColumn": "Activity",
					"masterColumn": "Id"
				}
			},
			"ZoomVideoRecordingsDetail": {
				"schemaName": "ZoomVideoRecordingsDetail",
				"entitySchemaName": "ZoomVideoRecordings",
				"filter": {
					"detailColumn": "Activity",
					"masterColumn": "Id"
				}
			}
		}/**SCHEMA_DETAILS*/,
		businessRules: /**SCHEMA_BUSINESS_RULES*/{}/**SCHEMA_BUSINESS_RULES*/,
		methods: {

			init: function() {
				this.callParent(arguments);
				Terrasoft.ServerChannel.on(Terrasoft.EventName.ON_MESSAGE, this.onMessageReceived, this);
				this.set("editMeeting","");
			},
			
			isZoomTabVisible: function(){
				var ZoomTab = this.$TabsCollection.get("Tab41aec888TabLabel");
				
				if (this.get("CreateZoomMeeting")===true){
					ZoomTab.set("Visible", true);
				}else{
					ZoomTab.set("Visible", false);
				}
			},
			onEntityInitialized: function() {
				this.callParent(arguments);
				this.checkIsUserZoomable();
				this.BuildEditUrl();
				this.isZoomTabVisible();
			},
			
			
			onMessageReceived: function(sender, message) {
				var result = "";
				if (message && message.Header && message.Header.Sender === "ZoomAccountUserEventListener") {
					result = this.Ext.decode(message.Body);
					var contact = result.Contact;
					var iszoomable = result.IsZoomable;
					//var event = result.Event;
					var CurrentUser = Terrasoft.SysValue.CURRENT_USER_CONTACT.value;
					if (CurrentUser === contact ){
						this.set("IsZoomable", iszoomable);
					}
				} 
			},
		
			getUsrURLpageLink: function(value) {
					return {
							"url": value,
							"caption": value
					};
			},
			onUsrURLpageLinkClick: function(url) {
					if (url != null) {
						window.open(url, "_blank");
						return false;
				}
			},
			BuildEditUrl: function(){
				var e = "https://zoom.us/user/" + this.get("HostId") + "/meeting/" + this.get("MeetingId"); 
				this.set("editMeeting", e);
			},

			checkIsUserZoomable: function(){
				var esq = this.Ext.create("Terrasoft.EntitySchemaQuery", 
				{
					rootSchemaName: "ZoomAccountUser"
				});
				// Add [UserId] column then add [UserId] alias to it
				esq.addColumn("FirstName");
				esq.addColumn("LastName");
				var CurrentUser = Terrasoft.SysValue.CURRENT_USER_CONTACT.value;
				
				var esqFilter = esq.createColumnFilterWithParameter(
					Terrasoft.ComparisonType.EQUAL, "Contact.Id", CurrentUser);
				esq.filters.addItem(esqFilter);
				
				esq.getEntityCollection(
					function(result) {
						if (result.success && result.collection.getItems().length > 0) {
							// error processing/logging, for example
							//this.showInformationDialog(result.collection.getItems().length);
							this.set("IsZoomable", true);
						} else {
							this.set("IsZoomable", false);
						}
					}, 
					this
				);
			}
		},
		dataModels: /**SCHEMA_DATA_MODELS*/{}/**SCHEMA_DATA_MODELS*/,
		diff: /**SCHEMA_DIFF*/[
			{
				"operation": "merge",
				"name": "StartDate",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 1
					}
				}
			},
			{
				"operation": "merge",
				"name": "Owner",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 1
					}
				}
			},
			{
				"operation": "move",
				"name": "Owner",
				"parentName": "Header",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "merge",
				"name": "DueDate",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 2
					}
				}
			},
			{
				"operation": "merge",
				"name": "Author",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 2
					}
				}
			},
			{
				"operation": "move",
				"name": "Author",
				"parentName": "Header",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "merge",
				"name": "Status",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 3
					}
				}
			},
			{
				"operation": "merge",
				"name": "Priority",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 3
					}
				}
			},
			{
				"operation": "move",
				"name": "Priority",
				"parentName": "Header",
				"propertyName": "items",
				"index": 7
			},
			{
				"operation": "merge",
				"name": "ShowInScheduler",
				"values": {
					"layout": {
						"colSpan": 6,
						"rowSpan": 1,
						"column": 0,
						"row": 4
					}
				}
			},
			{
				"operation": "move",
				"name": "ShowInScheduler",
				"parentName": "Header",
				"propertyName": "items",
				"index": 8
			},
			{
				"operation": "insert",
				"name": "CreateZoomMeeting",
				"values": {
					"layout": {
						"colSpan": 6,
						"rowSpan": 1,
						"column": 6,
						"row": 4,
						"layoutName": "Header"
					},
					"bindTo": "CreateZoomMeeting",
					"enabled": {
						"bindTo": "IsZoomable"
					}
				},
				"parentName": "Header",
				"propertyName": "items",
				"index": 9
			},
			{
				"operation": "merge",
				"name": "ActivityCategory",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 4
					}
				}
			},
			{
				"operation": "merge",
				"name": "CallDirection",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 5
					}
				}
			},
			{
				"operation": "merge",
				"name": "CustomActionSelectedResultControlGroup",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 0
					}
				}
			},
			{
				"operation": "merge",
				"name": "Result",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 0
					}
				}
			},
			{
				"operation": "merge",
				"name": "RemindToOwner",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 0
					},
					"labelConfig": {
						"caption": {
							"bindTo": "Resources.Strings.RemindToOwnerLabelCaption"
						}
					}
				}
			},
			{
				"operation": "merge",
				"name": "RemindToOwnerDate",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 0
					},
					"labelConfig": {
						"caption": {
							"bindTo": "Resources.Strings.RemindToOwnerDateLabelCaption"
						}
					}
				}
			},
			{
				"operation": "merge",
				"name": "RemindToAuthor",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 1
					},
					"labelConfig": {
						"caption": {
							"bindTo": "Resources.Strings.RemindToAuthorLabelCaption"
						}
					}
				}
			},
			{
				"operation": "merge",
				"name": "RemindToAuthorDate",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 1
					},
					"labelConfig": {
						"caption": {
							"bindTo": "Resources.Strings.RemindToAuthorDateLabelCaption"
						}
					}
				}
			},
			{
				"operation": "insert",
				"name": "Tab41aec888TabLabel",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Tab41aec888TabLabelTabCaption"
					},
					"items": []
				},
				"parentName": "Tabs",
				"propertyName": "tabs",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "Tab41aec888TabLabelGroup42a801a0",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Tab41aec888TabLabelGroup42a801a0GroupCaption"
					},
					"itemType": 15,
					"markerValue": "added-group",
					"items": []
				},
				"parentName": "Tab41aec888TabLabel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"values": {
					"itemType": 0,
					"items": []
				},
				"parentName": "Tab41aec888TabLabelGroup42a801a0",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "MeetingId",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 0,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "MeetingId",
					"enabled": true
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "HostId",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 12,
						"row": 0,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "HostId",
					"enabled": true
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "MeetingUUID",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 1,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "MeetingUUID",
					"enabled": true
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "AlternativeHosts",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 2,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "AlternativeHosts",
					"enabled": true
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "StartUrl",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 3,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "StartUrl",
					"enabled": true,
					"showValueAsLink": true,
					"href": {
						"bindTo": "StartUrl",
						"bindConfig": {
							"converter": "getUsrURLpageLink"
						}
					},
					"controlConfig": {
						"className": "Terrasoft.TextEdit",
						"linkclick": {
							"bindTo": "onUsrURLpageLinkClick"
						}
					}
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "EditMeeting",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 5,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "editMeeting",
					"labelConfig": {
						"caption": {
							"bindTo": "Resources.Strings.EditMeetingLabelCaption"
						}
					},
					"enabled": true,
					"showValueAsLink": true,
					"href": {
						"bindTo": "editMeeting",
						"bindConfig": {
							"converter": "getUsrURLpageLink"
						}
					},
					"controlConfig": {
						"className": "Terrasoft.TextEdit",
						"linkclick": {
							"bindTo": "onUsrURLpageLinkClick"
						}
					},
					"tip": {
						"content": {
							"bindTo": "Resources.Strings.EditMeetingUrlTip"
						}
					}
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 9
			},
			{
				"operation": "insert",
				"name": "JoinUrl",
				"values": {
					"layout": {
						"colSpan": 9,
						"rowSpan": 1,
						"column": 0,
						"row": 4,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "JoinUrl",
					"enabled": true,
					"showValueAsLink": true,
					"href": {
						"bindTo": "StartUrl",
						"bindConfig": {
							"converter": "getUsrURLpageLink"
						}
					},
					"controlConfig": {
						"className": "Terrasoft.TextEdit",
						"linkclick": {
							"bindTo": "onUsrURLpageLinkClick"
						}
					}
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "RegistrationUrl",
				"values": {
					"layout": {
						"colSpan": 15,
						"rowSpan": 1,
						"column": 9,
						"row": 4,
						"layoutName": "Tab41aec888TabLabelGridLayoutcb1e5d76"
					},
					"bindTo": "RegistrationUrl",
					"enabled": true,
					"showValueAsLink": true,
					"href": {
						"bindTo": "StartUrl",
						"bindConfig": {
							"converter": "getUsrURLpageLink"
						}
					},
					"controlConfig": {
						"className": "Terrasoft.TextEdit",
						"linkclick": {
							"bindTo": "onUsrURLpageLinkClick"
						}
					}
				},
				"parentName": "Tab41aec888TabLabelGridLayoutcb1e5d76",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "ZoomMeetingParticipantDetail",
				"values": {
					"itemType": 2,
					"markerValue": "added-detail"
				},
				"parentName": "Tab41aec888TabLabel",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "ZoomVideoRecordingsDetail",
				"values": {
					"itemType": 2,
					"markerValue": "added-detail"
				},
				"parentName": "Tab41aec888TabLabel",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "move",
				"name": "InformationOnStepButtonContainer",
				"parentName": "Header",
				"propertyName": "items",
				"index": 0
			}
		]/**SCHEMA_DIFF*/
	};
});
