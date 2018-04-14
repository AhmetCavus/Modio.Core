using Modio.Core.Container;
using Modio.Core.Module;
using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.Board
{
    public interface IBoardService : IService, IDisposable
    {
        IReadOnlyList<IModuleService> Modules { get; }
        IServiceContainer<IModuleService> Container { set; }
        void StartModule<TModuleService>() where TModuleService : class, IModuleService;
        void StopModule<TModuleService>() where TModuleService : class, IModuleService;
        void AddModule<TModuleService>() where TModuleService : class, IModuleService;
        void RemoveModule<TModuleService>() where TModuleService : class, IModuleService;
        bool IsModuleActive<TModuleService>() where TModuleService : class, IModuleService;
        TModuleService GetModule<TModuleService>() where TModuleService : class, IModuleService;
    }
}
