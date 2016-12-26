using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ayx
{
    public class AyxConfig
    {
        public static string appkey = "425c6f28f5e6d55a8aaf3353aa3e6c39";
        public static string postIdCardUrl = "http://api.id98.cn/api/idcard";
        public static string output = "json";

        /// <summary>
        /// model 转  IDictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetSortedList<T>(T model)
        {
            IDictionary<string, string> sortedList = new Dictionary<string, string>();
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
    }
}
