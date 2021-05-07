using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DatabaseConnection
{
    public class OracleDatabaseConnection : IDatabaseConnection
    {
        private OracleConnection Conn = null;
        private string ConnectionString = String.Empty;

        private const string UserId = "stud_nume";
        private const string Password = "stud_parola";
        private const string Hostname = "localhost";
        private const string Port = "1521";
        private const string Sid = "xe";

        public OracleDatabaseConnection()
        {
            ConnectionString = $"User Id={UserId};Password={Password};Data Source={Hostname}:{Port}/{Sid};Pooling=True;";
        }
        public void Connect()
        {
            try
            {
                if (Conn == null)
                {
                    Conn = new OracleConnection(ConnectionString);
                    Conn.Open();
                    Console.WriteLine("Connection opened");
                }
            }
            catch(OracleException oracleEx)
            {
                Console.WriteLine(oracleEx.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (Conn != null)
                {
                    Conn.Close();
                    //conn.Dispose();
                    Conn = null;
                    Console.WriteLine("Connection closed");
                }
            }
            catch(OracleException oracleEx)
            {
                Console.WriteLine(oracleEx.Message);
            }
        }

        public OracleConnection Connection
        {
            get { return Conn; }
        }
    }
}
