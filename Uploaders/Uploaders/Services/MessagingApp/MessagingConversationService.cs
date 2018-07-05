using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.MessagingApp
{
    public static class MessagingConversationService
    {
        public static MessagingConversation GetByID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.MessagingConversationDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }

        public static List<MessagingConversation> GetByRoomID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.MessagingConversationDB where i.RoomID == id orderby i.CreatedAt descending select i).ToList();
                return query;
            }
        }
        public static bool Insert(Guid id, string text, Guid messageType, string senderID, Guid roomID, DateTime createdAt) {
            try
            {
                using (var context = new UploadersContext())
                {
                    var model = MessagingConversationVM.set(id, text, messageType, senderID, roomID, createdAt);
                    context.MessagingConversationDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.MessagingConversationDB where i.ID == id select i).FirstOrDefault();
                    context.MessagingConversationDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}