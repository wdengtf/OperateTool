using Framework;
using Framework.Log;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;
using YYT.BLL;

namespace YYT.Api
{
    public class APICall
    {
        /// <summary>
        /// 调用接口入口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static JsonResult MainExcute<T>(T req, IOperation<T> operation) where T : BaseModel
        {
            JsonResult re = null;
            try
            {
                //LogService.LogInfo("原始数据：" + Utility.ToJson(req));
                //设置请求参数
                operation.Set(req);
                //验证数据
                bool result = operation.Validate();
                if (!result)
                {
                    string strErrMsg = operation.GetMessage();
                    //添加日志
                    QD_ChannelLog channelLogModel = new QD_ChannelLog();
                    channelLogModel.channelName = req.channelUser;
                    channelLogModel.@interface = Utility.LimitTitleLen("YYT.Api.APICall.MainExcute", 100, "");
                    channelLogModel.status = (int)StatusSFEnmu.Fail;
                    channelLogModel.failType = EventEnum.OnFail.ToString();
                    channelLogModel.failMessage = Utility.LimitTitleLen(strErrMsg, 50, "");
                    channelLogModel.RawData = Utility.ToJson(req);
                    channelLogModel.ip = Utility.GetRealIp();
                    channelLogModel.Createtime = DateTime.Now;
                    new QD_ChannelLogBO().Add(channelLogModel);

                    LogService.LogError(strErrMsg);
                    return JsonResult.FailResult(strErrMsg);
                }
                //执行操作
                result = operation.Excute();
                if (result)
                {
                    //执行正常
                    re = JsonResult.SuccessResult(operation.GetData());
                }
                else
                {
                    //执行出错
                    LogService.LogError(operation.GetMessage());
                    re = JsonResult.FailResult(operation.GetMessage());
                }
            }
            catch (Exception ex)
            {
                LogService.LogDebug(operation.GetMessage() + "," + ex.Message);
                re = JsonResult.FailResult(MsgShowConfig.Exception);
            }
            return re;
        }
    }
}
