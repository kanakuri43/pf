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
using System.Windows;
using System.Windows.Controls;

namespace SalesEntry.ViewModels
{
    public class SalesEntryViewModel : BindableBase
    {
        private DataTable _salesHeader = new DataTable();
        private Models.SalesEntryModel _salesEntryModel = new Models.SalesEntryModel();
        private int _slipNo = 0;
        private DateTime _slipDate;
        private string _customerCode;
        private string _customerName;
        private int _salesTaxRateId;
        private string _zipCode;
        private string _address;
        private string _tel;
        private string _messageString;
        private int _subtotal;
        private int _salesTax;
        private int _total;


        private ObservableCollection<SalesDetail> _salesDetails = new ObservableCollection<SalesDetail>();

        public SalesEntryViewModel()
        {
            CustomerSearchCommand = new DelegateCommand<TextBox>(CustomerSearchCommandExecute);
            ProductSearchCommand = new DelegateCommand<DataGrid>(ProductSearchCommandExecute);
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            EntryCommand = new DelegateCommand(EntryCommandExecute);


            SlipDate = DateTime.Today;

            // 初期値表示
            MessageString = "";
            
            // 明細データ作成
            for (var i = 1; i < 11; i++)
            {
                var sd = new SalesDetail();
                sd.LineNo = i;
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

        public int SlipNo
        {
            get { return _slipNo; }
            set { SetProperty(ref _slipNo, value); }
        }
        public DateTime SlipDate
        {
            get { return _slipDate; }
            set { SetProperty(ref _slipDate, value); }
        }
        public string CustomerCode
        {
            get { return _customerCode; }
            set { SetProperty(ref _customerCode, value); }
        }
        public string CustomerName
        {
            get { return _customerName; }
            set { SetProperty(ref _customerName, value); }
        }
        public int SalesTaxRateId
        {
            get { return _salesTaxRateId; }
            set { SetProperty(ref _salesTaxRateId, value); }
        }
        public string ZipCode
        {
            get { return _zipCode; }
            set { SetProperty(ref _zipCode, value); }
        }
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        public string Tel
        {
            get { return _tel; }
            set { SetProperty(ref _tel, value); }
        }
        public string MessageString
        {
            get { return _messageString; }
            set { SetProperty(ref _messageString, value); }
        }


        public int Subtotal
        {
            get 
            {
                _subtotal = SalesDetails.Sum(x => ((int)x.UnitPrice * (int)x.Qty));
                return _subtotal; 
            }
            set { SetProperty(ref _subtotal, value); }
        }

        public int SalesTax
        {
            get { return _salesTax; }
            set { SetProperty(ref _salesTax, value); }
        }
        public int Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }

        public ObservableCollection<SalesDetail> SalesDetails
        {
            get { return _salesDetails; }
            set 
            {
                Subtotal = SalesDetails.Sum(x => ((int)x.UnitPrice * (int)x.Qty));
                SetProperty(ref _salesDetails, value); 
            }
        }




        public DelegateCommand<TextBox> CustomerSearchCommand { get; }
        public DelegateCommand<DataGrid> ProductSearchCommand { get; }
        public DelegateCommand PrintCommand { get; }
        public DelegateCommand EntryCommand { get; }



        private void CustomerSearchCommandExecute(TextBox customerCode)
        {
            MessageString = "";
            if (customerCode.Text == "")
            {
                // 空欄だったら検索ウインドウ表示

            }
            else
            {
                // コードが入力されていたらマスタを検索

                var cf = new CommonFunctions();
                var ci = cf.CustomerCodeToId(customerCode.Text);
                if (ci == -1)
                {
                    MessageString = "得意先が見つかりません";
                    return;
                }
                var c = new Customer(ci);

                CustomerName = c.CustomerName;
                Address = c.Address;
                Tel = c.Tel;

                //UIElement element =  as UIElement;
                customerCode.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));


            }
        }

        private void ProductSearchCommandExecute(DataGrid SalesDetailsDataGrid)
        {

        }

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
