using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class Luck_ActivityJackpotBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        private readonly Luck_ActivityJackpotDAO dalSql = new Luck_ActivityJackpotDAO();
        public Luck_ActivityJackpotBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Luck_ActivityJackpot model)
        {
            return dal.Add(model);
        }

        public int AddSql(Luck_ActivityJackpot model)
        {
            return dalSql.AddSql(model);
        }

        public CommandInfo AddSqlCommand(Luck_ActivityJackpot model)
        {
            return dalSql.AddSqlCommand(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Luck_ActivityJackpot model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<Luck_ActivityJackpot, bool>> conditions, Expression<Func<Luck_ActivityJackpot, Luck_ActivityJackpot>> updateExpression)
        {
            return dal.UpdateByWhere<Luck_ActivityJackpot, Luck_ActivityJackpot>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Luck_ActivityJackpot model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<Luck_ActivityJackpot, bool>> conditions)
        {
            return dal.DeleteByWhere<Luck_ActivityJackpot>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Luck_ActivityJackpot Find(int id)
        {
            return dal.Find<Luck_ActivityJackpot>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Luck_ActivityJackpot GetSingle<S>(Expression<Func<Luck_ActivityJackpot, bool>> conditions, Expression<Func<Luck_ActivityJackpot, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Luck_ActivityJackpot, int>> orderById = p => p.id;
                return dal.GetSingle<Luck_ActivityJackpot, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<Luck_ActivityJackpot, S>(conditions, orderBy, direction);
            }
        }
        /// <summary>
        /// 根据条件查找List
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<Luck_ActivityJackpot> FindAll<S>(Expression<Func<Luck_ActivityJackpot, bool>> conditions, Expression<Func<Luck_ActivityJackpot, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Luck_ActivityJackpot, int>> orderById = p => p.id;
                return dal.FindAll<Luck_ActivityJackpot, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<Luck_ActivityJackpot, S>(conditions, orderBy, direction);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <param name="direction"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<Luck_ActivityJackpot> FindAllByPage<S>(Expression<Func<Luck_ActivityJackpot, bool>> conditions, Expression<Func<Luck_ActivityJackpot, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<Luck_ActivityJackpot, int>> orderById = p => p.id;
                return dal.FindAllByPage<Luck_ActivityJackpot, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<Luck_ActivityJackpot, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
