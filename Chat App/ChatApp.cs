using Chat_App.Views;
using ChatAppClient;
using MainServerNs;
using System;
using ChatAppClient.Messages;
using System.Windows.Forms;
using System.Drawing;

namespace Chat_App
{
    /// <summary>
    /// Clasă ce conține cele două view-uri principale programului. Singurul rol este cel de a încapsula view-urile pentru
    /// ușurința presenter-ului în a gestiona partea de view.
    /// </summary>
    public partial class ChatApp : BasicView, IView
    {
        /// <summary>
        /// Referința statică a clasei.
        /// </summary>
        private static ChatApp _instance;
        
        /// <summary>
        /// Referință către primul form principal, LogInView, cel care apare și la startup.
        /// </summary>
        private LogInView _loginForm;
        
        /// <summary>
        /// Referință către al doilea form principal, ChatView.
        /// </summary>
        private ChatView _chatForm;
        
        /// <summary>
        /// Presenter-ul care se ocupă de schimbările active ale datelor în view.
        /// </summary>
        private IPresenterServer _presenter;

        /// <summary>
        /// Implementarea de singleton. Există o singură instanțiere a clasei în tot programul.
        /// </summary>
        public static ChatApp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatApp();
                return _instance;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesarea presenterului de către restul view-urilor.
        /// </summary>
        public IPresenterServer Presenter
        {
            get
            {
                return _presenter;
            }
        }

        /// <summary>
        /// Constructor privat pentru o singură instanțiere în program.
        /// </summary>
        private ChatApp()
        {
            InitializeComponent();
            _loginForm = LogInView.Instance;
            _chatForm = ChatView.Instance;
            _loginForm.Show();
        }

        public void SetPresenter(IPresenterServer presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Ascunde form-ul la startup.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Hide();
        }

        public void AddMessageToChat(ChatAppClient.Messages.Message message)
        {
            ListViewItem newMessage = new ListViewItem();
            newMessage.Text = message.Msg;
            newMessage.ForeColor = Color.DarkOrange;

            _chatForm.Chat.Items.Add(newMessage);
        }

        public void FriendHasSeen(string username)
        {
            throw new NotImplementedException();
        }

        public void AddFriendList(string friend)
        {
            ListViewItem newFriend = new ListViewItem();
            newFriend.Text = friend;
            newFriend.ForeColor = Color.OrangeRed;
            _chatForm.FriendList.Items.Add(newFriend);
        }

        public void ChangeFriendStatus(string username, bool status)
        {
            foreach (ListViewItem friend in _chatForm.Chat.SelectedItems)
            {
                if (friend.Text.Contains(username))
                {
                    if (status)
                    {
                        // set online
                        friend.Text.Replace("[Offline]", "[Online]");
                    }
                    else
                    {
                        // set offline
                        friend.Text.Replace("[Online]", "[Offline]");
                    }
                }
            }
        }

        public void Login()
        {
            _loginForm.Hide();
            _presenter.GetFriendsList(_loginForm.Control.Username);
            _chatForm.Show();
        }

        public void Logout()
        {
            _chatForm.Hide();
            _loginForm.Show();
        }

        public void AddFriendRequest(string username)
        {
            ListViewItem newFriend = new ListViewItem();
            newFriend.Text = username;
            newFriend.ForeColor = Color.Orange;
            FriendRequestsView.Instance.FriendRequests.Items.Add(newFriend);
        }

        public void ShowErrorMessage(string message)
        {
            // this might need to be changed in the future
            _loginForm.Control.ErrorLabel.Text = message;
            _chatForm.ErrorLabel.Text = message;
        }
    }
}
