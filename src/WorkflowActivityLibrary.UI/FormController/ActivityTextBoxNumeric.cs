//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityTextBoxNumeric.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityTextBoxNumeric class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Web.UI.WebControls;

    #endregion

    /// <summary>
    /// Activity numeric textbox control
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityTextBoxNumeric : ActivityControl
    {
        #region Declarations

        /// <summary>
        /// The textbox control
        /// </summary>
        private readonly TextBox textBoxControl = new TextBox();

        /// <summary>
        /// The display zero value indicator
        /// </summary>
        private bool displayZeroValue;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTextBoxNumeric"/> class.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        public ActivityTextBoxNumeric(string id, string displayName, string description, bool required, bool visible)
            : base(null, displayName, description, required, visible)
        {
            this.textBoxControl.TextMode = TextBoxMode.SingleLine;
            this.textBoxControl.Width = 100;
            this.textBoxControl.CssClass = CssClass.TextBoxCssClass;
            this.Control = this.textBoxControl;
            this.Control.ID = id;
        }

        #region Properties

        /// <summary>
        /// Gets the textbox control.
        /// </summary>
        public TextBox TextBoxControl
        {
            get
            {
                return this.textBoxControl;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display zero value or not.
        /// </summary>
        public bool DisplayZeroValue
        {
            get
            {
                return this.displayZeroValue;
            }

            set
            {
                this.displayZeroValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the numeric value of the textbox.
        /// </summary>
        public int Value
        {
            get
            {
                // When evaluating the value specified for the text box control, make sure it is numeric
                // and return zero if it is not
                int parsed;
                if (!int.TryParse(this.textBoxControl.Text, out parsed))
                {
                    // TODO: Trace 
                }

                return parsed;
            }

            set
            {
                // When setting a value for the control, only display a zero when the control is configured to do so
                if (this.displayZeroValue || value != 0)
                {
                    this.textBoxControl.Text = value.ToString(CultureInfo.CurrentCulture);
                }
                else
                {
                    this.textBoxControl.Text = string.Empty;
                }
            }
        }

        #endregion
    }
}