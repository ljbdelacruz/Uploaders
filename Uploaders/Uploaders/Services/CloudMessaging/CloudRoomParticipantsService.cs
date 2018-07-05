using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.CloudMessaging
{
    public static class CloudRoomParticipantsService
    {
        public static List<CloudRoomParticipants> GetByRoomID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudRoomParticipantsDB where i.RoomID == id select i).ToList();
                return query;
            }
        }

        
        public static List<CloudRoomParticipants> GetByUserID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudRoomParticipantsDB where i.UserID == id select i).ToList();
                return query;
            }
        }
        public static CloudRoomParticipants GetByUIDRoomID(Guid id, Guid rid) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CloudRoomParticipantsDB where i.UserID == id && i.RoomID == rid select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid ID, Guid UserID, Guid RoomID) {
            try {
                using (var context = new UploadersContext()) {
                    var model = CloudRoomParticipantsVM.Set(ID, UserID, RoomID);
                    context.CloudRoomParticipantsDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.CloudRoomParticipantsDB where i.ID == id select i).FirstOrDefault();
                    context.CloudRoomParticipantsDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}