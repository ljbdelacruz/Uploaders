using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services
{
    public static class CompanyAPIKeyService
    {
        public static CompanyAPIKey GetByAPIKeyCompID(Guid id, Guid cid) {
            using (var context = new UploadersContext()) {
                var query = (from i in context.CompanyAPIKeyDB where i.APIKey == id && i.CompanyID == cid select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, Guid cid, Guid apiKEY, DateTime dStart, DateTime dEnd) {
            try {
                using (var context = new UploadersContext()) {
                    var model = CompanyAPIKeyVM.Set(id, cid, apiKEY, dStart, dEnd);
                    context.CompanyAPIKeyDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}