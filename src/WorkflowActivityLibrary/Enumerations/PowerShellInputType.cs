//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="PowerShellInputType.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// PowerShellInputType enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible input types for a PowerShell script.
    /// </summary>
    public enum PowerShellInputType
    {
        /// <summary>
        /// The script does not take any input
        /// </summary>
        None,

        /// <summary>
        /// The script uses named parameters as input
        /// </summary>
        Parameters,

        /// <summary>
        /// The script uses arguments as input
        /// </summary>
        Arguments
    }
}