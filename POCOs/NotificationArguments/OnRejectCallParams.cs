using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class OnRejectCallParams: BaseCallParams
    {
        /// <summary>
        /// SIP код причины отклонения вызова (см. RFC3261).
        /// </summary>
        public Int32 SipCode { get; set; }
        /// <summary>
        /// ISUP код причины отклонения вызова (см. RFC3398).
        /// </summary>
        public Int32 Cause { get; set; }
        /// <summary>
        /// Краткое описание причины отклонения вызова
        /// </summary>
        public String Message { get; set; }
    }
}
