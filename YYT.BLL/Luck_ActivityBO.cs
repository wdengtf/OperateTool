using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class Luck_ActivityBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public Luck_ActivityBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Luck_Activity model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Luck_Activity model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<Luck_Activity, bool>> conditions, Expression<Func<Luck_Activity, Luck_Activity>> updateExpression)
        {
            return dal.UpdateByWhere<Luck_Activity, Luck_Activity>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Luck_Activity model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<Luck_Activity, bool>> conditions)
        {
            return dal.DeleteByWhere<Luck_Activity>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Luck_Activity Find(int id)
        {
            return dal.Find<Luck_Activity>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Luck_Activity GetSingle<S>(Expression<Func<Luck_Activity, bool>> conditions, Expression<Func<Luck_Activity, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Luck_Activity, int>> orderById = p => p.Id;
                return dal.GetSingle<Luck_Activity, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<Luck_Activity, S>(conditions, orderBy, direction);
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
        public List<Luck_Activity> FindAll<S>(Expression<Func<Luck_Activity, bool>> conditions, Expression<Func<Luck_Activity, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Luck_Activity, int>> orderById = p => p.Id;
                return dal.FindAll<Luck_Activity, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<Luck_Activity, S>(conditions, orderBy, direction);
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
        public List<Luck_Activity> FindAllByPage<S>(Expression<Func<Luck_Activity, bool>> conditions, Expression<Func<Luck_Activity, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<Luck_Activity, int>> orderById = p => p.Id;
                return dal.FindAllByPage<Luck_Activity, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<Luck_Activity, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
