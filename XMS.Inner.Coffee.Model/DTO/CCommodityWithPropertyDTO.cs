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
    public class CCommodityWithPropertyDTO : DTOBase
    {
        // Properties
        [DataMember(Name = "commodityPropertyUUID")]
        public string CommodityPropertyUUID { get; set; }
        [DataMember(Name = "commodityUUID")]
        public string CommodityUUID { get; set; }
        [DataMember(Name = "commodityWithPropertyUUID")]
        public string CommodityWithPropertyUUID { get; set; }
        [DataMember(Name = "createName")]
        public string CreateName { get; set; }
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get; set; }
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }
        [DataMember(Name = "propertyCategoryUUID")]
        public string PropertyCategoryUUID { get; set; }
        [DataMember(Name = "remark")]
        public string Remark { get; set; }
        [DataMember(Name = "resUUID")]
        public string ResUUID { get; set; }
        [DataMember(Name = "updateName")]
        public string UpdateName { get; set; }
        [DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get; set; }
        [DataMember(Name = "version")]
        public int Version { get; set; }
    }
}