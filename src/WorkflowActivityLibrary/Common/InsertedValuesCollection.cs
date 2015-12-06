//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="InsertedValuesCollection.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// InsertedValuesCollection class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// This class is used to denote all values inserted from the specified list using InsertValues() function.
    /// It is used by a WAL activity so that it can generate update request parameters accordingly.
    /// </summary>
    public class InsertedValuesCollection : List<object>
    {
    }
}