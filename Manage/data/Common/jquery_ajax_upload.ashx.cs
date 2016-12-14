using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Model;
using Framework;
using WebControllers.Handle;
using Framework.Log;

namespace Web.Manage.data.Common
{
    /// <summary>
    /// jquery_ajax_upload 的摘要说明
    /// </summary>
    public class jquery_ajax_upload : BaseHandle
    {
        //private BusinessGW.Upload.UploadImg uploadImg = new BusinessGW.Upload.UploadImg();
        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "UploadImage":
                    re = UploadImage(httpContext);
                    break;
            }
            return re;
        }


        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        private JsonResult UploadImage(HttpContext context)
        {
            JsonResult re = new JsonResult();
            re.Result = Result.fail;
            try
            {
                //HttpFileCollection FileCollection = context.Request.Files;
                //if (FileCollection.Count == 0)
                //{
                //    re.Message = "请选择要上传的图片";
                //    return re;
                //}
                //re = uploadImg.UploadImgCall(FileCollection);
                //re.Data = Utility.ReturnImgUrl(re.Data.ToString());
            }
            catch (Exception ex)
            {
                re.Message = MsgShowConfig.Exception;
                LogService.logDebug(ex);
            }
            return re;
        }
    }
}