/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: WrongPasswordFormatException.cs                                 *
 *                                                                          *
 *  Descriere: Exceptie cand formatul parolei unui utilizator este incorect *
 *                                                                          *
 ****************************************************************************/

using System;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand formatul parolei unui utilizator este incorect
    /// </summary>
    [Serializable]
    public class WrongPasswordFormatException : WrongFormatException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="password">Formatul parolei</param>
        public WrongPasswordFormatException(string password) : base($"Wrong password format for {password}.")
        {
        }
    }
}
