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
        private static ChatView _instance;
        private AddFriendView addFriendForm;

        public static ChatView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatView();
                return _instance;
            }
        }

        private ChatView()
        {
            InitializeComponent();
        }

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
        }

        private void buttonAddFriend_Click(object sender, EventArgs e)
        {
            addFriendForm = AddFriendView.Instance;
            addFriendForm.SetParentForm(this);
            addFriendForm.Show();
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
            this.Close();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            var form = SettingsView.Instance;
            form.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += delegate { form.Close(); };
            form.Show();
        }
    }
}
