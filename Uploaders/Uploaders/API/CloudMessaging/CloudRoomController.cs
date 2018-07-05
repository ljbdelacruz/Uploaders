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
    public class CloudRoomController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var api = Guid.Parse(Request.Form["api"]);
                var compID = Guid.Parse(Request.Form["cid"]);
                var roomName = Request.Form["name"];
                //first check if api is valid 
                var access=CompanyAPIKeyService.GetByAPIKeyCompID(api, compID);
                if (access != null) {
                    //if api is valid insert room
                    if (CloudMessagingRoomService.Insert(id, roomName, api, false)) {
                        //returns the id of the room
                        return Json(new { success = true, data = id });
                    }
                    return Json(new { success = false, message = MessageUtility.DidNotFollowStandardUsingAPI() });
                }
                return Json(new { success = false, message = MessageUtility.APIKeyError() });
            } catch { return Json(new { success = false, message=MessageUtility.ServerError()}); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id) {
            try {
                var data = CloudMessagingRoomService.GetByID(Guid.Parse(id));
                return Success(CloudMessagingRoomVM.MToVM(data));
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