using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Input;

namespace SalesEntry.ViewModels
{
    public class SalesEntryViewModel : BindableBase
    {
        private DataTable _salesHeader = new DataTable();
        private Models.SalesEntryModel _salesEntryModel = new Models.SalesEntryModel();

        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            EntryCommand = new DelegateCommand(EntryCommandExecute);

            // 初期値表示
            //var m = new Models.SalesEntryModel();
            _salesHeader = _salesEntryModel.SalesHeader(2);

            SlipNo = (int)_salesHeader.Rows[0]["slip_no"];
            SlipDate = (DateTime)_salesHeader.Rows[0]["slip_date"];
            CustomerCode = _salesHeader.Rows[0]["customer_code"].ToString();
            CustomerName = _salesHeader.Rows[0]["customer_name"].ToString();
            SalesTaxRateId = 4;
            ZipCode = _salesHeader.Rows[0]["zip_code"].ToString();
            Address = _salesHeader.Rows[0]["address"].ToString();
            Tel = _salesHeader.Rows[0]["tel"].ToString();
            Subtotal = "1,000";
            SalesTax = "100";
            Total = 1100;

        }

        public DataTable SalesTaxRates
        {
            get
            {
                var tr = new pf.Models.SalesTaxRate();
                return tr.TaxRatesList;
            }
        }

        public int SlipNo { get; set; }
        public DateTime SlipDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int SalesTaxRateId { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Subtotal { get; set; }
        public string SalesTax { get; set; }
        public int Total { get; set; }

        public DataTable SalesDetail
        {
            get
            {
                return _salesEntryModel.SalesDetail(SlipNo);
            }
        }


        public DelegateCommand PrintCommand { get; }
        public DelegateCommand EntryCommand { get; }
         


        private void PrintCommandExecute()
        {
            // 印刷ボタンを押したときの処理

        }
        private void EntryCommandExecute()
        {
            int slipNo = 0;
            // 登録ボタンを押したときの処理
            slipNo = _salesEntryModel.RegistHeader(0, SlipDate, 1, 1, 2);
            slipNo = _salesEntryModel.RegistDetail(slipNo, 1, 1, 23, 1, 1000, 900, 500, 0, 0, 2);
            SlipNo = slipNo;
        }

    }

}
