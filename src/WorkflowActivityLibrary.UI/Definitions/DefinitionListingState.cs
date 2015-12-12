//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DefinitionListingState.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DefinitionListingState class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions
{
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;

    /// <summary>
    /// Represents state of all user-visible controls associated with the definition listing.
    /// </summary>
    public class DefinitionListingState : Definition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionListingState"/> class.
        /// </summary>
        /// <param name="selected">The state of the "Selected" checkbox.</param>
        /// <param name="left">The text of the left-hand side textbox.</param>
        /// <param name="right">The text of the right-hand side textbox.</param>
        /// <param name="check">The state of the "Allow Null" checkbox.</param>
        public DefinitionListingState(bool selected, string left, string right, bool check) : base(left, right, check)
        {
            this.Selected = selected;
        }

        /// <summary>
        /// Gets a value indicating whether the definition listing is selected.
        /// </summary>
        public bool Selected { get; private set; }
    }
}