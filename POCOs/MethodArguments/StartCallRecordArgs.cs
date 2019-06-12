using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.MethodArguments
{
    public class StartCallRecordArgs: HasSessionIdArgs
    {
        /// <summary>
        /// Признак закрытия фрагмента записи при детектировании тишины. При отсутствии детектирование тишины не осуществляется.
        /// </summary>
        [JsonProperty("detect_silence")]
        public Boolean DetectSilence { get; set; }
        /// <summary>
        /// Символ DTMF, при получении которого запись останавливается. При отсутствии запись нельзя остановить по DTMF.
        /// </summary>
        [JsonProperty("dtmf_term")]
        public String DtmfTerm { get; set; }
    }
}
