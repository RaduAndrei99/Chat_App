﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand o conversatie exista deja in baza de date
    /// </summary>
    [Serializable]
    public class ConversationAlreadyExistsException : AlreadyExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username1">Numele unui utilizator al unei persoane din conversatie</param>
        /// <param name="username2">Numele unui utilizator al celeilalte persoane din conversatie</param>
        public ConversationAlreadyExistsException(string username1, string username2) : base($"Conversation between {username1} and {username2} already exists.")
        {
        }
    }
}
