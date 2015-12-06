//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordCharType.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// PasswordCharType enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible types of characters in a password.
    /// </summary>
    public enum PasswordCharType
    {
        /// <summary>
        /// The character is a standard alpha-numeric character
        /// </summary>
        Standard,

        /// <summary>
        /// The character is a numeric character
        /// </summary>
        Number,

        /// <summary>
        /// The character is a special character
        /// </summary>
        Special
    }
}