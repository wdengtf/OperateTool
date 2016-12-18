using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Framework;
using System.Reflection;

namespace YYT.Api
{
    public class BaseApiModel
    {
        /// <summary>
        /// 签名
        /// </summary>
        [Required(ErrorMessage = "签名不能为空")]
        public string sign { get; set; }

        /// <summary>
        /// 渠道用户名
        /// </summary>
        [Required(ErrorMessage = "渠道用户名不能为空")]
        public string channelUser { get; set; }

        /// <summary>
        /// 返回签名数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IDictionary<string, string> CreateSignDictionary<T>(T model) where T : BaseApiModel
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            Type type = typeof(T);
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.Name != "sign")
                {
                    if (property.PropertyType.Name == "List`1") continue; //list集不加
                    string name = property.Name;

                    if (property.GetValue(model) != null)
                    {
                        string value = property.GetValue(model).ToString();
                        string newValue = Utility.FilterString(value);
                        if (newValue != value)
                        {
                            throw new Exception("输入数据含有特殊字符");
                        }
                        dic.Add(name, value);
                    }
                    else
                    {
                        dic.Add(name, "");
                    }
                }
            }
            return dic;
        }
    }
}
