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

namespace Chat_App.Views
{
    public partial class BasicView : MaterialForm
    {
        protected readonly MaterialSkinManager _manager;
        public BasicView()
        {
            InitializeComponent();
            _manager = MaterialSkin.MaterialSkinManager.Instance;
            _manager.AddFormToManage(this);
            _manager.Theme = MaterialSkinManager.Themes.DARK;
            _manager.ColorScheme = new ColorScheme(Primary.DeepOrange300, Primary.DeepOrange500, Primary.DeepOrange500, Accent.DeepOrange700, TextShade.BLACK);
        }
    }
}
