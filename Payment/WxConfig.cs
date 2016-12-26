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

namespace Payment
{
    public class WxConfig
    {
        public static string returnSuccessCode = "SUCCESS";

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
                    if (property.Name != "sign" || property.Name != "key" || property.Name != "postUrl")
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
                    if (String.IsNullOrEmpty(item.Value) || item.Key.Equals("sign") || item.Key.Equals("key") || item.Key.Equals("postUrl"))
                        continue;

                    if (String.IsNullOrWhiteSpace(sortStr.ToString()))
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
