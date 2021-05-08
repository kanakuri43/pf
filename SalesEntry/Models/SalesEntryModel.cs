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
        public bool ExecuteProcedure()
        {
            using (MySqlCommand command = new MySqlCommand(SQL, Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader reader = command.ExecuteReader();
                return (reader != null);
            }
        }


    }
}
