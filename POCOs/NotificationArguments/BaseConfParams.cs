using Newtonsoft.Json;
using System;

namespace MegafonAPINet.POCOs.NotificationArguments
{
    public class BaseConfParams: BaseParams
    {
        [JsonProperty("conf_session")]
        public Guid ConfSession { get; set; }
    }
}
