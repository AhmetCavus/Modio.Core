using System;

namespace Modio.Core.Messenger
{
    public class RequestEventArgs : EventArgs
    {
        public Type To { get; }
        public Type From { get; }
        public Type Data { get; }

        public RequestEventArgs(Type from, Type to, Type param)
        {
            To = to;
            From = from;
            Data = param;
        }
    }
}
