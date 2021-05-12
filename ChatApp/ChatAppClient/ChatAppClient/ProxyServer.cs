using System;
using System.Net.Sockets;
using System.Text;
using MainServerNs;


namespace ChatAppClient
{
    public class ProxyServer : MainServer
    {
        private ServerConnection _serverConnection; 

        public ProxyServer()
        {
            _serverConnection = ServerConnection.GetInstance();
        }
        public bool Login(string username, string password)
        {
            try
            {
                Socket sender = _serverConnection.GetConnection();

                byte[] msg = Encoding.ASCII.GetBytes("connect <EOF>");

                int bytesSent = sender.Send(msg);

                string receivedData = "";
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

                Console.WriteLine("Received: " + receivedData);

                sender.Send(Encoding.ASCII.GetBytes("login " + username + " " +  password  + " <EOF>"));

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

                Console.WriteLine("Received: " + receivedData);


                return true;
            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Server-ul nu raspunde.");


                return false;
            }
        }

        public void Logout(string username, string password)
        {
            try
            {
                Socket sender = _serverConnection.GetConnection();

                sender.Send(Encoding.ASCII.GetBytes("logout <EOF>"));



            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Server-ul nu raspunde.");

            }
        }

        public void SendMessage(string destination, string message)
        {
            try
            {
                Socket sender = _serverConnection.GetConnection();


                sender.Send(Encoding.ASCII.GetBytes("sendMessage" + " " + destination + " " + message + "<EOF>"));


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

        public bool Register(string username, string password, string firstName, string lastName, string email, string birthdate)
        {
            try
            {
                Socket sender = _serverConnection.GetConnection();

                sender.Send(Encoding.ASCII.GetBytes("register " + username  + " " + password + " "  + firstName + " " + lastName + " " + email + " " + birthdate +  " <EOF>"));

            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Server-ul nu raspunde.");

            }
        }
    }
}
