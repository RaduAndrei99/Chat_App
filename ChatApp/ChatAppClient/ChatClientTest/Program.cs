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

            ProxyServer c = new ProxyServer(null);

            c.Login(username, password);

            c.Run();



            while (true)
            {
                
                Console.Write("Send to: ");
                string sendto = Console.ReadLine();

                c.SendFriendRequest(username, sendto);


            }
            // c.Logout("", "");

        }
    }
}
