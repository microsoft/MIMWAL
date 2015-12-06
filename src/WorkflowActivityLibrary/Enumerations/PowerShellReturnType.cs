//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="PowerShellReturnType.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// PowerShellReturnType enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible return type for a PowerShell script.
    /// </summary>
    public enum PowerShellReturnType
    {
        /// <summary>
        /// The script does not return any value
        /// </summary>
        None,

        /// <summary>
        /// The script returns a single value
        /// </summary>
        Explicit,

        /// <summary>
        /// The script returns a hash table
        /// </summary>
        Table
    }
}