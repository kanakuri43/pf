using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesEntry.Models
{
    public class SalesDetail : BindableBase
    {
        private int _productId;
        private string _productName;
        private double _qty;
        private int _unitId;
        private string _unitName;
        private double _unitCost;
        private double _unitPrice;
        private double _catalogPrice;
        private double _price;

        public int LineNo { get; set; }
        public int ProductId
        {
            get { return _productId; }
            set { SetProperty(ref _productId, value); }
        }

        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        public double Qty
        {
            get { return _qty; }
            set 
            { 
                SetProperty(ref _qty, value);
                Price = Qty * UnitPrice;
            }
        }
        public int UnitId
        {
            get { return _unitId; }
            set { SetProperty(ref _unitId, value); }
        }

        public string UnitName
        {
            get { return _unitName; }
            set { SetProperty(ref _unitName, value); }
        }

        public double UnitCost
        {
            get { return _unitCost; }
            set { SetProperty(ref _unitCost, value); }
        }
        public double UnitPrice
        {
            get { return _unitPrice; }
            set 
            {
                SetProperty(ref _unitPrice, value);
                Price = Qty * UnitPrice;
            }
        }
        public double CatalogPrice
        {
            get { return _catalogPrice; }
            set { SetProperty(ref _catalogPrice, value); }
        }
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

    }
}