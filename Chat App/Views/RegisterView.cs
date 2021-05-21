using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.Commons;

namespace Chat_App.Views
{
    /// <summary>
    /// Clasă ce descrie form-ul de înregistrare. 
    /// Permite introducerea de nume, username, email, parolă și zi de naștere
    /// </summary>
    public partial class RegisterView : BasicView
    {
        /// <summary>
        /// Referință statică privată pentru implementarea singleton.
        /// </summary>
        private static RegisterView _instance;

        /// <summary>
        /// Proprietate publică care accesează instanțierea unică.
        /// </summary>
        public static RegisterView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RegisterView();
                return _instance;
            }
        }

        /// <summary>
        /// Proprietate publică pentru a accesa numele utilizatorului.
        /// </summary>
        public string EnteredName
        {
            get
            {
                return textfieldName.Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru a accesa username-ul.
        /// </summary>
        public string Username
        {
            get
            {
                return textfieldUsername.Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru a accesa e-mailul.
        /// </summary>
        public string Email
        {
            get
            {
                return textfieldEmail.Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru a accesa parola introdusă de utilizator.
        /// </summary>
        public string Password
        {
            get
            {
                return textfieldPassword.Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru a accesa parola reintrodusă pentru a fi verificată.
        /// </summary>
        public string VerifiedPassword
        {
            get
            {
                return textfieldPasswordVerify.Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru a accesa ziua de naștere introdusă.
        /// </summary>
        public string Birthdate
        {
            get
            {
                return textfieldBirthdate.Text;
            }
        }

        /// <summary>
        /// Constructor privat.
        /// </summary>
        private RegisterView() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Eveniment ce se apelează la încărcarea form-ului pentru implementarea de singleton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        /// <summary>
        /// Eveniment suprascris pentru a ascunde form-ul în loc de a îl închide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            // validate data and then send to presenter
            if (Password == VerifiedPassword)
            {
                if (IsValidUsername(Username) && IsValidPassword(Password) && IsValidEmail(Email))
                {
                    string firstName = EnteredName.Split(' ')[0];
                    string lastName = EnteredName.Split(' ')[1];
                    ChatApp.Instance.Presenter.Register(Username, Password, firstName, lastName, Email, Birthdate);
                }
                else
                {
                    labelErorrMessage.Text = "Datele nu sunt valide!";
                }
            }
            else
            {
                labelErorrMessage.Text = "Parolele trebuie să coincidă!";
            }
        }

        /// <summary>
        /// Regex care validează username-ul.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool IsValidUsername(string username)
        {
            return new Regex(Constraints.UsernameRegex).IsMatch(username);
        }

        /// <summary>
        /// Regex care validează parola.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsValidPassword(string password)
        {
            return new Regex(Constraints.PasswordRegex).IsMatch(password);
        }

        /// <summary>
        /// Regex care validează adresa de e-mail.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string email)
        {
            return new Regex(Constraints.EmailRegex).IsMatch(email);
        }
    }
}
