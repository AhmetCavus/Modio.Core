using Modio.Core.Messenger;
using Modio.Core.Service;
using System;

namespace Modio.Core.Module
{
    public interface IModuleService : IService, IDisposable
    {
        bool IsActive { get; }
        IModuleMeta MetaInfo { get; }
        IMessenger<IModuleService> Messenger { get; }

        void Reset();
        void Activate();
        void Deactivate();
    }
}
