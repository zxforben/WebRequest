using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace XXX
{
    [DataContract]
    public class Result
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string state { get; set; }

        [DataMember(Order = 1, IsRequired = true)]
        public string data { get; set; }

    }
}
