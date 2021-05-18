using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    class ApplicationSettingsAlreadyExistsException : AlreadyExistsException
    {
        public ApplicationSettingsAlreadyExistsException(string username) : base($"Application settings for {username} entry already exists.")
        {
        }
    }
}
