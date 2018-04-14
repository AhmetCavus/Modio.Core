using Modio.Core.App;
using Modio.Core.Board;
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
            IAppService appService = new TestAppService(new ServiceDictionaryContainer<IBoardService>());
            appService.AddBoard<TestBoardService>();
            appService.AddBoard<TestBoardService2>();
            appService.AddBoard<TestBoardService3>();

            var boardService = appService.GetBoard<TestBoardService2>();
            boardService.AddModule<TestModuleService>();
            boardService.AddModule<TestModuleService2>();
            boardService.AddModule<TestModuleService3>();

            foreach (var board in appService.Boards)
            {
                var module = board.GetModule<TestModuleService>();
                module?.Messenger.SendMessage<TestModuleService2, double>(14.53);
                module?.Messenger.SendMessage<TestModuleService3, int>(85);
                module?.Messenger.SendMessage<TestModuleService2, EventArgs>(EventArgs.Empty);
                module?.Messenger.RequestMessage<TestModuleService2, EventArgs>();
            }
        }
    }

    class TestAppService : BaseAppService
    {
        public TestAppService(IServiceContainer<IBoardService> container) : base(container)
        {
        }

        protected override void OnAddBoard(IBoardService board)
        {
            board.Container = new ServiceDictionaryContainer<IModuleService>();
        }

        protected override void OnRemoveBoard(IBoardService board)
        {
            throw new NotImplementedException();
        }

        protected override void OnSelectBoard(IBoardService board)
        {
            throw new NotImplementedException();
        }
    }

    class TestBoardService : BaseBoardService
    {
    }

    class TestBoardService2 : BaseBoardService
    {
    }

    class TestBoardService3 : BaseBoardService
    {
    }

    class TestModuleService : BaseModuleService
    {
        public void OnMessage(object sender, double param)
        {
            System.Diagnostics.Debug.WriteLine(param);
        }

        public void OnRequest(object sender, MessageEventArgs e) {
            if (!e.Data.ToString().Equals("test")) return;
        }
    }

    class TestModuleService2 : BaseModuleService
    {
        public void OnMessage(object sender, double param)
        {
            System.Diagnostics.Debug.WriteLine(param);
        }

        public void OnRequest(object sender, MessageEventArgs e)
        {
            if (!e.Data.ToString().Equals("test")) return;
        }
    }

    class TestModuleService3 : BaseModuleService
    {
        public void OnMessage(object sender, double param)
        {
            System.Diagnostics.Debug.WriteLine(param);
        }

        public void OnRequest(object sender, MessageEventArgs e)
        {
            if (!e.Data.ToString().Equals("test")) return;
        }
    }

}
