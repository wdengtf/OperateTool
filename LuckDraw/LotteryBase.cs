using Framework.EF;
using Framework.Model;
using LuckDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YYT.BLL;
using YYT.Model;
using Framework;

namespace LuckDraw
{
    /// <summary>
    /// 抽奖活动基类
    /// </summary>
    public class LotteryBase : EventBase, ILottery
    {
        private MethodBase methodBase = System.Reflection.MethodBase.GetCurrentMethod();
        protected string ClassName = "抽奖";
        private Luck_ActivityBO luckActivityBO = new Luck_ActivityBO();
        private Luck_ActivityPrizeBO luckActivityPrizeBO = new Luck_ActivityPrizeBO();
        private Luck_ActivityJackpotBO luckActivityJackpotBO = new Luck_ActivityJackpotBO();
        protected bool result = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LotteryBase()
        { }

        /// <summary>
        /// 获取抽奖活动数据
        /// </summary>
        /// <returns></returns>
        public virtual LotteryModel GetLotteryActivity(MemberBaseModel member, int activity_id)
        {
            LotteryModel lotteryModel = new LotteryModel();

            OperationFilePath = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            OperationName = "获取" + ClassName + "数据";
            try
            {
                BaseEvent(EventEnum.OnBegin);

                #region 参数验证
                Luck_Activity model = VerifyLottery(activity_id);
                if (model == null)
                    return null;

                //获取活动奖品
                Expression<Func<Luck_ActivityPrize, bool>> where = PredicateExtensionses.True<Luck_ActivityPrize>();
                where = where.AndAlso(p => p.sortid == activity_id && p.Status == (int)StatusEnmu.Normal);
                List<Luck_ActivityPrize> lists = luckActivityPrizeBO.FindAll<int>(where, p => p.Position.Value, OrderByEnum.ASC.ToString());
                if (lists == null || lists.Count < 0)
                {
                    Description = BaseMessage = "加载抽奖活动奖品信息失败";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                #endregion

                // 获取总共的次数
                int lotteryNumber = 0;
                #region 通过查看抽奖记录表来设置中奖次数
                Expression<Func<Luck_ActivityJackpot, bool>> whereJackpot = PredicateExtensionses.True<Luck_ActivityJackpot>();
                whereJackpot = whereJackpot.AndAlso(p => p.out_id.Equals(member.out_id)).AndAlso(p => p.data_type.Equals(member.data_type));
                whereJackpot = whereJackpot.AndAlso(p => p.ActivityId == activity_id);
                if (model.Rules == (int)Luck_ActivityRules.EveryDay) {
                    DateTime startDate = DateTime.Now.Date;
                    DateTime endDate = DateTime.Now.Date.AddDays(1);
                    whereJackpot = whereJackpot.AndAlso(p => p.Updatetime.Value > startDate && p.Updatetime < endDate);
                }
                List<Luck_ActivityJackpot> lotteryNumberLists = luckActivityJackpotBO.FindAll<int>(whereJackpot);

                if (lotteryNumberLists == null)
                {
                    lotteryNumber = model.maxNum.Value;
                }
                else if (lotteryNumberLists.Count >= model.maxNum.Value)
                {
                    Description = BaseMessage = "您没有机会了";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                lotteryNumber = model.maxNum.Value - lotteryNumberLists.Count;
                #endregion

                lotteryModel.ActivityId = model.id;
                lotteryModel.ActivityName = model.Name;
                lotteryModel.LotteryNumber = lotteryNumber;
                lotteryModel.LuckActivityPrize = lists;

                result = true;
                BaseEvent(EventEnum.OnSuccess);
            }
            catch (Exception ex)
            {
                BaseMessage = MsgShowConfig.Exception;
                Description = ex.ToString();
                BaseEvent(EventEnum.OnException);
            }
            BaseEvent(EventEnum.OnCompelete);
            return lotteryModel;
        }

        /// <summary>
        /// 获取会员抽奖奖品数据
        /// </summary>
        /// <returns></returns>
        public virtual List<WinRecordModel> GetLotteryPrize(MemberBaseModel member, List<int> activityIdList)
        {
            List<WinRecordModel> lists = new List<WinRecordModel>();
            OperationFilePath = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            OperationName = "获取会员" + ClassName + "奖品数据";
            try
            {
                BaseEvent(EventEnum.OnBegin);

                Expression<Func<Luck_ActivityJackpot, bool>> whereJackpot = PredicateExtensionses.True<Luck_ActivityJackpot>();
                whereJackpot = whereJackpot.AndAlso(p => p.out_id.Equals(member.out_id)).AndAlso(p => p.data_type.Equals(member.data_type));
                whereJackpot = whereJackpot.AndAlso(p => activityIdList.Contains(p.ActivityId.Value));
                List<Luck_ActivityJackpot> luckActivityJackpotList = luckActivityJackpotBO.FindAll<DateTime>(whereJackpot, p => p.Updatetime.Value);
                if (luckActivityJackpotList == null || luckActivityJackpotList.Count < 1)
                {
                    result = true;
                    return null;
                }

                //获取奖品列表
                Expression<Func<Luck_ActivityPrize, bool>> wherePrize = PredicateExtensionses.True<Luck_ActivityPrize>();
                List<int> prizeIdList = luckActivityJackpotList.Select(p => p.PrizeId.Value).ToList();
                wherePrize = wherePrize.AndAlso(p => prizeIdList.Contains(p.id));
                List<Luck_ActivityPrize> luckActivityPrizeList = luckActivityPrizeBO.FindAll<int>(wherePrize);
                foreach (Luck_ActivityJackpot jackpotModel in luckActivityJackpotList)
                {
                    WinRecordModel winRecordModel = new WinRecordModel();
                    winRecordModel.data_type = jackpotModel.data_type;
                    winRecordModel.out_id = jackpotModel.out_id;
                    winRecordModel.mobile = "";
                    winRecordModel.nickname = "";
                    winRecordModel.Id = jackpotModel.id;
                    winRecordModel.PrizeId = jackpotModel.PrizeId.Value;
                    winRecordModel.ActivityId = jackpotModel.ActivityId.Value;
                    winRecordModel.Receivetime = jackpotModel.Updatetime.Value;

                    Luck_ActivityPrize luckActivityPrizeModel = luckActivityPrizeList.Where(n => n.id == jackpotModel.id).FirstOrDefault();
                    if (luckActivityPrizeModel != null)
                    {
                        winRecordModel.PrizeName = luckActivityPrizeModel.name;
                        winRecordModel.PrizePrice = luckActivityPrizeModel.price.Value;
                        winRecordModel.PrizeLevel = luckActivityPrizeModel.PrizeLevel;
                        winRecordModel.PrizeType = luckActivityPrizeModel.PrizeType.Value;
                        winRecordModel.PrizeImg = luckActivityPrizeModel.PrizeImg;
                        winRecordModel.Introduction = luckActivityPrizeModel.Introduction;
                    }
                    lists.Add(winRecordModel);
                }
                result = true;
                BaseEvent(EventEnum.OnSuccess);
            }
            catch (Exception ex)
            {
                BaseMessage = MsgShowConfig.Exception;
                Description = ex.ToString();
                BaseEvent(EventEnum.OnException);
            }
            BaseEvent(EventEnum.OnCompelete);
            return lists;
        }

        /// <summary>
        /// 绑定会员抽奖数据并返回奖品
        /// </summary>
        /// <returns></returns>
        public virtual Luck_ActivityPrize MemberBindLottery(MemberBaseModel member, int activity_id)
        {
            Luck_ActivityPrize luckActivityPrizeModel = new Luck_ActivityPrize();
             OperationFilePath = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            OperationName = "绑定会员" + ClassName + "数据";
            try
            {
                BaseEvent(EventEnum.OnBegin);

                #region 参数验证
                Luck_Activity model = VerifyLottery(activity_id);
                if (model == null)
                    return null;

                //抽奖记录表里判断是否可以抽奖
                Expression<Func<Luck_ActivityJackpot, bool>> whereJackpot = PredicateExtensionses.True<Luck_ActivityJackpot>();
                whereJackpot = whereJackpot.AndAlso(p => p.out_id.Equals(member.out_id)).AndAlso(p => p.data_type.Equals(member.data_type));
                whereJackpot = whereJackpot.AndAlso(p => p.ActivityId == activity_id);
                if (model.Rules == (int)Luck_ActivityRules.EveryDay)
                {
                    DateTime startDate = DateTime.Now.Date;
                    DateTime endDate = DateTime.Now.Date.AddDays(1);
                    whereJackpot = whereJackpot.AndAlso(p => p.Updatetime.Value > startDate && p.Updatetime < endDate);
                }
                List<Luck_ActivityJackpot> lotteryNumberLists = luckActivityJackpotBO.FindAll<int>(whereJackpot);

                if (lotteryNumberLists != null && lotteryNumberLists.Count > 0)
                {
                    if (lotteryNumberLists.Count >= model.maxNum)
                    {
                        Description = BaseMessage = "您没有机会了";
                        BaseEvent(EventEnum.OnTipMsg);
                        return null;
                    }
                }

                //获取奖池里面的奖品信息
                Expression<Func<Luck_ActivityJackpot, bool>> whereNoDrawJackpot = PredicateExtensionses.True<Luck_ActivityJackpot>();
                whereNoDrawJackpot = whereNoDrawJackpot.AndAlso(p => p.ActivityId == activity_id).AndAlso(p => p.Status == (int)LuckActivityJackpotStatus.NotDraw);
                Luck_ActivityJackpot noDrawJackpotModel = luckActivityJackpotBO.GetSingle<Guid>(whereNoDrawJackpot, x => Guid.NewGuid());
                if (noDrawJackpotModel == null || noDrawJackpotModel.id < 1)
                {
                    Description = BaseMessage = "来晚了,活动奖品已经领完";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                #endregion

                //更新奖池记录
                noDrawJackpotModel.Status = (int)LuckActivityJackpotStatus.AlreadyDraw;
                noDrawJackpotModel.data_type = member.data_type;
                noDrawJackpotModel.out_id = member.out_id;
                noDrawJackpotModel.Updatetime = DateTime.Now;
                noDrawJackpotModel.UpdateAddtime = DateTime.Now;
                noDrawJackpotModel.Ip = Utility.GetRealIp();
                luckActivityJackpotBO.Update(noDrawJackpotModel);

                luckActivityPrizeModel = luckActivityPrizeBO.Find(noDrawJackpotModel.PrizeId.Value);
                result = true;

                BaseEvent(EventEnum.OnSuccess);
            }
            catch (Exception ex)
            {
                BaseMessage = MsgShowConfig.Exception;
                Description = ex.ToString();
                BaseEvent(EventEnum.OnException);
            }
            BaseEvent(EventEnum.OnCompelete);
            return luckActivityPrizeModel;
        }

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <returns></returns>
        public virtual string GetMessage()
        {
            return BaseMessage;
        }

        /// <summary>
        /// 验证抽奖活动
        /// </summary>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        public Luck_Activity VerifyLottery(int activity_id)
        {
            Luck_Activity model = null;
            OperationFilePath = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            OperationName = "验证" + ClassName + "活动";
            try
            {
                BaseEvent(EventEnum.OnBegin);

                model = luckActivityBO.Find(activity_id);
                if (model == null || model.id < 1)
                {
                    Description = BaseMessage = "加载抽奖活动失败";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                if (model.Status == (int)StatusEnmu.Locking)
                {
                    Description = BaseMessage = "该活动已锁定";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                if (model.Startdate.Value >= DateTime.Now)
                {
                    Description = BaseMessage = "该活动未开始";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                if (model.Enddate.Value.AddDays(1) < DateTime.Now)
                {
                    Description = BaseMessage = "该活动已结束";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
            }
            catch (Exception ex)
            {
                BaseMessage = MsgShowConfig.Exception;
                Description = ex.ToString();
                BaseEvent(EventEnum.OnException);
            }
            BaseEvent(EventEnum.OnCompelete);
            return model;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns></returns>
        public bool GetResultState()
        {
            return result;
        }
    }
}
