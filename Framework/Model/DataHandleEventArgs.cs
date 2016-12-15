using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Model
{
    public class DataHandleEventArgs : EventArgs
    {

        /// <summary>
        /// 操作用户名
        /// </summary>
        public string OperationUserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public OperationTypeEnum OperationType { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string OperationFilePath { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime OperationTime { get; set; }


        public DataHandleEventArgs()
        {
            this.OperationType = OperationTypeEnum.WangclGWInterfaceLog;
            this.OperationTime = DateTime.Now;
            
        }


        public enum OperationTypeEnum
        {
            /// <summary>
            /// 官网接口日志
            /// </summary>
            WangclGWInterfaceLog,
            /// <summary>
            /// 官网调DC接口日志
            /// </summary>
            WangclDCInterfaceLog,
            /// <summary>
            /// 会员表
            /// </summary>
            GW_Member,
        }
    }
}
