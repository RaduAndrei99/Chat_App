using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    [Serializable]
    public class RelationshipSettingsDoNotExistsException : DoNotExistsException
    {
        public RelationshipSettingsDoNotExistsException(string username1, string username2) : base($"Relationship settings entry between {username1} and {username2} do not exists.")
        {
        }
    }
}
