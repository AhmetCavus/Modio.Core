using Modio.Core.App;
using Modio.Core.Board;
using Modio.Core.Component;
using Modio.Core.Container;
using Modio.Core.Messenger;
using Modio.Core.Module;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Modio.Core.Test
{
    public class MainTest
    {
        public ITestOutputHelper Output { get; }

        public MainTest(ITestOutputHelper output)
        {
            Output = output;

        }

        [Fact]
        public void StartTest()
        {
            IAppService<UIBoardService, UIModuleService> appService = new TestAppService(new ServiceDictionaryContainer<UIBoardService>());
            appService.AddBoard<TestBoardService>();
            appService.AddBoard<TestBoardService2>();
            appService.AddBoard<TestBoardService3>();

            appService.AddModule<TestBoardService, TestModuleService>();
            appService.AddModule<TestBoardService, TestModuleService2>();
            appService.AddModule<TestBoardService, TestModuleService3>();

            var module = appService.GetModule<TestBoardService, TestModuleService>();
            module?.Messenger.SendMessage<TestModuleService2, double>(14.53);
            module?.Messenger.SendMessage<TestModuleService3, int>(85);
            module?.Messenger.SendMessage<TestModuleService2, EventArgs>(EventArgs.Empty);
            module?.Messenger.RequestMessage<TestModuleService3, EventArgs>();

        }
    }

    class TestAppService : UIAppService
    {
        public TestAppService(IServiceContainer<UIBoardService> container) : base(container)
        {
        }

        protected override void OnActivateModule<TSubBoardService>(UIModuleService module)
        {
            throw new NotImplementedException();
        }

        protected override void OnAddBoard<TSubBoardService>(UIBoardService board)
        {
            throw new NotImplementedException();
        }

        protected override void OnAddModule<TSubBoardService>(UIModuleService module)
        {
            throw new NotImplementedException();
        }

        protected override void OnAddWorker<TSubWorkerModuleService>(WorkerModuleService worker)
        {
            throw new NotImplementedException();
        }

        protected override void OnRemoveBoard<TSubBoardService>(UIBoardService board)
        {
            throw new NotImplementedException();
        }

        protected override void OnRemoveModule<TSubBoardService>(UIModuleService module)
        {
            throw new NotImplementedException();
        }

        protected override void OnRemoveWorker<TSubWorkerModuleService>(WorkerModuleService worker)
        {
            throw new NotImplementedException();
        }

        protected override void OnSelectBoard<TSubBoardService>(UIBoardService board)
        {
            throw new NotImplementedException();
        }
    }


    class TestBoardService : UIBoardService
    {
    }

    class TestBoardService2 : UIBoardService
    {
    }

    class TestBoardService3 : UIBoardService
    {
    }

    class TestModuleService : UIModuleService
    {
        public TestModuleService(TestComponent1 comp1, TestComponent2 comp2, TestComponent3 comp)
        {
            System.Diagnostics.Debug.WriteLine("{0} created", GetType().FullName);
        }

        public override bool IsUI => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override bool IsWorker => throw new NotImplementedException();

        public override bool IsActive => throw new NotImplementedException();

        public override IModuleMeta MetaInfo => throw new NotImplementedException();

        public void OnMessage(object sender, double param)
        {
            System.Diagnostics.Debug.WriteLine(param);
        }

        public void OnMessage(object sender, ResolveEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }

        public void OnRequest(object sender, RequestEventArgs e) {
            if (!e.Data.ToString().Equals("test")) return;
        }
    }

    class TestModuleService2 : UIModuleService
    {
        public override bool IsUI => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override bool IsWorker => throw new NotImplementedException();

        public override bool IsActive => throw new NotImplementedException();

        public override IModuleMeta MetaInfo => throw new NotImplementedException();

        public void OnMessage(object sender, double param)
        {
            System.Diagnostics.Debug.WriteLine(param);
        }

        public void OnRequest(object sender, RequestEventArgs e)
        {
            if (e.Data != typeof(EventArgs)) return;
            Messenger.SendMessage(e.From, EventArgs.Empty);
        }
    }

    class TestModuleService3 : UIModuleService
    {
        public TestModuleService3(TestComponent3 comp)
        {
            System.Diagnostics.Debug.WriteLine("{0} created", GetType().FullName);
        }

        public override bool IsUI => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override bool IsWorker => throw new NotImplementedException();

        public override bool IsActive => throw new NotImplementedException();

        public override IModuleMeta MetaInfo => throw new NotImplementedException();

        public void OnMessage(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }

        public void OnRequest(object sender, RequestEventArgs e)
        {
            if (e.Data != typeof(EventArgs)) return;
            Messenger.SendMessage(e.Sender, new ResolveEventArgs("Test"));
        }
    }

    class TestComponent1 : BaseComponentService
    {
        public override string Id => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override event EventHandler<EventArgs> Ready;

        public override void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("{0} is initialized", GetType().FullName);
        }

        protected override void OnDisposing()
        {
            throw new NotImplementedException();
        }
    }

    class TestComponent2 : BaseComponentService
    {
        public override string Id => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override event EventHandler<EventArgs> Ready;

        public override void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("{0} is initialized", GetType().FullName);
        }

        protected override void OnDisposing()
        {
            throw new NotImplementedException();
        }
    }

    class TestComponent3 : BaseComponentService
    {
        public override string Id => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override event EventHandler<EventArgs> Ready;

        public override void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("{0} is initialized", GetType().FullName);
        }

        protected override void OnDisposing()
        {
            throw new NotImplementedException();
        }
    }

}
