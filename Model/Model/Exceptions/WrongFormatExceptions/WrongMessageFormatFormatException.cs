using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand formatul formatului mesajului este incorect
    /// </summary>
    [Serializable]
    class WrongMessageFormatFormatException : WrongFormatException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="format">Formatul mesajului</param>
        public WrongMessageFormatFormatException(string format) : base($"Wrong message/attachment format for {format}.")
        {
        }
    }
}
