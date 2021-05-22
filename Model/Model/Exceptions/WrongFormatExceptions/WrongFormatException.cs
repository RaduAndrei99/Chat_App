using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Clasa de baza pentru exceptiile care trebuie aruncate atunci cand formatul unui element este incorect
    /// </summary>
    [Serializable]
    public abstract class WrongFormatException : Exception
    {
        /// <summary>
        /// Contructor implicit
        /// </summary>
        protected WrongFormatException() : base()
        {
        }

        /// <summary>
        /// Contructor cu parametrii ce initializeaza mesajul exceptiei
        /// </summary>
        protected WrongFormatException(string message) : base(message)
        {
        }

    }
}
