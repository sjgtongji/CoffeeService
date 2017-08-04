using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMS.Core.Messaging;

namespace XMS.Inner.Coffee.Business
{
    [Serializable]
    [Message("XMS.Core.Coffee.PushMessage")]
    public class PushOrderMessage
    {
        /// <summary>
        /// 餐厅ID
        /// </summary>
        public string ResId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        public PushOrderMessage(string resId, DateTime startDate, DateTime endDate)
        {
            this.ResId = resId;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
