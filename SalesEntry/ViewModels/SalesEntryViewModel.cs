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
                var tr = new pf.Models.SalesTaxRate();
                //tr.TaxRatesList.AsEnumerable().Select(r => r["tax-rate"] = r["tax-rate"] + 100);

                return tr.TaxRatesList;
            }
        }
        public DelegateCommand PrintCommand { get; }
        public DelegateCommand EntryCommand { get; }

        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            EntryCommand = new DelegateCommand(EntryCommandExecute);

        }

        private void PrintCommandExecute()
        {
            // 印刷ボタンを押したときの処理

        }
        private void EntryCommandExecute()
        {
            // 登録ボタンを押したときの処理
            DatabaseController dc = new DatabaseController();
            dc.SQL = "sp_sales_entry(0)";
            dc.ExecuteProcedure();

        }


    }

}
