using Modio.Core.Messenger;
using System;

namespace Modio.Core.Module
{
    public abstract class BaseModuleService : IModuleService
    {
        public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IModuleMeta MetaInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        string _id;
        public string Id => _id;

        IMessenger<IModuleService> _messenger;
        public IMessenger<IModuleService> Messenger => _messenger;

        public BaseModuleService()
        {
            _id = GetType().FullName;
            _messenger = new BaseMessenger<IModuleService>(GetType());
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

    }
}