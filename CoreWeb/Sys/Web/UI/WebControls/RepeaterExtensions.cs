using System.Web.UI.WebControls;

public static class RepeaterExtensions
{
    /// <summary>
    /// Sets the DataSource and then calls DataBind() on a repeater with the given datasource.
    /// </summary>
    /// <param name="repeater"></param>
    /// <param name="datasource"></param>
    public static void DataBindWith(this Repeater repeater, object datasource)
    {
        repeater.DataSource = datasource;
        repeater.DataBind();
    }
}