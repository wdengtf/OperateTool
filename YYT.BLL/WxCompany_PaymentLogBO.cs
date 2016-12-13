using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YYT.DAL;
using YYT.Model;

namespace YYT.BLL
{
    public class WxCompany_PaymentLogBO
    {
        private readonly BaseDAO dal = new BaseDAO();
        public WxCompany_PaymentLogBO()
        { }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(WxCompany_PaymentLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(WxCompany_PaymentLog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int UpdateByWhere(Expression<Func<WxCompany_PaymentLog, bool>> conditions, Expression<Func<WxCompany_PaymentLog, WxCompany_PaymentLog>> updateExpression)
        {
            return dal.UpdateByWhere<WxCompany_PaymentLog, WxCompany_PaymentLog>(conditions, updateExpression);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(WxCompany_PaymentLog model)
        {
            return dal.Delete(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteByWhere(Expression<Func<WxCompany_PaymentLog, bool>> conditions)
        {
            return dal.DeleteByWhere<WxCompany_PaymentLog>(conditions);
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public WxCompany_PaymentLog Find(int id)
        {
            return dal.Find<WxCompany_PaymentLog>(id);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public WxCompany_PaymentLog GetSingle<S>(Expression<Func<WxCompany_PaymentLog, bool>> conditions, Expression<Func<WxCompany_PaymentLog, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<WxCompany_PaymentLog, int>> orderById = p => p.Id;
                return dal.GetSingle<WxCompany_PaymentLog, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.GetSingle<WxCompany_PaymentLog, S>(conditions, orderBy, direction);
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
        public List<WxCompany_PaymentLog> FindAll<S>(Expression<Func<WxCompany_PaymentLog, bool>> conditions, Expression<Func<WxCompany_PaymentLog, S>> orderBy = null, string direction = "")
        {
            if (orderBy == null)
            {
                Expression<Func<WxCompany_PaymentLog, int>> orderById = p => p.Id;
                return dal.FindAll<WxCompany_PaymentLog, int>(conditions, orderById, direction);
            }
            else
            {
                return dal.FindAll<WxCompany_PaymentLog, S>(conditions, orderBy, direction);
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
        public List<WxCompany_PaymentLog> FindAllByPage<S>(Expression<Func<WxCompany_PaymentLog, bool>> conditions, Expression<Func<WxCompany_PaymentLog, S>> orderBy, string direction, int pageIndex, int pageSize, out int totalRecord)
        {
            if (orderBy == null)
            {
                Expression<Func<WxCompany_PaymentLog, int>> orderById = p => p.Id;
                return dal.FindAllByPage<WxCompany_PaymentLog, int>(conditions, orderById, direction, pageIndex, pageSize, out totalRecord);
            }
            else
            {
                return dal.FindAllByPage<WxCompany_PaymentLog, S>(conditions, orderBy, direction, pageIndex, pageSize, out totalRecord);
            }
        }
    }
}
