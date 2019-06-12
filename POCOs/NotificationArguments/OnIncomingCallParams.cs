using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class OnIncomingCallParams: BaseCallParams
    {
        /// <summary>
        /// Номер абонента, совершающего вызов в формате E.164
        /// </summary>
        public String ANum { get; set; }
        /// <summary>
        /// Номер абонента, принимающего вызов в формате E.164). 
        /// Данный номер соответствует номеру API-подписки, передаваемому при аутентификации разработчика в МегаФон.API.
        /// </summary>
        public String BNum { get; set; }
    }
}
