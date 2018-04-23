
using System.Collections.Generic;
using System.Linq;
using Modio.Core.Board;
using Modio.Core.Container;
using Modio.Core.Module;

namespace Modio.Core.App
{
    public abstract class BaseAppService : IAppService
    {

        #region Attributes

        IServiceContainer<IBoardService> _boardContainer;
        ModuleContainer<WorkerModuleService> _workerContainer;

        #endregion

        #region Properties

        string _id;
        public string Id => _id;

        string _name;
        public string Name => _name;

        public IReadOnlyList<IBoardService> Boards => _boardContainer.ToList();

        public IReadOnlyList<WorkerModuleService> Workers => _workerContainer.ToList();

        public IReadOnlyList<UIModuleService> Modules => throw new System.NotImplementedException();

        #endregion

        #region Constructor

        public BaseAppService(IServiceContainer<IBoardService> container)
        {
            _boardContainer = container;
            _id = GetType().FullName;
        }

        #endregion

        #region Public Methods

        public void AddBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            OnAddBoard(_boardContainer.Add<TBoardService>());
        }

        public TBoardService GetBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            return _boardContainer.Get<TBoardService>();
        }

        public void RemoveBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            OnRemoveBoard(_boardContainer.Remove<TBoardService>());
        }

        public void SelectBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            var board = GetBoard<TBoardService>();
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

        public void ActivateModule<TBoardService, TModuleService>()
            where TBoardService : class, IBoardService
            where TModuleService : UIModuleService
        {
            var board = GetBoard<TBoardService>();
            board.StartModule<TModuleService>();
        }

        public void AddModule<TBoardService, TModuleService>()
            where TBoardService : class, IBoardService
            where TModuleService : UIModuleService
        {
            var board = GetBoard<TBoardService>();
            board.AddModule<TModuleService>();
        }

        public void RemoveModule<TBoardService, TModuleService>()
            where TBoardService : class, IBoardService
            where TModuleService : UIModuleService
        {
            var board = GetBoard<TBoardService>();
            board.RemoveModule<TModuleService>();
        }

        public TModuleService GetModule<TBoardService, TModuleService>()
            where TBoardService : class, IBoardService
            where TModuleService : UIModuleService
        {
            var board = GetBoard<TBoardService>();
            return board.GetModule<TModuleService>();
        }

        public IReadOnlyList<TModuleService> GetModules<TModuleService>() where TModuleService : UIModuleService
        {
            List<TModuleService> result = new List<TModuleService>();
            var modules = Boards.SelectMany(board => board.Modules.Where(module => module.GetType() == typeof(TModuleService)));
            foreach (var module in modules)
            {
                result.Add(module as TModuleService);
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

        protected abstract void OnRemoveBoard(IBoardService board);
        protected abstract void OnSelectBoard(IBoardService board);
        protected abstract void OnAddBoard(IBoardService board);

        protected abstract void OnRemoveModule(UIModuleService module);
        protected abstract void OnActivateModule(UIModuleService module);
        protected abstract void OnAddModule(UIModuleService module);

        protected abstract void OnRemoveWorker(WorkerModuleService worker);
        protected abstract void OnAddWorker(WorkerModuleService worker);

        protected abstract void OnRemoveComponent(WorkerModuleService worker);
        protected abstract void OnAddComponent(WorkerModuleService worker);

        #endregion

    }
}