
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
    public class CCashCouponManager: DataMapperManagerBase<CCashCouponPO, CCashCouponDTO>, IManagerBase<CCashCouponPO>
    {
        public static readonly CCashCouponManager Instance = new CCashCouponManager();
        private CCashCouponManager() { }

        #region 自动生成
        public CCashCouponPO GetById(int id)
        {
            return CCashCouponPOManager.Instance.GetById(id);
        }

        public CCashCouponPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCashCouponPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public List<CCashCouponPO> GetCCashCouponList(int? id, List<int> assetProperty, string assetName, DateTime? validStartTime, DateTime? validEndTime, List<int> assetStatus, decimal? saleAmount, DateTime? saleStartTime, DateTime? saleEndTime)
        {
            List<CCashCouponPO> listResult = new List<CCashCouponPO>();
            int count = 1;
            while(true)
            {
                Core.Data.QueryResult<CCashCouponPO> result = GetCCashCoupon(id, assetProperty, assetName, validStartTime, validEndTime, assetStatus, saleAmount, saleStartTime, saleEndTime, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }
        
        public Core.Data.QueryResult<CCashCouponPO> GetCCashCoupon(int? id, List<int> assetProperty, string assetName, DateTime? validStartTime, DateTime? validEndTime, List<int> assetStatus, decimal? saleAmount, DateTime? saleStartTime, DateTime? saleEndTime,  int startIndex, int count)
        {
            IPredicate predicate = this.GetCCashCouponCondition(id, assetProperty, assetName, validStartTime, validEndTime, assetStatus, saleAmount, saleStartTime, saleEndTime);
            Core.Data.QueryResult<CCashCouponPO> resultRest = CCashCouponPOManager.Instance.GetCCashCouponWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetCCashCouponCondition(int? id, List<int> assetProperty, string assetName, DateTime? validStartTime, DateTime? validEndTime, List<int> assetStatus, decimal? saleAmount, DateTime? saleStartTime, DateTime? saleEndTime)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCashCouponPO>(t => t.Id, id));
            }
            if (assetProperty != null && assetProperty.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CCashCouponPO>(t => t.AssetProperty, assetProperty.ToArray()));
            }
            if (!string.IsNullOrWhiteSpace(assetName))
            {
                predicates.Add(PredicateFactory.Like<CCashCouponPO>(t => t.AssetName, assetName.ToSafeSQLLike()));
            }   
            if (validStartTime.HasValue)
            {
                predicates.Add(PredicateFactory.LessEqual<CCashCouponPO>(t => t.ValidStartTime, validStartTime));
            }
            if (validEndTime.HasValue)
            {
                predicates.Add(PredicateFactory.GreaterEqual<CCashCouponPO>(t => t.ValidEndTime, validEndTime));
            }
            if (assetStatus != null && assetStatus.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CCashCouponPO>(t => t.AssetStatus, assetStatus.ToArray()));
            }
            if (saleAmount.HasValue)
            {
                predicates.Add(PredicateFactory.LessEqual<CCashCouponPO>(t => t.SaleAmount, saleAmount));
            }
            if (saleStartTime.HasValue)
            {
                predicates.Add(PredicateFactory.LessEqual<CCashCouponPO>(t => t.SaleStartTime, saleStartTime));
            }
            if (saleEndTime.HasValue)
            {
                predicates.Add(PredicateFactory.GreaterEqual<CCashCouponPO>(t => t.SaleEndTime, saleEndTime));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddWithContext(IEntityContext entityContext, CCashCouponPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CCashCouponNull.ToException();
             if (string.IsNullOrWhiteSpace(po.AssetName))
                throw ErrorCodeHelper.CCashCouponAssetNameNotSet.ToException();           

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            CCashCouponPO oldObject = null;
            CCashCouponPO newObject = po;
            
            newObject.CreateName =  operatorName;
            newObject.CreateTime =  DateTime.Now;
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            CCashCouponPOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CCashCoupon, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int UpdateWithContext(IEntityContext entityContext, CCashCouponPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CCashCouponNull.ToException();
             if (string.IsNullOrWhiteSpace(po.AssetName))
                throw ErrorCodeHelper.CCashCouponAssetNameNotSet.ToException();          

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            CCashCouponPO oldObject = null;
            CCashCouponPO newObject = po;
                        
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            oldObject = GetByIdWithContext(entityContext, po.Id);  
            if (oldObject == null)
                throw ErrorCodeHelper.CCashCouponNotExist.ToException();  
            CCashCouponPOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CCashCoupon, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int Add(CCashCouponPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCashCouponPOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        public int Update(CCashCouponPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCashCouponPOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }

       
        public int AddCCashCoupon(AddCCashCouponDTO cCashCoupon, string operatorName)
        {
            if (cCashCoupon == null)
               throw ErrorCodeHelper.CCashCouponNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CCashCouponPO cCashCouponPO = new CCashCouponPO();
            
            cCashCouponPO.AssetProperty = cCashCoupon.AssetProperty;
            cCashCouponPO.AssetName = cCashCoupon.AssetName;
            cCashCouponPO.ValidStartTime = cCashCoupon.ValidStartTime;
            cCashCouponPO.ValidEndTime = cCashCoupon.ValidEndTime;
            cCashCouponPO.AssetStatus = cCashCoupon.AssetStatus;
            cCashCouponPO.SaleAmount = cCashCoupon.SaleAmount;
            cCashCouponPO.SaleStartTime = cCashCoupon.SaleStartTime;
            cCashCouponPO.SaleEndTime = cCashCoupon.SaleEndTime;
            cCashCouponPO.Description = cCashCoupon.Description;
            cCashCouponPO.ValAmount = cCashCoupon.ValAmount;
            
            return Add(cCashCouponPO, operatorName);
        }
        
        public int UpdateCCashCoupon(UpdateCCashCouponDTO cCashCoupon, string operatorName)
        {            
        
            if (cCashCoupon == null)
               throw ErrorCodeHelper.CCashCouponNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CCashCouponPO cCashCouponPO = CCashCouponManager.Instance.GetById(cCashCoupon.Id);
            if (cCashCouponPO == null)
                    throw ErrorCodeHelper.CCashCouponNotExist.ToException();
            
            if (cCashCoupon.Id <= 0)
            {
                throw ErrorCodeHelper.IdLessErr.ToException();
            }
            
            cCashCouponPO.AssetProperty = cCashCoupon.AssetProperty;
            cCashCouponPO.AssetName = cCashCoupon.AssetName;
            cCashCouponPO.ValidStartTime = cCashCoupon.ValidStartTime;
            cCashCouponPO.ValidEndTime = cCashCoupon.ValidEndTime;
            cCashCouponPO.AssetStatus = cCashCoupon.AssetStatus;
            cCashCouponPO.SaleAmount = cCashCoupon.SaleAmount;
            cCashCouponPO.SaleStartTime = cCashCoupon.SaleStartTime;
            cCashCouponPO.SaleEndTime = cCashCoupon.SaleEndTime;
            cCashCouponPO.Description = cCashCoupon.Description;
            cCashCouponPO.ValAmount = cCashCoupon.ValAmount;

            return Update(cCashCouponPO, operatorName);
        }
        
        #endregion
    }
}
