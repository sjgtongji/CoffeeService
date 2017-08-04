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
    public class CBannerPO : POBase
    {
    
        /// <summary>
        /// Banner图片地址
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(500)]
        public string ImgURL { get;set; }
        
        /// <summary>
        /// Banner名称
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(100)]
        public string Name { get;set; }
        
        /// <summary>
        /// Banner跳转地址
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(500)]
        public string BannerLink { get;set; }
        
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

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Sort { get; set; }

    }
}