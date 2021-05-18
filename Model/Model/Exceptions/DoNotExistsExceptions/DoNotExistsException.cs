using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.DoNotExistsExceptions
{
    [Serializable]
    public abstract class DoNotExistsException : Exception
    {
        protected DoNotExistsException() : base()
        {
        }
        protected DoNotExistsException(string message) : base(message)
        {
        }
    }
}
