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
    public class CommodityInfoDTO
    {
        // Properties
        [DataMember(Name = "chineseName")]
        public string ChineseName { get; set; }
        [DataMember(Name = "englishName")]
        public string EnglishName { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "picPath")]
        public string PicPath { get; set; }
        [DataMember(Name = "price")]
        public decimal Price { get; set; }
        [DataMember(Name = "target")]
        public string ShowRemark { get; set; }
        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "skuInfo")]
        public List<PropertyCategoryInfoDTO> SkuInfo = new List<PropertyCategoryInfoDTO>();

    }
}