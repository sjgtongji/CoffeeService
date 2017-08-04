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
    public class CRestaurantDTO : DTOBase
    {

        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "resUUID")]
        public string ResUUID { get;set; }

        /// <summary>
        /// 餐厅名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get;set; }

        /// <summary>
        /// 餐厅地址
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
        /// 餐厅所在城市ID
        /// </summary>
        [DataMember(Name = "cityId")]
        public string CityId { get;set; }

        /// <summary>
        /// 餐厅名称全拼(kf;kafei)
        /// </summary>
        [DataMember(Name = "alphabet")]
        public string Alphabet { get;set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get;set; }

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
        public int Version { get;set; }

        /// <summary>
        /// 允许配送范围
        /// </summary>
        [DataMember(Name= "allowedDistance")]
        public decimal AllowedDistance { get; set; }

        /// <summary>
        /// 服务费（配送费）
        /// </summary>
        [DataMember(Name = "serverFee")]
        public decimal ServerFee { get; set; }

        /// <summary>
        /// 餐厅图片路径
        /// </summary>
        [DataMember(Name = "imgUrl")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 餐厅图片路径
        /// </summary>
        [DataMember(Name = "contactNumber")]
        public string ContactNumber { get; set; }

        /// <summary>
        /// 0：下线 1：上线
        /// </summary>
        [DataMember(Name= "state")]
        public int State { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [DataMember(Name = "loginName")]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// 登录设备号
        /// </summary>
        [DataMember(Name = "deviceId")]
        public string DeviceId { get; set; }

        #region 虚拟属性
        /// <summary>
        /// 距离（单位：公里）
        /// </summary>
        [DataMember(Name = "douDistence")]
        public decimal DouDistance { get; set; }

        /// <summary>
        /// 距离（单位：公里）
        /// </summary>
        [DataMember(Name = "distence")]
        public string Distance { get { return DouDistance.ToString("0.0") + "km"; } set {
            } }
        #endregion

    }
}