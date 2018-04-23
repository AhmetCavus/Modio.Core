using System;

namespace Modio.Core.Service
{
    public interface IService : IDisposable
    {
        string Id { get; }
        string Name { get; }

    }
}
