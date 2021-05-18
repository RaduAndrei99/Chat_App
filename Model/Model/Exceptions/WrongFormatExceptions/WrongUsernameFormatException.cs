using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    [Serializable]
    class WrongUsernameFormatException : WrongFormatException
    {
        public WrongUsernameFormatException(string username) : base($"Wrong username format for {username}.")
        {
        }
    }
}
