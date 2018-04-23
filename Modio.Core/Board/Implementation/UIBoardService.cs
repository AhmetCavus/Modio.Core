using Modio.Core.Container;
using Modio.Core.Module;
using System.Collections.Generic;

namespace Modio.Core.Board
{
    public abstract class UIBoardService : BaseBoardService<UIModuleService>
    {
        #region Attributes

        #endregion

        #region Properties

        public override IReadOnlyList<UIModuleService> Modules => throw new System.NotImplementedException();

        IServiceContainer<UIModuleService> _container;
        public override IServiceContainer<UIModuleService> Container => _container;

        #endregion

        #region Constructor

        public UIBoardService()
        {
            _container = new ModuleContainer<UIModuleService>();
        }

        #endregion

        #region Public Methods

        #endregion

        #region Event Handler

        #endregion

        #region Helper Methods

        #endregion

        #region IDisposable Support

        #endregion

    }
}