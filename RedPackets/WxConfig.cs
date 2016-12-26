using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Framework;

namespace Wx.RedPackets
{
    public class WxConfig
    {
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            Random random = new Random();
            return GetMD5(random.Next(1000).ToString(), "GBK");
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 将对象转换成SortedList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SortedList<string, string> GetSortedList<T>(T model)
        {
            SortedList<string, string> sortedList = new SortedList<string, string>();
            try
            {
                Type type = typeof(T);
                foreach (PropertyInfo property in type.GetProperties())
                {
                    if (property.Name != "sign")
                    {
                        string name = property.Name;
                        if (property.PropertyType.Name == "List`1")
                        {
                            string value = Utility.ToJson(property.GetValue(model));
                            sortedList.Add(name, value);
                        }
                        else if (property.GetValue(model) != null)
                        {
                            string value = property.GetValue(model).ToString();
                            sortedList.Add(name, value);
                        }
                        else
                        {
                            sortedList.Add(name, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return sortedList;
        }

        /// <summary>
        /// 获取排序后的字符串
        /// </summary>
        /// <param name="sortedList"></param>
        /// <returns></returns>
        public static string GetSortStr(SortedList<string, string> sortedList)
        {
            StringBuilder sortStr = new StringBuilder(255);
            try
            {
                if (sortedList == null || sortedList.Count < 1)
                    return "";

                foreach (var item in sortedList)
                {
                    if (String.IsNullOrEmpty(item.Value) || item.Key.Equals("sign"))
                        continue;

                    if (String.IsNullOrEmpty(sortStr.ToString()))
                        sortStr.Append(item.Key + "=" + item.Value);
                    else
                        sortStr.Append("&" + item.Key + "=" + item.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return sortStr.ToString();
        }

        /// <summary>
        /// 生成XML
        /// </summary>
        /// <param name="sortedList"></param>
        /// <returns></returns>
        public static string GetXmlStr(SortedList<string, string> sortedList)
        {
            StringBuilder strXml = new StringBuilder();
            strXml.Append("<xml>");

            foreach (var item in sortedList)
            {
                if (Regex.IsMatch(item.Value, @"^[0-9.]$"))
                {
                    strXml.Append("<" + item.Key + ">" + item.Value + "</" + item.Key + ">");
                }
                else
                {
                    strXml.Append("<" + item.Key + "><![CDATA[" + item.Value + "]]></" + item.Key + ">");
                }
            }
            strXml.Append("</xml>");
            return strXml.ToString();
        }

        /// <summary>
        /// 调用接口错误提示
        /// </summary>
        /// <param name="strErrorCode"></param>
        /// <returns></returns>
        public static string GetErrorStr(string strErrorCode)
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

        /// <summary>
        /// XML转SortedList
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static SortedList<string, string> XmlTransSortedList(string strXml)
        {
            SortedList<string, string> sortedList = new SortedList<string, string>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(new System.IO.MemoryStream(System.Text.Encoding.GetEncoding("utf-8").GetBytes(strXml)));
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    sortedList.Add(xnf.Name, xnf.InnerText);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return sortedList;
        }

        #region 私有方法

        /** 获取大写的MD5签名结果 */
        private static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        #endregion
    }
}
