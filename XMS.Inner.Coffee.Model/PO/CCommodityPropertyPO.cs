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
    public class CCommodityPropertyPO : POBase
    {
        // Properties
        [DataMember, DataType(DbType.Decimal)]
        public decimal? AddPrice { get; set; }
        [DataMember, DataType(DbType.String), StringLength(200)]
        public string ChineseName { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string CommodityPropertyUUID { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string CommodityUUID { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string CreateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime CreateTime { get; set; }
        [DataMember, DataType(DbType.String), StringLength(200)]
        public string EnglishName { get; set; }
        [DataMember, Required, DataType(DbType.Boolean)]
        public bool IsDelete { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string PropertyCategoryUUID { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string ResUUID { get; set; }
        [DataMember, Required, DataType(DbType.Int32)]
        public int Sort { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string UpdateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime UpdateTime { get; set; }
        [DataMember, Required, DataType(DbType.Int32)]
        public int Version { get; set; }
    }


}