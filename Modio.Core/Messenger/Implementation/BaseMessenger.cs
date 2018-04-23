using System;
using System.Linq;
using System.Reflection;
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
            var e = new MessageEventArgs(_sender, to, param);
            var method = ResolveMessageMethod(e, "OnMessage");
            e.ToInvoke = method;
            OnSendMessage(this, e);
        }

        public void SendMessage<TSubService, TParam>(TParam param) where TSubService : TService
        {
            OnSendMessage(this, new MessageEventArgs(_sender, typeof(TSubService), param));
        }

        public void RequestMessage(Type from, Type param)
        {
            OnRequestMessage(this, new RequestEventArgs(_sender, from, param));
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

        #region Helper Methods

        public static MethodInfo ResolveMessageMethod(MessageEventArgs e, string methodName)
        {
            var methods = e.To.GetMethods();
            var toInvoke =
                methods
                .FirstOrDefault(method =>
                    method.Name.Equals(methodName) &&
                    method.GetParameters().FirstOrDefault(param => param.ParameterType.FullName == e.Data.GetType().FullName) != null);
            return toInvoke;
        }

        public static MethodInfo ResolveRequestMethod(RequestEventArgs e, string methodName)
        {
            var dataType = e.Data.FullName;
            var methods = e.From.GetMethods();
            var toInvoke =
                methods
                .FirstOrDefault(method =>
                    method.Name.Equals(methodName));
            return toInvoke;
        }

        #endregion
    }
}
