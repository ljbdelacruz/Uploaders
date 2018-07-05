using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Models.Uploaders;
using Uploaders.Services;

namespace Uploaders.API
{
    public class CloudMessageController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var roomID = Guid.Parse(Request.Form["rid"]);
                var messageType = Guid.Parse(Request.Form["mtype"]);
                var text = Request.Form["text"];
                var api = Guid.Parse(Request.Form["api"]);
                var cid = Guid.Parse(Request.Form["cid"]);
                var access = CompanyAPIKeyService.GetByAPIKeyCompID(api, cid);
                var senderID = Guid.Parse(Request.Form["sid"]);
                if (access != null)
                {
                    //insert CloudMessage
                    if (CloudMessagingConversationService.Insert(id, roomID, messageType, text, false, senderID)) {
                        return Json(new { success = true, data = id });
                    }
                }
                return Json(new { success = false, message = MessageUtility.APIKeyError() });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Update() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var isSync = Boolean.Parse(Request.Form["isSync"]);
                if (CloudMessagingConversationService.Sync(id, isSync)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (CloudMessagingConversationService.Remove(id)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByRoom(string id) {
            try {
                var conversations = CloudMessagingConversationService.GetByRoomConversation(Guid.Parse(id), false);
                return Success(CloudMessagingConversationVM.MsToVMs(conversations));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id) {
            try {
                var data = CloudMessagingConversationService.GetByID(Guid.Parse(id));
                return Success(CloudMessagingConversationVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }


        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }


    }
}