using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace SalesEntry.ViewModels
{
    public class SalesEntryViewModel : BindableBase
    {
        public DataTable SalesTaxRates
        {
            get 
            {
                pf.Models.SalesTaxRate taxRate = new pf.Models.SalesTaxRate();
                return taxRate.TaxRatesList;
            }
        }
        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);

        }
        public DelegateCommand PrintCommand { get; }

        private void PrintCommandExecute()
        {
            // ボタンを押したときの処理

        }


    }

}
