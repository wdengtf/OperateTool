using Framework;
using Framework.Handle;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Api.Base;
using YYT.Model.Auth;

namespace Web.Api.Api
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string strApiName = ddl_interface.SelectedItem.Value;
            string strUserName = txt_UserName.Text.Trim();
            string strKey = txt_Key.Text.Trim();
            string strParm = txt_Parm.Text.Trim();

            string strUrl = Request.Url.ToString().Replace(Request.Path.ToString(), "") + "/api/" + strApiName;
            IDictionary<String, String> param = new Dictionary<String, String>();
            try
            {
                switch (strApiName)
                {
                    case "WxServerAuth":
                        WxServerAuthModel wxServerAuthModel = new WxServerAuthModel();
                        wxServerAuthModel.channelUser = strUserName;
                        param = ConvertObj.ModelToIDictionary<WxServerAuthModel>(wxServerAuthModel);
                        wxServerAuthModel.channelSign = SignUtil.CreateSign(param, strKey);

                        txt_ResultJson.Text = Utility.ToJson(wxServerAuthModel);
                        txt_Result.Text = new WebUtils().DoPost(strUrl, txt_ResultJson.Text, "json");
                        break;
                }
            }
            catch (Exception ex)
            {
                txt_Result.Text = ex.ToString();
            }
        }
    }
}