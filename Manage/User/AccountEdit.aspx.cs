using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework;
using Framework.EF;
using Framework.Log;
using Framework.Utils;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Web.Manage.User
{
    public partial class AccountEdit : BaseAdminPage
    {
        private int id = 0;
        private HT_AccountBO accountBO = new HT_AccountBO();
        private HT_UserGroupBO userGroupBO = new HT_UserGroupBO();
        private HT_Account model = new HT_Account();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utility.FNumeric("id");
            if (!this.IsPostBack)
            {
                if (id > 0) readInfo(id);
                else txtCreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                bindGroup();
            }
        }

        private void readInfo(int id)
        {
            try
            {
                model = accountBO.Find(id);

                txtTitle.Text = model.username;
                rblStatus.SelectedValue = model.status.ToString();
                ddlGroup.SelectedValue = model.groupid.ToString();
                txtTitle.ReadOnly = true;
                txtCreatetime.Text = model.createtime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                CHK.Value = model.pwd;
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
        }

        private void bindGroup()
        {
            Expression<Func<HT_UserGroup, bool>> expre = PredicateExtensionses.True<HT_UserGroup>();
            if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                expre = expre.AndAlso(p => p.id == manageUserModel.UserId);

            List<HT_UserGroup> admin_GroupList = userGroupBO.FindAll<int>(expre);
            ddlGroup.DataTextField = "Title";
            ddlGroup.DataValueField = "Id";
            ListItem item = new ListItem("请选择", "", true);
            ddlGroup.Items.Add(item);
            ddlGroup.DataSource = admin_GroupList;
            ddlGroup.DataBind();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                model.username = txtTitle.Text.ToString().Trim();
                model.groupid = int.Parse(ddlGroup.SelectedValue.ToString().Trim());
                model.status = int.Parse(rblStatus.SelectedValue.ToString().Trim());
                model.id = id;
                if (model.id > 0)
                {
                    model.createtime = DateTime.Parse(DateTime.Now.ToString());
                    if (CHK.Checked)
                        model.pwd = SignUtil.MD5Hash(password.Text.Trim());
                    else
                        model.pwd = CHK.Value;

                    accountBO.Update(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('edit_" + id + "','修改成功!');");
                    LogService.LogInfo(manageUserModel.UserName + "修改数据" + txtTitle.Text.Trim() + "，成功！");
                }
                else
                {
                    model.pwd = SignUtil.MD5Hash(password.Text.Trim());
                    model.createtime = DateTime.Now;
                    accountBO.Add(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('add','新增成功!');");
                    LogService.LogInfo(manageUserModel.UserName + "新增数据" + txtTitle.Text.Trim() + "，成功！");
                }
            }
            catch (Exception ex)
            {
                Utility.ScriptMessage("parent.dialog.ShowTempMessage('参数错误!');");
                LogService.LogDebug(ex);
            }
        }
    }
}