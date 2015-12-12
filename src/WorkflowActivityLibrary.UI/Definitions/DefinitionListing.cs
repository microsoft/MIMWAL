//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DefinitionListing.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DefinitionListing class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Web.UI.WebControls;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;

    #endregion

    /// <summary>
    /// Task Definition UI
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class DefinitionListing
    {
        #region Declarations

        /// <summary>
        /// The hidden active checkbox which controls the visibility of the Definition row. 
        /// </summary>
        private readonly CheckBox active = new CheckBox();

        /// <summary>
        /// The "Allow Null" checkbox
        /// </summary>
        private readonly CheckBox check = new CheckBox();

        /// <summary>
        /// The left-hand side of the task equation.
        /// </summary>
        private readonly TextBox left = new TextBox();

        /// <summary>
        /// The right-hand side of the task equation.
        /// </summary>
        private readonly TextBox right = new TextBox();

        /// <summary>
        /// The selected checkbox which indicates if the Definition row is selected.
        /// </summary>
        private readonly CheckBox selected = new CheckBox();

        /// <summary>
        /// The table row that houses the definition listing controls.
        /// </summary>
        private readonly TableRow tableRow = new TableRow();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionListing"/> class.
        /// </summary>
        /// <param name="controllerId">The definitions controller identifier to which this definition listing belongs.</param>
        /// <param name="id">The base identifier of this definition listing which is then used to derive the controller identifiers of the child controls.</param>
        /// <param name="leftWidth">Width of the left-hand textbox control.</param>
        /// <param name="rightWidth">Width of the right-hand textbox control.</param>
        /// <param name="checkWidth">Width of the "Allow Null" checkbox control.</param>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "WebControls will be disposed when the Page is disposed")]
        public DefinitionListing(string controllerId, int id, int leftWidth, int rightWidth, int checkWidth)
        {
            // Publish the identifiers for the listing and controller
            this.ControllerId = controllerId;
            this.Id = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", controllerId, id);

            // By default, all new update definition listings will be hidden and inactive
            // Because the active check box is programatically managed, it will be hidden from the user
            this.tableRow.Visible = false;
            this.active.Visible = false;

            // Configure the controls for the listing
            this.selected.CssClass = CssClass.TextCssClass;
            this.left.CssClass = CssClass.TextBoxCssClass;
            this.right.CssClass = CssClass.TextBoxCssClass;
            this.check.CssClass = CssClass.TextCssClass;

            this.active.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Active", this.Id);
            this.selected.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Selected", this.Id);
            this.left.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Left", this.Id);
            this.right.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Right", this.Id);
            this.check.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Check", this.Id);

            this.selected.Width = 10;
            this.left.Width = leftWidth;
            this.right.Width = rightWidth;

            // Build the table cells to hold all mandatory controls and add them to the table row for the listing
            // Note that the active checkbox is added with the selected checkbox
            // However, because it is not visible, it will not effect the display
            TableCell selectedCell = new TableCell();
            TableCell leftCell = new TableCell();

            selectedCell.HorizontalAlign = HorizontalAlign.Center;

            selectedCell.Controls.Add(this.active);
            selectedCell.Controls.Add(this.selected);
            leftCell.Controls.Add(this.left);

            this.tableRow.Cells.Add(selectedCell);
            this.tableRow.Cells.Add(leftCell);

            // A right width of zero indicates that the right text field should be hidden
            // If the right text field should not be hidden, add it to the table
            if (rightWidth > 0)
            {
                TableCell rightCell = new TableCell();
                rightCell.Controls.Add(this.right);
                this.tableRow.Cells.Add(rightCell);
            }

            // A check width of zero indicates the check column should be hidden
            // If the checkbox should not be hidden, add it to the table
            if (checkWidth > 0)
            {
                TableCell checkCell = new TableCell();
                checkCell.Controls.Add(this.check);
                this.tableRow.Cells.Add(checkCell);
            }
        }

        #region Properties

        /// <summary>
        /// Gets the definitions controller identifier to which this definition listing belongs.
        /// </summary>
        public string ControllerId { get; private set; }

        /// <summary>
        /// Gets the table row that houses the definition listing controls.
        /// </summary>
        public TableRow TableRow
        {
            get
            {
                return this.tableRow;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Definition row is selected.
        /// </summary>
        public bool Selected
        {
            get
            {
                return this.selected.Checked;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the definition listing is active (visible).
        /// </summary>
        public bool Active
        {
            // The active property will be determined by the value of the
            // hidden Active checkbox which is programatically managed
            // When a new value is supplied for the property, it will govern
            // the value of the Active checkbox and the visibility of the listing table row
            get
            {
                return this.active.Checked;
            }

            set
            {
                this.active.Checked = value;
                this.tableRow.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the definition listing is enabled.
        /// </summary>
        public bool Enabled
        {
            // The Enabled property will be determined by and govern the
            // enabled status for each of the four user-configurable controls for the listing
            get
            {
                return this.selected.Enabled | this.left.Enabled | this.right.Enabled | this.check.Enabled;
            }

            set
            {
                this.selected.Enabled = value;
                this.left.Enabled = value;
                this.right.Enabled = value;
                this.check.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        public Definition Definition
        {
            get
            {
                // Verify that the source and target values have been supplied by the user
                // and return the Update Definition which represents all values
                // If source or target are missing, return null
                if (!this.active.Checked || string.IsNullOrEmpty(this.left.Text) || string.IsNullOrEmpty(this.right.Text))
                {
                    return null;
                }

                return new Definition(this.left.Text, this.right.Text, this.check.Checked);
            }

            set
            {
                if (value == null)
                {
                    throw Logger.Instance.ReportError(new ArgumentNullException("value"));
                }

                // When an Update Definition is supplied, use the associated properties
                // to set the values for the source, target, and allow null controls
                // Also, make the listing active and visible
                this.left.Text = value.Left;
                this.right.Text = value.Right;
                this.check.Checked = value.Check;
                this.Active = true;
            }
        }

        /// <summary>
        /// Gets or sets the state of all user-visible controls associated with the definition listing.
        /// </summary>
        public DefinitionListingState State
        {
            get
            {
                // Return the state of all user-visible controls associated with the listing
                return !this.active.Checked ? null : new DefinitionListingState(this.selected.Checked, this.left.Text, this.right.Text, this.check.Checked);
            }

            set
            {
                if (value == null)
                {
                    throw Logger.Instance.ReportError(new ArgumentNullException("value"));
                }

                // Restore the state of all user-visible controls
                this.selected.Checked = value.Selected;
                this.Definition = new Definition(value.Left, value.Right, value.Check);
            }
        }

        /// <summary>
        /// Gets or sets the base identifier of this definition listing which is then used to derive the controller identifiers of the child controls.
        /// </summary>
        private string Id { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Clears all controls of the definition listing.
        /// </summary>
        public void Clear()
        {
            this.left.Text = string.Empty;
            this.right.Text = string.Empty;
            this.check.Checked = false;
            this.selected.Checked = false;
            this.active.Checked = false;
            this.tableRow.Visible = false;
        }

        #endregion
    }
}