//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="WellKnownGuids.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// WellKnownGuids class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System;

    #endregion

    /// <summary>
    /// Well-known FIM Guids
    /// </summary>
    public static class WellKnownGuids
    {
        /// <summary>
        /// Well-known Guid for FIM Service Account
        /// </summary>
        public static readonly Guid FIMServiceAccount = new Guid("e05d1f1b-3d5e-4014-baa6-94dee7d68c89");

        /// <summary>
        /// Well-known Guid for Built-in Synchronization Account
        /// </summary>
        public static readonly Guid BuiltInSynchronizationAccount = new Guid("fb89aefa-5ea1-47f1-8890-abe7797d6497");

        /// <summary>
        /// Well-known Guid for Anonymous User
        /// </summary>
        public static readonly Guid AnonymousUser = new Guid("b0b36673-d43b-4cfa-a7a2-aff14fd90522");
    }
}
