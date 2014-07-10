using System;
using System.IO;
using System.Text;
using System.Web.UI;

namespace CoreWeb.Helpers
{
    public class ControlHelper
    {
        public static T LoadControl<T>(Page page, String controlPath) where T : Control
        {
            Control result = page.LoadControl(controlPath);
            return result as T;
        }

        public static String Render(Control control)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter tw = new StringWriter(sb))
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(tw))
                {
                    control.RenderControl(hw);
                    return sb.ToString();
                }
            }
        }
    }
}
