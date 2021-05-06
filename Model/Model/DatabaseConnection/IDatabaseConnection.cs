using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DatabaseConnection
{
    public interface IDatabaseConnection
    {
        void Connect();
        void CloseConnection();

        OracleConnection Connection
        {
            get;
        }
    }
}
