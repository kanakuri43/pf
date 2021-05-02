using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using MainMenu.Views;
using System.Data;
using System.ComponentModel;
using pf.Models;

namespace MainMenu.ViewModels
{
    public class LoginFormViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private string _plainPassword = string.Empty;
        private string _userID = string.Empty;
        private string _messageString = string.Empty;

        public string MessageString
        {
            get { return _messageString; }
            set { SetProperty(ref _messageString, value); }
        }
        public string UserID
        {
            get { return _userID; }
            set { SetProperty(ref _userID, value); }
        }
        private string _loginPassword = string.Empty;
        public string LoginPassword
        {
            get { return _loginPassword; }
            set { SetProperty(ref _loginPassword, value); }
        }

        public LoginFormViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoginCommand = new DelegateCommand(LoginCommandExecute);

        }
        public DelegateCommand LoginCommand { get; }
        private void LoginCommandExecute()
        {
            Operator o = new Operator(UserID);
            if (o.TryLogin(PlainPassword))
            {

                // Menu表示
                var p = new NavigationParameters();
                p.Add(nameof(MenuViewModel.OperatorCode), UserID);
                _regionManager.RequestNavigate("ContentRegion", nameof(Menu), p);

                MessageString = string.Empty;
                UserID = string.Empty;
                PlainPassword = string.Empty;
                Password = string.Empty;
            }
            else
            {
                MessageString = "User ID または Password が違います。";
            }
        }

        //private bool TryLogin(string operatorCode, String loginPassword)
        //{
        //    DataTable dt = new DataTable();

        //    var dc = new DatabaseController();
        //    dc.SQL = "SELECT * FROM operators "
        //            + "WHERE "
        //            + " state = 1 "
        //            + " AND operator_code = '" + operatorCode + "'"
        //            + " AND login_password = '" + loginPassword + "'";
        //    dt = dc.ReadAsDataTable();

        //    if (dt.Rows.Count == 1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public string Password
        {
            get => new string('*', PlainPassword.Length);
            set
            {
                if (value.Length < PlainPassword.Length)
                {
                    PlainPassword = PlainPassword.Substring(0, value.Length);
                }
                else if (value.Length > PlainPassword.Length)
                {
                    PlainPassword += value.Substring(PlainPassword.Length, value.Length - PlainPassword.Length);
                }
                else
                {
                    PlainPassword = value;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }
        public string PlainPassword
        {
            get => _plainPassword;
            set
            {
                _plainPassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlainPassword)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
