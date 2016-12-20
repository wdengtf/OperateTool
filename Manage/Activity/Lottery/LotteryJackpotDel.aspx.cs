using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class LotteryJackpotDel : BaseAdminPage
    {
        private int id;
        private string subgrid_table_id = "";
        private Luck_ActivityPrizeBO luckActivityPrizeBO = new Luck_ActivityPrizeBO();
        private Luck_ActivityJackpotBO luckActivityJackpotBO = new Luck_ActivityJackpotBO();
        protected int delMaxNum = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utility.FNumeric("id");
            subgrid_table_id = Utility.RF("subgrid_table_id");
            if (!Page.IsPostBack)
            {
                delMaxNum = GettDelNum(id).Count;
            }
        }

        /// <summary>
        /// 获取可以删除数量
        /// </summary>
        private List<Luck_ActivityJackpot> GettDelNum(int id)
        {
            List<Luck_ActivityJackpot> actJackpotList = new List<Luck_ActivityJackpot>();
            if (id < 1)
                return actJackpotList;

            Expression<Func<Luck_ActivityJackpot, bool>> where = PredicateExtensionses.True<Luck_ActivityJackpot>();
            if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                where = where.AndAlso(p => p.channelUserId == manageUserModel.UserId);

            where = where.AndAlso(p => p.PrizeId == id && p.Status==(int)LuckActivityJackpotStatus.NotDraw);
            return luckActivityJackpotBO.FindAll<int>(where);
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            List<Luck_ActivityJackpot> actJackpotList = GettDelNum(id);
            int delMaxNum = actJackpotList.Count;
            int delNum = int.Parse(txtNum.Text.Trim());
            if (delNum > 0 && delMaxNum > 0 && delMaxNum > delNum)
            {
                List<int> idList = actJackpotList.Select(n => n.id).Take(delNum).ToList();
                Expression<Func<Luck_ActivityJackpot, bool>> where = PredicateExtensionses.True<Luck_ActivityJackpot>();
                where = where.AndAlso(p => idList.Contains(p.id));
                luckActivityJackpotBO.DeleteByWhere(where);

                //更新总数
                Luck_ActivityPrize lotteryPrizeModel = luckActivityPrizeBO.Find(id);
                lotteryPrizeModel.num = delMaxNum - delNum;
                luckActivityPrizeBO.Update(lotteryPrizeModel);

                Utility.ScriptMessage("parent.dialog.closeDialogAlertMsgReferJqGrid('del_" + id + "','删除成功!','" + subgrid_table_id + "');");
                LogService.LogInfo(manageUserModel.UserName + "删除数据，成功！");
            }
            else
            {
                Utility.ScriptMessage("parent.dialog.ShowTempMessage('删除失败,删除的奖品数量大于剩余数量!');");
                return;
            }
        }
    }
}