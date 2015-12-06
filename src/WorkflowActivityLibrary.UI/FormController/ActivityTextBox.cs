//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityTextBox.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityTextBox class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System.Diagnostics.CodeAnalysis;
    using System.Web.UI.WebControls;

    #endregion

    /// <summary>
    /// Activity textbox control
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityTextBox : ActivityControl
    {
        #region Declarations

        /// <summary>
        /// The textbox control
        /// </summary>
        private readonly TextBox textBoxControl = new TextBox();
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTextBox"/> class.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        public ActivityTextBox(string id, string displayName, string description, bool required, bool visible)
            : base(null, displayName, description, required, visible)
        {
            this.textBoxControl.TextMode = TextBoxMode.SingleLine;
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
        /// Gets or sets the text of the textbox.
        /// </summary>
        public string Value
        {
            get
            {
                return this.textBoxControl.Text;
            }

            set
            {
                this.textBoxControl.Text = value;
            }
        }
        
        #endregion
    }
}