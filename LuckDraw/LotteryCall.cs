﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Log;
using YYT.Model;
using Events;
using YYT.Model.LuckDraw;

namespace Act.LuckDraw
{
    /// <summary>
    /// 外部调用类
    /// </summary>
    public class LotteryCall
    {
        private readonly ILottery lottery = null;
        //private readonly IEvent events = new MsmqEvents();
        private readonly IEvent events = new Log4NetEvents();

        /// <summary>
        /// 构造函数
        /// </summary>
        public LotteryCall()
        {
            lottery = Factory.Lottery();
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="className">类名</param>
        public LotteryCall(string className)
        {
            lottery = Factory.Lottery(className);
        }

        /// <summary>
        /// 获取抽奖活动数据
        /// </summary>
        /// <param name="member"></param>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        public LotteryModel GetLotteryActivity(MemberBaseModel member, int activity_id, string channelUser)
        {
            LotteryModel lotteryModel = new LotteryModel();
            try
            {
                DeleteEvent();
                AddEvent();

                lotteryModel = lottery.GetLotteryActivity(member, activity_id, channelUser);
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return lotteryModel;
        }

        /// <summary>
        /// 绑定会员抽奖数据
        /// </summary>
        /// <param name="member"></param>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        public Luck_ActivityPrize MemberBindLottery(MemberBaseModel member, int activity_id, string channelUser)
        {
            Luck_ActivityPrize model = null;
            try
            {
                DeleteEvent();
                AddEvent();

                model = lottery.MemberBindLottery(member, activity_id, channelUser);
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return model;
        }


        /// <summary>
        /// 获取会员抽奖奖品数据
        /// </summary>
        /// <param name="member"></param>
        /// <param name="activityIdList"></param>
        /// <returns></returns>
        public List<WinRecordModel> GetLotteryPrize(MemberBaseModel member, List<int> activityIdList, string channelUser)
        {
            List<WinRecordModel> winRecordList = new List<WinRecordModel>();
            try
            {
                DeleteEvent();
                AddEvent();

                winRecordList = lottery.GetLotteryPrize(member, activityIdList, channelUser);
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return winRecordList;
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            string message = "";
            try
            {
                message = lottery.GetMessage();
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return message;
        }

        /// <summary>
        /// 返回执行结果状态
        /// </summary>
        /// <returns></returns>
        public bool GetResultState()
        {
            bool result = false;
            try
            {
                result = lottery.GetResultState();
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return result;
        }

        #region 事件操作
        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvent()
        {
            lottery.OnBegin += new EventHandler(events.OnBegin);
            lottery.OnTipMsg += new EventHandler(events.OnTipMsg);
            lottery.OnSuccess += new EventHandler(events.OnSuccess);
            lottery.OnFail += new EventHandler(events.OnFail);
            lottery.OnException += new EventHandler(events.OnException);
            lottery.OnCompelete += new EventHandler(events.OnCompelete);
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        private void DeleteEvent()
        {
            lottery.OnBegin -= new EventHandler(events.OnBegin);
            lottery.OnTipMsg -= new EventHandler(events.OnTipMsg);
            lottery.OnException -= new EventHandler(events.OnException);
            lottery.OnSuccess -= new EventHandler(events.OnSuccess);
            lottery.OnFail -= new EventHandler(events.OnFail);
            lottery.OnCompelete -= new EventHandler(events.OnCompelete);
        }
        #endregion
    }
}
