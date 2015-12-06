//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateLookupDefinition.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// UpdateLookupDefinition class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions
{
    #region Namespaces Declarations

    using System;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

    #endregion

    /// <summary>
    /// Update Lookup Definition
    /// </summary>
    [Serializable]
    public class UpdateLookupDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateLookupDefinition"/> class.
        /// </summary>
        /// <param name="targetLookup">The target lookup.</param>
        /// <param name="value">The target value.</param>
        /// <param name="mode">The update mode.</param>
        public UpdateLookupDefinition(string targetLookup, object value, UpdateMode mode)
        {
            this.TargetLookup = targetLookup;
            this.Value = value;
            this.Mode = mode;
        }

        #region Properties

        /// <summary>
        /// Gets the target lookup.
        /// </summary>
        public string TargetLookup { get; private set; }

        /// <summary>
        /// Gets the target value.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets the update mode.
        /// </summary>
        public UpdateMode Mode { get; private set; }
        
        #endregion
    }
}