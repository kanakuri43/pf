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
            OpenConnection();
        }

        public DataTable SalesHeader
        {
            get
            {
                SQL = "SELECT "
                    + "  h.id "
                    + "  , h.slip_no "
                    + "  , h.slip_date "
                    + "  , customers.customer_code "
                    + "  , customers.customer_name "
                    + "  , customers.zip_code "
                    + "  , customers.address1 +  customers.address1 address"
                    + "  , customers.tel "
                    + "  , staffs.staff_code "
                    + "  , staffs.staff_name "
                    + "FROM "
                    + "  sales_header h "
                    + "  LEFT JOIN customers "
                    + "    ON h.customer_id = customers.id "
                    + "    AND customers.state = 0 "
                    + "  LEFT JOIN staffs "
                    + "    ON h.staff_id = staffs.id "
                    + "   AND staffs.state = 0 "
                    + "  LEFT JOIN sales_detail d "
                    + "    ON h.slip_no = d.slip_no "
                    + "    AND d.state = 0 "
                    + "WHERE "
                    + "  h.state = 0 "
                    + "  AND h.slip_no = 2 ";
                using (MySqlCommand command = new MySqlCommand(SQL, Connection))
                {
                    DataTable dt;
                    var addapter = new MySqlDataAdapter(command);
                    dt = new DataTable();
                    addapter.Fill(dt);
                    return dt;
                }
            }
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
        public int RegistDetail(int slipNo, int lineNo, int productId, int qty, int unitId, int catalogPrice, int unitPrice, int unitCost, int lineTaxPrice, int lineIncludeTaxPrice, int operatorId)
        {
            using (MySqlCommand command = new MySqlCommand("usp_sales_detail_entry", Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@arg_slip_no", slipNo);
                command.Parameters.AddWithValue("@arg_line_no", lineNo);
                command.Parameters.AddWithValue("@arg_product_id", productId);
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
