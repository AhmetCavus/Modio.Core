using System.Collections.Generic;
using Modio.Core.Board;
using Modio.Core.Container;

namespace Modio.Core.App
{
    public abstract class BaseAppService : IAppService
    {

        #region Attributes

        IServiceContainer<IBoardService> _container;

        #endregion

        #region Properties

        string _id;
        public string Id => _id;

        public IReadOnlyList<IBoardService> Boards => _container.ToList();

        #endregion

        #region Constructor

        public BaseAppService(IServiceContainer<IBoardService> container)
        {
            _container = container;
            _id = GetType().FullName;
        }

        #endregion

        #region Public Methods

        public void AddBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            OnAddBoard(_container.Add<TBoardService>());
        }

        public TBoardService GetBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            return _container.Get<TBoardService>();
        }

        public void RemoveBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            var board = _container.Remove<TBoardService>();
            OnRemoveBoard(board);
        }

        public void SelectBoard<TBoardService>() where TBoardService : class, IBoardService
        {
            var board = GetBoard<TBoardService>();
            if (board == null) return;
            OnSelectBoard(board);
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
                    _container.Dispose();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.
                _container = null;
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

        #endregion

    }
}