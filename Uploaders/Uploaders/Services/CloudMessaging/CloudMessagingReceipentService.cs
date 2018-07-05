using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.CloudMessaging
{
    public static class CloudMessagingReceipentService
    {

        public static List<CloudMessageReceipent> GetByMessageID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudMessageReceipentDB where i.CloudMessagingConversationID == id select i).ToList();
                return query;
            }
        }
        public static List<CloudMessageReceipent> GetByUserID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudMessageReceipentDB where i.UserID == id select i).ToList();
                return query;
            }
        }
        public static bool Insert(Guid id, Guid uid, Guid cmcID, DateTime createdAt, Guid roomID) {
            try {
                using (var context = new UploadersContext()) {
                    var model = CloudMessageReceipentVM.Set(id, uid, cmcID, createdAt, roomID);
                    context.CloudMessageReceipentDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.CloudMessageReceipentDB where i.ID == id select i).FirstOrDefault();
                    context.CloudMessageReceipentDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }


    }
}