using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    [Serializable]
    public class UserDoNotExistsException : DoNotExistsException
    {
        public UserDoNotExistsException(string username) : base($"Username {username} do not exists.")
        {
        }
    }
}
