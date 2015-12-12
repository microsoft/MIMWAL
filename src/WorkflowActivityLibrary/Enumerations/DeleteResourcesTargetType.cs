//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteResourcesTargetType.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DeleteResourcesTargetType enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible targets for DeleteResources activity.
    /// </summary>
    public enum DeleteResourcesTargetType
    {
        /// <summary>
        /// The target for DeleteResources activity is the target of the request
        /// </summary>
        WorkflowTarget,

        /// <summary>
        /// The target for DeleteResources activity is specified search query
        /// </summary>
        SearchForTarget,

        /// <summary>
        /// Resolve the reference as the target for the DeleteResources activity
        /// </summary>
        ResolveTarget
    }
}