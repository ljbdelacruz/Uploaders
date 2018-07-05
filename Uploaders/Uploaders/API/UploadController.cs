using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Uploaders.Services;
using Utility.Image;

namespace Uploaders.API
{
    public class UploadController : Controller
    {
        //upload images
        //older generation please use the current one
        #region post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UploadBase64() {
            try {
                var company = Request.Form["company"];
                var source = Request.Form["image"];
                var fname = Guid.NewGuid().ToString();
                //if (!Directory.Exists(Path.Combine(Server.MapPath("~/UPLOADS/" + company))))
                //{
                //    Directory.CreateDirectory(Path.Combine(Server.MapPath("~/UPLOADS/" + company)));
                //}
                var path = Path.Combine(Server.MapPath("~/UPLOADS/" + company), fname.ToString() + ".png");
                //var path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/UPLOADS/" + company),fname.ToString() + ".png");

                var byteData = Convert.FromBase64String(source);
                System.IO.File.WriteAllBytes(path, byteData);
                //UploadUtility.UriToServer(source, path);
                return Success("/UPLOADS/" + company + "/" + fname.ToString() + ".png");

            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> u64image(string  company, string source) {
            try {
                var fname = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/UPLOADS/" + company), fname.ToString() + ".png");
                byte[] bytes = Convert.FromBase64String(source);
                System.IO.File.WriteAllBytes(path, bytes);
                return Success("/UPLOADS/" + company + "/" + fname.ToString() + ".png");
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Image() {
            try {
                var company = Request.Form["path"];
                var fileName = "file0";
                foreach (string file in Request.Files) {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        //before uploading image make sure it does not exist in path
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var path = Path.Combine(Server.MapPath("~/UPLOADS/" + company), fileName + ".png");
                        var index = 0;
                        while (true) {
                            if (!UploadUtility.IsExist(path))
                            {
                                break;
                            }
                            else {
                                index++;
                                fileName = "file" + index;
                                path = Path.Combine(Server.MapPath("~/UPLOADS/" + company), fileName + ".png");
                            }
                        }

                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
                return Json(new { success = true, data="UPLOADS/"+company+"/"+fileName+".png" });
            } catch { return Json(new { success = false, message=MessageUtility.ServerError() }); }
        }
        //this method can create path and automatic generate filename using hash value
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> UploadImage1() {
            try {
                var path = Request.Form["path"];
                var fileName = Guid.NewGuid().ToString();
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        //before uploading image make sure it does not exist in path
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        if(!Directory.Exists(Path.Combine(Server.MapPath("~/UPLOADS/" + path)))){
                            Directory.CreateDirectory(Path.Combine(Server.MapPath("~/UPLOADS/" + path)));
                        }
                        var uploadPath = Path.Combine(Server.MapPath("~/UPLOADS/" + path), fileName + ".png");
                        using (var fileStream = System.IO.File.Create(uploadPath))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
                return Json(new { success = true, data = "UPLOADS/"+path+"/"+fileName+".png"});
            }
            catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }
        //upload video
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Video() {
            try {
                return Json(new { success = true });
            } catch { return Json(new { success = false }); }
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