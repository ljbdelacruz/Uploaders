using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services
{
    public static class CloudMessagingRoomService
    {
        public static CloudMessagingRoom GetByID(Guid ID) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudMessagingRoomDB where i.ID == ID select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, string RoomName, Guid APIKey, bool isSync) {
            try {
                using (var context = new UploadersContext()) {
                    var model = CloudMessagingRoomVM.Set(id, RoomName, APIKey, isSync);
                    context.CloudMessagingRoomDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool RemoveByID(Guid id) {
            try
            {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.CloudMessagingRoomDB where i.ID == id select i).FirstOrDefault();
                    context.CloudMessagingRoomDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateSyncStatus(Guid id, int mode, bool isSync) {
            try {
                using (var context = new UploadersContext()) {
                    //receiver update
                    var query = (from i in context.CloudMessagingRoomDB where i.ID == id select i).FirstOrDefault();
                    if (mode == 1)
                    {
                        query.isSync = isSync;
                    }
                    else
                    {
                        //sender update
                        query.isSync = isSync;
                    }
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}