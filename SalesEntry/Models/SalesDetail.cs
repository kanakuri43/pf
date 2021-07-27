using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pf.Models;

namespace SalesEntry.Models
{
    public class SalesDetail : BindableBase
    {
        private int _productId;
        private string _productCode;
        private string _productName;
        private int _qty;
        private int _unitId;
        private string _unitName;
        private int _unitCost;
        private int _unitPrice;
        private int _catalogPrice;
        private int _price;

        public int LineNo { get; set; }
        public int ProductId
        {
            get { return _productId; }
            set { SetProperty(ref _productId, value); }
        }

        public string ProductCode
        {
            get { return _productCode; }
            set 
            {
                SetProperty(ref _productCode, value);

                var cf = new CommonFunctions();
                var pi = cf.ProductCodeToId(ProductCode);
                if (pi == -1)
                {
                    ProductName = "商品が見つかりません";
                    return;
                }
                var p = new Product(pi);
                ProductId = pi;
                ProductName = p.ProductName;
            }
        }

        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        public int Qty
        {
            get { return _qty; }
            set 
            { 
                SetProperty(ref _qty, value);
                Price = (int)(Qty * UnitPrice);
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

        public int UnitCost
        {
            get { return _unitCost; }
            set { SetProperty(ref _unitCost, value); }
        }
        public int UnitPrice
        {
            get { return _unitPrice; }
            set 
            {
                SetProperty(ref _unitPrice, value);
                Price = Qty * UnitPrice;
            }
        }
        public int CatalogPrice
        {
            get { return _catalogPrice; }
            set { SetProperty(ref _catalogPrice, value); }
        }
        public int Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

    }
}