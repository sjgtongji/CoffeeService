

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
    public class CCashCouponPO : POBase
    {
        /// <summary>
        /// 未知：0， 实物型（现金券优惠券等各种可退）：1， 虚拟型（优惠折扣促销各种不可退）：2
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int AssetProperty { get;set; }
        
        /// <summary>
        /// 资产名称
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(500)]
        public string AssetName { get;set; }
        
        /// <summary>
        /// 资产有效期开始时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? ValidStartTime { get;set; }
        
        /// <summary>
        /// 资产有效期结束时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? ValidEndTime { get;set; }
        
        /// <summary>
        /// 资产状态 0:创建 1：生效 2：人为失效 3：过期失效
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int AssetStatus { get;set; }
        
        /// <summary>
        /// 多少积分可以抵用
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Decimal)]
        public decimal SaleAmount { get;set; }
        
        /// <summary>
        /// 开始销售时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? SaleStartTime { get;set; }
        
        /// <summary>
        /// 结束销售时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? SaleEndTime { get;set; }
        
        /// <summary>
        /// 资产描述
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(500)]
        public string Description { get;set; }
        
        /// <summary>
        /// 现金券面值
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Decimal)]
        public decimal ValAmount { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Boolean)]
        public bool IsDelete { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string CreateName { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.DateTime)]
        public DateTime CreateTime { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string UpdateName { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.DateTime)]
        public DateTime UpdateTime { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Version { get;set; }
        
    }
}