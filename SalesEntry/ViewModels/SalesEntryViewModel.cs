﻿using Prism.Commands;
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
        private int _slipNo = 0;
        private DateTime _slipDate;
        private string _customerCode;
        private string _customerName;
        private int _salesTaxRateId;
        private string _zipCode;
        private string _address;
        private string _tel;
        private int _subtotal;
        private int _salesTax;
        private int _total;


        private ObservableCollection<SalesDetail> _salesDetails = new ObservableCollection<SalesDetail>();

        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            EntryCommand = new DelegateCommand(EntryCommandExecute);


            SlipDate = DateTime.Today;

            /*
            // 初期値表示

            _salesHeader = _salesEntryModel.SalesHeader(1);
            SlipNo = (int)_salesHeader.Rows[0]["slip_no"];
            CustomerCode = _salesHeader.Rows[0]["customer_code"].ToString();
            CustomerName = _salesHeader.Rows[0]["customer_name"].ToString();
            SalesTaxRateId = 4;
            ZipCode = _salesHeader.Rows[0]["zip_code"].ToString();
            Address = _salesHeader.Rows[0]["address"].ToString();
            Tel = _salesHeader.Rows[0]["tel"].ToString();
            Subtotal = "1,000";
            SalesTax = "100";
            Total = 1100;
            */


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

        public int Subtotal
        {
            get { return _subtotal; }
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
