using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MainServerNs;
using ProtectionProxy;

namespace ChatAppClient
{
    public class ProxyServer : MainServer
    {
        private ServerConnection _serverConnection;

        private const string EncryptionPassword = "1234qwertasdfg1234";

        public ProxyServer()
        {
            _serverConnection = ServerConnection.GetInstance();
        }
        public bool Login(string username, string password)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                string receivedData = "";

                sender.Send(PrepareMessageToSend("login " + username + " " +  password ));

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
                Console.WriteLine("Received: " + msg);


                return true;
            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Server-ul nu raspunde.");


                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public void Logout(string username, string password)
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

        public void CloseServerConnection()
        {
            _serverConnection.CloseConnection();
        }

      

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

        public bool Register(string username, string password, string firstName, string lastName, string email, string birthdate)
        {
            try
            {
                Socket sender = _serverConnection.GetSenderConnection();

                sender.Send(PrepareMessageToSend("register " + username + " " + password + " " + firstName + " " + lastName + " " + email + " " + birthdate ));

                return true;
            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The server is not responding");

                return false;
            }
        }

        private void ReceiveMessages()
        {
            Socket sender = _serverConnection.GetSenderConnection();

            string receivedData;

            sender.Send(PrepareMessageToSend("openSocket"));

            Console.WriteLine("I'm waiting for messages from the server...");

            while (true)
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
                }
            }
        }

        public void Run()
        {
            Thread backgroundThread = new Thread(ReceiveMessages);
            // Start thread  
            backgroundThread.Start();
        }

        public byte[] PrepareMessageToSend(string message)
        {
            return Encoding.ASCII.GetBytes(Cryptography.Encrypt(message, EncryptionPassword) + "<EOF>");
        }

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
    }
}
