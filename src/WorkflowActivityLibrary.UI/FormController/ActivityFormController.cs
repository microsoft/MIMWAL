//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityFormController.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityFormController class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Web.UI.WebControls;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Activity form controller
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityFormController
    {
        #region Declarations

        /// <summary>
        /// The activity control table which houses all the child controls.
        /// </summary>
        private readonly Table activityControlTable = new Table();

        /// <summary>
        /// The primary control table which will be returned to FIM to draw the form
        /// Houses the control table of all child controls as well as the validation error label.
        /// </summary>
        private readonly Table controlTable = new Table();

        /// <summary>
        /// The list of child activity controls on the form
        /// </summary>
        private readonly List<ActivityControl> controls = new List<ActivityControl>();

        /// <summary>
        /// The default title of the activity form
        /// </summary>
        private readonly string defaultTitle;

        /// <summary>
        /// The validation error label
        /// </summary>
        private readonly Label validationError = new Label();

        /// <summary>
        /// The overridable display name of the activity form
        /// </summary>
        private ActivityTextBox activityDisplayName;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityFormController"/> class.
        /// </summary>
        /// <param name="defaultTitle">The default title.</param>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "WebControls will be disposed when the Page is disposed")]
        public ActivityFormController(string defaultTitle)
        {
            // Assign the default title for the controller
            // This value will only be used if an activity display name text box
            // is not added to the form
            this.defaultTitle = defaultTitle;

            // Build the activity control table which will hold
            // the standard activity controls
            // This table can be built upon, if necessary, and will still
            // be presented above the validation error label
            this.activityControlTable.Width = Unit.Percentage(100.0);
            this.activityControlTable.BorderWidth = 0;
            this.activityControlTable.CellPadding = 3;

            // Build the row and table cell which will hold the activity control table
            // within the primary control table which will be returned to FIM
            TableRow activityControlRow = new TableRow();
            TableCell activityControlCell = new TableCell();
            activityControlCell.Controls.Add(this.activityControlTable);
            activityControlRow.Cells.Add(activityControlCell);

            // Assign the error CSS class to the validation error label
            // and build the row and table cell which will hold it within
            // the primary control table which will be returned to FIM
            this.validationError.CssClass = CssClass.ErrorCssClass;
            TableRow validationRow = new TableRow();
            TableCell validationCell = new TableCell { HorizontalAlign = HorizontalAlign.Center };
            validationCell.Controls.Add(this.validationError);
            validationRow.Cells.Add(validationCell);

            // Build the primary control table which will be returned to FIM
            // to draw the form
            this.controlTable.Width = Unit.Percentage(100.0);
            this.controlTable.BorderWidth = 0;
            this.controlTable.CellPadding = 3;
            this.controlTable.Rows.Add(activityControlRow);
            this.controlTable.Rows.Add(validationRow);
        }

        #region Properties

        /// <summary>
        /// Gets the primary control table which houses the control table of all child controls as well as the validation error label.
        /// </summary>
        public Table ControlTable
        {
            get
            {
                return this.controlTable;
            }
        }

        /// <summary>
        /// Gets the activity control table which houses all the child controls.
        /// </summary>
        public Table ActivityControlTable
        {
            get
            {
                return this.activityControlTable;
            }
        }

        /// <summary>
        /// Gets the list of child activity controls on the form.
        /// </summary>
        public ReadOnlyCollection<ActivityControl> Controls
        {
            get
            {
                return new ReadOnlyCollection<ActivityControl>(this.controls);
            }
        }

        /// <summary>
        /// Gets the title of the activity form.
        /// </summary>
        public string Title
        {
            get
            {
                // When a call is made for the title,
                // use the value from the activity display name text box if it has been added to the form
                // Otherwise, use the default title that was supplied to the constructor for the controller
                if (this.activityDisplayName != null && !string.IsNullOrEmpty(this.activityDisplayName.Value))
                {
                    return string.Format(CultureInfo.CurrentCulture, "{0}:  {1}", this.defaultTitle, this.activityDisplayName.Value);
                }

                return this.defaultTitle;
            }
        }

        /// <summary>
        /// Gets or sets the validation error of the form.
        /// </summary>
        public string ValidationError
        {
            get
            {
                return this.validationError.Text;
            }

            set
            {
                // Hide or present the validation error label based on whether or not
                // a value was supplied for the validation error
                this.validationError.Text = value;
                this.validationError.Visible = !string.IsNullOrEmpty(value);
            }
        }

        #endregion

        #region Methods

        #region Add Control Methods

        #region AddDisplayNameTextBox Methods

        /// <summary>
        /// Adds the display name textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddDisplayNameTextBox(string displayName)
        {
            return this.AddDisplayNameTextBox(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the display name textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddDisplayNameTextBox(string displayName, string description)
        {
            return this.AddDisplayNameTextBox(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the display name textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddDisplayNameTextBox(string displayName, bool required)
        {
            return this.AddDisplayNameTextBox(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the display name textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddDisplayNameTextBox(string displayName, string description, bool required)
        {
            return this.AddDisplayNameTextBox(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the display name textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddDisplayNameTextBox(string displayName, bool required, bool visible)
        {
            return this.AddDisplayNameTextBox(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the display name textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddDisplayNameTextBox(string displayName, string description, bool required, bool visible)
        {
            return this.AddDisplayNameTextBox(null, displayName, description, required, visible);
        }

        #endregion

        #region AddTextBox Methods

        /// <summary>
        /// Adds the textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddTextBox(string displayName)
        {
            return this.AddTextBox(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddTextBox(string displayName, string description)
        {
            return this.AddTextBox(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddTextBox(string displayName, bool required)
        {
            return this.AddTextBox(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddTextBox(string displayName, string description, bool required)
        {
            return this.AddTextBox(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddTextBox(string displayName, bool required, bool visible)
        {
            return this.AddTextBox(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        public ActivityTextBox AddTextBox(string displayName, string description, bool required, bool visible)
        {
            return this.AddTextBox(null, displayName, description, required, visible);
        }

        #endregion

        #region AddTextBoxMultiLine Methods

        /// <summary>
        /// Adds the multiline textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityTextBoxMultiline"/> object.</returns>
        public ActivityTextBoxMultiline AddTextBoxMultiline(string displayName)
        {
            return this.AddTextBoxMultiline(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the multiline textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityTextBoxMultiline"/> object.</returns>
        public ActivityTextBoxMultiline AddTextBoxMultiline(string displayName, string description)
        {
            return this.AddTextBoxMultiline(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the multiline textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBoxMultiline"/> object.</returns>
        public ActivityTextBoxMultiline AddTextBoxMultiline(string displayName, bool required)
        {
            return this.AddTextBoxMultiline(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the multiline textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBoxMultiline"/> object.</returns>
        public ActivityTextBoxMultiline AddTextBoxMultiline(string displayName, string description, bool required)
        {
            return this.AddTextBoxMultiline(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the multiline textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxMultiline"/> object.</returns>
        public ActivityTextBoxMultiline AddTextBoxMultiline(string displayName, bool required, bool visible)
        {
            return this.AddTextBoxMultiline(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the multiline textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxMultiline"/> object.</returns>
        public ActivityTextBoxMultiline AddTextBoxMultiline(string displayName, string description, bool required, bool visible)
        {
            return this.AddTextBoxMultiline(null, displayName, description, required, visible);
        }

        #endregion

        #region AddTextBoxNumeric Methods

        /// <summary>
        /// Adds the numeric textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityTextBoxNumeric"/> object.</returns>
        public ActivityTextBoxNumeric AddTextBoxNumeric(string displayName)
        {
            return this.AddTextBoxNumeric(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the numeric textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityTextBoxNumeric"/> object.</returns>
        public ActivityTextBoxNumeric AddTextBoxNumeric(string displayName, string description)
        {
            return this.AddTextBoxNumeric(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the numeric textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBoxNumeric"/> object.</returns>
        public ActivityTextBoxNumeric AddTextBoxNumeric(string displayName, bool required)
        {
            return this.AddTextBoxNumeric(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the numeric textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBoxNumeric"/> object.</returns>
        public ActivityTextBoxNumeric AddTextBoxNumeric(string displayName, string description, bool required)
        {
            return this.AddTextBoxNumeric(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the numeric textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxNumeric"/> object.</returns>
        public ActivityTextBoxNumeric AddTextBoxNumeric(string displayName, bool required, bool visible)
        {
            return this.AddTextBoxNumeric(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the numeric textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxNumeric"/> object.</returns>
        public ActivityTextBoxNumeric AddTextBoxNumeric(string displayName, string description, bool required, bool visible)
        {
            return this.AddTextBoxNumeric(null, displayName, description, required, visible);
        }

        #endregion

        #region AddTextBoxGuid Methods

        /// <summary>
        /// Adds the unique identifier textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityTextBoxGuid"/> object.</returns>
        public ActivityTextBoxGuid AddTextBoxGuid(string displayName)
        {
            return this.AddTextBoxGuid(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the unique identifier textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityTextBoxGuid"/> object.</returns>
        public ActivityTextBoxGuid AddTextBoxGuid(string displayName, string description)
        {
            return this.AddTextBoxGuid(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the unique identifier textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBoxGuid"/> object.</returns>
        public ActivityTextBoxGuid AddTextBoxGuid(string displayName, bool required)
        {
            return this.AddTextBoxGuid(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the unique identifier textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityTextBoxGuid"/> object.</returns>
        public ActivityTextBoxGuid AddTextBoxGuid(string displayName, string description, bool required)
        {
            return this.AddTextBoxGuid(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the unique identifier textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxGuid"/> object.</returns>
        public ActivityTextBoxGuid AddTextBoxGuid(string displayName, bool required, bool visible)
        {
            return this.AddTextBoxGuid(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the unique identifier textbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxGuid"/> object.</returns>
        public ActivityTextBoxGuid AddTextBoxGuid(string displayName, string description, bool required, bool visible)
        {
            return this.AddTextBoxGuid(null, displayName, description, required, visible);
        }

        #endregion

        #region AddCheckBox Methods

        /// <summary>
        /// Adds the checkbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityCheckBox"/> object.</returns>
        public ActivityCheckBox AddCheckBox(string displayName)
        {
            return this.AddCheckBox(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the checkbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityCheckBox"/> object.</returns>
        public ActivityCheckBox AddCheckBox(string displayName, string description)
        {
            return this.AddCheckBox(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the checkbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityCheckBox"/> object.</returns>
        public ActivityCheckBox AddCheckBox(string displayName, bool required)
        {
            return this.AddCheckBox(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the checkbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityCheckBox"/> object.</returns>
        public ActivityCheckBox AddCheckBox(string displayName, string description, bool required)
        {
            return this.AddCheckBox(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the checkbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityCheckBox"/> object.</returns>
        public ActivityCheckBox AddCheckBox(string displayName, bool required, bool visible)
        {
            return this.AddCheckBox(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the checkbox.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityCheckBox"/> object.</returns>
        public ActivityCheckBox AddCheckBox(string displayName, string description, bool required, bool visible)
        {
            return this.AddCheckBox(null, displayName, description, required, visible);
        }

        #endregion

        #region AddDropDownList Methods

        /// <summary>
        /// Adds the dropdown list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityDropDownList"/> object.</returns>
        public ActivityDropDownList AddDropDownList(string displayName)
        {
            return this.AddDropDownList(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the dropdown list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityDropDownList"/> object.</returns>
        public ActivityDropDownList AddDropDownList(string displayName, string description)
        {
            return this.AddDropDownList(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the dropdown list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityDropDownList"/> object.</returns>
        public ActivityDropDownList AddDropDownList(string displayName, bool required)
        {
            return this.AddDropDownList(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the dropdown list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityDropDownList"/> object.</returns>
        public ActivityDropDownList AddDropDownList(string displayName, string description, bool required)
        {
            return this.AddDropDownList(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the dropdown list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityDropDownList"/> object.</returns>
        public ActivityDropDownList AddDropDownList(string displayName, bool required, bool visible)
        {
            return this.AddDropDownList(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the dropdown list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityDropDownList"/> object.</returns>
        public ActivityDropDownList AddDropDownList(string displayName, string description, bool required, bool visible)
        {
            return this.AddDropDownList(null, displayName, description, required, visible);
        }

        #endregion

        #region AddRadioButtonList Methods

        /// <summary>
        /// Adds the radio button list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <returns>The <see cref="ActivityRadioButtonList"/> object.</returns>
        public ActivityRadioButtonList AddRadioButtonList(string displayName)
        {
            return this.AddRadioButtonList(null, displayName, null, false, true);
        }

        /// <summary>
        /// Adds the radio button list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityRadioButtonList"/> object.</returns>
        public ActivityRadioButtonList AddRadioButtonList(string displayName, string description)
        {
            return this.AddRadioButtonList(null, displayName, description, false, true);
        }

        /// <summary>
        /// Adds the radio button list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <returns>The <see cref="ActivityRadioButtonList"/> object.</returns>
        public ActivityRadioButtonList AddRadioButtonList(string displayName, bool required)
        {
            return this.AddRadioButtonList(null, displayName, null, required, true);
        }

        /// <summary>
        /// Adds the radio button list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <returns>The <see cref="ActivityRadioButtonList"/> object.</returns>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        public ActivityRadioButtonList AddRadioButtonList(string displayName, string description, bool required)
        {
            return this.AddRadioButtonList(null, displayName, description, required, true);
        }

        /// <summary>
        /// Adds the radio button list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityRadioButtonList"/> object.</returns>
        public ActivityRadioButtonList AddRadioButtonList(string displayName, bool required, bool visible)
        {
            return this.AddRadioButtonList(null, displayName, null, required, visible);
        }

        /// <summary>
        /// Adds the radio button list.
        /// </summary>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityRadioButtonList"/> object.</returns>
        public ActivityRadioButtonList AddRadioButtonList(string displayName, string description, bool required, bool visible)
        {
            return this.AddRadioButtonList(null, displayName, description, required, visible);
        }

        #endregion

        #endregion

        #region Form Management Methods

        /// <summary>
        /// Saves the current UI control settings.
        /// </summary>
        /// <returns>Returns <see cref="ActivitySettingsPartData"/>.</returns>
        public ActivitySettingsPartData PersistSettings()
        {
            // Build a new activity settings part data object
            // and loop through each control managed by the controller to add its value
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            foreach (ActivityControl control in this.controls)
            {
                // Because the value is based on the activity control type,
                // determine the type of the control before adding it to the settings part data
                if (control.GetType() == typeof(ActivityTextBox))
                {
                    data[control.ID] = ((ActivityTextBox)control).Value;
                }
                else if (control.GetType() == typeof(ActivityTextBoxMultiline))
                {
                    data[control.ID] = ((ActivityTextBoxMultiline)control).Value;
                }
                else if (control.GetType() == typeof(ActivityTextBoxNumeric))
                {
                    data[control.ID] = ((ActivityTextBoxNumeric)control).Value;
                }
                else if (control.GetType() == typeof(ActivityTextBoxGuid))
                {
                    data[control.ID] = ((ActivityTextBoxGuid)control).Value;
                }
                else if (control.GetType() == typeof(ActivityCheckBox))
                {
                    data[control.ID] = ((ActivityCheckBox)control).Value;
                }
                else if (control.GetType() == typeof(ActivityDropDownList))
                {
                    data[control.ID] = ((ActivityDropDownList)control).Value;
                }
                else if (control.GetType() == typeof(ActivityRadioButtonList))
                {
                    data[control.ID] = ((ActivityRadioButtonList)control).Value;
                }
            }

            return data;
        }

        /// <summary>
        /// Restores UI control settings stored in the data parameter. 
        /// </summary>
        /// <param name="data">Contains data about the values of UI controls.</param>
        public void RestoreSettings(ActivitySettingsPartData data)
        {
            foreach (ActivityControl control in this.controls.Where(control => data != null && data[control.ID] != null))
            {
                // If a key was identified for the control, extract the value from the settings part data
                // and assign it to the activity control based on its type
                if (control.GetType() == typeof(ActivityTextBox))
                {
                    ((ActivityTextBox)control).Value = (string)data[control.ID];
                }
                else if (control.GetType() == typeof(ActivityTextBoxMultiline))
                {
                    ((ActivityTextBoxMultiline)control).Value = (string)data[control.ID];
                }
                else if (control.GetType() == typeof(ActivityTextBoxNumeric))
                {
                    ((ActivityTextBoxNumeric)control).Value = (int)data[control.ID];
                }
                else if (control.GetType() == typeof(ActivityTextBoxGuid))
                {
                    ((ActivityTextBoxGuid)control).Value = (Guid)data[control.ID];
                }
                else if (control.GetType() == typeof(ActivityCheckBox))
                {
                    ((ActivityCheckBox)control).Value = (bool)data[control.ID];
                }
                else if (control.GetType() == typeof(ActivityDropDownList))
                {
                    ((ActivityDropDownList)control).Value = (string)data[control.ID];
                }
                else if (control.GetType() == typeof(ActivityRadioButtonList))
                {
                    ((ActivityRadioButtonList)control).Value = (string)data[control.ID];
                }
            }
        }

        /// <summary>
        /// Switches the activity UI between read only mode and edit mode.
        /// </summary>
        /// <param name="mode">Represents read only mode or edit mode.</param>
        public void SwitchMode(ActivitySettingsPartMode mode)
        {
            // Loop through each control managed by the controller and enable/disable
            // the control based on the supplied mode
            bool edit = mode == ActivitySettingsPartMode.Edit;
            foreach (ActivityControl control in this.controls)
            {
                control.Enabled = edit;
            }
        }

        /// <summary>
        /// Validates the inputs. Returns true if all of the UI controls contain valid values. Otherwise, returns false.
        /// </summary>
        /// <returns>true if all of the UI controls contain valid values. Otherwise, false.</returns>
        public bool ValidateInputs()
        {
            // Loop through each control managed by the controller to apply some basic validation logic
            foreach (ActivityControl control in this.controls)
            {
                // Determine how the control should be referenced in any validation message
                // Use the display name if it is available and, if it is missing, specify an unnamed field
                string label = !string.IsNullOrEmpty(control.DisplayName) ? control.DisplayName : ActivitySettings.UnnamedField;

                // For activity text box, text box multiline, drop down list, and radio button list,
                // perform a simple null or empty string check to determine if a value has been supplied
                bool valueMissing = false;
                if (control.GetType() == typeof(ActivityTextBox) && string.IsNullOrEmpty(((ActivityTextBox)control).Value.Trim()))
                {
                    valueMissing = true;
                }
                else if (control.GetType() == typeof(ActivityTextBoxMultiline) && string.IsNullOrEmpty(((ActivityTextBoxMultiline)control).Value.Trim()))
                {
                    valueMissing = true;
                }
                else if (control.GetType() == typeof(ActivityDropDownList) && string.IsNullOrEmpty(((ActivityDropDownList)control).Value.Trim()))
                {
                    valueMissing = true;
                }
                else if (control.GetType() == typeof(ActivityRadioButtonList) && string.IsNullOrEmpty(((ActivityRadioButtonList)control).Value.Trim()))
                {
                    valueMissing = true;
                }

                // If the control is required and no value has been supplied,
                // assign a validation error and return false
                if (control.Required && valueMissing)
                {
                    this.validationError.Text = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.RequiredFieldValidationError, label);
                    return false;
                }

                if (control.GetType() == typeof(ActivityTextBoxNumeric))
                {
                    // If the control is a numeric text box, attempt to parse the value to verify that a valid integer was supplied
                    // Also apply a required field check to make sure a value was supplied if the control is required
                    int parsedInt;
                    string text = ((ActivityTextBoxNumeric)control).TextBoxControl.Text.Trim();

                    if ((control.Required && string.IsNullOrEmpty(text)) ||
                        (!string.IsNullOrEmpty(text) && !int.TryParse(text, out parsedInt)))
                    {
                        this.validationError.Text = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.NumericFieldValidationError, label);
                        return false;
                    }
                }

                if (control.GetType() == typeof(ActivityTextBoxGuid))
                {
                    // If the control is a Guid check box, attempt to parse the value to verify that a valid Guid was supplied
                    // Also apply a required field check to make sure a value was supplied if the control is required
                    Guid parsedGuid = new Guid();
                    string text = ((ActivityTextBoxGuid)control).TextBoxControl.Text.Trim();

                    try
                    {
                        parsedGuid = new Guid(text);
                    }
                    catch (ArgumentNullException)
                    {
                    }
                    catch (FormatException)
                    {
                    }

                    if ((control.Required && string.IsNullOrEmpty(text)) ||
                        (!string.IsNullOrEmpty(text) && parsedGuid == Guid.Empty))
                    {
                        this.validationError.Text = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.GuidFieldValidationError, label);
                        return false;
                    }
                }
            }

            // If an error hasn't been detected, clear any validation error message
            // and return a true value to indicate validity
            this.validationError.Text = string.Empty;
            return true;
        }

        #endregion

        /// <summary>
        /// Adds the activity control on the form.
        /// </summary>
        /// <param name="control">The activity control to add on the form.</param>
        private void AddActivityControl(ActivityControl control)
        {
            // This method is used by all controller methods which add a specific control type
            // Only known activity control types can be added to the controller
            // If the control has not been initialized, throw an exception
            if (control == null)
            {
                throw Logger.Instance.ReportError(new ArgumentNullException("control", ActivitySettings.FormControlInitializationError));
            }

            // If a control ID has not been supplied, assign a generic identifier
            // based on the control's index in the control list
            if (string.IsNullOrEmpty(control.ID))
            {
                control.ID = string.Format(CultureInfo.InvariantCulture, "activityControl{0}", this.controls.Count);
            }

            this.activityControlTable.Rows.Add(control.TableRow);
            this.controls.Add(control);
        }

        /// <summary>
        /// Adds the display name textbox.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        private ActivityTextBox AddDisplayNameTextBox(string id, string displayName, string description, bool required, bool visible)
        {
            // A single activity display name text box can be added to the form: throw an exception if a second is added
            // A display name text box is created as a standard activity text box
            if (this.activityDisplayName != null)
            {
                throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(ActivitySettings.ActivityDisplayNameControlValidationError));
            }

            ActivityTextBox control = new ActivityTextBox(id, displayName, description, required, visible);
            this.activityDisplayName = control;
            this.AddActivityControl(control);
            return control;
        }

        /// <summary>
        /// Adds the textbox.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBox"/> object.</returns>
        private ActivityTextBox AddTextBox(string id, string displayName, string description, bool required, bool visible)
        {
            ActivityTextBox control = new ActivityTextBox(id, displayName, description, required, visible);
            this.AddActivityControl(control);
            return control;
        }

        /// <summary>
        /// Adds the multiline textbox.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxMultiline"/> object.</returns>
        private ActivityTextBoxMultiline AddTextBoxMultiline(string id, string displayName, string description, bool required, bool visible)
        {
            ActivityTextBoxMultiline control = new ActivityTextBoxMultiline(id, displayName, description, required, visible);
            this.AddActivityControl(control);
            return control;
        }

        /// <summary>
        /// Adds the numeric textbox.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxNumeric"/> object.</returns>
        private ActivityTextBoxNumeric AddTextBoxNumeric(string id, string displayName, string description, bool required, bool visible)
        {
            ActivityTextBoxNumeric control = new ActivityTextBoxNumeric(id, displayName, description, required, visible);
            this.AddActivityControl(control);
            return control;
        }

        /// <summary>
        /// Adds the unique identifier textbox.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityTextBoxGuid"/> object.</returns>
        private ActivityTextBoxGuid AddTextBoxGuid(string id, string displayName, string description, bool required, bool visible)
        {
            ActivityTextBoxGuid control = new ActivityTextBoxGuid(id, displayName, description, required, visible);
            this.AddActivityControl(control);
            return control;
        }

        /// <summary>
        /// Adds the checkbox.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityCheckBox"/> object.</returns>
        private ActivityCheckBox AddCheckBox(string id, string displayName, string description, bool required, bool visible)
        {
            ActivityCheckBox control = new ActivityCheckBox(id, displayName, description, required, visible);
            this.AddActivityControl(control);
            return control;
        }

        /// <summary>
        /// Adds the dropdown list.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityDropDownList"/> object.</returns>
        private ActivityDropDownList AddDropDownList(string id, string displayName, string description, bool required, bool visible)
        {
            ActivityDropDownList control = new ActivityDropDownList(id, displayName, description, required, visible);
            this.AddActivityControl(control);
            return control;
        }

        /// <summary>
        /// Adds the radio button list.
        /// </summary>
        /// <param name="id">The activity control identifier.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        /// <returns>The <see cref="ActivityRadioButtonList"/> object.</returns>
        private ActivityRadioButtonList AddRadioButtonList(string id, string displayName, string description, bool required, bool visible)
        {
            ActivityRadioButtonList control = new ActivityRadioButtonList(id, displayName, description, required, visible);
            this.AddActivityControl(control);
            return control;
        }

        #endregion
    }
}