using System;

namespace MegafonAPINet.Exceptions
{
    public class SessionIsDeadException : Exception
    {
        public SessionIsDeadException() { }

        public SessionIsDeadException(string message): base(message) {}
    }
}
