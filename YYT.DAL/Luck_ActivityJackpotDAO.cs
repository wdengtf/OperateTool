using Framework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;

namespace YYT.DAL
{
    public class Luck_ActivityJackpotDAO
    {
        private SQLHelperBase SQLHelper = new SQLHelperBase();
        public Luck_ActivityJackpotDAO()
        { }
        /// <summary>
        /// 添加SQL
        /// </summary>

        private CommandParm AddCommandInfo(Luck_ActivityJackpot model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Luck_ActivityJackpot(");
            strSql.Append("channelUserId,ActivityId,PrizeId,Status,Createtime,Ip,data_type,out_id,Updatetime,UpdateAddtime)");
            strSql.Append(" values (");
            strSql.Append("@channelUserId,@ActivityId,@PrizeId,@Status,@Createtime,@Ip,@data_type,@out_id,@Updatetime,@UpdateAddtime)");
            strSql.Append(";select SCOPE_IDENTITY();");
            SqlParameter[] parameters = {
                 new SqlParameter("@channelUserId",SqlDbType.Int,4),
                 new SqlParameter("@ActivityId",SqlDbType.Int,4),
                 new SqlParameter("@PrizeId",SqlDbType.Int,4),
                 new SqlParameter("@Status",SqlDbType.Int,4),
                 new SqlParameter("@Createtime",SqlDbType.DateTime),
                 new SqlParameter("@Ip",SqlDbType.NVarChar,50),
                 new SqlParameter("@data_type",SqlDbType.NVarChar,10),
                 new SqlParameter("@out_id",SqlDbType.NVarChar,50),
                 new SqlParameter("@Updatetime",SqlDbType.DateTime),
                 new SqlParameter("@UpdateAddtime",SqlDbType.DateTime),
             };
            parameters[0].Value = model.channelUserId;
            parameters[1].Value = model.ActivityId;
            parameters[2].Value = model.PrizeId;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Createtime;
            parameters[5].Value = model.Ip;
            parameters[6].Value = model.data_type;
            parameters[7].Value = model.out_id;
            parameters[8].Value = model.Updatetime;
            parameters[9].Value = model.UpdateAddtime;
            return CommandParm.Command(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>

        public Int32 Insert(Luck_ActivityJackpot model)
        {
            CommandParm Comm = AddCommandInfo(model);
            object obj = SQLHelper.GetDataSet(Comm.strSql, Comm.SqlPara);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(((DataSet)obj).Tables[0].Rows[0][0].ToString());
            }
        }
        /// <summary>
        /// 增加一条数据[用于事务处理]
        /// </summary>

        public CommandInfo AddSql(Luck_ActivityJackpot model)
        {
            CommandParm Comm = AddCommandInfo(model);
            if (Comm == null)
                return null;
            return new CommandInfo(Comm.strSql, Comm.SqlPara);
        }
    }
}
