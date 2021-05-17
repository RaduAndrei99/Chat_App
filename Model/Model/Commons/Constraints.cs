namespace Model.Commons
{

    /// <summary>
    /// Formatul datei in aplicatie 
    /// </summary>
    public class DateFormat
    {
        /// <summary>
        /// Sirul de caractere ce defineste formatul datei
        /// </summary>
        private string _dateFormat;

        /// <summary>
        /// Constructorul privat ce initializeaza sirul de caractere ce defineste formatul datei
        /// </summary>
        /// <param name="dateFormat">Formatul datei ca sir de caractere</param>
        private DateFormat(string dateFormat) { _dateFormat = dateFormat; }

        public static readonly DateFormat MonthDayYearDateFormat = new DateFormat("MM/dd/yyyy");
        public static readonly DateFormat MonthNameDayYear = new DateFormat("MMMM/dd/yyyy");
        public static readonly DateFormat DayMonthYear = new DateFormat("dd/MM/yyyy");
        public static readonly DateFormat DayMonthNameYear = new DateFormat("dd/MMMM/yyyy");

        /// <summary>
        /// Returneaza un sir de caractere reprezentand formatul datei
        /// </summary>
        /// <returns>Formatul datei in format sir de caractere</returns>
        public override string ToString() => _dateFormat;

        /// <summary>
        /// Returneaza un obiect DateFormat in functie de sirul de caractere dat ca parametru
        /// </summary>
        /// <param name="dateFormatAsString">Formatul datei dat ca sir de caractere</param>
        /// <returns>Formatul datei</returns>
        public static DateFormat GetDateFormat(string dateFormatAsString)
        {
            switch(dateFormatAsString)
            {
                case "MM/dd/yyyy":
                    return MonthDayYearDateFormat;

                case "MMMM/dd/yyyy":
                    return MonthNameDayYear;

                case "dd/MM/yyyy":
                    return DayMonthYear;

                case "dd/MMMM/yyyy":
                    return DayMonthNameYear;

                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Formatul orei in aplicatie 
    /// </summary>
    public class TimeFormat
    {
        /// <summary>
        /// Sirul de caractere ce defineste formatul orei
        /// </summary>
        private string _timeFormat;

        /// <summary>
        /// Constructorul privat ce initializeaza sirul de caractere ce defineste formatul orei
        /// </summary>
        /// <param name="timeFormat">Formatul orei ca sir de caractere</param>
        private TimeFormat(string timeFormat) { _timeFormat = timeFormat; }

        public static readonly TimeFormat Hours24TimeFormat = new TimeFormat("HH:mm");
        public static readonly TimeFormat Hours12TimeFormat = new TimeFormat("hh:mm tt");

        /// <summary>
        /// Returneaza un sir de caractere reprezentand formatul orei
        /// </summary>
        /// <returns>Formatul orei in format sir de caractere</returns>
        public override string ToString() => _timeFormat;

        /// <summary>
        /// Returneaza un obiect TimeFormat in functie de sirul de caractere dat ca parametru
        /// </summary>
        /// <returns>Formatul datei</returns>
        /// <param name="timeFormatAsString">Formatul orei dat ca sir de caractere</param>
        public static TimeFormat GetTimeFormat(string timeFormatAsString)
        {
            switch (timeFormatAsString)
            {
                case "HH:mm":
                    return Hours24TimeFormat;

                case "hh:mm tt":
                    return Hours12TimeFormat;

                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Constrangerile bazei de date
    /// </summary>
    public class Constraints
    {
        public static readonly string UsernameRegex = @"^\S+$";
        public static readonly string PasswordRegex = @"^\S+$";
        public static readonly string EmailRegex = @"^[a-z0-9._%-]+@[a-z0-9._%-]+\.[a-z]{2,4}$";
        public static readonly string FormatRegex = @"^[a-z]{3,4}$";

        public static readonly string FriendshipPendingStatus = "pending";
        public static readonly string FriendshipFriendsStatus = "friends";

        public static readonly string BooleanTrueStatus = "T";
        public static readonly string BooleanFalseStatus = "F";
    }
}
