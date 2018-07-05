using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services
{
    public static class UserInformationServices
    {
        public static UserInformation GetByID(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.UserInformationDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        //signalR ID
        public static UserInformation GetBySID(Guid id) {
            using (var context = new UploadersContext())
            {
                var query = (from i in context.UserInformationDB where i.SignalRID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, string uid, Guid sid, bool isActive) {
            try {
                using (var context = new UploadersContext()) {
                    var model = UserInformationVM.Set(id, uid, sid, isActive);
                    context.UserInformationDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, Guid sid, bool isActive) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.UserInformationDB where i.ID == id select i).FirstOrDefault();
                    query.SignalRID = sid;
                    query.isActive = isActive;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }


    }
}