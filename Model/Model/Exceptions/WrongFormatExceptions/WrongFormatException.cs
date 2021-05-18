using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    [Serializable]
    public abstract class WrongFormatException : Exception
    {
        protected WrongFormatException() : base()
        {
        }

        protected WrongFormatException(string message) : base(message)
        {
        }

    }
}
