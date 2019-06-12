using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class BaseCallParams: BaseParams
    {
        [JsonProperty("call_session")]
        public Guid CallSessionId { get; set; }
    }
}