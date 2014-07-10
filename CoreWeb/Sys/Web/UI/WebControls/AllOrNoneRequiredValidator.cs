using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoreWeb.Sys.Web.UI.WebControls
{
    /// <summary>
    /// A required field validator control that enforces a group of controls are either all populated or all not populated.
    /// </summary>
    /// <remarks>
    /// Requires jQuery.
    /// </remarks>
    [ToolboxData("<{0}:AllOrNoneRequiredValidator runat=\"server\" AssociatedControls=\"\" />")] 
    public class AllOrNoneRequiredValidator : BaseValidator
    {
        /// <summary>
        /// Comma-delimited list of Control IDs associated.
        /// </summary>
        [DefaultValue(""), Themeable(false)]
        [Description("Comma-delimited list of Control IDs associated.")]
        public string AssociatedControls { get; set; }

        /// <summary>
        /// List of associated Control IDs.
        /// </summary>
        protected IEnumerable<string> AssociatedControlsIDs
        {
            get
            {
                return AssociatedControls.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// List of associated Controls.
        /// </summary>
        protected List<Control> AssociatedControlsObjects
        {
            get
            {
                var controls = new List<Control>();
                foreach (var controlID in AssociatedControlsIDs)
                {
                    var control = this.NamingContainer.FindControl(controlID);
                    if (control != null)
                    {
                        controls.Add(control);
                    }
                }
                return controls;
            }
        }

        /// <summary>
        /// List of associated Control ClientIDs.
        /// </summary>
        protected IEnumerable<string> AssociatedControlsClientIDs
        {
            get
            {
                return AssociatedControlsObjects.Select(c => c.ClientID);
            }
        }

        /// <summary>
        /// Determines whether the Validator's properties are all valid.
        /// </summary>
        /// <returns>True/False.</returns>
        protected override bool ControlPropertiesValid()
        {
            if (String.IsNullOrWhiteSpace(AssociatedControls))
            {
                throw new HttpException("The AssociatedControls property of " + this.ID + " cannot be blank.");
            }

            if (!AssociatedControlsIDs.Any())
            {
                throw new HttpException("The AssociatedControls property of " + this.ID + " is not a comma-delimited list of at least one Control ID.");
            }

            if (AssociatedControlsIDs.Count() != AssociatedControlsObjects.Count())
            {
                string controlsNotFoundIDs = String.Join(", ", AssociatedControlsIDs.Except(AssociatedControlsObjects.Select(c => c.ID)));
                throw new HttpException("The AssociatedControls property of " + this.ID + " is not a list of Control IDs which could all be resolved to Controls in the same naming container. The following could not be found: " + controlsNotFoundIDs + ".");
            }

            return base.ControlPropertiesValid();
        }

        /// <summary>
        /// Valid iff no other related controls are populated or this one's controltovalidate is populated.
        /// </summary>
        /// <returns>True/False.</returns>
        protected override bool EvaluateIsValid()
        {
            return AssociatedControlsObjects.TrueForAll(c => String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Form[c.UniqueID]))
                || !String.IsNullOrEmpty(this.GetControlValidationValue(this.ControlToValidate));
        }

        /// <summary>
        /// Handles the PreRender event.
        /// Add custom expando attributes and scripts required for this IValidator to operate.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {            
            if (base.EnableClientScript && base.DetermineRenderUplevel())
            {
                // Set expando attributes required for client-side validation
                string thisClientID = this.ClientID;
                Type thisType = GetType();
                string assocProp = "associatedcontrols";
                string funcName = "AllOrNoneRequiredValidatorEvaluateIsValid";
                Page.ClientScript.RegisterExpandoAttribute(thisClientID, "evaluationfunction", funcName, false);
                Page.ClientScript.RegisterExpandoAttribute(thisClientID, assocProp, String.Join(",", AssociatedControlsClientIDs), false);
                
                // Evaluation/Validation function
                var sbScript = new StringBuilder(1000);
                sbScript.AppendLine("function " + funcName + "(validator) {");
                sbScript.AppendLine("    var anyOthersPopulated = false;");
                sbScript.AppendLine("    var anyOthersIDs = validator." + assocProp + ".split(',');");
                sbScript.AppendLine("    for (var i = 0; i < anyOthersIDs.length; i++) {");
                sbScript.AppendLine("        var ctrl = $('#' + anyOthersIDs[i]);");
                sbScript.AppendLine("        if (ctrl && ctrl.val().trim()) {");
                sbScript.AppendLine("            anyOthersPopulated = true; break;");
                sbScript.AppendLine("        }");
                sbScript.AppendLine("    }");
                sbScript.AppendLine("    return !anyOthersPopulated || $('#' + validator.controltovalidate).val().trim();");
                sbScript.AppendLine("}");
                Page.ClientScript.RegisterClientScriptBlock(thisType, thisType.Name + "_validator", sbScript.ToString(), true);

                // Hookup controls that should trigger this validator to refresh and should participate in the validation
                var sbHookupScript = new StringBuilder(100 * AssociatedControlsClientIDs.Count());
                sbHookupScript.AppendLine("jQuery(function($) {");
                sbHookupScript.AppendLine("    var val = document.getElementById('" + thisClientID + "');");
                foreach (var assocControlClientID in AssociatedControlsClientIDs)
                {
                    sbHookupScript.AppendLine("    ValidatorHookupControlID('" + assocControlClientID + "', val);");
                }
                sbHookupScript.AppendLine("});");
                Page.ClientScript.RegisterStartupScript(thisType, thisType.Name + "_validatorhookup_" + thisClientID, sbHookupScript.ToString(), true);
            }

            base.OnPreRender(e);
        }
    }
}