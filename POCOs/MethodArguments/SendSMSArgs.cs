using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MegafonAPINet.POCOs.MethodArguments
{
    public class SendSMSArgs: BaseMethodParams
    {
        /// <summary>
        /// Номер (номер отправителя) в формате E.164.
        /// Данный номер соответствует номеру API-подписки, передаваемому при аутентификации разработчика. 
        /// Не обязательный параметр, если в учетной записи разработчика только одна API-подписка. 
        /// Особенно удобно его не указывать при авторизации через токен.
        /// </summary>
        [JsonProperty("from")]
        public String From { get; set; }
        /// <summary>
        /// Номер (номер получателя) в формате E.164.
        /// </summary>
        [JsonProperty("to")]
        public String To { get; set; }
        /// <summary>
        /// Текстовое или бинарное сообщение
        /// </summary>
        [JsonProperty("message")]
        public String Message { get; set; }
        /// <summary>
        /// Кодировка сообщения в поле message.
        /// По умолчанию: 0 -- кодировка настроенная на SMSC по умолчанию.Возможные значения можно найти в спецификации SMPP v3.4.
        /// </summary>
        [JsonProperty("data_coding")]
        public int? DataCoding { get; set; }
        /// <summary>
        /// Тип сообщения.
        /// По умолчанию: TEXT.Следует согласовывать тип сообщения и кодировку.Подробности можно найти в спецификации SMPP v3.4.
        /// </summary>
        [JsonProperty("message_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        TEXT,
        BINARY,
        FLASH
    }
}
