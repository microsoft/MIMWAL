//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="LogOnType.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// Logon types enumerations.
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// Used for the impersonation API calls
    /// </summary>
    public enum LogOnType
    {
        /// <summary>
        /// Invalid value
        /// </summary>
        None = 0,

        /// <summary>
        /// The security principal is logging on interactively.
        /// </summary>
        LogOnInteractive = 2,

        /// <summary>
        /// The security principal is logging using a network.
        /// </summary>
        LogOnNetwork,

        /// <summary>
        /// The logon is for a batch process.
        /// </summary>
        LogOnBatch,

        /// <summary>
        /// The logon is for a service account.
        /// </summary>
        LogOnService,

        /// <summary>
        /// The logon is a network logon with plaintext credentials.
        /// </summary>
        LogOnNetworkClearText = 8
    }
}
