using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Commons
{
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
