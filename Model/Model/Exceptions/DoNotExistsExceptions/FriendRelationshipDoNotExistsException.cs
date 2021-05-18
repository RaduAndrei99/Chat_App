using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    [Serializable]
    public class FriendRelationshipDoNotExistsException : DoNotExistsException
    {
        public FriendRelationshipDoNotExistsException(string fromUsername, string toUsername) : base($"Friend relationship between {fromUsername} and {toUsername} do not exists.")
        {
        }
    }
}
