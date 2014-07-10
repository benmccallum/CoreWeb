using System;
using System.Web;
using System.Web.UI;

namespace CoreWeb.Sys.Web.UI.WebControls
{
    /// <summary>
    /// A validator for required checkbox.
    /// </summary>
    public class CheckBoxValidator : System.Web.UI.WebControls.BaseValidator
    {
        private readonly string evaluationFunctionName = "validateCheckboxRequired";

        private System.Web.UI.WebControls.CheckBox checkBoxToValidate;
        /// <summary>
        /// CheckBox to validate.
        /// </summary>
        private System.Web.UI.WebControls.CheckBox CheckBoxToValidate
        {
            get
            {
                if (checkBoxToValidate == null)
                {
                    checkBoxToValidate = this.NamingContainer.FindControl(this.ControlToValidate) as System.Web.UI.WebControls.CheckBox;
                }
                return checkBoxToValidate;
            }
        }

        /// <summary>
        /// The checked state to validate the checkbox matches.
        /// Default: true.
        /// </summary>
        public bool DesiredCheckedState { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CheckBoxValidator()
        {
            this.DesiredCheckedState = true;
        }

        /// <summary>
        /// Is the control valid given its properties.
        /// </summary>
        /// <returns>true/false.</returns>
        protected override bool ControlPropertiesValid()
        {
            if (this.ControlToValidate.Length == 0)
            {
                throw new HttpException(string.Format("The ControlToValidate property of '{0}' is required.", this.ID));
            }

            if (this.CheckBoxToValidate == null)
            {
                throw new HttpException(string.Format("This control can only validate CheckBox."));
            }

            return true;
        }

        /// <summary>
        /// Is valid if checkbox.checked is the same value as required CheckedState.
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool EvaluateIsValid()
        {
            return CheckBoxToValidate.Checked == DesiredCheckedState;
        }

        /// <summary>
        /// Event handler for control's PreRender event.
        /// Renders js in page for client-side validation if possible.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.Visible && this.Enabled)
            {
                ClientScriptManager cs = this.Page.ClientScript;
                if (this.DetermineRenderUplevel() && this.EnableClientScript)
                {
                    cs.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", evaluationFunctionName, false);
                }
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType().FullName))
                {
                    cs.RegisterClientScriptBlock(this.GetType(), this.GetType().FullName, GetClientSideScript(), true);
                }
            }
        }
        
        /// <summary>
        /// Generates some client JScript for validation client-side.
        /// </summary>
        /// <returns>Some JS without a script tag.</returns>
        private string GetClientSideScript()
        {
            return "function " + evaluationFunctionName + "(sender){return document.getElementById(sender.controltovalidate).checked == " + (DesiredCheckedState ? "true" : "false") + ";}";
        }
    }
}