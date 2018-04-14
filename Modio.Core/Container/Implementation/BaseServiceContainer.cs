using System;
using System.Collections.Generic;
using Modio.Core.Service;

namespace Modio.Core.Container
{
    public abstract class BaseServiceContainer<TService> : IServiceContainer<TService> where TService : class, IService
    {
        public abstract void Dispose();
        public abstract IReadOnlyList<TService> ToList();
        public abstract bool Contains<TSubService>() where TSubService : class, TService;
        public abstract TSubService Add<TSubService>() where TSubService : class, TService;
        public abstract TService Get(Type serviceType);
        public abstract TSubService Get<TSubService>() where TSubService : class, TService;
        public abstract TSubService Remove<TSubService>() where TSubService : class, TService;
    }
}
