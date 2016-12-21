﻿using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public abstract class EventBase
    {
        protected string BaseMessage = "";//提示消息
        protected string OperationUserName = "";//操作用户名
        protected string OperationName = "";//操作名称
        protected string OperationFilePath = "";//操作路径
        protected string Description = "";//操作描述

        public virtual event EventHandler OnBegin;
        public virtual event EventHandler OnCompelete;
        public virtual event EventHandler OnException;
        public virtual event EventHandler OnFail;
        public virtual event EventHandler OnSuccess;
        public virtual event EventHandler OnTipMsg;

        /// <summary>
        /// 默认事件处理
        /// </summary>
        /// <param name="eEvent"></param>
        protected virtual void BaseEvent(EventEnum eEvent)
        {
            DataHandleEventArgs Args = new DataHandleEventArgs();
            Args.OperationUserName = OperationUserName;
            Args.OperationFilePath = OperationFilePath;
            Args.OperationType = MessageObjType.LuckDraw;
            Args.OperationTime = DateTime.Now;

            switch (eEvent)
            {
                case EventEnum.OnBegin:
                    if (this.OnBegin != null)
                    {
                        Args.OperationName = OperationName + "之前";
                        this.OnBegin(this, Args);
                    }
                    break;
                case EventEnum.OnTipMsg:
                    if (this.OnTipMsg != null)
                    {
                        Args.OperationName = OperationName + "提示信息";
                        Args.Description = Description;
                        this.OnTipMsg(this, Args);
                    }
                    break;
                case EventEnum.OnException:
                    if (OnException != null)
                    {
                        Args.OperationName = OperationName + "异常";
                        Args.Description = Description;
                        this.OnException(this, Args);
                    }
                    break;
                case EventEnum.OnSuccess:
                    if (this.OnSuccess != null)
                    {
                        Args.OperationName = OperationName + "成功";
                        Args.Description = Description;
                        this.OnSuccess(this, Args);
                    }
                    break;
                case EventEnum.OnFail:
                    if (this.OnFail != null)
                    {
                        Args.OperationName = OperationName + "失败";
                        Args.Description = Description;
                        this.OnFail(this, Args);
                    }
                    break;
                case EventEnum.OnCompelete:
                    if (OnCompelete != null)
                    {
                        Args.OperationName = OperationName + "完成";
                        this.OnCompelete(this, Args);
                    }
                    break;
            }
        }
    }
}
