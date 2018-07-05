using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.SecurityCodeGenerator
{
    public static class SecurityCodeGeneratorService
    {
        public static SecurityCode GetBy(Guid id) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.SecurityCodeDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static SecurityCode GetByOwnerID(string id, Guid api) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.SecurityCodeDB where i.OwnerID.Equals(id) && i.API==api select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, Guid api, string code, string oid) {
            try {
                using (var context = new UploadersContext()) {
                    var model = SecurityCodeVM.Set(id, api, code, oid);
                    context.SecurityCodeDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid api) {
            try {
                using (var context = new UploadersContext()) {
                    var query = (from i in context.SecurityCodeDB where i.ID == id && i.API==api select i).FirstOrDefault();
                    context.SecurityCodeDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        #region personal func
        public static string GenerateCode() {
            var code = Guid.NewGuid().ToString();
            code=Regex.Replace(code, "-", "");
            return code;
        }

        #endregion


    }
}