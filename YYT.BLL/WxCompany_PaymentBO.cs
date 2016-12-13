using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class WxCompany_PaymentBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public WxCompany_PaymentBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(WxCompany_Payment model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(WxCompany_Payment model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<WxCompany_Payment, bool>> conditions, Expression<Func<WxCompany_Payment, WxCompany_Payment>> updateExpression)
        {
            return dal.UpdateByWhere<WxCompany_Payment, WxCompany_Payment>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(WxCompany_Payment model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<WxCompany_Payment, bool>> conditions)
        {
            return dal.DeleteByWhere<WxCompany_Payment>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public WxCompany_Payment Find(int id)
        {
            return dal.Find<WxCompany_Payment>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public WxCompany_Payment GetSingle<S>(Expression<Func<WxCompany_Payment, bool>> conditions, Expression<Func<WxCompany_Payment, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<WxCompany_Payment, int>> orderById = p => p.Id;
                return dal.GetSingle<WxCompany_Payment, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<WxCompany_Payment, S>(conditions, orderBy, direction);
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
        public List<WxCompany_Payment> FindAll<S>(Expression<Func<WxCompany_Payment, bool>> conditions, Expression<Func<WxCompany_Payment, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<WxCompany_Payment, int>> orderById = p => p.Id;
                return dal.FindAll<WxCompany_Payment, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<WxCompany_Payment, S>(conditions, orderBy, direction);
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
        public List<WxCompany_Payment> FindAllByPage<S>(Expression<Func<WxCompany_Payment, bool>> conditions, Expression<Func<WxCompany_Payment, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<WxCompany_Payment, int>> orderById = p => p.Id;
                return dal.FindAllByPage<WxCompany_Payment, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<WxCompany_Payment, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
