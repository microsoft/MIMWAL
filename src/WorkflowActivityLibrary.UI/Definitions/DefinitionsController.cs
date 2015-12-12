//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DefinitionsController.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DefinitionsController class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.UI.WebControls;
    using Microsoft.IdentityManagement.WebUI.Controls;

    #endregion

    /// <summary>
    /// Definitions Controller
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "WebControls will be disposed when the Page is disposed")]
    public class DefinitionsController
    {
        #region Declarations

        /// <summary>
        /// The add link button
        /// </summary>
        private readonly LinkButton add = new LinkButton();

        /// <summary>
        /// The label of the "Allow Null" checkbox
        /// </summary>
        private readonly Label checkLabel = new Label();

        /// <summary>
        /// The definition listings
        /// </summary>
        private readonly List<DefinitionListing> definitionListings = new List<DefinitionListing>();

        /// <summary>
        /// The delete link button
        /// </summary>
        private readonly LinkButton delete = new LinkButton();

        /// <summary>
        /// The description of the definitions controller
        /// </summary>
        private readonly Label description = new Label();

        /// <summary>
        /// The display name of the definitions controller
        /// </summary>
        private readonly Label displayName = new Label();

        /// <summary>
        /// The header row of the definitions controller
        /// </summary>
        private readonly TableRow headerRow = new TableRow();

        /// <summary>
        /// The label of the left-hand side of the definition
        /// </summary>
        private readonly Label leftLabel = new Label();

        /// <summary>
        /// The move down link button
        /// </summary>
        private readonly LinkButton moveDown = new LinkButton();

        /// <summary>
        /// The move up link button
        /// </summary>
        private readonly LinkButton moveUp = new LinkButton();

        /// <summary>
        /// The label of the right-hand side of the definition
        /// </summary>
        private readonly Label rightLabel = new Label();

        /// <summary>
        /// The table row that houses all the child controls of the definition controller
        /// </summary>
        private readonly TableRow tableRow = new TableRow();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionsController"/> class.
        /// </summary>
        /// <param name="id">The identifier of the definitions controller.</param>
        /// <param name="leftWidth">Width of the left-hand side of the definition.</param>
        /// <param name="rightWidth">Width of the right-hand side of the definition.</param>
        /// <param name="checkWidth">Width of the "Allow Null" checkbox label.</param>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "WebControls will be disposed when the Page is disposed")]
        public DefinitionsController(string id, int leftWidth, int rightWidth, int checkWidth)
        {
            // Publish the supplied identifier for the definitions controller
            this.Id = id;

            // Build the display name and description labels which will be presented in the
            // header row for the definitions controller
            // A padding label is added to provide seperation from standard controls on the form
            // The display name and description labels will be hidden until values are supplied to them via properties
            Label padding = new Label();
            padding.Font.Size = 2;
            this.displayName.CssClass = CssClass.TextCssClass;
            this.description.CssClass = CssClass.TextCssClass;
            this.displayName.Font.Bold = true;

            padding.Width = 600;
            this.displayName.Width = 600;
            this.description.Width = 600;

            this.displayName.Visible = false;
            this.description.Visible = false;

            // Build the Add, Delete, Move Up, and Move Down link buttons and add event handlers
            this.add.Text = ActivitySettings.LinkButtonAdd;
            this.delete.Text = ActivitySettings.LinkButtonDelete;
            this.moveUp.Text = ActivitySettings.LinkButtonMoveUp;
            this.moveDown.Text = ActivitySettings.LinkButtonMoveDown;

            this.add.CssClass = CssClass.TextCssClass;
            this.delete.CssClass = CssClass.TextCssClass;
            this.moveUp.CssClass = CssClass.TextCssClass;
            this.moveDown.CssClass = CssClass.TextCssClass;

            this.add.Font.Bold = true;
            this.delete.Font.Bold = true;
            this.moveUp.Font.Bold = true;
            this.moveDown.Font.Bold = true;

            this.add.Click += this.Add_Click;
            this.delete.Click += this.Delete_Click;
            this.moveUp.Click += this.MoveUp_Click;
            this.moveDown.Click += this.MoveDown_Click;

            // Build the command table
            TableRow commandRow = new TableRow();
            Table commandTable = new Table { BorderWidth = 0, CellPadding = 0 };
            commandTable.Rows.Add(commandRow);

            TableCell addCell = new TableCell { Height = 20, Width = 30, VerticalAlign = VerticalAlign.Bottom };
            addCell.Controls.Add(this.add);
            commandRow.Cells.Add(addCell);

            TableCell deleteCell = new TableCell { Height = 20, Width = 45, VerticalAlign = VerticalAlign.Bottom };
            deleteCell.Controls.Add(this.delete);
            commandRow.Cells.Add(deleteCell);

            TableCell moveUpCell = new TableCell { Height = 20, Width = 58, VerticalAlign = VerticalAlign.Bottom };
            moveUpCell.Controls.Add(this.moveUp);
            commandRow.Cells.Add(moveUpCell);

            TableCell moveDownCell = new TableCell { Height = 20, Width = 70, VerticalAlign = VerticalAlign.Bottom };
            moveDownCell.Controls.Add(this.moveDown);
            commandRow.Cells.Add(moveDownCell);

            // Build the controls which will be added to the header 
            // for the update definitions table
            this.leftLabel.CssClass = CssClass.TextCssClass;
            this.rightLabel.CssClass = CssClass.TextCssClass;
            this.checkLabel.CssClass = CssClass.TextCssClass;

            this.leftLabel.Width = leftWidth;
            this.rightLabel.Width = rightWidth;
            this.checkLabel.Width = checkWidth;

            // Build the table cells and rows which will constitute the command bar (add/delete)
            // and header for the update definitions table
            // Add the controls to the appropriate cells and the cells to the appropriate rows
            // Add both rows to the update definitions table
            TableCell commandCell = new TableCell();
            TableCell leftCell = new TableCell();
            TableCell rightCell = new TableCell();
            TableCell checkCell = new TableCell();

            commandCell.ColumnSpan = 3;

            TableRow commands = new TableRow();
            TableRow definitionsHeader = new TableRow();

            commandCell.Controls.Add(commandTable);
            leftCell.Controls.Add(this.leftLabel);
            rightCell.Controls.Add(this.rightLabel);
            checkCell.Controls.Add(this.checkLabel);

            commands.Cells.Add(commandCell);
            definitionsHeader.Cells.Add(new TableCell());
            definitionsHeader.Cells.Add(leftCell);
            definitionsHeader.Cells.Add(rightCell);
            definitionsHeader.Cells.Add(checkCell);

            TableCell textCell = new TableCell { ColumnSpan = 3 };
            textCell.Controls.Add(padding);
            textCell.Controls.Add(this.displayName);
            textCell.Controls.Add(this.description);
            this.headerRow.Cells.Add(textCell);

            // Build a new definitions table and add it to the table row which is to be returned
            // This table row will be added to the form generated by the form controller and, consequently,
            // the cell which holds the table must be configured to span the standard 3 columns for that form
            Table definitionsTable = new Table { BorderWidth = 0, CellPadding = 2 };

            TableCell definitionsCell = new TableCell { ColumnSpan = 3 };
            definitionsCell.Controls.Add(definitionsTable);

            this.tableRow.Cells.Add(definitionsCell);

            definitionsTable.Rows.Add(commands);
            definitionsTable.Rows.Add(definitionsHeader);

            // Create 1000 new update definitions listings for the form
            // Because the constructor for this class is called multiple times during user input,
            // all controls must be pre-added and there is no way to support the dynamic adding of update
            // definition listings via the add button
            // Consequently, all listings will be added in a hidden state, except for one, and the add/delete
            // buttons will be used to manage the visibility of the associated table rows
            for (int i = 0; i < 1000; i++)
            {
                DefinitionListing listing = new DefinitionListing(id, this.definitionListings.Count, leftWidth, rightWidth, checkWidth);
                this.definitionListings.Add(listing);
                definitionsTable.Rows.Add(listing.TableRow);
            }

            this.definitionListings[0].Active = true;
        }

        #region Properties

        /// <summary>
        /// Gets the definition listings.
        /// </summary>
        public List<DefinitionListing> DefinitionListings
        {
            get
            {
                return this.definitionListings;
            }
        }

        /// <summary>
        /// Gets the header row of the definition controller.
        /// </summary>
        public TableRow HeaderRow
        {
            get
            {
                return this.headerRow;
            }
        }

        /// <summary>
        /// Gets the table row of the definition controller.
        /// </summary>
        public TableRow TableRow
        {
            get
            {
                return this.tableRow;
            }
        }

        /// <summary>
        /// Gets or sets the display name of the definition controller.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return this.displayName.Text;
            }

            set
            {
                // If the display name is null or empty,
                // hide the display name label so it doesn't impact the formatting
                // of the text cell
                this.displayName.Text = value;
                this.displayName.Visible = !string.IsNullOrEmpty(value);
            }
        }

        /// <summary>
        /// Gets or sets the description  of the definitions controller.
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
        /// Gets or sets the label of the left-hand side of the definition.
        /// </summary>
        public string LeftHeader
        {
            get
            {
                return this.leftLabel.Text;
            }

            set
            {
                this.leftLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the label of the right-hand side of the definition.
        /// </summary>
        public string RightHeader
        {
            get
            {
                return this.rightLabel.Text;
            }

            set
            {
                this.rightLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the label of the "Allow Null" checkbox.
        /// </summary>
        public string CheckHeader
        {
            get
            {
                return this.checkLabel.Text;
            }

            set
            {
                this.checkLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier of this definition controller.
        /// </summary>
        private string Id { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Saves the current UI control settings.
        /// Converts the definition listings (web controls) to settings part data entries
        /// If an existing settings part data object (presumably created by the form controller) was supplied,
        /// all settings part data for the definition listings will be added to the existing data
        /// </summary>
        /// <param name="data">The <see cref="ActivitySettingsPartData"/> data created by the form controller.</param>
        /// <returns>Returns <see cref="ActivitySettingsPartData"/>.</returns>
        public ActivitySettingsPartData PersistSettings(ActivitySettingsPartData data)
        {
            DefinitionsConverter converter = new DefinitionsConverter(this.definitionListings, data, this.Id);
            return converter.SettingsPartData;
        }

        /// <summary>
        /// Restores UI control settings stored in the data parameter. 
        /// Converts the activity settings part data to a list of definitions
        /// which can be applied to the available definition listings (web controls)
        /// Assigning a definition to a listing will make it active and visible
        /// </summary>
        /// <param name="data">Contains data about the values of UI controls.</param>
        public void RestoreSettings(ActivitySettingsPartData data)
        {
            this.ResetListings();
            DefinitionsConverter converter = new DefinitionsConverter(data, this.Id);
            for (int i = 0; i < converter.Definitions.Count; i++)
            {
                this.definitionListings[i].Definition = converter.Definitions[i];
            }
        }

        /// <summary>
        /// This method initializes definitions listing with data from the definitions hash table. 
        /// </summary>
        /// <param name="definitionsTable">The definitions hash table.</param>
        public void LoadActivitySettings(Hashtable definitionsTable)
        {
            // Convert the update definitions hashtable to a list of update definitions
            // which can be applied to the available update definition listings (web controls)
            // Assigning an update definition to a listing will make it active and visible
            this.ResetListings();
            DefinitionsConverter converter = new DefinitionsConverter(definitionsTable);
            for (int i = 0; i < converter.Definitions.Count; i++)
            {
                this.definitionListings[i].Definition = converter.Definitions[i];
            }
        }

        /// <summary>
        /// Switches the activity UI between read only mode and edit mode.
        /// </summary>
        /// <param name="mode">Represents read only mode or edit mode.</param>
        public void SwitchMode(ActivitySettingsPartMode mode)
        {
            // Enable or disable each definition listing based on the supplied mode
            // and additionally enable/disable all command buttons
            bool edit = mode == ActivitySettingsPartMode.Edit;
            foreach (DefinitionListing listing in this.definitionListings)
            {
                listing.Enabled = edit;
            }

            this.add.Enabled = edit;
            this.delete.Enabled = edit;
            this.moveUp.Enabled = edit;
            this.moveDown.Enabled = edit;
        }

        /// <summary>
        /// Handles the Click event of the Add control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Add_Click(object sender, EventArgs e)
        {
            // When a user clicks the Add button, they will not actually be adding a new definition listing
            // to the form but will instead be unhiding an existing listing
            // This is necessary because FIM calls the constructor multiple times during a form's session, which means
            // that all controls which will be used during the session must be created by the constructor... true dynamic forms are not possible
            // 
            // To ensure a new definition listing is exposed at the end of the currently exposed list,
            // identify the last index of an active listing to determine the next value
            // Because the Delete button may be used to clear and hide a listing in the middle of the table, this is necessary
            // to ensure listings are not added to seemingly random places
            // A maximum of 100 listings can be created for the activity
            int nextIndex = 0;
            for (int i = 0; i < this.definitionListings.Count; i++)
            {
                if (this.definitionListings[i].Active)
                {
                    nextIndex = i + 1;
                }
            }

            if (nextIndex < this.definitionListings.Count)
            {
                this.definitionListings[nextIndex].Active = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the Delete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Delete_Click(object sender, EventArgs e)
        {
            // When the Delete button is clicked, identify all definition listings
            // which have been selected by the user and clear them
            foreach (DefinitionListing listing in this.definitionListings.Where(listing => listing.Selected))
            {
                listing.Clear();
            }

            // Make sure at least one listing is always active
            bool active = this.definitionListings.Any(listing => listing.Active);

            if (!active)
            {
                this.definitionListings[0].Active = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the MoveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MoveUp_Click(object sender, EventArgs e)
        {
            // When the Move Up button is clicked, the current active definition listings need to be resorted
            // Build a dictionary to hold the sorted listings and load the current order
            Dictionary<int, DefinitionListingState> sorted = new Dictionary<int, DefinitionListingState>();
            List<DefinitionListingState> startingOrder = (from listing in this.definitionListings where listing.Active select listing.State).ToList();

            // Loop through each selected definition listing and move
            // it up in the sorted list
            for (int i = 0; i < startingOrder.Count; i++)
            {
                if (startingOrder[i].Selected)
                {
                    // A listing cannot be moved up if it is already at the top of the list
                    // or is part of a selected block of listings which is at the top of the list
                    if (i == 0 || sorted.ContainsKey(i - 1))
                    {
                        sorted.Add(i, startingOrder[i]);
                    }
                    else
                    {
                        sorted.Add(i - 1, startingOrder[i]);
                    }
                }
            }

            // Loop through each unselected listing and add
            // it to the next available index in the sorted list
            foreach (DefinitionListingState state in startingOrder.Where(state => !state.Selected))
            {
                for (int i = 0; i < startingOrder.Count; i++)
                {
                    if (!sorted.ContainsKey(i))
                    {
                        sorted.Add(i, state);
                        break;
                    }
                }
            }

            // Create a sorted enumerable list based on the keys for the sorted dictionary
            List<int> sortedList = sorted.Keys.ToList();
            sortedList.Sort();

            // Clear all of the listings and re-add the active listings in the new sorted order
            this.ResetListings();
            for (int i = 0; i < sortedList.Count; i++)
            {
                this.definitionListings[i].State = sorted[sortedList[i]];
            }
        }

        /// <summary>
        /// Handles the Click event of the MoveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MoveDown_Click(object sender, EventArgs e)
        {
            // When the Move Down button is clicked, the current active rename condition listings need to be resorted
            // Build a dictionary to hold the sorted listings and load the current order
            Dictionary<int, DefinitionListingState> sorted = new Dictionary<int, DefinitionListingState>();
            List<DefinitionListingState> startingOrder = (from listing in this.definitionListings where listing.Active select listing.State).ToList();

            // Loop through each selected definition listing in reverse and move
            // it down in the sorted list
            for (int i = startingOrder.Count - 1; i >= 0; i--)
            {
                if (startingOrder[i].Selected)
                {
                    // A listing cannot be moved down if it is already at the bottom of the list
                    // or is part of a selected block of listings which is at the bottom of the list
                    if (i == startingOrder.Count - 1 || sorted.ContainsKey(i + 1))
                    {
                        sorted.Add(i, startingOrder[i]);
                    }
                    else
                    {
                        sorted.Add(i + 1, startingOrder[i]);
                    }
                }
            }

            // Loop through each unselected definition listing and add
            // it to the next available index in the sorted list
            foreach (DefinitionListingState state in startingOrder.Where(state => !state.Selected))
            {
                for (int i = 0; i < startingOrder.Count; i++)
                {
                    if (!sorted.ContainsKey(i))
                    {
                        sorted.Add(i, state);
                        break;
                    }
                }
            }

            // Create a sorted enumerable list based on the keys for the sorted dictionary
            List<int> sortedList = sorted.Keys.ToList();
            sortedList.Sort();

            // Clear all of the listings and re-add the active listings in the new sorted order
            this.ResetListings();
            for (int i = 0; i < sortedList.Count; i++)
            {
                this.definitionListings[i].State = sorted[sortedList[i]];
            }
        }

        /// <summary>
        /// Resets the definition listings.
        /// </summary>
        private void ResetListings()
        {
            // Clear all listings and make sure the first one is active
            foreach (DefinitionListing listing in this.definitionListings)
            {
                listing.Clear();
            }

            this.definitionListings[0].Active = true;
        }

        #endregion
    }
}