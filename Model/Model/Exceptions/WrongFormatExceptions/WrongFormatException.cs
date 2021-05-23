/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: WrongFormatException.cs                                         *
 *                                                                          *
 *  Descriere: Exceptie cand formatul unui element este incorect            *
 *                                                                          *
 ****************************************************************************/

using System;

namespace Model.Exceptions.WrongFormatExceptions
{
    /// <summary>
    /// Clasa de baza pentru exceptiile care trebuie aruncate atunci cand formatul unui element este incorect
    /// </summary>
    [Serializable]
    public abstract class WrongFormatException : Exception
    {
        /// <summary>
        /// Contructor implicit
        /// </summary>
        protected WrongFormatException() : base()
        {
        }

        /// <summary>
        /// Contructor cu parametrii ce initializeaza mesajul exceptiei
        /// </summary>
        protected WrongFormatException(string message) : base(message)
        {
        }

    }
}
