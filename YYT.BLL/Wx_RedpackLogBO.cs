using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class Wx_RedpackLogBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public Wx_RedpackLogBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Wx_RedpackLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Wx_RedpackLog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<Wx_RedpackLog, bool>> conditions, Expression<Func<Wx_RedpackLog, Wx_RedpackLog>> updateExpression)
        {
            return dal.UpdateByWhere<Wx_RedpackLog, Wx_RedpackLog>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Wx_RedpackLog model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<Wx_RedpackLog, bool>> conditions)
        {
            return dal.DeleteByWhere<Wx_RedpackLog>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Wx_RedpackLog Find(int id)
        {
            return dal.Find<Wx_RedpackLog>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Wx_RedpackLog GetSingle<S>(Expression<Func<Wx_RedpackLog, bool>> conditions, Expression<Func<Wx_RedpackLog, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Wx_RedpackLog, int>> orderById = p => p.Id;
                return dal.GetSingle<Wx_RedpackLog, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<Wx_RedpackLog, S>(conditions, orderBy, direction);
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
        public List<Wx_RedpackLog> FindAll<S>(Expression<Func<Wx_RedpackLog, bool>> conditions, Expression<Func<Wx_RedpackLog, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Wx_RedpackLog, int>> orderById = p => p.Id;
                return dal.FindAll<Wx_RedpackLog, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<Wx_RedpackLog, S>(conditions, orderBy, direction);
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
        public List<Wx_RedpackLog> FindAllByPage<S>(Expression<Func<Wx_RedpackLog, bool>> conditions, Expression<Func<Wx_RedpackLog, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<Wx_RedpackLog, int>> orderById = p => p.Id;
                return dal.FindAllByPage<Wx_RedpackLog, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<Wx_RedpackLog, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
