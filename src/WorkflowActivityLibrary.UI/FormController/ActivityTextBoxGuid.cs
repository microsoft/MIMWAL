//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityTextBoxGuid.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityTextBoxGuid class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.UI.WebControls;

    #endregion

    /// <summary>
    /// Activity unique identifier textbox control
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityTextBoxGuid : ActivityControl
    {
        #region Declarations

        /// <summary>
        /// The textbox control
        /// </summary>
        private readonly TextBox textBoxControl = new TextBox();
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTextBoxGuid"/> class.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        public ActivityTextBoxGuid(string id, string displayName, string description, bool required, bool visible)
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
        /// Gets or sets the value of the textbox.
        /// </summary>
        public Guid Value
        {
            get
            {
                // When evaluating the value for a Guid text box control,
                // make sure the value specified can be parsed as a Guid and
                // return an empty Guid if it cannot
                Guid parsed = new Guid();
                try
                {
                    parsed = new Guid(this.textBoxControl.Text);
                }
                catch (ArgumentNullException)
                {
                }
                catch (FormatException)
                {
                }
                
                return parsed;
            }

            set
            {
                // Only assign a value to the text box control if the supplied value is not an empty Guid
                // This prevents the form from displaying an empty Guid when an optional property
                // was not assigned
                this.textBoxControl.Text = value != Guid.Empty ? value.ToString() : string.Empty;
            }
        }
        
        #endregion
    }
}