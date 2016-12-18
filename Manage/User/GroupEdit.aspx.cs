using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Framework;
using Framework.EF;
using Framework.Log;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;
using Framework.Model;

namespace Web.Manage.User
{
    public partial class GroupEdit : BaseAdminPage
    {
        private int id = 0;
        private string Dlist = "";
        private HT_UserGroupBO userGroupBO = new HT_UserGroupBO();
        private HT_MenuBO menuBO = new HT_MenuBO();
        private HT_UserGroup model = new HT_UserGroup();

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utility.FNumeric("id");
            if (!this.IsPostBack)
            {
                if (id > 0) readinfo(id);
                sDroit.Text = getAllDroitForCheckbox(Dlist);
            }
        }


        private void readinfo(int id)
        {
            try
            {
                model = userGroupBO.Find(id);
                txtTitle.Text = model.Title;
                Dlist = model.Droit;
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string strDroit = Utility.RF("sDroit");
                //表单验证
                if (String.IsNullOrEmpty(strDroit))
                {
                    Utility.ScriptMessage("parent.dialog.ShowTempMessage('请选择相关权限!');");
                    return;
                }
                model.Title = txtTitle.Text;
                model.Droit = strDroit;
                model.Createtime = DateTime.Now;
                model.id = id;
                if (model.id > 0)
                {
                    userGroupBO.Update(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('edit_" + id + "','修改成功!');");
                    LogService.LogInfo(manageUserModel.UserName + "修改数据" + txtTitle.Text.Trim() + "，成功！");
                }
                else
                {
                    userGroupBO.Add(model);
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

        private string getAllDroitForCheckbox(string sDroitlist)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("<table witdh=\"100%\">");

            Expression<Func<HT_Menu, bool>> where = PredicateExtensionses.True<HT_Menu>();
            where = where.AndAlso(p => p.status == (int)StatusEnmu.Normal);
            Expression<Func<HT_Menu, int>> orderBy = p => p.SortId.Value;
            defaultSort = "asc";
            List<HT_Menu> menuList = menuBO.FindAll<int>(where, orderBy, defaultSort);

            foreach (HT_Menu admin_MenuModel in menuList.Where(n => n.Pid == 0 && n.isMenu == (int)HT_MenuMenu.MainMenu))
            {
                strb.Append("<tr><td class=\"MainMenu\"  width=\"100%\"><input type=\"checkbox\" name=\"sDroit\" value=\"" + admin_MenuModel.id + "\"  " + CheckIndex(sDroitlist, admin_MenuModel.id.ToString()) + ">" + admin_MenuModel.Title + "</td></tr>");

                foreach (HT_Menu admin_MenuModel_2 in menuList.Where(n => n.Pid == admin_MenuModel.id))
                {
                    strb.Append("<tr><td class=\"SubMenu\"  width=\"100%\">&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"sDroit\" value=\"" + admin_MenuModel_2.id + "\"   " + CheckIndex(sDroitlist, admin_MenuModel_2.id.ToString()) + ">" + admin_MenuModel_2.Title + "</td></tr>");

                    strb.Append("<tr><td class=\"SubItem\" width=\"100%\"><div class=\"sItem\"  width=\"100%\">");
                    foreach (HT_Menu admin_MenuModel_3 in menuList.Where(n => n.Pid == admin_MenuModel_2.id))
                    {
                        strb.Append("<div class=\"cssItem\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"sDroit\" value=\"" + admin_MenuModel_3.id + "\"   " + CheckIndex(sDroitlist, admin_MenuModel_3.id.ToString()) + ">" + admin_MenuModel_3.Title + "</div>");
                    }
                    strb.Append("</div>");
                    strb.Append("</td></tr>");
                }
            }
            strb.Append("</table>");
            return strb.ToString();
        }

        private static string CheckIndex(string slist, string sid)
        {
            string reStr = "";
            slist = "," + slist + ",";
            if (slist.IndexOf("," + sid.ToString() + ",") > -1) reStr = "checked";
            return reStr;

        }
    }
}