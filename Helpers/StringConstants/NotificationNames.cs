using System;

namespace MegafonApiNet.Helpers.StringConstants
{
    public static class NotificationNames
    {
        /// <summary>
        /// В случае входящего вызова на номер, по которому прошла авторизация в МегаФон.API, в приложение будет передано событие OnIncomingCall
        /// </summary>
        public const String OnIncomingCall = nameof(OnIncomingCall);
        /// <summary>
        /// Событие отправляется при подтверждении исходящего вызова сетью сигнализации.
        /// </summary>
        public const String OnAcceptCall = nameof(OnAcceptCall);
        /// <summary>
        /// Событие отправляется при принятии исходящего вызова вызываемой стороной
        /// </summary>
        public const String OnAnswerCall = nameof(OnAnswerCall);
        /// <summary>
        /// Событие отправляется при прерывании проигрывания файла со стороны API.
        /// </summary>
        public const String OnCancelAnnouncement = nameof(OnCancelAnnouncement);
        /// <summary>
        /// Событие возникает при закрытии фрагмента записи звонковой сессии по тишине.
        /// </summary>
        public const String OnCallRecordFragment = nameof(OnCallRecordFragment);
        /// <summary>
        /// Событие возникает при закрытии фрагмента записи конференции по достижению максимальной длительности фрагмента.
        /// При этом запись будет продолжена в файл следующего фрагмента.
        /// </summary>
        public const String OnConfRecordFragment = nameof(OnConfRecordFragment);
        /// <summary>
        /// Событие отправляется при завершении сбора DTMF.
        /// </summary>
        public const String OnCollectDtmf = nameof(OnCollectDtmf);
        /// <summary>
        /// Событие отправляется при проигрывании звукового файла до конца.
        /// </summary>
        public const String OnPlayAnnouncement = nameof(OnPlayAnnouncement);
        /// <summary>
        /// Событие отправляется при отклонении вызова сетью сигнализации или вызываемой стороной
        /// </summary>
        public const String OnRejectCall = nameof(OnRejectCall);
        /// <summary>
        /// Событие возникает при остановке записи звонковой сессии по сигналу DTMF (если было запрошено с помощью параметра "dtmf_term") или по команде "StopCallRecord".
        /// </summary>
        public const String OnStopCallRecord = nameof(OnStopCallRecord);
        /// <summary>
        /// Событие возникает при остановке записи конференции по команде StopConfRecord или при разрушении записываемой конференции по команде DestroyConf.
        /// </summary>
        public const String OnStopConfRecord = nameof(OnStopConfRecord);
        /// <summary>
        /// Событие отправляется при завершении вызова
        /// </summary>
        public const String OnTerminateCall = nameof(OnTerminateCall);
        /// <summary>
        /// Событие отправляется при получении статуса доставки от SMS-центра
        /// </summary>
        public const String OnSMSDelivery = nameof(OnSMSDelivery);
        /// <summary>
        /// Событие отправляется при закрытии сессии (взаимодействия с Megafon API)
        /// </summary>
        public const String OnClose = nameof(OnClose);

    }
}
