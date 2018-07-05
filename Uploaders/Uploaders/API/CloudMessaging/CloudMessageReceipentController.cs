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
    public class CloudMessageReceipentController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var uid = Guid.Parse(Request.Form["uid"]);
                var cmcID = Guid.Parse(Request.Form["cmcID"]);
                var date = DateTime.Now;
                var roomID = Guid.Parse(Request.Form["rid"]);
                if (CloudMessagingReceipentService.Insert(id, uid, cmcID, date, roomID)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError());  }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (CloudMessagingReceipentService.Remove(id)){
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByUserID(string id) {
            try {
                var data = CloudMessagingReceipentService.GetByUserID(Guid.Parse(id));
                return Success(CloudMessageReceipentVM.MsToVMs(data));
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