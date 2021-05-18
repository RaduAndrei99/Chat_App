using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    [Serializable]
    public class FriendRelationshipAlreadyExistsException : AlreadyExistsException
    {
        public FriendRelationshipAlreadyExistsException(string fromUsername, string toUsername) : base($"Friend relationship between {fromUsername} and {toUsername} already exists.")
        {
        }
    }
}
