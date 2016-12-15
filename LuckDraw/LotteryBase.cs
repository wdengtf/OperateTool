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
                Luck_Activity model = luckActivityBO.Find(activity_id);
                if (model == null || model.Id < 1)
                {
                    Description = BaseMessage = "加载抽奖活动失败";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }

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

                //组装12个奖品
                int maxCount = 12;
                while (lists.Count < maxCount)
                {
                    lists.AddRange(lists);
                }
                lists = lists.Take(maxCount).ToList();

                // 获取总共的次数
                int lotteryNumber = 0;
                #region 通过查看抽奖记录表来设置中奖次数
                string strWhere = String.Format("out_id='{0}' and data_type='{1}' and win_activity_id={2}", member.out_id, member.data_type, model.Id);
                if (model.Frequency == (int)ACT_LotteryFrequency.EveryDay)
                    strWhere += " and DATEDIFF(DAY,createtime,GETDATE())=0";

                Expression<Func<Luck_ActivityJackpot, bool>> whereJackpot = PredicateExtensionses.True<Luck_ActivityJackpot>();
                whereJackpot = whereJackpot.AndAlso(p => p.out_id.Equals(member.out_id)).AndAlso(p=>p.data_type.Equals(member.data_type));
                whereJackpot = whereJackpot.AndAlso(p => p.ActivityId == activity_id);
                if (model.Rules == (int)Luck_ActivityRules.EveryDay)
                    whereJackpot = whereJackpot.AndAlso(p => p.Updatetime.Value.Date == DateTime.Now.Date);
                List<Luck_ActivityJackpot> lotteryNumberLists = luckActivityJackpotBO.FindAll<int>(whereJackpot);

                if (lotteryNumberLists == null)
                {
                    lotteryNumber = model.MaxNum;
                }
                else if (lotteryNumberLists.Count >= model.MaxNum)
                {
                    Description = BaseMessage = "您今天没有机会了";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                lotteryNumber = model.MaxNum - lotteryNumberLists.Count;
                #endregion


                lotteryModel.ActivityId = model.Id;
                lotteryModel.ActivityName = model.Name;
                lotteryModel.LotteryNumber = lotteryNumber;
                lotteryModel.LuckActivityPrize = lists;

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
        public virtual List<WinRecordModel> GetLotteryPrize(MemberBaseModel member, int activity_id)
        {
            List<WinRecordModel> lists = new List<WinRecordModel>();
            OperationFilePath = LogName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            OperationName = "获取会员" + ClassName + "奖品数据";
            try
            {
                BaseEvent(EventEnum.OnBegin);

                Dictionary<string, string> Parm = new Dictionary<string, string>();
                Parm.Add("activity_id", activity_id.ToString());
                Parm.Add("data_type", member.data_type);
                Parm.Add("out_id", member.out_id);

                #region 参数验证
                jsonResult re_Verify = new ParmVerify().NotEmptyVerify(Parm);
                if (re_Verify.Result != Result.success)
                {
                    Description = BaseMessage = "参数非空验证";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }

                ACT_LotteryModel model = luckActivityBO.GetModel(activity_id);
                if (model == null || model.Id < 1)
                {
                    Description = BaseMessage = "加载抽奖活动失败";
                    BaseEvent(EventEnum.OnTipMsg);
                    return null;
                }
                #endregion

                string strWhere = string.Format(" data_type='{0}' and out_id='{1}' and win_activity_id={2} order by  createtime desc", member.data_type, member.out_id, activity_id);
                List<ACT_Win_RecordModel> winLists = ACT_Win_RecordBus.Lists(10, strWhere);
                if (winLists == null || winLists.Count < 1)
                    return null;


                foreach (ACT_Win_RecordModel winModel in winLists)
                {
                    WinRecordModel reModel = new WinRecordModel();
                    reModel.Id = winModel.Id;
                    reModel.Win_name = winModel.Win_name;
                    reModel.Createtime = winModel.Createtime;
                    reModel.Data_type = winModel.Data_type;
                    reModel.Out_id = winModel.Out_id;

                    ACT_Lottery_PrizeModel prizeModel = luckActivityPrizeBO.GetModel(winModel.Award_id);
                    if (prizeModel != null)
                    {
                        reModel.Name = prizeModel.Name;
                        reModel.Win_img = prizeModel.Win_img;
                        reModel.Win_explain = prizeModel.Win_explain;
                    }
                    lists.Add(reModel);
                }

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
        /// 绑定会员抽奖数据
        /// </summary>
        /// <returns></returns>
        public virtual int MemberBindLottery(MemberBaseModel member, int activity_id, string win_name, string win_phone)
        {
            int winIndex = -1;
            OperationFilePath = LogName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            OperationName = "绑定会员" + ClassName + "数据";
            try
            {
                BaseEvent(EventEnum.OnBegin);

                Dictionary<string, string> Parm = new Dictionary<string, string>();
                Parm.Add("activity_id", activity_id.ToString());
                Parm.Add("data_type", member.data_type);
                Parm.Add("out_id", member.out_id);

                #region 参数验证
                jsonResult re_Verify = new ParmVerify().NotEmptyVerify(Parm);
                if (re_Verify.Result != Result.success)
                {
                    Description = BaseMessage = "参数非空验证";
                    BaseEvent(EventEnum.OnTipMsg);
                    return winIndex;
                }

                ACT_LotteryModel model = luckActivityBO.GetModel(activity_id);
                if (model == null || model.Id < 1)
                {
                    Description = BaseMessage = "加载抽奖活动失败";
                    BaseEvent(EventEnum.OnTipMsg);
                    return winIndex;
                }

                //抽奖记录表里判断是否可以抽奖
                string strWhere = String.Format("out_id='{0}' and data_type='{1}' and win_activity_id={2}", member.out_id, member.data_type, activity_id);
                if (model.Frequency == (int)ACT_LotteryFrequency.EveryDay)
                    strWhere += " and DATEDIFF(DAY,createtime,GETDATE())=0 ";

                List<ACT_Lottery_RecordModel> LotteryNumberLists = luckActivityJackpotBO.Lists(0, strWhere);
                if (LotteryNumberLists != null && LotteryNumberLists.Count > 0)
                {
                    if (LotteryNumberLists.Count >= model.MaxNum)
                    {
                        Description = BaseMessage = "您今天没有机会了";
                        BaseEvent(EventEnum.OnTipMsg);
                        return winIndex;
                    }
                }

                //获取奖池里面的奖品信息
                string WinWhere = String.Format(" win_activity_id={0} and win_status={1} order by NEWID();", model.Id, (int)ACT_JackpotStatus.NotDraw);
                ACT_JackpotModel WinModel = ACT_JackpotBus.Lists(1, WinWhere).FirstOrDefault();
                if (WinModel == null || WinModel.Id < 1)
                {
                    Description = BaseMessage = "来晚了,活动奖品已经领完";
                    BaseEvent(EventEnum.OnTipMsg);
                    return winIndex;
                }

                //奖品活动对象
                ACT_Lottery_PrizeModel award = luckActivityPrizeBO.GetModel(WinModel.Award_id);
                if (award == null || award.Id < 1 || award.Status == (int)ACT_Lottery_PrizeState.Locking)
                {
                    Description = BaseMessage = "活动奖品不存在或已被锁定";
                    BaseEvent(EventEnum.OnTipMsg);
                    return winIndex;
                }
                #endregion

                List<CommandInfo> UpDateList = new List<CommandInfo>();
                //更新奖池记录
                WinModel.Win_status = (int)ACT_JackpotStatus.AlreadyDraw;
                WinModel.Data_type = member.data_type;
                WinModel.Out_id = member.out_id;
                UpDateList.Add(ACT_JackpotBus.UpDateSql(WinModel));

                //添加中奖记录
                ACT_Win_RecordModel winRecord = new ACT_Win_RecordModel();
                winRecord.Win_activity_id = WinModel.Win_activity_id;
                winRecord.Award_id = WinModel.Award_id;
                winRecord.Award_name = award.Name;
                winRecord.Win_name = win_name + win_phone;
                winRecord.Ip = Utility.GetRealIp();
                winRecord.Createtime = DateTime.Now;
                winRecord.Status = (int)ACT_Win_RecordStatus.NoAward;
                winRecord.Data_type = member.data_type;
                winRecord.Out_id = member.out_id;
                UpDateList.Add(ACT_Win_RecordBus.AddSql(winRecord));

                //添加抽奖记录
                ACT_Lottery_RecordModel record = new ACT_Lottery_RecordModel();
                record.Out_id = member.out_id;
                record.Data_type = member.data_type;
                record.Ip = Utility.GetRealIp();
                record.Createtime = DateTime.Now;
                record.Win_activity_id = WinModel.Win_activity_id;
                record.Win_name = win_name + win_phone;
                UpDateList.Add(luckActivityJackpotBO.AddSql(record));

                int executeResult = SqlBll.ExecuteSqlTran(UpDateList);

                //更新优惠券
                #region 绑定优惠券
                if (executeResult > 0 && award.CouponsSortId > 0)
                {
                    jsonResult re_coupons = new Coupons.CouponsCall().MemberBindCounpons(member, award.CouponsSortId);
                    if (re_coupons.Result != Result.success)
                    {
                        LogService.logFatal(re_coupons.Message);
                    }
                }
                #endregion

                //返回抽奖记录id 余11是为了去除索引小于11的数字，因为奖品是从0-11开始的
                winIndex = award.Win_index % 11;
                BaseEvent(EventEnum.OnSuccess);
            }
            catch (Exception ex)
            {
                BaseMessage = MsgShowConfig.Exception;
                Description = ex.ToString();
                BaseEvent(EventEnum.OnException);
            }
            BaseEvent(EventEnum.OnCompelete);
            return winIndex;
        }


        /// <summary>
        /// 提示消息
        /// </summary>
        /// <returns></returns>
        public virtual string GetMessage()
        {
            return BaseMessage;
        }
    }
}
