using Framework.EF;
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
using Framework;
using Framework.Model;
using Framework.Log;

namespace Manage.Activity
{
    public partial class LotteryDataEdit : BaseAdminPage
    {
        private Luck_ActivityBO luckActivityBo = new Luck_ActivityBO();
        private Luck_ActivityJackpotBO luckActivityJackpotBo = new Luck_ActivityJackpotBO();
        private YYT_MemberBO memberBo = new YYT_MemberBO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BindActivity();
            }
        }

        /// <summary>
        /// 绑定活动
        /// </summary>
        private void BindActivity()
        {
            Expression<Func<Luck_Activity, bool>> where = PredicateExtensionses.True<Luck_Activity>();
            if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                where = where.AndAlso(p => p.channelUserId == manageUserModel.UserId);
            List<Luck_Activity> actLotteryList = luckActivityBo.FindAll<int>(where);

            ddlActivity.DataTextField = "Name";
            ddlActivity.DataValueField = "id";
            ddlActivity.DataSource = actLotteryList;
            ddlActivity.DataBind();

            ddlActivity.Items.Insert(0, new ListItem("请选择", "0"));
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                int activityId = int.Parse(ddlActivity.SelectedItem.Value);
                int prizeId = int.Parse(Utility.RF("selActivieyPrize"));
                if (activityId < 1 || prizeId < 1)
                {
                    Utility.ScriptMessage("parent.dialog.ShowTempMessage('参数错误!');");
                    return;
                }

                //获取奖池里面的奖品信息
                Expression<Func<Luck_ActivityJackpot, bool>> whereNoDrawJackpot = PredicateExtensionses.True<Luck_ActivityJackpot>();
                whereNoDrawJackpot = whereNoDrawJackpot.AndAlso(p => p.ActivityId == activityId);
                whereNoDrawJackpot = whereNoDrawJackpot.AndAlso(p => p.PrizeId == prizeId);
                whereNoDrawJackpot = whereNoDrawJackpot.AndAlso(p => p.Status == (int)LuckActivityJackpotStatus.NotDraw);
                Luck_ActivityJackpot noDrawJackpotModel = luckActivityJackpotBo.GetSingle<Guid>(whereNoDrawJackpot, x => Guid.NewGuid());
                if (noDrawJackpotModel == null || noDrawJackpotModel.id < 1)
                {
                    Utility.ScriptMessage("parent.dialog.ShowTempMessage('奖品没有了!');");
                    return;
                }
               //添加浏览记录
                QD_ChannelLog channelLog = new QD_ChannelLog();
                channelLog.channelName = txtNickName.Text.Trim();
                channelLog.@interface = txtOutId.Text.Trim();
                channelLog.status = 1;
                channelLog.Createtime = DateTime.Parse(txtUpdateTime.Text.Trim());
                channelLog.Addtime = DateTime.Parse(txtUpdateTime.Text.Trim());
                channelLog.ip = txtIP.Text.Trim();
                new QD_ChannelLogBO().Add(channelLog);

                //添加抽奖记录
                noDrawJackpotModel.Status = (int)LuckActivityJackpotStatus.AlreadyDraw;
                noDrawJackpotModel.data_type = ddlDataType.SelectedItem.Value;
                noDrawJackpotModel.out_id = txtOutId.Text.Trim();
                noDrawJackpotModel.UpdateAddtime = noDrawJackpotModel.Updatetime = DateTime.Parse(txtUpdateTime.Text.Trim()).AddMilliseconds(20);
                noDrawJackpotModel.Ip = txtIP.Text.Trim();
                luckActivityJackpotBo.Update(noDrawJackpotModel);

                //更新会员信息
                Expression<Func<YYT.Model.YYT_Member, bool>> where = PredicateExtensionses.True<YYT.Model.YYT_Member>();
                where = where.AndAlso(p => p.out_id.Contains(noDrawJackpotModel.out_id));
                YYT.Model.YYT_Member memberModel = memberBo.GetSingle<int>(where);
                memberModel.realName = txtRealName.Text.Trim();
                memberModel.Mobile = txtMobile.Text.Trim();
                memberModel.addr = txtAddress.Text.Trim();
                memberModel.Updatetime = DateTime.Parse(txtUpdateTime.Text.Trim()).AddSeconds(151);
                memberBo.Update(memberModel);

                Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('add','操作成功!');");
                LogService.LogInfo(manageUserModel.UserName + "操作成功！");
            }
            catch (Exception ex)
            {
                Utility.ScriptMessage("parent.dialog.ShowTempMessage('参数错误!');");
                LogService.LogDebug(ex);
            }
        }
    }
}