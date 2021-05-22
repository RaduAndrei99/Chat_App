using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand formatul adresei de email este incorect
    /// </summary>
    [Serializable]
    public class WrongEmailFormatException : WrongFormatException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="email">Adresa de email</param>
        public WrongEmailFormatException(string email) : base($"Wrong email format for {email}.")
        {
        }
    }
}
