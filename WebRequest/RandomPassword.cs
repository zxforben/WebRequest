using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XXX
{
    class RandomPassword
    {
        public static string GetRandomPWD()
        {
           return Guid.NewGuid().ToString().Substring(0,8);
        }
    }
}
