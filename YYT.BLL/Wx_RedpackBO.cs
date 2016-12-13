using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class Wx_RedpackBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public Wx_RedpackBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Wx_Redpack model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Wx_Redpack model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<Wx_Redpack, bool>> conditions, Expression<Func<Wx_Redpack, Wx_Redpack>> updateExpression)
        {
            return dal.UpdateByWhere<Wx_Redpack, Wx_Redpack>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Wx_Redpack model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<Wx_Redpack, bool>> conditions)
        {
            return dal.DeleteByWhere<Wx_Redpack>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Wx_Redpack Find(int id)
        {
            return dal.Find<Wx_Redpack>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public Wx_Redpack GetSingle<S>(Expression<Func<Wx_Redpack, bool>> conditions, Expression<Func<Wx_Redpack, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Wx_Redpack, int>> orderById = p => p.Id;
                return dal.GetSingle<Wx_Redpack, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<Wx_Redpack, S>(conditions, orderBy, direction);
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
        public List<Wx_Redpack> FindAll<S>(Expression<Func<Wx_Redpack, bool>> conditions, Expression<Func<Wx_Redpack, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<Wx_Redpack, int>> orderById = p => p.Id;
                return dal.FindAll<Wx_Redpack, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<Wx_Redpack, S>(conditions, orderBy, direction);
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
        public List<Wx_Redpack> FindAllByPage<S>(Expression<Func<Wx_Redpack, bool>> conditions, Expression<Func<Wx_Redpack, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<Wx_Redpack, int>> orderById = p => p.Id;
                return dal.FindAllByPage<Wx_Redpack, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<Wx_Redpack, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
