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
        private string _operatorCode = string.Empty;
        private string _messageString = string.Empty;

        public LoginFormViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoginCommand = new DelegateCommand(LoginCommandExecute);

        }

        public string MessageString
        {
            get { return _messageString; }
            set { SetProperty(ref _messageString, value); }
        }
        public string OperatorCode
        {
            get { return _operatorCode; }
            set { SetProperty(ref _operatorCode, value); }
        }
        private string _loginPassword = string.Empty;
        public string LoginPassword
        {
            get { return _loginPassword; }
            set { SetProperty(ref _loginPassword, value); }
        }

        public DelegateCommand LoginCommand { get; }

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

        private void LoginCommandExecute()
        {
            var cf = new CommonFunctions();
            var oi = cf.OperatorCodeToId(OperatorCode);
            if (oi == -1)
            {
                MessageString = "User IDが違います。";
                return;
            }

            var o = new Operator(oi);
            if (o.TryLogin(PlainPassword) == false)
            {
                MessageString = "User ID または Password が違います。";
                return;
            }

            // Menu表示
            var p = new NavigationParameters();
            p.Add(nameof(MenuViewModel.OperatorCode), OperatorCode);
            _regionManager.RequestNavigate("ContentRegion", nameof(Menu), p);

            MessageString = string.Empty;
            OperatorCode = string.Empty;
            PlainPassword = string.Empty;
            Password = string.Empty;
        }


    }
}
