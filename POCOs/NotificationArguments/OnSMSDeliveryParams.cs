using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class OnSMSDeliveryParams: BaseParams
    {
        /// <summary>
        /// Уникальный идентификатор запроса на отправку короткого сообщения
        /// Возвращается из метода SendSMS
        /// </summary>
        [JsonProperty("sms_id")]
        public Guid SmsId { get; set; }
        /// <summary>
        /// Статус доставки SMS
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageDeliveryStatus Status { get; set; }
    }

    public enum MessageDeliveryStatus
    {
        DELIVERED,
        EXPIRED,
        DELETED,
        UNDELIVERABLE,
        ACCEPTED,
        UNKNOWN,
        REJECTED
    }
}
