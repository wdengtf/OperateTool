using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;
using YYT.Model;

namespace Auth
{
    public interface IAuth<T, M> : IEventHandler, IBase<T, M>
        where T : class
        where M : class
    {
        
    }
}
