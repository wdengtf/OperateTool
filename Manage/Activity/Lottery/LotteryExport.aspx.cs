using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebControllers.Handle;
using Framework;
using Framework.Log;
using YYT.BLL;
using System.Text;
using YYT.BLL.Common;
using System.Data;
using System.IO;

namespace Manage.Activity.Lottery
{
    public partial class LotteryExport : BaseAdminPage
    {
        private string action = "";
        private Luck_ActivityPrizeBO luckActivityPrizeBo = new Luck_ActivityPrizeBO();
        private SqlBO sqlBo = new SqlBO();
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Utility.RF("action");
            if (!Page.IsPostBack)
            {
                switch (action)
                {
                    case "jackpotExport":
                        JackpotExport();
                        break;
                }
            }
        }

        private void JackpotExport()
        {
            try
            {
                int activity_id = Utility.FNumeric("activity_id");
                int award_id = Utility.FNumeric("award_id");
                string out_id = Utility.RF("out_id");
                int win_status = Utility.FNumeric("status");

                StringBuilder strWhere = new StringBuilder(255);
                strWhere.Append(" 1=1 ");
                if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                    strWhere.Append(String.Format(" and channelUserId = {0}", manageUserModel.UserId));

                if (!String.IsNullOrWhiteSpace(out_id))
                    strWhere.Append(String.Format(" and a.out_id ={0}", out_id));
                if (award_id > 0)
                    strWhere.Append(String.Format(" and a.PrizeId ={0}", award_id));
                if (activity_id > 0)
                    strWhere.Append(String.Format(" and a.ActivityId ={0}", activity_id));
                if (win_status == 0 || win_status == 1)
                    strWhere.Append(String.Format(" and a.Status ={0}", win_status));

                string strColumn = " a.id,b.name as prizeName,c.name as activityName,a.out_id,a.data_type,a.Status,a.createtime,a.updatetime,a.PrizeId,a.ActivityId,d.Mobile,d.addr,d.realName";
                string tableName = "  Luck_ActivityJackpot a inner join Luck_ActivityPrize b on a.PrizeId=b.id inner join Luck_Activity c on c.id=a.ActivityId left join YYT_Member d on d.out_id=a.out_id";
                string strOrder = " a.id desc";

                DataTable dt = sqlBo.GetdataBySql(sqlBo.GetSqlStr(strColumn, tableName, strWhere.ToString(), strOrder));
                if (dt == null || dt.Rows.Count < 1)
                {
                    Response.Write("没有数据需要导出");
                    return;
                }

                StringWriter sw = new StringWriter();
                sw.WriteLine("Id\t活动名称\t奖品名称\t状态\t姓名\t中奖outId\t手机\t地址\t中奖时间");
                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["id"].ToString() + "\t" + dr["activityName"].ToString() + "\t" + dr["prizeName"].ToString() + "\t" + dr["Status"].ToString() + "\t" + dr["realName"].ToString() + "\t" + dr["out_id"].ToString() + "\t" + dr["Mobile"].ToString() + "\t" + dr["addr"].ToString() + "\t" + dr["updatetime"].ToString());
                }
                sw.Dispose();
                sw.Close();
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=Jackpot" + System.DateTime.Now.ToShortDateString() + ".xls");
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.Write(sw);
                System.Web.HttpContext.Current.Response.End();
                
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
        }
    }
}