using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions
{
    /// <summary>
    /// Clasa de baza pentru exceptiile care trebuie aruncate atunci cand un element exista deja in baza de date
    /// </summary>
    [Serializable]
    public abstract class AlreadyExistsException : Exception
    {
        /// <summary>
        /// Contructor implicit
        /// </summary>
        protected AlreadyExistsException() : base()
        {
        }

        /// <summary>
        /// Contructor cu parametrii ce initializeaza mesajul exceptiei
        /// </summary>
        protected AlreadyExistsException(string message) : base(message)
        {
        }
    }
}
