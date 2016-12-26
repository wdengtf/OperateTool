using Payment.PayModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using Framework.Log;

namespace Payment.WxRedPacket
{
    /// <summary>
    /// 发放红包
    /// </summary>
    public class SendRedPacket : PayBase<RedPacketModel>
    {
        public SendRedPacket()
        { }

        public override void Excute()
        {
            try
            {
                #region 创建签名
                SortedList<string, string> sortedList = WxConfig.GetSortedList<RedPacketModel>(req);
                string strSortList = WxConfig.GetSortStr(sortedList);
                strSortList += "&key=" + req.key;
                string sign = Framework.Utils.SignUtil.MD5Hash(strSortList).ToUpper();
                sortedList.Add("sign", sign);
                req.sign = sign;
                #endregion

                if (!Validate()) return;

                string retrunPost = new Framework.Utils.WebUtils().DoPostWebRequest(req.postUrl, WxConfig.GetXmlStr(sortedList), Encoding.UTF8);
                LogService.LogInfo("微信发放红包返回参数：" + retrunPost);

                if (String.IsNullOrEmpty(retrunPost))
                {
                    this.message = "微信发放红包返回参数接口返回数据为空";
                    return;
                }

                SortedList<string, string> returnSortedList = WxConfig.XmlTransSortedList(retrunPost);

                #region 返回参数生成签名
                string returnStrSortedList = WxConfig.GetSortStr(returnSortedList);
                returnStrSortedList += "&key=" + req.key;
                string returnSign = Framework.Utils.SignUtil.MD5Hash(returnStrSortedList).ToUpper();
                #endregion


                if (!returnSortedList.ContainsKey("return_code"))
                {
                    this.message = "未获取到微信红包返回状态码";
                    return;
                }
                if (returnSortedList["return_code"] != WxConfig.returnSuccessCode)
                {
                    this.message = returnSortedList["return_msg"];
                    return;
                }
                if (returnSortedList["result_code"] != WxConfig.returnSuccessCode)
                {
                    this.message = "业务结果错误,代码:[" + returnSortedList["err_code"] + "],错误代码描述:" + returnSortedList["err_code_des"];
                    return;
                }

                if (returnSortedList["sign"] != returnSign)
                {
                    this.message = "返回参数签名错误";
                    return;
                }

                RedPacketNotifyModel redPacketNotifyModel = new RedPacketNotifyModel();
                redPacketNotifyModel.return_code = returnSortedList.ContainsKey("return_code") ? returnSortedList["return_code"] : "";
                redPacketNotifyModel.return_msg = returnSortedList.ContainsKey("return_msg") ? returnSortedList["return_msg"] : "";
                redPacketNotifyModel.sign = returnSortedList.ContainsKey("sign") ? returnSortedList["sign"] : "";
                redPacketNotifyModel.result_code = returnSortedList.ContainsKey("result_code") ? returnSortedList["result_code"] : "";
                redPacketNotifyModel.err_code = returnSortedList.ContainsKey("err_code") ? returnSortedList["err_code"] : "";
                redPacketNotifyModel.err_code_des = returnSortedList.ContainsKey("err_code_des") ? returnSortedList["err_code_des"] : "";
                redPacketNotifyModel.mch_billno = returnSortedList.ContainsKey("mch_billno") ? returnSortedList["mch_billno"] : "";
                redPacketNotifyModel.mch_id = returnSortedList.ContainsKey("mch_id") ? returnSortedList["mch_id"] : "";
                redPacketNotifyModel.wxappid = returnSortedList.ContainsKey("wxappid") ? returnSortedList["wxappid"] : "";
                redPacketNotifyModel.re_openid = returnSortedList.ContainsKey("re_openid") ? returnSortedList["re_openid"] : "";
                redPacketNotifyModel.total_amount = returnSortedList.ContainsKey("total_amount") ? int.Parse(returnSortedList["total_amount"]) : 0;
                redPacketNotifyModel.send_listid = returnSortedList.ContainsKey("send_listid") ? returnSortedList["send_listid"] : "";

                this.data = redPacketNotifyModel;
                this.result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return;
        }


        /// <summary>
        /// 调用接口错误提示
        /// </summary>
        /// <param name="strErrorCode"></param>
        /// <returns></returns>
        private string GetErrorStr(string strErrorCode)
        {
            string strErrorMsg = "";
            switch (strErrorCode)
            {
                case "NO_AUTH":
                    strErrorMsg = "发放失败，此请求可能存在风险，已被微信拦截";
                    break;
                case "SENDNUM_LIMIT":
                    strErrorMsg = "该用户今日领取红包个数超过限制";
                    break;
                case "CA_ERROR":
                    strErrorMsg = "请求未携带证书，或请求携带的证书出错";
                    break;
                case "ILLEGAL_APPID":
                    strErrorMsg = "错误传入了app的appid";
                    break;
                case "SIGN_ERROR":
                    strErrorMsg = "商户签名错误";
                    break;
                case "FREQ_LIMIT":
                    strErrorMsg = "受频率限制";
                    break;
                case "XML_ERROR":
                    strErrorMsg = "请求的xml格式错误，或者post的数据为空";
                    break;
                case "PARAM_ERROR":
                    strErrorMsg = "参数错误";
                    break;
                case "OPENID_ERROR":
                    strErrorMsg = "Openid错误";
                    break;
                case "NOTENOUGH":
                    strErrorMsg = "余额不足";
                    break;
                case "FATAL_ERROR":
                    strErrorMsg = "重复请求时，参数与原单不一致";
                    break;
                case "SECOND_OVER_LIMITED":
                    strErrorMsg = "企业红包的按分钟发放受限";
                    break;
                case "DAY_ OVER_LIMITED":
                    strErrorMsg = "企业红包的按天日发放受限";
                    break;
                case "MONEY_LIMIT":
                    strErrorMsg = "红包金额发放限制";
                    break;
                case "SEND_FAILED":
                    strErrorMsg = "红包发放失败,请更换单号再重试";
                    break;
                case "SYSTEMERROR":
                    strErrorMsg = "系统繁忙，请再试";
                    break;
                case "PROCESSING":
                    strErrorMsg = "请求已受理，请稍后使用原单号查询发放结果";
                    break;
                default:
                    strErrorMsg = strErrorCode;
                    break;
            }
            return strErrorMsg;
        }

    }
}
