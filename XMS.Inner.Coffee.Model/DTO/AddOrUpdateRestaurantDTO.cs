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
    public class AddOrUpdateCRestaurantDTO
    {
        // Properties
        [DataMember(Name = "address")]
        public string Address { get; set; }
        [DataMember(Name = "alphabet")]
        public string Alphabet { get; set; }
        [DataMember(Name = "cityId")]
        public string CityId { get; set; }
        [DataMember(Name = "latitude")]
        public decimal? Latitude { get; set; }
        [DataMember(Name = "longitude")]
        public decimal? Longitude { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "remark")]
        public string Remark { get; set; }
        [DataMember(Name = "resUUID")]
        public string ResUUID { get; set; }
    }

}