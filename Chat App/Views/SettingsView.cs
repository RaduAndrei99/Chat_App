using MaterialSkin;
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
    public partial class SettingsView : BasicView
    {
        private static SettingsView _instance;

        public static ColorScheme orangeScheme = new ColorScheme(Primary.DeepOrange300, Primary.DeepOrange500, Primary.DeepOrange500, Accent.DeepOrange700, TextShade.BLACK);
        public static ColorScheme blueScheme = new ColorScheme(Primary.LightBlue300, Primary.LightBlue500, Primary.LightBlue500, Accent.Blue700, TextShade.BLACK);

        public static SettingsView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SettingsView();
                return _instance;
            }
        }

        private SettingsView() : base()
        {
            InitializeComponent();
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        private void radioButtonDark_Click(object sender, EventArgs e)
        {
            _manager.Theme = MaterialSkinManager.Themes.DARK;
            LogInView.Instance.Control.Image.Image = Properties.Resources.outline_account_circle_white_48dp;
        }

        private void radioButtonWhite_Click(object sender, EventArgs e)
        {
            _manager.Theme = MaterialSkinManager.Themes.LIGHT;
            LogInView.Instance.Control.Image.Image = Properties.Resources.outline_account_circle_black_48dp;
        }

        private void radioButtonOrange_Click(object sender, EventArgs e)
        {
            _manager.ColorScheme = orangeScheme;
        }

        private void radioButtonBlue_CheckedChanged(object sender, EventArgs e)
        {
            _manager.ColorScheme = blueScheme;
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
