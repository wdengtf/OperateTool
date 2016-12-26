using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Validate;
using Framework.Model;
using Framework.Log;
using YYT.Model;
using YYT.BLL;
using Framework;
using System.Linq.Expressions;
using Framework.EF;
using Events;

namespace YYT.Api
{
    /// <summary>
    /// 外部接口调用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OperationBase<T> : IOperation<T> where T : BaseModel
    {
        protected string message = String.Empty;
        protected object date = null;
        protected bool result = false;
        protected T req;
        private QD_ChannelUserBO channelUserBo = new QD_ChannelUserBO();
        protected readonly IEvent events = new Log4NetEvents();


        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="req"></param>
        public void Set(T req)
        {
            this.req = req;
        }

        /// <summary>
        /// 操作失败返回消息
        /// </summary>
        /// <returns></returns>
        public virtual string GetMessage()
        {
            return message;
        }

        /// <summary>
        /// 操作成功返回结果
        /// </summary>
        /// <returns></returns>
        public virtual object GetData()
        {
            return date;
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        public abstract bool Excute();

        /// <summary>
        /// 参数验证
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            bool result = false;
            try
            {
                #region 非空验证
                if (req == null)
                {
                    this.message = MsgShowConfig.ParmNotEmpty;
                    return false;
                }
                var listError = req.IsValid();
                if (listError.Count > 0)
                {
                    this.message = listError[0].Message;
                    return false;
                }
                #endregion

                #region 签名认证
                Expression<Func<QD_ChannelUser, bool>> where = PredicateExtensionses.True<QD_ChannelUser>();
                where = where.AndAlso(p => p.user_name.Equals(req.channelUser));
                QD_ChannelUser channelUserModel = channelUserBo.GetSingle<int>(where);
                if (channelUserModel == null)
                {
                    this.message = "授权用户不存在";
                    return false;
                }
                if (channelUserModel.Status == (int)StatusEnmu.Locking)
                {
                    this.message = "用户已锁定,请和管理员联系";
                    return false;
                }
                if (channelUserModel.end_time.Value <= DateTime.Now)
                {
                    this.message = "用户账号已过期,请和管理员联系";
                    return false;
                }
                IDictionary<string, string> dic;
                try
                {
                    dic = BaseApiModel.CreateSignDictionary(req);
                }
                catch (Exception ex)
                {
                    this.message = ex.Message;
                    return false;
                }
                string sign = Framework.Utils.SignUtil.CreateSign(dic, channelUserModel.user_key);
                if (sign != req.sign)
                {
                    this.message = "签名错误";
                    LogService.LogError("签名错误:" + Utility.ToJson(req));
                    return false;
                }
                #endregion

                result= true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return result;
        }
    }
}
