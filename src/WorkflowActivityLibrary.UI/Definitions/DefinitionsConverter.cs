//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DefinitionsConverter.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DefinitionsConverter class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI;

    #endregion

    /// <summary>
    /// Helper class for converting  definitions listing into and from hash table.
    /// </summary>
    public class DefinitionsConverter
    {
        #region Declarations

        /// <summary>
        /// The list of definitions
        /// </summary>
        private readonly List<Definition> definitions = new List<Definition>();

        /// <summary>
        /// The definitions hash table
        /// </summary>
        private readonly Hashtable definitionsTable = new Hashtable();

        /// <summary>
        /// The activity settings part data
        /// </summary>
        private readonly ActivitySettingsPartData settingsPartData = new ActivitySettingsPartData();
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionsConverter"/> class.
        /// </summary>
        /// <param name="definitionListings">The definition listings.</param>
        public DefinitionsConverter(IEnumerable<DefinitionListing> definitionListings)
            : this(definitionListings, null, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionsConverter"/> class.
        /// </summary>
        /// <param name="definitionListings">The definition listings.</param>
        /// <param name="settingsPartData">The settings part data.</param>
        /// <param name="controllerId">The controller identifier.</param>
        public DefinitionsConverter(IEnumerable<DefinitionListing> definitionListings, ActivitySettingsPartData settingsPartData, string controllerId)
        {
            // This constructor handles a list of definition listings which exist for a controller
            // Throw an exception if that list is null
            if (definitionListings == null)
            {
                throw Logger.Instance.ReportError(new ArgumentNullException("definitionListings", ActivitySettings.NullDefinitionListingsError));
            }

            // If settings part data was supplied, the activity has other controls which were
            // stored to the data before calling this constructor
            // In this circumstance, the published definitions settings part data needs 
            // to start with the provided data so we don't lose those values
            if (settingsPartData != null)
            {
                this.settingsPartData = settingsPartData;
            }

            int i = 0;
            foreach (DefinitionListing listing in definitionListings.Where(listing => listing.Active && listing.Definition != null))
            {
                // For each active listing where the definition can be appropriately constructed:
                // Add the definition to the published list
                // Add the definition to the published settings part data
                // Add each value of the definition to the published hash table
                this.definitions.Add(listing.Definition);
                this.settingsPartData[string.Format(CultureInfo.InvariantCulture, "Definition:{0}:{1}", listing.ControllerId, i)] = listing.Definition;
                this.definitionsTable.Add(string.Format(CultureInfo.InvariantCulture, "{0}:0", i), listing.Definition.Left);
                this.definitionsTable.Add(string.Format(CultureInfo.InvariantCulture, "{0}:1", i), listing.Definition.Right);
                this.definitionsTable.Add(string.Format(CultureInfo.InvariantCulture, "{0}:2", i), listing.Definition.Check);
                i += 1;
            }

            // Add the number of active definitions to the published hashtable and settings part data
            // so that it can be used later to reconstruct a list of definitions
            this.definitionsTable.Add("Count", i);
            this.settingsPartData[string.Format(CultureInfo.InvariantCulture, "{0}:Count", controllerId)] = i;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionsConverter"/> class.
        /// </summary>
        /// <param name="definitionsTable">The definitions table.</param>
        public DefinitionsConverter(Hashtable definitionsTable)
        {
            // This constructor handles a hash table which is deserialized from an activity definition's XOML
            // Throw an exception if the hashtable is null or missing the Count key which indicates how many definitions it stores
            if (definitionsTable == null || !definitionsTable.ContainsKey("Count"))
            {
                throw Logger.Instance.ReportError(new ArgumentException(ActivitySettings.DefinitionsConverter_NullOrEmptyDefinitionsTableError, "definitionsTable"));
            }

            // Publish the supplied hashtable
            this.definitionsTable = definitionsTable;

            // Extract the number of definitions stored in the table
            int count = (int)definitionsTable["Count"];
            for (int i = 0; i < count; i++)
            {
                // Each value for each definition is stored independently in the hashtable
                // Use the current index to extract the values for the definition
                string left = (string)definitionsTable[string.Format(CultureInfo.InvariantCulture, "{0}:0", i)];
                string right = (string)definitionsTable[string.Format(CultureInfo.InvariantCulture, "{0}:1", i)];
                bool check = (bool)definitionsTable[string.Format(CultureInfo.InvariantCulture, "{0}:2", i)];

                // Build the definition using the extracted values
                // and add it to the definitions list and settings part data
                Definition definition = new Definition(left, right, check);
                this.definitions.Add(definition);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionsConverter"/> class.
        /// </summary>
        /// <param name="settingsPartData">The settings part data.</param>
        /// <param name="controllerId">The controller identifier.</param>
        public DefinitionsConverter(ActivitySettingsPartData settingsPartData, string controllerId)
        {
            // This constructor handles activity settings part data provided by the activity settings part interface
            // Do nothing if that data is null or missing the Count key which indicates how many definitions it stores
            if (settingsPartData == null || settingsPartData[string.Format(CultureInfo.InvariantCulture, "{0}:Count", controllerId)] == null)
            {
                return;
            }

            // Publish the supplied settings part data
            this.settingsPartData = settingsPartData;

            // Extract the number of definitions stored in the table
            // and publish it to the hashtable
            int count = (int)settingsPartData[string.Format(CultureInfo.InvariantCulture, "{0}:Count", controllerId)];
            this.definitionsTable.Add("Count", count);

            for (int i = 0; i < count; i++)
            {
                string id = string.Format(CultureInfo.InvariantCulture, "Definition:{0}:{1}", controllerId, i);
                if (settingsPartData[id] != null)
                {
                    // For each definition:
                    // Add the definition to the published list
                    // Add each value of the definition to the published hash table
                    Definition definition = (Definition)settingsPartData[id];
                    this.definitions.Add(definition);
                    this.definitionsTable.Add(string.Format(CultureInfo.InvariantCulture, "{0}:0", i), definition.Left);
                    this.definitionsTable.Add(string.Format(CultureInfo.InvariantCulture, "{0}:1", i), definition.Right);
                    this.definitionsTable.Add(string.Format(CultureInfo.InvariantCulture, "{0}:2", i), definition.Check);
                }
            }
        }

        #region Properties

        /// <summary>
        /// Gets the definitions list.
        /// </summary>
        public List<Definition> Definitions
        {
            get
            {
                return this.definitions;
            }
        }

        /// <summary>
        /// Gets the definitions hash table.
        /// </summary>
        public Hashtable DefinitionsTable
        {
            get
            {
                return this.definitionsTable;
            }
        }

        /// <summary>
        /// Gets the activity settings part data.
        /// </summary>
        public ActivitySettingsPartData SettingsPartData
        {
            get
            {
                return this.settingsPartData;
            }
        }
        
        #endregion
    }
}