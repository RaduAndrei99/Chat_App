using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Commons
{
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
            if (timeFormatAsString == Hours24TimeFormat.ToString())
                return Hours24TimeFormat;

            if (timeFormatAsString == Hours12TimeFormat.ToString())
                return Hours12TimeFormat;

            return null;
        }
    }
}
