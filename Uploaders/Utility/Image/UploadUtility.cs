using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utility.Image
{
    public static class UploadUtility
    {

        //source is uri base64 data image and filePath is where the image will be stored
        public static bool UriToServer(string source, string filePath)
        {
            var byteData = Convert.FromBase64String(source);
            System.IO.File.WriteAllBytes(filePath, byteData);
            return true;
        }

        public static bool IsExist(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            return false;
        }



    }
}
