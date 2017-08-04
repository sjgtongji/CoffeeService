using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Model
{
    [DataContract]
    [Serializable]
    public class CCommodityCategoryDTO : DTOBase
    {
        // Properties
        [DataMember(Name = "chineseName")]
        public string ChineseName { get; set; }
        [DataMember(Name = "commodityCategoryUUID")]
        public string CommodityCategoryUUID { get; set; }
        [DataMember(Name = "createName")]
        public string CreateName { get; set; }
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get; set; }
        [DataMember(Name = "englishName")]
        public string EnglishName { get; set; }
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }
        [DataMember(Name = "picPath")]
        public string PicPath { get; set; }
        [DataMember(Name = "remark")]
        public string Remark { get; set; }
        [DataMember(Name = "resUUID")]
        public string ResUUID { get; set; }
        [DataMember(Name = "sort")]
        public int? Sort { get; set; }
        [DataMember(Name = "updateName")]
        public string UpdateName { get; set; }
        [DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get; set; }
        [DataMember(Name = "version")]
        public int Version { get; set; }
    }
}