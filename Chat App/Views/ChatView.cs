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

        public static ChatView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatView();
                return _instance;
            }
        }

        public ChatView()
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
            newMessage.BackColor = Color.Orange;
            listviewChat.Items.Add(listviewChat.Columns[0].Name);
        }
    }
}
