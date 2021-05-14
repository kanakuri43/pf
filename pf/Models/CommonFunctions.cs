using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace pf.Models
{
    public class CommonFunctions
    {
        public int OperatorCodeToId(string operatorCode)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " id "
                    + "FROM operators "
                    + "WHERE "
                    + " state = 0 "
                    + " AND operator_code = '" + operatorCode + "'";
            var dr = dc.ReadAsDataReader();
            if (dr == null)
            {
                return -1;
            }
            else
            {
                dr.Read();
                return (int)dr[0];
            }
        }
    }
}
