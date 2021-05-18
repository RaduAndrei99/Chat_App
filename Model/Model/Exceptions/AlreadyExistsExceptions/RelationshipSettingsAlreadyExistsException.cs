using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    [Serializable]
    public class RelationshipSettingsAlreadyExistsException : AlreadyExistsException
    {
        public RelationshipSettingsAlreadyExistsException(string username1, string username2) : base($"Relationship settings entry between {username1} and {username2} already exists.")
        {
        }
    }
}
