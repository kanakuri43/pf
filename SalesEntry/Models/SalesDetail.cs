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
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public string UnitName { get; set; }
        public double Cost { get; set; }
        public double UnitPrice { get; set; }

    }
}