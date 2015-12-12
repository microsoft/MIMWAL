//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityControl.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActivityControl class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.UI.WebControls;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;

    #endregion

    /// <summary>
    /// Base class for any activity controls
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class ActivityControl
    {
        #region Declarations

        /// <summary>
        /// The control table cell
        /// </summary>
        private readonly TableCell controlCell = new TableCell();

        /// <summary>
        /// The control description
        /// </summary>
        private readonly Label description = new Label();

        /// <summary>
        /// The control display name
        /// </summary>
        private readonly Label displayName = new Label();

        /// <summary>
        /// Indicated whether the control is required
        /// </summary>
        private readonly Label required = new Label();

        /// <summary>
        /// The control table row
        /// </summary>
        private readonly TableRow tableRow = new TableRow();

        /// <summary>
        /// The underlying web control
        /// </summary>
        private WebControl control;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityControl"/> class.
        /// </summary>
        /// <param name="control">The underlying web control.</param>
        /// <param name="displayName">The display name of the control.</param>
        /// <param name="description">The description of the control.</param>
        /// <param name="required">if set to true, the control is required. Otherwise not required.</param>
        /// <param name="visible">if set to true, the control is visible. Otherwise not visible.</param>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "WebControls will be disposed when the Page is disposed")]
        public ActivityControl(WebControl control, string displayName, string description, bool required, bool visible)
        {
            Logger.Instance.WriteMethodEntry("DisplayName: '{0}'. Description: '{1}'. Required: '{2}'. Visible: '{3}'.", displayName, description, required, visible);

            try
            {
                // Set the properties for the display name, description,
                // and required labels
                this.displayName.CssClass = CssClass.TextCssClass;
                this.description.CssClass = CssClass.TextCssClass;
                this.required.CssClass = CssClass.ErrorCssClass;

                this.displayName.Width = 300;
                this.description.Width = 300;

                this.displayName.Font.Bold = true;
                this.displayName.Text = displayName;
                this.description.Text = description;

                // If the control should be required, assign a * character for the required label
                // Hide the description label if a description value was not supplied
                if (required)
                {
                    this.required.Text = ActivitySettings.RequiredIndicator;
                }

                this.description.Visible = !string.IsNullOrEmpty(description);

                // Assign the visible property by hiding or presenting the table row
                this.tableRow.Visible = visible;

                // Build the text and required table cells
                // Both the display name and description labels will be added
                // to the text cell
                TableCell textCell = new TableCell();
                textCell.Controls.Add(this.displayName);
                textCell.Controls.Add(this.description);
                textCell.Width = Unit.Percentage(50.0);
                textCell.VerticalAlign = VerticalAlign.Middle;

                TableCell requiredCell = new TableCell();
                requiredCell.Controls.Add(this.required);
                requiredCell.VerticalAlign = VerticalAlign.Middle;

                // If a control was passed to the constructor,
                // assume that it was built to specifiation and should simply
                // be added to the control cell without modification
                this.controlCell.VerticalAlign = VerticalAlign.Middle;
                if (control != null)
                {
                    this.control = control;
                    this.controlCell.Controls.Add(this.control);
                }

                // Add the cells to the table row
                this.tableRow.Cells.Add(textCell);
                this.tableRow.Cells.Add(requiredCell);
                this.tableRow.Cells.Add(this.controlCell);
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        #region Properties

        /// <summary>
        /// Gets the table row of the control.
        /// </summary>
        /// <value>
        /// The table row.
        /// </value>
        public TableRow TableRow
        {
            get
            {
                return this.tableRow;
            }
        }

        /// <summary>
        /// Gets or sets the web control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
        public WebControl Control
        {
            get
            {
                return this.control;
            }

            set
            {
                // The control will be set by the contructors
                // for activity control classes which inherit this class
                // Set a CSS class, clear the control cell to ensure nothing else is included,
                // and add the control
                this.control = value;
                if (value != null)
                {
                    this.controlCell.Controls.Clear();
                    this.controlCell.Controls.Add(this.control);
                }
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the control.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID", Justification = "Using same casing as underling WebControl")]
        public string ID
        {
            get
            {
                return this.control.ID;
            }

            set
            {
                this.control.ID = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.control.Enabled;
            }

            set
            {
                this.control.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the display name of the control.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return this.displayName.Text;
            }

            set
            {
                this.displayName.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of the control.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description.Text;
            }

            set
            {
                // If the description is null or empty,
                // hide the description label so it doesn't impact the formatting
                // of the text cell
                this.description.Text = value;
                this.description.Visible = !string.IsNullOrEmpty(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is required.
        /// </summary>
        public bool Required
        {
            // The required property is determined by the text
            // value for the required label
            // A * character indicates that the control is required,
            // and a blank value indicates that it is optional
            get
            {
                return this.required.Text.Equals(ActivitySettings.RequiredIndicator, StringComparison.OrdinalIgnoreCase);
            }

            set
            {
                this.required.Text = value ? ActivitySettings.RequiredIndicator : string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                return this.tableRow.Visible;
            }

            set
            {
                this.tableRow.Visible = value;
            }
        }

        #endregion
    }
}