/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: IModel.cs                                                       *
 *                                                                          *
 *  Descriere: Interfata pentru un model generic                            *
 *                                                                          *
 ****************************************************************************/

using Model.Commons;
using Model.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Interfata modelului din modelul de proiectare MODEL - VIEW - PRESENTER
    /// </summary>
    public interface IModel
    {
        void AddNewUser(string username, string password);
        void ChangeUsername(string currentUsername, string newUsername);
        void ChangeUserPassword(string username, string newPassword);
        void DeleteUser(string username);


        void RegisterUser(string username, string firstname, string lastname, string email, DateTime birthdate);


        void AddApplicationSettings(string username);
        void SetDateFormat(string username, DateFormat dateFormat);
        void SetTimeFormat(string username, TimeFormat timeFormat);


        void CreateConversation(string username1, string username2);
        void DeleteConversation(string username1, string username2);


        void RegisterFriendRequest(string fromUsername, string toUsername);
        void AcceptFriendRequest(string username1, string username2);
        void DeleteFriendRelationship(string username1, string username2);


        void AddRelationshipSettings(string username1, string username2);
        void ChangeNickname(string fromUsername, string toUsername, string nickname);


        void StoreMessage(string senderUsername, string receiverUsername, string format, byte[] message_data, DateTime sentDate);


        bool CheckUserCredentials(string username, string password);
        void GetLastNMessagesFromConversation(string username1, string username2, long bellowThisMessageId, uint howManyMessages, out List<MessageDTO> messages, out long lastMessageId);
        DateFormat GetDateFormat(string username);
        TimeFormat GetTimeFormat(string username);
        string GetNicknameFromFriendRelationship(string fromUsername, string friendUsername);
        List<string> GetFriendList(string username);
        List<string> GetSentPendingRequests(string username);
        List<string> GetReceivedPendingRequest(string username);
    }
}
