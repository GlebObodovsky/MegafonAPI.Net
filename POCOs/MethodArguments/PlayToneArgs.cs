using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.MethodArguments
{
    public class PlayToneArgs: HasSessionIdArgs
    {
        /// <summary>
        /// Идентификатор системного файла, натуральное число. Значение 1 соответствует стандартному сигналу КПВ для ТФОП РФ.
        /// </summary>
        [JsonProperty("tone_id")]
        public Int32 ToneId { get; set; }
    }
}
