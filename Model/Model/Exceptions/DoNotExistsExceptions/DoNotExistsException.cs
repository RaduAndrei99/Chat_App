/***************************************************************************
 *                                                                         *
 *  Autor:  Cojocaru Constantin-Cosmin                                     *
 *  Grupa:  1309A                                                          *
 *  Fisier: DoNotExistsException.cs                                                     *
 *                                                                         *
 *  Descriere: Clasa de baza pentru exceptiile care trebuie aruncate       *
 *              atunci cand un element nu exista in baza de date           *
 *                                                                         *
 ***************************************************************************/

using System;

namespace Model.Exceptions.DoNotExistsExceptions
{
    /// <summary>
    /// Clasa de baza pentru exceptiile care trebuie aruncate atunci cand un element nu exista in baza de date
    /// </summary>
    [Serializable]
    public abstract class DoNotExistsException : Exception
    {
        /// <summary>
        /// Contructor implicit
        /// </summary>
        protected DoNotExistsException() : base()
        {
        }

        /// <summary>
        /// Contructor cu parametrii ce initializeaza mesajul exceptiei
        /// </summary>
        protected DoNotExistsException(string message) : base(message)
        {
        }
    }
}
