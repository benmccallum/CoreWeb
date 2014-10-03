using CoreWeb.Sys.Web.UI.WebControls;
using System.Web.UI.WebControls;

/// <summary>
/// Extensions methods for the <see cref="HyperLink"/> type.
/// </summary>
public static class HyperLinkExtensions
{
    /// <summary>
    /// Sets the navigate url property to a value to enable click to call interactions on a mobile phone.
    /// (e.g. tel:0400123123)
    /// </summary>
    /// <param name="hyperlinkControl">HyperLink control to set NavigateUrl on.</param>
    /// <param name="phoneNumber">Phone number (any disallowed chars will be removed!)</param>
    public static void SetNavigateUrlAsTel(this HyperLink hyperlinkControl, string phoneNumber)
    {
        hyperlinkControl.NavigateUrl = HyperLinkHelper.GetTelHref(phoneNumber);
    }
}