using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class YYT_MemberBO : YYT_DBEntities
    {
        private static YYT_MemberBO Service = null;
        private static readonly object syncLock = new object();

        /// <summary>
        /// 构造函数私有化，防止通过new实例化对象
        /// </summary>
        private YYT_MemberBO()
        { }

        /// <summary>
        ///  单例模式 保证实例只被创建一次
        /// </summary>
        public static YYT_MemberBO GetService
        {
            get
            {
                //使用延迟初始化 支持多线程 双检查锁定模式
                if (null == Service)
                {
                    lock (syncLock)
                    {
                        if (null == Service)
                        {
                            Service = new YYT_MemberBO();
                        }
                    }
                }
                return Service;
            }
        }
    }
}
