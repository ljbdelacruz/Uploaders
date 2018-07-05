using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Hubs;
using Uploaders.Services;

namespace Uploaders.API
{
    public class SignalTunnelController : Controller
    {
        //put all the signal R functionalities on this app
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Test() {
            try {
                NotificationManagerHub.Test();
                return Success("");
            } catch { return Failed(MessageUtility.ServerError()); }
        }


        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message });
        }

    }
}