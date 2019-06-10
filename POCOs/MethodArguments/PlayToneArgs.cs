using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegafonAPINet.POCOs.MethodArguments
{
    public class PlayToneArgs: BaseMethodParams
    {
        /// <summary>
        /// Уникальный идентификатор звонковой сессии.
        /// </summary>
        [JsonProperty("call_session")]
        public Guid SessionId { get; set; }
        /// <summary>
        /// Идентификатор системного файла, натуральное число. Значение 1 соответствует стандартному сигналу КПВ для ТФОП РФ.
        /// </summary>
        [JsonProperty("tone_id")]
        public Int32 ToneId { get; set; }
    }
}
