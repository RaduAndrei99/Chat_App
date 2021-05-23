/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: AlreadyExistsException.cs                                       *
 *                                                                          *
 *  Descriere: Clasa de baza pentru exceptiile care trebuie aruncate        *
 *              atunci cand un element exista deja in baza de date          *
 *                                                                          *
 ****************************************************************************/

using System;

namespace Model.Exceptions
{
    /// <summary>
    /// Clasa de baza pentru exceptiile care trebuie aruncate atunci cand un element exista deja in baza de date
    /// </summary>
    [Serializable]
    public abstract class AlreadyExistsException : Exception
    {
        /// <summary>
        /// Contructor implicit
        /// </summary>
        protected AlreadyExistsException() : base()
        {
        }

        /// <summary>
        /// Contructor cu parametrii ce initializeaza mesajul exceptiei
        /// </summary>
        protected AlreadyExistsException(string message) : base(message)
        {
        }
    }
}
