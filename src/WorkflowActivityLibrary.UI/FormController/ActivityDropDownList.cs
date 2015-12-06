//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityDropDownList.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityDropDownList class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System.Diagnostics.CodeAnalysis;
    using System.Web.UI.WebControls;

    #endregion

    /// <summary>
    /// Activity dropdown list control
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityDropDownList : ActivityControl
    {
        #region Declarations

        /// <summary>
        /// The drop down list control
        /// </summary>
        private readonly DropDownList dropDownListControl = new DropDownList();
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDropDownList"/> class.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        public ActivityDropDownList(string id, string displayName, string description, bool required, bool visible)
            : base(null, displayName, description, required, visible)
        {
            this.dropDownListControl.CssClass = CssClass.TextBoxCssClass;
            this.Control = this.dropDownListControl;
            this.Control.ID = id;
        }

        #region Properties

        /// <summary>
        /// Gets the drop down list control.
        /// </summary>
        public DropDownList DropDownListControl
        {
            get
            {
                return this.dropDownListControl;
            }
        }

        /// <summary>
        /// Gets or sets the selected value of the dropdown list control.
        /// </summary>
        public string Value
        {
            get
            {
                return this.dropDownListControl.SelectedValue;
            }

            set
            {
                this.dropDownListControl.SelectedValue = value;
            }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Adds the list item to the dropdown list.
        /// </summary>
        /// <param name="displayName">The display name of the list item.</param>
        /// <param name="value">The value of the list item.</param>
        public void AddListItem(string displayName, string value)
        {
            this.dropDownListControl.Items.Add(new ListItem(displayName, value));
        }
    
        #endregion
    }
}