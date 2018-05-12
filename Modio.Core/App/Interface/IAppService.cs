using Modio.Core.Board;
using Modio.Core.Module;
using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.App
{
    public interface IAppService<TBoardService, TModuleService> : IService, IDisposable 
        where TBoardService : class, IBoardService<TModuleService>
        where TModuleService : class, IModuleService
    {
        IReadOnlyList<TBoardService> Boards { get; }

        IReadOnlyList<WorkerModuleService> Workers { get; }

        IReadOnlyList<TModuleService> Modules { get; }

        TSubBoardService SelectBoard<TSubBoardService>() where TSubBoardService : class, TBoardService;

        void AddBoard<TSubBoardService>() where TSubBoardService : class, TBoardService;

        TSubBoardService RemoveBoard<TSubBoardService>() where TSubBoardService : class, TBoardService;

        TSubBoardService GetBoard<TSubBoardService>() where TSubBoardService : class, TBoardService;

        void AddWorker<TWorkerModule>()
            where TWorkerModule : WorkerModuleService;

        TWorkerModule RemoveWorker<TWorkerModule>()
            where TWorkerModule : WorkerModuleService;

        TWorkerModule GetWorker<TWorkerModule>()
            where TWorkerModule : WorkerModuleService;

        TSubModuleService ActivateModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService;

        void AddModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService;

        TSubModuleService RemoveModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService;

        TSubModuleService GetModule<TSubBoardService, TSubModuleService>()
            where TSubBoardService : class, TBoardService
            where TSubModuleService : class, TModuleService;

        IReadOnlyList<TSubModuleService> GetModules<TSubModuleService>()
            where TSubModuleService : class, TModuleService;

    }
}