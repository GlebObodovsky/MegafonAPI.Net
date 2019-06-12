using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.MethodArguments
{
    public class HasSessionIdArgs: BaseMethodArgs
    {
        /// <summary>
        /// Уникальный идентификатор звонковой сессии.
        /// </summary>
        [JsonProperty("call_session")]
        public Guid SessionId { get; set; }
    }
}