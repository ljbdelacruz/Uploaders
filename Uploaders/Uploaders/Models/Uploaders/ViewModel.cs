using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Services.Notification;

namespace Uploaders.Models.Uploaders
{
    public class CompanyAPIKeyVM {
        public static CompanyAPIKey Set(Guid id, Guid cid, Guid apiKey, DateTime dStart, DateTime dEnd) {
            try {
                return new CompanyAPIKey() {
                    ID = id,
                    CompanyID = cid,
                    APIKey = apiKey,
                    DateExpired = dEnd,
                    DateStarted = dStart
                };
            } catch { return null; }
        }
    }
    #region Messaging Platforms
    #region CloudMessaging
    public class CloudMessagingRoomVM
    {
        public string ID { get; set; }
        public string RoomName { get; set; }

        public static CloudMessagingRoom Set(Guid id, string room, Guid api, bool isSync)
        {
            try
            {
                return new CloudMessagingRoom()
                {
                    ID = id,
                    RoomName = room,
                    APIKey = api,
                    isSync = isSync
                };
            }
            catch { return null; }
        }
        public static CloudMessagingRoomVM MToVM(CloudMessagingRoom model)
        {
            try
            {
                return new CloudMessagingRoomVM()
                {
                    ID = model.ID.ToString(),
                    RoomName = model.RoomName
                };
            }
            catch { return null; }
        }

    }
    public class CloudRoomParticipantsVM
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string RoomID { get; set; }
        #region static methods
        public static CloudRoomParticipants Set(Guid id, Guid uid, Guid rid)
        {
            try
            {
                return new CloudRoomParticipants()
                {
                    ID = id,
                    UserID = uid,
                    RoomID = rid
                };
            }
            catch { return null; }
        }
        public static CloudRoomParticipantsVM MToVM(CloudRoomParticipants model)
        {
            try
            {
                return new CloudRoomParticipantsVM()
                {
                    ID = model.ID.ToString(),
                    UserID = model.UserID.ToString(),
                    RoomID = model.RoomID.ToString()
                };
            }
            catch { return null; }
        }
        public static List<CloudRoomParticipantsVM> MsToVMs(List<CloudRoomParticipants> models)
        {
            var list = new List<CloudRoomParticipantsVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class CloudMessageReceipentVM
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string CloudMessagingConversationID { get; set; }
        public string RoomID { get; set; }
        #region static methods
        public static CloudMessageReceipent Set(Guid id, Guid uid, Guid cmcID, DateTime createdAt, Guid roomID)
        {
            try
            {
                return new CloudMessageReceipent()
                {
                    ID = id,
                    UserID = uid,
                    CloudMessagingConversationID = cmcID,
                    CreatedAt = createdAt,
                    RoomID = roomID
                };
            }
            catch { return null; }
        }
        public static CloudMessageReceipentVM MToVM(CloudMessageReceipent model)
        {
            try
            {
                return new CloudMessageReceipentVM()
                {
                    ID = model.ID.ToString(),
                    UserID = model.UserID.ToString(),
                    CloudMessagingConversationID = model.CloudMessagingConversationID.ToString(),
                    RoomID = model.RoomID.ToString()
                };
            }
            catch { return null; }
        }
        public static List<CloudMessageReceipentVM> MsToVMs(List<CloudMessageReceipent> models)
        {
            var list = new List<CloudMessageReceipentVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    public class CloudMessagingConversationVM
    {
        public string ID { get; set; }
        public string RoomConversation { get; set; }
        public string MessageType { get; set; }
        public string Text { get; set; }
        public bool isSync { get; set; }
        public string CreatedAt { get; set; }
        public string SenderID { get; set; }
        #region static methods
        public static CloudMessagingConversation Set(Guid id, Guid rc, Guid mt, string text, bool isSync, Guid senderID)
        {
            try
            {
                return new CloudMessagingConversation()
                {
                    ID = id,
                    RoomConversation = rc,
                    MessageType = mt,
                    Text = text,
                    isSync = isSync,
                    SenderID = senderID
                };
            }
            catch { return null; }
        }
        public static CloudMessagingConversationVM MToVM(CloudMessagingConversation model)
        {
            try
            {
                return new CloudMessagingConversationVM()
                {
                    ID = model.ID.ToString(),
                    RoomConversation = model.RoomConversation.ToString(),
                    MessageType = model.MessageType.ToString(),
                    Text = model.Text,
                    isSync = model.isSync,
                    SenderID = model.SenderID.ToString()
                };
            }
            catch { return null; }
        }
        public static List<CloudMessagingConversationVM> MsToVMs(List<CloudMessagingConversation> models)
        {
            var list = new List<CloudMessagingConversationVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }

    #endregion
    #region MessagingApp
    public class MessagingRoomVM {
        public string ID { get; set; }
        public string Name { get; set; }
        #region static methods
        public static MessagingRoom set(Guid id, string name, Guid api, DateTime createdAt) {
            try {
                return new MessagingRoom() {
                    ID = id,
                    Name = name,
                    API = api,
                    createdAt = createdAt
                };
            } catch { return null; }
        }
        public static MessagingRoomVM MToVM(MessagingRoom model) {
            try {
                return new MessagingRoomVM() {
                    ID = model.ID.ToString(),
                    Name = model.Name,
                };
            } catch { return null; }
        }
        public static List<MessagingRoomVM> MsToVMs(List<MessagingRoom> models) {
            var list = new List<MessagingRoomVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class MessagingRoomParticipantsVM {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string RoomID { get; set; }
        #region static methods
        public static MessagingRoomParticipants set(Guid id, string uid, Guid rid, Guid api) {
            try {
                return new MessagingRoomParticipants() {
                    ID = id,
                    UserID = uid,
                    RoomID = rid,
                    API=api
                };
            } catch { return null; }
        }
        public static MessagingRoomParticipantsVM MToVM(MessagingRoomParticipants model) {
            try {
                return new MessagingRoomParticipantsVM() {
                    ID = model.ID.ToString(),
                    UserID = model.UserID,
                    RoomID = model.RoomID.ToString()
                };
            } catch { return null; }
        }
        public static List<MessagingRoomParticipantsVM> MsToVMs(List<MessagingRoomParticipants> models) {
            var list = new List<MessagingRoomParticipantsVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    public class MessagingConversationVM {
        public string ID { get; set; }
        public string Text { get; set; }
        public string MessageType { get; set; }
        public string SenderID { get; set; }
        public string RoomID { get; set; }
        public string CreatedAt { get; set; }
        #region static methods
        public static MessagingConversation set(Guid id, string text, Guid messageType, string senderID, Guid roomID, DateTime createdAt) {
            try {
                return new MessagingConversation() {
                    ID = id,
                    Text = text,
                    MessageType = messageType,
                    SenderID = senderID,
                    RoomID = roomID,
                    CreatedAt = createdAt
                };
            } catch { return null; }
        }
        public static MessagingConversationVM MToVM(MessagingConversation model) {
            try {
                return new MessagingConversationVM() {
                    ID = model.ID.ToString(),
                    Text = model.Text,
                    MessageType = model.MessageType.ToString(),
                    SenderID = model.SenderID,
                    RoomID = model.RoomID.ToString(),
                    CreatedAt = Utility.Strings.StringConverters.DateTimeToStrings(model.CreatedAt)
                };
            } catch { return null; }
        }
        public static List<MessagingConversationVM> MsToVMs(List<MessagingConversation> models) {
            var list = new List<MessagingConversationVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #endregion
    #region Notification Manager
    public class NotificationManagerVM {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string UID { get; set; }

        public static NotificationManager Set(Guid id, string message, Guid API, string title) {
            try {
                return new NotificationManager() {
                    ID = id,
                    Title=title,
                    Message = message,
                    apiKey = API,
                };
            } catch { return null; }
        }
        public static NotificationManagerVM MToVM(NotificationManager model) {
            try {
                return new NotificationManagerVM() {
                    ID = model.ID.ToString(),
                    Title = model.Title,
                    Message = model.Message
                };
            } catch { return null; }
        }
        public static List<NotificationManagerVM> MsToVMs(List<NotificationManager> models) {
            var list = new List<NotificationManagerVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
    }
    public class NotificationManagerReceipentVM {
        public string ID { get; set; }
        #region static methods
        public static NotificationManagerReceipent set(Guid id, Guid notifID, string rid, Guid api) {
            try {
                return new NotificationManagerReceipent() {
                    ID=id,
                    NotificationInfo=notifID,
                    ReceiverID=rid,
                    API=api
                };
            } catch { return null; }
        }
        #endregion
    }

    #endregion
    #region UserInformation
    public class UserInformationVM {
        public string ID { get; set; }
        public string UserID { get; set;}
        public string SignalRID { get; set; }
        public bool isActive { get; set; }
        public static UserInformation Set(Guid id, string uid, Guid sid, bool isActive) {
            try {
                return new UserInformation() {
                    ID=id,
                    UserID=uid, 
                    SignalRID=sid,
                    isActive=isActive
                };
            } catch { return null; }
        }
        public static UserInformationVM MToVM(UserInformation model) {
            try {
                return new UserInformationVM() {
                    ID=model.ID.ToString(),
                    UserID=model.UserID.ToString(),
                    SignalRID=model.SignalRID.ToString(),
                    isActive=model.isActive
                };
            } catch { return null; }
        }
        public static List<UserInformationVM> MsToVMs(List<UserInformation> models) {
            var list = new List<UserInformationVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
    }
    #endregion
    #region Security Code Generator
    public class SecurityCodeVM {
        public string ID { get; set; }
        public string API { get; set; }
        public string Code { get; set; }
        public string OwnerID { get; set; }
        #region static methods
        public static SecurityCode Set(Guid id, Guid api, string code, string oid) {
            try {
                return new SecurityCode() {
                    ID=id,
                    API=api,
                    Code=code,
                    OwnerID=oid
                };
            } catch { return null; }
        }
        public static SecurityCodeVM MToVM(SecurityCode model) {
            try {
                return new SecurityCodeVM() {
                    ID=model.ID.ToString(),
                    API=model.API.ToString(),
                    Code=model.Code,
                    OwnerID=model.OwnerID
                };
            } catch { return null; }
        }
        public static List<SecurityCodeVM> MsToVMs(List<SecurityCode> models) {
            var list = new List<SecurityCodeVM>();
            foreach (var model in models) {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
    #region Image Link Storage
    public class ImageLinkStorageVM
    {
        public string ID { get; set; }
        public string Source { get; set; }
        #region static methods
        public static ImageLinkStorage Set(Guid id, Guid api, Guid ownerID, string source)
        {
            try
            {
                return new ImageLinkStorage()
                {
                    ID = id,
                    API = api,
                    OwnerID = ownerID,
                    Source = source
                };
            }
            catch { return null; }
        }
        public static ImageLinkStorageVM MToVM(ImageLinkStorage model)
        {
            try
            {
                return new ImageLinkStorageVM()
                {
                    ID = model.ID.ToString(),
                    Source = model.Source
                };
            }
            catch { return null; }
        }
        public static List<ImageLinkStorageVM> MsToVMs(List<ImageLinkStorage> models)
        {
            var list = new List<ImageLinkStorageVM>();
            foreach (var model in models)
            {
                list.Add(MToVM(model));
            }
            return list;
        }
        #endregion
    }
    #endregion
}