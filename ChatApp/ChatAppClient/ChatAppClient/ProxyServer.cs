using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MainServerNs;
using ProtectionProxy;
using Chat_App.Views;

namespace ChatAppClient
{
    /// <summary>
    /// Clasa ce reperezinta obiectul Proxy din sablonul cu acelasi nume.
    /// </summary>
    public class ProxyServer : IPresenterServer
    {
        /// <summary>
        /// Referinta catre obiectul care se ocupa de conexiunea cu server-ul
        /// </summary>
        private ServerConnection _serverConnection;

        /// <summary>
        /// parola de pentru criptarea/decriptarea datelor
        /// </summary>
        private const string EncryptionPassword = "1234qwertasdfg1234";

        private bool _isRunning;

   
        private IView _view;

        /// <summary>
        /// Constructorul cu argumente al clasei ProxyServer.
        /// </summary>
        /// <param name="view"></param>
        public ProxyServer(IView view)
        {
            _serverConnection = ServerConnection.GetInstance();
            _view = view;
        }


        /// <summary>
        /// Metoda folosita pentru a realiza operatia de logare al sserver.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void Login(string username, string password)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("login " + username + " " +  password ));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Server-ul nu raspunde.");

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }

        /// <summary>
        /// Metoda folosita pentru a realiza operatia de delogare de la server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Logout(string username)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("logout"));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Server-ul nu raspunde.");

            }
        }

        /// <summary>
        /// Metoda folosita pentru trimiterea unui mesaj catre alt utilizator.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="destination"></param>
        /// <param name="message"></param>
        public void SendMessage(string from, string destination, string message)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();


                sender.Send(PrepareMessageToSend("sendMessage" + " " + from + " " + destination + " " + message));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Server-ul nu raspunde.");

            }
        }

        /// <summary>
        /// Metoda folosita pentru a inchide conexiunea cu server-ul.
        /// </summary>
        public void CloseServerConnection()
        {
            _serverConnection.CloseConnection();
        }

      
        /// <summary>
        /// Metoda folosita pentru a trimite o cerere de prietenie catre un alt utilizator.
        /// </summary>
        /// <param name="asker"></param>
        /// <param name="friend"></param>
        public void SendFriendRequest(string asker, string friend)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("addFriend " + asker + " " + friend ));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding.");

            }
        }

        /// <summary>
        /// Metoda folosita pentru inregistrarea unui nou utilizator.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="birthdate"></param>
        /// <returns>True daca operatiunea s-a realizat cu succes, false in caz contrar.</returns>
        public void Register(string username, string password, string firstName, string lastName, string email, string birthdate)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("register " + username + " " + password + " " + firstName + " " + lastName + " " + email + " " + birthdate ));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding");
            }
        }

        /// <summary>
        /// Metoda folosita pentru receptiona mesajele de la server.
        /// Ruleaza pe un thread separat atunci cand se apeleaza metoda Run().
        /// </summary>
        private void ReceiveMessages()
        {
            Socket sender = _serverConnection.GetSenderConnection();

            string receivedData;

            Console.WriteLine("I'm waiting for messages from the server...");

            while (_isRunning)
            {
                receivedData = "";
                while (true)
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = sender.Receive(bytes);
                    receivedData += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (receivedData.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                string msg = Cryptography.Decrypt(receivedData.Replace("<EOF>", ""), EncryptionPassword);
                Console.WriteLine("Received(run): " + msg);

                string msgType = msg.Split(' ')[0];
                switch (msgType)
                {
                    case "message":
                    {
                        string from = msg.Split(' ')[1];
                        string message = msg.Substring((msgType + " " + from + " ").Length);

                        Console.WriteLine(from + ": " + message);
                        break;
                    }

                    case "storedMessage":
                    {
                        string friend = msg.Split(' ')[1];
                        string message = msg.Substring((msgType + " " + friend + " ").Length);

                        Console.WriteLine(friend + ":" + message);
                        break;
                    }

                    case "friendRequest":
                    {
                        string friend = msg.Split(' ')[1];
                        Console.WriteLine("Friend request from: " + friend);

                        break;
                    }

                    case "friend":
                    {
                        string friend = msg.Split(' ')[1];
                        Console.WriteLine("Friend: " + friend);

                        break;
                    }

                    case "online":
                    {
                        string friend = msg.Split(' ')[1];
                        Console.WriteLine("friend " + friend + " is online.");

                        break;
                    }

                    case "confirmLogin":
                    {
                        string error = msg.Split(' ')[1];

                        _view.Login();

                        break;
                    }

                    case "ERROR":
                    {
                        string error = msg.Split(' ')[1];

                        Console.WriteLine("ERROR: " + error);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Metoda ce porneste pe un nou thread functia care asculta mesajele venite de la server.
        /// </summary>
        public void Run()
        {
            Thread backgroundThread = new Thread(ReceiveMessages);
            // Start thread  
            backgroundThread.Start();
        }

        /// <summary>
        /// Metoda folosita pentru a pregati mesajele inainte de trimiterea catre server.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] PrepareMessageToSend(string message)
        {
            return Encoding.ASCII.GetBytes(Cryptography.Encrypt(message, EncryptionPassword) + "<EOF>");
        }

        /// <summary>
        /// Metoda folosita pentru schimbarea nickname-ului unui utlizator din lista de prieteni.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="who"></param>
        /// <param name="nickname"></param>
        public void ChangeFriendNickname(string from, string who, string nickname)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("changeUsername " + from + " " + who + " " + nickname));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding");
            }
        }

        /// <summary>
        /// Metoda folosita pentru a obtine ultimele n mesaje dintr-o conversatie de la server.
        /// </summary>
        /// <param name="username1"></param>
        /// <param name="username2"></param>
        /// <param name="howManyMessages"></param>
        public void GetLastNMessages(string username1, string username2, uint howManyMessages)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("getMessages " + howManyMessages.ToString() + " " + username1 + " " + username2));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding");
            }
        }

        /// <summary>
        /// Metoda folosita pentru a accepta o cerere de prietenie.
        /// </summary>
        /// <param name="asker"></param>
        /// <param name="friend"></param>
        public void AcceptFriendRequest(string asker, string friend)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("acceptFriendRequest " + asker + " " + friend));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding");
            }
        }

        /// <summary>
        /// Metoda folosita pentru a cere de la server toate cererile de prietenie pentru un user.
        /// </summary>
        /// <param name="username"></param>
        public void GetFriendRequests(string username)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("getFriendRequests " + username));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding");
            }
        }

        /// <summary>
        /// Metoda folosita pentru a obtine lista de prieteni de la server pentru un utilizator.
        /// </summary>
        /// <param name="username"></param>
        public void GetFriendsList(string username)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("getFriendsList " + username));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding");
            }
        }

        public void StopRunning()
        {
            _isRunning = false;
        }
    }
}
