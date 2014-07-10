using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoreWeb.Sys.Web.UI.Web.WebControls
{
    /// <summary>
    /// A custom control that inherits from panel and overloads the 
    /// functionality of the DefaultButton property enabling forms to
    /// be submitted with the enter button as scoped within this control.
    /// </summary>
    public class FormPanel : Panel
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Page.IsPostBack)
            {
                // In addition to using onkeypress, register onkeydown with the same js function call.
                // Unfortunately onkeypress is already consumed by the javascript of any validators with ClientSideScript enabled.
                if (!String.IsNullOrWhiteSpace(DefaultButton))
                {
                    var defaultButtonControl = this.FindControl(DefaultButton);
                    if (defaultButtonControl != null)
                    {
                        this.Attributes.Add("onkeydown", "javascript: return WebForm_FireDefaultButton (event, '" + defaultButtonControl.ClientID + "')");
                    }
                }
            }
        }
    }
}
