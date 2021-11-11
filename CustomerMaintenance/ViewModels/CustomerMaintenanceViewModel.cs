using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerMaintenance.ViewModels
{
    public class CustomerMaintenanceViewModel : BindableBase
    {
        private string _customerCode;
        private string _customerName;
        private int _salesTaxRateId;
        private string _zipCode;
        private string _address1;
        private string _address2;
        private string _tel;

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
        public string Address1
        {
            get { return _address1; }
            set { SetProperty(ref _address1, value); }
        }
        public string Address2
        {
            get { return _address2; }
            set { SetProperty(ref _address2, value); }
        }
        public string Tel
        {
            get { return _tel; }
            set { SetProperty(ref _tel, value); }
        }

        public CustomerMaintenanceViewModel()
        {

        }
    }
}
