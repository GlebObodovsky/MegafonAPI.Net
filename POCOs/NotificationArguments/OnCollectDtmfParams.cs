using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class OnCollectDtmfParams: BaseCallParams
    {
        /// <summary>
        /// Строка, содержащая сиволы, нажатые пользователем на цифровой клавиатуре телефона (DTMF-коды).
        /// </summary>
        public String Dtmf { get; set; }
    }
}
