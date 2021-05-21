using ChatAppClient.Messages;
using System.Collections.Generic;

namespace ChatAppClient
{
    public class Friend
    {
        /// <summary>
        /// Mesajele dintr-o conversatie cu un prieten
        /// </summary>
        private List<Message> _messages;

        /// <summary>
        /// Variabila care indica daca prietenul este online;
        /// </summary>
        private bool _isOnline;
        public bool Online
        {
            get
            {
                return _isOnline;
            }
            set
            {
                _isOnline = value;
            }
        }


        /// <summary>
        /// Numele unui prieten.
        /// </summary>
        private string _username;

        public string Username
        {
            get
            {
                return _username;
            }
        }


        /// <summary>
        /// Nick-name-ul unui prieten.
        /// </summary>
        string _nickname;
        public string Nickname
        {
            get
            {
                return _nickname;
            }

            set
            {
                _nickname = value;
            }
        }

        public Friend()
        {
            _messages = new List<Message>();
        }
    }
}
