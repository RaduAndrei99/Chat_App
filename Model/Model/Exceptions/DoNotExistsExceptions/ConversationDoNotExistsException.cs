using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    [Serializable]
    public class ConversationDoNotExistsException : DoNotExistsException
    {
        public ConversationDoNotExistsException(string username1, string username2) : base($"Conversation between {username1} and {username2} do not exists.")
        {
        }
    }
}
