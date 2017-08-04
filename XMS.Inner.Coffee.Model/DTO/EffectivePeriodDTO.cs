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
    public class EffectivePeriodDTO
    {
        /// <summary>
        /// 1:周一,2：周二,.....7:周日
        /// </summary>
        [DataMember]
        public int WeekFlag { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DataMember]
        public string ShowName { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }

        /// <summary>
        /// 营业时段段
        /// </summary>
        [DataMember]
        public List<EffectivePeriodInfo> EffectivePeriod = new List<EffectivePeriodInfo>();

    }

    [DataContract]
    [Serializable]
    public class EffectivePeriodInfo
    {
        /// <summary>
        /// 开始时间，0点毫秒数
        /// </summary>
        [DataMember]
        public long StartTime { get; set; }

        /// <summary>
        /// 结束时间，0点毫秒数
        /// </summary>
        [DataMember]
        public long EndTime { get; set; }

        /// <summary>
        /// 是否可用（false:已预订满）
        /// </summary>
        [DataMember]
        public bool Available { get; set; }
    }
}
