using Modio.Core.Container;
using Modio.Core.Module;
using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.Board
{
    public interface IBoardService<TModuleService> : IService, IDisposable where TModuleService : class, IModuleService
    {
        IList<TModuleService> Modules { get; set; }
        IServiceContainer<TModuleService> Container { get; }
        TSubModuleService StartModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        TSubModuleService StopModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        void AddModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        TSubModuleService RemoveModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
        bool IsModuleActive<TSubModuleService>() where TSubModuleService : class, TModuleService;
        TSubModuleService GetModule<TSubModuleService>() where TSubModuleService : class, TModuleService;
    }
}
