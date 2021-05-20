using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    [Serializable]
    public class WrongPasswordFormatException : WrongFormatException
    {
        public WrongPasswordFormatException(string password) : base($"Wrong password format for {password}.")
        {
        }
    }
}
