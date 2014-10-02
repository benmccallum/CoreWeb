using System;
using System.IO;
using System.Text;
using System.Web.UI;

namespace CoreWeb.Helpers
{
    /// <summary>
    /// Helper for web controls and pages.
    /// </summary>
    public class ControlHelper
    {
        public static T LoadControl<T>(Page page, String controlPath)
            where T : Control
        {
            return page.LoadControl(controlPath) as T;
        }
        
        /// <summary>
        /// Renders an instantiated <see cref="Control"/> into it's HTML output.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        /// <remarks>Super useful if you want to then return this in an AJAX call or something similar.</remarks>
        public static string Render(Control control)
        {
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                using (var hw = new HtmlTextWriter(tw))
                {
                    control.RenderControl(hw);
                    return sb.ToString();
                }
            }
        }
    }
}
