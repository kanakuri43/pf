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
        //private string _operatorCode;
        //private int _operatorId;
        private DataTable _operatorDataTable;

        public Operator(int operatorId)
        {
            //_operatorId = operatorId;

            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " * "
                    + "FROM operators "
                    + "WHERE "
                    + " id = '" + operatorId + "'";

            _operatorDataTable = dc.ReadAsDataTable();
        }
        public string OperatorCode
        {
            get
            {
                return _operatorDataTable.Rows[0]["operator_code"].ToString();
            }
        }
        public string OperatorName
        {
            get 
            {
                return _operatorDataTable.Rows[0]["operator_name"].ToString(); 
            }
        }


        //public string OperatorName => _operatorDataTable.Rows[0]["operator_name"].ToString();


        public bool TryLogin(String loginPassword)
        {
            DataTable dt = new DataTable();

            var dc = new DatabaseController();
            dc.SQL = "SELECT * FROM operators "
                    + "WHERE "
                    + " state = 0 "
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
    }
}
