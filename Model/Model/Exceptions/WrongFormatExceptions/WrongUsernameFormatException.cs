using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand formatul numelui de utilizator este incorect
    /// </summary>
    [Serializable]
    public class WrongUsernameFormatException : WrongFormatException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username">Numele de utilizator</param>
        public WrongUsernameFormatException(string username) : base($"Wrong username format for {username}.")
        {
        }
    }
}
