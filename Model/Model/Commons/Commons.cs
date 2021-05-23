/***************************************************************************
 *                                                                         *
 *  Autor:  Cojocaru Constantin-Cosmin                                     *
 *  Grupa:  1309A                                                          *
 *  Fisier: Commons.cs                                                     *
 *                                                                         *   
 *  Descriere: Contine constrangerile bazei de date la nivelul aplicatiei  *
 *                                                                         *
 ***************************************************************************/

namespace Model.Commons
{

    /// <summary>
    /// Constrangerile campurilor bazei de date
    /// </summary>
    public class Constraints
    {
        /// <summary>Constrangerea numelui de utilizator din baza de date</summary>
        public static readonly string UsernameRegex = @"^\w{3,}$";

        /// <summary>Constrangerea numelui de familie si a prenumelui utilizatorului din baza de date</summary>
        public static readonly string PersonalNameRegex = @"^\D+$";

        /// <summary>Constrangerea parolei utilizatorilor din baza de date</summary>
        public static readonly string PasswordRegex = @"^\S{8,16}$";

        /// <summary>Constrangerea parolei utilizatorilor din baza de date</summary>
        public static readonly string PasswordHashedRegex = @"^\S{1,64}$";

        /// <summary>Constrangerea adresei de email al utilizatorilor din baza de date</summary>
        public static readonly string EmailRegex = @"^[a-z0-9._%-]+@[a-z0-9._%-]+\.[a-z]{2,4}$";

        /// <summary>Constrangerea formatului mesajului din baza de date</summary>
        public static readonly string FormatRegex = @"^[a-z]{3,4}$";

        /// <summary>Constrangerea starii cererii de prietenie in asteptare din baza de date</summary>
        public static readonly string FriendshipPendingStatus = "pending";

        /// <summary>Constrangerea starii cererii de prietenie in acceptata din baza de date</summary>
        public static readonly string FriendshipFriendsStatus = "friends";

        /// <summary>Constrangerea booleana true folosita in baza de date</summary>
        public static readonly string BooleanTrueStatus = "T";

        /// <summary>Constrangerea booleana false folosita in baza de date</summary>
        public static readonly string BooleanFalseStatus = "F";
    }
}
