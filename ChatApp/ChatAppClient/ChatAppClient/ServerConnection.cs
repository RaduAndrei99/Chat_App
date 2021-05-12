using System;
using System.Net;
using System.Net.Sockets;

namespace ChatAppClient
{
    public class ServerConnection
    {

        private IPHostEntry _host;
        private IPAddress _ipAddress;
        private IPEndPoint _remoteEP;


        /// <summary>
        /// Socket-ul folosit pentru conexiunea cu server-ul.
        /// </summary>
        private Socket _sender;

        /// <summary>
        /// IP-ul server-ului.
        /// </summary>
        private const string ServerHost = "127.0.0.1";

        /// <summary>
        /// Port-ul server-ului
        /// </summary>
        private const int ServerPort = 5678;

        /// <summary>
        /// Instanta statica catre obiect; este folosita pentru implementarea sablonului Singleton.
        /// </summary>
        public static ServerConnection instance;


        /// <summary>
        /// Constructorul fara argumente al clasei ServerConnection.
        /// </summary>
        private ServerConnection()
        {
            Connect();
        }

        /// <summary>
        /// Metoda folosita pentru conectarea unui user la server.
        /// </summary>
        private void Connect()
        {
            try
            {
                _host = Dns.GetHostEntry(ServerHost);
                _ipAddress = _host.AddressList[0];
                _remoteEP = new IPEndPoint(_ipAddress, ServerPort);

                _sender = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


                _sender.Connect(_remoteEP);

                Console.WriteLine("M-am conectat la server: " + _sender.RemoteEndPoint.ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine("Can't connect to server.");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Metoda statica folosita pentru obtinerea instantei unice catre clase.
        /// </summary>
        /// <returns></returns>
        public static ServerConnection GetInstance()
        {
            if (instance == null)
                instance = new ServerConnection();

            return instance;
        }

        /// <summary>
        /// Getter pentru obiectul de tip Socket.
        /// </summary>
        /// <returns></returns>
        public Socket GetConnection()
        {
            return _sender;
        }

        /// <summary>
        /// Metoda folosita pentru a inchide conexiunea cu server-ul.
        /// </summary>
        public void CloseConnection()
        {
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();
        }
    }
}
