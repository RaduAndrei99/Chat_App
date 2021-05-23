/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: UserRegistrationAlreadyExistsException.cs                       *
 *                                                                          *
 *  Descriere: Exceptie cand utilizatorul este deja inregistrat             *
 *              in baza de date                                             *
 *                                                                          *
 ****************************************************************************/

using System;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand un utilizator este deja inregistrat in baza de date
    /// </summary>
    [Serializable]
    public class UserRegistrationAlreadyExistsException : AlreadyExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username">Numele utilizatorului care este deja inregistrat in baza de date</param>
        public UserRegistrationAlreadyExistsException(string username) : base($"Username {username} registration already exists.")
        {
        }
    }
}