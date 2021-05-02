using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using pf.Models;

namespace MainMenu.ViewModels
{
    public class MenuViewModel : BindableBase, INavigationAware
    {
        public MenuViewModel()
        {
            SalesEntryCommand = new DelegateCommand(SalesEntryCommandExecute);
            PaymentEntryCommand = new DelegateCommand(PaymentEntryCommandExecute);
            InvoiceCommand = new DelegateCommand(invoiceCommandExecute);
        }
        private string _operatorCode = string.Empty;
        public string OperatorCode
        {
            get { return _operatorCode; }
            set { SetProperty(ref _operatorCode, value); }
        }
        public int Category { get; set; }
        public int Operation { get; set; }

        private string _information1 = string.Empty;
        public string Information1
        {
            get { return _information1; }
            set { SetProperty(ref _information1, value); }
        }

        private string _information0 = string.Empty;

        public string Information0
        {
            get { return _information0; }
            set { SetProperty(ref _information1, value); }
        }

        public DelegateCommand SalesEntryCommand { get; }
        public DelegateCommand PaymentEntryCommand { get; }
        public DelegateCommand InvoiceCommand { get; }
        private void SalesEntryCommandExecute()
        {
            var p = new System.Diagnostics.Process();
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;

            p.StartInfo.FileName = directory + @"SalesEntry.exe";
            p.Start();
        }
        private void PaymentEntryCommandExecute()
        {
            var p = new System.Diagnostics.Process();
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;

            p.StartInfo.FileName = directory + @"PaymentEntry.exe";
            p.Start();
        }
        private void invoiceCommandExecute()
        {
            var p = new System.Diagnostics.Process();
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;

            p.StartInfo.FileName = directory + @"Invoice.exe";
            p.Start();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OperatorCode = navigationContext.Parameters.GetValue<string>(nameof(OperatorCode));

            Operator o = new Operator(OperatorCode);
            Information1 = "Operator : " + o.OperatorName;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
