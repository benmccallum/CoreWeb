using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoreWeb.Sys.Web.UI.WebControls
{
    /// <summary>
    /// A custom control that inherits from label and provides additional 
    /// functionality for common functionality such as required field markers.
    /// </summary>
    public class FormLabel : Label
    {
        /// <summary>
        /// Flag for whether this label marks a required field or not.
        /// When true, the label will be rendered with a required field marker.
        /// Default Value: true.
        /// </summary>
        public bool IsRequiredFieldLabel
        {
            get
            {
               return (bool)(ViewState["IsRequiredFieldLabel"] ?? true);
            }
            set
            {
                ViewState["IsRequiredFieldLabel"] = value;
            }
        }
        
        /// <summary>
        /// Overrides the RenderContents method adding the required field marker if necessary.
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);

            if (this.IsRequiredFieldLabel)
            {
                writer.Write("<span class=\"req\">*</span>");
            }
        }
    }
}