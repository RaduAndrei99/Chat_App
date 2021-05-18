using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Model.DatabaseConnection
{
    public class OracleDatabaseConnection : IDatabaseConnection
    {
        /// <summary>
        /// Dicitonar cu conexiunile deschise cu baza de date
        /// </summary>
        private Dictionary<uint, OracleConnection> _connections;

        /// <summary>
        /// String-ul cu datele de conectare la baza de date
        /// </summary>
        private string _connectionString = String.Empty;

        /// <summary>
        /// Constructorul clasei. Acesta face conexiunea la baza de date.
        /// </summary>
        /// <param name="databaseUsername">Numele utilizatorului bazei de date</param>
        /// <param name="databaseUserPassword">Numele utilizatorului bazei de date</param>
        /// <param name="hostname">Adresa IP al host-ului</param>
        /// <param name="port">Port-ul pe care ruleaza baza de date</param>
        /// <param name="sid">Sid-ul bazei de date</param>
        /// <param name="pooling">Setarea care indica daca baza de date va face pooling la conexiuni sau nu</param>
        /// <returns></returns>
        public OracleDatabaseConnection(in string databaseUsername, in string databaseUserPassword, in string hostname, in string port, in string sid, in bool pooling = true)
        {
            _connections = new Dictionary<uint, OracleConnection>();

            string poolingValue = (pooling == true) ? "True" : "False";
            _connectionString = $"User Id={databaseUsername};Password={databaseUserPassword};Data Source={hostname}:{port}/{sid};Pooling={poolingValue};";
        }

        /// <summary>
        /// Deschide o conexiunea cu baza de date
        /// </summary>
        /// <returns>Id-ul conexiunii deschise cu baza de date</returns>
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

        /// <summary>
        /// Inchide o conexiunea cu baza de date
        /// </summary>
        /// <param name="connectionId">Id-ul conexiunii deschise cu baza de date</param>
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

        /// <summary>
        /// Returneaza o conexiune din dictionar
        /// </summary>
        /// <param name="connectionId">Id-ul conexiunii deschise cu baza de date</param>
        /// <returns>Obiectul conexiune din dictionar</returns>
        public OracleConnection Connection(uint connectionId)
        {
            return _connections[connectionId];
        }

        /// <summary>
        /// Gaseste un loc liber in dictionar pentru stocarea conexiuni deschise cu baza de date
        /// </summary>
        /// <returns>Id-ul locului liber din dicitonar</returns>
        private uint GetNextConnectionLocationInConnections()
        {
            uint count = 0;
            while (_connections.ContainsKey(++count));
            return count;
        }
    }
}
