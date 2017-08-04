

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
    public class COrderCommodityRelationPO : POBase
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
        /// 组合商品UUID(一个商品，通过此属性分组)
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string CommodityWithPropertyUUID { get;set; }
        
        /// <summary>
        /// 商品数量
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Quantity { get;set; }
        
        /// <summary>
        /// 组合商品单价
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? Price { get;set; }
        
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
        /// 商品UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string CommodityUUID { get; set; }

        /// <summary>
        /// 商品名称（中文）
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(200)]
        public string ChineseName { get; set; }

        /// <summary>
        /// 商品名称（英文）
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(200)]
        public string EnglishName { get; set; }

        /// <summary>
        /// 商品名称（中文）
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(2000)]
        public string ChinesePropertyName { get; set; }

        /// <summary>
        /// 商品名称（英文）
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(2000)]
        public string EnglishPropertyName { get; set; }
    }
}