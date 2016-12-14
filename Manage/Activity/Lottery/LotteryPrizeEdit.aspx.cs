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
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Web.Manage.Activity.Lottery
{
    public partial class LotteryPrizeEdit : BaseAdminPage
    {
        private int id, sortid;
        private string subgrid_table_id = "";
        private Luck_ActivityBO luckActivityBO = new Luck_ActivityBO();
        private Luck_ActivityPrizeBO luckActivityPrizeBO = new Luck_ActivityPrizeBO();
        private Luck_ActivityPrize model = new Luck_ActivityPrize();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utility.FNumeric("id");
            sortid = Utility.FNumeric("sortid");
            subgrid_table_id = Utility.RF("subgrid_table_id");

            if (!Page.IsPostBack)
            {
                BindLottery(sortid);//抽奖活动
                if (id > 0) readinfo(id);
                else
                {
                    txt_createtime.Text = DateTime.Now.ToString();
                }

                //奖品显示位置
                if (sortid > 0 && id == 0)
                {
                    Expression<Func<Luck_ActivityPrize, bool>> where = PredicateExtensionses.True<Luck_ActivityPrize>();
                    where = where.AndAlso(p => p.sortid == sortid);
                    Luck_ActivityPrize lotteryPrizeList = luckActivityPrizeBO.GetSingle<int>(where, p => p.Position.Value);
                    if (lotteryPrizeList == null)
                    {
                        txtPosition.Text = "1";
                    }
                    else
                    {
                        txtPosition.Text = (lotteryPrizeList.Position + 1).ToString();
                    }
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            model.channelUserId = manageUserModel.UserId;
            model.name = Utility.FilterString(txt_Name.Text.Trim());
            model.sortid = int.Parse(ddl_activie_name.SelectedItem.Value);
            model.price = decimal.Parse(Utility.FilterString(txt_price.Text.Trim()));
            model.PrizeType = int.Parse(prizeType.SelectedItem.Value);
            model.PrizeLevel = Utility.FilterString(txt_level.Text.Trim());
            model.num = int.Parse(txt_num.Text.Trim());
            model.winNum = 0;
            model.PrizeUrl = Utility.FilterString(txt_Url.Text.Trim());
            model.PrizeImg = Utility.FilterString(txt_winImg.Text.Trim());
            model.Status = int.Parse(rbl_status.SelectedItem.Value);
            model.Introduction = Utility.FilterString(txt_Introduction.Text.Trim());
            model.descr = "";
            model.Position = int.Parse(txtPosition.Text.Trim());
            model.PositionImg = Utility.FilterString(txtPostionImg.Text.Trim());
            model.Createtime = DateTime.Parse(txt_createtime.Text.Trim());
            model.Id = id;

            string strMsg = "";
            if (string.IsNullOrEmpty(model.name))
                strMsg += "请填写奖品名称!<br />";
            if (model.sortid == 0)
                strMsg += "请选择抽奖活动!<br />";
            if (string.IsNullOrEmpty(model.PrizeLevel))
                strMsg += "请填写奖品级别!<br />";
            if (model.price <= 0)
                strMsg += "请填写奖品价格!";
            if (!string.IsNullOrEmpty(strMsg))
            {
                Utility.ScriptMessage("parent.dialog.ShowTempMessage('" + strMsg + "');");
                return;
            }

            if (model.Id > 0)
            {
                luckActivityPrizeBO.Update(model);
                Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('edit_" + id + "','修改成功!','" + subgrid_table_id + "');");
                LogService.logInfo(manageUserModel.UserName + "修改数据，成功！");
            }
            else
            {
                luckActivityPrizeBO.Add(model);
                Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('add','新增成功!','" + subgrid_table_id + "');");
                LogService.logInfo(manageUserModel.UserName + "新增数据成功！");
            }
        }

        private void readinfo(int id)
        {
            model = luckActivityPrizeBO.Find(id);
            if (model == null)
                return;

            txt_Name.Text = model.name;
            ddl_activie_name.SelectedValue = model.sortid.ToString();
            txt_price.Text = model.price.ToString();
            txt_level.Text = model.PrizeLevel;
            prizeType.SelectedValue = model.PrizeType.ToString();
            txt_num.Text = model.num.ToString();
            txt_Url.Text = model.PrizeUrl;
            txt_winImg.Text = model.PrizeImg;
            rbl_status.SelectedValue = model.Status.ToString();
            txt_Introduction.Text = model.Introduction;
            txtPosition.Text = model.Position.ToString();
            txtPostionImg.Text = model.PositionImg.ToString();
            txt_createtime.Text = model.Createtime.ToString();
        }


        private void BindLottery(int sortid)
        {

            Expression<Func<Luck_Activity, bool>> where = PredicateExtensionses.True<Luck_Activity>();
            where = where.AndAlso(p => p.Status == (int)StatusEnmu.Normal);
            List<Luck_Activity> actLotteryList = luckActivityBO.FindAll<int>(where);
            if (actLotteryList == null || actLotteryList.Count < 1)
                return;

            ddl_activie_name.DataTextField = "Name";
            ddl_activie_name.DataValueField = "id";
            ddl_activie_name.DataSource = actLotteryList;
            ddl_activie_name.DataBind();

            ddl_activie_name.Items.Insert(0, new ListItem("请选择", "0"));
        }
    }
}