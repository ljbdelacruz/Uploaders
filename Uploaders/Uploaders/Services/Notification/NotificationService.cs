using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.Notification
{
    public static  class NotificationService
    {
        #region Notification Receipent
        public static bool InsertNR(Guid id, Guid NotificationID, string receiverID, Guid api) {
            try {
                using (var context = new UploadersContext()) {
                    var model = NotificationManagerReceipentVM.set(id, NotificationID, receiverID, api);
                    context.NotificationManagerReceipentDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove_NR(Guid id, Guid api)
        {
            try
            {
                using (var context = new UploadersContext())
                {
                    var query = (from i in context.NotificationManagerReceipentDB where i.ID == id && i.API == api select i).FirstOrDefault();
                    context.NotificationManagerReceipentDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
        public static NotificationManagerReceipent GetByID_NR(Guid id, Guid api) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.NotificationManagerReceipentDB where i.ID == id && i.API == api select i).FirstOrDefault();
                return query;
            }
        }
        public static List<NotificationManagerReceipent> GetByUIDAPI_NR(string id, Guid api) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.NotificationManagerReceipentDB where i.ReceiverID == id && i.API == api select i).ToList();
                return query;
            }
        }
        public static List<NotificationManagerReceipent> GetByNotificationID_NR(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.NotificationManagerReceipentDB where i.NotificationInfo == id select i).ToList();
                return query;
            }
        }
        #endregion
        #region NotificationContent
        public static bool Insert(Guid id, string message, Guid API, string title) {
            try {
                using (var context = new UploadersContext()) {
                    var model = NotificationManagerVM.Set(id, message, API, title);
                    context.NotificationManagerDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove_N(Guid id, Guid api) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.NotificationManagerDB where i.ID == id && i.apiKey == api select i).FirstOrDefault();
                    context.NotificationManagerDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static NotificationManager GetByID_N(Guid id, Guid api) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.NotificationManagerDB where i.ID == id && i.apiKey == api select i).FirstOrDefault();
                return query;
            }
        }
        #endregion

    }
}