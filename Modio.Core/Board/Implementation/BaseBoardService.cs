using Modio.Core.Container;
using Modio.Core.Messenger;
using Modio.Core.Module;
using System;
using System.Collections.Generic;

namespace Modio.Core.Board
{
    public abstract class BaseBoardService<TModuleService> : IBoardService<TModuleService> where TModuleService : class, IModuleService
    {
        #region Attributes

        #endregion

        #region Properties

        string _id;
        public string Id => _id;

        public string Name => GetType().FullName;

        public abstract IList<TModuleService> Modules { get; set; }

        public abstract IServiceContainer<TModuleService> Container { get; }

        public bool IsInitialized => String.IsNullOrEmpty(_id);

        #endregion

        #region Constructor

        public BaseBoardService()
        {
            _id = GetType().FullName;
        }

        #endregion

        #region Public Methods

        public void AddModule<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            Container.Add<TSubModuleService>();
            var module = GetModule<TSubModuleService>();
            module.Messenger.Broadcast -= OnReceiveModuleMessage;
            module.Messenger.Broadcast += OnReceiveModuleMessage;
            module.Messenger.Request -= OnReceiveModuleRequest;
            module.Messenger.Request += OnReceiveModuleRequest;
            OnAddModule(module);
        }

        public TSubModuleService GetModule<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            return Container.Get<TSubModuleService>();
        }

        public bool IsModuleActive<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            var module = GetModule<TSubModuleService>();
            return module.IsActive;
        }

        public TSubModuleService RemoveModule<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            var module = Container.Remove<TSubModuleService>();
            OnRemoveModule(module);
            return module;
        }

        public TSubModuleService StartModule<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            var module = GetModule<TSubModuleService>();
            module.Activate();
            return module;
        }

        public TSubModuleService StopModule<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            var module = GetModule<TSubModuleService>();
            module.Deactivate();
            return module;
        }

        #endregion

        #region Event Handler

        void OnReceiveModuleMessage(object sender, MessageEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("{0} {1} {2}", e.Sender, e.To, e.Data);
            var toModule = Container.Get(e.To);
            var fromModule = Container.Get(e.Sender);
            var toInvoke = BaseMessenger<IModuleService>.ResolveMessageMethod(e, "OnMessage");
            toInvoke?.Invoke(toModule, new object [] { fromModule, e.Data });
        }

        void OnReceiveModuleRequest(object sender, RequestEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("{0} {1} {2}", e.Sender, e.From, e.Data);
            var fromModule = Container.Get(e.From);
            var senderModule = Container.Get(e.Sender);
            var toInvoke = BaseMessenger<IModuleService>.ResolveRequestMethod(e, "OnRequest");
            toInvoke?.Invoke(fromModule, new object[] { senderModule, e });
        }


        #endregion

        #region Helper Methods

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

        #region Abstract Methods

        protected abstract void OnRemoveModule(TModuleService module);
        protected abstract void OnActivateModule(TModuleService module);
        protected abstract void OnAddModule(TModuleService module);

        #endregion
    }
}