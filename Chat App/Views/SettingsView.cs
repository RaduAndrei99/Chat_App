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
    /// <summary>
    /// Clasă ce permite schimbarea setărilor de către utilizator.
    /// Acesta poate schimba tema aplicației, culorile și formatul zilei.
    /// </summary>
    public partial class SettingsView : BasicView
    {
        /// <summary>
        /// Referință pentru implementarea singleton.
        /// </summary>
        private static SettingsView _instance;

        /// <summary>
        /// Atribut static ce descrie schema portocalie de culori.
        /// </summary>
        public static ColorScheme orangeScheme = new ColorScheme(Primary.DeepOrange300, Primary.DeepOrange500, Primary.DeepOrange500, Accent.DeepOrange700, TextShade.BLACK);
        /// <summary>
        /// Atribut static ce descrie schema albastră de culori.
        /// </summary>
        public static ColorScheme blueScheme = new ColorScheme(Primary.LightBlue300, Primary.LightBlue500, Primary.LightBlue500, Accent.Blue700, TextShade.BLACK);

        /// <summary>
        /// Proprietate publică pentru accesul instanței unice.
        /// </summary>
        public static SettingsView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SettingsView();
                return _instance;
            }
        }

        /// <summary>
        /// Constructor privat.
        /// </summary>
        private SettingsView() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Eveniment ce se apelează la încărcarea form-ului pentru implementarea singleton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        /// <summary>
        /// Metodă apelată la apăsarea opțiunii de dark, schimba tema aplicației în dark.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonDark_Click(object sender, EventArgs e)
        {
            _manager.Theme = MaterialSkinManager.Themes.DARK;
            LogInView.Instance.Control.Image.Image = Properties.Resources.outline_account_circle_white_48dp;
        }

        /// <summary>
        /// Metodă apelată la apăsarea opțiunii de white, schimbă tema aplicației în white.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonWhite_Click(object sender, EventArgs e)
        {
            _manager.Theme = MaterialSkinManager.Themes.LIGHT;
            LogInView.Instance.Control.Image.Image = Properties.Resources.outline_account_circle_black_48dp;
        }

        /// <summary>
        /// Metodă apelată la apăsarea opțiunii de orange. Schimbă schema de culori în portocaliu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonOrange_Click(object sender, EventArgs e)
        {
            _manager.ColorScheme = orangeScheme;
        }

        /// <summary>
        /// Metodă apelată la apăsarea opțiunii de blue. Schimbă schema de culori în albastru.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonBlue_CheckedChanged(object sender, EventArgs e)
        {
            _manager.ColorScheme = blueScheme;
        }

        /// <summary>
        /// Metodă apelată la închiderea form-ului. Doar îl ascunde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Metodă apelată la apăsarea butonului aplicației.
        /// Închide form-ul.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
