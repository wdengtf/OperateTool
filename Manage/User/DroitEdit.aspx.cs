using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using System.Web.UI.WebControls;
using WebControllers.Handle;
using Framework.Log;
using YYT.BLL;
using YYT.Model;
using System.Linq.Expressions;
using Framework.EF;

namespace Web.Manage.User
{
    public partial class DroitEdit : BaseAdminPage
    {
        private int id = 0;
        private HT_MenuBO menuBO = new HT_MenuBO();
        private HT_Menu model = new HT_Menu();

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utility.FNumeric("id");
            if (!this.IsPostBack)
            {
                if (id > 0) readInfo(id);

                BindPid();
            }
        }

        private void readInfo(int id)
        {
            try
            {
                model = menuBO.Find(id);

                txtTitle.Text = model.Title;
                ddlisMenu.SelectedValue = model.isMenu.ToString();
                txtUrl.Text = model.Url;
                txtRs_order.Text = model.SortId.ToString();
                Pid.SelectedValue = model.Pid.ToString();
                rblDroit.SelectedValue = model.Droit; ;
                rbl_status.SelectedValue = model.status.ToString();
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
        }


        private void BindPid()
        {
            Expression<Func<HT_Menu, bool>> where = PredicateExtensionses.True<HT_Menu>();
            where = where.AndAlso(p => p.isMenu >= (int)HT_MenuMenu.MainMenu);
            Expression<Func<HT_Menu, int>> orderBy = p => p.SortId.Value;
            defaultSort = "asc";
            List<HT_Menu> menuList = menuBO.FindAll<int>(where, orderBy, defaultSort);

            Pid.Items.Clear();
            Pid.Items.Add(new ListItem("请选择", "0"));

            foreach (HT_Menu menuModel in menuList.Where(n => n.isMenu == (int)HT_MenuMenu.MainMenu))
            {
                Pid.Items.Add(new ListItem(menuModel.Title, menuModel.id.ToString()));
                string blank = "├";
                BindNode(menuModel.id, menuList, blank, Pid);
            }
            Pid.DataBind();
        }

        #region 绑定子分类
        private void BindNode(int parentid, List<HT_Menu> menuList, string blank, DropDownList DDL1)
        {
            foreach (HT_Menu menuModel in menuList.Where(n => n.Pid == parentid))
            {
                DDL1.Items.Add(new ListItem(blank + menuModel.Title, menuModel.id.ToString()));

                string blank2 = blank + "─";
            }
        }
        #endregion

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                model.Title = txtTitle.Text.ToString().Trim();
                model.isMenu = int.Parse(ddlisMenu.SelectedValue.ToString().Trim());
                model.Droit = rblDroit.SelectedValue.ToString().Trim();
                model.SortId = int.Parse(txtRs_order.Text.ToString().Trim());
                model.Url = txtUrl.Text.ToString().Trim();
                model.Pid = int.Parse(Pid.SelectedValue.ToString().Trim());
                model.status = int.Parse(rbl_status.SelectedValue.ToString().Trim());
                model.createtime = DateTime.Now;
                model.id = id;
                if (model.id > 0)
                {
                    menuBO.Update(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('edit_" + id + "','修改成功!');");
                    LogService.logInfo(manageUserModel.UserName + "修改数据" + txtTitle.Text.Trim() + "，成功！");
                }
                else
                {
                    menuBO.Add(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('add','新增成功!');");
                    LogService.logInfo(manageUserModel.UserName + "新增数据" + txtTitle.Text.Trim() + "，成功！");
                }
            }
            catch (Exception ex)
            {
                Utility.ScriptMessage("parent.dialog.ShowTempMessage('参数错误!');");
                LogService.logDebug(ex);
            }
        }
    }
}