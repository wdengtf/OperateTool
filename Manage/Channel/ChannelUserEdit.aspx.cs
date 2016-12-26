using Framework;
using Framework.EF;
using Framework.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Manage.Channel
{
    public partial class ChannelUserEdit : BaseAdminPage
    {
        private int id = 0;
        private QD_ChannelBO channelBo = new QD_ChannelBO();
        private QD_ChannelUserBO channelUserBo = new QD_ChannelUserBO();
        private QD_ChannelUser model = new QD_ChannelUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utility.FNumeric("id");
            if (!this.IsPostBack)
            {
                BindChannelSort();
                if (id > 0) readInfo(id);
                else
                {
                    txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtEndDate.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
                    txtCreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        private void BindChannelSort()
        {
            Expression<Func<QD_Channel, bool>> where = PredicateExtensionses.True<QD_Channel>();
            List<QD_Channel> channelList = channelBo.FindAll<int>(where);
            ddlChannelSort.DataTextField = "name";
            ddlChannelSort.DataValueField = "id";
            ddlChannelSort.DataSource = channelList;
            ddlChannelSort.DataBind();
            ddlChannelSort.Items.Insert(0, "-请选择-");
        }

        private void readInfo(int id)
        {
            try
            {
                model = channelUserBo.Find(id);

                txtUserName.Text = model.user_name;
                txtUserKey.Text = model.user_key;
                rblStatus.SelectedValue = model.Status.ToString();
                rblLimitIp.SelectedValue = model.validate_ip.ToString();
                ddlChannelSort.SelectedValue = model.channel_id.ToString();
                txtCreatetime.Text = model.Createtime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                txtStartDate.Text = model.start_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
                txtEndDate.Text = model.end_time.Value.ToString("yyyy-MM-dd HH:mm:ss");

                if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                    ddlChannelSort.Enabled = false;
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
                model.user_name = txtUserName.Text.ToString().Trim();
                model.user_key = txtUserKey.Text.Trim();
                model.channel_id = int.Parse(ddlChannelSort.SelectedItem.Value);
                model.validate_ip = int.Parse(rblLimitIp.SelectedValue.ToString().Trim());
                model.Status = int.Parse(rblStatus.SelectedValue.ToString().Trim());
                model.start_time = DateTime.Parse(txtStartDate.Text.Trim());
                model.end_time = DateTime.Parse(txtEndDate.Text.Trim());
                model.Createtime = DateTime.Parse(txtCreatetime.Text.Trim());

                model.id = id;
                if (model.id > 0)
                {
                    channelUserBo.Update(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('edit_" + id + "','修改成功!');");
                    LogService.LogInfo(manageUserModel.UserName + "修改数据" + txtUserName.Text.Trim() + "，成功！");
                }
                else
                {
                    channelUserBo.Add(model);
                    Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('add','新增成功!');");
                    LogService.LogInfo(manageUserModel.UserName + "新增数据" + txtUserName.Text.Trim() + "，成功！");
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