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
    public class BusinessHourWeekBaseDTO
    {
        /// <summary>
        /// 餐厅UUID
        /// </summary>
        [DataMember]
        public string ResUUID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 营业开始日期
        /// </summary>
        [DataMember]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 营业结束时间
        /// </summary>
        [DataMember]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 开始时间段(0点毫秒数)
        /// </summary>
        [DataMember]
        public int StartTime { get; set; }

        /// <summary>
        /// 结束时间段(0点毫秒数)
        /// </summary>
        [DataMember]
        public int EndTime { get; set; }

        /// <summary>
        /// 提前预定时间
        /// </summary>
        [DataMember]
        public decimal? InAdvance { get; set; }

        /// <summary>
        /// 最后预定时间
        /// </summary>
        [DataMember]
        public int? LatestOrderTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int? SortIndex { get; set; }

        /// <summary>
        /// 允许预订数量
        /// </summary>
        [DataMember]
        public decimal? AllowOrderNumber { get; set; }

        /// <summary>
        /// 状态（0：上线 1：线下）
        /// </summary>
        [DataMember]
        public byte State { get; set; }
    }
}
