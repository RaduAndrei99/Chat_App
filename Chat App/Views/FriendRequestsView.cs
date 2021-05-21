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
    public partial class FriendRequestsView : BasicView
    {
        private static FriendRequestsView _instance;

        public static FriendRequestsView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FriendRequestsView();
                return _instance;
            }
        }

        public ListView FriendRequests
        {
            get
            {
                return listviewFriendRequests;
            }
        }

        private FriendRequestsView()
        {
            InitializeComponent();
        }

        private void FriendRequestsView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        private void FriendRequestsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
