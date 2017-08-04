using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core;
using XMS.Core.Data;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public class CMemberAddressManager : DataMapperManagerBase<CMemberAddressPO, CMemberAddressDTO>, IManagerBase<CMemberAddressPO>
    {
        public static readonly CMemberAddressManager Instance = new CMemberAddressManager();
        private CMemberAddressManager() { }

        public bool DeleteMemberAddress(int memberAddressId, int memberWeiXinId)
        {
            CMemberAddressPO memberAddressPO = GetById(memberAddressId);
            if (memberAddressPO == null)
                throw new BusinessException("会员地址不存在");
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberWeiXinId);
            if (memberWeiXinPO == null)
                throw new BusinessException("微信会员不存在");
            if (memberWeiXinPO.MemberUUID != memberAddressPO.MemberUUID)
                throw new BusinessException("会员和订单不一致");

            memberAddressPO.IsDelete = true;
            CMemberAddressManager.Instance.Update(memberAddressPO, memberWeiXinPO.Id.ToString());
            return true;
        }

        public int Add(CMemberAddressPO po, string operatorName)
        {
            using (IEntityContext entityContext = CMemberAddressPOManager.Instance.CreateEntityContext())
            {
                return this.AddWithContext(entityContext, po, operatorName);
            }
        }
        public int AddOrUpdateMemberAddress(AddMemberAddressDTO addMemberAddress, string operatorName)
        {
            if (addMemberAddress == null)
            {
                throw new ArgumentNullException("对象不能为空");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentNullException("操作者不能为空");
            }
            CMemberAddressPO spo1 = new CMemberAddressPO
            {
                Address = addMemberAddress.Address,
                CityId = addMemberAddress.CityId,
                HouseNumber = addMemberAddress.HouseNumber,
                Label = addMemberAddress.Label,
                Latitude = addMemberAddress.Latitude,
                Longitude = addMemberAddress.Longitude,
                MemberUUID = addMemberAddress.MemberUUID,
                Name = addMemberAddress.Name,
                Telephone = addMemberAddress.Telephone,
                Id = addMemberAddress.Id,
                Sex = addMemberAddress.Sex
            };
            CMemberAddressPO cMemberAddressPO = spo1;
            if (addMemberAddress.Id > 0)
            {
                CMemberAddressPO memberAddressPO = this.GetById(addMemberAddress.Id);
                if (memberAddressPO == null)
                {
                    throw new BusinessException("此用户地址不存在");
                }
                cMemberAddressPO.MemberAddressUUID = memberAddressPO.MemberAddressUUID;
                return this.Update(cMemberAddressPO, operatorName);
            }
            return this.Add(cMemberAddressPO, operatorName);

        }
        public int AddWithContext(IEntityContext entityContext, CMemberAddressPO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不能为空");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentNullException("操作人不能为空");
            }
            if (po.Id > 0)
            {
                throw new BusinessException("添加对象错误，Id不能大于0");
            }
            if (string.IsNullOrWhiteSpace(po.MemberAddressUUID))
            {
                po.MemberAddressUUID = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(po.MemberUUID))
            {
                throw new BusinessException("会员UUID不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.Name))
            {
                throw new BusinessException("名称不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.Telephone))
            {
                throw new BusinessException("电话不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.Address))
            {
                throw new BusinessException("送货地址不能为空");
            }
            if (!po.Longitude.HasValue)
            {
                throw new BusinessException("经度必须设定");
            }
            if (!po.Latitude.HasValue)
            {
                throw new BusinessException("纬度必须设定");
            }
            if (string.IsNullOrWhiteSpace(po.HouseNumber))
            {
                throw new BusinessException("门牌号必须设定");
            }
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            CMemberAddressPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CMemberAddress, EnumOperationLogAction.Add, null, po, operatorName);
            return po.Id;

        }
        public CMemberAddressPO GetById(int id)
        {
            return CMemberAddressPOManager.Instance.GetById(id);

        }
        public CMemberAddressPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CMemberAddressPOManager.Instance.GetByIdWithContext(entityContext, id);

        }
        public CMemberAddressDTO GetLastMemberAddress(int weixinMemberId)
        {
            if (weixinMemberId == 0)
            {
                throw new ArgumentNullException("用户ID不能为空");
            }
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(weixinMemberId);
            if (memberWeiXinPO == null)
            {
                throw new ArgumentNullException("微信用户不存在");
            }
            int? id = null;
            Core.Data.QueryResult<COrderPO> QueryOrderPO = COrderManager.Instance.GetOrders(id, null, null, memberWeiXinPO.MemberUUID, null, null, null, null, null, null, null, null, null, null, null, 1, 1);
            if (((QueryOrderPO == null) || (QueryOrderPO.Items == null)) || (QueryOrderPO.Items.Length == 0))
            {
                return null;
            }
            Core.Data.QueryResult<CMemberAddressPO> addressResult = this.GetMemberAddress(null, QueryOrderPO.Items[0].MemberAddressUUID, null, null, null, null, null, null, 1, 1);
            if (((addressResult == null) || (addressResult.Items == null)) || (addressResult.Items.Length == 0))
            {
                return null;
            }
            return base.PoToDto(addressResult.Items[0]);

        }
        public Core.Data.QueryResult<CMemberAddressPO> GetMemberAddress(int? id, string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber, int startIndex, int count)
        {
            IPredicate predicate = this.GetMemberAddressCondition(id, memberAddressUUID, memberUUID, name, telephone, cityId, address, houseNumber);
            return CMemberAddressPOManager.Instance.GetMemberAddressWithTotalCountByPredicate(predicate, startIndex, count, null);

        }
        private IPredicate GetMemberAddressCondition(int? id, string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<CManagerPO>(t => t.IsDelete, false));
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CMemberAddressPO>(t => t.Id, id.Value));
            }
            if (!string.IsNullOrWhiteSpace(memberAddressUUID))
            {
                predicates.Add(PredicateFactory.Equal<CMemberAddressPO>(t => t.MemberAddressUUID, memberAddressUUID));
            }
            if (!string.IsNullOrWhiteSpace(memberUUID))
            {
                predicates.Add(PredicateFactory.Equal<CMemberAddressPO>(t => t.MemberUUID, memberUUID));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicates.Add(PredicateFactory.Like<CMemberAddressPO>(t => t.Name, "%" + name.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(telephone))
            {
                predicates.Add(PredicateFactory.Like<CMemberAddressPO>(t => t.Telephone, "%" + telephone.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(cityId))
            {
                predicates.Add(PredicateFactory.Like<CMemberAddressPO>(t => t.CityId, "%" + cityId.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                predicates.Add(PredicateFactory.Like<CMemberAddressPO>(t => t.Address, "%" + address.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(houseNumber))
            {
                predicates.Add(PredicateFactory.Like<CMemberAddressPO>(t => t.HouseNumber, "%" + houseNumber.ToSafeSQLLike() + "%"));
            }

            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }
        public Core.Data.QueryResult<CMemberAddressDTO> GetMemberAddressDTO(int? id, string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber, int startIndex, int count)
        {
            Core.Data.QueryResult<CMemberAddressDTO> resultDTO = new Core.Data.QueryResult<CMemberAddressDTO>();
            Core.Data.QueryResult<CMemberAddressPO> resultPO = this.GetMemberAddress(id, memberAddressUUID, memberUUID, name, telephone, cityId, address, houseNumber, startIndex, count);
            if (resultPO != null)
            {
                resultDTO.TotalCount = resultPO.TotalCount;
                if ((resultPO.Items != null) && (resultPO.Items.Length > 0))
                {
                    resultDTO.Items = base.PoToDtoList(resultPO.Items.ToList<CMemberAddressPO>()).ToArray();
                }
            }
            return resultDTO;

        }

        public List<CMemberAddressDTO> GetMemberAddressDTOList(int? id, string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber)
        {
            List<CMemberAddressDTO> listResult = new List<CMemberAddressDTO>();
            int count = 1;
            while (true)
            {
                Core.Data.QueryResult<CMemberAddressDTO> result = GetMemberAddressDTO(id, memberAddressUUID, memberUUID, name, telephone, cityId, address, houseNumber, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }


        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberAddressUUID"></param>
        /// <param name="memberUUID"></param>
        /// <param name="name"></param>
        /// <param name="telephone"></param>
        /// <param name="cityId"></param>
        /// <param name="address"></param>
        /// <param name="houseNumber"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public List<CMemberAddressDTO> GetAllMemberAddress(int? id, string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber, int? shopId)
        {
            List<CMemberAddressDTO> listMemeberAddress = new List<CMemberAddressDTO>();
            List<CMemberAddressDTO> memberAddresResult = GetMemberAddressDTOList(id, memberAddressUUID, memberUUID, name, telephone, cityId, address, houseNumber);
            if (memberAddresResult == null || memberAddresResult.Count == 0)
                return listMemeberAddress;

            if (!shopId.HasValue)
                return memberAddresResult;

            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(shopId.Value);
            if (restaurant == null)
                return new List<CMemberAddressDTO>();

            memberAddresResult.ForEach(item => {
                List<CResCoordinatePO> listResCoordinate = CResCoordinateManager.Instance.GetCResCoordinateList(null, restaurant.ResUUID, null, null, null);
                if (CRestaurantManager.Instance.Contains(listResCoordinate, item.Longitude, item.Latitude) == true)
                    item.IsOutRange = false;
                else
                    item.IsOutRange = true;
                listMemeberAddress.Add(item);
            });

            return listMemeberAddress;
        }
        public int Update(CMemberAddressPO po, string operatorName)
        {
            using (IEntityContext entityContext = CMemberAddressPOManager.Instance.CreateEntityContext())
            {
                return this.UpdateWithContext(entityContext, po, operatorName);
            }

        }
        public int UpdateWithContext(IEntityContext entityContext, CMemberAddressPO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不能为空");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentNullException("操作人不能为空");
            }
            if (po.Id <= 0)
            {
                throw new BusinessException("添加对象错误，Id不能大于0");
            }
            if (string.IsNullOrWhiteSpace(po.MemberAddressUUID))
            {
                throw new BusinessException("MemberAddressUUID必须设定");
            }
            if (string.IsNullOrWhiteSpace(po.MemberUUID))
            {
                throw new BusinessException("会员UUID不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.Name))
            {
                throw new BusinessException("名称不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.Telephone))
            {
                throw new BusinessException("电话不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.Address))
            {
                throw new BusinessException("送货地址不能为空");
            }
            if (!po.Longitude.HasValue)
            {
                throw new BusinessException("经度必须设定");
            }
            if (!po.Latitude.HasValue)
            {
                throw new BusinessException("纬度必须设定");
            }
            if (string.IsNullOrWhiteSpace(po.HouseNumber))
            {
                throw new BusinessException("门牌号必须设定");
            }
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            CMemberAddressPO oldObject = base.DtoToPo(base.PoToDto(this.GetByIdWithContext(entityContext, po.Id)));
            po.CreateName = oldObject.CreateName;
            po.CreateTime = oldObject.CreateTime;
            CMemberAddressPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CMemberAddress, EnumOperationLogAction.Update, oldObject, po, operatorName);
            return po.Id;

        }
    }



}