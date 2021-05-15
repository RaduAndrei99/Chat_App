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
        uint Connect();
        void CloseConnection(uint connection);
        OracleConnection Connection(uint connection);
    }
}
