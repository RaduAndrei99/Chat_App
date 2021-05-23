/***************************************************************************
 *                                                                         *
 *  Autor:  Gafencu Gabriel                                                *
 *  Grupa:  1309A                                                          *
 *  Fisier: FriendRequestsView.cs                                          *
 *                                                                         *
 *  Descriere: Form-ul ce conține lista de cereri de prietenie             *
 *  ***********************************************************************/

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
    /// Form-ul ce descrie lista de cereri de la prieteni.
    /// </summary>
    public partial class FriendRequestsView : BasicView
    {
        /// <summary>
        /// Referință statică pentru implementarea de singleton.
        /// </summary>
        private static FriendRequestsView _instance;

        /// <summary>
        /// Proprietatea publică pentru accesul instanței unice.
        /// </summary>
        public static FriendRequestsView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FriendRequestsView();
                return _instance;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul la lista de cereri de prieteni.
        /// </summary>
        public ListView FriendRequests
        {
            get
            {
                return listviewFriendRequests;
            }
        }

        /// <summary>
        /// Constructor privat.
        /// </summary>
        private FriendRequestsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metodă ce se apelează la încărcarea form-ului.
        /// Pentru implementarea de singleton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FriendRequestsView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        /// <summary>
        /// Se apelează la închidera form-ului.
        /// Îl ascunde în loc să îl închidă.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FriendRequestsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Metodă apelată la apăsarea butonului de Accept.
        /// Trimite către presenter datele necesare acceptări unei cereri.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (listviewFriendRequests.SelectedItems.Count > 0)
            {
                ChatApp.Instance.Presenter.AcceptFriendRequest(listviewFriendRequests.SelectedItems[0].Text, LogInView.Instance.Control.Username);
                listviewFriendRequests.Items.Remove(listviewFriendRequests.SelectedItems[0]);
                this.Close();
            }
        }
    }
}
