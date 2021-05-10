using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pf.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace SalesEntry.Models
{
    public class SalesEntryModel : DatabaseController
    {
        public SalesEntryModel()
        {
            base.OpenConnection();
        }

        public int RegistHeader(int slipNo, DateTime slipDate, int customerId, int staffId, int operatorId)
        {
            using (MySqlCommand command = new MySqlCommand("usp_sales_header_entry", Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@arg_slip_no", slipNo);
                command.Parameters.AddWithValue("@arg_slip_date", slipDate);
                command.Parameters.AddWithValue("@arg_customer_id", customerId);
                command.Parameters.AddWithValue("@arg_staff_id", staffId);
                command.Parameters.AddWithValue("@arg_operator_id", operatorId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return (int)reader["arg_slip_no"];
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        public int RegistDetail(int slipNo, int lineNo, int product_id, int qty, int unitId, int catalogPrice, int unitPrice, int unitCost, int lineTaxPrice, int lineIncludeTaxPrice, int operatorId)
        {
            using (MySqlCommand command = new MySqlCommand("usp_sales_detail_entry", Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@arg_slip_no", slipNo);
                command.Parameters.AddWithValue("@arg_line_no", lineNo);
                command.Parameters.AddWithValue("@arg_product_id", product_id);
                command.Parameters.AddWithValue("@arg_qty", qty);
                command.Parameters.AddWithValue("@arg_unit_id", unitId);
                command.Parameters.AddWithValue("@arg_catalog_price", catalogPrice);
                command.Parameters.AddWithValue("@arg_unit_price", unitPrice);
                command.Parameters.AddWithValue("@arg_unit_cost", unitCost);
                command.Parameters.AddWithValue("@arg_line_tax_price", lineTaxPrice);
                command.Parameters.AddWithValue("@arg_line_include_tax_price", lineIncludeTaxPrice);
                command.Parameters.AddWithValue("@arg_operator_id", operatorId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return (int)reader["arg_slip_no"];
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public bool DeleteHeader(int slipNo)
        {

            return true;
        }
        public bool DeleteDetail(int slipNo)
        {

            return true;
        }


    }
}
