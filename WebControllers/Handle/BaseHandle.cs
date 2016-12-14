using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Framework;
using Framework.Log;
using Framework.Model;
using Framework.Validate;
using WebControllers.Model;
using WebControllers.Admin;
using System.Linq.Expressions;
using Framework.EF;
using YYT.BLL;
using YYT.Model;

namespace WebControllers.Handle
{
    public abstract class BaseHandle : IHttpHandler, IRequiresSessionState
    {
        private string data, sign;
        protected string message = String.Empty;
        protected string action = String.Empty;
        protected string defaultSort = "desc";//默认排序

        protected ManageUserModel manageUserModel = null;
        protected AdminUser adminUser = new AdminUser();

        protected HttpContext httpContext = null;
        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            JsonResult re = new JsonResult();
            try
            {
                OnInit();
                httpContext = context;

                //登录验证
                if (!MemberValidate())
                {
                    re = JsonResult.FailResult(GetMessage());
                }
                else
                {
                    re = HandleProcess();
                }
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.EmptyFunction);
                LogService.logDebug(ex);
            }
            context.Response.Write(Utility.ToJson(re));
        }

        public abstract JsonResult HandleProcess();

        /// <summary>
        /// 参数初始化
        /// </summary>
        protected void OnInit()
        {
            try
            {
                data = Utility.RF("data");
                sign = Utility.RF("sign");
                action = Utility.RF("action");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        protected string GetData()
        {
            return this.data;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected int GetTotalPage(int totalRecord,int pageSize)
        {
            return totalRecord > ((totalRecord / pageSize) * pageSize) ? (totalRecord / pageSize) + 1 : (totalRecord / pageSize);
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <returns></returns>
        protected bool SignValidate()
        {
            return true;
        }

        /// <summary>
        /// 获取提示消息
        /// </summary>
        /// <returns></returns>
        protected string GetMessage()
        {
            return this.message;
        }

        /// <summary>
        /// 参数验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        protected bool Validate<T>(T t)
        {
            #region 参数验证
            if (t == null)
            {
                message = "数据错误";
                return false;
            }
            List<BrokenRule> ErrorMessage = t.IsValid();
            if (ErrorMessage.Count > 0)
            {
                this.message = ErrorMessage[0].Message;
                return false;
            }
            return true;
            #endregion
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        private bool MemberValidate()
        {
            bool _flag = false;

            #region 登录信息设置
            if (!adminUser.CheckLogin())
            {
                this.message = MsgShowConfig.NoLogin;
                return _flag;
            }
            manageUserModel = adminUser.GetManageUserModel();
            #endregion
            _flag = true;
            return _flag;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expre"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        //protected virtual JsonResult GetList<T>(Expression<Func<T, bool>> expre, HandleCall<T> k) where T : class
        //{
        //    JsonResult re = new JsonResult();
        //    try
        //    {
        //        int pageIndex = Utility.FNumeric("page");
        //        int pageSize = Utility.FNumeric("rows") == 0 ? 10 : Utility.FNumeric("rows");
        //        int totalRecord = 0;

        //        List<T> list = k.GetPage(expre, pageIndex, pageSize, sortField, defaultSort, out totalRecord);

        //        int totalPage = GetTotalPage(totalRecord, pageSize);
        //        JqGridPagingModel<T> jqGridPagingModel = new JqGridPagingModel<T>(pageIndex, totalPage, totalRecord, list);
        //        re = JsonResult.SuccessResult(jqGridPagingModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        re = JsonResult.FailResult(MsgShowConfig.Exception);
        //        LogService.logDebug(ex);
        //    }
        //    return re;
        //}

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idStr"></param>
        /// <returns></returns>
        //protected virtual JsonResult DelData<T>(BaseBO<T> t) where T : class
        //{
        //    JsonResult re = new JsonResult();
        //    try
        //    {
        //        string idStr = Utility.RF("id");
        //        if (String.IsNullOrEmpty(idStr))
        //        {
        //            return JsonResult.FailResult(MsgShowConfig.ParmNotEmpty);
        //        }

        //        List<string> list = new List<string>(idStr.Split(','));
        //        Expression<Func<T, bool>> where = PredicateExtensionses.True<T>();
        //        where = where.AndAlso(p => list.Contains(p.id.ToString()));
        //        if (t.DeleteByWhere(where) > 0)
        //        {
        //            re = JsonResult.SuccessResult(MsgShowConfig.Success);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        re = JsonResult.FailResult(MsgShowConfig.Exception);
        //        LogService.logDebug(ex);
        //    }
        //    return re;
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
