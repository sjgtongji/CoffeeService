﻿using System;
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
    public class CCashCouponDetailDTO : DTOBase
    {
    
        ///// <summary>
        ///// 自增ID
        ///// </summary>
        //[DataMember]
        //public int Id { get;set; }
        
        /// <summary>
        /// 未知：0， 实物型（现金券优惠券等各种可退）：1， 虚拟型（优惠折扣促销各种不可退）：2
        /// </summary>
        [DataMember]
        public int AssetProperty { get;set; }
        
        /// <summary>
        /// 资产名称
        /// </summary>
        [DataMember]
        public string AssetName { get;set; }
        
        /// <summary>
        /// 资产有效期开始时间
        /// </summary>
        [DataMember]
        public DateTime? ValidStartTime { get;set; }
        
        /// <summary>
        /// 资产有效期结束时间
        /// </summary>
        [DataMember]
        public DateTime? ValidEndTime { get;set; }
        
        /// <summary>
        /// 多少积分可以抵用
        /// </summary>
        [DataMember]
        public decimal SaleAmount { get;set; }
        
        /// <summary>
        /// 开始销售时间
        /// </summary>
        [DataMember]
        public DateTime? SaleStartTime { get;set; }
        
        /// <summary>
        /// 结束销售时间
        /// </summary>
        [DataMember]
        public DateTime? SaleEndTime { get;set; }
        
        /// <summary>
        /// 资产描述
        /// </summary>
        [DataMember]
        public string Description { get;set; }
        
        /// <summary>
        /// 现金券面值
        /// </summary>
        [DataMember]
        public decimal ValAmount { get;set; }
        
        /// <summary>
        /// 微信用户ID
        /// </summary>
        [DataMember]
        public int MemberWeiXinId { get;set; }
        
        /// <summary>
        /// 使用状态（0：未使用，1：已使用）
        /// </summary>
        [DataMember]
        public int UseState { get;set; }
        
        /// <summary>
        /// 使用时间
        /// </summary>
        [DataMember]
        public DateTime? UseTime { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsDelete { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CreateName { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string UpdateName { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Version { get;set; }

        /// <summary>
        /// 资产状态 0:创建 1：生效 2：人为失效 3：过期失效[Add][Update][QueryList][Need]
        /// </summary>
        [DataMember]
        public int AssetStatus { get; set; }

    }
}