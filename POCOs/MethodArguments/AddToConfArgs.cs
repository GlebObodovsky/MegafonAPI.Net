using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.MethodArguments
{
    public class AddToConfArgs: HasSessionIdArgs
    {
        [JsonProperty("conf_session")]
        public Guid ConfSessionId { get; set; }
    }
}
