using System;

namespace MegafonApiNet.Exceptions
{
    public class SessionIsDeadException : Exception
    {
        public SessionIsDeadException() { }

        public SessionIsDeadException(string message): base(message) {}
    }
}
