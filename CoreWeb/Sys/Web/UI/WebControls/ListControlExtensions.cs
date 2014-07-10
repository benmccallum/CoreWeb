using System;
using System.Web.UI.WebControls;

public static class ListControlExtensions
{
    /// <summary>
    /// Gets a ListControl's SelectedValue, or a default if the SelectedValue is null or empty.
    /// </summary>
    /// <param name="listControl">ListControl</param>
    /// <param name="defaultValue">Default value if the SelectedValue is null or empty.</param>
    /// <returns>ListControl's SelectedValue, or a default if the SelectedValue is null or empty</returns>
    public static string GetSelectedValueOrDefault(this ListControl listControl, string defaultValue)
    {
        return String.IsNullOrEmpty(listControl.SelectedValue) ? defaultValue : listControl.SelectedValue;
    }
}
