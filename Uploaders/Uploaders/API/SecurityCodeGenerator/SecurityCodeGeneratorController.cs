using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Models.Uploaders;
using Uploaders.Services;
using Uploaders.Services.SecurityCodeGenerator;
using Utility.Strings;

namespace Uploaders.API.SecurityCodeGenerator
{
    public class SecurityCodeGeneratorController : Controller
    {
        #region Request Post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var api = Guid.Parse(Request.Form["api"]);
                var code = StringConverters.GuidToCode(Guid.NewGuid(), 4);
                var ownerID = Request.Form["oid"];
                if (SecurityCodeGeneratorService.Insert(id, api, code, ownerID)) {
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
                if (SecurityCodeGeneratorService.Remove(id, api)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByOwnerID(string id, string api) {
            try {
                var data = SecurityCodeGeneratorService.GetByOwnerID(id, Guid.Parse(api));
                return Success(SecurityCodeVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GenerateCode() {
            var code = SecurityCodeGeneratorService.GenerateCode();
            return Success(code);
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GenerateUID(){
            try{
                return Success(Guid.NewGuid().ToString());
            }catch { return Failed(MessageUtility.ServerError()); }
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