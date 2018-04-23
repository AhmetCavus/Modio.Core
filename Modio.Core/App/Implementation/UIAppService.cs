using Modio.Core.Board;
using Modio.Core.Container;
using Modio.Core.Module;

namespace Modio.Core.App
{
    public abstract class UIAppService : BaseAppService<UIBoardService, UIModuleService>
    {

        public UIAppService(IServiceContainer<UIBoardService> container) : base(container)
        {
        }

        protected override void OnActivateModule(UIModuleService module)
        {
            System.Diagnostics.Debug.WriteLine("OnActivateModule {0} {1}", module.Id, module.Name);
        }

        protected override void OnAddBoard(UIBoardService board)
        {
            System.Diagnostics.Debug.WriteLine("OnAddBoard {0} {1}", board.Id, board.Name);
        }

        protected override void OnAddComponent(WorkerModuleService worker)
        {
            System.Diagnostics.Debug.WriteLine("OnAddComponent {0} {1}", worker.Id, worker.Name);
        }

        protected override void OnAddModule(UIModuleService module)
        {
            System.Diagnostics.Debug.WriteLine("OnAddModule {0} {1}", module.Id, module.Name);
        }

        protected override void OnAddWorker(WorkerModuleService worker)
        {
            System.Diagnostics.Debug.WriteLine("OnAddWorker {0} {1}", worker.Id, worker.Name);
        }

        protected override void OnRemoveBoard(UIBoardService board)
        {
            System.Diagnostics.Debug.WriteLine("OnRemoveBoard {0} {1}", board.Id, board.Name);
        }

        protected override void OnRemoveComponent(WorkerModuleService worker)
        {
            System.Diagnostics.Debug.WriteLine("OnRemoveComponent {0} {1}", worker.Id, worker.Name);
        }

        protected override void OnRemoveModule(UIModuleService module)
        {
            System.Diagnostics.Debug.WriteLine("OnRemoveModule {0} {1}", module.Id, module.Name);
        }

        protected override void OnRemoveWorker(WorkerModuleService worker)
        {
            System.Diagnostics.Debug.WriteLine("OnRemoveWorker {0} {1}", worker.Id, worker.Name);
        }

        protected override void OnSelectBoard(UIBoardService board)
        {
            System.Diagnostics.Debug.WriteLine("OnSelectBoard {0} {1}", board.Id, board.Name);
        }
    }
}