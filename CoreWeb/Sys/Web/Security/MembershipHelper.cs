using System.Text.RegularExpressions;
using System.Web.Security;

namespace CoreWeb.Sys.Web.Security
{
    public static class MembershipHelper
    {
        /// <summary>
        /// Checks password complexity requirements for the actual membership provider
        /// </summary>
        /// <param name="password">password to check</param>
        /// <returns>true if the password meets the req. complexity</returns>
        public static bool CheckPasswordComplexity(string password)
        {
            return CheckPasswordComplexity(Membership.Provider, password);
        }

        /// <summary>
        /// Checks password complexity requirements for the given membership provider
        /// </summary>
        /// <param name="membershipProvider">membership provider</param>
        /// <param name="password">password to check</param>
        /// <returns>true if the password meets the req. complexity</returns>
        public static bool CheckPasswordComplexity(MembershipProvider membershipProvider, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Check length
            if (password.Length < membershipProvider.MinRequiredPasswordLength)
            {
                return false;
            }

            // Check non-alphanumeric minimum
            int nonAlnumCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                {
                    nonAlnumCount++;
                }
            }
            if (nonAlnumCount < membershipProvider.MinRequiredNonAlphanumericCharacters)
            {
                return false;
            }

            // Check strength regex
            if (!string.IsNullOrEmpty(membershipProvider.PasswordStrengthRegularExpression)
                &&
                !Regex.IsMatch(password, membershipProvider.PasswordStrengthRegularExpression))
            {
                return false;
            }

            // Valid
            return true;
        }
    }
}
