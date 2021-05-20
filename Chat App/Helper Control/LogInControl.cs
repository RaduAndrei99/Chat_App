using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Chat_App.Views
{
    /// <summary>
    /// Control custom făcut pentru a fi reutilizabil.
    /// Conține funcționalități pentru înregistrare, introducerea contului, validarea acestuia și setarea preferințelor.
    /// </summary>
    public partial class LogInControl : UserControl
    {
        /// <summary>
        /// Referintă către form-ul părinte care folosește acest control.
        /// </summary>
        private LogInView _parentForm;

        /// <summary>
        /// Proprietate publică pentru a obține username-ul.
        /// </summary>
        public string Username
        {
            get
            {
                return textboxUsername.Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru a obține parola.
        /// </summary>
        public string Password
        {
            get
            {
                return textboxPassword.Text;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentForm">Form-ul părinte care conține controlul.</param>
        public LogInControl(LogInView parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        /// <summary>
        /// Trimite o cerere de login către server dacă username-ul și parola sunt valide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            // check if valid user
            // move below line to log out
            /*_parentForm.ChatForm.FormClosing += delegate { _parentForm.Show(); };
            _parentForm.ChatForm.Show();
            _parentForm.Hide();*/

            // verify if user and password are correct
            if (IsValidUsername(Username) && IsValidPassword(Password))
            {
                /*if (!ChatApp.Instance.Presenter.Login(Username, Password))
                    labelErrorMessage.Text = "Server not responding...";*/
                ChatApp.Instance.Presenter.Login(Username, Password);
            }
            else
            {
                labelErrorMessage.Text = "Invalid username or password!";
            }
        }

        /// <summary>
        /// Afișează setările.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            _parentForm.FormClosing += delegate { _parentForm.SettingsForm.Close(); };
            _parentForm.SettingsForm.Show();
        }

        /// <summary>
        /// Validează username-ul introdus astfel încât să respecte toate cerințele.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool IsValidUsername(string username)
        {
            return new Regex(Model.Commons.Constraints.UsernameRegex).IsMatch(username);
        }

        /// <summary>
        /// Validează parola introdusă astfel încât să respecte toate cerințele.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsValidPassword(string password)
        {
            return new Regex(Model.Commons.Constraints.PasswordRegex).IsMatch(password);
        }
    }
}
