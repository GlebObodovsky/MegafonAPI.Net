using Newtonsoft.Json;
using System;

namespace MegafonApiNet.POCOs.NotificationArguments
{
    public class OnCallRecordFragmentParams: BaseCallParams
    {
        /// <summary>
        /// Уникальный идентификатор записи, общий для всех фрагментов. Генерируется в момент старта записи.
        /// Возвращается из методов "StartCallRecord" или "StartConfRecord".
        /// Имя имеет формат CALL_SESSION-IDX.pcm, где CALL_SESSION - идентификатор звонковой сессии, а IDX - суффикс, показывающий номер фрагмента записи
        /// </summary>
        [JsonProperty("record_id")]
        public String RecordId { get; set; }
        /// <summary>
        /// Порядковый номер текущего фрагмента записи.
        /// </summary>
        [JsonProperty("sequence_number")]
        public Int32 SequenceNumber { get; set; }
        /// <summary>
        /// Имя файла фрагмента записи звонковой сессии или конференции.
        /// Имеет формат { record_id }_{sequence_number}.pcm
        /// После записи файлы располагаются на дисковом пространстве, к которому разработчик может получить доступ.
        /// Записанные файлы периодически удаляются, поэтому рекомендуется забирать их как можно быстрее.
        /// </summary>
        public String FileName { get; set; }
        /// <summary>
        /// Признак закрытия файла по тишине (true) или по достижению максимальной длительности фрагмента (false)
        /// </summary>
        public Boolean Silence { get; set; }
    }
}
