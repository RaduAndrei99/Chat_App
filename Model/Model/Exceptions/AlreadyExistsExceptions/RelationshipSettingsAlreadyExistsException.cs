using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand setarile relatiei de prietenie exista deja in baza de date
    /// </summary>
    [Serializable]
    public class RelationshipSettingsAlreadyExistsException : AlreadyExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username1">Numele unui utilizator al unei persoane din relatia de prietenie</param>
        /// <param name="username2">Numele unui utilizator al celeilalte persoane din relatia de prietenie</param>
        public RelationshipSettingsAlreadyExistsException(string username1, string username2) : base($"Relationship settings entry between {username1} and {username2} already exists.")
        {
        }
    }
}
