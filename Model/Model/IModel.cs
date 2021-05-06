using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IModel
    {
        bool CheckIfUserExists(string username);

        void AddNewUser(string username, string password);

        void RegisterUser(string username, string firstname, string lastname, string email, string birthdate);

        void RegisterFriendRequest(string username1, string username2);

        void ChangeNickname(string fromUsername, string toUsername);

        void CreateConversation(string username1, string username2);

        void StoreMessage(string senderUsername, string receiverUsername, string format, byte[] message_data, string sentDate);
    }
}
