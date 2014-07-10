using System;
using System.Linq;
using System.Web.UI.WebControls;

/// <summary>
/// Extensions for the <c>System.Web.UI.WebControls.WebControl</c> type.
/// </summary>
public static class WebControlExtensions
{
    /// <summary>
    /// Adds the given css class to the <c>System.Web.UI.WebControl.CssClass</c> property,
    /// appropriately handling spacing and preserving existing classes.
    /// </summary>
    /// <param name="control">The control to add to.</param>
    /// <param name="cssClassName">The class name to add.</param>
    public static void AddCssClass(this WebControl control, string cssClassName)
    {
        if (control.CssClass.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Contains(cssClassName))
        {
            return;
        }

        if (String.IsNullOrWhiteSpace(control.CssClass))
        {
            control.CssClass = cssClassName;
        }
        else
        {
            control.CssClass += " " + cssClassName;
        }
    }

    /// <summary>
    /// Removes the given css class from the <c>System.Web.UI.WebControl.CssClass</c> property,
    /// appropriately handling spacing and preserving existing classes.
    /// </summary>
    /// <param name="control">The control to remove from.</param>
    /// <param name="cssClassName">The class name to remove.</param>
    public static void RemoveCssClass(this WebControl control, string cssClassName)
    {
        control.CssClass = control.CssClass.Replace(" " + cssClassName, "").Replace(cssClassName, "");
    }
}
