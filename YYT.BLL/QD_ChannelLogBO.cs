using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class QD_ChannelLogBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public QD_ChannelLogBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(QD_ChannelLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(QD_ChannelLog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<QD_ChannelLog, bool>> conditions, Expression<Func<QD_ChannelLog, QD_ChannelLog>> updateExpression)
        {
            return dal.UpdateByWhere<QD_ChannelLog, QD_ChannelLog>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(QD_ChannelLog model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<QD_ChannelLog, bool>> conditions)
        {
            return dal.DeleteByWhere<QD_ChannelLog>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public QD_ChannelLog Find(int id)
        {
            return dal.Find<QD_ChannelLog>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public QD_ChannelLog GetSingle<S>(Expression<Func<QD_ChannelLog, bool>> conditions, Expression<Func<QD_ChannelLog, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<QD_ChannelLog, long>> orderById = p => p.id;
                return dal.GetSingle<QD_ChannelLog, long>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<QD_ChannelLog, S>(conditions, orderBy, direction);
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
        public List<QD_ChannelLog> FindAll<S>(Expression<Func<QD_ChannelLog, bool>> conditions, Expression<Func<QD_ChannelLog, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<QD_ChannelLog, long>> orderById = p => p.id;
                return dal.FindAll<QD_ChannelLog, long>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<QD_ChannelLog, S>(conditions, orderBy, direction);
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
        public List<QD_ChannelLog> FindAllByPage<S>(Expression<Func<QD_ChannelLog, bool>> conditions, Expression<Func<QD_ChannelLog, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<QD_ChannelLog, long>> orderById = p => p.id;
                return dal.FindAllByPage<QD_ChannelLog, long>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<QD_ChannelLog, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
