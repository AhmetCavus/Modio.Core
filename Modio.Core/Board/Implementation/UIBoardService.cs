using Modio.Core.Container;
using Modio.Core.Module;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Modio.Core.Board
{
    public abstract class UIBoardService : BaseBoardService<UIModuleService>, INotifyPropertyChanged
    {
        #region Attributes

        #endregion

        #region Properties

        IServiceContainer<UIModuleService> _container;

        public event PropertyChangedEventHandler PropertyChanged;

        public override IServiceContainer<UIModuleService> Container => _container;

        IList<UIModuleService> _modules;
        public override IList<UIModuleService> Modules
        {
            get => _modules;
            set
            {
                if (_modules == value) return;
                _modules = value;
                OnPropertyChanged();
            }
        }

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

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Helper Methods

        #endregion

        #region IDisposable Support

        #endregion

    }
}