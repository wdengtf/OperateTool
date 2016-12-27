using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;

namespace YYT.DAL
{
    public class SQLHelperBase
    {
        private readonly static string Str_conn = ConfigurationManager.AppSettings["Sqlconn"].ToString();

        #region 数据库操作类
        /// <summary>
        /// 数据库操作类[返回受影响行数]
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecSQL(string sql)
        {
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                int count = 0;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    count = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return count;
            }
        }

        /// <summary>
        /// 数据库操作类,带参数[返回受影响行数]
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public virtual int ExecSQL(string sql, SqlParameter[] sp)
        {
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                int count = 0;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    foreach (SqlParameter para in sp)
                    {
                        cmd.Parameters.Add(para);
                    }
                    count = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return count;
            }
        }


        /// <summary>
        /// 数据库操作类,带参数[返回成功失败]
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual bool ExecCmd(ref SqlCommand cmd)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        #endregion

        #region 返回DataSet
        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(string sql)
        {
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    sda.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return ds;
            }
        }

        /// <summary>
        /// GetDataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(string sql, SqlParameter[] sp)
        {
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    foreach (SqlParameter para in sp)
                    {
                        cmd.Parameters.Add(para);
                    }

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
                return ds;
            }
        }
        #endregion

        #region 返回DataTable
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sSQL">查询语句</param>
        /// <returns></returns>
        public virtual DataTable GetDataTable(string sSQL)
        {
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                try
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sSQL, conn);
                    da.SelectCommand.CommandTimeout = 180;
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region 执行sql语句并不返回值
        /// <summary>
        /// 执行sql语句并不返回值
        /// </summary>
        /// <param name="sSQL"></param>
        public virtual void RunSql(string sSQL)
        {
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                try
                {
                    conn.Open();
                    SqlCommand SqlCommand1 = new SqlCommand(sSQL, conn);
                    SqlCommand1.ExecuteReader();
                    SqlCommand1.Dispose();
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region 填充到数据适配器
        /// <summary>
        /// 填充到数据适配器
        /// </summary>
        /// <param name="sSQL">查询语句</param>
        /// <returns></returns>
        public virtual SqlDataReader GetDataReader(string sSQL)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Str_conn);
                SqlCommand cmd = new SqlCommand(sSQL, conn);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 事务处理
        /// <summary>
        /// 事务处理
        /// </summary>
        /// <param name="cmdList">多个没有逻辑处理的SQL</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;
                                if (obj == null && obj == DBNull.Value)
                                {
                                    isHave = false;
                                }
                                isHave = Convert.ToInt32(obj) > 0;

                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                continue;
                            }
                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("事务异常", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 事务处理 主要用于主从表关系 比如 分类表和详细表
        /// </summary>
        /// <param name="mainSql">主SQL 返回值</param>
        /// <param name="otherSQL">子SQL 根据关键词$identity_id 替换主SQL返回的值</param>
        /// <returns>成功返回1 </returns>
        public virtual int ExecuteSqlTran(CommandInfo mainSql, List<CommandInfo> otherSQL)
        {
            int result = 0;
            string logSQL = string.Empty;
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = tran;

                        logSQL = mainSql.CommandText;

                        string cmdText = mainSql.CommandText;
                        SqlParameter[] cmdParms = (SqlParameter[])mainSql.Parameters;
                        PrepareCommand(cmd, conn, tran, cmdText, cmdParms);
                        object id = cmd.ExecuteScalar();
                        id = id == null ? "0" : id;

                        foreach (CommandInfo sql in otherSQL)
                        {
                            logSQL = sql.CommandText;
                            cmd.Parameters.Clear();
                            String commandText = sql.CommandText.Replace("$identity_id", "\'" + id.ToString() + "\'");
                            cmdParms = (SqlParameter[])sql.Parameters;
                            PrepareCommand(cmd, conn, tran, commandText, cmdParms);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception("事务异常", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            return result;
        }


        /// <summary>
        /// 事务处理 主要用于主从表关系 比如 分类表和详细表
        /// </summary>
        /// <param name="mainSql">主SQL 返回值</param>
        /// <param name="mainSql_2">第二个SQL 依赖第一个SQL返回值</param>
        /// <param name="otherSQL">子SQL 根据关键词$identity_id 替换主SQL返回的值</param>
        /// <returns>成功返回1 </returns>
        public virtual int ExecuteSqlTran(CommandInfo mainSql, CommandInfo mainSql_2, List<CommandInfo> otherSQL)
        {
            int result = 0;
            string logSQL = string.Empty;
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = tran;

                        logSQL = mainSql.CommandText;

                        string cmdText = mainSql.CommandText;
                        SqlParameter[] cmdParms = (SqlParameter[])mainSql.Parameters;
                        PrepareCommand(cmd, conn, tran, cmdText, cmdParms);
                        object id = cmd.ExecuteScalar();
                        id = id == null ? "0" : id;

                        object value_2 = "0";
                        if (!string.IsNullOrEmpty(mainSql_2.CommandText))
                        {
                            logSQL = mainSql_2.CommandText;
                            cmdText = mainSql_2.CommandText.Replace("$identity_id", "\'" + id.ToString() + "\'");
                            cmdParms = (SqlParameter[])mainSql_2.Parameters;
                            PrepareCommand(cmd, conn, tran, cmdText, cmdParms);
                            value_2 = cmd.ExecuteScalar();
                        }
                        value_2 = value_2 == null ? "0" : value_2;

                        foreach (CommandInfo sql in otherSQL)
                        {
                            logSQL = sql.CommandText;
                            cmd.Parameters.Clear();
                            String commandText = sql.CommandText.Replace("$identity_id", "\'" + value_2.ToString() + "\'");
                            cmdParms = (SqlParameter[])sql.Parameters;
                            PrepareCommand(cmd, conn, tran, commandText, cmdParms);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception("事务异常", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            return result;
        }


        /// <summary>
        /// 事务处理 前面3个SQL返回的值 添加到otherSQL里面
        /// </summary>
        /// <param name="mainSql_1">SQL返回值</param>
        /// <param name="mainSql_2">SQL返回值</param>
        /// <param name="mainSql_3">SQL返回值</param>
        /// <param name="otherSQL">子SQL 把上面返回的值替换后 执行</param>
        /// <returns>成功返回1</returns>
        public virtual int ExecuteSqlTran(CommandInfo mainSql_1, CommandInfo mainSql_2, CommandInfo mainSql_3, List<CommandInfo> otherSQL)
        {
            int result = 0;
            string logSQL = string.Empty;//用来记录是哪条SQL异常
            using (SqlConnection conn = new SqlConnection(Str_conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = tran;

                        logSQL = mainSql_1.CommandText;

                        string cmdText = mainSql_1.CommandText;
                        SqlParameter[] cmdParms = (SqlParameter[])mainSql_1.Parameters;
                        PrepareCommand(cmd, conn, tran, cmdText, cmdParms);
                        object value_1 = cmd.ExecuteScalar();

                        object value_2 = "0", value_3 = "0";
                        if (!string.IsNullOrEmpty(mainSql_2.CommandText))
                        {
                            logSQL = mainSql_2.CommandText;
                            cmdText = mainSql_2.CommandText;
                            cmdParms = (SqlParameter[])mainSql_2.Parameters;
                            PrepareCommand(cmd, conn, tran, cmdText, cmdParms);
                            value_2 = cmd.ExecuteScalar();
                        }
                        value_2 = value_2 == null ? "0" : value_2;

                        if (!string.IsNullOrEmpty(mainSql_3.CommandText))
                        {
                            logSQL = mainSql_3.CommandText;
                            cmdText = mainSql_3.CommandText;
                            cmdParms = (SqlParameter[])mainSql_3.Parameters;
                            PrepareCommand(cmd, conn, tran, cmdText, cmdParms);
                            value_3 = cmd.ExecuteScalar();
                        }
                        value_3 = value_3 == null ? "0" : value_3;

                        foreach (CommandInfo sql in otherSQL)
                        {
                            logSQL = sql.CommandText;
                            cmd.Parameters.Clear();
                            String commandText = sql.CommandText.Replace("$identity_value_1", "\'" + value_1.ToString() + "\'");

                            commandText = commandText.Replace("$identity_value_2", "\'" + value_2.ToString() + "\'");

                            commandText = commandText.Replace("$identity_value_3", "\'" + value_3.ToString() + "\'");


                            cmdParms = (SqlParameter[])sql.Parameters;
                            PrepareCommand(cmd, conn, tran, commandText, cmdParms);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception("事务异常", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            return result;
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion
    }
}
