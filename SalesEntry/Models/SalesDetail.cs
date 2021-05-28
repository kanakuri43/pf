using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesEntry.Models
{
    public class SalesDetail
    {
        public int LineNo { get; set; }
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public double UnitCost { get; set; }
        public double UnitPrice { get; set; }
        public double CatalogPrice { get; set; }

    }
}