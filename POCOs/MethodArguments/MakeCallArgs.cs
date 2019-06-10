using Newtonsoft.Json;
using System;

namespace MegafonAPINet.POCOs.MethodArguments
{
    public class MakeCallArgs: BaseMethodParams
    {
        /// <summary>
        /// Номер абонента, совершающего вызов или отправляющего SMS-сообщение, в формате E.164
        /// Данный номер соответствует номеру API-подписки, передаваемому при аутентификации разработчика в МегаФон.API. 
        /// Не обязательный параметр, если в учетной записи разработчика только одна API-подписка, особенно удобно его не указывать при авторизации через токен.
        /// </summary>
        [JsonProperty("anum")]
        public String CallingNumber { get; set; }
        /// <summary>
        /// Номер вызываемого абонента в формате E.164
        /// </summary>
        [JsonProperty("bnum")]
        public String NumberToReach { get; set; }
    }
}
