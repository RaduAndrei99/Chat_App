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
    public partial class RegisterView : BasicView
    {
        private static RegisterView _instance;

        private static RegisterView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RegisterView();
                return _instance;
            }
        }
        
        private RegisterView()
        {
            InitializeComponent();
        }
    }
}
