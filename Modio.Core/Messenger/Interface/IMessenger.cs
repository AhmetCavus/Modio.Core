using Modio.Core.Service;
using System;

namespace Modio.Core.Messenger
{
    public interface IMessenger<TService> where TService : IService
    {
        event EventHandler<MessageEventArgs> Broadcast;
        event EventHandler<RequestEventArgs> Request;

        void SendMessage(Type to, object param);
        void SendMessage<TSubService, TParam>(TParam param) where TSubService : TService;

        void RequestMessage(Type to, Type param);
        void RequestMessage<TSubService, TParam>() where TSubService : TService;
    }
}
