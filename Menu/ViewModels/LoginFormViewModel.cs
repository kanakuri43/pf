using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using MainMenu.Views;

namespace MainMenu.ViewModels
{
    public class LoginFormViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public LoginFormViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoginCommand = new DelegateCommand(LoginCommandExecute);

        }
        public DelegateCommand LoginCommand { get; }
        private void LoginCommandExecute()
        {


            // Menu表示
            _regionManager.RequestNavigate("ContentRegion", nameof(Menu));
        }


    }
}
