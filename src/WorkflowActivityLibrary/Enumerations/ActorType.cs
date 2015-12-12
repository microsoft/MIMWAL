//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ActorType.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ActorType enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible actors for FIM data access activities.
    /// </summary>
    public enum ActorType
    {
        /// <summary>
        /// Use FIM Service as the actor for FIM data access.
        /// </summary>
        Service,

        /// <summary>
        /// Use Requestor as the actor for FIM data access.
        /// </summary>
        Requestor,

        /// <summary>
        /// Resolve the reference as the actor for FIM data access.
        /// </summary>
        Resolve,

        /// <summary>
        /// Use the specified account as the actor for FIM data access.
        /// </summary>
        Account
    }
}