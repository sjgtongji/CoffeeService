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
    public class AddOrUpdateCouponDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember()]
        public int Id { get; set; }

        /// <summary>
        /// 1：新用户优惠（未下单） 2：全场优惠 3：满额优惠
        /// </summary>
        [DataMember()]
        public int CouponType { get; set; }

        /// <summary>
        /// 1：减额 2：折扣
        /// </summary>
        [DataMember()]
        public int PreferentialType { get; set; }

        /// <summary>
        /// PreferentialType=1：减少额度  PreferentialType=2：折扣
        /// </summary>
        [DataMember()]
        public decimal? Preferential { get; set; }

        /// <summary>
        /// 0：初始 1：有效 2：无效
        /// </summary>
        [DataMember()]
        public int State { get; set; }

        /// <summary>
        /// 有效开始时间
        /// </summary>
        [DataMember()]
        public DateTime? EffectiveStartTime { get; set; }

        /// <summary>
        /// 有效结束时间
        /// </summary>
        [DataMember()]
        public DateTime? EffectiveEndTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember()]
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember()]
        public bool IsDelete { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [DataMember()]
        public int ResId { get; set; }

        /// <summary>
        /// CouponType=2时，满足优惠优惠条件的金额
        /// </summary>
        [DataMember()]
        public decimal OfferAmount { get; set; }

        /// <summary>
        /// 优惠卷名称
        /// </summary>
        [DataMember()]
        public string CouponName { get; set; }
    }
}
