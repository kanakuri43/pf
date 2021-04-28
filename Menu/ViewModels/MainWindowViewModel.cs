using Prism.Mvvm;
using System;
using Prism.Commands;
using Prism.Regions;
using MainMenu.Views;

namespace MainMenu.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //menucommand = new DelegateCommand(menucommandExecute);
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(LoginForm));

        }
        public DelegateCommand menucommand { get; }
        private void menucommandExecute()
        {
            //_regionManager.RequestNavigate("ContentRegion", nameof(Menu));
        }

    }
}
