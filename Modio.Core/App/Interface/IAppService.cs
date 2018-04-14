using Modio.Core.Board;
using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.App
{
    public interface IAppService : IService, IDisposable
    {
        IReadOnlyList<IBoardService> Boards { get; }
        void SelectBoard<TBoardService>() where TBoardService : class, IBoardService;
        void AddBoard<TBoardService>() where TBoardService : class, IBoardService;
        void RemoveBoard<TBoardService>() where TBoardService : class, IBoardService;
        TBoardService GetBoard<TBoardService>() where TBoardService : class, IBoardService;
    }
}
