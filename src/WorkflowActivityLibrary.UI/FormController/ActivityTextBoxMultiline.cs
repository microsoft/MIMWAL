//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityTextBoxMultiline.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityTextBoxMultiline class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Web.UI.WebControls;

    #endregion

    /// <summary>
    /// Activity multiline textbox control
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityTextBoxMultiline : ActivityControl
    {
        #region Declarations

        /// <summary>
        /// The textbox control
        /// </summary>
        private readonly TextBox textBoxControl = new TextBox();
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTextBoxMultiline"/> class.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        public ActivityTextBoxMultiline(string id, string displayName, string description, bool required, bool visible)
            : base(null, displayName, description, required, visible)
        {
            this.textBoxControl.TextMode = TextBoxMode.MultiLine;
            this.textBoxControl.Width = 300;
            this.textBoxControl.Rows = 3;
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

        /// <summary>
        /// Gets or sets the text lines of the textbox.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Reviewed. It's a non-concern here.")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. It's a non-concern here.")]
        public List<string> Values
        {
            get
            {
                // Build a new array list which contains a value
                // for each line of the multiline text box control
                return this.textBoxControl.Text.Split('\n').Where(s => !string.IsNullOrEmpty(s.Trim())).Select(s => s.Replace("\r", string.Empty)).ToList();
            }

            set
            {
                // For each value in the array list,
                // add a line to the multiline text box control
                if (value != null)
                {
                    foreach (string s in value)
                    {
                        this.textBoxControl.Text += string.Format(CultureInfo.CurrentCulture, "{0}\n", s);
                    }
                }
                else
                {
                    this.textBoxControl.Text = null;
                }
            }
        }
        
        #endregion
    }
}