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
    public class MessagingRoomController : Controller
    {
        #region Request Post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var name = Request.Form["name"];
                var api = Guid.Parse(Request.Form["api"]);
                if (MessagingRoomService.Insert(id, name, api, DateTime.Now)) {
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
                var api = Guid.Parse(Request.Form["api"]);
                if (MessagingRoomService.Remove(id, api)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string api) {
            try {
                var data = MessagingRoomService.GetByID(Guid.Parse(id), Guid.Parse(api));
                return Success(MessagingRoomVM.MToVM(data));
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