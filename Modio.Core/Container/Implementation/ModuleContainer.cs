using Modio.Core.Component;
using Modio.Core.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Modio.Core.Container
{
    public class ModuleContainer<TModuleService> : BaseServiceContainer<TModuleService> where TModuleService : class, IModuleService
    {
        #region Attributes

        protected IReadOnlyList<TModuleService> _modules = new List<TModuleService>();
        protected Dictionary<string, TModuleService> _moduleDict = new Dictionary<string, TModuleService>();
        protected Dictionary<string, IComponentService> _componentDict = new Dictionary<string, IComponentService>();

        #endregion

        #region Public Methods

        public override void Add<TSubService>()
        {
            ResolveModuleInstance<TSubService>();
        }

        public override bool Contains<TSubService>()
        {
            var type = typeof(TSubService);
            return _moduleDict.ContainsKey(type.FullName);
        }

        public override TSubService Get<TSubService>()
        {
            var serviceType = typeof(TSubService);
            TSubService result = default(TSubService);
            if (Contains<TSubService>()) result = _moduleDict[serviceType.FullName] as TSubService;
            else result = null;
            return result;
        }

        public override TModuleService Get(Type serviceType)
        {
            return _moduleDict[serviceType.FullName];
        }

        public override TSubService Remove<TSubService>()
        {
            TSubService result = Get<TSubService>();
            if(result != null) _moduleDict.Remove(result.GetType().FullName);
            return result;
        }

        public override IReadOnlyList<TModuleService> ToList()
        {
            if(_modules.Count != _moduleDict.Count)
            {
                var modifiableList = (_modules as IList);
                modifiableList.Clear();
                foreach (var item in _moduleDict)
                {
                    modifiableList.Add(item.Value);
                }
            }
            return _modules;
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
                    foreach (var service in _moduleDict)
                    {
                        service.Value.Dispose();
                    }
                    _moduleDict.Clear();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.
                _moduleDict = null;
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

        TSubModuleService ResolveModuleInstance<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            var type = typeof(TSubModuleService);
            var ctor = type.GetConstructors().FirstOrDefault();
            var parameterInfos = ctor.GetParameters();
            object[] parameters = new object[parameterInfos.Length];
            int index = 0;
            foreach (var param in parameterInfos)
            {
                var component = ResolveComponentService(param.ParameterType);
                parameters[index] = component;
                _componentDict[type.FullName] = component;
                index += 1;
            }
            var instance = Activator.CreateInstance(type, parameters) as TSubModuleService;
            _moduleDict.Add(type.FullName, instance);
            return instance;
        }

        IComponentService ResolveComponentService(Type type)
        {
            IComponentService component = default(IComponentService);
            if (!(typeof(IComponentService).IsAssignableFrom(type))) throw new Exception("Only subtypes of IComponentService are allowed as parameters in Module Services");

            if (_componentDict.ContainsKey(type.FullName))
            {
                component = _componentDict[type.FullName];
            }
            else
            {
                component = Activator.CreateInstance(type) as IComponentService;
            }
            return component;
        }

        #endregion
    }
}
