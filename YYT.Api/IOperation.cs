using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YYT.Model;

namespace YYT.Api
{
    /// <summary>
    /// 外部接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOperation<T, M> : IBase<T, M>
        where T : BaseModel
        where M : class
    {
       
    }
}
