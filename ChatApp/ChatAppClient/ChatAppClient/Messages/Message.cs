/**************************************************************************
 *                                                                        *
 *  Fisier:     Message.cs                                                *
 *  Autor:      Budeanu Radu-Andrei                                       *
 *  E-mail:     budeanuradu99@gmail.com                                  *
 *  Descriere:  Contine descrierea unui mesaj ce este receptionat de      *
 *              catre un utilizator.                                      *
 *                                                                        *
 **************************************************************************/

using System;

namespace ChatAppClient.Messages
{
 
    /// <summary>
    /// Clasa folosita pentru a abstractiza notiunea de mesaj.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Atribut ce arata daca mesajul a fost vazut de catre destinatar.
        /// </summary>
        private bool _seen;
        public bool Seen
        {
            get
            {
                return _seen;
            }
        }

        /// <summary>
        /// Mesajul reprezentat ca string.
        /// </summary>
        private string _message;
        public string Msg
        {
            get 
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// Utilizatorul care a trimis mesajul.
        /// </summary>
        private string _from;
        public string From
        {
            get
            {
                return _from;
            }

            set
            {
                _from = value;
            }
        }


        /// <summary>
        /// Data la care a fost trimis mesajul.
        /// </summary>
        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value; 
            }
        }

        /// <summary>
        /// Constructorul obiectului Message
        /// </summary>
        public Message()
        {
            _seen = false;
        }

    }
}
