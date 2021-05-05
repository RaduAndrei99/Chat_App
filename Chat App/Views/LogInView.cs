using Chat_App.Views;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App
{
    public partial class LogInView : BasicView
    {
        private static LogInView _instance;

        public static LogInView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogInView();
                return _instance;
            }
        }

        private LogInView() : base()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            _instance = this;
            panelUser.Controls.Add(new LogInControl(this));
        }
    }
}
