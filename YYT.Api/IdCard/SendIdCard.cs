﻿using Ayx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYT.Api.IdCard
{
    public class SendIdCard : OperationBase<IdcardModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SendIdCard()
        {
           
        }
        public override bool Excute()
        {
            result = false;
            //LotteryCall call = new LotteryCall();
            //MemberBaseModel user = new MemberBaseModel
            //{
            //    data_type = req.data_type,
            //    out_id = req.out_id,
            //    mobile = req.mobile,
            //    nickname = req.nickname,
            //};
            //LotteryModel lotteryModel = call.GetLotteryActivity(user, req.activity_id, req.channelUser);
            //if (call.GetResultState())
            //{
            //    this.result = true;
            //    this.date = lotteryModel;
            //}
            //else
            //{
            //    this.result = false;
            //    this.message = call.GetMessage();
            //}
            return result;
        }
    }
}
