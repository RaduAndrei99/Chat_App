using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainServerNs;
using ChatAppClient.Messages;

namespace Chat_App.Views
{
    public interface IView
    {
        void Login();
        void Logout();
        void AddMessageToChat(Message message, bool addToEnd);
        void AddFriendList(string friend);
        void AddFriendRequest(string username);
        void ShowErrorMessage(string message);
        void SetPresenter(IPresenterServer presenter);
        void ChangeFriendStatus(string username, bool status);
        void FriendHasSeen(string username);

        void AcceptRegister();
    }
}
