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
    public class ProcessPayNotifyRequestDto
    {
        [DataMember]
        public int PayMode { get; set; }

        /// <summary>
        /// 目前只有支付宝客户端回调需要赋值  （???微信、招行支付）
        /// </summary>
        [DataMember]
        public string PayOrderId { get; set; }

        [DataMember]
        public string SaleChannelApplicationId { get; set; }
        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        public Dictionary<string, string> Parameters { get; set; }
    }
}
