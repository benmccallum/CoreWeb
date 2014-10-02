using System;
using System.Reflection;
using System.Web.UI;
using CoreWeb.Helpers;

/// <summary>
/// Extensions for the System.Web.UI.Page type.
/// </summary>
public static class PageExtensions
{
    /// <summary>
    /// Helper method to detect whether the <c>System.Web.UI.Page</c> has been validated.
    /// </summary>
    /// <returns>True/false</returns>
    public static bool IsPageValidated(this Page page)
    {
        if (page == null)
        {
            throw new ArgumentException("System.Web.UI.Page object supplied was null.");
        }
        var validatedFieldInfo = typeof(Page).GetField("_validated", BindingFlags.Instance | BindingFlags.NonPublic);
        return validatedFieldInfo != null && (bool)validatedFieldInfo.GetValue(page);
    }

    public static T LoadControl<T>(this Page page, string controlPath)
        where T : Control
    {
        return ControlHelper.LoadControl<T>(page, controlPath);
    }
}
