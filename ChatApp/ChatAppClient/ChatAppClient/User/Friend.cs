using ChatAppClient.Messages;
using System.Collections.Generic;

namespace ChatAppClient
{
    class Friend
    {
        /// <summary>
        /// Mesajele dintr-o conversatie cu un prieten
        /// </summary>
        private List<Message> _messages;

        /// <summary>
        /// Variabila care indica daca prietenul este online;
        /// </summary>
        private bool isOnline;
        public bool Online
        {
            get
            {
                return isOnline;
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
