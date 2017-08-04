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
    public class CMemberWeiXinManager: DataMapperManagerBase<CMemberWeiXinPO, CMemberWeiXinDTO>, IManagerBase<CMemberWeiXinPO>
    {
        public static readonly CMemberWeiXinManager Instance = new CMemberWeiXinManager();
        private CMemberWeiXinManager() { }

        public Core.Data.QueryResult<CMemberWeiXinDTO> GetMemberWeiXinDTOs(int? id,string memberUUID, string weiXinOpenId, string phoneNumber, string name, int startIndex, int count)
        {
            Core.Data.QueryResult<CMemberWeiXinDTO> resultDTO = new Core.Data.QueryResult<CMemberWeiXinDTO>();
            Core.Data.QueryResult<CMemberWeiXinPO> resultPO = GetMemberWeiXins(id, memberUUID, weiXinOpenId, phoneNumber, name, startIndex, count);
            if (resultPO == null)
                return resultDTO;
            resultDTO.TotalCount = resultPO.TotalCount;

            if (resultPO.Items != null && resultPO.Items.Length > 0)
                resultDTO.Items = PoToDtoList(resultPO.Items.ToList()).ToArray();

            return resultDTO;
        }

        public Core.Data.QueryResult<CMemberWeiXinPO> GetMemberWeiXins(int? id,string memberUUID, string weiXinOpenId, string phoneNumber, string name, int startIndex, int count)
        {
            Sort sort = PredicateFactory.Sort("CreateTime", "desc"); ;
            IPredicate predicate = this.GetMemberWeiXinsCondition(id, memberUUID, weiXinOpenId, phoneNumber, name);
            Core.Data.QueryResult<CMemberWeiXinPO> resultRest = CMemberWeiXinPOManager.Instance.GetMemberWeiXinWithTotalCountByPredicate(predicate, startIndex, count, sort);
            return resultRest;
        }

        private IPredicate GetMemberWeiXinsCondition(int? id,string memberUUID, string weiXinOpenId, string phoneNumber, string name)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<CMemberWeiXinPO>(t => t.IsDelete, false));
            if (id.HasValue)
                predicates.Add(PredicateFactory.Equal<CMemberWeiXinPO>(t => t.Id, id.Value));
            if (!string.IsNullOrWhiteSpace(memberUUID))
            {
                predicates.Add(PredicateFactory.Equal<CMemberWeiXinPO>(t => t.MemberUUID, memberUUID));
            }
            if (!string.IsNullOrWhiteSpace(weiXinOpenId))
            {
                predicates.Add(PredicateFactory.Equal<CMemberWeiXinPO>(t => t.WeiXinOpenId, weiXinOpenId));
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                predicates.Add(PredicateFactory.Like<CMemberWeiXinPO>(t => t.PhoneNumber, "%" + phoneNumber.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicates.Add(PredicateFactory.Like<CMemberWeiXinPO>(t => t.Name, "%" + name.ToSafeSQLLike() + "%"));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddOrUpdateDTO(CMemberWeiXinDTO dto, string operatorName)
        {
            if (dto == null)
                return 0;
            if (dto.Id == 0)
                return Add(DtoToPo(dto), operatorName);
            else
                return Update(DtoToPo(dto), operatorName);
        }

        public int Add(CMemberWeiXinPO po, string operatorName)
        {
            using (IEntityContext entityContext = CMemberWeiXinPOManager.Instance.CreateEntityContext())
            {
                return AddWithContext(entityContext, po, operatorName);
            }
        }

        public int AddWithContext(IEntityContext entityContext, CMemberWeiXinPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id > 0)
                throw new BusinessException("添加对象错误，Id不能大于0");
            if (string.IsNullOrWhiteSpace(po.MemberUUID))
                throw new BusinessException("UUID必须设定");
            if (string.IsNullOrWhiteSpace(po.WeiXinOpenId))
                throw new BusinessException("微信OpenId必须设定");

            Core.Data.QueryResult<CMemberWeiXinPO> resultMemberWeiXin = GetMemberWeiXins(null, null, po.WeiXinOpenId, null, null, 1, 1);
            if (resultMemberWeiXin != null && resultMemberWeiXin.Items != null && resultMemberWeiXin.Items.Length > 0)
            {
                throw new BusinessException("此微信用户已存在");
            }

            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            CMemberWeiXinPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CMemberWeiXin, EnumOperationLogAction.Add, null, po, operatorName);

            return po.Id;
        }

        public CMemberWeiXinPO GetById(int id)
        {
            return CMemberWeiXinPOManager.Instance.GetById(id);
        }

        public CMemberWeiXinPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CMemberWeiXinPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public int Update(CMemberWeiXinPO po, string operatorName)
        {
            using (IEntityContext entityContext = CMemberWeiXinPOManager.Instance.CreateEntityContext())
            {
                return UpdateWithContext(entityContext, po, operatorName);
            }
        }

        public int UpdateWithContext(IEntityContext entityContext, CMemberWeiXinPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id <= 0)
                throw new BusinessException("添加对象错误，Id不能小于0");
            if (string.IsNullOrWhiteSpace(po.MemberUUID))
                throw new BusinessException("UUID必须设定");
            if (string.IsNullOrWhiteSpace(po.WeiXinOpenId))
                throw new BusinessException("微信OpenId必须设定");

            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            Core.Data.QueryResult<CMemberWeiXinPO> resultMemberWeiXin = GetMemberWeiXins(null, null, po.WeiXinOpenId, null, null, 1, 1);
            if (resultMemberWeiXin != null && resultMemberWeiXin.Items != null && resultMemberWeiXin.Items.Length > 0)
            {
                if (resultMemberWeiXin.Items[0].Id != po.Id)
                    throw new BusinessException("此微信用户已存在");
            }

            object oldObject = this.DtoToPo(this.PoToDto(GetByIdWithContext(entityContext, po.Id)));
            CMemberWeiXinPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CMemberWeiXin, EnumOperationLogAction.Update, oldObject, po, operatorName);

            return po.Id;
        }
    }
}
