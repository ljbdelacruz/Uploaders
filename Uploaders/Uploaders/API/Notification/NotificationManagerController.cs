using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Hubs;
using Uploaders.Models.Uploaders;
using Uploaders.Services;
using Uploaders.Services.Notification;

namespace Uploaders.API
{
    public class NotificationManagerController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var apiID = Guid.Parse(Request.Form["api"]);
                //companyID
                var cid = Guid.Parse(Request.Form["cid"]);
                var title = Request.Form["title"];
                var message = Request.Form["message"];
                //insert Notification
                if (NotificationService.Insert(id, message, apiID, title))
                {
                    return Success(id.ToString());
                }
                else {
                    return Failed(MessageUtility.APIKeyError());
                }
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> InsertReceipent() {
            try {
                var id = Guid.NewGuid();
                var notifInfo = Guid.Parse(Request.Form["nid"]);
                var rid = Request.Form["rid"];
                var api = Guid.Parse(Request.Form["api"]);
                if (NotificationService.InsertNR(id, notifInfo, rid, api)) {
                    var data = NotificationService.GetByID_N(notifInfo, api);
                    NotificationManagerHub.SendNotification(id.ToString(), api.ToString(), rid, data.Title, data.Message);
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NotificationReceived() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var api = Guid.Parse(Request.Form["api"]);
                var data = NotificationService.GetByID_NR(id, api);
                if (NotificationService.Remove_NR(id, api)) {
                    //check if notification still have notification receipent else
                    //delete from database
                    var list = NotificationService.GetByNotificationID_NR(data.ID);
                    if (list.Count <= 0) {
                        NotificationService.Remove_N(data.NotificationInfo, api);
                    }
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> CheckOwnerNotification() {
            try {
                var api = Guid.Parse(Request.Form["api"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                var uid = Request.Form["uid"];
                //check user receipent
                var receipents = NotificationService.GetByUIDAPI_NR(uid, api);
                if (receipents.Count > 0) {
                    foreach (var r in receipents) {
                        var data = NotificationService.GetByID_N(r.NotificationInfo, r.API);
                        //receipent Notification ID so it can be removed
                        NotificationManagerHub.SendNotification(r.ID.ToString(), data.apiKey.ToString(), r.ReceiverID, data.Title, data.Message);
                    }
                }
                return Success("");
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region util
        private static bool CheckAPI(Guid api, Guid cid) {
            var access = CompanyAPIKeyService.GetByAPIKeyCompID(api, cid);
            if (access != null)
            {
                return true;
            }
            return false;
        }
        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}