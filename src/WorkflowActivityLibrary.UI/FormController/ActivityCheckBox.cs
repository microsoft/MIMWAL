//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityCheckBox.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityCheckBox class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System.Diagnostics.CodeAnalysis;
    using System.Web.UI.WebControls;

    #endregion

    /// <summary>
    /// Activity checkbox control
    /// </summary>
    [SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityCheckBox : ActivityControl
    {
        #region Declarations

        /// <summary>
        /// The checkbox control
        /// </summary>
        private readonly CheckBox checkBoxControl = new CheckBox();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityCheckBox"/> class.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        public ActivityCheckBox(string id, string displayName, string description, bool required, bool visible)
            : base(null, displayName, description, required, visible)
        {
            this.checkBoxControl.CssClass = CssClass.TextCssClass;
            this.Control = this.checkBoxControl;
            this.Control.ID = id;
        }

        #region Properties

        /// <summary>
        /// Gets the CheckBox control.
        /// </summary>
        public CheckBox CheckBoxControl
        {
            get
            {
                return this.checkBoxControl;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control is checked.
        /// </summary>
        public bool Value
        {
            get
            {
                return this.checkBoxControl.Checked;
            }

            set
            {
                this.checkBoxControl.Checked = value;
            }
        }

        #endregion
    }
}