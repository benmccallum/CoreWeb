using System.Web.UI.WebControls;

/// <summary>
/// Extensions on the <see cref="RepeaterItemEventArgs"/> type.
/// </summary>
public static class RepeaterItemEventArgsExtensions
{
    public static bool IsItem(this RepeaterItemEventArgs e)
    {
        var itemType = e.Item.ItemType;
        return itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem;
    }
    
    public static T GetControl<T>(this RepeaterItemEventArgs args, string controlName) where T : class
    {
        return args.Item.FindControl(controlName) as T;
    }
}