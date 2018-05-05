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

    }
}