using Framework;
using Framework.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;
using YYT.BLL;
using System.Linq.Expressions;
using Framework.EF;
using Framework.Handle;
using Framework.Validate;

namespace Web.Api.Base
{
    public class BaseApi
    {
        public BaseApi()
        { }

        /// <summary>
        /// 调用接口入口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static JsonResult MainExcute<T, M>(T req, IBase<T, M> iBase)
            where T : BaseModel
            where M : class
        {
            JsonResult re = null;
            try
            {
                //LogService.LogInfo("原始数据：" + Utility.ToJson(req));
                //设置请求参数
                iBase.Set(req);
                //验证数据
                string strValidateMsg = Validate<T>(req);
                if (!String.IsNullOrWhiteSpace(strValidateMsg))
                {
                    //添加日志
                    QD_ChannelLog channelLogModel = new QD_ChannelLog();
                    channelLogModel.channelName = req.channelUser;
                    channelLogModel.@interface = Utility.LimitTitleLen("YYT.Api.APICall.MainExcute", 100, "");
                    channelLogModel.status = (int)StatusSFEnmu.Fail;
                    channelLogModel.failType = EventEnum.OnFail.ToString();
                    channelLogModel.failMessage = Utility.LimitTitleLen(strValidateMsg, 50, "");
                    channelLogModel.RawData = Utility.ToJson(req);
                    channelLogModel.ip = Utility.GetRealIp();
                    channelLogModel.Createtime = DateTime.Now;
                    new QD_ChannelLogBO().Add(channelLogModel);

                    LogService.LogError(strValidateMsg);
                    return JsonResult.FailResult(strValidateMsg);
                }
                //执行操作
                M m = iBase.Excute();
                bool result = iBase.GetResultState();
                if (result)
                {
                    //执行正常
                    re = JsonResult.SuccessResult(m);
                }
                else
                {
                    //执行出错
                    LogService.LogError(iBase.GetMessage());
                    re = JsonResult.FailResult(iBase.GetMessage());
                }
            }
            catch (Exception ex)
            {
                LogService.LogDebug(iBase.GetMessage() + "," + ex.Message);
                re = JsonResult.FailResult(MsgShowConfig.Exception);
            }
            return re;
        }


        #region 私有方法
        /// <summary>
        /// 参数验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        private static string Validate<T>(T req) where T : BaseModel
        {
            try
            {
                #region 非空验证
                if (req == null)
                {
                    return MsgShowConfig.ParmNotEmpty;
                }
                var listError = req.IsValid();
                if (listError.Count > 0)
                {
                    return listError[0].Message;
                }
                #endregion

                #region 签名认证
                Expression<Func<QD_ChannelUser, bool>> where = PredicateExtensionses.True<QD_ChannelUser>();
                where = where.AndAlso(p => p.user_name.Equals(req.channelUser));
                QD_ChannelUser channelUserModel = new QD_ChannelUserBO().GetSingle<int>(where);
                if (channelUserModel == null)
                {
                    return "授权用户不存在";
                }
                if (channelUserModel.Status == (int)StatusEnmu.Locking)
                {
                    return "用户已锁定,请和管理员联系";
                }
                if (channelUserModel.end_time.Value <= DateTime.Now)
                {
                    return "用户账号已过期,请和管理员联系";
                }
                IDictionary<string, string> dic;
                try
                {
                    dic = ConvertObj.ModelToIDictionary(req);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                string sign = Framework.Utils.SignUtil.CreateSign(dic, channelUserModel.user_key);
                if (sign != req.channelSign)
                {
                    LogService.LogError("签名错误:" + Utility.ToJson(req));
                    return "签名错误";
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return "";
        }
        #endregion
    }
}