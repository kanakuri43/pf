using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using MainMenu.Views;
using System.Data;

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
            if (TryLogin("555", "555"))
            {
                // Menu表示
                _regionManager.RequestNavigate("ContentRegion", nameof(Menu));

            }
        }

        private bool TryLogin(string operatorCode, String loginPassword)
        {
            DataTable dt = new DataTable();

            var dc = new DatabaseController();
            dc.SQL = "SELECT * FROM operators "
                    + "WHERE "
                    + " state = 1 "
                    + " AND operator_code = '" + operatorCode + "'"
                    + " AND login_password = '" + loginPassword + "'";
            dt = dc.ReadAsDataTable();

            if (dt.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
