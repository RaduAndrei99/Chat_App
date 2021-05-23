/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: ApplicationSettingsAlreadyExistsException.cs                    *
 *                                                                          *
 *  Descriere: Exceptie cand setarile aplicatiei exista deja                *
 *              in baza de date                                             *
 *                                                                          *
 ****************************************************************************/

using System;

namespace Model.Exceptions.AlreadyExistsExceptions
{
    /// <summary>
    /// Exceptie ce trebuie aruncata atunci cand setarile aplicatiei exista deja in baza de date
    /// </summary>
    [Serializable]
    class ApplicationSettingsAlreadyExistsException : AlreadyExistsException
    {
        /// <summary>
        /// Constructorul cu parametrii ce initializeaza datele exceptiei
        /// </summary>
        /// <param name="username">Numele utilizatorului pentru care setarile aplicatiei exista deja in baza de date</param>
        public ApplicationSettingsAlreadyExistsException(string username) : base($"Application settings for {username} entry already exists.")
        {
        }
    }
}
