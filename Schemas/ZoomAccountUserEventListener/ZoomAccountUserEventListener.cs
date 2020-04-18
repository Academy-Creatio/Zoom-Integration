
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Terrasoft.Common;
using Terrasoft.Configuration;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Entities.Events;

namespace ZoomIntegration
{
    //https://academy.bpmonline.com/documents/technic-sdk/7-13/entity-event-layer
    [EntityEventListener(SchemaName = "ZoomAccountUser")]
    public class ZoomAccountUserEventListener : BaseEntityEventListener
    {
        private Entity ZoomAccountUser;
        private UserConnection UserConnection;

        public override void OnInserting(object sender, EntityBeforeEventArgs e)
        {
            base.OnInserting(sender, e);
            ZoomAccountUser = (Entity)sender;
            UserConnection = ZoomAccountUser.UserConnection;

            string Email = ZoomAccountUser.GetTypedColumnValue<string>("Email");
            Guid ContactId = ColumnIdValue("ContactCommunication", "ContactId", "Number", Email);

            if (ContactId != Guid.Empty)
            {
                ZoomAccountUser.SetColumnValue("ContactId", ContactId);
                ZoomAccountUser.Save(false);
            }
            SendMessageToUi(
                ZoomAccountUser.GetTypedColumnValue<Guid>("ContactId"),
                "OnUpdating",
                (ZoomAccountUser.GetTypedColumnValue<Guid>("ContactId") == Guid.Empty) ? false : true);
        }

        public override void OnUpdating(object sender, EntityBeforeEventArgs e)
        {
            base.OnUpdating(sender, e);
            ZoomAccountUser = (Entity)sender;
            UserConnection = ZoomAccountUser.UserConnection;

            string Email = ZoomAccountUser.GetTypedColumnValue<string>("Email");
            Guid ContactId = ColumnIdValue("ContactCommunication", "ContactId", "Number", Email);

            if (ContactId != Guid.Empty)
            {
                ZoomAccountUser.SetColumnValue("ContactId", ContactId);
                ZoomAccountUser.Save(false);
            }
            SendMessageToUi(
                ZoomAccountUser.GetTypedColumnValue<Guid>("ContactId"),
                "OnUpdating",
                (ZoomAccountUser.GetTypedColumnValue<Guid>("ContactId") == Guid.Empty) ? false : true);
        }

        public override void OnUpdated(object sender, EntityAfterEventArgs e)
        {
            base.OnUpdated(sender, e);
            ZoomAccountUser = (Entity)sender;
            UserConnection = ZoomAccountUser.UserConnection;
        }

        static void SendMessageToUi(Guid ContactId, string MyEvent, bool Zoomable)
        {
            string senderName = "ZoomAccountUserEventListener";
            // Example for message
            string message = JsonConvert.SerializeObject(new
            {
                Event = MyEvent,
                Contact = ContactId,
                IsZoomable = Zoomable
            });
            // For all users
            MsgChannelUtilities.PostMessageToAll(senderName, message);
        }

        private Guid ColumnIdValue(string Table, string IdColumn, string SearchColumn, string SearchValue)
        {
            Select select = new Select(UserConnection)
                .Column(IdColumn)
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

        /*
        private LocalizableString GetLocalizableString(string name)
        {
            var stringName = string.Format("LocalizableStrings.{0}.Value", name);
            return new LocalizableString(UserConnection.Workspace.ResourceStorage, "ZoomAccountUserEventListener", stringName);
        }
        */
    }
}