using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class HT_MenuBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public HT_MenuBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(HT_Menu model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(HT_Menu model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<HT_Menu, bool>> conditions, Expression<Func<HT_Menu, HT_Menu>> updateExpression)
        {
            return dal.UpdateByWhere<HT_Menu, HT_Menu>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(HT_Menu model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<HT_Menu, bool>> conditions)
        {
            return dal.DeleteByWhere<HT_Menu>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public HT_Menu Find(int id)
        {
            return dal.Find<HT_Menu>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public HT_Menu GetSingle<S>(Expression<Func<HT_Menu, bool>> conditions, Expression<Func<HT_Menu, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<HT_Menu, int>> orderById = p => p.id;
                return dal.GetSingle<HT_Menu, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<HT_Menu, S>(conditions, orderBy, direction);
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
        public List<HT_Menu> FindAll<S>(Expression<Func<HT_Menu, bool>> conditions, Expression<Func<HT_Menu, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<HT_Menu, int>> orderById = p => p.id;
                return dal.FindAll<HT_Menu, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<HT_Menu, S>(conditions, orderBy, direction);
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
        public List<HT_Menu> FindAllByPage<S>(Expression<Func<HT_Menu, bool>> conditions, Expression<Func<HT_Menu, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<HT_Menu, int>> orderById = p => p.id;
                return dal.FindAllByPage<HT_Menu, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<HT_Menu, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
