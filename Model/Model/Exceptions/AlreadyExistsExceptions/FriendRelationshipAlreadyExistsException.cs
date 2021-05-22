using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand o relatie de prietenie exista deja in baza de date
    /// </summary>
    [Serializable]
    public class FriendRelationshipAlreadyExistsException : AlreadyExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="fromUsername">Numele unui utilizator al unei persoane din relatie de prietenie</param>
        /// <param name="toUsername">Numele unui utilizator al celeilalte persoane din relatia de prietenie</param>
        public FriendRelationshipAlreadyExistsException(string fromUsername, string toUsername) : base($"Friend relationship between {fromUsername} and {toUsername} already exists.")
        {
        }
    }
}
