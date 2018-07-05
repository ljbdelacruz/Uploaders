using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Models.Uploaders;
using Uploaders.Services;
using Uploaders.Services.MessagingApp;

namespace Uploaders.API.MessagingApp
{
    public class MessagingRoomParticipantController : Controller
    {
        #region Request Post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var uid = Request.Form["uid"];
                var roomID = Guid.Parse(Request.Form["rid"]);
                var api = Guid.Parse(Request.Form["api"]);
                if (MessagingRoomParticipantService.Insert(id, uid, roomID, api))
                {
                    return Success(id.ToString());
                }
                else {
                    return Failed(MessageUtility.ServerError());
                }
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (MessagingRoomParticipantService.Remove(id)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByRoomID(string id) {
            try {
                var data = MessagingRoomParticipantService.GetByRoomID(Guid.Parse(id));
                return Success(MessagingRoomParticipantsVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByParticipantID(string id, string api) {
            try {
                var data = MessagingRoomParticipantService.GetByParticipantID(id, Guid.Parse(api));
                return Success(MessagingRoomParticipantsVM.MsToVMs(data));
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