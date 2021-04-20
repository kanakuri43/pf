using MainMenu.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace MainMenu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Login>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
