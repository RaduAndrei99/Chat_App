namespace MainServerNs
{
    public interface MainServer
    {
        bool Login(string username, string password);
        void Logout(string username, string password);
        void SendMessage(string destination, string message);
        bool Register(string username, string password, string firstName, string lastName, string email, string birthdate);
        void SendFriendRequest(string asker, string friend);
    }
}
