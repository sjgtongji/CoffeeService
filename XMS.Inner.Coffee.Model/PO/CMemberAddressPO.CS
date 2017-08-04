

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
    public class CMemberAddressPO : POBase
    {
    
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string MemberAddressUUID { get;set; }
        
        /// <summary>
        /// 会员UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string MemberUUID { get;set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string Name { get;set; }
        
        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string Telephone { get;set; }
        
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
        public string Address { get;set; }
        
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
        /// 门牌号
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(500)]
        public string HouseNumber { get;set; }
        
        /// <summary>
        /// 标签：例如公司，家里等  最多四个字
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(10)]
        public string Label { get;set; }
        
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
        /// 性别（0：女  1：男）
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string Sex { get; set; }

    }
}