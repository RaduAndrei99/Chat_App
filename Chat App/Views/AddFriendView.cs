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
    /// Clasă ce descrie meniul pentru adăugarea unui prieten. 
    /// </summary>
    public partial class AddFriendView : BasicView
    {
        /// <summary>
        /// Referință către sine pentru implementarea de singleton.
        /// </summary>
        private static AddFriendView _instance;

        /// <summary>
        /// Referință către form-ul părinte.
        /// </summary>
        private ChatView _parentForm;

        /// <summary>
        /// Propriteate publică pentru implementarea de singleton.
        /// </summary>
        public static AddFriendView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AddFriendView();
                return _instance;
            }
        }

        /// <summary>
        /// Constructor privat.
        /// </summary>
        private AddFriendView()
        {
            InitializeComponent();
            this.ActiveControl = textfieldFriendName;
        }

        /// <summary>
        /// Se apelează la încărcarea form-ului. Pentru implementarea de singleton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFriendView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        /// <summary>
        /// Se apelează la închiderea form-ului.
        /// Ascunda înloc să închidă.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFriendView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Setter pentru referința către form-ul părinte.
        /// </summary>
        /// <param name="parent"></param>
        public void SetParentForm(ChatView parent)
        {
            _parentForm = parent;
        }

        /// <summary>
        /// Trimite o cerere către prieten, prin presenter, după care ascunde form-ul.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //_parentForm.AddFriendToList(textfieldFriendName.Text);
            ChatApp.Instance.Presenter.SendFriendRequest(LogInView.Instance.Control.Username, textfieldFriendName.Text);
            textfieldFriendName.Clear();
            this.Hide();
        }
    }
}
