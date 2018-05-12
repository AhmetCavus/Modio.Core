using Modio.Core.Service;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Modio.Core.Container
{
    public class ServiceDictionaryContainer<TService> : BaseServiceContainer<TService> where TService : class, IService
    {
        #region Attributes

        protected IReadOnlyList<TService> _services = new List<TService>();
        protected Dictionary<string, TService> _serviceDict = new Dictionary<string, TService>();

        #endregion

        #region Public Methods

        public override void Add<TSubService>()
        {
            var type = typeof(TSubService);
            var instance = Activator.CreateInstance<TSubService>();
            _serviceDict.Add(type.FullName, instance);
        }

        public override bool Contains<TSubService>()
        {
            var type = typeof(TSubService);
            return _serviceDict.ContainsKey(type.FullName);
        }

        public override TSubService Get<TSubService>()
        {
            var serviceType = typeof(TSubService);
            TSubService result = default(TSubService);
            if (Contains<TSubService>()) result = _serviceDict[serviceType.FullName] as TSubService;
            else result = null;
            return result;
        }

        public override TService Get(Type serviceType)
        {
            return _serviceDict[serviceType.FullName];
        }

        public override TSubService Remove<TSubService>()
        {
            TSubService result = Get<TSubService>();
            if(result != null) _serviceDict.Remove(result.GetType().FullName);
            return result;
        }

        public override IReadOnlyList<TService> ToList()
        {
            if(_services.Count != _serviceDict.Count)
            {
                var modifiableList = (_services as IList);
                modifiableList.Clear();
                foreach (var item in _serviceDict)
                {
                    modifiableList.Add(item.Value);
                }
            }
            return _services;
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
                    foreach (var service in _serviceDict)
                    {
                        service.Value.Dispose();
                    }
                    _serviceDict.Clear();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.
                _serviceDict = null;
                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~BaseAppService() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public override void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
