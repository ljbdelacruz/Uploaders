using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Services;

namespace Uploaders.API
{
    public class TesterController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Test1() {
            try {
                var test = Request.Form["test"];
                var test2 = Request.Form["test2"];
                return Success("Test1: "+test + " Test2: " + test2);

            } catch { return Failed(MessageUtility.ServerError()); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> testimg(string obj) {
            try {   
                var fname = Guid.NewGuid();
                var src = Request.Form["src"];
                //var path = Path.Combine(Server.MapPath("~/UPLOADS/" + obj), fname.ToString() + ".png");
                //var byteData = Convert.FromBase64String(src);
                //System.IO.File.WriteAllBytes(path, src);
                return Success("Base64 Image: "+src);
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        #region util
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}