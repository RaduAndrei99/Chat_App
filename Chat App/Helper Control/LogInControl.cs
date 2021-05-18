using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App.Views
{
    public partial class LogInControl : UserControl
    {
        private BasicView _parentForm;
        private BasicView _chatForm = ChatView.Instance;
        private BasicView _settingsForm = SettingsView.Instance;

        public LogInControl(BasicView parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            // check if valid user
            _chatForm.StartPosition = FormStartPosition.CenterScreen;
            // move below line to log out
            _chatForm.FormClosing += delegate { _parentForm.Show(); };
            _chatForm.Show();
            _parentForm.Hide();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            _settingsForm.StartPosition = FormStartPosition.CenterScreen;
            _parentForm.FormClosing += delegate { _settingsForm.Close(); };
            _settingsForm.Show();
        }
    }
}
