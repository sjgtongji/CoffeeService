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
    public class GetAvailableCouponDTO
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        [DataMember(Name = "resId")]
        public int ResId { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        [DataMember(Name = "deliveryType")]
        public int DeliveryType { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        [DataMember(Name = "commodity")]
        public List<Commodity> Commodity = new List<Commodity>();

        /// <summary>
        /// 送达最小时间
        /// </summary>
        [DataMember(Name = "deliveryMinTime")]
        public DateTime? DeliveryMinTime { get; set; }

        /// <summary>
        /// 送达最大时间
        /// </summary>
        [DataMember(Name = "deliveryMaxTime")]
        public DateTime? DeliveryMaxTime { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Commodity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMember(Name = "commodityId")]
        public int CommodityId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
        /// <summary>
        /// 商品属性
        /// </summary>
        [DataMember(Name = "skuList")]
        public List<int> SkuList { get; set; }
    }
}
