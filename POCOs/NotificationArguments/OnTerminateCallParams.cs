using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class OnTerminateCallParams: BaseCallParams
    {
        public String Message { get; set; }
        public String Cause { get; set; }
    }
}
