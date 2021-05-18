using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    [Serializable]
    public class UserAlreadyExistsException : AlreadyExistsException
    {
        public UserAlreadyExistsException(string username) : base($"Username {username} already exists.")
        {
        }
    }
}
