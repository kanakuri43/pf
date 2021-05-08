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

        public int SlipNo { get; set; }
        public DateTime SlipDate { get; set; }
        public int OderSlipNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int SalesTaxRateId { get; set; }


        public DelegateCommand PrintCommand { get; }
        public DelegateCommand EntryCommand { get; }

        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            EntryCommand = new DelegateCommand(EntryCommandExecute);

            // 初期値表示
            SlipNo = 123;
            SlipDate = DateTime.Today;
            OderSlipNo = 456;
            CustomerCode = "100";
            CustomerName = "オビサン株式会社";
            SalesTaxRateId = 4;
        }

        private void PrintCommandExecute()
        {
            // 印刷ボタンを押したときの処理

        }
        private void EntryCommandExecute()
        {
            // 登録ボタンを押したときの処理
            var dc = new Models.SalesEntryModel();
            dc.SQL = "sp_sales_entry";
            dc.ExecuteProcedure();

        }


    }

}
