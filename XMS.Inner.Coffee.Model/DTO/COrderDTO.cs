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
    public class COrderDTO : DTOBase
    {
        // Properties
        [DataMember(Name = "address")]
        public CMemberAddressDTO Address { get; set; }
        [DataMember(Name = "cityId")]
        public string CityId { get; set; }
        [DataMember(Name = "couponName")]
        public string CouponName { get; set; }
        [DataMember(Name = "couponUUID")]
        public string CouponUUID { get; set; }
        [DataMember(Name = "createName")]
        public string CreateName { get; set; }
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get; set; }
        [DataMember(Name = "deliveryAddress")]
        public string DeliveryAddress { get; set; }
        [DataMember(Name = "deliveryMaxTime")]
        public DateTime? DeliveryMaxTime { get; set; }
        [DataMember(Name = "deliveryMinTime")]
        public DateTime? DeliveryMinTime { get; set; }
        [DataMember(Name = "deliveryType")]
        public int DeliveryType { get; set; }
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }
        [DataMember(Name = "isOutOfTime")]
        public bool IsOutOfTime { get; set; }
        [DataMember(Name = "managerRemark")]
        public string ManagerRemark { get; set; }
        [DataMember(Name = "memberAddressUUID")]
        public string MemberAddressUUID { get; set; }
        [DataMember(Name = "memberName")]
        public string MemberName { get; set; }
        [DataMember(Name = "memberUUID")]
        public string MemberUUID { get; set; }
        [DataMember(Name = "orderMomey")]
        public decimal OrderMomey { get; set; }
        [DataMember(Name = "orderState")]
        public int OrderState { get; set; }
        [DataMember(Name = "orderTime")]
        public DateTime OrderTime { get; set; }
        [DataMember(Name = "orderUUID")]
        public string OrderUUID { get; set; }
        [DataMember(Name = "payDateTime")]
        public DateTime? PayDateTime { get; set; }
        [DataMember(Name = "payMomey")]
        public decimal? PayMomey { get; set; }
        [DataMember(Name = "payStatus")]
        public int? PayStatus { get; set; }
        [DataMember(Name = "payType")]
        public int? PayType { get; set; }
        [DataMember(Name = "preferential")]
        public decimal? Preferential { get; set; }
        [DataMember(Name = "preferentialType")]
        public int? PreferentialType { get; set; }
        [DataMember(Name = "printDate")]
        public DateTime? PrintDate { get; set; }
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
        [DataMember(Name = "remark")]
        public string Remark { get; set; }
        [DataMember(Name = "resUUID")]
        public string ResUUID { get; set; }
        [DataMember(Name = "serverFee")]
        public decimal ServerFee { get; set; }
        [DataMember(Name = "telephone")]
        public string Telephone { get; set; }
        [DataMember(Name = "updateName")]
        public string UpdateName { get; set; }
        [DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get; set; }
        [DataMember(Name = "version")]
        public int Version { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 配送人员ID
        /// </summary>
        [DataMember(Name = "distributionId")]
        public int? DistributionId { get; set; }

        /// <summary>
        /// 0：自己抢单 1：后台分配
        /// </summary>
        [DataMember(Name = "assignCategory")]
        public int? AssignCategory { get; set; }

        /// <summary>
        /// 0:未发送 1：已发送
        /// </summary>
        [DataMember(Name = "messageStatus")]
        public int? MessageStatus { get; set; }


        #region 虚拟状态
        [DataMember(Name = "listCOrderCommodityRelation")]
        public List<COrderCommodityRelationDTO> ListCOrderCommodityRelation = new List<COrderCommodityRelationDTO>();

        /// <summary>
        /// 配送人员名称
        /// </summary>
        [DataMember(Name = "distributionName")]
        public string DistributionName { get; set; }

        /// <summary>
        /// 配送人员手机号码
        /// </summary>
        [DataMember(Name = "distributionMobile")]
        public string DistributionMobile { get; set; }
        #endregion


    }
}