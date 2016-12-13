using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class QD_ChannelUserBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public QD_ChannelUserBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(QD_ChannelUser model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(QD_ChannelUser model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<QD_ChannelUser, bool>> conditions, Expression<Func<QD_ChannelUser, QD_ChannelUser>> updateExpression)
        {
            return dal.UpdateByWhere<QD_ChannelUser, QD_ChannelUser>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(QD_ChannelUser model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<QD_ChannelUser, bool>> conditions)
        {
            return dal.DeleteByWhere<QD_ChannelUser>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public QD_ChannelUser Find(int id)
        {
            return dal.Find<QD_ChannelUser>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public QD_ChannelUser GetSingle<S>(Expression<Func<QD_ChannelUser, bool>> conditions, Expression<Func<QD_ChannelUser, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<QD_ChannelUser, int>> orderById = p => p.id;
                return dal.GetSingle<QD_ChannelUser, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<QD_ChannelUser, S>(conditions, orderBy, direction);
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
        public List<QD_ChannelUser> FindAll<S>(Expression<Func<QD_ChannelUser, bool>> conditions, Expression<Func<QD_ChannelUser, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<QD_ChannelUser, int>> orderById = p => p.id;
                return dal.FindAll<QD_ChannelUser, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<QD_ChannelUser, S>(conditions, orderBy, direction);
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
        public List<QD_ChannelUser> FindAllByPage<S>(Expression<Func<QD_ChannelUser, bool>> conditions, Expression<Func<QD_ChannelUser, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<QD_ChannelUser, int>> orderById = p => p.id;
                return dal.FindAllByPage<QD_ChannelUser, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<QD_ChannelUser, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
