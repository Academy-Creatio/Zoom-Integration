define("ActivityMiniPage", [], function() {
	return {
		entitySchemaName: "",
		attributes: {
			"IsZoomable": {
				dataValueType: Terrasoft.DataValueType.BOOLEAN,
				value: false
			}
		},
		modules: /**SCHEMA_MODULES*/{}/**SCHEMA_MODULES*/,
		details: /**SCHEMA_DETAILS*/{}/**SCHEMA_DETAILS*/,
		businessRules: /**SCHEMA_BUSINESS_RULES*/{}/**SCHEMA_BUSINESS_RULES*/,
		methods: {
			
			init: function(){
				this.callParent(arguments);
				this.checkIsUserZoomable();
			},
			
			checkIsUserZoomable: function(){
				var esq = this.Ext.create("Terrasoft.EntitySchemaQuery", 
				{
					rootSchemaName: "ZoomAccountUser"
				});

				var CurrentUser = Terrasoft.SysValue.CURRENT_USER_CONTACT.value;
				
				var esqFilter = esq.createColumnFilterWithParameter(
					Terrasoft.ComparisonType.EQUAL, "Contact.Id", CurrentUser);
				esq.filters.addItem(esqFilter);
				
				esq.getEntityCollection(
					function(result) {
						if (result.success && result.collection.getItems().length > 0) {
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
				"operation": "insert",
				"name": "CreateZoomMeeting508c18c1-c22d-4a24-a36a-9e4dae0c5277",
				"values": {
					"layout": {
						"colSpan": 24,
						"rowSpan": 1,
						"column": 0,
						"row": 13,
						"layoutName": "MiniPage"
					},
					"isMiniPageModelItem": true,
					"visible": {
						"bindTo": "isAddMode"
					},
					"bindTo": "CreateZoomMeeting",
					"enabled": {
						"bindTo": "IsZoomable"
					}
				},
				"parentName": "MiniPage",
				"propertyName": "items",
				"index": 10
			}
		]/**SCHEMA_DIFF*/
	};
});
