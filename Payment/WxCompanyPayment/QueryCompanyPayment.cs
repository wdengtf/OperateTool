using Framework.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model.Pay;

namespace Payment.WxCompanyPayment
{
    /// <summary>
    /// 查询企业付款
    /// </summary>
    public class QueryCompanyPayment : PayBase<CompanyPaymentQueryModel>
    {
        public QueryCompanyPayment()
        { }

        public override bool Excute()
        {
            result = false;
            try
            {
                #region 创建签名
                SortedList<string, string> sortedList = WxConfig.GetSortedList<CompanyPaymentQueryModel>(req);
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
                    this.message = "调用微信企业付款接口返回数据为空";
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

                CompanyPaymentQueryNotifyModel queryNotifyModel = new CompanyPaymentQueryNotifyModel();
                queryNotifyModel.return_code = returnSortedList.ContainsKey("return_code") ? returnSortedList["return_code"] : "";
                queryNotifyModel.return_msg = returnSortedList.ContainsKey("return_msg") ? returnSortedList["return_msg"] : "";
                queryNotifyModel.result_code = returnSortedList.ContainsKey("result_code") ? returnSortedList["result_code"] : "";
                queryNotifyModel.err_code = returnSortedList.ContainsKey("err_code") ? returnSortedList["err_code"] : "";
                queryNotifyModel.err_code_des = returnSortedList.ContainsKey("err_code_des") ? returnSortedList["err_code_des"] : "";
                queryNotifyModel.partner_trade_no = returnSortedList.ContainsKey("partner_trade_no") ? returnSortedList["partner_trade_no"] : "";
                queryNotifyModel.mch_id = returnSortedList.ContainsKey("mch_id") ? returnSortedList["mch_id"] : "";
                queryNotifyModel.detail_id = returnSortedList.ContainsKey("detail_id") ? returnSortedList["detail_id"] : "";
                queryNotifyModel.status = returnSortedList.ContainsKey("status") ? returnSortedList["status"] : "";
                queryNotifyModel.reason = returnSortedList.ContainsKey("reason") ? returnSortedList["reason"] : "";
                queryNotifyModel.openid = returnSortedList.ContainsKey("openid") ? returnSortedList["openid"] : "";
                queryNotifyModel.transfer_name = returnSortedList.ContainsKey("transfer_name") ? returnSortedList["transfer_name"] : "";
                queryNotifyModel.payment_amount = returnSortedList.ContainsKey("payment_amount") ? int.Parse(returnSortedList["payment_amount"]) : 0;
                queryNotifyModel.transfer_time = returnSortedList.ContainsKey("transfer_time") ? returnSortedList["transfer_time"] : "";
                queryNotifyModel.desc = returnSortedList.ContainsKey("desc") ? returnSortedList["desc"] : "";
              
                this.data = queryNotifyModel;
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
                case "CA_ERROR":
                    strErrorMsg = "请求未携带证书，或请求携带的证书出错";
                    break;
                case "SIGN_ERROR":
                    strErrorMsg = "商户签名错误";
                    break;
                case "NO_AUTH":
                    strErrorMsg = "没有权限";
                    break;
                case "NOT_FOUND":
                    strErrorMsg = "指定单号数据不存在";
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
                case "SYSTEMERROR":
                    strErrorMsg = "系统繁忙，请再试";
                    break;
                default:
                    strErrorMsg = strErrorCode;
                    break;
            }
            return strErrorMsg;
        }
    }
}
