using System;

namespace MegafonApiNet.Helpers.StringConstants
{
    public static class MethodNames
    {
        /// <summary>
        /// Принять входящий вызов.
        /// </summary>
        public const String AnswerCall = nameof(AnswerCall);
        /// <summary>
        /// Отклонить входящий вызов.
        /// </summary>
        public const String RejectCall = nameof(RejectCall);
        /// <summary>
        /// Инициирует установление голосового вызова на телефонную сеть.
        /// </summary>
        public const String MakeCall = nameof(MakeCall);
        /// <summary>
        /// Метод осуществляет разрыв вызова по инициативе приложения.
        /// </summary>
        public const String TerminateCall = nameof(TerminateCall);
        /// <summary>
        /// Соединяет речевой тракт двух сессий (полный дуплекс)
        /// </summary>
        public const String TromboneCall = nameof(TromboneCall);
        /// <summary>
        /// Проигрывает звуковое сообщение на входящее или исходящее плечо вызова.
        /// </summary>
        public const String PlayAnnouncement = nameof(PlayAnnouncement);
        /// <summary>
        /// Проигрывает системные звуковые файлы на входящее или исходящее плечо вызова.
        /// </summary>
        public const String PlayTone = nameof(PlayTone);
        /// <summary>
        /// Позволяет остановить запись звонковой сессии (не конференции!)
        /// </summary>
        public const String StartCallRecord = nameof(StartCallRecord);
        /// <summary>
        /// Позволяет остановить запись звонковой сессии (не конференции!)
        /// </summary>
        public const String StopCallRecord = nameof(StopCallRecord);
        /// <summary>
        /// Метод создает голосовую конференцию
        /// </summary>
        public const String CreateConf = nameof(CreateConf);
        /// <summary>
        /// Добавляет вызов в аудиоконференцию. Аудио-тракт вызова коммутируется в общую конференцию.
        /// </summary>
        public const String AddToConf = nameof(AddToConf);
        /// <summary>
        /// Удаляет вызов из аудиоконференции.
        /// </summary>
        public const String RemoveFromConf = nameof(RemoveFromConf);
        /// <summary>
        /// Завершает все соединения и разрушает конференцию.
        /// </summary>
        public const String DestroyConf = nameof(DestroyConf);
        /// <summary>
        /// Возвращает статус конференции
        /// </summary>
        public const String StatusConf = nameof(StatusConf);
        /// <summary>
        /// Позволяет включить запись конференции целиком.
        /// </summary>
        public const String StartConfRecord = nameof(StartConfRecord);
        /// <summary>
        /// Позволяет остановить запись конференции.
        /// </summary>
        public const String StopConfRecord = nameof(StopConfRecord);
        /// <summary>
        /// Отправить SMS
        /// </summary>
        public const String SendSMS = nameof(SendSMS);
    }
}
