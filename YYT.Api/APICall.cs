using Framework;
using Framework.Log;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static JsonResult MainExcute<T>(T req, IOperation<T> operation) where T : BaseApiModel
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
                    LogService.LogError(operation.GetMessage());
                    return JsonResult.FailResult(operation.GetMessage());
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
                    re = JsonResult.FailResult(operation.GetMessage());
                    LogService.LogError(operation.GetMessage());
                }
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(operation.GetMessage() + "," + ex.Message);
            }
            return re;
        }
    }
}
