using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class YYT_MemberBO : BaseBO<YYT_Member>
    {
        private readonly BaseDAO dal = new BaseDAO();
        public YYT_MemberBO()
        { }
    }
}
