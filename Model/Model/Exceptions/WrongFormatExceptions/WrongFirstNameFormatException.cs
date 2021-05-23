using System;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand formatul prenumelui utilizatorului este incorect
    /// </summary>
    [Serializable]
    public class WrongFirstNameFormatException : WrongFormatException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="firstname">Prenumele utilizatorului</param>
        public WrongFirstNameFormatException(string firstname) : base($"Wrong firstname format for {firstname}.")
        {
        }
    }
}
