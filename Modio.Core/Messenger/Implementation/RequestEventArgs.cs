using System;
using System.Reflection;

namespace Modio.Core.Messenger
{
    public class RequestEventArgs : EventArgs
    {
        public Type From { get; }
        public Type Sender { get; }
        public Type Data { get; }
        public MethodInfo ToInvoke { get; set; }

        public RequestEventArgs(Type sender, Type from, Type param)
        {
            From = from;
            Sender = sender;
            Data = param;
        }
    }
}
