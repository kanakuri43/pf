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
        private double _qty;
        private double _unitPrice;
        private double _price;

        public int LineNo { get; set; }
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public double Qty
        {
            get { return _qty; }
            set 
            { 
                SetProperty(ref _qty, value);
                Price = Qty * UnitPrice;
            }
        }

        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public double UnitCost { get; set; }
        public double UnitPrice
        {
            get { return _unitPrice; }
            set 
            {
                SetProperty(ref _unitPrice, value);
                Price = Qty * UnitPrice;
            }
        }
        public double CatalogPrice { get; set; }

        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

    }
}