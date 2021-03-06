﻿

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
    public class BusinessHourWeekPO : POBase
    {
    
        /// <summary>
        /// 星期 1~7
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int WeekDay { get;set; }
        
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(128)]
        public string UUID { get;set; }
        
        /// <summary>
        /// 餐厅UUID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string ResUUID { get;set; }
        
        /// <summary>
        /// 删除标记
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Boolean)]
        public bool Deleted { get;set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string Name { get;set; }
        
        /// <summary>
        /// 类别UUID 用于区分同一时间段
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(50)]
        public string TypeUUID { get;set; }
        
        /// <summary>
        /// 营业开始日期
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? StartDate { get;set; }
        
        /// <summary>
        /// 营业结束时间
        /// </summary>
        [DataMember]
        [DataType(DbType.DateTime)]
        public DateTime? EndDate { get;set; }
        
        /// <summary>
        /// 开始时间段(0点毫秒数)
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int StartTime { get;set; }
        
        /// <summary>
        /// 结束时间段(0点毫秒数)
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int EndTime { get;set; }
        
        /// <summary>
        /// 提前预定时间
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? InAdvance { get;set; }
        
        /// <summary>
        /// 最后预定时间
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? LatestOrderTime { get;set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        [DataType(DbType.Int32)]
        public int? SortIndex { get;set; }
        
        /// <summary>
        /// 允许预订数量
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? AllowOrderNumber { get;set; }
        
        /// <summary>
        /// 状态（0：上线 1：线下）
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Byte)]
        public byte State { get;set; }
        
    }
}