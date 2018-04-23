using Modio.Core.Board;
using Modio.Core.Container;
using Modio.Core.Module;
using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.App
{
    public interface IAppService : IService, IDisposable
    {
        IReadOnlyList<IBoardService> Boards { get; }

        IReadOnlyList<WorkerModuleService> Workers { get; }

        IReadOnlyList<UIModuleService> Modules { get; }

        void SelectBoard<TBoardService>() where TBoardService : class, IBoardService;

        void AddBoard<TBoardService>() where TBoardService : class, IBoardService;

        void RemoveBoard<TBoardService>() where TBoardService : class, IBoardService;

        TBoardService GetBoard<TBoardService>() where TBoardService : class, IBoardService;

        void AddWorker<TWorkerModule>()
            where TWorkerModule : WorkerModuleService;

        void RemoveWorker<TWorkerModule>()
            where TWorkerModule : WorkerModuleService;

        TWorkerModule GetWorker<TWorkerModule>()
            where TWorkerModule : WorkerModuleService;

        void ActivateModule<TBoardService, TModuleService>()
            where TBoardService : class, IBoardService
            where TModuleService : UIModuleService;

        void AddModule<TBoardService, TModuleService>() 
            where TBoardService : class, IBoardService
            where TModuleService: UIModuleService;

        void RemoveModule<TBoardService, TModuleService>()
            where TBoardService : class, IBoardService
            where TModuleService : UIModuleService;

        TModuleService GetModule<TBoardService, TModuleService>() 
            where TBoardService : class, IBoardService
            where TModuleService : UIModuleService;

        IReadOnlyList<TModuleService> GetModules<TModuleService>()
            where TModuleService : UIModuleService;

    }
}