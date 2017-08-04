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
    public class PropertyCategoryInfoDTO
    {
        // Properties
        [DataMember(Name = "chineseName")]
        public string ChineseName { get; set; }
        [DataMember(Name = "enableMultiple")]
        public bool EnableMultiple { get; set; }
        [DataMember(Name = "englishName")]
        public string EnglishName { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "sort")]
        public int Sort { get; set; }
        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "list")]
        public List<CCommodityPropertyInfoDTO> List = new List<CCommodityPropertyInfoDTO>();
    }



}
