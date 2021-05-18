using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    [Serializable]
    public class UserRegistrationAlreadyExistsException : AlreadyExistsException
    {
        public UserRegistrationAlreadyExistsException(string username) : base($"Username {username} registration already exists.")
        {
        }
    }
}