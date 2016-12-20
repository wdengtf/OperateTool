using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YYT.Model;
using YYT.BLL;
using System.Linq.Expressions;
using Framework.EF;
using Framework;

namespace Manage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private YYT_MemberBO memberBO = new YYT_MemberBO();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Utility.ToJson(memberBO.Find(3)) + "<br/>");
            //Response.Write(Utility.ToJson(Delete()) + "<br/>");
            Response.Write(Utility.ToJson(UpdateByWhere()) + "<br/>");
            Response.Write(Utility.ToJson(memberBO.Find(3)) + "<br/>");
        }
        private int Add()
        {
            YYT_Member member = new YYT_Member();
            member.Mobile = "18975831540";
            member.data_type = "GW";
            member.out_id = "18975831540";
            member.Createtime = DateTime.Now;
            return memberBO.Add(member);
        }

        private int Update()
        {
            int id = 1;
            YYT_Member member = memberBO.Find(id);
            member.email = "wdeng@foxmail.com";
            return memberBO.Update(member);
        }

        private int UpdateByWhere()
        {
            Expression<Func<YYT_Member, bool>> where = PredicateExtensionses.True<YYT_Member>();
            where = where.AndAlso(p => p.Mobile.Contains("189"));
            Expression<Func<YYT_Member, YYT_Member>> updateExpression = u2 => new YYT_Member { addr = "9999" };
            return memberBO.UpdateByWhere(where, updateExpression);
        }

        private YYT_Member GetSingle()
        {
            Expression<Func<YYT_Member, bool>> where = PredicateExtensionses.True<YYT_Member>();
            where = where.AndAlso(p => p.Mobile.Contains("189"));
            return memberBO.GetSingle<int>(where);
        }

        private List<YYT_Member> FindAll()
        {
            Expression<Func<YYT_Member, bool>> where = PredicateExtensionses.True<YYT_Member>();
            where = where.AndAlso(p => p.Mobile.Contains("189"));
            Expression<Func<YYT_Member, int>> orderBy = p => p.id;
            return memberBO.FindAll<int>(where, orderBy, "desc");
        }

        private List<YYT_Member> FindAllByPage()
        {
            int total = 0;
            return memberBO.FindAllByPage<int>(null, null, "desc", 1, 10, out total);
        }

        private int Delete()
        {
            int id = 1;
            YYT_Member member = memberBO.Find(id);
            return memberBO.Delete(member);
        }

        private int DeleteByWhere()
        {
            Expression<Func<YYT_Member, bool>> where = PredicateExtensionses.True<YYT_Member>();
            where = where.And(p => p.email.Contains("wdeng"));
            return memberBO.DeleteByWhere(where);
        }
    }
}