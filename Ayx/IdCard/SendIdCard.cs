using Ayx.Model;
using Framework.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using Framework.Model;

namespace Ayx.IdCard
{
    /// <summary>
    /// 身份证验证
    /// </summary>
    public class SendIdCard : AyxBase<IdcardModel>
    {
        private string ClassName = "悦园身份证实名认证";
        public SendIdCard()
        { }

        public override bool Excute()
        {
            OperationUserName = req.channelUser;
            OperationFilePath = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            OperationName = "获取" + ClassName + "数据";

            try
            {
                BaseEvent(EventEnum.OnBegin);

                if (!Validate())
                {
                    Description = BaseMessage;
                    BaseEvent(EventEnum.OnTipMsg);
                    return false;
                }

                string retrunPost = new Framework.Utils.WebUtils().DoPost(req.postUrl, AyxConfig.GetSortedList(req));
                LogService.LogInfo(ClassName + "返回参数：" + retrunPost);

                if (String.IsNullOrEmpty(retrunPost))
                {
                    Description = BaseMessage = ClassName + "返回数据为空";
                    BaseEvent(EventEnum.OnTipMsg);
                    return false;
                }

                IdcardNotifyModel idcardNotifyModel = Utility.JsonToObject<IdcardNotifyModel>(retrunPost);
                this.data = idcardNotifyModel;
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
            return result;
        }

        /// <summary>
        /// 调用接口错误提示
        /// </summary>
        /// <param name="strErrorCode"></param>
        /// <returns></returns>
        private string GetErrorStr(int strErrorCode)
        {
            string strErrorMsg = "";
            switch (strErrorCode)
            {
                case 11:
                    strErrorMsg = "参数不正确";
                    break;
                case 12:
                    strErrorMsg = "商户余额不足";
                    break;
                case 13:
                    strErrorMsg = "appkey不存在";
                    break;
                case 14:
                    strErrorMsg = "IP被拒绝";
                    break;
                case 20:
                    strErrorMsg = "身份证中心维护中";
                    break;
            }
            return strErrorMsg;
        }
    }
}
