using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Handle
{
    /// <summary>
    /// 将表单转换成对象
    /// </summary>
    public class FromToObj
    {
        public FromToObj()
        { }

        /// <summary>
        /// 把Form Post过来的表单集合转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public T FormToClass<T>(NameValueCollection collection)
        {
            T t = Activator.CreateInstance<T>();
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo _target in properties)
            {
                if (_target.CanWrite)
                    _target.SetValue(t, Convert.ChangeType(collection[_target.Name], _target.PropertyType));
            }
            return t;
        }

        /// <summary>
        /// 将一个对象转换成另外一个对象
        /// </summary>
        /// <typeparam name="T">对象T</typeparam>
        /// <typeparam name="K">对象K</typeparam>
        /// <param name="k">对象实例</param>
        /// <returns></returns>
        public T ClassToClass<T, K>(K k)
        {
            T t = Activator.CreateInstance<T>();
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            PropertyInfo[] properties_kk = k.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo _target in properties)
            {
                if (_target.CanWrite)
                {
                    foreach (PropertyInfo _target_kk in properties_kk)
                    {
                        if (_target.Name == _target_kk.Name && _target.PropertyType == _target_kk.PropertyType)
                        {
                            _target.SetValue(t, _target_kk.GetValue(k, null));
                            break;
                        }
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 将DataTable转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> ConvertToModel<T>(DataTable dt)
        {
            // 定义集合    
            List<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = Activator.CreateInstance<T>();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
