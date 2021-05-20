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
        private LogInView _parentForm;

        public LogInControl(LogInView parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            // check if valid user
            // move below line to log out
            _parentForm.ChatForm.FormClosing += delegate { _parentForm.Show(); };
            _parentForm.ChatForm.Show();
            _parentForm.Hide();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            _parentForm.FormClosing += delegate { _parentForm.SettingsForm.Close(); };
            _parentForm.SettingsForm.Show();
        }
    }
}
