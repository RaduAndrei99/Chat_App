/***************************************************************************
 *                                                                         *
 *  Autor:  Gafencu Gabriel                                                *
 *  Grupa:  1309A                                                          *
 *  Fisier: ChatApp.cs                                                     *
 *                                                                         *
 *  Descriere: Conține form-ul care încapsulează toate view-urile și       *
 *  implementarea View-ului din MVP                                        *
 *  ***********************************************************************/


using Chat_App.Views;
using ChatAppClient;
using MainServerNs;
using System;
using ChatAppClient.Messages;
using System.Windows.Forms;
using System.Drawing;
using Model.Commons;

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
        /// Referință către un DateFormat ce are rolul de a afișa data într-o anumită formă.
        /// </summary>
        private DateFormat _dateFormat = DateFormat.MonthDayYearDateFormat;

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
        /// Proprietate publică care permite accesul formatului de data.
        /// </summary>
        public DateFormat DateFormat
        {
            get
            {
                return _dateFormat;
            }

            set
            {
                _dateFormat = value;
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
            //_chatForm.Show();
        }

        /// <summary>
        /// Setează presenterul care se va ocupa de view.
        /// </summary>
        /// <param name="presenter"></param>
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

        /// <summary>
        /// Metodă apelată de către presenter. Adaugă un mesajul în chat.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="addToEnd"></param>
        public void AddMessageToChat(ChatAppClient.Messages.Message message, bool addToEnd)
        {
            /*ListViewItem newMessage = new ListViewItem();
            newMessage.Text = message.Msg;
            newMessage.ForeColor = Color.DarkOrange;*/

            if (addToEnd)
            {
                _chatForm.Chat.Text += "[" + message.Timestamp.ToString(DateFormat.ToString() + " HH:mm") + "]" + message.From + ": " + message.Msg + '\n';
            }
            else
            {
                _chatForm.Chat.Text = "[" + message.Timestamp.ToString(DateFormat.ToString() + " HH:mm") + "]" + message.From + ": " + message.Msg + '\n' + _chatForm.Chat.Text;
            }
            //_chatForm.Chat.Items.Add(newMessage);
        }

        /// <summary>
        /// TODO
        /// Trebuie să modifice chat-ul de 
        /// </summary>
        /// <param name="username"></param>
        public void FriendHasSeen(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adaugă un prieten în lista de prieteni.
        /// </summary>
        /// <param name="friend"></param>
        public void AddFriendList(string friend)
        {
            ListViewItem newFriend = new ListViewItem();
            newFriend.Text = friend + "[Offline]";
            newFriend.ForeColor = Color.OrangeRed;
            _chatForm.FriendList.Items.Add(newFriend);
        }

        /// <summary>
        /// Schimbă status-ul prietenului din online în offline sau invers.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="status"></param>
        public void ChangeFriendStatus(string username, bool status)
        {
            foreach (ListViewItem friend in _chatForm.FriendList.Items)
            {
                if (friend.Text.Contains(username))
                {
                    if (status)
                    {
                        // set online
                        friend.Text = friend.Text.Replace("[Offline]", "[Online]");
                        if (_chatForm.ActiveFriend.Text.Contains(username))
                            _chatForm.OnlineIcon.Visible = true;
                    }
                    else
                    {
                        // set offline
                        friend.Text = friend.Text.Replace("[Online]", "[Offline]");
                        if (_chatForm.ActiveFriend.Text.Contains(username))
                            _chatForm.OnlineIcon.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Loghează utilizatorul și ia toate datele necesare care trebuie puse
        /// in view-ul ChatView.
        /// </summary>
        public void Login()
        {
            _loginForm.Hide();
            _chatForm.Text = "Chat(" + _loginForm.Control.Username + ")";
            _chatForm.ClearChat();
            _presenter.GetFriendsList(_loginForm.Control.Username);
            _presenter.GetFriendRequests(_loginForm.Control.Username);
            _chatForm.Show();
        }

        /// <summary>
        /// Deloghează utilizatorul.
        /// </summary>
        public void Logout()
        {
            _chatForm.Hide();
            _loginForm.Show();
        }

        /// <summary>
        /// Adaugă un prieten în lista de cereri de prietenie.
        /// </summary>
        /// <param name="username"></param>
        public void AddFriendRequest(string username)
        {
            ListViewItem newFriend = new ListViewItem();
            newFriend.Text = username;
            newFriend.ForeColor = Color.Orange;
            FriendRequestsView.Instance.FriendRequests.Items.Add(newFriend);
        }

        /// <summary>
        /// Arată mesajele de eroare care vin de la server în funcție de form-ul deschis.
        /// </summary>
        /// <param name="message"></param>
        public void ShowErrorMessage(string message)
        {
            // this might need to be changed in the future
            if (_loginForm.Visible)
                if (RegisterView.Instance.Visible)
                    RegisterView.Instance.ErrorMessage.Text = message;
                else
                    _loginForm.Control.ErrorLabel.Text = message;
            if (_chatForm.Visible)
                _chatForm.ErrorLabel.Text = message;
        }

        /// <summary>
        /// Se apelează atunci când înregistrarea e corectă.
        /// </summary>
        public void AcceptRegister()
        {
            RegisterView.Instance.Close();
        }

        /// <summary>
        /// Închide conexiunea cu serverul atunci când se închide aplicația.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            _presenter.CloseConnection("");
        }
    }
}
