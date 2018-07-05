using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uploaders.Context;
using Uploaders.Models.Uploaders;

namespace Uploaders.Services.ImageStorage
{
    public static class ImageLinkStorageService
    {
        public static bool Insert(Guid id, Guid api, Guid oid, string source)
        {
            try
            {
                var data = ImageLinkStorageVM.Set(id, api, oid, source);
                using (var context = new UploadersContext())
                {
                    context.ImageLinkStorageDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
        public static bool Remove(Guid id, Guid api, Guid oid)
        {
            try
            {
                using (var context = new UploadersContext())
                {
                    var query = (from i in context.ImageLinkStorageDB where i.ID == id && i.API == api && i.OwnerID == oid select i).FirstOrDefault();
                    context.ImageLinkStorageDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
        public static List<ImageLinkStorage> GetByOidAPI(Guid oid, Guid api)
        {
            using (var context = new UploadersContext())
            {
                var query = (from i in context.ImageLinkStorageDB where i.OwnerID == oid && i.API == api select i).ToList();
                return query;
            }
        }
        public static ImageLinkStorage GetByID(Guid id, Guid api)
        {
            using (var context = new UploadersContext())
            {
                var query = (from i in context.ImageLinkStorageDB where i.ID == id && i.API == api select i).FirstOrDefault();
                return query;
            }
        }
    }
}