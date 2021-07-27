using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pf.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace SalesEntry.Models
{
    public class SalesEntryModel : DatabaseController
    {

        public SalesEntryModel()
        {
            OpenConnection();
        }

        public DataTable SalesHeader(int slipNo)
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
                + "  AND h.slip_no = " + slipNo.ToString();
            using (MySqlCommand command = new MySqlCommand(SQL, Connection))
            {
                DataTable dt;
                var addapter = new MySqlDataAdapter(command);
                dt = new DataTable();
                addapter.Fill(dt);
                return dt;
            }
        }
        public DataTable SalesDetail(int slipNo)
        {

            SQL = "SELECT " 
                + "  d.id "
                + "  , products.product_code "
                + "  , products.product_name "
                + "  , d.qty "
                + "  , units.unit_name "
                + "  , d.unit_price "
                + "  , d.qty* d.unit_price price "
                + "FROM "
                + "  sales_detail d "
                + "  LEFT JOIN products "
                + "    ON d.product_id = products.id "
                + "    AND products.state = 0 "
                + "  LEFT JOIN units "
                + "    ON d.unit_id = units.id "
                + "    AND units.state = 0 "
                + "WHERE "
                + "  d.state = 0 "
                + "  AND d.slip_no = " + slipNo.ToString();
            using (MySqlCommand command = new MySqlCommand(SQL, Connection))
            {
                DataTable dt;
                var addapter = new MySqlDataAdapter(command);
                dt = new DataTable();
                addapter.Fill(dt);
                return dt;
            }
        }
        public int RegistSalesHeader(int slipNo, DateTime slipDate, string customerCode, string staffCode, string operatorCode)
        {
            var cf = new CommonFunctions();

            using (MySqlCommand command = new MySqlCommand("usp_sales_header_regist", Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@arg_slip_no", slipNo);
                command.Parameters.AddWithValue("@arg_slip_date", slipDate);
                command.Parameters.AddWithValue("@arg_customer_id", cf.CustomerCodeToId(customerCode));
                command.Parameters.AddWithValue("@arg_staff_id", cf.StaffCodeToId(staffCode));
                command.Parameters.AddWithValue("@arg_operator_id", cf.OperatorCodeToId(operatorCode));
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
        public int RegistSalesDetail(int slipNo, ObservableCollection<SalesDetail> salesDetails, string operatorCode)
        {
            var cf = new CommonFunctions();
            foreach (var sd in salesDetails)
            {
                if (sd.ProductId != 0)
                {
                    using (MySqlCommand command = new MySqlCommand("usp_sales_detail_regist", Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@arg_slip_no", slipNo);
                        command.Parameters.AddWithValue("@arg_line_no", sd.LineNo);
                        command.Parameters.AddWithValue("@arg_product_id", sd.ProductId);
                        command.Parameters.AddWithValue("@arg_qty", sd.Qty);
                        command.Parameters.AddWithValue("@arg_unit_id", sd.UnitId);
                        command.Parameters.AddWithValue("@arg_catalog_price", sd.CatalogPrice);
                        command.Parameters.AddWithValue("@arg_unit_price", sd.UnitPrice);
                        command.Parameters.AddWithValue("@arg_unit_cost", sd.UnitCost);
                        command.Parameters.AddWithValue("@arg_line_sales_tax_price", 0);
                        command.Parameters.AddWithValue("@arg_line_include_sales_tax_price", 0);
                        command.Parameters.AddWithValue("@arg_operator_id", cf.OperatorCodeToId(operatorCode));
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            return slipNo;
        }

        public bool DeleteSales(int slipNo, string operatorCode)
        {
            var cf = new CommonFunctions();
            using (MySqlCommand command = new MySqlCommand("usp_sales_delete", Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@arg_slip_no", slipNo);
                command.Parameters.AddWithValue("@arg_operator_id", cf.OperatorCodeToId(operatorCode));
                command.ExecuteNonQuery();
            }

            return true;
        }


    }
}
