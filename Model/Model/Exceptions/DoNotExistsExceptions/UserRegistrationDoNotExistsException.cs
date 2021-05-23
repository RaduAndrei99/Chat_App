/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: UserRegistrationDoNotExistsException.cs                         *
 *                                                                          *
 *  Descriere: Exceptie cand utilizatorul nu este inregistrat               *
 *              in baza de date                                             *
 *                                                                          *
 ****************************************************************************/

using System;

namespace Model.Exceptions.DoNotExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand un utilizator nu este inregistrat in baza de date
    /// </summary>
    [Serializable]
    public class UserRegistrationDoNotExistsException : DoNotExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username">Numele utilizatorului al persoanei</param>
        public UserRegistrationDoNotExistsException(string username) : base($"Username {username} registration do not exists.")
        {
        }
    }
}