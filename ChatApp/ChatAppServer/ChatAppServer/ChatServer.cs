/**************************************************************************
 *                                                                        *
 *  Fisier:    ChatServer.cs                                              *
 *  Autor:     (c) Budeanu Radu=-Andrei                                   *
 *  E-mail:    budeanuradu99@gmail.com                                    *
 *  Descriere: Proxy-ul real pentru server-ul aplicatiei. Este folosit    *  
 *             pentru rula aplicatia de tip server ce se ocupa cu         *
 *             gestionarea utilizatorilor si a mesajelor.                 *
 **************************************************************************/

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MainServerNs;
using System.Collections.Generic;
using Model;
using ProtectionProxy;


namespace ChatAppServer
{

    /// <summary>
    /// Clasa folosita pentru a crea un obiect ce implementeaza notiunea de pereche de obiecte.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class Pair<T, U>
    {
        /// <summary>
        /// Constructorul cu argumente al clasei Pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public Pair(T first, U second)
        {
            this.Item1 = first;
            this.Item2 = second;
        }

        /// <summary>
        /// Getter/setter pentru primul obiect.
        /// </summary>
        public T Item1 { get; set; }

        /// <summary>
        /// Getter/setter pentru primul obiect.
        /// </summary>
        public U Item2 { get; set; }
    };


    /// <summary>
    /// Clasa ce implementeaza server-ul central care este intermediatorul dintre utilizatori.
    /// </summary>
    public class ChatServer : IPresenterServer
    {
        /// <summary>
        /// IP-ul pe care ruleaza server-ul
        /// </summary>
        // private string Host = "192.168.0.220";
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
        private Dictionary<string, Pair<Socket, int>> _loggedUserConnections;

        /// <summary>
        /// Parola pentru criptarea/decriptarea datelor.
        /// </summary>
        private const string EncryptionPassword = "1234qwertasdfg1234";

        /// <summary>
        /// Parametrii necesari pentru conectarea la baza de date(locala).
        /// </summary>
        private const string UserId = "radu";
        private const string Password = "1991129";
        private const string Hostname = "localhost";
        private const string DBPort = "1521";
        private const string Sid = "xe";
        private const bool Pooling = true;

        /// <summary>
        /// Constructorul fara argumente al clasei ChatServer
        /// </summary>
        public ChatServer()
        {
            _loggedUserConnections = new Dictionary<string, Pair<Socket, int>>();
            _model = new OracleDatabaseModel(UserId, Password, Hostname, DBPort, Sid, Pooling);
        }

        /// <summary>
        /// Aceasta metoda incearca sa conecteze un utilizator la server.
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
            catch(Model.Exceptions.DoNotExistsExceptions.UserDoNotExistsException e)
            {

                return false;
            }
        }

        /// <summary>
        /// Aceasta metoda realizeaza delogarea unui utilizator de la server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Logout(string username)
        {
            NotifyFriendsWithMessage(username, "offline");

            _loggedUserConnections[username].Item1.Send(PrepareMessageToSend("confirmLogout"));

        }

        /// <summary>
        /// Metoda folosita pentru a trimite un mesaj tuturor prietenilor unui utilizator.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        private void NotifyFriendsWithMessage(string username, string message)
        {
            foreach (string friend in _model.GetFriendList(username))
            {
                //daca prietenul este si el online, il notific
                if (_loggedUserConnections.ContainsKey(friend))
                    _loggedUserConnections[friend].Item1.Send(PrepareMessageToSend(message + " "  + username));
            }
        }

        private void NotifyUserAboutFriends(string username, string message)
        {
            foreach (string friend in _model.GetFriendList(username))
            {
                if (_loggedUserConnections.ContainsKey(friend))
                    _loggedUserConnections[username].Item1.Send(PrepareMessageToSend(message + " " + friend));
            }
        }



        /// <summary>
        /// Metoda folosita pentru a gestiona mesajele venite de la fiecare client.
        /// </summary>
        /// <param name="handlerObj"></param>
        void HandleClient(Object handlerObj)
        {
            Socket handler = (Socket)handlerObj;

            String userName = "";

            string data = null;
            byte[] bytes = null;

            bool loggedIn = false;

            Console.WriteLine("New client on: " + (IPEndPoint)handler.RemoteEndPoint);
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
                    Console.WriteLine("Client disconnected: " + (IPEndPoint)handler.RemoteEndPoint);

                    NotifyFriendsWithMessage(userName, "offline");


                    if (handler != null)
                    {
                        _loggedUserConnections.Remove(userName);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }

                    return;

                }

                Console.WriteLine("Encrypted: " + data);

                string[] stringSeparators = new string[] { "<EOF>" };

                foreach (string singleMsg in data.Split(stringSeparators, StringSplitOptions.None))
                {
                    if (singleMsg == "")
                        break;

                    string msg = Cryptography.Decrypt(singleMsg.Replace("<EOF>", ""), EncryptionPassword);
                    Console.WriteLine("Received: " + msg);

                    string messageType = msg.Split(' ')[0];

                    switch (messageType)
                    {

                        //login USERNAME HASHED_PASSWORD
                        case "login":
                            {
                                userName = msg.Split(' ')[1];
                                string password = msg.Split(' ')[2];

                                Console.WriteLine("**" + " " + userName + " " + password);
                                if (Login(userName, password))
                                {
                                    if(loggedIn == false)
                                    {
                                        if (!_loggedUserConnections.ContainsKey(userName))
                                            _loggedUserConnections.Add(userName, new Pair<Socket, int>(handler, -1));
                                        else
                                        {
                                            handler.Send(PrepareMessageToSend("ERROR ALREADY_LOGGED_IN"));

                                            break;
                                        }
                                        handler.Send(PrepareMessageToSend("confirmLogin"));

                                        NotifyFriendsWithMessage(userName, "online");

                                        loggedIn = true;
                                    }
                                    else
                                    {
                                        handler.Send(PrepareMessageToSend("ERROR ALREADY_LOGGED_IN"));
                                    }
                                }
                                else
                                {
                                    handler.Send(PrepareMessageToSend("ERROR WRONG_PASSWORD_OR_USERNAME"));
                                }

                                break;
                            }


                        //logout
                        case "logout":
                            {
                                Console.WriteLine("Client logged out - " + userName + ": " + (IPEndPoint)handler.RemoteEndPoint);
                                Logout(userName);
                                loggedIn = false;
                                break;
                            }
                        //sendMessage FROM TO MESSAGE
                        case "sendMessage":
                            {
                                string destinationUserName = msg.Split(' ')[2];
                                string from = msg.Split(' ')[1];

                                if(destinationUserName == "")
                                {
                                    break;
                                }

                                string destinationMessage = msg.Substring((messageType + " " + from + " " + destinationUserName + " ").Length);

                                //daca avem conectat deja user-ul destinatie, ii trimitem direct mesajul
                                if (_loggedUserConnections.ContainsKey(destinationUserName))
                                {
                                    SendMessage(userName, destinationUserName, destinationMessage);
                                    Console.WriteLine("User " + destinationUserName + " is connected.");
                                }
                                else
                                {
                                    Console.WriteLine("User " + destinationUserName + " is not connected.");
                                }

                                //incercam sa stocam mesajul. lipsa conversatiei va duce la o exceptie pe care o tratez
                                try
                                {
                                    _model.StoreMessage(userName, destinationUserName, "txt", Encoding.ASCII.GetBytes(destinationMessage), DateTime.UtcNow);
                                }
                                catch (Model.Exceptions.DoNotExistsExceptions.ConversationDoNotExistsException e)
                                {
                                    Console.WriteLine(e.Message);
                                    _model.CreateConversation(userName, destinationUserName);
                                    _model.StoreMessage(userName, destinationUserName, "txt", Encoding.ASCII.GetBytes(destinationMessage), DateTime.UtcNow);
                                }

                                break;
                            }


                        //register USERNAME HASHED_PASSWORD FIRST_NAME LAST_NAME EMAIL BIRTHDATE
                        case "register":
                            {
                                string username = msg.Split(' ')[1];
                                string pass = msg.Split(' ')[2];
                                string firstName = msg.Split(' ')[3];
                                string lastName = msg.Split(' ')[4];
                                string email = msg.Split(' ')[5];
                                string birthdate = msg.Split(' ')[6];

                                if (Register(username, pass, firstName, lastName, email, birthdate))
                                {
                                    handler.Send(PrepareMessageToSend("confirmRegister"));
                                }
                                else
                                {
                                    handler.Send(PrepareMessageToSend("ERROR ERROR_REGISTERING_THE_USER"));
                                }

                                break;
                            }

                        //addFriend FROM TO
                        case "addFriend":
                            {
                                string friend = msg.Split(' ')[2];

                                foreach (string friendAlready in _model.GetFriendList(userName))
                                {
                                    if (friendAlready == friend)
                                    {

                                        handler.Send(PrepareMessageToSend("ERROR ALREADY_FRIENDS"));

                                        break;
                                    }
              
                                }

                                try
                                {
                                    _model.RegisterFriendRequest(userName, friend);
                                }
                                catch (Model.Exceptions.AlreadyExistsException e)
                                {
                                    handler.Send(PrepareMessageToSend("ERROR FRIEND_REQUEST_ALREADY_SENT " + e.Message));
                                }
                                catch (Model.Exceptions.DoNotExistsExceptions.UserDoNotExistsException e)
                                {
                                    handler.Send(PrepareMessageToSend("ERROR USERNAME_DOESN'T_EXISTS " + e.Message));
                                }

                                catch (Model.Exceptions.WrongFormatExceptions.WrongUsernameFormatException e)
                                {
                                    handler.Send(PrepareMessageToSend("ERROR WRONG_USERNAME" + e.Message));
                                }

                                //daca prietenul este online, ii trimit direct cerereea
                                if (_loggedUserConnections.ContainsKey(friend))
                                {
                                    SendFriendRequest(userName, friend);
                                }
                                break;
                            }

                        //changeUsername FROM TO NICKNAME
                        case "changeUsername":
                            {
                                string friend = msg.Split(' ')[2];
                                string nickname = msg.Split(' ')[3];

                                ChangeFriendNickname(userName, friend, nickname);
                                break;
                            }

                        //getMessages NUMBER_OF_MESSAGES USERNAME1 USERNAME2
                        case "getMessages":
                            {
                                uint noOfMessages = uint.Parse(msg.Split(' ')[1]);
                                string username2 = msg.Split(' ')[3];

                                GetLastNMessages(userName, username2, noOfMessages);

                                break;
                            }

                        //getFriendRequest 
                        case "getFriendRequests":
                            {
                                GetFriendRequests(userName);
                                break;
                            }

                        //acceptFriendRequest from
                        case "acceptFriendRequest":
                            {
                                string from = msg.Split(' ')[1];

                                AcceptFriendRequest(from, userName);
                                try
                                {
                                    _model.AddRelationshipSettings(from, userName);

                                }
                                catch (Model.Exceptions.AlreadyExistsExceptions.FriendRelationshipAlreadyExistsException e)
                                {
                                    handler.Send(PrepareMessageToSend("ERROR ALREADY_FRIENDS " + e.Message));
                                }

                                break;
                            }

                        case "changeNickname":
                            {
                                string user = msg.Split(' ')[1];
                                string nickname = msg.Split(' ')[2];

                                ChangeFriendNickname(userName, user, nickname);
                                break;
                            }

                        case "closeConnection":
                            {
                                handler.Send(PrepareMessageToSend("confirmClose"));

                                CloseConnection(userName);

                                return;
                            }


                        case "getFriendsList":
                            {
                                GetFriendsList(userName);
                                NotifyUserAboutFriends(userName, "online");
                                break;
                            }


                        case "resetMessageID":
                            {
                                ResetMessageID(userName);
                                break;
                            }


                    }
                }
            }
        }


        /// <summary>
        /// Metoda care ruleaza server-ul, asteptand conexiuni de la client.
        /// </summary>
        public void run()
        {

            IPAddress ipAddress = System.Net.Dns.GetHostAddresses(Host)[0];

            Console.WriteLine(ipAddress);

            Socket handler = null;
            try
            {
                Socket listener = new Socket( AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

                listener.Bind(new IPEndPoint(ipAddress, Port));

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
        public void SendMessage(string from, string destination, string message)
        {
            try
            {
                _loggedUserConnections[destination].Item1.Send(PrepareMessageToSend("message" + " " + from + " " + DateTime.UtcNow.ToString().Replace(" ", "_") + " " + message ));
            }
            catch(Exception e)
            {
                _loggedUserConnections[from].Item1.Send(PrepareMessageToSend("ERROR" + " " + "FAILED_TO_SEND_MESSAGE " + e.Message));
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
                _model.RegisterUser(username, firstName, lastName, email, DateTime.Parse(birthdate));

                return true;
            }
            catch(Model.Exceptions.AlreadyExistsExceptions.UserRegistrationAlreadyExistsException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (Model.Exceptions.AlreadyExistsExceptions.UserAlreadyExistsException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (Exception e)
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
                _loggedUserConnections[friend].Item1.Send(PrepareMessageToSend("friendRequest " + asker));             
            }
            catch(Exception e)
            {
                _loggedUserConnections[asker].Item1.Send(PrepareMessageToSend("ERROR " + "FAILED_TO_SEND_FRIEND_REQUEST " + e.Message));
            }
        }

        /// <summary>
        /// Metoda care schimba nick-name-ul pentru un utilizator intr-o conversatie.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] PrepareMessageToSend(string message)
        {
            return Encoding.ASCII.GetBytes(Cryptography.Encrypt(message, EncryptionPassword) + "<EOF>");
        }

        /// <summary>
        /// Metoda folosita pentru a schimba nickname-ul unui utilizator.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="who"></param>
        /// <param name="nickname"></param>
        public void ChangeFriendNickname(string from, string who, string nickname)
        {
            try
            {
                _model.ChangeNickname(from, who, nickname);
            }
            catch(Exception E)
            {
                _loggedUserConnections[from].Item1.Send(PrepareMessageToSend("ERROR UNABLE_TO_CHANGE_THE_NICKNAME " + E.Message));
            }
        }

        /// <summary>
        /// Metoda folosita pentru a obtine ultimele n mesaje dintr-o conversatie din baza de date.
        /// </summary>
        /// <param name="username1"></param>
        /// <param name="username2"></param>
        /// <param name="howManyMessages"></param>
        public void GetLastNMessages(string username1, string username2 , uint howManyMessages)
        {
            try
            {
                List<Model.DataTransferObjects.MessageDTO> messages;

                long msgId;
                _model.GetLastNMessagesFromConversation(username1, username2, _loggedUserConnections[username1].Item2, (uint)howManyMessages , out messages, out msgId);
                Console.WriteLine("ID:" + msgId.ToString());

                if (msgId == -1)
                    return;

                _loggedUserConnections[username1].Item2 = (int)msgId;
                for (int i = 0; i < messages.Count; ++i)
                {
                    Console.WriteLine("MESSAGE: " + Encoding.ASCII.GetString(messages[i].MessageData, 0, messages[i].MessageData.Length) );
                    _loggedUserConnections[username1].Item1.Send(PrepareMessageToSend("storedMessage" + " " + messages[i].SenderUsername + " " + messages[i].SentAt.ToString().Replace(" ", "_") + " " + 
                        Encoding.ASCII.GetString(messages[i].MessageData, 0, messages[i].MessageData.Length)));
                    Thread.Sleep(10);
                }


            }
            catch (Model.Exceptions.DoNotExistsExceptions.ConversationDoNotExistsException e)
            {
                return; 
            }

        }

        /// <summary>
        /// Metoda ce accepta o cerere de prietenie primita de catre un utilizator.
        /// </summary>
        /// <param name="asker"></param>
        /// <param name="friend"></param>
        public void AcceptFriendRequest(string asker, string friend)
        {
            try
            {
                _model.AcceptFriendRequest(asker, friend);

                if (_loggedUserConnections.ContainsKey(asker))
                {
                    _loggedUserConnections[asker].Item1.Send(PrepareMessageToSend("friend" + " " + friend));

                    _loggedUserConnections[asker].Item1.Send(PrepareMessageToSend("online" + " " + friend));

                }

                if (_loggedUserConnections.ContainsKey(friend))
                {
                    _loggedUserConnections[friend].Item1.Send(PrepareMessageToSend("friend" + " " + asker));

                    _loggedUserConnections[friend].Item1.Send(PrepareMessageToSend("online" + " " + asker));
                }
            }
            catch (Exception e)
            {
                _loggedUserConnections[friend].Item1.Send(PrepareMessageToSend("ERROR UNABLE_TO_ACCEPT_THE_FRIEND_REQUEST " + e.Message));
            }
        }


        /// <summary>
        /// Metoda ce trimite lista de cereri de prietenie unui utilizator.
        /// </summary>
        /// <param name="username"></param>
        public void GetFriendRequests(string username)
        {
            try
            {
                List<string> requests = _model.GetReceivedPendingRequest(username);

                foreach (string from in requests)
                {
                    //inversate
                    SendFriendRequest(from, username);
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                _loggedUserConnections[username].Item1.Send(PrepareMessageToSend("ERROR UNABLE_TO_GET_FRIEND_REQUESTS " + e.Message));
            }
        }

        /// <summary>
        /// Metoda ce trimite lista de prieteni unui utilizator.
        /// </summary>
        /// <param name="username"></param>
        public void GetFriendsList(string username)
        {
            try
            {
                List<string> friends = _model.GetFriendList(username);

                foreach (string friend in friends)
                {
                    _loggedUserConnections[username].Item1.Send(PrepareMessageToSend("friend" + " " + friend));
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                _loggedUserConnections[username].Item1.Send(PrepareMessageToSend("ERROR UNABLE_TO_GET_FRIEND_LIST " + e.Message));
            }
        }


        /// <summary>
        /// Metoda ce inchide conexiunea cu un utilizator.
        /// </summary>
        /// <param name="username"></param>
        public void CloseConnection(string username)
        {

            if (_loggedUserConnections.ContainsKey(username))
            {
                _loggedUserConnections[username].Item1.Shutdown(SocketShutdown.Both);
                _loggedUserConnections[username].Item1.Close();

                _loggedUserConnections.Remove(username);

            }
        }

        public void ResetMessageID(string username)
        {
            try
            {
                _loggedUserConnections[username].Item2 = -1;
            }
            catch (Exception e)
            {
                _loggedUserConnections[username].Item1.Send(PrepareMessageToSend("ERROR UNABLE_TO_RESET " + e.Message));
            }
        }
    }
}
