using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    class WrongMessageFormatFormatException : WrongFormatException
    {
        public WrongMessageFormatFormatException(string format) : base($"Wrong message/attachment format for {format}.")
        {
        }
    }
}
