using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using XMS.Core;
using XMS.Core.Data;
using System.Data;

namespace XMS.Inner.Coffee.Model
{
    [Serializable, DataContract]
    public class SignatureObjectDTO
    {
        [DataMember()]
        public int TimeStamp { get; set; }
        [DataMember()]
        public string NonceStr { get; set; }
        [DataMember()]
        public string Signature { get; set; }
        [DataMember()]
        public string AppId { get; set; }
    }
}
