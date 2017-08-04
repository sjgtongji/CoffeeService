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
    public class AddOrderDTO
    {
        /// <summary>
        /// 餐厅UUID
        /// </summary>
        [DataMember(Name = "resId")]
        public int ResId { get; set; }

        /// <summary>
        /// 会员UUID
        /// </summary>
        [DataMember(Name = "memberId")]
        public int MemberId { get; set; }

        /// <summary>
        /// 0:配送 1：门店自取
        /// </summary>
        [DataMember(Name = "deliveryType")]
        public int DeliveryType { get; set; }

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

        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMember(Name = "cityId")]
        public string CityId { get; set; }

        /// <summary>
        /// 会员地址UUID
        /// </summary>
        [DataMember(Name = "memberAddressId")]
        public int MemberAddressId { get; set; }

        /// <summary>
        /// 优惠券UUID
        /// </summary>
        [DataMember(Name = "couponId")]
        public int CouponId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 服务费（配送费）
        /// </summary>
        [DataMember(Name = "serverFee")]
        public decimal ServerFee { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMember(Name = "commodityId")]
        public int CommodityId { get; set; }

        /// <summary>
        /// 商品SKU
        /// </summary>
        [DataMember(Name = "skuList")]
        public List<int> SkuList = new List<int>();

        /// <summary>
        /// 商品数量
        /// </summary>
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 下单时，是否是超时订单
        /// </summary>
        [DataMember(Name = "isOutOfTime")]
        public bool IsOutOfTime { get; set; }
    }
}
