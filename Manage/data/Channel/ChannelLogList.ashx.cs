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
    /// ChannelLogList 的摘要说明
    /// </summary>
    public class ChannelLogList : BaseHandle
    {
        private QD_ChannelLogBO channelLogBo = new QD_ChannelLogBO();
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
        /// 抽奖列表
        /// </summary>
        /// <returns></returns>
        private JsonResult GetList()
        {
            JsonResult re = new JsonResult();
            try
            {
                int status = Utility.FNumeric("status");
                string channelName = Utility.RF("channelName");
                string failType = Utility.RF("failType");
                string strIp = Utility.RF("ip");
                string beginTime = Utility.RF("beginTime");
                string endTime = Utility.RF("endTime");


                Expression<Func<QD_ChannelLog, bool>> expre = PredicateExtensionses.True<QD_ChannelLog>();
                //if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                //    expre = expre.AndAlso(p => p.channelUserId == manageUserModel.UserId);

                //if (status == 0 || status == 1)
                //    expre = expre.AndAlso(p => p.status == status);
                if (!String.IsNullOrEmpty(channelName))
                    expre = expre.AndAlso(p => p.channelName.Equals(channelName));
                if (!String.IsNullOrEmpty(failType))
                    expre = expre.AndAlso(p => p.failType.Equals(failType));
                if (!String.IsNullOrEmpty(strIp))
                    expre = expre.AndAlso(p => p.ip.Contains(strIp));
                if (!String.IsNullOrEmpty(beginTime))
                {
                    DateTime beginDate = DateTime.Parse(beginTime);
                    expre = expre.AndAlso(p => p.Createtime >= beginDate);
                }
                if (!String.IsNullOrEmpty(endTime))
                {
                    DateTime endDate = DateTime.Parse(endTime).AddDays(1);
                    expre = expre.AndAlso(p => p.Createtime < endDate);
                }
                Expression<Func<QD_ChannelLog, long>> orderBy = p => p.id;
                re = GetListByObject<QD_ChannelLog, long>(expre, channelLogBo, orderBy);
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

                Expression<Func<QD_ChannelLog, bool>> where = PredicateExtensionses.True<QD_ChannelLog>();
                where = where.AndAlso(p => idList.Contains(p.id.ToString()));
                re = DelDataById(channelLogBo, where);
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