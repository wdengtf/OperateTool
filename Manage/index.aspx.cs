using System;
using System.Collections.Generic;
using System.Web.UI;
using Framework.Utils;
using Framework;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;
using System.Linq.Expressions;
using Framework.EF;

namespace Web.Manage
{
    public partial class index : BasePage
    {
        private HT_AccountBO accountBO = new HT_AccountBO();
        private HT_UserGroupBO userGroupBO = new HT_UserGroupBO();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            string strUserName = Utility.FilterString(username.Text);
            if (String.IsNullOrEmpty(strUserName))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "message", "dialog.ShowTempMessage('用户名称不能为空');", true);
                return;
            }

            if (String.IsNullOrEmpty(password.Text))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "message", "dialog.ShowTempMessage('密码不能为空');", true);
                return;
            }
            string strPassword = SignUtil.MD5Hash(password.Text);

            Expression<Func<HT_Account, bool>> where = PredicateExtensionses.True<HT_Account>();
            where = where.AndAlso(p => p.username.Equals(strUserName) && p.pwd.Equals(strPassword));
            Expression<Func<HT_Account, int>> orderBy = p => p.id;
            HT_Account adminUserList = accountBO.GetSingle<int>(where);
            if (adminUserList == null)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "message", "dialog.ShowTempMessage('用户名或密码错误');", true);
                return;
            }
            else if (adminUserList.status == (int)StatusEnmu.Locking)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "message", "dialog.ShowTempMessage('该用户已被锁定，请联系管理员');", true);
                return;
            }
            else
            {
                //登陆成功后保存用户信息
                HT_UserGroup adminGroupModel = userGroupBO.Find(adminUserList.groupid.Value);
                if (adminGroupModel == null)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "message", "dialog.ShowTempMessage('该用户未分配权限，请联系管理员');", true);
                    return;
                }
                cookieHandle.SetManagerUser(adminUserList.id, adminUserList.username, adminGroupModel.Droit);
                this.Response.Redirect("MainFrm.html");
            }
        }
    }
}