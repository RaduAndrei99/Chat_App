using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand setarile aplicatiei exista deja in baza de date
    /// </summary>
    class ApplicationSettingsAlreadyExistsException : AlreadyExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username">Numele utilizatorului pentru care setarile aplicatiei exista deja in baza de date</param>
        public ApplicationSettingsAlreadyExistsException(string username) : base($"Application settings for {username} entry already exists.")
        {
        }
    }
}
