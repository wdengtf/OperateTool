using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class YYT_MemberBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public YYT_MemberBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(YYT_Member model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(YYT_Member model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<YYT_Member, bool>> conditions, Expression<Func<YYT_Member, YYT_Member>> updateExpression)
        {
            return dal.UpdateByWhere<YYT_Member, YYT_Member>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(YYT_Member model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<YYT_Member, bool>> conditions)
        {
            return dal.DeleteByWhere<YYT_Member>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public YYT_Member Find(int id)
        {
            return dal.Find<YYT_Member>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public YYT_Member GetSingle<S>(Expression<Func<YYT_Member, bool>> conditions, Expression<Func<YYT_Member, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<YYT_Member, int>> orderById = p => p.Id;
                return dal.GetSingle<YYT_Member, int>(conditions, orderById, direction);
            }
            else {
                return dal.GetSingle<YYT_Member, S>(conditions, orderBy, direction);
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
        public List<YYT_Member> FindAll<S>(Expression<Func<YYT_Member, bool>> conditions, Expression<Func<YYT_Member, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<YYT_Member, int>> orderById = p => p.Id;
                return dal.FindAll<YYT_Member, int>(conditions, orderById, direction);
            }
            else {
                return dal.FindAll<YYT_Member, S>(conditions, orderBy, direction);
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
        public List<YYT_Member> FindAllByPage<S>(Expression<Func<YYT_Member, bool>> conditions, Expression<Func<YYT_Member, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<YYT_Member, int>> orderById = p => p.Id;
                return dal.FindAllByPage<YYT_Member, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else {
                return dal.FindAllByPage<YYT_Member, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
