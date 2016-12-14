using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using Framework.Cookies;
using WebControllers.Model;
using YYT.BLL;
using YYT.Model;
using System.Linq.Expressions;
using Framework.EF;

namespace WebControllers.Admin
{
    /// <summary>
    /// 后台会员
    /// </summary>
    public class AdminUser
    {
        private CookieHandle cookieHandle = new CookieHandle();
        private HT_MenuBO menuBO = new HT_MenuBO();

        /// <summary>
        /// 获取会员对象
        /// </summary>
        /// <returns></returns>
        public ManageUserModel GetManageUserModel()
        {
            ManageUserModel manageUserModel = null;
            try
            {
                if (cookieHandle.GetManagerUserId() > 0)
                {
                    manageUserModel = new ManageUserModel();
                    manageUserModel.UserId = cookieHandle.GetManagerUserId();
                    manageUserModel.UserName = cookieHandle.GetManagerUserName();
                    manageUserModel.UserDroit = cookieHandle.GetManagerDroit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return manageUserModel;
        }

        /// <summary>
        /// 验证用户登录信息并跳转
        /// </summary>
        public void CheckLoginJump()
        {
            if (!CheckLogin())
            {
                System.Web.HttpContext.Current.Response.Write("<script>window.top.location.href= '/index.aspx ';</script>");
            }
        }

        /// <summary>
        /// 验证用户登录信息
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin()
        {
            bool flag = false;
            int userId = cookieHandle.GetManagerUserId();
            string userName = cookieHandle.GetManagerUserName();
            if (userId > 0 && !String.IsNullOrEmpty(userName))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 返回用户对象
        /// </summary>
        /// <param name="sDroit"></param>
        /// <returns></returns>
        public List<ManageDroitModel> GetManageDroitModel(string sDroit)
        {
            List<ManageDroitModel> manageDroitList = null;
            try
            {
                if (String.IsNullOrEmpty(sDroit))
                    return manageDroitList;


                Expression<Func<HT_Menu, bool>> where = PredicateExtensionses.True<HT_Menu>();
                where = where.AndAlso(p => p.status == (int)StatusEnmu.Normal);
                List<string> listDroit = new List<string>(sDroit.Split(','));
                where = where.AndAlso(p => listDroit.Contains(p.id.ToString()));
                Expression<Func<HT_Menu, int>> orderBy = p => p.SortId;
                List<HT_Menu> list = menuBO.FindAll<int>(where, orderBy, "desc");
                if (list == null || list.Count < 1)
                    return manageDroitList;

                manageDroitList = new List<ManageDroitModel>();
                foreach (HT_Menu menu in list)
                {
                    ManageDroitModel manageDroitModel = new ManageDroitModel();

                    manageDroitModel.Id = menu.id;
                    manageDroitModel.Rs_title = menu.Title;
                    manageDroitModel.Droit = menu.Droit;
                    manageDroitModel.Pid = menu.Pid.Value;
                    manageDroitModel.Url = menu.Url;

                    manageDroitList.Add(manageDroitModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return manageDroitList;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public bool LoginOut()
        {
            return cookieHandle.RemoveCookies();
        }
    }
}
