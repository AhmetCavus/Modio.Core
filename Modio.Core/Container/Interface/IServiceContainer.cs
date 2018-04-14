using Modio.Core.Service;
using System;
using System.Collections.Generic;

namespace Modio.Core.Container
{
    public interface IServiceContainer<TService> : IDisposable where TService : class, IService
    {
        IReadOnlyList<TService> ToList();
        bool Contains<TSubService>() where TSubService : class, TService;
        TSubService Add<TSubService>() where TSubService : class, TService;
        TSubService Get<TSubService>() where TSubService : class, TService;
        TService Get(Type serviceType);
        TSubService Remove<TSubService>() where TSubService : class, TService;
    }
}
