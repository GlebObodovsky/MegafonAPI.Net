using Newtonsoft.Json;
using System;

namespace MegafonAPINet.POCOs.MethodArguments
{
    public class TerminateCallArgs : BaseMethodParams
    {
        /// <summary>
        /// Уникальный идентификатор звонковой сессии.
        /// </summary>
        [JsonProperty("call_session")]
        public Guid SessionId { get; set; }
    }
}
