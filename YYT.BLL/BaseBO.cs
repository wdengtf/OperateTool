using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class BaseBO<T> where T : class
    {
        private readonly BaseDAO dal = new BaseDAO();
        public BaseBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(T model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(T model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<T, bool>> conditions, Expression<Func<T, T>> updateExpression)
        {
            return dal.UpdateByWhere<T, T>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(T model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<T, bool>> conditions)
        {
            return dal.DeleteByWhere<T>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T Find(int id)
        {
            return dal.Find<T>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T GetSingle<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy = null, string direction = "")
        {
            return dal.GetSingle<T, S>(conditions, orderBy, direction);
        }
        /// <summary>
        /// 根据条件查找List
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<T> FindAll<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy = null, string direction = "")
        {
            return dal.FindAll<T, S>(conditions, orderBy, direction);
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
        public List<T> FindAllByPage<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            return dal.FindAllByPage<T, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
        }
    }
}
