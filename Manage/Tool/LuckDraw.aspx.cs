using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Model;
using Framework;
using LuckDraw;
using LuckDraw.Model;
using YYT.Model;

namespace Manage.Tool
{
    public partial class LuckDraw : System.Web.UI.Page
    {
        private LotteryCall lotteryCall = new LotteryCall();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string strApiName = ddl_interface.SelectedItem.Value;
            string strUserName = txt_UserName.Text.Trim();
            string strKey = txt_Key.Text.Trim();
            string strParm = txt_Parm.Text.Trim();
            string strUrl = Request.Url.ToString().Replace(Request.Path.ToString(), "") + "/api/" + strApiName;

            MemberBaseModel memberBaseModel = new MemberBaseModel()
            {
                data_type = "GW",
                out_id = "18975831540",
            };
            int actitityId = 3;

            try
            {
                switch (strApiName)
                {
                    case "GetLotteryActivity":
                        LotteryModel lotteryModel = lotteryCall.GetLotteryActivity(memberBaseModel, actitityId);
                        if (lotteryCall.GetResultState())
                            txt_Result.Text = Utility.ToJson(lotteryModel);
                        else
                            txt_Result.Text = lotteryCall.GetMessage();
                        break;
                    case "GetLotteryPrize":
                        List<WinRecordModel> winRecordList = lotteryCall.GetLotteryPrize(memberBaseModel, new List<int>() { actitityId });
                        if (lotteryCall.GetResultState())
                            txt_Result.Text = Utility.ToJson(winRecordList);
                        else
                            txt_Result.Text = lotteryCall.GetMessage();
                        break;
                    case "MemberBindLottery":
                        Luck_ActivityPrize luckActivityPrizeModel = lotteryCall.MemberBindLottery(memberBaseModel, actitityId);
                        if (lotteryCall.GetResultState())
                            txt_Result.Text = Utility.ToJson(luckActivityPrizeModel);
                        else
                            txt_Result.Text = lotteryCall.GetMessage();
                        break;
                }
            }
            catch (Exception ex)
            {
                txt_Result.Text = ex.ToString();
            }
        }
    }
}