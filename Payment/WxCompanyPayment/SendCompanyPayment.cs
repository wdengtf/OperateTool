using Framework.Log;
using Payment.PayModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.WxCompanyPayment
{
    /// <summary>
    /// 企业付款
    /// </summary>
    public class SendCompanyPayment : PayBase<CompanyPaymentModel>
    {
        public SendCompanyPayment()
        { }

        public override bool Excute()
        {
            result = false;
            try
            {
                #region 创建签名
                SortedList<string, string> sortedList = WxConfig.GetSortedList<CompanyPaymentModel>(req);
                string strSortList = WxConfig.GetSortStr(sortedList);
                strSortList += "&key=" + req.key;
                string sign = Framework.Utils.SignUtil.MD5Hash(strSortList).ToUpper();
                sortedList.Add("sign", sign);
                req.sign = sign;
                #endregion

                if (!Validate()) return result;

                string retrunPost = new Framework.Utils.WebUtils().DoPostWebRequest(req.postUrl, WxConfig.GetXmlStr(sortedList), Encoding.UTF8);
                LogService.LogInfo("微信企业付款返回参数：" + retrunPost);

                if (String.IsNullOrEmpty(retrunPost))
                {
                    this.message = "微信企业付款返回参数接口返回数据为空";
                    return result;
                }

                SortedList<string, string> returnSortedList = WxConfig.XmlTransSortedList(retrunPost);

                #region 返回参数生成签名
                string returnStrSortedList = WxConfig.GetSortStr(returnSortedList);
                returnStrSortedList += "&key=" + req.key;
                string returnSign = Framework.Utils.SignUtil.MD5Hash(returnStrSortedList).ToUpper();
                #endregion


                if (!returnSortedList.ContainsKey("return_code"))
                {
                    this.message = "未获取到企业付款返回状态码";
                    return result;
                }
                if (returnSortedList["return_code"] != WxConfig.returnSuccessCode)
                {
                    this.message = returnSortedList["return_msg"];
                    return result;
                }
                if (returnSortedList["result_code"] != WxConfig.returnSuccessCode)
                {
                    this.message = "业务结果错误,代码:[" + returnSortedList["err_code"] + "],错误代码描述:" + returnSortedList["err_code_des"];
                    return result;
                }

                if (returnSortedList["sign"] != returnSign)
                {
                    this.message = "返回参数签名错误";
                    return result;
                }

                CompanyPaymentNotifyModel companyPaymentNotifyModel = new CompanyPaymentNotifyModel();
                companyPaymentNotifyModel.return_code = returnSortedList.ContainsKey("return_code") ? returnSortedList["return_code"] : "";
                companyPaymentNotifyModel.return_msg = returnSortedList.ContainsKey("return_msg") ? returnSortedList["return_msg"] : "";
                companyPaymentNotifyModel.mch_appid = returnSortedList.ContainsKey("mch_appid") ? returnSortedList["mch_appid"] : "";
                companyPaymentNotifyModel.mchid = returnSortedList.ContainsKey("mchid") ? returnSortedList["mchid"] : "";
                companyPaymentNotifyModel.device_info = returnSortedList.ContainsKey("device_info") ? returnSortedList["device_info"] : "";
                companyPaymentNotifyModel.nonce_str = returnSortedList.ContainsKey("nonce_str") ? returnSortedList["nonce_str"] : "";
                companyPaymentNotifyModel.result_code = returnSortedList.ContainsKey("result_code") ? returnSortedList["result_code"] : "";
                companyPaymentNotifyModel.err_code = returnSortedList.ContainsKey("err_code") ? returnSortedList["err_code"] : "";
                companyPaymentNotifyModel.err_code_des = returnSortedList.ContainsKey("err_code_des") ? returnSortedList["err_code_des"] : "";
                companyPaymentNotifyModel.partner_trade_no = returnSortedList.ContainsKey("partner_trade_no") ? returnSortedList["partner_trade_no"] : "";
                companyPaymentNotifyModel.payment_no = returnSortedList.ContainsKey("payment_no") ? returnSortedList["payment_no"] : "";
                companyPaymentNotifyModel.payment_time = returnSortedList.ContainsKey("payment_time") ? returnSortedList["payment_time"] : "";

                this.data = companyPaymentNotifyModel;
                this.result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return result;
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
                case "NOAUTH":
                    strErrorMsg = "没有权限";
                    break;
                case "AMOUNT_LIMIT":
                    strErrorMsg = "付款金额不能小于最低限额";
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
                case "SYSTEMERROR":
                    strErrorMsg = "系统繁忙，请再试";
                    break;
                case "NAME_MISMATCH":
                    strErrorMsg = "姓名校验出错";
                    break;
                case "SIGN_ERROR":
                    strErrorMsg = "商户签名错误";
                    break;
                case "XML_ERROR":
                    strErrorMsg = "请求的xml格式错误，或者post的数据为空";
                    break;
                case "FATAL_ERROR":
                    strErrorMsg = "重复请求时，参数与原单不一致";
                    break;
                case "CA_ERROR":
                    strErrorMsg = "证书出错";
                    break;
                default:
                    strErrorMsg = strErrorCode;
                    break;
            }
            return strErrorMsg;
        }
    }
}
