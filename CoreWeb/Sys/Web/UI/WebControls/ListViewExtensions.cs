using System.Web.UI.WebControls;

/// <summary>
/// Extensions on the ListView control.
/// </summary>
public static class ListViewExtensions
{
    /// <summary>
    /// Sets the listview's DataSource with the given object and then calls DataBind().
    /// </summary>
    /// <param name="listView">ListView control.</param>
    /// <param name="datasource">Datasource to use.</param>
    public static void DataBindWith(this ListView listView, object datasource)
    {
        listView.DataSource = datasource;
        listView.DataBind();
    }
}