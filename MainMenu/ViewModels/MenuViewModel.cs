using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainMenu.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        public MenuViewModel()
        {
            SalesEntryCommand = new DelegateCommand(SalesEntryCommandExecute);
            PaymentEntryCommand = new DelegateCommand(PaymentEntryCommandExecute);
            InvoiceCommand = new DelegateCommand(invoiceCommandExecute);

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
    }
}
