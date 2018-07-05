using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.MessagingApp
{
    public static class MessagingRoomService
    {
        public static MessagingRoom GetByID(Guid id, Guid API) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.MessagingRoomDB where i.ID == id && i.API == API select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, string name, Guid api, DateTime createdAt) {
            try {
                using (var context = new UploadersContext()) {
                    var model = MessagingRoomVM.set(id, name, api, createdAt);
                    context.MessagingRoomDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid api) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.MessagingRoomDB where i.ID == id && i.API == api select i).FirstOrDefault();
                    context.MessagingRoomDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
    }
}