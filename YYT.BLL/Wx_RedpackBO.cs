using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class Wx_RedpackBO : BaseBO<Wx_Redpack>
    {
        private readonly BaseDAO dal = new BaseDAO();
        public Wx_RedpackBO()
        { }
    }
}
