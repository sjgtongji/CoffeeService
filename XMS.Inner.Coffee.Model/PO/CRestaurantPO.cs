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
    public class CRestaurantPO : POBase
    {
        // Properties
        [DataMember, DataType(DbType.String), StringLength(0x3e8)]
        public string Address { get; set; }
        [DataMember, Required, DataType(DbType.Decimal)]
        public decimal AllowedDistance { get; set; }
        [DataMember, DataType(DbType.String), StringLength(500)]
        public string Alphabet { get; set; }
        [DataMember, DataType(DbType.String), StringLength(20)]
        public string CityId { get; set; }
        [DataMember, DataType(DbType.String), StringLength(0x3e8)]
        public string ContactNumber { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string CreateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime CreateTime { get; set; }
        [DataMember, DataType(DbType.String), StringLength(0x3e8)]
        public string ImgUrl { get; set; }
        [DataMember, Required, DataType(DbType.Boolean)]
        public bool IsDelete { get; set; }
        [DataMember, DataType(DbType.Decimal)]
        public decimal? Latitude { get; set; }
        [DataMember, DataType(DbType.Decimal)]
        public decimal? Longitude { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(200)]
        public string Name { get; set; }
        [DataMember, DataType(DbType.String), StringLength(0x3e8)]
        public string Remark { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string ResUUID { get; set; }
        [DataMember, Required, DataType(DbType.Decimal)]
        public decimal ServerFee { get; set; }
        [DataMember, Required, DataType(DbType.Int32)]
        public int State { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string UpdateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime UpdateTime { get; set; }
        [DataMember, Required, DataType(DbType.Int32)]
        public int Version { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// 登录设备号
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(80)]
        public string DeviceId { get; set; }
    }
}