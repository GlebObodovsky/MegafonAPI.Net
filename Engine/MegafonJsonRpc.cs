using MegafonApiNet.Exceptions;
using MegafonApiNet.Helpers.StringConstants;
using MegafonApiNet.POCOs;
using MegafonApiNet.POCOs.MethodArguments;
using MegafonApiNet.POCOs.NotificationArguments;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebSocketSharp;

namespace MegafonApiNet.Engine
{
    public class MegafonJsonRpc: IDisposable
    {
        #region Fields
        private WebSocket socket;

        private Int32 requestsCounter;

        /// <summary>
        /// Свойство, задающее формат телефонного номера в регулярном выражении, по умолчанию - "^7\d{10}$", 
        /// что эквивалентно номеру в формате 7XXXXXXXXXX
        /// </summary>
        public String PhoneNumberRegex { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// Статус соедниения с MegaFon.API
        /// </summary>
        public Boolean IsAlive => socket.IsAlive;
        #endregion

        #region Ctor
        public MegafonJsonRpc(string uri, KeyValuePair<string, string> credentials = default(KeyValuePair<string, string>))
        {
            socket = new WebSocket(uri);
            if (!default(KeyValuePair<string, string>).Equals(credentials))
            {
                socket.SetCredentials(credentials.Key, credentials.Value, true);
            }

            Initialize();
        }

        private void Initialize()
        {
            if (socket == null)
                throw new NullReferenceException("Something went wrong. WebSocket object is null.");

            requestsCounter = 0;

            PhoneNumberRegex = @"^7\d{10}$";

            socket.OnOpen += OnConnectionEstablishedHandler;
            socket.OnMessage += OnMessageReceivedHandler;
            socket.OnError += OnErrorReceivedHandler;
            socket.OnClose += OnConnectionClosedHandler;
            socket.Connect();
        }
        #endregion

        #region Private Functions
        private void Request(string method, BaseMethodArgs @params = null)
        {
            if (!socket.IsAlive)
                throw new SessionIsDeadException("The connection wasn't established or died.");

            var request = JsonConvert.SerializeObject(
                new
                {
                    id = ++requestsCounter,
                    jsonrpc = "2.0",
                    method,
                    @params
                }, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore } );

            socket.Send(request);
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// Принять входящий вызов.
        /// </summary>
        public void AnswerCall(AnswerCallArgs answerCallArgs)
        {
            if (answerCallArgs == null)
                throw new ArgumentNullException("AnswerCallArgs argument is null.");

            if (answerCallArgs.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId is empty.");

            Request(MethodNames.AnswerCall, answerCallArgs);
        }

        /// <summary>
        /// Отклонить входящий вызов.
        /// </summary>
        /// <param name="rejectCallArgs"></param>
        public void RejectCall(RejectCallArgs rejectCallArgs)
        {
            if (rejectCallArgs == null)
                throw new ArgumentNullException("RejectCallArgs argument is null.");

            if (rejectCallArgs.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId is empty.");

            Request(MethodNames.RejectCall, rejectCallArgs);
        }

        /// <summary>
        /// Инициирует установление голосового вызова на телефонную сеть.
        /// </summary>
        /// <param name="numberToReach">Номер вызываемого абонента в формате E.164</param>
        /// <param name="callingNumber">Номер абонента, совершающего вызов или отправляющего SMS-сообщение, в формате E.164 (Необязательный параметр если в учетной записи разработчика только одна API-подписка)</param>
        public void MakeCall(string numberToReach, string callingNumber = null)
        {
            MakeCall(new MakeCallArgs { NumberToReach = numberToReach, CallingNumber = callingNumber });
        }

        /// <summary>
        /// Инициирует установление голосового вызова на телефонную сеть.
        /// </summary>
        public void MakeCall(MakeCallArgs makeCallArgs)
        {
            if (makeCallArgs == null)
                throw new ArgumentNullException("MakeCall argument is null.");

            if (!Regex.IsMatch(makeCallArgs.NumberToReach, PhoneNumberRegex))
                throw new ArgumentException($"Number you were going to reach: ${makeCallArgs.NumberToReach} is invalid. Use 7XXXXXXXXXX format.");

            if (!String.IsNullOrEmpty(makeCallArgs.CallingNumber) && !Regex.IsMatch(makeCallArgs.CallingNumber, PhoneNumberRegex))
                throw new ArgumentException($"Initializing call number: ${makeCallArgs.CallingNumber} is invalid. Use 7XXXXXXXXXX format.");

            Request(MethodNames.MakeCall, makeCallArgs);
        }

        /// <summary>
        /// Метод осуществляет разрыв вызова по инициативе приложения.
        /// </summary>
        public void TerminateCall(TerminateCallArgs terminateCallArgs)
        {
            if (terminateCallArgs == null)
                throw new ArgumentNullException("MakeCall argument is null.");

            if (terminateCallArgs.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId is empty.");

            Request(MethodNames.TerminateCall, terminateCallArgs);
        }

        /// <summary>
        /// Соединяет речевой тракт двух сессий (полный дуплекс)
        /// </summary>
        public void TromboneCall(TromboneCallArgs tromboneCallArgs)
        {
            if (tromboneCallArgs == null)
                throw new ArgumentNullException("TromboneCallArgs argument is null.");

            if (tromboneCallArgs.ASessionId == Guid.Empty || tromboneCallArgs.BSessionId == Guid.Empty)
                throw new ArgumentException($"Either of sessions A: {tromboneCallArgs.ASessionId} or B: {tromboneCallArgs.BSessionId} is empty.");

            Request(MethodNames.TromboneCall, tromboneCallArgs);
        }

        /// <summary>
        /// Проигрывает звуковое сообщение на входящее или исходящее плечо вызова.
        /// </summary>
        /// <param name="playAnnouncementArgs"></param>
        public void PlayAnnouncement(PlayAnnouncementArgs playAnnouncementArgs)
        {
            if(playAnnouncementArgs == null)
                throw new ArgumentNullException("PlayAnnouncementArgs is null.");

            if (playAnnouncementArgs.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId is empty.");

            if (String.IsNullOrEmpty(playAnnouncementArgs.FileName))
                throw new ArgumentException("FileName parameter hasn't been specified.");

            Request(MethodNames.PlayAnnouncement, playAnnouncementArgs);
        }

        /// <summary>
        /// Проигрывает системные звуковые файлы на входящее или исходящее плечо вызова.
        /// </summary>
        public void PlayTone(PlayToneArgs playToneArgs)
        {
            if (playToneArgs == null)
                throw new ArgumentNullException("PlayToneArgs argument is null.");

            if (playToneArgs.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId is empty.");

            Request(MethodNames.PlayTone, playToneArgs);
        }

        /// <summary>
        /// Позволяет включить запись звонковой сессии (не конференции!)
        /// </summary>
        /// <param name="startCallRecordArgs"></param>
        public void StartCallRecord(StartCallRecordArgs startCallRecordArgs)
        {
            if (startCallRecordArgs == null)
                throw new ArgumentNullException("StartCallRecordArgs is null.");

            if (startCallRecordArgs.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId is empty.");

            Request(MethodNames.StartCallRecord, startCallRecordArgs);
        }

        /// <summary>
        /// Позволяет остановить запись звонковой сессии (не конференции!)
        /// </summary>
        public void StopCallRecord(StopCallRecordArgs stopCallRecordArgs)
        {
            if (stopCallRecordArgs == null)
                throw new ArgumentNullException("StopCallRecordArgs is null.");

            if (stopCallRecordArgs.SessionId == Guid.Empty)
                throw new ArgumentException("SessionId is empty.");

            Request(MethodNames.StopCallRecord, stopCallRecordArgs);
        }

        /// <summary>
        /// Метод создает голосовую конференцию
        /// </summary>
        public void CreateConf() => Request(MethodNames.CreateConf);

        /// <summary>
        /// Добавляет вызов в аудиоконференцию. Аудио-тракт вызова коммутируется в общую конференцию.
        /// </summary>
        /// <param name="addToConfArgs"></param>
        public void AddToConf(AddToConfArgs addToConfArgs)
        {
            if (addToConfArgs == null)
                throw new ArgumentNullException("AddToConfArgs is null.");

            if (addToConfArgs.SessionId == Guid.Empty || addToConfArgs.ConfSessionId == Guid.Empty)
                throw new ArgumentException($"Either SessionId: {addToConfArgs.SessionId} or ConfSessionId: {addToConfArgs.ConfSessionId} is empty.");

            Request(MethodNames.AddToConf, addToConfArgs);
        }

        // ToDo:  RemoveFromConf
        // ToDo:  DestroyConf
        // ToDo:  StatusConf
        // ToDo:  StartConfRecord
        // ToDo:  StopConfRecord

        /// <summary>
        /// Отправить СМС
        /// </summary>
        /// <param name="sendSMSArgs"></param>
        public void SendSMS(SendSMSArgs sendSMSArgs)
        {
            if (sendSMSArgs == null)
                throw new ArgumentNullException("SendSMSArgs is null.");

            if (!Regex.IsMatch(sendSMSArgs.To, PhoneNumberRegex))
                throw new ArgumentException($"Number youare sending SMS to: ${sendSMSArgs.To} is invalid. Use 7XXXXXXXXXX format.");

            Request(MethodNames.SendSMS, sendSMSArgs);
        }
        #endregion

        #region Callbacks
        public Action<EventArgs> OnConnectionEstablished { get; set; }
        public Action<ErrorEventArgs> OnErrorReceived { get; set; }
        /// <summary>
        /// В случае входящего вызова на номер, по которому прошла авторизация в МегаФон.API, в приложение будет передано событие OnIncomingCall
        /// </summary>
        public Action<NotificatonEventArgs<OnIncomingCallParams>> OnIncomingCall { get; set; }
        /// <summary>
        /// Событие отправляется при принятии исходящего вызова вызываемой стороной
        /// </summary>
        public Action<NotificatonEventArgs<OnAnswerCallParams>> OnAnswerCall { get; set; }
        /// <summary>
        /// Событие отправляется при подтверждении исходящего вызова сетью сигнализации.
        /// </summary>
        public Action<NotificatonEventArgs<OnAcceptCallParams>> OnAcceptCall { get; set; }
        /// <summary>
        /// Событие отправляется при прерывании проигрывания файла со стороны API.
        /// </summary>
        public Action<NotificatonEventArgs<OnCancelAnnouncementParams>> OnCancelAnnouncement { get; set; }
        /// <summary>
        /// Событие возникает при закрытии фрагмента записи звонковой сессии по тишине.
        /// </summary>
        public Action<NotificatonEventArgs<OnCallRecordFragmentParams>> OnCallRecordFragment { get; set; }
        /// <summary>
        /// Событие возникает при закрытии фрагмента записи конференции по достижению максимальной длительности фрагмента.
        /// При этом запись будет продолжена в файл следующего фрагмента.
        /// </summary>
        public Action<NotificatonEventArgs<OnConfRecordFragmentParams>> OnConfRecordFragment { get; set; }
        /// <summary>
        /// Событие отправляется при завершении сбора DTMF.
        /// </summary>
        public Action<NotificatonEventArgs<OnCollectDtmfParams>> OnCollectDtmf { get; set; }
        /// <summary>
        /// Событие отправляется при проигрывании звукового файла до конца.
        /// </summary>
        public Action<NotificatonEventArgs<OnPlayAnnouncementParams>> OnPlayAnnouncement { get; set; }
        /// <summary>
        /// Событие отправляется при отклонении вызова сетью сигнализации или вызываемой стороной
        /// </summary>
        public Action<NotificatonEventArgs<OnRejectCallParams>> OnRejectCall { get; set; }
        /// <summary>
        /// Событие возникает при остановке записи звонковой сессии по сигналу DTMF (если было запрошено с помощью параметра "dtmf_term") или по команде "StopCallRecord".
        /// </summary>
        public Action<NotificatonEventArgs<OnStopCallRecordParams>> OnStopCallRecord { get; set; }
        /// <summary>
        /// Событие возникает при остановке записи конференции по команде StopConfRecord или при разрушении записываемой конференции по команде DestroyConf.
        /// </summary>
        public Action<NotificatonEventArgs<OnStopConfRecordParams>> OnStopConfRecord { get; set; }
        /// <summary>
        /// Событие отправляется при завершении вызова
        /// </summary>
        public Action<NotificatonEventArgs<OnTerminateCallParams>> OnTerminateCall { get; set; }
        /// <summary>
        /// Событие отправляется при получении статуса доставки от SMS-центра
        /// </summary>
        public Action<NotificatonEventArgs<OnSMSDeliveryParams>> OnSMSDelivery { get; set; }
        #endregion

        #region Handlers
        private void OnConnectionEstablishedHandler(object sender, EventArgs e)
        {
            OnConnectionEstablished?.Invoke(e);
        }

        private void OnConnectionClosedHandler(object sender, CloseEventArgs e)
        {
        }

        private void OnMessageReceivedHandler(object sender, MessageEventArgs e)
        {
            // Обрабатывается результат отправленного запроса
            if (e.Data.Contains("result"))
            {
                RequestResultHandle(e.Data);
            }
            // Обрабатывается Callback, поступивший из API
            else if (e.Data.Contains("params"))
            {
                CallbackHandler(e.Data);
            }
        }

        private void CallbackHandler(string data)
        {
            var method = ((dynamic)JsonConvert.DeserializeObject(data))?.method?.Value;

            switch (method)
            {
                case NotificationNames.OnIncomingCall:
                    {
                        OnIncomingCall?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnIncomingCallParams>>(data));
                    }
                    break;
                case NotificationNames.OnAcceptCall:
                    {
                        OnAcceptCall?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnAcceptCallParams>>(data));
                    }
                    break;
                case NotificationNames.OnAnswerCall:
                    {
                        OnAnswerCall?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnAnswerCallParams>>(data));
                    }
                    break;
                case NotificationNames.OnCancelAnnouncement:
                    {
                        OnCancelAnnouncement?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnCancelAnnouncementParams>>(data));
                    }
                    break;
                case NotificationNames.OnCallRecordFragment:
                    {
                        OnCallRecordFragment?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnCallRecordFragmentParams>>(data));
                    }
                    break;
                case NotificationNames.OnConfRecordFragment:
                    {
                        OnConfRecordFragment?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnConfRecordFragmentParams>>(data));
                    }
                    break;
                case NotificationNames.OnCollectDtmf:
                    {
                        OnCollectDtmf?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnCollectDtmfParams>>(data));
                    }
                    break;
                case NotificationNames.OnPlayAnnouncement:
                    {
                        OnPlayAnnouncement?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnPlayAnnouncementParams>>(data));
                    }
                    break;
                case NotificationNames.OnRejectCall:
                    {
                        OnRejectCall?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnRejectCallParams>>(data));
                    }
                    break;
                case NotificationNames.OnStopCallRecord:
                    {
                        OnStopCallRecord?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnStopCallRecordParams>>(data));
                    }
                    break;
                case NotificationNames.OnStopConfRecord:
                    {
                        OnStopConfRecord?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnStopConfRecordParams>>(data));
                    }
                    break;
                case NotificationNames.OnTerminateCall:
                    {
                        OnTerminateCall?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnTerminateCallParams>>(data));
                    }
                    break;
                case NotificationNames.OnSMSDelivery:
                    {
                        OnSMSDelivery?.Invoke(JsonConvert.DeserializeObject<NotificatonEventArgs<OnSMSDeliveryParams>>(data));
                    }
                    break;
            }
        }

        private void RequestResultHandle(string data)
        {
            var actionResult = JsonConvert.DeserializeObject<ActionResult<object>>(data);
        }

        private void OnErrorReceivedHandler(object sender, ErrorEventArgs e)
        {
            OnErrorReceived?.Invoke(e);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (socket.IsAlive)
                socket.Close();
        }
        #endregion
    }
}
