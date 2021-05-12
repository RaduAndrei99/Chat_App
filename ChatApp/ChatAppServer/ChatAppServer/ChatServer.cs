using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MainServerNs;
using System.Collections.Generic;
using Model;

namespace ChatAppServer
{
    public class ChatServer : MainServer
    {
        /// <summary>
        /// IP-ul pe care ruleaza server-ul
        /// </summary>
        private string Host = "127.0.0.1";

        /// <summary>
        /// Port-ul pe care ruleaza server-ul
        /// </summary>
        private int Port = 5678;

        /// <summary>
        /// Referinta catre obiectul ce se ocupa cu persistenta datelor.
        /// </summary>
        private IModel _model;

        /// <summary>
        /// Un dictionar folosit pentru gestiunea clientilor conectati la un moment dat.
        /// </summary>
        private Dictionary<string, Socket> _userConnections;

        /// <summary>
        /// Constructorul fara argumente al clasei ChatServer
        /// </summary>
        public ChatServer()
        {
            _userConnections = new Dictionary<string, Socket>();
            _model = new Model.OracleDatabaseModel();
        }

        /// <summary>
        /// Aceasta metoda incearca sa conecteze un usesr la server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            try
            {
                return _model.CheckUserCredentials(username, password);
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Aceasta metoda il deconecteaza pe user de la server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Logout(string username, string password)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Metoda folosita pentru a gestiona mesajele de la fiecare client.
        /// </summary>
        /// <param name="handlerObj"></param>
        void HandleClient(Object handlerObj)
        {
            Socket handler = (Socket)handlerObj;

            String userName = "";

            string data = null;
            byte[] bytes = null;

            while (true)
            {
                data = "";
                try
                {
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec;

                        bytesRec = handler.Receive(bytes);
                        if(bytesRec == 0)
                        {

                            return;
                        }

                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("S-a deconectat un client.");

                    if (handler != null)
                    {
                        _userConnections.Remove(userName);
                        return;
                    }

                    break;

                }


                Console.WriteLine("Am primit: " + data);

                string message = data.Split(' ')[0];

                switch (message)
                {
                    case "connect":
                        {
                            Console.WriteLine("S-a conectat un user.");
                            string m = "Confirm <EOF>";
                            Thread.Sleep(500);
                            handler.Send(Encoding.ASCII.GetBytes(m));

                            break;
                        }

                    case "login":
                        {
                            userName = data.Split(' ')[1];
                            string password = data.Split(' ')[2];

                            Console.WriteLine("**" + " " + userName + " " + password);
                            if (Login(userName, password))
                            {
                                _userConnections.Add(userName, handler);
                                handler.Send(Encoding.ASCII.GetBytes("CONFIRM <EOF>"));
                            }
                            else
                            {
                                handler.Send(Encoding.ASCII.GetBytes("ERROR <EOF>"));
                            }

                            break;
                        }

                    case "logout":
                        {
                            _userConnections.Remove(userName);

                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();

                            return;
                        }

                    case "sendMessage":
                        {
                            string destinationUserName = data.Split(' ')[1];
                            string destinationMessage = data.Split(' ')[2];

                            //daca avem conectat deja user-ul destinatie, ii trimitem direct mesajul
                            if (_userConnections.ContainsKey(destinationUserName))
                            {
                                SendMessage(destinationUserName, destinationMessage);
                                Console.WriteLine("User-ul " + destinationUserName + " este conectat.");
                            }
                            else
                            {
                                Console.WriteLine("User-ul " + destinationUserName + " nu este conectat.");
                            }

                            //incercam sa cream conversatia, iar daca deja exista va arunca o exceptie
                            try
                            {
                                _model.CreateConversation(userName, destinationUserName);
                            }
                            catch (Exception e)
                            {

                            }

                            //adaugam mesajul in baza de date
                            _model.StoreMessage(userName, destinationUserName, "txt", Encoding.ASCII.GetBytes(destinationMessage), DateTime.UtcNow);

                            break;
                        }

                    case "register":
                        {
                            string username = data.Split(' ')[1];
                            string pass = data.Split(' ')[2];
                            string firstName = data.Split(' ')[3];
                            string lastName = data.Split(' ')[4];
                            string email = data.Split(' ')[5];
                            string birthdate = data.Split(' ')[6];

                            if (Register(username, pass, firstName, lastName, email, birthdate))
                            {
                                handler.Send(Encoding.ASCII.GetBytes("CONFIRM <EOF>"));
                            }
                            else
                            {
                                handler.Send(Encoding.ASCII.GetBytes("ERROR <EOF>"));
                            }

                            break;
                        }

                    case "addFriend":
                        {

                            string friend = data.Split(' ')[2];

                            SendFriendRequest(userName, friend);

                            break;
                        }

                    case "changeUsername":
                        {
                            string friend = data.Split(' ')[2];
                            string nickname = data.Split(' ')[3];

                            _model.ChangeNickname(userName, friend, nickname);
                            break;
                        }

                }
            }

            Console.WriteLine("S-a inchis un socket");
        }


        /// <summary>
        /// Metoda care ruleaza server-ul, asteptand conexiuni de la client.
        /// </summary>
        public void run()
        {

            IPHostEntry host = Dns.GetHostEntry(Host);
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

            Socket handler = null;
            try
            {
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);

                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                while (true)
                {
              
                    handler = listener.Accept();

                    Thread thread = new Thread(HandleClient);

                    thread.Start(handler);
                }


            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Aceasta metoda este folosita pentru a trimite un mesaj de la un user la altul.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="message"></param>
        public void SendMessage(string destination, string message)
        {
            try
            {
                _userConnections[destination].Send(Encoding.ASCII.GetBytes("message" + " " + message + "<EOF>"));
            }
            catch(Exception e)
            {

            }
        }

        /// <summary>
        /// Aceasta metoda foloseste model-ul pentru a inregistra un nou utilizator.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="birthdate"></param>
        public bool Register(string username, string password, string firstName, string lastName, string email, string birthdate)
        {
            try
            {
                //TODO - de convertit data din string in DateTime
                _model.AddNewUser(username, password);
                _model.RegisterUser(username, firstName, lastName,  email, DateTime.UtcNow);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// Metoda folosita pentru a trimite o cerere de prietenie unui user si pentru a salva cererea in baza de date.
        /// </summary>
        /// <param name="asker"></param>
        /// <param name="friend"></param>
        public void SendFriendRequest(string asker, string friend)
        {
            try
            {
                _model.RegisterFriendRequest(asker, friend);

                if (_userConnections.ContainsKey(asker))
                {
                    _userConnections[asker].Send(Encoding.ASCII.GetBytes("friendRequest " + asker + " " + friend));
                }
            }
            catch(Exception e)
            {

            }
        }
 
    }
}
