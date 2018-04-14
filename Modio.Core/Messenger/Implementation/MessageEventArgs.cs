using System;

namespace Modio.Core.Messenger
{
    public class MessageEventArgs : EventArgs
    {
        public Type To { get; }
        public Type From { get; }
        public object Data { get; }

        public MessageEventArgs(Type from, Type to, object param)
        {
            To = to;
            From = from;
            Data = param;
        }
    }
}
