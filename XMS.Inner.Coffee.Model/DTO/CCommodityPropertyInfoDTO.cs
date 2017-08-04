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
    public class CCommodityPropertyInfoDTO
    {
        // Properties
        [DataMember(Name = "addPrice")]
        public decimal AddPrice { get; set; }
        [DataMember(Name = "chineseName")]
        public string ChineseName { get; set; }
        [DataMember(Name = "englishName")]
        public string EnglishName { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }

}
