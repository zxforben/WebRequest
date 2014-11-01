using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XXX
{
    class Common
    {
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
