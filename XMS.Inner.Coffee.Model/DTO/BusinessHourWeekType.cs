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
    public class BusinessHourWeekTypeInfoDTO
    {
        [DataMember]
        public List<BusinessHourWeekTypeDTO> items = new List<BusinessHourWeekTypeDTO>();
    }

    [DataContract]
    [Serializable]
    public class BusinessHourWeekTypeDTO
    {
        /// <summary>
        /// Key
        /// </summary>
        [DataMember]
        public string Key { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [DataMember]
        public string Value { get; set; }

        /// <summary>
        /// 时间段
        /// </summary>
        [DataMember]
        public List<BusinessHourWeekDTO> listBusinessHourWeek = new List<BusinessHourWeekDTO>();
    }
}
