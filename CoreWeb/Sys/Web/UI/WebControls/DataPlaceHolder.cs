using System.Web.UI.WebControls;

namespace CoreWeb.Sys.Web.UI.WebControls
{
    /// <summary>
    /// This placeholder ensures the content will not be data-bounded when its Visible property is evaluated to false.
    /// </summary>
    public class DataPlaceHolder : PlaceHolder
    {
        protected override void DataBindChildren()
        {
            if (this.Visible)
            {
                base.DataBindChildren();
            }
        }
    }
}