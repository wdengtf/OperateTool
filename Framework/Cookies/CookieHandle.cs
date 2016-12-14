using System;
using System.Collections.Generic;
using System.Web;
using Framework.Log;

namespace Framework.Cookies
{
    /// <summary>
    /// Cookies Session处理
    /// </summary>
    public class CookieHandle
    {
        #region 设置后台用户信息
        private string strManagerCookName = "YYTManage";
        private string[] UserCookName = { "userId", "UserName", "Droit" };

        /// <summary>
        /// 设置后台用户的Cookies
        /// </summary>
        /// <param name="userId">会员id</param>
        /// <param name="UserName">会员名</param>
        /// <param name="Droit">权限值</param>
        /// <returns></returns>
        public bool SetManagerUser(int userId, string UserName, string Droit)
        {
            bool flag = false;
            try
            {
                if (userId < 1 || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Droit))
                    return false;

                IDictionary<string, string> parm = new Dictionary<string, string>();
                parm.Add(UserCookName[0], userId.ToString());
                parm.Add(UserCookName[1], UserName);
                parm.Add(UserCookName[2], Droit);

                DateTime ExpiresTime = DateTime.Now.AddDays(1);

                CookUtils.AddCookie(strManagerCookName, parm, ExpiresTime);
                flag = true;
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return flag;
        }

        /// <summary>
        /// 获取后台用户id
        /// </summary>
        /// <returns></returns>
        public int GetManagerUserId()
        {
            int UserId = 0;
            try
            {
                string cookValue = CookUtils.GetCookieValue(strManagerCookName, UserCookName[0]);

                if (string.IsNullOrEmpty(cookValue))
                    return UserId;

                int.TryParse(cookValue, out UserId);

                return UserId;
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return UserId;
        }

        /// <summary>
        /// 获取后台用户名称
        /// </summary>
        /// <returns></returns>
        public string GetManagerUserName()
        {
            string strCookValue = "";
            try
            {
                return CookUtils.GetCookieValue(strManagerCookName, UserCookName[1]);
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return strCookValue;
        }

        /// <summary>
        /// 获取后台用户权限
        /// </summary>
        /// <returns></returns>
        public string GetManagerDroit()
        {
            string strCookValue = "";
            try
            {
                return CookUtils.GetCookieValue(strManagerCookName, UserCookName[2]);
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return strCookValue;
        }

        /// <summary>
        /// 清除Cookies
        /// </summary>
        /// <returns></returns>
        public bool RemoveCookies()
        {
            bool flag = false;
            //清除所有的Session
            System.Web.HttpContext.Current.Session.RemoveAll();

            //删除所有的Cookies数据
            int limit = System.Web.HttpContext.Current.Request.Cookies.Count;
            HttpCookie aCookie;
            string cookieName = "";
            for (int i = 0; i < limit; i++)
            {
                cookieName = System.Web.HttpContext.Current.Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(aCookie);
            }
            flag = true;
            return flag;
        }

        #endregion
    }
}
