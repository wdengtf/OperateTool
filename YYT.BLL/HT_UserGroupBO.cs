using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class HT_UserGroupBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public HT_UserGroupBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(HT_UserGroup model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(HT_UserGroup model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<HT_UserGroup, bool>> conditions, Expression<Func<HT_UserGroup, HT_UserGroup>> updateExpression)
        {
            return dal.UpdateByWhere<HT_UserGroup, HT_UserGroup>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(HT_UserGroup model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<HT_UserGroup, bool>> conditions)
        {
            return dal.DeleteByWhere<HT_UserGroup>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public HT_UserGroup Find(int id)
        {
            return dal.Find<HT_UserGroup>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public HT_UserGroup GetSingle<S>(Expression<Func<HT_UserGroup, bool>> conditions, Expression<Func<HT_UserGroup, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<HT_UserGroup, int>> orderById = p => p.id;
                return dal.GetSingle<HT_UserGroup, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<HT_UserGroup, S>(conditions, orderBy, direction);
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
        public List<HT_UserGroup> FindAll<S>(Expression<Func<HT_UserGroup, bool>> conditions, Expression<Func<HT_UserGroup, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<HT_UserGroup, int>> orderById = p => p.id;
                return dal.FindAll<HT_UserGroup, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<HT_UserGroup, S>(conditions, orderBy, direction);
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
        public List<HT_UserGroup> FindAllByPage<S>(Expression<Func<HT_UserGroup, bool>> conditions, Expression<Func<HT_UserGroup, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<HT_UserGroup, int>> orderById = p => p.id;
                return dal.FindAllByPage<HT_UserGroup, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<HT_UserGroup, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
