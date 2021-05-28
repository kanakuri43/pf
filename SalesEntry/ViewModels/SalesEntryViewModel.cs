using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SalesEntry.Models;
using pf.Models;

namespace SalesEntry.ViewModels
{
    public class SalesEntryViewModel : BindableBase
    {
        private DataTable _salesHeader = new DataTable();
        private Models.SalesEntryModel _salesEntryModel = new Models.SalesEntryModel();
        private ObservableCollection<SalesDetail> _salesDetails = new ObservableCollection<SalesDetail>();

        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            EntryCommand = new DelegateCommand(EntryCommandExecute);

            // 初期値表示
            //var m = new Models.SalesEntryModel();
            _salesHeader = _salesEntryModel.SalesHeader(1);

            SlipNo = (int)_salesHeader.Rows[0]["slip_no"];
            SlipDate = DateTime.Today;
            CustomerCode = _salesHeader.Rows[0]["customer_code"].ToString();
            CustomerName = _salesHeader.Rows[0]["customer_name"].ToString();
            SalesTaxRateId = 4;
            ZipCode = _salesHeader.Rows[0]["zip_code"].ToString();
            Address = _salesHeader.Rows[0]["address"].ToString();
            Tel = _salesHeader.Rows[0]["tel"].ToString();
            Subtotal = "1,000";
            SalesTax = "100";
            Total = 1100;

            // 明細データ作成
            for (var i = 1; i < 4; i++)
            {
                var sd = new SalesDetail();
                sd.LineNo = i;
                sd.ItemName = "A10" + i.ToString() + "  " + "B10" + i.ToString();
                sd.Qty = (i * 10);
                _salesDetails.Add(sd);
            }

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
        public ObservableCollection<SalesDetail> SalesDetails
        {
            get { return _salesDetails; }
            set { SetProperty(ref _salesDetails, value); }
        }




        public DelegateCommand PrintCommand { get; }
        public DelegateCommand EntryCommand { get; }
         


        private void PrintCommandExecute()
        {
            // 印刷ボタンを押したときの処理

        }
        private void EntryCommandExecute()
        {
            // 登録ボタンを押したときの処理

            int slipNo = 0;
            slipNo = _salesEntryModel.RegistHeader(0, SlipDate, CustomerCode, "0", "2");
            slipNo = _salesEntryModel.RegistDetail(slipNo, _salesDetails);
            SlipNo = slipNo;
        }

    }

}
