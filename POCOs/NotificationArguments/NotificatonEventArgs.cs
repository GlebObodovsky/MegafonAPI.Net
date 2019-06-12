using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class NotificatonEventArgs<T>: EventArgs
        where T: BaseParams
    {
        public T Params { get; set; } 
        public String Method { get; set; }
        public String JsonRpc { get; set; }
    }
}
