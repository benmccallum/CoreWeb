using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoreWeb.Helpers
{
    public class InlineFormValidator
    {
        /// <summary>
        /// Highlights inline errored form inputs in the page.
        /// </summary>
        /// <param name="masterPage">A reference to the master page.</param>
        /// <param name="inlineValidatorCssClass">
        /// A className used on the validation controls that identifies they should be shown inline.
        /// </param>
        public static void HighlightInvalidInlineFormElements(MasterPage masterPage, string inlineValidatorCssClass, string inlineInvalidCssClass)
        {
            // If the page was never server-side validated 
            // then no validation controls needed to be validated and we can do nothing.
            if (!masterPage.Page.IsPageValidated())
            {
                return;
            }

            // Show/hide any inline validation if the user got past the inline JS validation
            string lastControlIdValidated = "";
            foreach (BaseValidator validator in masterPage.Page.Validators)
            {
                if (!validator.CssClass.Contains(inlineValidatorCssClass))
                {
                    continue;
                }

                // Try find in master page, else look recusively in content placeholders and usercontrols
                WebControl controlToValidateObject = masterPage.FindControl(validator.ControlToValidate) as WebControl;
                if (controlToValidateObject == null)
                {
                    foreach (var cph in masterPage.FindControlsByType<ContentPlaceHolder>())
                    {
                        controlToValidateObject = cph.FindControl(validator.ControlToValidate) as WebControl;
                        if (controlToValidateObject != null) { break; }
                    }
                    foreach (var uc in masterPage.FindControlsByType<UserControl>())
                    {
                        controlToValidateObject = uc.FindControl(validator.ControlToValidate) as WebControl;
                        if (controlToValidateObject != null) { break; }
                    }
                }

                // Control to validate found, apply styling
                if (controlToValidateObject != null)
                {
                    bool showInlineErrorMessage = false;
                    bool.TryParse(controlToValidateObject.Attributes["data-showInlineErrorMessage"], out showInlineErrorMessage);

                    if (controlToValidateObject is TextBox)
                    {
                        TextBox controlToValidate = (TextBox)controlToValidateObject;

                        // Clear old validation error
                        if (controlToValidate.ID != lastControlIdValidated)
                        {
                            controlToValidate.Attributes["placeholder"] = controlToValidate.Attributes["title"];
                            controlToValidate.RemoveCssClass(inlineInvalidCssClass);
                        }

                        // Add new validation error and message if invalid 
                        if (!validator.IsValid)
                        {
                            controlToValidate.AddCssClass(inlineInvalidCssClass);
                            if (showInlineErrorMessage)
                            {
                                controlToValidate.Text = "";
                                controlToValidate.Attributes["placeholder"] = validator.ErrorMessage;
                            }
                        }
                    }

                    lastControlIdValidated = controlToValidateObject.ID;
                }
            }
        }
    }
}