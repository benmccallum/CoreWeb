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

    /// <summary>
    /// Select an item safely. If an item with the given <paramref name="value"/> doesn't exist, just don't select anything.
    /// </summary>
    /// <param name="listControl">ListControl</param>
    /// <param name="value">Value to try an select.</param>
    public static void SelectItemSafely(this ListControl listControl, string value)
    {
        listControl.TrySelectItem(value);
    }

    /// <summary>
    /// Select an item safely. If an item with the given <paramref name="value"/> doesn't exist, just don't select anything.
    /// </summary>
    /// <param name="listControl">ListControl</param>
    /// <param name="value">Value to try an select.</param>
    /// <returns>Bool indicating whether an item was found and selected.</returns>
    public static bool TrySelectItem(this ListControl listControl, string value)
    {
        if (listControl.Items.FindByValue(value) != null)
        {
            listControl.SelectedValue = value;
            return true;
        }
        return false;
    }
}
