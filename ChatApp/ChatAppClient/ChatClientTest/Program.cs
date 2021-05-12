using ChatAppClient;
using System;

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

            Console.Write("Send to: ");
            string sendto = Console.ReadLine();

            c.SendMessage(sendto, "HELLO_MA");
            // c.Logout("", "");
            while (true) ;
            c.CloseServerConnection();

        }
    }
}
