using System;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand formatul numelui de familie al utilizatorului este incorect
    /// </summary>
    [Serializable]
    public class WrongLastNameFormatException : WrongFormatException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="lastname">Numele de familie al utilizatorului</param>
        public WrongLastNameFormatException(string lastname) : base($"Wrong lastname format for {lastname}.")
        {
        }
    }
}
