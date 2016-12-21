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
using System.Data;
using YYT.BLL.Common;
using Framework.Cookies;

namespace WebControllers.Handle
{
    /// <summary>
    /// 后台Ashx页面
    /// </summary>
    public abstract class BaseHandle : IHttpHandler, IRequiresSessionState
    {
        private string data, sign;
        protected string message = String.Empty;
        protected string action = String.Empty;
        protected string defaultSort = "desc";//默认排序

        protected ManageUserModel manageUserModel = null;
        protected AdminUser adminUser = new AdminUser();
        protected CommonPage clspage = new CommonPage();
        protected CookieHandle cookieHandle = new CookieHandle();
        protected int jumpDroitGroupId = 0;

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
                LogService.LogDebug(ex);
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
                jumpDroitGroupId = ConfigBL.JumpDroitGroupId();
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
        /// 根据对象返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expre"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        protected virtual JsonResult GetListByObject<T>(Expression<Func<T, bool>> expre, BaseBO<T> t, Expression<Func<T, int>> orderBy) where T : class
        {
            return GetListByObject<T,int>(expre, t, orderBy);
        }

        protected virtual JsonResult GetListByObject<T,S>(Expression<Func<T, bool>> expre, BaseBO<T> t, Expression<Func<T, S>> orderBy) where T : class
        {
            JsonResult re = new JsonResult();
            try
            {
                if (orderBy == null)
                    return JsonResult.FailResult("排序" + MsgShowConfig.ParmNotEmpty);

                if (expre == null)
                    expre = PredicateExtensionses.True<T>();

                int pageIndex = Utility.FNumeric("page") == 0 ? 1 : Utility.FNumeric("page");
                int pageSize = Utility.FNumeric("rows") == 0 ? 20 : Utility.FNumeric("rows");
                int totalRecord = 0;

                List<T> list = t.FindAllByPage<S>(expre, orderBy, defaultSort, pageIndex, pageSize, out totalRecord);
                int totalPage = GetTotalPage(totalRecord, pageSize);
                JqGridPagingModel<List<T>> jqGridPagingModel = new JqGridPagingModel<List<T>>(pageIndex, totalPage, totalRecord, list);
                re = JsonResult.SuccessResult(jqGridPagingModel);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }

        /// <summary>
        /// 根据SQL返回列表
        /// </summary>
        /// <param name="clspage"></param>
        /// <returns></returns>
        protected virtual JsonResult GetListBySql(CommonPage clspage)
        {
            JsonResult re = new JsonResult();
            try
            {
                int pageIndex = Utility.FNumeric("page") == 0 ? 1 : Utility.FNumeric("page");
                int pageSize = Utility.FNumeric("rows") == 0 ? 20 : Utility.FNumeric("rows");
                clspage.PageSize = pageSize;

                int totalRecord = 0;
                DataTable dt = clspage.getDataByPage(pageIndex, out totalRecord);
                int totalPage = GetTotalPage(totalRecord, pageSize);

                JqGridPagingModel<DataTable> jqGridPagingModel = new JqGridPagingModel<DataTable>(pageIndex, totalPage, totalRecord, dt);
                re = JsonResult.SuccessResult(jqGridPagingModel);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idStr"></param>
        /// <returns></returns>
        protected virtual JsonResult DelDataById<T>(BaseBO<T> t, Expression<Func<T, bool>> where) where T : class
        {
            JsonResult re = new JsonResult();
            try
            {
                if (where == null)
                    return JsonResult.FailResult("删除条件" + MsgShowConfig.ParmNotEmpty);

                if (t.DeleteByWhere(where) > 0)
                {
                    re = JsonResult.SuccessResult(MsgShowConfig.Success);
                }
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private int GetTotalPage(int totalRecord, int pageSize)
        {
            return totalRecord > ((totalRecord / pageSize) * pageSize) ? (totalRecord / pageSize) + 1 : (totalRecord / pageSize);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
