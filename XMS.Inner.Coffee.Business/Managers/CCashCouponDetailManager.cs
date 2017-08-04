
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;
using XMS.Core.Data;
using XMS.Core;

namespace XMS.Inner.Coffee.Business
{
    public class CCashCouponDetailManager : DataMapperManagerBase<CCashCouponDetailPO, CCashCouponDetailDTO>, IManagerBase<CCashCouponDetailPO>
    {
        public static readonly CCashCouponDetailManager Instance = new CCashCouponDetailManager();
        private CCashCouponDetailManager() { }

        #region 自动生成
        public CCashCouponDetailPO GetById(int id)
        {
            return CCashCouponDetailPOManager.Instance.GetById(id);
        }

        public CCashCouponDetailPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCashCouponDetailPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public List<CCashCouponDetailPO> GetCCashCouponDetailList(int? id, List<int> assetProperty, List<int> memberWeiXinId, List<int> useState, List<int> assetStatus, string assetName, DateTime? validStartTime, DateTime? validEndTime, decimal? saleAmount, DateTime? saleStartTime, DateTime? saleEndTime)
        {
            List<CCashCouponDetailPO> listResult = new List<CCashCouponDetailPO>();
            int count = 1;
            while (true)
            {
                Core.Data.QueryResult<CCashCouponDetailPO> result = GetCCashCouponDetail(id, assetProperty, memberWeiXinId, useState, assetStatus, assetName, validStartTime, validEndTime, saleAmount, saleStartTime, saleEndTime, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }

        public Core.Data.QueryResult<CCashCouponDetailPO> GetCCashCouponDetail(int? id, List<int> assetProperty, List<int> memberWeiXinId, List<int> useState, List<int> assetStatus, string assetName, DateTime? validStartTime, DateTime? validEndTime, decimal? saleAmount, DateTime? saleStartTime, DateTime? saleEndTime, int startIndex, int count)
        {
            IPredicate predicate = this.GetCCashCouponDetailCondition(id, assetProperty, memberWeiXinId, useState, assetStatus, assetName, validStartTime, validEndTime, saleAmount, saleStartTime, saleEndTime);
            Core.Data.QueryResult<CCashCouponDetailPO> resultRest = CCashCouponDetailPOManager.Instance.GetCCashCouponDetailWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetCCashCouponDetailCondition(int? id, List<int> assetProperty, List<int> memberWeiXinId, List<int> useState, List<int> assetStatus, string assetName, DateTime? validStartTime, DateTime? validEndTime, decimal? saleAmount, DateTime? saleStartTime, DateTime? saleEndTime)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(t => t.Id, id));
            }
            if (assetProperty != null && assetProperty.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CCashCouponDetailPO>(t => t.AssetProperty, assetProperty.ToArray()));
            }
            if (memberWeiXinId != null && memberWeiXinId.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CCashCouponDetailPO>(t => t.MemberWeiXinId, memberWeiXinId.ToArray()));
            }

            if (useState != null && useState.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CCashCouponDetailPO>(t => t.UseState, useState.ToArray()));
            }
            if (assetStatus != null && assetStatus.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CCashCouponDetailPO>(t => t.AssetStatus, assetStatus.ToArray()));
            }

            if (!string.IsNullOrWhiteSpace(assetName))
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(t => t.AssetName, assetName));
            }
            if (validStartTime.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(t => t.ValidStartTime, validStartTime));
            }
            if (validEndTime.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(t => t.ValidEndTime, validEndTime));
            }
            if (saleAmount.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(t => t.SaleAmount, saleAmount));
            }
            if (saleStartTime.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(t => t.SaleStartTime, saleStartTime));
            }
            if (saleEndTime.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(t => t.SaleEndTime, saleEndTime));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddWithContext(IEntityContext entityContext, CCashCouponDetailPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CCashCouponDetailNull.ToException();
            if (string.IsNullOrWhiteSpace(po.AssetName))
                throw ErrorCodeHelper.CCashCouponDetailAssetNameNotSet.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            CCashCouponDetailPO oldObject = null;
            CCashCouponDetailPO newObject = po;

            newObject.CreateName = operatorName;
            newObject.CreateTime = DateTime.Now;
            newObject.UpdateName = operatorName;
            newObject.UpdateTime = DateTime.Now;

            CCashCouponDetailPOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CCashCouponDetail, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }

        public int UpdateWithContext(IEntityContext entityContext, CCashCouponDetailPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CCashCouponDetailNull.ToException();
            if (string.IsNullOrWhiteSpace(po.AssetName))
                throw ErrorCodeHelper.CCashCouponDetailAssetNameNotSet.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            CCashCouponDetailPO oldObject = null;
            CCashCouponDetailPO newObject = po;

            newObject.UpdateName = operatorName;
            newObject.UpdateTime = DateTime.Now;

            oldObject = GetByIdWithContext(entityContext, po.Id);
            if (oldObject == null)
                throw ErrorCodeHelper.CCashCouponDetailNotExist.ToException();
            CCashCouponDetailPOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CCashCouponDetail, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }

        public int Add(CCashCouponDetailPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCashCouponDetailPOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }

        public int Update(CCashCouponDetailPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCashCouponDetailPOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }


        public int AddCCashCouponDetail(AddCCashCouponDetailDTO cCashCouponDetail, string operatorName)
        {
            if (cCashCouponDetail == null)
                throw ErrorCodeHelper.CCashCouponDetailNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CCashCouponDetailPO cCashCouponDetailPO = new CCashCouponDetailPO();

            cCashCouponDetailPO.AssetProperty = cCashCouponDetail.AssetProperty;
            cCashCouponDetailPO.AssetName = cCashCouponDetail.AssetName;
            cCashCouponDetailPO.ValidStartTime = cCashCouponDetail.ValidStartTime;
            cCashCouponDetailPO.ValidEndTime = cCashCouponDetail.ValidEndTime;
            cCashCouponDetailPO.SaleAmount = cCashCouponDetail.SaleAmount;
            cCashCouponDetailPO.SaleStartTime = cCashCouponDetail.SaleStartTime;
            cCashCouponDetailPO.SaleEndTime = cCashCouponDetail.SaleEndTime;
            cCashCouponDetailPO.Description = cCashCouponDetail.Description;
            cCashCouponDetailPO.ValAmount = cCashCouponDetail.ValAmount;

            return Add(cCashCouponDetailPO, operatorName);
        }

        public int UpdateCCashCouponDetail(UpdateCCashCouponDetailDTO cCashCouponDetail, string operatorName)
        {

            if (cCashCouponDetail == null)
                throw ErrorCodeHelper.CCashCouponDetailNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CCashCouponDetailPO cCashCouponDetailPO = CCashCouponDetailManager.Instance.GetById(cCashCouponDetail.Id);
            if (cCashCouponDetailPO == null)
                throw ErrorCodeHelper.CCashCouponDetailNotExist.ToException();

            if (cCashCouponDetail.Id <= 0)
            {
                throw ErrorCodeHelper.IdLessErr.ToException();
            }

            cCashCouponDetailPO.AssetProperty = cCashCouponDetail.AssetProperty;
            cCashCouponDetailPO.AssetName = cCashCouponDetail.AssetName;
            cCashCouponDetailPO.ValidStartTime = cCashCouponDetail.ValidStartTime;
            cCashCouponDetailPO.ValidEndTime = cCashCouponDetail.ValidEndTime;
            cCashCouponDetailPO.SaleAmount = cCashCouponDetail.SaleAmount;
            cCashCouponDetailPO.SaleStartTime = cCashCouponDetail.SaleStartTime;
            cCashCouponDetailPO.SaleEndTime = cCashCouponDetail.SaleEndTime;
            cCashCouponDetailPO.Description = cCashCouponDetail.Description;
            cCashCouponDetailPO.ValAmount = cCashCouponDetail.ValAmount;

            return Update(cCashCouponDetailPO, operatorName);
        }

        #endregion
    }
}
