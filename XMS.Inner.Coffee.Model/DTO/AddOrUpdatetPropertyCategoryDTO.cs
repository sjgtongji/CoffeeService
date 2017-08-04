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
    public class AddOrUpdatetPropertyCategoryDTO
    {
        // Properties
        [DataMember()]
        public string ChineseName { get; set; }
        [DataMember()]
        public bool EnableMultiple { get; set; }
        [DataMember()]
        public string EnglishName { get; set; }
        [DataMember()]
        public int Id { get; set; }
        [DataMember()]
        public bool IsDelete { get; set; }
        [DataMember()]
        public int ResId { get; set; }
        [DataMember()]
        public int Sort { get; set; }
        [DataMember()]
        public int Type { get; set; }
    }


}