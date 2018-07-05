using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Hubs;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.MessagingApp
{
    public static class MessagingRoomParticipantService
    {
        public static List<MessagingRoomParticipants> GetByRoomIDExceptUID(Guid rid, string uid) {
            using (var context = new UploadersContext()){
                var query = (from i in context.MessagingRoomParticipantsDB where i.RoomID == rid && i.UserID != uid select i).ToList();
                return query;
            }
        }
        public static List<MessagingRoomParticipants> GetByRoomID(Guid id){
            using (var context = new UploadersContext()) {
                var query = (from i in context.MessagingRoomParticipantsDB where i.RoomID == id select i).ToList();
                return query; 
            }
        }
        public static List<MessagingRoomParticipants> GetByParticipantID(string id, Guid api) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.MessagingRoomParticipantsDB where i.UserID.Equals(id) && i.API==api select i).ToList();
                return query;
            }
        }

        public static bool Insert(Guid id, string uid, Guid roomID, Guid api) {
            try {
                using (var context = new UploadersContext()) {
                    var model = MessagingRoomParticipantsVM.set(id, uid, roomID, api);
                    context.MessagingRoomParticipantsDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.MessagingRoomParticipantsDB where i.ID == id select i).FirstOrDefault();
                    //invoke signalR hub that someone is leaving the room
                    MessagingAppHub.LeaveRoom(query.RoomID.ToString(), query.UserID, query.API.ToString());
                    context.MessagingRoomParticipantsDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
    }
}