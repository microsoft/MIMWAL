//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DefinitionsConverter.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DefinitionsConverter class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;

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

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionsConverter"/> class.
        /// </summary>
        /// <param name="definitionsTable">The definitions table.</param>
        public DefinitionsConverter(Hashtable definitionsTable)
        {
            // Do nothing if the hashtable is null. This is to allow backward compatibility of the old Activity UI XOML when a newer version is introduced
            if (definitionsTable == null)
            {
                return;
            }

            // This constructor handles a hash table which is deserialized from an activity definition's XOML
            // Throw an exception if the hashtable is null or missing the Count key which indicates how many definitions it stores
            if (definitionsTable != null && !definitionsTable.ContainsKey("Count"))
            {
                throw Logger.Instance.ReportError(new ArgumentException(Messages.DefinitionsConverter_NullOrEmptyDefinitionsTableError, "definitionsTable"));
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

        #endregion
    }
}