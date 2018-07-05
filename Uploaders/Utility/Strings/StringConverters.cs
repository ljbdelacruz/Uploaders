using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Strings
{
    public static class StringConverters
    {
        public static string GuidToCode(Guid code, int takeNumber) {
            return code.ToString().Take(takeNumber).ToString();
        }
        public static string DateTimeToStrings(DateTime dtime) {
            return dtime.Day + "/" + dtime.Month + "/" + dtime.Year;
        }
    }
}
