using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Log;
using Framework.Model;
using System.Linq.Expressions;
using Framework.EF;
using Framework;
using WebControllers.Handle;
using YYT.Model;
using YYT.BLL;

namespace Web.Manage.data.Member
{
    /// <summary>
    /// memberList 的摘要说明
    /// </summary>
    public class memberList : BaseHandle
    {
        private YYT_MemberBO memberBO = new YYT_MemberBO();
        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "getList":
                    re = GetList();
                    break;
            }
            return re;
        }

        /// <summary>
        /// 会员列表
        /// </summary>
        /// <returns></returns>
        private JsonResult GetList()
        {
            JsonResult re = new JsonResult();
            try
            {
                string mobile = Utility.RF("mobile");
                string outId = Utility.RF("outid");
                string beginTime = Utility.RF("beginTime");
                string endTime = Utility.RF("endTime");

                Expression<Func<YYT_Member, bool>> expre = PredicateExtensionses.True<YYT_Member>();
                if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                    expre = expre.AndAlso(p => p.channelUserId == manageUserModel.UserId);

                if (!String.IsNullOrEmpty(mobile))
                {
                    expre = expre.AndAlso(p => p.Mobile.Equals(mobile));
                }
                if (!String.IsNullOrEmpty(outId))
                {
                    expre = expre.AndAlso(p => p.out_id.Equals(outId));
                }
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

                Expression<Func<YYT_Member, int>> orderBy = p => p.id;
                re = GetListByObject<YYT_Member>(expre, memberBO, orderBy);
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