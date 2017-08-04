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
    public class AddResCoordinateDTO
    {
        /// <summary>
        /// 餐厅ID
        /// </summary>
        [DataMember(Name = "resId")]
        public int ResId { get; set; }

        /// <summary>
        /// 坐标集合
        /// </summary>
        [DataMember(Name = "listCoordinateInfo")]
        public List<CoordinateInfo> ListCoordinateInfo = new List<CoordinateInfo>();
    }

    public class CoordinateInfo
    {

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember(Name = "longitude")]
        public decimal Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember(Name = "latitude")]
        public decimal Latitude { get; set; }
    }
}
