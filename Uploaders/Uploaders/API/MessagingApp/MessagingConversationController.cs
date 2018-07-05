using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Hubs;
using Uploaders.Models.Uploaders;
using Uploaders.Services;
using Uploaders.Services.MessagingApp;
using Uploaders.Services.Notification;

namespace Uploaders.API.MessagingApp
{
    public class MessagingConversationController : Controller
    {
        #region Request Post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var text = Request.Form["text"];
                var mtype = Guid.Parse(Request.Form["mtype"]);
                var sid = Request.Form["sid"];
                var rid = Guid.Parse(Request.Form["rid"]);
                var api = Guid.Parse(Request.Form["api"]);
                if (MessagingConversationService.Insert(id, text, mtype, sid, rid, DateTime.Now)) {
                    //add notification to users that this user sent a message
                    //do not include the uid that sent the message
                    var receipents = MessagingRoomParticipantService.GetByRoomIDExceptUID(rid, sid);
                    var nid = Guid.NewGuid();
                    NotificationService.Insert(nid, text, api, "New Message");
                    foreach (var r in receipents){
                        var nrID = Guid.NewGuid();
                        NotificationService.InsertNR(nrID, nid, r.UserID, api);
                        //invoke signal r that there is a new message notification for the users
                        //nrID=id for notificationReceipent so that if its confirmed received this id will be passed back to the 
                        //server to remove the notificationreceipent

                        var textToSend = this.IdentifyMessageNotification(mtype.ToString(), text);
                        NotificationManagerHub.SendNotification(nrID.ToString(), api.ToString(), r.UserID, "New Message", textToSend);
                        MessagingAppHub.SendNotification(id.ToString(), api.ToString(), rid.ToString());
                    }
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        private string IdentifyMessageNotification(string messageType, string text) {
            switch (messageType) {
                case "46a4d670-cda1-483c-a35a-1b7ac5111a5a":
                    return text;
                case "f7e61265-0ba9-4513-9993-870a57e2dbf2":
                    return "Sent an Image";
                default:
                    return "";
            }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (MessagingConversationService.Remove(id)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region Get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id) {
            try {
                var data = MessagingConversationService.GetByID(Guid.Parse(id));
                return Success(MessagingConversationVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> GetByRoomID(){
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var data = MessagingConversationService.GetByRoomID(id);
                return Success(MessagingConversationVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region util
        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}