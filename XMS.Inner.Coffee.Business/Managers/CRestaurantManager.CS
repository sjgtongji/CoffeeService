using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core.Data;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;
using XMS.Core;
using System.Drawing;

namespace XMS.Inner.Coffee.Business
{
    public class CRestaurantManager : DataMapperManagerBase<CRestaurantPO, CRestaurantDTO>, IManagerBase<CRestaurantPO>
    {
        public static readonly CRestaurantManager Instance = new CRestaurantManager();
        private CRestaurantManager() { }

        /// <summary>
        /// 通过ID获取餐厅
        /// </summary>
        /// <param name="resId"></param>
        /// <returns></returns>
        public CRestaurantDTO GetRestaurantById(int resId)
        {
            if (resId <= 0)
                throw new BusinessException("餐厅ID错误");
            CRestaurantPO po = GetById(resId);
            if (po == null)
                throw new BusinessException("此餐厅不存在");
            return PoToDto(po);
        }

        public CRestaurantDTO GetRestaurantByLogin(string loginName, string password, string deviceId)
        {
            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
            {
                throw new BusinessException("登录信息不正确");
            }

            Core.Data.QueryResult<CRestaurantDTO> queryResult = GetRestaurantDTOByCondition(null, null, null, null, null, null, null, 1, 1, true, null, loginName, password);
            if (queryResult == null || queryResult.Items == null || queryResult.Items.Length == 0)
                throw new BusinessException("用户或者密码不正确");

            return queryResult.Items[0];
        }

        public bool SetRestaurantState(int resId, int state, string operatorName)
        {
            CRestaurantPO restaurantPO = CRestaurantManager.Instance.GetById(resId);
            if (restaurantPO == null)
                throw new BusinessException("此餐厅不存在");
            if (state != 0 && state != 1)
                throw new BusinessException("此餐厅状态不正确");
            restaurantPO.State = state;
            Update(restaurantPO, operatorName);
            return true;
        }

        public bool DeleteRestaurant(int resId, string operatorName)
        {
            CRestaurantPO restaurantPO = CRestaurantManager.Instance.GetById(resId);
            if (restaurantPO == null)
                throw new BusinessException("此餐厅不存在");
            restaurantPO.IsDelete = true;
            Update(restaurantPO, operatorName);
            return true;
        }

        public int AddOrUpdateRestaurantDTO(AddOrUpdateRestaurantDTO restaurant, string operatorName)
        {
            if (restaurant == null)
                throw new ArgumentNullException("餐厅必须设定");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人必须设定");

            CRestaurantPO restaurantPO = null;
            if (restaurant.Id > 0)
            {
                restaurantPO = CRestaurantManager.Instance.GetById(restaurant.Id);
                if (restaurantPO == null)
                    throw new BusinessException("此餐厅不存在");
            }
            else
            {
                restaurantPO = new CRestaurantPO();
            }

            restaurantPO.Address = restaurant.Address;
            restaurantPO.AllowedDistance = restaurant.AllowedDistance;
            restaurantPO.CityId = restaurant.CityId;
            restaurantPO.ContactNumber = restaurant.ContactNumber;
            restaurantPO.ImgUrl = restaurant.ImgUrl;
            restaurantPO.Latitude = restaurant.Latitude;
            restaurantPO.Longitude = restaurant.Longitude;
            restaurantPO.Name = restaurant.Name;
            restaurantPO.IsDelete = restaurant.IsDelete;
            restaurantPO.Remark = restaurant.Remark;
            restaurantPO.ServerFee = restaurant.ServerFee;
            restaurantPO.State = restaurant.State;
            restaurantPO.Alphabet = restaurant.Alphabet;

            if (restaurantPO.Id > 0)
                return Update(restaurantPO, operatorName);
            else
               return Add(restaurantPO, operatorName);
        }

        public List<CRestaurantDTO> GetCRestaurantList(List<string> resUUID, string name, string address, string cityId, string alphabet, List<int> state)
        {
            List<CRestaurantDTO> listResult = new List<CRestaurantDTO>();
            int count = 1;
            while (true)
            {
                Core.Data.QueryResult<CRestaurantPO> result = GetRestaurants(resUUID, name, address, cityId, alphabet, count, 1000, state, null, null);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange( PoToDtoList(result.Items.ToList()));
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }


        public Core.Data.QueryResult<CRestaurantDTO> GetRestaurantDTOByCondition(List<string> resUUID, string name, string address, string cityId, decimal? longitude, decimal? latitude, string alphabet, int startIndex, int count, bool containOutRange, List<int> state
            , string loginName, string password)
        {
            IPredicate predicate = this.GetRestaurantsCondition(resUUID, name, address, cityId, alphabet, state, loginName, password);
            Core.Data.QueryResult<CRestaurantPO> resultRest = CRestaurantPOManager.Instance.GetRestaurantWithTotalCountByPredicate(predicate, startIndex, count, null);
            if (longitude.HasValue && latitude.HasValue)
            {
                //resultRest = CRestaurantPOManager.Instance.GetRestaurantWithTotalCountByPredicate(predicate, startIndex, (int)resultRest.TotalCount, null);
                List<CRestaurantDTO> listCrest = GetCRestaurantList(resUUID, name, address, cityId, alphabet, state);
                resultRest.TotalCount = listCrest.Count;
                resultRest.Items = DtoToPoList(listCrest).ToArray();
            }
                
            Core.Data.QueryResult<CRestaurantDTO> resultRestDto = new Core.Data.QueryResult<CRestaurantDTO>();
            resultRestDto.TotalCount = resultRest.TotalCount;

            List<CRestaurantDTO> listRestaurant = new List<CRestaurantDTO>();
            if (longitude.HasValue && latitude.HasValue && resultRest.Items != null && resultRest.Items.Length > 0)
            {
                CoordinateTool AppCoordinateTool = new CoordinateTool((double)longitude.Value, (double)latitude.Value);
                foreach (var item in resultRest.Items)
                {
                    CRestaurantDTO cRestDto = PoToDto(item);
                    if (item == null)
                        continue;
                    CoordinateTool restCoordinateTool = new CoordinateTool((double)item.Longitude, (double)item.Latitude);
                    cRestDto.DouDistance = (decimal)restCoordinateTool.Distance(AppCoordinateTool);
                    listRestaurant.Add(cRestDto);
                }
            }
            else if (resultRest.Items != null && resultRest.Items.Length > 0)
            {
                listRestaurant = PoToDtoList(resultRest.Items.ToList()).ToList();
            }
            if (containOutRange == false)
            {
                //改为多边形坐标序列 开始
                //listRestaurant = listRestaurant.Where(x => x.AllowedDistance >= x.DouDistance).ToList();
                List<CRestaurantDTO> listRestaurantNew = new List<CRestaurantDTO>();
                listRestaurant.ForEach(x =>
                {
                    List<CResCoordinatePO> listResCoordinate = CResCoordinateManager.Instance.GetCResCoordinateList(null, x.ResUUID, null, null, null);
                    if (Contains(listResCoordinate, latitude, longitude) == true)
                        listRestaurantNew.Add(x);
                });
                listRestaurant = listRestaurantNew;
                //改为多边形坐标序列 结束
                resultRestDto.TotalCount = listRestaurant.Count;
                listRestaurant = (from res in listRestaurant orderby res.DouDistance ascending, res.Id ascending select res).Skip(startIndex - 1).Take(count).ToList();
            }

            listRestaurant = listRestaurant.OrderBy(x => x.DouDistance).ToList();
            resultRestDto.Items = listRestaurant.ToArray();

            return resultRestDto;
        }


        /// <summary>
        /// 判断餐厅时候在指定坐标内
        /// </summary>
        /// <param name="listResCoordinate"></param>
        /// <param name="rest"></param>
        /// <returns></returns>
        public bool Contains(List<CResCoordinatePO> listResCoordinate, decimal? longitude, decimal? latitude)
        {
            if (listResCoordinate == null || listResCoordinate.Count < 3)
                return false;

            //decimal? value = rest.Latitude;
            //rest.Latitude = rest.Longitude;
            //rest.Longitude = value;

            //int i;
            //int j;
            //bool result = false;
            //for (i = 0, j = listResCoordinate.Count - 1; i < listResCoordinate.Count; j = i++)
            //{
            //    if ((listResCoordinate[i].Latitude > rest.Latitude) != (listResCoordinate[j].Latitude > rest.Latitude) &&
            //        (rest.Longitude < (listResCoordinate[j].Longitude - listResCoordinate[i].Longitude) * (rest.Latitude - listResCoordinate[i].Latitude) / (listResCoordinate[j].Latitude - listResCoordinate[i].Latitude) + listResCoordinate[i].Longitude))
            //    {
            //        result = !result;
            //    }
            //}
            //return result;
            if (!longitude.HasValue || !latitude.HasValue)
                return false;

            PointF checkPoint = new PointF() { X = (float)longitude.Value, Y = (float)latitude.Value};
            List<PointF> polygonPoints = new List<PointF>();
            listResCoordinate.ForEach(item => {
                polygonPoints.Add(new PointF()
                {
                    X = (float)item.Longitude,
                    Y = (float)item.Latitude
                });
            });
            //List< PointF > polygonPoints
            return HelperTool.IsInPolygon(checkPoint, polygonPoints);
        }

        public Core.Data.QueryResult<CRestaurantPO> GetRestaurants(List<string> resUUID, string name,string address,string cityId, string alphabet, int startIndex, int count, List<int> state
            ,string loginName,string password)
        {
            IPredicate predicate = this.GetRestaurantsCondition(resUUID, name, address, cityId, alphabet, state, loginName, password);
            Core.Data.QueryResult<CRestaurantPO> resultRest = CRestaurantPOManager.Instance.GetRestaurantWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetRestaurantsCondition(List<string> resUUID, string name, string address, string cityId, string alphabet, List<int> state, string loginName, string password)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<CRestaurantPO>(t => t.IsDelete, false));
            if (resUUID != null && resUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CRestaurantPO>(t => t.ResUUID, resUUID.ToArray()));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicates.Add(PredicateFactory.Like<CRestaurantPO>(t => t.Name, "%" + name.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                predicates.Add(PredicateFactory.Like<CRestaurantPO>(t => t.Address, "%" + address.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(cityId))
            {
                predicates.Add(PredicateFactory.Equal<CRestaurantPO>(t => t.CityId, cityId));
            }
            if (!string.IsNullOrWhiteSpace(alphabet))
            {
                predicates.Add(PredicateFactory.Like<CRestaurantPO>(t => t.Alphabet, "%" + alphabet.ToSafeSQLLike() + "%"));
            }
            if (state != null && state.Count > 0)
                predicates.Add(PredicateFactory.In<CRestaurantPO>(t => t.State, state.ToArray()));
            if (!string.IsNullOrWhiteSpace(loginName))
                predicates.Add(PredicateFactory.Equal<CRestaurantPO>(t => t.LoginName, loginName));
            if (!string.IsNullOrWhiteSpace(password))
                predicates.Add(PredicateFactory.Equal<CRestaurantPO>(t => t.Password, password));
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int Add(CRestaurantPO po, string operatorName)
        {
            using (IEntityContext entityContext = CRestaurantPOManager.Instance.CreateEntityContext())
            {
               return AddWithContext(entityContext, po, operatorName);
            }
        }

        public int AddWithContext(IEntityContext entityContext, CRestaurantPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id > 0)
                throw new BusinessException("添加对象错误，Id不能大于0");
            if (string.IsNullOrWhiteSpace(po.ResUUID))
                po.ResUUID = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(po.Name))
                throw new BusinessException("餐厅名称不能为空");

            WordPinYin wordPinYin1 = WordPinYinManager.Instance.GetWord_Pinyin(po.Name);
            //if (wordPinYin1 != null && !string.IsNullOrWhiteSpace(wordPinYin1.PinYin))
            //{
            //    po.Alphabet = wordPinYin1.PinYin + ";" + wordPinYin1.FirstLetter;
            //}
            po.Alphabet = po.Alphabet;
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            CRestaurantPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CPropertyCategory, EnumOperationLogAction.Add, null, po, operatorName);

            return po.Id;
        }

        public CRestaurantPO GetById(int id)
        {
           return CRestaurantPOManager.Instance.GetById(id);
        }

        public CRestaurantPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CRestaurantPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public int Update(CRestaurantPO po, string operatorName)
        {
            using (IEntityContext entityContext = CRestaurantPOManager.Instance.CreateEntityContext())
            {
                return UpdateWithContext(entityContext, po, operatorName);
            }
        }

        public int UpdateWithContext(IEntityContext entityContext, CRestaurantPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id <= 0)
                throw new BusinessException("添加对象错误，Id不能大于0");
            if (string.IsNullOrWhiteSpace(po.ResUUID))
                throw new BusinessException("UUID不能为空");
            if (string.IsNullOrWhiteSpace(po.Name))
                throw new BusinessException("餐厅名称不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new BusinessException("操作人不能为空");

            //WordPinYin wordPinYin1 = WordPinYinManager.Instance.GetWord_Pinyin(po.Name);
            //if (wordPinYin1 != null && !string.IsNullOrWhiteSpace(wordPinYin1.PinYin))
            //{
            //    po.Alphabet = wordPinYin1.PinYin + ";" + wordPinYin1.FirstLetter;
            //}
            po.Alphabet = po.Alphabet;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            object oldObject = this.DtoToPo(this.PoToDto(GetByIdWithContext(entityContext, po.Id)));
            CRestaurantPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CPropertyCategory, EnumOperationLogAction.Update, oldObject, po, operatorName);

            return po.Id;
        }
    }
}
