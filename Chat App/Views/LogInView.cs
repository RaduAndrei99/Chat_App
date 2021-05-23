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
    /// <summary>
    /// Clasă ce descrie form-ul de LogIn.
    /// Permite logarea, înregistrarea și schimbarea setărilor. 
    /// </summary>
    public partial class LogInView : BasicView
    {
        /// <summary>
        /// Referință către instanța. Pentru implementare de singleton.
        /// </summary>
        private static LogInView _instance;

        /// <summary>
        /// Referință către controlul care conține restul controller-urilor.
        /// </summary>
        private LogInControl _control;

        /// <summary>
        /// Proprietate publică pentru accesul instanței unice.
        /// </summary>
        public static LogInView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogInView();
                return _instance;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul controlului care deține alte date necesare
        /// precum username-ul și parola.
        /// </summary>
        public LogInControl Control
        {
            get
            {
                return _control;
            }
        }

        /// <summary>
        /// Constructor privat.
        /// </summary>
        private LogInView() : base()
        {
            InitializeComponent();
            _control = new LogInControl(this);
            ChatView.Instance.StartPosition = FormStartPosition.CenterScreen;
            SettingsView.Instance.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Metodă apelată la încărcarea form-ului.
        /// Pentru implementarea singleton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogIn_Load(object sender, EventArgs e)
        {
            _instance = this;
            panelUser.Controls.Add(_control);
        }

        /// <summary>
        /// Metodă apelată la închiderea form-ului.
        /// Ascunde form-ul în loc să îl închidă.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogInView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatApp.Instance.Close();
        }
    }
}
