using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework;
using Framework.Log;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Web.Manage.Activity
{
    public partial class LotteryEdit : BaseAdminPage
    {
        private int id;
        private Luck_ActivityBO luckActivityBO = new Luck_ActivityBO();
        private Luck_Activity model = new Luck_Activity();

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utility.FNumeric("id");
            if (!this.IsPostBack)
            {
                if (id > 0)
                {
                    readinfo(id);
                }
                else
                {
                    txt_startdate.Text = DateTime.Now.ToShortDateString();
                    txt_enddate.Text = DateTime.Now.AddMonths(1).ToShortDateString();
                }
            }
        }

        private void readinfo(int id)
        {
            try
            {
                model = luckActivityBO.Find(id);

                txt_Name.Text = model.Name;
                txt_startdate.Text = model.Startdate.ToString();
                txt_enddate.Text = model.Enddate.ToString();
                rbl_status.SelectedValue = model.Status.ToString();
                rblRules.SelectedValue = model.Rules.ToString();
                txt_maxNum.Text = model.maxNum.ToString();
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
                model.Name = Utility.FilterString(txt_Name.Text.Trim());
                model.Startdate = DateTime.Parse(txt_startdate.Text.Trim());
                model.Enddate = DateTime.Parse(txt_enddate.Text.Trim());
                model.Status = int.Parse(rbl_status.SelectedItem.Value);
                model.Rules = int.Parse(rblRules.Text);
                model.Img = "";
                model.Url = "";
                model.Createtime = DateTime.Now;
                model.Introduction = "";
                model.descr = "";
                model.channelUserId = manageUserModel.UserId;
                model.maxNum = int.Parse(txt_maxNum.Text.Trim());
                model.id = id;
                if (String.IsNullOrEmpty(model.Name))
                {
                    Utility.ScriptMessage("parent.dialog.ShowTempMessage('请填写活动名称!');");
                    return;
                }

                if (id > 0)
                {
                    luckActivityBO.Update(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('edit_" + id + "','修改成功!');");
                    LogService.LogInfo(manageUserModel.UserName + "修改数据，成功！");
                }
                else
                {
                    luckActivityBO.Add(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('add','新增成功!');");
                    LogService.LogInfo(manageUserModel.UserName + "新增数据成功！");
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