//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="RemovedValuesCollection.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// RemovedValuesCollection class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// This class is used to denote all values removed from the specified list using RemoveValues() function.
    /// It is used by a WAL activity so that it can generate update request parameters accordingly.
    /// </summary>
    public class RemovedValuesCollection : List<object>
    {
    }
}