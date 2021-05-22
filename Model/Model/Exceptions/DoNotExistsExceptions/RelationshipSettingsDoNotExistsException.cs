using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand setarile relatiei de prietenie nu exista in baza de date
    /// </summary>
    [Serializable]
    public class RelationshipSettingsDoNotExistsException : DoNotExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username1">Numele unui utilizator al unei persoane</param>
        /// <param name="username2">Numele unui utilizator al celeilalte persoane</param>
        public RelationshipSettingsDoNotExistsException(string username1, string username2) : base($"Relationship settings entry between {username1} and {username2} do not exists.")
        {
        }
    }
}
