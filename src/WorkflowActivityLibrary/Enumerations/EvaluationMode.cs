//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="EvaluationMode.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// EvaluationMode enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible mode of expression evaluation.
    /// </summary>
    public enum EvaluationMode
    {
        /// <summary>
        /// Parse the expression
        /// </summary>
        Parse,

        /// <summary>
        /// Resolve the expression and look up the value
        /// </summary>
        Resolve
    }
}