using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using Framework.EF;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Web.Manage.Activity
{
    public partial class LotteryJackpotList : BaseAdminPage
    {
        private Luck_ActivityBO luckActivityBo = new Luck_ActivityBO();
        protected List<Luck_Activity> actLotteryList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Expression<Func<Luck_Activity, bool>> where= PredicateExtensionses.True<Luck_Activity>();
                actLotteryList = luckActivityBo.FindAll<int>(where);
            }
        }
    }
}