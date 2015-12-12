//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="PowerShellScriptLocation.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// PowerShellScriptLocation enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible locations for a PowerShell script.
    /// </summary>
    public enum PowerShellScriptLocation
    {
        /// <summary>
        /// The script is located in a file
        /// </summary>
        Disk,

        /// <summary>
        /// The script is located inline with the workflow xoml
        /// </summary>
        WorkflowDefinition,

        /// <summary>
        /// The script is located on an attribute of a FIM resource
        /// </summary>
        Resource
    }
}