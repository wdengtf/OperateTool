using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;

namespace MSMQ
{
    public interface ISend
    {
        bool Send(MessageModel messageModel);

        bool BatchSend(List<MessageModel> messageModelList);
    }
}
