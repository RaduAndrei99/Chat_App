using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    [Serializable]
    public class UserRegistrationDoNotExistsException : DoNotExistsException
    {
        public UserRegistrationDoNotExistsException(string username) : base($"Username {username} registration do not exists.")
        {
        }
    }
}