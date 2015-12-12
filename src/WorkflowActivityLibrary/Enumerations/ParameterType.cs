//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterType.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ParameterType enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible types of parameters to a function.
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// The function parameter is a Unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// The function parameter is a string
        /// </summary>
        String,

        /// <summary>
        /// The function parameter is a number
        /// </summary>
        Integer,

        /// <summary>
        /// The function parameter is a boolean
        /// </summary>
        Boolean,

        /// <summary>
        /// The function parameter is a lookup
        /// </summary>
        Lookup,

        /// <summary>
        /// The function parameter is a variable
        /// </summary>
        Variable,

        /// <summary>
        /// The function parameter is a function
        /// </summary>
        Function,

        /// <summary>
        /// The function parameter is an expression (nested function)
        /// </summary>
        Expression,

        /// <summary>
        /// The function parameter is a byte array
        /// </summary>
        Binary,

        /// <summary>
        /// The function parameter is EvaluateExpression function
        /// </summary>
        EvaluateExpression,
    }
}