
namespace MainServerNs
{
    public interface MainServer
    {
        bool Login(string username, string password);
        void Logout(string username, string password);
        void SendMessage(string from, string destination, string message);
        bool Register(string username, string password, string firstName, string lastName, string email, string birthdate);
        void SendFriendRequest(string asker, string friend);
        void AcceptFriendRequest(string asker, string friend);
        byte[] PrepareMessageToSend(string message);
        void ChangeFriendNickname(string from, string who, string nickname);
        void GetLastNMessages(string username1, string username2, uint howManyMessages);
        void GetFriendRequests(string username);
        void GetFriendsList(string username);

    }
}
