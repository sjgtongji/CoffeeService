

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
    public class CDistributionCoordinatePO : POBase
    {
    
        /// <summary>
        /// 配送人员ID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int DistributionId { get;set; }
        
        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? Longitude { get;set; }
        
        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? Latitude { get;set; }
        
        /// <summary>
        /// 处理状态（0：未处理，1：已处理）
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int State { get;set; }
        
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