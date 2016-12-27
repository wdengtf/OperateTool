using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class Luck_ActivityJackpotBO : BaseBO<Luck_ActivityJackpot>
    {
        private readonly Luck_ActivityJackpotDAO dalSql = new Luck_ActivityJackpotDAO();
        public Luck_ActivityJackpotBO()
        { }

        public int AddSql(Luck_ActivityJackpot model)
        {
            return dalSql.AddSql(model);
        }

        public CommandInfo AddSqlCommand(Luck_ActivityJackpot model)
        {
            return dalSql.AddSqlCommand(model);
        }
    }
}
