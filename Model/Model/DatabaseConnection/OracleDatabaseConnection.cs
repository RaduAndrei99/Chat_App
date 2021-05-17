using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Model.DatabaseConnection
{
    public class OracleDatabaseConnection : IDatabaseConnection
    {
        private Dictionary<uint, OracleConnection> _connections;
        private string _connectionString = String.Empty;

        private const string UserId = "stud_nume";
        private const string Password = "stud_parola";
        private const string Hostname = "localhost";
        private const string Port = "1521";
        private const string Sid = "xe";
        private const string Pooling = "True";

        public OracleDatabaseConnection()
        {
            _connections = new Dictionary<uint, OracleConnection>();
            _connectionString = $"User Id={UserId};Password={Password};Data Source={Hostname}:{Port}/{Sid};Pooling={Pooling};";
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public uint Connect()
        {
            uint connectionId = GetNextConnectionLocationInConnections();
            try
            {
                OracleConnection conn = new OracleConnection(_connectionString);
                _connections.Add(connectionId, conn);

                conn.Open();
                Console.WriteLine($"Connection {connectionId} opened");
            }
            catch(OracleException oracleEx)
            {
                Console.WriteLine(oracleEx.Message);
            }

            return connectionId;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CloseConnection(uint connectionId)
        {
            try
            {
                _connections[connectionId].Close();
                _connections[connectionId].Dispose();
                _connections.Remove(connectionId);

                Console.WriteLine($"Connection {connectionId} closed");
                
            }
            catch(OracleException oracleEx)
            {
                Console.WriteLine(oracleEx.Message);
            }
        }

        public OracleConnection Connection(uint connectionId)
        {
            return _connections[connectionId];
        }

        private uint GetNextConnectionLocationInConnections()
        {
            uint count = 0;
            while (_connections.ContainsKey(++count));
            return count;
        }
    }
}
