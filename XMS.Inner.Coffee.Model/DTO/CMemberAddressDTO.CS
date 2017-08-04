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
    public class CMemberAddressDTO : DTOBase
    {
    
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name= "memberAddressUUID")]
        public string MemberAddressUUID { get;set; }

        /// <summary>
        /// 会员UUID
        /// </summary>
        [DataMember(Name = "memberUUID")]
        public string MemberUUID { get;set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get;set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DataMember(Name = "telephone")]
        public string Telephone { get;set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMember(Name = "cityId")]
        public string CityId { get;set; }

        /// <summary>
        /// 送货地址
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get;set; }

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember(Name = "longitude")]
        public decimal? Longitude { get;set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember(Name = "latitude")]
        public decimal? Latitude { get;set; }

        /// <summary>
        /// 门牌号
        /// </summary>
        [DataMember(Name = "houseNumber")]
        public string HouseNumber { get;set; }

        /// <summary>
        /// 标签：例如公司，家里等  最多四个字
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get;set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get;set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember(Name = "createName")]
        public string CreateName { get;set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get;set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember(Name = "updateName")]
        public string UpdateName { get;set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get;set; }

        /// <summary>
        /// 版本
        /// </summary>
        [DataMember(Name = "version")]
        public int Version { get;set; }

        /// <summary>
        /// 性别（0：女  1：男）
        /// </summary>
        [DataMember(Name = "sex")]
        public string Sex { get; set; }

        /// <summary>
        /// 是不是超出餐厅的配送范围
        /// </summary>
        [DataMember(Name = "isOutRange")]
        public bool IsOutRange { get; set; }
    }
}