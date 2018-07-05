using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Models.Uploaders;
using Uploaders.Services;
using Uploaders.Services.CloudMessaging;

namespace Uploaders.API.CloudMessaging
{
    public class CloudRoomParticipantsController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var userID = Guid.Parse(Request.Form["uid"]);
                var roomID = Guid.Parse(Request.Form["rid"]);
                if (CloudRoomParticipantsService.Insert(id, userID, roomID)){
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (CloudRoomParticipantsService.Remove(id)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByRoomID(string id)
        {
            try {
                var data = CloudRoomParticipantsService.GetByRoomID(Guid.Parse(id));
                return Success(CloudRoomParticipantsVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByUserID(string id) {
            try {
                var data = CloudRoomParticipantsService.GetByUserID(Guid.Parse(id));
                return Success(CloudRoomParticipantsVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }


        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}