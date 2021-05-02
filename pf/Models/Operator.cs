using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pf.Models
{
    public class Operator
    {
        private string _operatorCode;
        public string OperatorCode
        {
            get { return _operatorCode; }
        }
        public string OperatorName => _operatorDataTable.Rows[0]["operator_name"].ToString();

        private DataTable _operatorDataTable;

        public bool TryLogin(String loginPassword)
        {
            DataTable dt = new DataTable();

            var dc = new DatabaseController();
            dc.SQL = "SELECT * FROM operators "
                    + "WHERE "
                    + " state = 1 "
                    + " AND operator_code = '" + OperatorCode + "'"
                    + " AND login_password = '" + loginPassword + "'";
            dt = dc.ReadAsDataTable();

            if (dt.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Operator(string OperatorCode)
        {
            _operatorCode = OperatorCode;

            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " * "
                    + "FROM operators "
                    + "WHERE "
                    + " state = 1 "
                    + " AND operator_code = '" + OperatorCode + "'";
        
            _operatorDataTable = dc.ReadAsDataTable();

        

        }
    }
}
