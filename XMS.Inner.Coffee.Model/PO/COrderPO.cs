

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
    [DataContract]
    [Serializable]
    public class COrderPO : POBase
    {
    
        /// <summary>
        /// 订单UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(40)]
        public string OrderUUID { get;set; }
        
        /// <summary>
        /// 餐厅UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(40)]
        public string ResUUID { get;set; }
        
        /// <summary>
        /// 会员UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string MemberUUID { get;set; }
        
        /// <summary>
        /// 订餐人
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string MemberName { get;set; }
        
        /// <summary>
        /// 用户号码
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string Telephone { get;set; }
        
        /// <summary>
        /// 订单创建时间
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.DateTime)]
        public DateTime OrderTime { get;set; }

        /// <summary>
        /// 订单状态(0:未确认；1：已确认；2：取消；3：已配送；4：已完成)
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int OrderState { get;set; }
        
        /// <summary>
        /// 支付状态（0:未支付；1：线下支付；2：在线支付）
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? PayStatus { get;set; }
        
        /// <summary>
        /// 支付类别（0：支付宝；1：微信；2：E宝）
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? PayType { get;set; }
        
        /// <summary>
        /// 0:配送 1：门店自取
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int DeliveryType { get;set; }
        
        /// <summary>
        /// 支付时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? PayDateTime { get;set; }
        
        /// <summary>
        /// 支付金额
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? PayMomey { get;set; }
        
        /// <summary>
        /// 打印时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? PrintDate { get;set; }
        
        /// <summary>
        /// 送达最小时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? DeliveryMinTime { get;set; }
        
        /// <summary>
        /// 送达最大时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? DeliveryMaxTime { get;set; }
        
        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(20)]
        public string CityId { get;set; }
        
        /// <summary>
        /// 送货地址
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(1000)]
        public string DeliveryAddress { get;set; }
        
        /// <summary>
        /// 会员地址UUID
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string MemberAddressUUID { get;set; }
        
        /// <summary>
        /// 优惠券UUID
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string CouponUUID { get;set; }
        
        /// <summary>
        /// 1：减额 2：折扣
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? PreferentialType { get;set; }
        
        /// <summary>
        /// PreferentialType=1：减少额度  PreferentialType=2：折扣
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? Preferential { get;set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(1000)]
        public string Remark { get;set; }
        
        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Boolean)]
        public bool IsDelete { get;set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string CreateName { get;set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.DateTime)]
        public DateTime CreateTime { get;set; }
        
        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string UpdateName { get;set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.DateTime)]
        public DateTime UpdateTime { get;set; }
        
        /// <summary>
        /// 版本
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Version { get;set; }

        /// <summary>
        /// 服务费（配送费）
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Decimal)]
        public decimal ServerFee { get; set; }

        /// <summary>
        /// 订单金额 未优惠前
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Decimal)]
        public decimal OrderMomey { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Quantity { get; set; }

        /// <summary>
        /// 下单时，是否是超时订单
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Boolean)]
        public bool IsOutOfTime { get; set; }

        /// <summary>
        /// 优惠卷名称
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(500)]
        public string CouponName { get; set; }

        /// <summary>
        /// 管理员备注
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(2000)]
        public string ManagerRemark { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(500)]
        public string OrderId { get; set; }

        /// <summary>
        /// 配送人员ID
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? DistributionId { get; set; }

        /// <summary>
        /// 0：自己抢单 1：后台分配
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? AssignCategory { get; set; }

        /// <summary>
        /// 0:未发送 1：已发送
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? MessageStatus { get; set; }
    }
}