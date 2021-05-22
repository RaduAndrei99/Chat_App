using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand o relatie de prietenie nu exista in baza de date
    /// </summary>
    [Serializable]
    public class FriendRelationshipDoNotExistsException : DoNotExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="fromUsername">Numele unui utilizator al unei persoane</param>
        /// <param name="toUsername">Numele unui utilizator al celeilalte persoane</param>
        public FriendRelationshipDoNotExistsException(string fromUsername, string toUsername) : base($"Friend relationship between {fromUsername} and {toUsername} do not exists.")
        {
        }
    }
}
