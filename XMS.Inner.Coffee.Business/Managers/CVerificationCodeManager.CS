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
    public class CVerificationCodeManager : DataMapperManagerBase<CVerificationCodePO, CVerificationCodeDTO>, IManagerBase<CVerificationCodePO>
    {
        public static readonly CVerificationCodeManager Instance = new CVerificationCodeManager();
        private CVerificationCodeManager() { }

        public int Add(CVerificationCodePO po, string operatorName)
        {
            using (IEntityContext entityContext = CVerificationCodePOManager.Instance.CreateEntityContext())
            {
                return this.AddWithContext(entityContext, po, operatorName);
            }
        }
        public int AddWithContext(IEntityContext entityContext, CVerificationCodePO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不能为空");
            }
            if (po.Id > 0)
            {
                throw new BusinessException("添加对象错误，Id不能大于0");
            }
            CVerificationCodePOManager.Instance.AddWithContext(entityContext, po);
            return po.Id;
        }
        public bool CheckCode(int memberId, string code)
        {
            if (memberId <= 0)
            {
                throw new BusinessException("用户ID不正确");
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new BusinessException("请输入验证码");
            }

            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberId);
            if (memberWeiXinPO == null)
            {
                throw new BusinessException("此微信用户不存在");
            }
            List<int> verificationState = new List<int> { 0 };
            Core.Data.QueryResult<CVerificationCodePO> verificationCodeQuery = this.GetVerificationCodes(memberWeiXinPO.MemberUUID, null, null, code, verificationState, new DateTime?(DateTime.Now), null, 1, 0x3e8);
            if ((verificationCodeQuery.Items == null) || (verificationCodeQuery.Items.Length == 0))
            {
                throw new BusinessException("无效验证码");
            }
            foreach (CVerificationCodePO item in verificationCodeQuery.Items)
            {
                item.VerificationState = 1;
                CVerificationCodePOManager.Instance.Update(item);
            }
            memberWeiXinPO.PhoneNumber = verificationCodeQuery.Items[0].PhoneNumber;
            CMemberWeiXinManager.Instance.Update(memberWeiXinPO, "验证验证码");
            return true;

        }
        public int CreateVerificationCode(int memberId, string phoneNumber)
        {
            if (memberId <= 0)
            {
                throw new BusinessException("用户ID不正确");
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new BusinessException("电话号码为空");
            }
            if (!phoneNumber.IsMobilePhone())
            {
                throw new BusinessException("电话号码格式不正确");
            }
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberId);
            if (memberWeiXinPO == null)
            {
                throw new BusinessException("此微信用户不存在");
            }
            CVerificationCodePO epo1 = new CVerificationCodePO
            {
                Code = HelperTool.GenerateRandomNumberNew(6),
                EffectiveEndTime = new DateTime?(DateTime.Now.AddMinutes(10.0)),
                MemberUUID = memberWeiXinPO.MemberUUID,
                PhoneNumber = phoneNumber,
                VerificationState = 0,
                WeiXinOpenId = memberWeiXinPO.WeiXinOpenId
            };
            CVerificationCodePO po = epo1;
            List<int> verificationState = new List<int> { 0 };
            Core.Data.QueryResult<CVerificationCodePO> verificationCodeQuery = this.GetVerificationCodes(memberWeiXinPO.MemberUUID, null, null, null, verificationState, new DateTime?(DateTime.Now), null, 1, 1000);
            if ((verificationCodeQuery.Items != null) && (verificationCodeQuery.Items.Length > 0))
            {
                while (true)
                {
                    if (verificationCodeQuery.Items.Count<CVerificationCodePO>(x => x.Code == po.Code) == 0)
                    {
                        break;
                    }
                    po.Code = HelperTool.GenerateRandomNumberNew(6);
                }
            }
            int id = CVerificationCodePOManager.Instance.Add(po);
            HttpHelper.SendSms(phoneNumber, string.Format("您好！你的验证码为{0}，请在十分钟内完成验证。", po.Code));
            return id;

        }
        public CVerificationCodePO GetById(int id)
        {
            return CVerificationCodePOManager.Instance.GetById(id);
        }
        public CVerificationCodePO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CVerificationCodePOManager.Instance.GetByIdWithContext(entityContext, id);
        }
        public Core.Data.QueryResult<CVerificationCodePO> GetVerificationCodes(string memberUUID, string weiXinOpenId, string phoneNumber, string code, List<int> verificationState, DateTime? minEffectiveEndTime, DateTime? maxEffectiveEndTime, int startIndex, int count)
        {
            IPredicate predicate = this.GetVerificationCodesCondition(memberUUID, weiXinOpenId, phoneNumber, code, verificationState, minEffectiveEndTime, maxEffectiveEndTime);
            return CVerificationCodePOManager.Instance.GetVerificationCodeWithTotalCountByPredicate(predicate, startIndex, count, null);
        }
        private IPredicate GetVerificationCodesCondition(string memberUUID, string weiXinOpenId, string phoneNumber, string code, List<int> verificationState, DateTime? minEffectiveEndTime, DateTime? maxEffectiveEndTime)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            //predicates.Add(PredicateFactory.Equal<CVerificationCodePO>(t => t.d, false));
            if (!string.IsNullOrWhiteSpace(memberUUID))
            {
                predicates.Add(PredicateFactory.Equal<CVerificationCodePO>(t => t.MemberUUID, memberUUID));
            }
            if (!string.IsNullOrWhiteSpace(weiXinOpenId))
            {
                predicates.Add(PredicateFactory.Equal<CVerificationCodePO>(t => t.WeiXinOpenId, weiXinOpenId));
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                predicates.Add(PredicateFactory.Equal<CVerificationCodePO>(t => t.PhoneNumber, phoneNumber));
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                predicates.Add(PredicateFactory.Equal<CVerificationCodePO>(t => t.Code, code));
            }
            if ((verificationState != null) && (verificationState.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CVerificationCodePO>(t => t.VerificationState, verificationState.ToArray()));
            }
            if (minEffectiveEndTime.HasValue)
            {
                predicates.Add(PredicateFactory.GreaterEqual<CVerificationCodePO>(t => t.EffectiveEndTime, minEffectiveEndTime));
            }
            if (maxEffectiveEndTime.HasValue)
            {
                predicates.Add(PredicateFactory.LessEqual<CVerificationCodePO>(t => t.EffectiveEndTime, maxEffectiveEndTime));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }
        public int Update(CVerificationCodePO po, string operatorName)
        {
            using (IEntityContext entityContext = CVerificationCodePOManager.Instance.CreateEntityContext())
            {
                return this.UpdateWithContext(entityContext, po, operatorName);
            }
        }
        public int UpdateWithContext(IEntityContext entityContext, CVerificationCodePO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不能为空");
            }
            if (po.Id <= 0)
            {
                throw new BusinessException("添加对象错误，Id不能大于0");
            }
            CVerificationCodePOManager.Instance.UpdateWithContext(entityContext, po);
            return po.Id;

        }
    }



}