using System.Collections.Generic;
using System.Web.UI;
using CoreWeb.Helpers;

public static class ControlExtensions
{
    /// <summary>
    /// Renders an instantiated control into output HTML string.
    /// </summary>
    /// <param name="control">Control.</param>
    /// <returns>HTML string.</returns>
    /// <remarks>Useful for sending back in AJAX.</remarks>
    public static string Render(this Control control)
    {
        return ControlHelper.Render(control);
    }

    /// <summary>
    /// Finds a list of control objects from a root control that match the type requested.
    /// This method recursively crawls down the control hierarchy.
    /// </summary>
    /// <typeparam name="T">The type of control to look for.</typeparam>
    /// <param name="root">The root element to start searching under.</param>
    /// <returns>A list of control objects of the specified type.</returns>
    public static IList<T> FindControlsByType<T>(this Control root)
        where T : Control
    {
        var controls = new List<T>();
        FindControlsByType(root, controls);
        return controls;
    }

    /// <summary>
    /// Recursive helper method for iterating down control hierarchy.
    /// </summary>
    /// <typeparam name="T">The type of control to look for.</typeparam>
    /// <param name="root">The root element to start searching under.</param>
    /// <param name="controls">List of controls to add found controls to.</param>
    /// <returns>A list of control objects of the specified type.</returns>
    private static void FindControlsByType<T>(Control root, IList<T> controls)
        where T : Control
    {
        foreach (Control control in root.Controls)
        {
            if (control is T)
            {
                controls.Add(control as T);
            }
            if (control.Controls.Count > 0)
            {
                FindControlsByType(control, controls);
            }
        }
    }
}
