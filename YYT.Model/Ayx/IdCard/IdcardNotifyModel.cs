using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYT.Model.Ayx.IdCard
{
    public class IdcardNotifyModel
    {
        /// <summary>
        /// 是否查询成功 
        /// 0：查询失败 ， 1：查询成功
        /// </summary>
        public int isok { get; set; }

        /// <summary>
        /// 查询结果，详细code含义见下方表格
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 身份证包含信息
        /// </summary>
        public IdCardDataModel data { get; set; }
    }

    public class IdCardDataModel
    {
        /// <summary>
        /// 是否符合身份证号码格式
        /// 0：符合 ， -1：不符合
        /// </summary>
        public string err { get; set; }

        /// <summary>
        /// unicode格式身份证所在地 （err:-1时无此结果）
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 性别（err:-1时无此结果）
        /// M：男性 ， F：女性
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 生日信息（err:-1时无此结果）
        /// </summary>
        public string birthday { get; set; }
    }
}
