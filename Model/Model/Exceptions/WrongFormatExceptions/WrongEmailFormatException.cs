using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    [Serializable]
    public class WrongEmailFormatException : WrongFormatException
    {
        public WrongEmailFormatException(string email) : base($"Wrong email format for {email}.")
        {
        }
    }
}
