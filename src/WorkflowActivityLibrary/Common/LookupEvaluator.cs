//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="LookupEvaluator.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// LookupEvaluator class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Evaluates Lookup Expressions
    /// </summary>
    public class LookupEvaluator
    {
        #region Declarations

        /// <summary>
        /// The components of the lookup expression
        /// </summary>
        private readonly List<string> components = new List<string>();

        /// <summary>
        /// The dictionary of the resource xpath and it's attribute value that are needed to be read
        /// </summary>
        private readonly Dictionary<string, string> reads = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupEvaluator" /> class.
        /// </summary>
        /// <param name="lookup">The lookup expression.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated in ValidateLookupGrammar.")]
        public LookupEvaluator(string lookup)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LookupEvaluatorConstructor, "Lookup: '{0}'.", lookup);

            try
            {
                // Publish the lookup to the associated property
                this.Lookup = lookup;

                // Verify the validity of the lookup grammar
                // The lookup must be structured as follows: [//.../...]
                if (!ValidateLookupGrammar(lookup))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.LookupEvaluatorConstructorLookupParameterExpressionValidationError, new InvalidLookupParameterExpressionException(Messages.ExpressionEvaluator_LookupParameterExpressionValidationError, lookup));
                }

                // Verify lookup paramter
                this.Parameter = DetermineLookupParameter(lookup);

                // Evaluate the lookup to determine the associated reads which are required
                // For example, consider the following lookup: [//Delta/ExplicitMember/Added/Manager/DisplayName]
                // We then need to perform the following reads in the appropriate order:
                //  1. Resource = Delta/ExplicitMember/Added, Attribute = Manager
                //  2. Resource = Delta/ExplicitMember/Added/Manager, Attribute = DisplayName
                // A read will only be required when an attribute is included in the lookup: For example, [//Delta/Attribute] or [//Target/Attribute]
                // If the lookup represents only a parameter name, it will be resolved directly and without a standard read: For example, [//Target], [//Requestor], etc.
                this.components = lookup.Substring(3, lookup.Length - 4).Split('/').ToList();
                StringBuilder resourceBuilder = new StringBuilder();
                if (this.components.Count > 1)
                {
                    // Loop through each component of the relative lookup except for the last, which is known to be an attribute
                    for (int i = 0; i < this.components.Count - 1; i++)
                    {
                        // Determine the resource name by adding the current component to all previous components
                        // If this is the first component, do not add the preceding "/" character
                        if (i > 0)
                        {
                            resourceBuilder.Append("/");
                        }

                        resourceBuilder.Append(this.components[i]);

                        // Determine if the resource is associated with a required read operation
                        // by evaluating special parsing conditions
                        bool requiresRead = true;
                        switch (this.Parameter)
                        {
                            case LookupParameter.Delta:

                                // If the resource being evaluated is "Delta", or alternatively if the resource is "Delta/MultiValuedAttribute" and
                                // is part of a "Delta/MultiValuedAttribute/Added" or "Delta/MultiValuedAttribute/Removed" expression,
                                // it should not be added to the read dictionary as it does not represent a required resource read operation
                                // The attribute will either be resolved by evaluating the request parameters, or the attribute needs to be resolved 
                                // from a read on the multi-valued attribute's Added or Removed value
                                if (i == 0)
                                {
                                    requiresRead = false;
                                }

                                if (i == 1 && this.components.Count > 2 &&
                                    (this.components[2].Equals("Added", StringComparison.OrdinalIgnoreCase) ||
                                     this.components[2].Equals("Removed", StringComparison.OrdinalIgnoreCase)))
                                {
                                    requiresRead = false;
                                }

                                break;
                            case LookupParameter.ComparedRequest:

                                // If the resource being evaluated is "ComparedRequest/Delta", or alternatively if the resource is "ComparedRequest/Delta/MultiValuedAttribute" and
                                // is part of a "ComparedRequest/Delta/MultiValuedAttribute/Added" or "ComparedRequest/Delta/MultiValuedAttribute/Removed" expression,
                                // it should not be added to the read dictionary as it does not represent a required resource read operation
                                // The attribute will either be resolved by evaluating the compared request parameters, or the attribute needs to be resolved 
                                // from a read on the multi-valued attribute's Added or Removed value
                                if (this.components[1].Equals(LookupParameter.Delta.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    if (i < 2)
                                    {
                                        requiresRead = false;
                                    }

                                    if (this.components.Count > 3 &&
                                        i == 2 &&
                                        (this.components[3].Equals("Added", StringComparison.OrdinalIgnoreCase) ||
                                         this.components[3].Equals("Removed", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        requiresRead = false;
                                    }
                                }

                                break;
                            case LookupParameter.WorkflowData:

                                // If the resource being evaluated is "WorkflowData",
                                // it should not be added to the read dictionary as it does not represent a valid resource
                                // Deep resolution against the workflow dictionary will be supported by evaluating any resources written to the dictionary
                                // For example: WorkflowData/MyResource/DisplayName will flag WorkflowData/MyResource as a resource to be read
                                if (i == 0)
                                {
                                    requiresRead = false;
                                }

                                break;
                            case LookupParameter.Queries:

                                // If the resource being evaluated is "Queries",
                                // it should not be added to the read dictionary because it is only used to access supplied query results via a lookup key
                                // For example: [//Queries/DirectReports/...] will facilitate lookups against the results of the "DirectReports" query
                                if (i == 0)
                                {
                                    requiresRead = false;
                                }

                                break;
                            case LookupParameter.RequestParameter:

                                // The "RequestParameter" lookup parameter is only used for two supported lookups
                                // [//RequestParameter/AllChangesAuthorizationTable] and [//RequestParameter/AllChangesActionTable]
                                // This lookup will always be outsourced to the native FIM grammar resolution activity
                                requiresRead = false;
                                break;
                        }

                        // If necessary, add the resource and its attribute value
                        // to the read dictionary
                        if (requiresRead)
                        {
                            this.reads.Add(resourceBuilder.ToString(), this.components[i + 1]);

                            Logger.Instance.WriteVerbose(EventIdentifier.LookupEvaluatorConstructor, "Added the resource '{0}' and its attribute '{1}' to the read dictionary.", resourceBuilder, this.components[i + 1]);
                        }
                    }
                }

                // Determine the target resource lookup value and attribute
                // These values will be used when a lookup is used to express the target for an update request
                if (this.reads.Count > 0)
                {
                    // If the read dictionary has values,
                    // we can safely assume that the last entry represents the target resource and attribute
                    // For example, consider the following lookup: [//Target/DisplayedOwner/Manager/MyAttribute]
                    // The last entry in the read dictionary should be: Target/DisplayedOwner/Manager, MyAttribute
                    string resourceKey = this.reads.Keys.Last();
                    this.TargetResourceLookup = string.Format(CultureInfo.InvariantCulture, "[//{0}]", resourceKey);
                    this.TargetAttribute = this.reads[resourceKey];
                }
                else if (this.Parameter == LookupParameter.WorkflowData && this.components.Count == 2)
                {
                    // If the read dictionary is empty,
                    // the only supported target is the workflow dictionary
                    this.TargetIsWorkflowDictionary = true;
                    this.TargetAttribute = this.components[1];
                }

                // Determine if the lookup represents a valid target for update
                if (!string.IsNullOrEmpty(this.TargetAttribute) &&
                    (this.TargetIsWorkflowDictionary || !string.IsNullOrEmpty(this.TargetResourceLookup)))
                {
                    this.IsValidTarget = true;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LookupEvaluatorConstructor, "Lookup: '{0}'.", lookup);
            }
        }

        #region Properties

        /// <summary>
        /// Gets the lookup expression.
        /// </summary>
        public string Lookup { get; private set; }

        /// <summary>
        /// Gets the components of the lookup expression.
        /// </summary>
        public List<string> Components
        {
            get
            {
                return this.components;
            }
        }

        /// <summary>
        /// Gets the dictionary of the resource xpath and it's attribute value that are needed to be read
        /// </summary>
        public Dictionary<string, string> Reads
        {
            get
            {
                return this.reads;
            }
        }

        /// <summary>
        /// Gets the lookup parameter type.
        /// </summary>
        public LookupParameter Parameter { get; private set; }

        /// <summary>
        /// Gets the target resource (last entry key in the Reads dictionary).
        /// </summary>
        public string TargetResourceLookup { get; private set; }

        /// <summary>
        /// Gets the target attribute (last entry value in the Reads dictionary).
        /// </summary>
        public string TargetAttribute { get; private set; }

        /// <summary>
        /// Gets a value indicating whether target is the workflow dictionary.
        /// </summary>
        public bool TargetIsWorkflowDictionary { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the lookup represents a valid target for update.
        /// </summary>
        public bool IsValidTarget { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Verifies the validity of the lookup grammar
        /// The lookup must be structured as follows: [//.../...]
        /// </summary>
        /// <param name="lookup">The lookup expression.</param>
        /// <returns>True if look expression is valid. Otherwise false.</returns>
        public static bool ValidateLookupGrammar(string lookup)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LookupEvaluatorValidateLookupGrammar, "Lookup: '{0}'.", lookup);

            bool valid = true;
            try
            {
                if (string.IsNullOrEmpty(lookup) ||
                    !lookup.StartsWith("[//", StringComparison.OrdinalIgnoreCase) ||
                    !lookup.EndsWith("]", StringComparison.OrdinalIgnoreCase) ||
                    lookup.Substring(3, lookup.Length - 4).Contains("[") ||
                    lookup.Substring(3, lookup.Length - 4).Contains("]"))
                {
                    valid = false;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LookupEvaluatorValidateLookupGrammar, "Lookup: '{0}'. Valid: '{1}'.", lookup, valid);
            }

            return valid;
        }

        /// <summary>
        /// Determines the parameter type of the lookup expression
        /// The activities support all standard parameters: [//Target/...], [//Requestor/...], etc.
        /// as well as enhanced support for the [//Delta/...] and [//Request/...]
        /// New parameters are also supported: [//Queries/...], [//ComparedRequest/...], [//Approvers/...], and [//Effective/...]
        /// </summary>
        /// <param name="lookup">The lookup expression.</param>
        /// <returns>The LookupParameter</returns>
        public static LookupParameter DetermineLookupParameter(string lookup)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LookupEvaluatorDetermineLookupParameter, "Lookup: '{0}'.", lookup);

            LookupParameter lookupParameter = LookupParameter.Unknown;

            try
            {
                if (string.IsNullOrEmpty(lookup))
                {
                    return lookupParameter;
                }

                string component = lookup.Substring(3, lookup.Length - 4).Split(new char[] { '/' })[0];
                switch (component.ToUpperInvariant())
                {
                    case "REQUEST":
                        lookupParameter = LookupParameter.Request;
                        break;
                    case "REQUESTOR":
                        lookupParameter = LookupParameter.Requestor;
                        break;
                    case "WORKFLOWDATA":
                        lookupParameter = LookupParameter.WorkflowData;
                        break;
                    case "TARGET":
                        lookupParameter = LookupParameter.Target;
                        break;
                    case "DELTA":
                        lookupParameter = LookupParameter.Delta;
                        break;
                    case "REQUESTPARAMETER":
                        lookupParameter = LookupParameter.RequestParameter;
                        break;
                    case "QUERIES":
                        lookupParameter = LookupParameter.Queries;
                        break;
                    case "COMPAREDREQUEST":
                        lookupParameter = LookupParameter.ComparedRequest;
                        break;
                    case "APPROVERS":
                        lookupParameter = LookupParameter.Approvers;
                        break;
                    case "EFFECTIVE":
                        lookupParameter = LookupParameter.Effective;
                        break;
                    case "VALUE":
                        lookupParameter = LookupParameter.Value;
                        break;
                    default:
                        throw Logger.Instance.ReportError(EventIdentifier.LookupEvaluatorDetermineLookupParameterError, new NotSupportedLookupParameterException(Messages.ExpressionEvaluator_LookupParameterValidationError, lookup));
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LookupEvaluatorDetermineLookupParameter, "Lookup: '{0}'. LookupParameter: '{1}'.", lookup, lookupParameter);
            }

            return lookupParameter;
        }

        #endregion
    }
}