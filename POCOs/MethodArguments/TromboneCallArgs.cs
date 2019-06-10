using Newtonsoft.Json;
using System;

namespace MegafonAPINet.POCOs.MethodArguments
{
    public class TromboneCallArgs: BaseMethodParams
    {
        /// <summary>
        /// Уникальный номер сессии для объединения. Возвращается методом MakeCall или событием OnIncomingCall.
        /// </summary>
        [JsonProperty("a_session")]
        public Guid ASessionId { get; set; }
        /// <summary>
        /// Уникальный номер сессии для объединения. Возвращается методом MakeCall или событием OnIncomingCall.
        /// </summary>
        [JsonProperty("b_session")]
        public Guid BSessionId { get; set; }
    }
}
