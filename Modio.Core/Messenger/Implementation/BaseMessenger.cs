using System;
using Modio.Core.Service;

namespace Modio.Core.Messenger
{
    public class BaseMessenger<TService> : IMessenger<TService> where TService : IService
    {
        public event EventHandler<MessageEventArgs> Broadcast;
        public event EventHandler<RequestEventArgs> Request;

        Type _sender;

        public BaseMessenger(Type sender)
        {
            _sender = sender;
        }

        public void SendMessage(Type to, object param)
        {
            OnSendMessage(this, new MessageEventArgs(_sender, to, param));
        }

        public void SendMessage<TSubService, TParam>(TParam param) where TSubService : TService
        {
            OnSendMessage(this, new MessageEventArgs(_sender, typeof(TSubService), param));
        }

        public void RequestMessage(Type to, Type param)
        {
            OnRequestMessage(this, new RequestEventArgs(_sender, to, param));
        }

        public void RequestMessage<TSubService, TParam>() where TSubService : TService
        {
            OnRequestMessage(this, new RequestEventArgs(_sender, typeof(TSubService), typeof(TParam)));
        }

        protected void OnSendMessage(object sender, MessageEventArgs e)
        {
            Broadcast?.Invoke(sender, e);
        }

        protected void OnRequestMessage(object sender, RequestEventArgs e)
        {
            Request?.Invoke(sender, e);
        }
    }
}
