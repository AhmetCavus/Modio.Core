using Modio.Core.Messenger;
using Modio.Core.Service;
using System;

namespace Modio.Core.Module
{
    public interface IModuleService : IService, IDisposable
    {
        bool IsUI { get; }
        bool IsActive { get; }
        bool IsWorker { get; }

        IModuleMeta MetaInfo { get; }
        IMessenger<IModuleService> Messenger { get; }

        void Reset();
        void Activate();
        void Deactivate();
    }
}
