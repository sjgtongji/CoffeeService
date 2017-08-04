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
    public class CCouponDTO : DTOBase
    {
    
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "couponUUID")]
        public string CouponUUID { get;set; }
        
        /// <summary>
        /// 1：新用户优惠（未下单） 2：全场优惠 3：满额优惠 4:满足数量优惠
        /// </summary>
        [DataMember(Name = "couponType")]
        public int CouponType { get;set; }
        
        /// <summary>
        /// 1：减额 2：折扣
        /// </summary>
        [DataMember(Name = "preferentialType")]
        public int PreferentialType { get;set; }
        
        /// <summary>
        /// PreferentialType=1：减少额度  PreferentialType=2：折扣
        /// </summary>
        [DataMember(Name = "preferential")]
        public decimal? Preferential { get;set; }

        /// <summary>
        /// 0：初始 1：有效 2：无效
        /// </summary>
        [DataMember(Name = "state")]
        public int State { get;set; }

        /// <summary>
        /// 有效开始时间
        /// </summary>
        [DataMember(Name = "effectiveStartTime")]
        public DateTime? EffectiveStartTime { get;set; }

        /// <summary>
        /// 有效结束时间
        /// </summary>
        [DataMember(Name = "effectiveEndTime")]
        public DateTime? EffectiveEndTime { get;set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get;set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get;set; }

        /// <summary>
        /// 创建人
        /// </summary>
        //[DataMember(Name = "createName")]
        public string CreateName { get;set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        //[DataMember(Name = "createTime")]
        public DateTime CreateTime { get;set; }

        /// <summary>
        /// 修改人
        /// </summary>
        //[DataMember(Name = "updateName")]
        public string UpdateName { get;set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        //[DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get;set; }

        /// <summary>
        /// 版本
        /// </summary>
        //[DataMember(Name = "version")]
        public int Version { get;set; }

        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "resUUID")]
        public string ResUUID { get; set; }

        /// <summary>
        /// CouponType=2时，满足优惠优惠条件的金额
        /// </summary>
        [DataMember(Name = "offerAmount")]
        public decimal OfferAmount { get; set; }

        /// <summary>
        /// 优惠卷名称
        /// </summary>
        [DataMember(Name = "couponName")]
        public string CouponName { get; set; }
    }
}