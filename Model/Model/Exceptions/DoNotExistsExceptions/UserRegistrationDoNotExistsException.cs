using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand un utilizator nu este inregistrat in baza de date
    /// </summary>
    [Serializable]
    public class UserRegistrationDoNotExistsException : DoNotExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username">Numele utilizatorului al persoanei</param>
        public UserRegistrationDoNotExistsException(string username) : base($"Username {username} registration do not exists.")
        {
        }
    }
}