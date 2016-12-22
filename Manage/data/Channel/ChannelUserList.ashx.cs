using Framework;
using Framework.EF;
using Framework.Log;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Manage.data.Channel
{
    /// <summary>
    /// ChannelList 的摘要说明
    /// </summary>
    public class ChannelList : BaseHandle
    {
        private QD_ChannelUserBO channelUserBo = new QD_ChannelUserBO();
        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "getList":
                    re = GetList();
                    break;
                case "delData":
                    re = DelData();
                    break;
            }
            return re;
        }

        /// <summary>
        /// 渠道会员列表
        /// </summary>
        /// <returns></returns>
        private JsonResult GetList()
        {
            JsonResult re = new JsonResult();
            try
            {
                int status = Utility.FNumeric("status");
                string name = Utility.RF("name");
                string beginTime = Utility.RF("beginTime");
                string endTime = Utility.RF("endTime");

                Expression<Func<QD_ChannelUser, bool>> expre = PredicateExtensionses.True<QD_ChannelUser>();
                if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                    expre = expre.AndAlso(p => p.id == manageUserModel.UserId);

                if (status == 0 || status == 1)
                    expre = expre.AndAlso(p => p.Status == status);
                if (!String.IsNullOrEmpty(name))
                    expre = expre.AndAlso(p => p.user_name.Equals(name));
                if (!String.IsNullOrEmpty(beginTime))
                {
                    DateTime beginDate = DateTime.Parse(beginTime);
                    expre = expre.AndAlso(p => p.end_time >= beginDate);
                }
                if (!String.IsNullOrEmpty(endTime))
                {
                    DateTime endDate = DateTime.Parse(endTime).AddDays(1);
                    expre = expre.AndAlso(p => p.end_time < endDate);
                }
                re = GetListByObject<QD_ChannelUser>(expre, channelUserBo, p => p.id);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }


        /// <summary>
        ///  删除数据
        /// </summary>
        /// <returns></returns>
        private JsonResult DelData()
        {
            JsonResult re = new JsonResult();
            try
            {
                string idStr = Utility.RF("id");
                if (String.IsNullOrEmpty(idStr))
                {
                    return JsonResult.FailResult(MsgShowConfig.ParmNotEmpty);
                }
                List<string> idList = new List<string>(idStr.Split(','));

                Expression<Func<QD_ChannelUser, bool>> where = PredicateExtensionses.True<QD_ChannelUser>();
                where = where.AndAlso(p => idList.Contains(p.id.ToString()));
                re = DelDataById(channelUserBo, where);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }
    }
}