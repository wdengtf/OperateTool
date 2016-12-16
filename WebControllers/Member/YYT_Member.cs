using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using Framework.Cookies;
using Framework.Model;
using Framework.Utils;

namespace WebControllers.Member
{
    public class YYT_Member
    {
        //会员Cookies名称
        private static string MemberCookiesName = "member";
        private static int MemberCookiesExpires = 24 * 30;//一个月

        public YYT_Member()
        { }


        /// <summary>
        /// 保存账号信息到Cookies
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="lists">账号List</param>
        /// <param name="out_id">登录账号out_id</param>
        /// <returns></returns>
        public bool SaveMemberCookies(MemberBaseModel memberBaseModel)
        {
            bool _flag = false;
            try
            {
                CookieHandle cookies = new CookieHandle(MemberCookiesName, SignUtil.Encrypt(Utility.ToJson(memberBaseModel)), MemberCookiesExpires);
                _flag = cookies.SetCookies();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return _flag;
        }

        /// <summary>
        /// 从Cookies获取账号信息
        /// </summary>
        /// <returns></returns>
        public MemberBaseModel GetMemberCookies()
        {
            #region 新
            MemberBaseModel memberBaseModel = null;
            try
            {
                CookieHandle cookies = new CookieHandle();
                string CookiesValue = cookies.GetCookies(MemberCookiesName);

                if (String.IsNullOrEmpty(CookiesValue))
                    return memberBaseModel;

                memberBaseModel = new MemberBaseModel();
                memberBaseModel = Utility.JsonToObject<MemberBaseModel>(SignUtil.Decrypt(CookiesValue));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return memberBaseModel;
            #endregion

        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        public bool MemberValidate(MemberBaseModel memberBaseModel)
        {
            bool _flag = false;
            if (memberBaseModel == null)
            {
                memberBaseModel = new MemberBaseModel()
                {
                    data_type = "GW",
                    out_id = "o_jctwEKMWg5XKRGFzjCDzIYnb-I",
                };
                new WebControllers.Member.YYT_Member().SaveMemberCookies(memberBaseModel);
                //return _flag;
            }
            _flag = true;
            return _flag;
        }
    }
}
