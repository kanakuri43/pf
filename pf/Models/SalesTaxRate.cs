using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace pf.Models
{
    public class SalesTaxRate
    {
        public DataTable TaxRatesList
        {
            get 
            {
                DatabaseController dc = new DatabaseController();
                DataTable dt = new DataTable();
                string sql = "SELECT  "
                          + "  id  "
                          + "  , tax_rate  "
                          + "FROM "
                          + "  sales_tax_rates "
                          + "ORDER BY "
                          + "  tax_rate  "
                          ;
                dc.SQL = sql;

                return dc.ReadAsDataTable(); 
            }
        }

        public SalesTaxRate()
        {

        }
    }
}
