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
    public class COrderCommodityRelationDTO : DTOBase
    {
        // Properties
        [DataMember(Name = "chineseName")]
        public string ChineseName { get; set; }
        [DataMember(Name = "chinesePropertyName")]
        public string ChinesePropertyName { get; set; }
        [DataMember(Name = "commodityUUID")]
        public string CommodityUUID { get; set; }
        [DataMember(Name = "commodityWithPropertyUUID")]
        public string CommodityWithPropertyUUID { get; set; }
        [DataMember(Name = "createName")]
        public string CreateName { get; set; }
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get; set; }
        [DataMember(Name = "englishName")]
        public string EnglishName { get; set; }
        [DataMember(Name = "englishPropertyName")]
        public string EnglishPropertyName { get; set; }
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }
        [DataMember(Name = "orderUUID")]
        public string OrderUUID { get; set; }
        [DataMember(Name = "price")]
        public decimal? Price { get; set; }
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
        [DataMember(Name = "updateName")]
        public string UpdateName { get; set; }
        [DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get; set; }
        public int Version { get; set; }
    }
}