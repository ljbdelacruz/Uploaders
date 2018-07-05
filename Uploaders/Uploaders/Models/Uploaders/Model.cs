using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Uploaders.Models.Uploaders
{
    public class CompanyAPIKey {
        public Guid ID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid APIKey { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateExpired { get; set; }
    }
    #region Messaging Platforms

    #region CloudMessaging
    //this is the messaging platform that keeps the messages in the server
    //still on process since i have no perfect algorithm to solve this problem
    public class CloudMessagingRoom {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string RoomName { get; set; }
        public Guid APIKey { get; set; }
        //this verify if user already sync this data to its devices
        public bool isSync { get; set; }
    }
    public class CloudRoomParticipants { 
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoomID { get; set; }
    }
    public class CloudMessagingConversation {
        public Guid ID { get; set; }
        public Guid RoomConversation { get; set; }
        public Guid MessageType { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Text { get; set; }
        public bool isSync { get; set; }
        public Guid SenderID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class CloudMessageReceipent {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid CloudMessagingConversationID { get; set; }
        public Guid RoomID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    //text message/image
    #endregion
    #region MessagingApp
    //this is the messaging platform that keeps the messages in the server
    public class MessagingRoom {
        public Guid ID { get; set; }
        public string Name { get; set; }
        //this will determine which application this room belongs to
        public Guid API { get; set; }
        public DateTime createdAt { get; set; }
    }
    public class MessagingRoomParticipants {
        public Guid ID { get; set; }
        public string UserID { get; set; }
        public Guid RoomID { get; set; }
        public Guid API { get; set; }
    }
    public class MessagingConversation {
        public Guid ID { get; set; }
        public string Text { get; set; }
        public Guid MessageType { get; set; }
        public string SenderID { get; set; }
        public Guid RoomID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    #endregion
    public class MessageType
    {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Name { get; set; }
    }
    #endregion
    #region NotificationManager
    public class NotificationManager {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string Message { get; set; }
        //uid where you store the owner of this notification
        public Guid apiKey { get; set; }
    }
    public class NotificationManagerReceipent {
        public Guid ID { get; set; }
        public Guid NotificationInfo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string ReceiverID { get; set; }
        //this will sort what notification to send to user based on the 
        //app he/she request this item
        public Guid API { get; set; }
    }

    #endregion
    #region UserInformation
    public class UserInformation {
        public Guid ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength]
        public string UserID { get; set; }
        public Guid SignalRID { get; set; }
        public bool isActive { get; set; }
    }
    #endregion
    #region SecurityCodeGenerator
    public class SecurityCode {
        public Guid ID { get; set; }
        public Guid API { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string Code { get; set; }
        public string OwnerID { get; set; }
    }
    #endregion
    #region InventorySystem

    #endregion
    #region ImageLinkStorage
    public class ImageLinkStorage
    {
        public Guid ID { get; set; }
        public Guid API { get; set; }
        //can be userID, QuestionID or etc
        public Guid OwnerID { get; set; }
        public string Source { get; set; }
    }
    #endregion
}