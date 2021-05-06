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
    public partial class AddFriendView : BasicView
    {
        private static AddFriendView _instance;
        private ChatView _parentForm;

        public static AddFriendView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AddFriendView();
                return _instance;
            }
        }

        private AddFriendView()
        {
            InitializeComponent();
            this.ActiveControl = textfieldFriendName;
        }

        private void AddFriendView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        private void AddFriendView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void SetParentForm(ChatView parent)
        {
            _parentForm = parent;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            _parentForm.AddFriendToList(textfieldFriendName.Text);
            this.Hide();
        }
    }
}
