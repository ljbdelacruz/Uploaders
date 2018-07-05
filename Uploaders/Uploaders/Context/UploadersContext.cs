using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Uploaders.Models.Uploaders;

namespace Uploaders.Context
{
    public class UploadersContext:DbContext
    {
        public UploadersContext() : base("name=UploadersContext") {
        }

        public DbSet<CompanyAPIKey> CompanyAPIKeyDB { get; set; }
        #region Messaging Platform
        #region CloudMessaging
        public DbSet<CloudMessagingRoom> CloudMessagingRoomDB { get; set; }
        public DbSet<CloudMessagingConversation> CloudMessagingConversationDB { get; set; }
        public DbSet<CloudMessageReceipent> CloudMessageReceipentDB { get; set; }
        public DbSet<CloudRoomParticipants> CloudRoomParticipantsDB { get; set; }
        #endregion
        #region Messaging App
        public DbSet<MessagingRoom> MessagingRoomDB { get; set; }
        public DbSet<MessagingRoomParticipants> MessagingRoomParticipantsDB { get; set; }
        public DbSet<MessagingConversation> MessagingConversationDB { get; set; }
        #endregion
        public DbSet<MessageType> MessageTypeDB { get; set; }
        #endregion
        #region NotificationManager
        public DbSet<NotificationManager> NotificationManagerDB { get; set; }
        public DbSet<NotificationManagerReceipent> NotificationManagerReceipentDB { get; set; }
        #endregion
        public DbSet<UserInformation> UserInformationDB { get; set; }
        #region SecurityCodeGenerator
        public DbSet<SecurityCode> SecurityCodeDB { get; set; }
        #endregion
        #region ImageLinkStorage
        public DbSet<ImageLinkStorage> ImageLinkStorageDB { get; set; }
        #endregion
    }
}