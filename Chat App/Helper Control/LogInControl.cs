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
        public LogInControl(BasicView parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            // check if valid user
            var form = ChatView.Instance;
            form.StartPosition = FormStartPosition.CenterScreen;
            // move below line to log out
            form.FormClosing += delegate { _parentForm.Show(); };
            form.Show();
            _parentForm.Hide();
        }
    }
}
