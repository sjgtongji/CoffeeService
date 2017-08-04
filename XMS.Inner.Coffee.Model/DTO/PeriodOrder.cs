using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Model
{
    public class PeriodOrder
    {
        /// <summary>
        /// 时间段Id
        /// </summary>
        public int BusinessHourWeekId { get; set; }
       /// <summary>
       /// 开始时间
       /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 订单集合
        /// </summary>
        public List<COrderPO> ListOrder = new List<COrderPO>();
    }
}
