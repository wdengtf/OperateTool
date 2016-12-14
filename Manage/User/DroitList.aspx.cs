using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.EF;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Web.Manage.User
{
    public partial class DroitList : BaseAdminPage
    {
        private HT_MenuBO menuBO = new HT_MenuBO();
        protected List<HT_Menu> admin_MenuList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Expression<Func<HT_Menu, bool>> where = PredicateExtensionses.True<HT_Menu>();
                where = where.AndAlso(p => p.isMenu >= (int)HT_MenuMenu.MainMenu);
                Expression<Func<HT_Menu, int>> orderBy = p => p.SortId;
                admin_MenuList = menuBO.FindAll<int>(where, orderBy);
            }
        }
    }
}