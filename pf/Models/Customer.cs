using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pf.Models
{
    public class Customer
    {
        private DataTable _customerDataTable;

        public Customer(int customerId)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " * "
                    + "FROM customers "
                    + "WHERE "
                    + " id = '" + customerId + "'";

            _customerDataTable = dc.ReadAsDataTable();
        }

        public string CustomerCode
        {
            get { return _customerDataTable.Rows[0]["customer_code"].ToString(); }
        }
        public string CustomerName
        {
            get { return _customerDataTable.Rows[0]["customer_name"].ToString(); }
        }
        public string Address
        {
            get { return _customerDataTable.Rows[0]["address1"].ToString() + _customerDataTable.Rows[0]["address2"].ToString(); }
        }
        public string Tel
        {
            get { return _customerDataTable.Rows[0]["tel"].ToString(); }
        }



    }
}
