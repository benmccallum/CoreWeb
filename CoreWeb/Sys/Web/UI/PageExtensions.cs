using System;
using System.Reflection;

/// <summary>
/// Extensions for the System.Web.UI.Page type.
/// </summary>
public static class PageExtensions
{
    /// <summary>
    /// Helper method to detect whether the <c>System.Web.UI.Page</c> has been validated.
    /// </summary>
    /// <returns>True/false</returns>
    public static bool IsPageValidated(this System.Web.UI.Page page)
    {
        if (page == null)
        {
            throw new ArgumentException("System.Web.UI.Page object supplied was null.");
        }
        FieldInfo fieldValidated = typeof(System.Web.UI.Page).GetField("_validated", BindingFlags.Instance | BindingFlags.NonPublic);
        return (bool)fieldValidated.GetValue(page);
    }
}
