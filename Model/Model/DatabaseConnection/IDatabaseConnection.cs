using Oracle.ManagedDataAccess.Client;

namespace Model.DatabaseConnection
{
    public interface IDatabaseConnection
    {
        uint Connect();
        void CloseConnection(uint connection);
        OracleConnection Connection(uint connection);
    }
}
