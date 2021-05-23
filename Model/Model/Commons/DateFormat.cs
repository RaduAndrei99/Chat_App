/***************************************************************************
 *                                                                         *
 *  Autor:  Cojocaru Constantin-Cosmin                                     *
 *  Grupa:  1309A                                                          *
 *  Fisier: DateFormat.cs                                                  *
 *                                                                         *
 *  Descriere: Contine constrangerile formatului datei ale bazei de date   *
 *              la nivelul aplicatiei                                      *
 *                                                                         *
 ***************************************************************************/

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
            if (dateFormatAsString == MonthDayYearDateFormat.ToString())
                return MonthDayYearDateFormat;

            if (dateFormatAsString == MonthNameDayYear.ToString())
                return MonthNameDayYear;

            if (dateFormatAsString == DayMonthYear.ToString())
                return DayMonthYear;

            if (dateFormatAsString == DayMonthNameYear.ToString())
                return DayMonthNameYear;

            return null;
        }
    }
}
