
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

        public bool IsInitialized => Boards != null && Boards.Count > 0;

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
            _boardContainer.Add<TSubBoardService>();
            var board = GetBoard<TSubBoardService>();
            OnAddBoard<TSubBoardService>(board);
        }

        public TSubBoardService GetBoard<TSubBoardService>() where TSubBoardService : class, TBoardService
        {
            return _boardContainer.Get<TSubBoardService>();
        }

        public TSubBoardService RemoveBoard<TSubBoardService>() where TSubBoardService : class, TBoardService
        {
            var board = GetBoard<TSubBoardService>();
            OnRemoveBoard<TSubBoardService>(board);
            _boardContainer.Remove<TSubBoardService>();
            return board;
        }

        public TSubBoardService SelectBoard<TSubBoardService>() where TSubBoardService : class, TBoardService
        {
            var board = GetBoard<TSubBoardService>();
            if (board == null) return default(TSubBoardService);
            OnSelectBoard<TSubBoardService>(board);
            return board;
        }

        public void AddWorker<TWorkerModule>() where TWorkerModule : WorkerModuleService
        {
            _workerContainer.Add<TWorkerModule>();
            var worker = GetWorker<TWorkerModule>();
            OnAddWorker<TWorkerModule>(worker);
        }

        public TWorkerModule RemoveWorker<TWorkerModule>() where TWorkerModule : WorkerModuleService
        {
            var worker = _workerContainer.Remove<TWorkerModule>();
            OnRemoveWorker<TWorkerModule>(worker);
            return worker;
        }

        public TWorkerModule GetWorker<TWorkerModule>() where TWorkerModule : WorkerModuleService
        {
            return _workerContainer.Get<TWorkerModule>();
        }

        public TSubModuleService ActivateModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService
        {
            var board = GetBoard<TSubBoardService>();
            return board.StartModule<TSubModuleService>();
        }

        public void AddModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService
        {
            var board = GetBoard<TSubBoardService>();
            board.AddModule<TSubModuleService>();
        }

        public TSubModuleService RemoveModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService
        {
            var board = GetBoard<TBoardService>();
            return board.RemoveModule<TSubModuleService>();
        }

        public TSubModuleService GetModule<TSubBoardService, TSubModuleService>()
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

        protected abstract void OnRemoveBoard<TSubBoardService>(TBoardService board) where TSubBoardService : TBoardService;
        protected abstract void OnSelectBoard<TSubBoardService>(TBoardService board) where TSubBoardService : TBoardService;
        protected abstract void OnAddBoard<TSubBoardService>(TBoardService board) where TSubBoardService : TBoardService;

        protected abstract void OnRemoveModule<TSubBoardService>(TModuleService module) where TSubBoardService : TBoardService;
        protected abstract void OnActivateModule<TSubBoardService>(TModuleService module) where TSubBoardService : TBoardService;
        protected abstract void OnAddModule<TSubBoardService>(TModuleService module) where TSubBoardService : TBoardService;

        protected abstract void OnRemoveWorker<TSubWorkerModuleService>(WorkerModuleService worker) where TSubWorkerModuleService : WorkerModuleService;
        protected abstract void OnAddWorker<TSubWorkerModuleService>(WorkerModuleService worker) where TSubWorkerModuleService : WorkerModuleService;

        #endregion

    }
}