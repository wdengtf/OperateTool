using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Act.LuckDraw;
using Auth;
using YYT.Model.Auth;
using YYT.Model;

namespace YYT.Api
{
    public class ApiFactory
    {
        public ApiFactory()
        { }

        #region 抽奖活动接口
        ///// <summary>
        ///// 获取抽奖活动数据
        ///// </summary>
        ///// <returns></returns>
        //public static IOperation<ReqLotteryActivityModel> GetLotteryActivity()
        //{
        //    return new LuckDraw.GetLotteryActivity();
        //}

        ///// <summary>
        ///// 获取会员抽奖奖品数据
        ///// </summary>
        ///// <returns></returns>
        //public static IOperation<ReqLotteryPrizeModel> GetLotteryPrize()
        //{
        //    return new LuckDraw.GetLotteryPrize();
        //}

        ///// <summary>
        ///// 绑定会员抽奖数据
        ///// </summary>
        ///// <returns></returns>
        //public static IOperation<ReqLotteryActivityModel> MemberBindLottery()
        //{
        //    return new LuckDraw.MemberBindLottery();
        //}
        #endregion

        #region 悦园数据接口
        //public static IOperation<IdcardModel> SendIdCard()
        //{
        //    return new Ayx.IdCard.SendIdCard();
        //}
        #endregion

        #region 用户授权 外部可直接调用 未验证签名
        /// <summary>
        /// 微信公众号授权
        /// </summary>
        /// <returns></returns>
        public static IBase<WxServerAuthModel, ServerTokenAndTicketModel> WxServerAuth(WxServerAuthModel wxServerAuthModel)
        {
            return new AuthCall<WxServerAuthModel, ServerTokenAndTicketModel>();
        }

        /// <summary>
        /// 微信网页授权
        /// </summary>
        /// <param name="wxWebAuthModel"></param>
        /// <returns></returns>
        public static IBase<WxWebAuthModel, WxMemberModel> WxWebAuth(WxWebAuthModel wxWebAuthModel)
        {
            return new AuthCall<WxWebAuthModel, WxMemberModel>();
        }
        #endregion
    }
}
