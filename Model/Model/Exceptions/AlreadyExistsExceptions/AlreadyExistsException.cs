using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions
{
    [Serializable]
    public abstract class AlreadyExistsException : Exception
    {
        protected AlreadyExistsException() : base()
        {
        }
        protected AlreadyExistsException(string message) : base(message)
        {
        }
    }
}
