using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IModel
    {
        void AddNewUser(string username, string password);
        void RegisterUser(string username, string firstname, string lastname, string email, DateTime birthdate);
        void AddApplicationSettings(string username);
        void CreateConversation(string username1, string username2);
        void RegisterFriendRequest(string fromUsername, string toUsername);
        void AddRelationshipSettings(string username1, string username2);
        void ChangeNickname(string fromUsername, string toUsername, string nickname);
        void StoreMessage(string senderUsername, string receiverUsername, string format, byte[] message_data, DateTime sentDate);
        bool CheckUserCredentials(string username, string password);
    }
}
