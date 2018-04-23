
using System.Collections.Generic;
using System.Linq;
using Modio.Core.Board;
using Modio.Core.Container;
using Modio.Core.Module;

namespace Modio.Core.App
{
    public abstract class BaseAppService<TBoardService, TModuleService> : IAppService<TBoardService, TModuleService> 
        where TBoardService : class, IBoardService<TModuleService>
        where TModuleService : class, IModuleService
    {

        #region Attributes

        IServiceContainer<TBoardService> _boardContainer;
        ModuleContainer<WorkerModuleService> _workerContainer;

        #endregion

        #region Properties

        string _id;
        public string Id => _id;

        string _name;
        public string Name => _name;

        public IReadOnlyList<TBoardService> Boards => _boardContainer.ToList();

        public IReadOnlyList<WorkerModuleService> Workers => _workerContainer.ToList();

        public IReadOnlyList<TModuleService> Modules => throw new System.NotImplementedException();

        #endregion

        #region Constructor

        public BaseAppService(IServiceContainer<TBoardService> container)
        {
            _boardContainer = container;
            _id = GetType().FullName;
        }

        #endregion

        #region Public Methods

        public void AddBoard<TSubBoardService>() where TSubBoardService : class, TBoardService
        {
            OnAddBoard(_boardContainer.Add<TSubBoardService>());
        }

        public TBoardService GetBoard<TSubBoardService>() where TSubBoardService : class, TBoardService
        {
            return _boardContainer.Get<TSubBoardService>();
        }

        public void RemoveBoard<TSubBoardService>() where TSubBoardService : class, TBoardService
        {
            OnRemoveBoard(_boardContainer.Remove<TSubBoardService>());
        }

        public void SelectBoard<TSubBoardService>() where TSubBoardService : class, TBoardService
        {
            var board = GetBoard<TSubBoardService>();
            if (board == null) return;
            OnSelectBoard(board);
        }

        public void AddWorker<TWorkerModule>() where TWorkerModule : WorkerModuleService
        {
            OnAddWorker(_workerContainer.Add<TWorkerModule>());
        }

        public void RemoveWorker<TWorkerModule>() where TWorkerModule : WorkerModuleService
        {
            OnRemoveWorker(_workerContainer.Remove<TWorkerModule>());
        }

        public TWorkerModule GetWorker<TWorkerModule>() where TWorkerModule : WorkerModuleService
        {
            return _workerContainer.Get<TWorkerModule>();
        }

        public void ActivateModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService
        {
            var board = GetBoard<TSubBoardService>();
            board.StartModule<TSubModuleService>();
        }

        public void AddModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService
        {
            var board = GetBoard<TSubBoardService>();
            board.AddModule<TSubModuleService>();
        }

        public void RemoveModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService
        {
            var board = GetBoard<TBoardService>();
            board.RemoveModule<TModuleService>();
        }

        public TModuleService GetModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService
        {
            var board = GetBoard<TSubBoardService>();
            return board.GetModule<TSubModuleService>();
        }

        public IReadOnlyList<TSubModuleService> GetModules<TSubModuleService>() where TSubModuleService : class, TModuleService
        {
            List<TSubModuleService> result = new List<TSubModuleService>();
            var modules = Boards.SelectMany(board => board.Modules.Where(module => module.GetType() == typeof(TSubModuleService)));
            foreach (var module in modules)
            {
                result.Add(module as TSubModuleService);
            }
            return result;
        }

        #endregion

        #region Private Methods

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
                    _boardContainer.Dispose();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.
                _boardContainer = null;
                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~BaseAppService() {
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

        protected abstract void OnRemoveBoard(TBoardService board);
        protected abstract void OnSelectBoard(TBoardService board);
        protected abstract void OnAddBoard(TBoardService board);

        protected abstract void OnRemoveModule(TModuleService module);
        protected abstract void OnActivateModule(TModuleService module);
        protected abstract void OnAddModule(TModuleService module);

        protected abstract void OnRemoveWorker(WorkerModuleService worker);
        protected abstract void OnAddWorker(WorkerModuleService worker);

        protected abstract void OnRemoveComponent(WorkerModuleService worker);
        protected abstract void OnAddComponent(WorkerModuleService worker);

        #endregion

    }
}