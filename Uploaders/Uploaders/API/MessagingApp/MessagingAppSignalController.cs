using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Services;

namespace Uploaders.API.MessagingApp
{
    public class MessagingAppSignalController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> SendMessage() {
            try {


                return Success("");
            } catch {
                return Failed(MessageUtility.ServerError());
            }
        }

        private JsonResult Success(dynamic data) {
            return Json(new { succes = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}