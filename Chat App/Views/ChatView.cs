/***************************************************************************
 *                                                                         *
 *  Autor:  Gafencu Gabriel                                                *
 *  Grupa:  1309A                                                          *
 *  Fisier: ChatView.cs                                                    *
 *                                                                         *
 *  Descriere: Form-ul ce conține chat-ul, și lista de prieteni            *
 *  ***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App.Views
{
    /// <summary>
    /// Clasa care descrie form-ul de chat.
    /// Utilizatorul poate alege din mai mulți prieteni și să vorbească cu ei.
    /// </summary>
    public partial class ChatView : BasicView
    {
        /// <summary>
        /// Referință pentru implementarea șablonului de singleton.
        /// </summary>
        private static ChatView _instance;

        /// <summary>
        /// Referință către form-ul pentru adăugarea unui prieten.
        /// </summary>
        private AddFriendView _addFriendForm;

        /// <summary>
        /// Referință către form-ul care arată lista de cereri de prietenie în așteptare.
        /// </summary>
        private FriendRequestsView _friendRequestsForm;
        
        /// <summary>
        /// Atribut care ține minte prietenul al cărui chat este activ, în cazul în care
        /// utilizatorul selectează simplu alt prieten înainte de a trimite mesaje.
        /// </summary>
        private string _activeChatFriend;

        /// <summary>
        /// Proprietate publică pentru accesul la instanța unică.
        /// </summary>
        public static ChatView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatView();
                return _instance;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul listei de prieteni.
        /// </summary>
        public ListView FriendList
        {
            get
            {
                return listviewFriends;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul chatului.
        /// </summary>
        public RichTextBox Chat
        {
            get
            {
                return richtextboxChat;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul prietenului selectat și al cărui chat este deschis.
        /// </summary>
        public string SelectedFriend
        {
            get
            {
                return listviewFriends.Items[0].Text;
            }
        }

        /// <summary>
        /// Proprietate publică pentru acessul mesajului de eroare.
        /// </summary>
        public Label ErrorLabel
        {
            get
            {
                return labelErrorMessage;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul la prietenul al cărui chat este activ.
        /// </summary>
        public Label ActiveFriend
        {
            get
            {
                return labelActiveFriend;
            }
        }

        /// <summary>
        /// Proprietate publică pentru accesul la iconița de online.
        /// </summary>
        public PictureBox OnlineIcon
        {
            get
            {
                return pictureboxOnline;
            }
        }

        /// <summary>
        /// Constructor privat.
        /// </summary>
        private ChatView()
        {
            InitializeComponent();
            _addFriendForm = AddFriendView.Instance;
            _friendRequestsForm = FriendRequestsView.Instance;
        }


        /// <summary>
        /// Metodă ce se apelează odată cu încărcarea formului.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatView_Load(object sender, EventArgs e)
        {
            _instance = this;
        }

        /// <summary>
        /// Metodă apelată atunci când se apasă butonul de Send.
        /// Trimite mesajul la presenter pentru a fi prelucrat și îl afișează în chat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            /*ListViewItem newMessage = new ListViewItem();
            newMessage.Text = textboxMessage.Text;
            newMessage.ForeColor = Color.DarkOrange;
            listviewChat.Items.Add(newMessage);*/

            // check that a friend is selected and the message isn't empty
            if (FriendList.Items.Count != 0)
            {
                if (textboxMessage.Text.Length > 0)
                {
                    Chat.Text += 
                        "[" + DateTime.Now.ToString(ChatApp.Instance.DateFormat.ToString() + " HH:mm") + "]" +
                        LogInView.Instance.Control.Username + ": " + textboxMessage.Text + '\n';
                    Chat.SelectionStart = Chat.Text.Length;
                    Chat.ScrollToCaret();
                    ChatApp.Instance.Presenter.SendMessage(LogInView.Instance.Control.Username, ActiveFriend.Text.Split('[')[0], textboxMessage.Text);
                }
            }
            else
            {
                labelErrorMessage.Text = "Selectează un prieten mai întâi!";
            }
            textboxMessage.Clear();
        }

        /// <summary>
        /// Deschide form-ul de adăugare de prieteni.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddFriend_Click(object sender, EventArgs e)
        {
            _addFriendForm.SetParentForm(this);
            _addFriendForm.Show();
        }

        /// <summary>
        /// Adaugă un prieten la lista de prieteni din form-ul de adăugare de prieteni.
        /// </summary>
        /// <param name="newFriendName"></param>
        public void AddFriendToList(string newFriendName)
        {
            ListViewItem newFriend = new ListViewItem();
            newFriend.Text = newFriendName;
            newFriend.ForeColor = Color.OrangeRed;
            //newFriend.BackColor = Color.OrangeRed;
            listviewFriends.Items.Add(newFriend);
        }

        /// <summary>
        /// Metodă apelată la apăsarea dublu click pe un prieten.
        /// Deschide chat-ul cu prietenul respectiv.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listviewFriends_DoubleClick(object sender, EventArgs e)
        {
            try 
            {
                var selectedItem = listviewFriends.SelectedItems;
                labelActiveFriend.Text = selectedItem[0].Text;
                _activeChatFriend = selectedItem[0].Text;
                if (_activeChatFriend.Contains("Online"))
                    pictureboxOnline.Visible = true;
                else
                    pictureboxOnline.Visible = false;
                // clean up old chat, load in new chat messages
                Chat.Clear();
                textboxMessage.Clear();
                ChatApp.Instance.Presenter.GetLastNMessages(LogInView.Instance.Control.Username, _activeChatFriend.Split('[')[0], 14);
                Chat.SelectionStart = Chat.Text.Length;
                Chat.ScrollToCaret();
            }
            catch (Exception exception)
            {
                // utilizatorul trebuie sa dea click pe un user
                Debug.Write(exception.Message);
            }
        }

        /// <summary>
        /// Metodă ce se apelează atunci când se închide form-ul.
        /// Doar îl ascunde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatView_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonLogout_Click(sender, e);
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Apelat în momentul apăsării butonului de log-out.
        /// Deconectează utilizatorul, prin intermediul presenterului.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            // log user out
            ChatApp.Instance.Presenter.Logout(LogInView.Instance.Control.Username);
        }

        /// <summary>
        /// Deschide form-ul de setări.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            var form = SettingsView.Instance;
            form.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += delegate { form.Close(); };
            form.Show();
        }

        /// <summary>
        /// Deschide lista de cereri de prieteni.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFriendRequests_Click(object sender, EventArgs e)
        {
            var form = FriendRequestsView.Instance;
            form.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += delegate { form.Close(); };
            form.Show();
        }

        /// <summary>
        /// Elimină tot conținutul din orice tip de control.
        /// Este apelat atunci când un user se loghează.
        /// </summary>
        public void ClearChat()
        {
            listviewChat.Clear();
            listviewFriends.Clear();
            textboxMessage.Clear();
            ActiveFriend.Text = "";
            _friendRequestsForm.FriendRequests.Clear();
        }

        /// <summary>
        /// Adaugă funcționalitatea de a apăsa enter pentru a trimite mesajul în loc de butonul SEND.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textboxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSend_Click(sender, e);
            }
        }

        private void richtextboxChat_VScroll(object sender, EventArgs e)
        {
            if (Chat.GetPositionFromCharIndex(0).Y == 1)
                ChatApp.Instance.Presenter.GetLastNMessages(LogInView.Instance.Control.Username, ActiveFriend.Text.Split('[')[0], 10);
        }
    }
}
