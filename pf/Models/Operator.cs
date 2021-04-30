using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

public class Operator
{
    public string OperatorName { get; }
    public string LoginPassword { get; }

    public Operator(string OperatorCode)
    {
        DataTable dt = new DataTable();

        var dc = new DatabaseController();
        var sql = "SELECT "
                + " * "
                + "FROM operators "
                + "WHERE "
                + " state = 1 "
                + " AND operator_code = '" + OperatorCode + "'";
        using (MySqlCommand command = new MySqlCommand(sql, dc.Connection))
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    this.OperatorName = reader["operator_name"].ToString();
                }
            }
        }


    }
}

