using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services
{
    public static class CloudMessagingConversationService
    {

        public static List<CloudMessagingConversation> GetByRoomConversation(Guid id, bool isSync) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudMessagingConversationDB where i.RoomConversation == id && i.isSync==isSync select i).ToList();
                return query;
            }
        }
        public static CloudMessagingConversation GetByID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudMessagingConversationDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, Guid rc, Guid mt, string Text, bool isSync, Guid senderID) {
            try {
                using (var context = new UploadersContext()) {
                    var model = CloudMessagingConversationVM.Set(id, rc, mt, Text, isSync, senderID);
                    context.CloudMessagingConversationDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.CloudMessagingConversationDB where i.ID == id select i).FirstOrDefault();
                    context.CloudMessagingConversationDB.Remove(query);
                    return true;
                }
            } catch { return false; }
        }
        //update the message into synced
        public static bool Sync(Guid id, bool isSync) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.CloudMessagingRoomDB where i.ID == id select i).FirstOrDefault();
                    query.isSync = isSync;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}