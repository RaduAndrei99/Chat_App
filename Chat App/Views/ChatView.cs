using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App.Views
{
    public partial class ChatView : BasicView
    {
        /// <summary>
        /// Referință pentru implementarea șablonului de singleton.
        /// </summary>
        private static ChatView _instance;

        /// <summary>
        /// Referință către form-ul pentru adăugarea unui prieten.
        /// </summary>
        private AddFriendView _addFriendForm;

        /// <summary>
        /// Referință către form-ul care arată lista de cereri de prietenie în așteptare.
        /// </summary>
        private FriendRequestsView _friendRequestsForm;

        /// <summary>
        /// Proprietate publică pentru accesul la instanța unică.
        /// </summary>
        public static ChatView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatView();
                return _instance;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul listei de prieteni.
        /// </summary>
        public ListView FriendList
        {
            get
            {
                return listviewFriends;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul chatului.
        /// </summary>
        public ListView Chat
        {
            get
            {
                return listviewChat;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul prietenului selectat și al cărui chat este deschis.
        /// </summary>
        public string SelectedFriend
        {
            get
            {
                return listviewChat.SelectedItems[0].Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru acessul mesajului de eroare.
        /// </summary>
        public Label ErrorLabel
        {
            get
            {
                return labelErrorMessage;
            }
        }

        /// <summary>
        /// Constructor privat.
        /// </summary>
        private ChatView()
        {
            InitializeComponent();
            _addFriendForm = AddFriendView.Instance;
            _friendRequestsForm = FriendRequestsView.Instance;
        }


        /// <summary>
        /// Metodă ce se apelează odată cu încărcarea formului.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            ListViewItem newMessage = new ListViewItem();
            newMessage.Text = textboxMessage.Text;
            newMessage.ForeColor = Color.DarkOrange;
            listviewChat.Items.Add(newMessage);
            ChatApp.Instance.Presenter.SendMessage(LogInView.Instance.Control.Username, SelectedFriend, textboxMessage.Text);
        }

        private void buttonAddFriend_Click(object sender, EventArgs e)
        {
            _addFriendForm.SetParentForm(this);
            _addFriendForm.Show();
        }

        public void AddFriendToList(string newFriendName)
        {
            ListViewItem newFriend = new ListViewItem();
            newFriend.Text = newFriendName;
            newFriend.ForeColor = Color.OrangeRed;
            listviewFriends.Items.Add(newFriend);
        }

        private void listviewFriends_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = listviewFriends.SelectedItems;
            labelActiveFriend.Text = selectedItem[0].Text;
            // clean up old chat, load in new chat messages
            listviewChat.Clear();
            textboxMessage.Clear();
        }

        private void ChatView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void listviewFriends_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectedItem = listviewFriends.SelectedItems;
            labelActiveFriend.Text = selectedItem[0].Text;
            // clean up old chat, load in new chat messages
            listviewChat.Clear();
            textboxMessage.Clear();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            // log user out
            ChatApp.Instance.Presenter.Logout(LogInView.Instance.Control.Username);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            var form = SettingsView.Instance;
            form.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += delegate { form.Close(); };
            form.Show();
        }

        private void buttonFriendRequests_Click(object sender, EventArgs e)
        {
            var form = FriendRequestsView.Instance;
            form.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += delegate { form.Close(); };
            form.Show();
        }

        public void ClearChat()
        {
            listviewChat.Clear();
            listviewFriends.Clear();
            textboxMessage.Clear();
            _friendRequestsForm.FriendRequests.Clear();
        }
    }
}
