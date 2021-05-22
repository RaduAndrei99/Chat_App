using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand un utilizator exista deja in baza de date
    /// </summary>
    [Serializable]
    public class UserAlreadyExistsException : AlreadyExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username">Numele unui utilizator al persoanei</param>
        public UserAlreadyExistsException(string username) : base($"Username {username} already exists.")
        {
        }
    }
}
