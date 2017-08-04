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
    public class AddMemberAddressDTO
    {
        // Properties
        [DataMember(Name = "address")]
        public string Address { get; set; }
        [DataMember(Name = "cityId")]
        public string CityId { get; set; }
        [DataMember(Name = "houseNumber")]
        public string HouseNumber { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "label")]
        public string Label { get; set; }
        [DataMember(Name = "latitude")]
        public decimal? Latitude { get; set; }
        [DataMember(Name = "longitude")]
        public decimal? Longitude { get; set; }
        [DataMember(Name = "memberUUID")]
        public string MemberUUID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "sex")]
        public string Sex { get; set; }
        [DataMember(Name = "telephone")]
        public string Telephone { get; set; }

    }
}