using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;
using YYT.DAL;

namespace YYT.BLL.Common
{
    public class SqlBO
    {
        private SQLHelperBase SqlDal = new SQLHelperBase();
        public SqlBO()
        { }

        /// <summary>
        /// 取得DataTable数据
        /// </summary>
        public DataTable GetdataBySql(string sql)
        {
            return SqlDal.GetDataTable(sql);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sql"></param>
        public void RunSql(string sql)
        {
            SqlDal.RunSql(sql);
        }

        #region 事务处理
        /// <summary>
        /// 事务处理
        /// </summary>
        /// <param name="SQLStringList">多个没有逻辑处理的SQL</param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteSqlTran(List<CommandInfo> SQLStringList)
        {
            return SqlDal.ExecuteSqlTran(SQLStringList);
        }

        /// <summary>
        /// 事务处理 主要用于主从表关系 比如 分类表和详细表
        /// </summary>
        /// <param name="mainSql">主SQL 返回值</param>
        /// <param name="otherSQL">子SQL 根据关键词$identity_id 替换主SQL返回的值</param>
        /// <returns>成功返回1 </returns>
        public int ExecuteSqlTran(CommandInfo mainSql, List<CommandInfo> otherSQL)
        {
            return SqlDal.ExecuteSqlTran(mainSql, otherSQL);
        }
        /// <summary>
        /// 事务处理 前面3个SQL返回的值 添加到otherSQL里面
        /// </summary>
        /// <param name="mainSql_1">SQL返回值</param>
        /// <param name="mainSql_2">SQL把mainSql_1返回值替换后 执行</param>
        /// <param name="otherSQL">子SQL 把mainSql_2返回的值替换后 执行</param>
        /// <returns>成功返回1</returns>
        public int ExecuteSqlTran(CommandInfo mainSql_1, CommandInfo mainSql_2, List<CommandInfo> otherSQL)
        {
            return SqlDal.ExecuteSqlTran(mainSql_1, mainSql_2, otherSQL);
        }
        /// <summary>
        /// 事务处理 前面3个SQL返回的值 添加到otherSQL里面
        /// </summary>
        /// <param name="mainSql_1">SQL返回值</param>
        /// <param name="mainSql_2">SQL返回值</param>
        /// <param name="mainSql_3">SQL返回值</param>
        /// <param name="otherSQL">子SQL 把上面返回的值替换后 执行</param>
        /// <returns>成功返回1</returns>
        public int ExecuteSqlTran(CommandInfo mainSql_1, CommandInfo mainSql_2, CommandInfo mainSql_3, List<CommandInfo> otherSQL)
        {
            return SqlDal.ExecuteSqlTran(mainSql_1, mainSql_2, mainSql_3, otherSQL);
        }
        #endregion
    }
}
