using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Model
{
    [DataContract]
    [Serializable]
    public class PayNotifyResult
    {
        [DataMember]
        public bool HasError { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string PayOrderId { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
