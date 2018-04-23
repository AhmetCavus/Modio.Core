using Modio.Core.Container;
using Modio.Core.Module;
using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.Board
{
    public interface IBoardService<TModuleService> : IService, IDisposable where TModuleService : class, IModuleService
    {
        IReadOnlyList<TModuleService> Modules { get; }
        IServiceContainer<TModuleService> Container { set; }
        void StartModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        void StopModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        void AddModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        void RemoveModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        bool IsModuleActive<TSubModuleService>() where TSubModuleService : class, TModuleService;
        TModuleService GetModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
    }
}
