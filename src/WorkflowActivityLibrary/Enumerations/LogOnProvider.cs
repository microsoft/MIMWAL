//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="LogOnProvider.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// Logon providers enumerations.
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// Used for the impersonation API calls
    /// </summary>
    public enum LogOnProvider
    {
        /// <summary>
        /// The default logon provider for the system
        /// </summary>
        ProviderDefault,

        /// <summary>
        /// NTLM logon provider
        /// </summary>
        ProviderWinNT40 = 2,

        /// <summary>
        /// The negotiate logon provider
        /// </summary>
        ProviderWinNT50
    }
}
