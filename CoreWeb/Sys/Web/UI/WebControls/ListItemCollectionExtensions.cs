using System.Web.UI.WebControls;

namespace CoreWeb.Sys.Web.UI.WebControls
{
    /// <summary>
    /// This class contains the extension methods for ListItemCollection
    /// </summary>
    public static class ListItemCollectionExtension
    {
        /// <summary>
        /// Find list item by text, ignoring the cases.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ListItem FindByTextIgnoreCase(this ListItemCollection collection, string text)
        {
            foreach (ListItem item in collection)
            {
                if (string.Compare(item.Text, text, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Find list item by value, ignoring the cases.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ListItem FindByValueIgnoreCase(this ListItemCollection collection, string value)
        {
            foreach (ListItem item in collection)
            {
                if (string.Compare(item.Value, value, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }
    }
}