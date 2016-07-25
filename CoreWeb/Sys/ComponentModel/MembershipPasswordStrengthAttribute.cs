using CoreWeb.Sys.Web.Security;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoreWeb.Sys.ComponentModel
{
    /// <summary>
    /// A validation attribute for checking password stength against the membershipProvider minimum requirements.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MembershipPasswordStrengthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var strValue = value as string;
            return strValue == null ? false : MembershipHelper.CheckPasswordComplexity(strValue);
        }
    }
}
