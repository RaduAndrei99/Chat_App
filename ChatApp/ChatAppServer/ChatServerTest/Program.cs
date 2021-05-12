using ChatAppServer;

namespace ChatServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer s = new ChatServer();

            s.run();
        }
    }
}
