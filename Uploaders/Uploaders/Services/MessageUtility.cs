using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uploaders.Services
{
    public class MessageUtility
    {
        public static string ServerError()
        {
            return "Server error occured please try again later";
        }
        public static string APIKeyError() {
            return "Please use valid api key or request from the administrator of this app";
        }
        public static string DidNotFollowStandardUsingAPI() {
            return "Please follow the standards in using this api to avoid future problems";
        }
    }
}