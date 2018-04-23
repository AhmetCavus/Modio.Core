using Modio.Core.Container;
using Modio.Core.Module;
using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.Board
{
    public interface IBoardService : IService, IDisposable
    {
        IReadOnlyList<UIModuleService> Modules { get; }
        void StartModule<TModuleService>() where TModuleService : UIModuleService;
        void StopModule<TModuleService>() where TModuleService : UIModuleService;
        void AddModule<TModuleService>() where TModuleService : UIModuleService;
        void RemoveModule<TModuleService>() where TModuleService : UIModuleService;
        bool IsModuleActive<TModuleService>() where TModuleService : UIModuleService;
        TModuleService GetModule<TModuleService>() where TModuleService : UIModuleService;
    }
}
