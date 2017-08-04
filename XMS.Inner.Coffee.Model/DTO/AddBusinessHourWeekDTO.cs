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
    public class AddBusinessHourWeekDTO : BusinessHourWeekBaseDTO
    {
        /// <summary>
        /// 星期 1~7
        /// </summary>
        [DataMember]
        public int WeekDay { get; set; }

        /// <summary>
        /// 类别UUID 用于区分同一时间段
        /// </summary>
        [DataMember]
        public string TypeUUID { get; set; }

    }
}