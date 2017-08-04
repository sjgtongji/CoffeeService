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
    public class CCommodityWithPropertyPO : POBase
    {
        // Properties
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string CommodityPropertyUUID { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string CommodityUUID { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string CommodityWithPropertyUUID { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string CreateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime CreateTime { get; set; }
        [DataMember, Required, DataType(DbType.Boolean)]
        public bool IsDelete { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string PropertyCategoryUUID { get; set; }
        [DataMember, DataType(DbType.String), StringLength(0x3e8)]
        public string Remark { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string ResUUID { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string UpdateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime UpdateTime { get; set; }
        [DataMember, Required, DataType(DbType.Int32)]
        public int Version { get; set; }
    }
}