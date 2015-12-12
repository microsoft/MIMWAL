//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="LookupParameter.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// LookupParameter enum
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations
{
    /// <summary>
    /// The enumeration defines possible types of lookup parameters.
    /// </summary>
    public enum LookupParameter
    {
        /// <summary>
        /// Unknown Lookup parameter
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Lookup the request for evaluation
        /// </summary>
        Request,

        /// <summary>
        /// Lookup the requestor evaluation
        /// </summary>
        Requestor,

        /// <summary>
        /// Lookup the workflow data for evaluation
        /// </summary>
        WorkflowData,

        /// <summary>
        /// Lookup the target for evaluation
        /// </summary>
        Target,

        /// <summary>
        /// Lookup the delta for evaluation
        /// </summary>
        Delta,

        /// <summary>
        /// Lookup the request parameter for evaluation
        /// </summary>
        RequestParameter,

        /// <summary>
        /// Lookup the queries for evaluation
        /// </summary>
        Queries,

        /// <summary>
        /// Lookup the compared request for evaluation. Used mainly to verify request conflict
        /// </summary>
        ComparedRequest,

        /// <summary>
        /// Lookup the request approvers for evaluation
        /// </summary>
        Approvers,

        /// <summary>
        /// Lookup the effective object for evaluation. This may be request delta or target.
        /// </summary>
        Effective,

        /// <summary>
        /// Lookup the Value returned by Iteration expression or GenerateUniqueValue activity for evaluation
        /// </summary>
        Value,

        /// <summary>
        /// Lookup the uniqueness key.
        /// </summary>
        UniquenessKey
    }
}