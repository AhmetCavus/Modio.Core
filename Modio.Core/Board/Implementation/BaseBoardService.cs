using Modio.Core.Container;
using Modio.Core.Messenger;
using Modio.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Modio.Core.Board
{
    public class BaseBoardService : IBoardService
    {
        #region Attributes

        IServiceContainer<IModuleService> _container;
        public IServiceContainer<IModuleService> Container {
            set
            {
                _container = value;
            }
        }

        #endregion

        #region Properties

        string _id;
        public string Id => _id;

        public IReadOnlyList<IModuleService> Modules => _container.ToList();

        #endregion

        #region Constructor

        public BaseBoardService()
        {
            _id = GetType().FullName;
        }

        #endregion

        #region Public Methods

        public void AddModule<TModuleService>() where TModuleService : class, IModuleService
        {
            var module = _container.Add<TModuleService>();
            module.Messenger.Broadcast -= OnReceiveModuleMessage;
            module.Messenger.Broadcast += OnReceiveModuleMessage;
            module.Messenger.Request -= OnReceiveModuleRequest;
            module.Messenger.Request += OnReceiveModuleRequest;
        }

        public TModuleService GetModule<TModuleService>() where TModuleService : class, IModuleService
        {
            return _container.Get<TModuleService>();
        }

        public bool IsModuleActive<TModuleService>() where TModuleService : class, IModuleService
        {
            var module = GetModule<TModuleService>();
            return module.IsActive;
        }

        public void RemoveModule<TModuleService>() where TModuleService : class, IModuleService
        {
            _container.Remove<TModuleService>();
        }

        public void StartModule<TModuleService>() where TModuleService : class, IModuleService
        {
            var module = GetModule<TModuleService>();
            module.Activate();
        }

        public void StopModule<TModuleService>() where TModuleService : class, IModuleService
        {
            var module = GetModule<TModuleService>();
            module.Deactivate();
        }

        #endregion

        #region Event Handler

        void OnReceiveModuleMessage(object sender, MessageEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("{0} {1} {2}", e.From, e.To, e.Data);
            var toModule = _container.Get(e.To);
            var fromModule = _container.Get(e.From);
            var toInvoke = ResolveMessageMethod(e, "OnMessage");
            toInvoke?.Invoke(toModule, new object [] { fromModule, e.Data });
        }

        void OnReceiveModuleRequest(object sender, RequestEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("{0} {1} {2}", e.Sender, e.From, e.Data);
            var fromModule = _container.Get(e.From);
            var senderModule = _container.Get(e.Sender);
            var toInvoke = ResolveRequestMethod(e, "OnRequest");
            toInvoke?.Invoke(fromModule, new object[] { senderModule, e });
        }


        #endregion

        #region Helper Methods

        MethodInfo ResolveMessageMethod(MessageEventArgs e, string methodName)
        {
            var dataType = e.Data.GetType().FullName;
            var methods = e.To.GetMethods();
            var toInvoke =
                methods
                .FirstOrDefault(method =>
                    method.Name.Equals(methodName) &&
                    method.GetParameters().FirstOrDefault(param => param.ParameterType.FullName == dataType) != null);
            return toInvoke;
        }

        MethodInfo ResolveRequestMethod(RequestEventArgs e, string methodName)
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

        #region IDisposable Support

        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~BaseBoardService() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}