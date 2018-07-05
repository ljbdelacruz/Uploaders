using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Models.Uploaders;
using Uploaders.Services;
using Uploaders.Services.ImageStorage;

namespace Uploaders.API.ImageStorage
{
    public class ImageLinkStorageController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var api = Guid.Parse(Request.Form["api"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                var source = Request.Form["source"];
                if (ImageLinkStorageService.Insert(id, api, oid, source))
                {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove()
        {
            try
            {
                var id = Guid.Parse(Request.Form["id"]);
                var api = Guid.Parse(Request.Form["api"]);
                var oid = Guid.Parse(Request.Form["oid"]);
                if (ImageLinkStorageService.Remove(id, api, oid))
                {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByOIDAPI(string id, string aid)
        {
            try
            {
                var data = ImageLinkStorageService.GetByOidAPI(Guid.Parse(id), Guid.Parse(aid));
                return Success(ImageLinkStorageVM.MsToVMs(data));
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string aid)
        {
            try
            {
                var data = ImageLinkStorageService.GetByID(Guid.Parse(id), Guid.Parse(aid));
                return Success(ImageLinkStorageVM.MToVM(data));
            }
            catch { return Failed(MessageUtility.ServerError()+" "+id+" "+aid); }
        }
        #endregion
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