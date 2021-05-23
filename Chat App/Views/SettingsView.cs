/***************************************************************************
 *                                                                         *
 *  Autor:  Gafencu Gabriel                                                *
 *  Grupa:  1309A                                                          *
 *  Fisier: SettingsView.cs                                                *
 *                                                                         *
 *  Descriere: Conține setările aplicației, de la temă, culori și formatul *
 *  datelor                                                                *
 *  ***********************************************************************/


using MaterialSkin;
using Model.Commons;
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
            if (Properties.Settings.Default.theme == 1)
                radioButtonDark.Checked = true;
            else
                radioButtonLight.Checked = true;
            if (Properties.Settings.Default.color == 1)
                radioButtonOrange.Checked = true;
            else
                radioButtonBlue.Checked = true;
            switch (Properties.Settings.Default.dateFormat)
            {
                case 1:
                    radiobuttonMMdd.Checked = true;
                    break;
                case 2:
                    radiobuttonMMMMdd.Checked = true;
                    break;
                case 3:
                    radiobuttonddMM.Checked = true;
                    break;
                case 4:
                    radiobuttonddMMMM.Checked = true;
                    break;
            }
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
            Properties.Settings.Default.theme = 1;
            //LogInView.Instance.Control.Image.Image = Properties.Resources.outline_account_circle_white_48dp;
        }

        /// <summary>
        /// Metodă apelată la apăsarea opțiunii de white, schimbă tema aplicației în white.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonWhite_Click(object sender, EventArgs e)
        {
            _manager.Theme = MaterialSkinManager.Themes.LIGHT;
            Properties.Settings.Default.theme = 2;
            //LogInView.Instance.Control.Image.Image = Properties.Resources.outline_account_circle_black_48dp;
        }

        /// <summary>
        /// Metodă apelată la apăsarea opțiunii de orange. Schimbă schema de culori în portocaliu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonOrange_Click(object sender, EventArgs e)
        {
            _manager.ColorScheme = orangeScheme;
            Properties.Settings.Default.color = 1;
        }

        /// <summary>
        /// Metodă apelată la apăsarea opțiunii de blue. Schimbă schema de culori în albastru.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonBlue_CheckedChanged(object sender, EventArgs e)
        {
            _manager.ColorScheme = blueScheme;
            Properties.Settings.Default.color = 2;
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
            Properties.Settings.Default.Save();
            this.Close();
        }

        /// <summary>
        /// Setează formatul de dată ca MM/dd/yyyy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobuttonMMdd_Click(object sender, EventArgs e)
        {
            ChatApp.Instance.DateFormat = DateFormat.MonthDayYearDateFormat;
            Properties.Settings.Default.dateFormat = 1;
        }

        /// <summary>
        /// Setează formatul de dată ca MMMM/dd/yyyy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobuttonMMMMdd_Click(object sender, EventArgs e)
        {
            ChatApp.Instance.DateFormat = DateFormat.MonthNameDayYear;
            Properties.Settings.Default.dateFormat = 2;
        }

        /// <summary>
        /// Setează formatul de dată ca dd/MM/yyyy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobuttonddMM_Click(object sender, EventArgs e)
        {
            ChatApp.Instance.DateFormat = DateFormat.DayMonthYear;
            Properties.Settings.Default.dateFormat = 3;
        }

        /// <summary>
        /// Setează formatul de dată ca dd/MMMM/yyyy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobuttonddMMMM_Click(object sender, EventArgs e)
        {
            ChatApp.Instance.DateFormat = DateFormat.DayMonthNameYear;
            Properties.Settings.Default.dateFormat = 4;
        }
    }
}
