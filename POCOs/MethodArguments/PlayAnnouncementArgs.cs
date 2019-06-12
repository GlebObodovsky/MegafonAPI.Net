using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.MethodArguments
{
    public class PlayAnnouncementArgs: HasSessionIdArgs
    {
        /// <summary>
        /// Имя файла проигрываемого сообщения. Формат файла - 64K PCM A-law (8KHz, 8 Bit).
        /// </summary>
        [JsonProperty("filename")]
        public String FileName { get; set; }
        /// <summary>
        /// Временной промежуток (в 100 миллисекундных интервалах), в течение которого платформа 
        /// будет ожидать пользовательского ввода с цифровой клавиатуры телефона 
        /// ПОСЛЕ завершения проигрывания голосового сообщения. 
        /// Обязателен при наличии параметра "dtmf_max".
        /// </summary>
        [JsonProperty("timeout")]
        public Int32? Timeout { get; set; }
        /// <summary>
        /// Временной интервал (в 100 миллисекундных интервалах), в течение которого платформа 
        /// будет ожидать нажатие каждой следующей кнопки на цифровой клавиатуре телефона (если нажимается несколько). 
        /// Если пользователь не нажал кнопку в течение этого интервала, платформа считает ввод завершенным. 
        /// Обязателен при наличии параметра "dtmf_max".
        /// </summary>
        [JsonProperty("dtmf_idd")]
        public Int32? DtmfIdd { get; set; }
        /// <summary>
        /// Максимальное количество собираемых символов DTMF от 1 до 31. Опционален. При отсутствии DTMF не собираются.
        /// </summary>
        [JsonProperty("dtmf_max")]
        public Int32? DtmfMax { get; set; }
        /// <summary>
        /// Набор символов, при получении хотя бы одного из которых, завершается сбор DTMF. 
        /// Опционален. При отсутствии ни один символ не завершает набор.
        /// </summary>
        [JsonProperty("dtmf_term")]
        public String DtmfTerm { get; set; }
        /// <summary>
        /// Набор ожидаемых символов, остальные игнорируются. Опционален. При отсутствии собираются любые символы.
        /// </summary>
        [JsonProperty("dtmf_valid")]
        public String DtmfValid { get; set; }
    }
}
