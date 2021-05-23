using System;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand formatul parolei hash-uite unui utilizator este incorect
    /// </summary>
    [Serializable]
    public class WrongHashedPasswordFormatException : WrongFormatException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="hashedPassword">Formatul parolei hash-uite</param>
        public WrongHashedPasswordFormatException(string hashedPassword) : base($"Wrong password format for {hashedPassword}.")
        {
        }
    }
}
