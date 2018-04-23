using Modio.Core.Service;
using System;

namespace Modio.Core.Component
{
    public interface IComponentService : IService
    {
        event EventHandler<EventArgs> Ready;

        void Initialize();
    }
}
