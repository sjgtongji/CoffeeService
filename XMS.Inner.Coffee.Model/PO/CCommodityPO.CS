

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
    public class CCommodityPO : POBase
    {
    
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string CommodityUUID { get;set; }
        
        /// <summary>
        /// 商品名称（中文）
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(200)]
        public string ChineseName { get;set; }
        
        /// <summary>
        /// 商品名称（英文）
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(200)]
        public string EnglishName { get;set; }
        
        /// <summary>
        /// 商品基础价格
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? Price { get;set; }
        
        /// <summary>
        /// 排序权重（越大越靠前）
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? Sort { get;set; }
        
        /// <summary>
        /// 商品类别UUID
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string CommodityCategoryUUID { get;set; }
        
        /// <summary>
        /// 0:普通 1：组合
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Type { get;set; }
        
        /// <summary>
        /// 0：初始；1：上线；2：下线
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int State { get;set; }
        
        /// <summary>
        /// 显示备注
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string ShowRemark { get;set; }
        
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
        /// UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string ResUUID { get; set; }


        /// <summary>
        /// 类别图片路径
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(200)]
        public string PicPath { get; set; }

        /// <summary>
        /// 是否参加优惠(true:参加，false:不参加)
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Boolean)]
        public bool PreferentialFlag { get; set; }

        /// <summary>
        /// 优惠数量
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? PreferentialQuantity { get; set; }

        /// <summary>
        /// 优惠百分比（百分比）
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? PreferentialProportion { get; set; }
    }
}