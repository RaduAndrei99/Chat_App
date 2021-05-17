using ChatAppClient;
using System;
using System.Threading;

namespace ChatClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter username: " );
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            ProxyServer c = new ProxyServer();

            c.Login(username, password);

            c.Run();


            while (true)
            {
                
                Console.Write("Send to: ");
                string sendto = Console.ReadLine();

                Console.Write("Message: ");
                string msg = Console.ReadLine();
                

                c.SendMessage(username, sendto, msg);

                c.GetLastNMessages(username, username, 10);
                Thread.Sleep(100);
                c.SendFriendRequest(username, sendto);


            }
            // c.Logout("", "");

            c.CloseServerConnection();

        }
    }
}
