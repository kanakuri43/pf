using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pf.Models
{
    public class Product
    {
        private DataTable _productDataTable;

        public Product(int productId)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " * "
                    + "FROM products "
                    + "WHERE "
                    + " id = '" + productId + "'";

            _productDataTable = dc.ReadAsDataTable();
        }

        public string ProductName
        {
            get { return _productDataTable.Rows[0]["product_name"].ToString(); }
        }

    }
}
