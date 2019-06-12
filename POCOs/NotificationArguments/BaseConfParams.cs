using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class BaseConfParams: BaseParams
    {
        [JsonProperty("conf_session")]
        public Guid ConfSession { get; set; }
    }
}
