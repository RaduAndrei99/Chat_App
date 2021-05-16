using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Commons
{
    public class DateFormat
    {
        private string _dateFormat;
        private DateFormat(string dateFormat) { _dateFormat = dateFormat; }

        public static readonly DateFormat MonthDayYearDateFormat = new DateFormat("MM/dd/yyyy");
        public static readonly DateFormat MonthNameDayYear = new DateFormat("MMMM/dd/yyyy");
        public static readonly DateFormat DayMonthYear = new DateFormat("dd/MM/yyyy");
        public static readonly DateFormat DayMonthNameYear = new DateFormat("dd/MMMM/yyyy");

        public override string ToString() => _dateFormat;

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

    public class TimeFormat
    {
        private string _timeFormat;
        private TimeFormat(string timeFormat) { _timeFormat = timeFormat; }

        public static readonly TimeFormat Hours24TimeFormat = new TimeFormat("HH:mm");
        public static readonly TimeFormat Hours12TimeFormat = new TimeFormat("hh:mm tt");

        public override string ToString() => _timeFormat;

        public static TimeFormat GetTimeFormat(string dateFormatAsString)
        {
            switch (dateFormatAsString)
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
