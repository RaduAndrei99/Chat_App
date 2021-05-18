using System;
using System.Net;
using System.Net.Sockets;

namespace ChatAppClient
{

    /// <summary>
    /// Clasa implementata ca un SIngleton, folosita pentru a gestiona conexiunea catre server.
    /// </summary>
    public class ServerConnection
    {
        private IPEndPoint _remoteEP;

        /// <summary>
        /// Socket-ul folosit pentru conexiunea cu server-ul atunci cand se trimit mesaje.
        /// </summary>
        private Socket _sender;

        /// <summary>
        /// Socket-ul folosit pentru conexiunea cu server-ul atunci cand se primesc mesaje dinspre server.
        /// </summary>
        private Socket _receiver;

        /// <summary>
        /// IP-ul server-ului.
        /// </summary>
        private const string ServerHost = "192.168.0.220";

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
                IPAddress ipa = IPAddress.Parse(ServerHost);
                _remoteEP = new IPEndPoint(ipa, ServerPort);

                _sender = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
                //_receiver = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                _sender.Connect(_remoteEP);
                //_receiver.Connect(_remoteEP);

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
        /// Getter pentru obiectul de tip Socket pentru trimiterea de mesaje.
        /// </summary>
        /// <returns></returns>
        public Socket GetSenderConnection()
        {
            return _sender;
        }

        /// <summary>
        /// Getter pentru obiectul de tip Socket pentru primirea de mesaje.
        /// </summary>
        /// <returns></returns>
        public Socket GetReceiverConnection()
        {
            return _receiver;
        }

        /// <summary>
        /// Metoda folosita pentru a inchide conexiunea cu server-ul.
        /// </summary>
        public void CloseConnection()
        {
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();

            _receiver.Shutdown(SocketShutdown.Both);
            _receiver.Close();
        }
    }
}
