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
    public class AddOrderResultDTO
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        [DataMember(Name = "succ")]
        public bool Succ { get; set; }

        /// <summary>
        /// 订单信息
        /// </summary>
        [DataMember(Name = "order")]
        public COrderDTO Order { get; set; }

        /// <summary>
        /// 支付信息
        /// </summary>
        [DataMember(Name = "payInfo")]
        public PayInfoDTO PayInfo { get; set; }
    }

    [DataContract]
    [Serializable]
    public class PayInfoDTO
    {
        /// <summary>
        /// 支付链接
        /// </summary>
        [DataMember(Name = "payUrl")]
        public Dictionary<string, string> PayUrl { get; set; }
    }
}
