using System;

namespace MegafonApiNet.POCOs
{
    public class ActionResult<T>
    {
        public Result<T> Result { get; set; }
        public String JsonRpc { get; set; }
        public Int32 Id { get; set; }
    }

    public class Result<T>
    {
        public String Message { get; set; }
        public T Data { get; set; }
    }

    public class Data
    {
        public Guid CallSession { get; set; }
    }
}
