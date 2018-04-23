using System;
using System.Reflection;

namespace Modio.Core.Messenger
{
    public class MessageEventArgs : EventArgs
    {
        public Type Sender { get; }
        public object Data { get; }
        public Type To { get; }
        public MethodInfo ToInvoke { get; set; }

        public MessageEventArgs(Type sender, Type to, object param)
        {
            To = to;
            Sender = sender;
            Data = param;
        }
    }
}
