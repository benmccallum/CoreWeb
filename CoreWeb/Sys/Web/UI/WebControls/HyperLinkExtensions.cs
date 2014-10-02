using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

/// <summary>
/// Extensions methods for the <see cref="HyperLink"/> type.
/// </summary>
public static class HyperLinkExtensions
{
    /// <summary>
    /// As found at: http://www.ietf.org/rfc/rfc3966.txt#section-3.
    /// Allows:
    ///   +1(800)555-1212
    ///   +18005551212,+18005553434;ext=123
    ///   911;phone-context=+1
    ///   123;phone-context=example.com
    /// 
    /// Does not allow # or $ as the iPhone won't do it anyway.
    ///  http://stackoverflow.com/questions/4660951/how-to-use-tel-with-star-asterisk-or-hash-pound-on-ios
    ///  https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/PhoneLinks/PhoneLinks.html
    /// 
    /// </summary>
    private static readonly Regex TelephoneCharsOnlyRegex = new Regex(@"[^\da-z-+();,=.]");

    /// <summary>
    /// Sets the navigate url property on the given controls to a value to enable click to call interactions on a mobile phone.
    /// (e.g. tel:0400123123)
    /// </summary>
    /// <param name="phoneNumber">Phone number (any disallowed chars will be removed!)</param>
    /// <param name="hyperLinkControls">HyperLink controls to set NavigateUrl on.</param>
    public static void SetNavigateUrlAsTel(string phoneNumber, params HyperLink[] hyperLinkControls)
    {
        var navigateUrl = GetTelHref(phoneNumber);
        foreach (var hlc in hyperLinkControls)
        {
            hlc.NavigateUrl = navigateUrl;
        }
    }

    /// <summary>
    /// Sets the navigate url property to a value to enable click to call interactions on a mobile phone.
    /// (e.g. tel:0400123123)
    /// </summary>
    /// <param name="hyperlinkControl">HyperLink control to set NavigateUrl on.</param>
    /// <param name="phoneNumber">Phone number (any disallowed chars will be removed!)</param>
    public static void SetNavigateUrlAsTel(this HyperLink hyperlinkControl, string phoneNumber)
    {
        hyperlinkControl.NavigateUrl = GetTelHref(phoneNumber);
    }

    /// <summary>
    /// Gets a formatted and sanitised click to call (tel) type href to http://www.ietf.org/rfc/rfc3966.txt standard.
    /// </summary>
    /// <param name="phoneNumber">Phone number (any disallowed chars will be removed!)</param>
    /// <returns>tel:0411123123</returns>
    public static string GetTelHref(string phoneNumber)
    {
        return String.Format("tel:{0}", TelephoneCharsOnlyRegex.Replace(phoneNumber, ""));
    }
}