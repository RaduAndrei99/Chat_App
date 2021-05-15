using Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Commons.Constraints;

namespace Model
{
    public interface IModel
    {
        void AddNewUser(string username, string password);
        void ChangeActiveStatus(string username, bool isActive);
        void DeleteUser(string username);

        void RegisterUser(string username, string firstname, string lastname, string email, DateTime birthdate);


        void AddApplicationSettings(string username);


        void CreateConversation(string username1, string username2);
        void DeleteConversation(string username1, string username2);


        void RegisterFriendRequest(string fromUsername, string toUsername);
        void AcceptFriendRequest(string username1, string username2);
        void DeleteFriendRelationship(string username1, string username2);


        void AddRelationshipSettings(string username1, string username2);
        void ChangeNickname(string fromUsername, string toUsername, string nickname);


        void StoreMessage(string senderUsername, string receiverUsername, string format, byte[] message_data, DateTime sentDate);


        bool CheckUserCredentials(string username, string password);
        void GetLastNMessagesFromConversation(string username1, string username2, long fromMessageId, uint howManyMessages, out List<MessageDTO> messages, out long lastMessageId);
    }
}
