//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="GenerateUniqueValue.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// GenerateUniqueValue Activity 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.DirectoryServices;
    using System.Globalization;
    using System.Linq;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Generates unique value
    /// </summary>
    public partial class GenerateUniqueValue : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(GenerateUniqueValue));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty PublicationTargetProperty =
            DependencyProperty.Register("PublicationTarget", typeof(string), typeof(GenerateUniqueValue));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(GenerateUniqueValue));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConflictFilterProperty =
            DependencyProperty.Register("ConflictFilter", typeof(string), typeof(GenerateUniqueValue));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryLdapProperty =
            DependencyProperty.Register("QueryLdap", typeof(bool), typeof(GenerateUniqueValue));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty LdapQueriesTableProperty =
            DependencyProperty.Register("LdapQueriesTable", typeof(Hashtable), typeof(GenerateUniqueValue));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty UniquenessSeedProperty =
            DependencyProperty.Register("UniquenessSeed", typeof(int), typeof(GenerateUniqueValue));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ValueExpressionsProperty =
            DependencyProperty.Register("ValueExpressions", typeof(ArrayList), typeof(GenerateUniqueValue));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The list of unique identifiers of matching objects found.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> FoundIds;

        /// <summary>
        /// The list of matching resources found
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<ResourceType> FoundResources;

        /// <summary>
        /// The LDAP query definitions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Definition> LdapQueries;

        /// <summary>
        /// The conflict filter XPath.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "XPath", Justification = "Reviewed and overruled.")]
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public string XPathConflictFilter;

        /// <summary>
        /// The value expression
        /// </summary>
        private static readonly string Value = string.Format(CultureInfo.InvariantCulture, "[//{0}]", LookupParameter.Value);

        /// <summary>
        /// The uniqueness key expression [//UniquenessKey]
        /// </summary>
        private static readonly string UniquenessKey = string.Format(CultureInfo.InvariantCulture, "[//{0}]", LookupParameter.UniquenessKey);

        /// <summary>
        /// The number of iterations preformed so far
        /// </summary>
        private int iterations;

        /// <summary>
        /// The LDAP conflict found?
        /// </summary>
        private bool ldapConflict;

        /// <summary>
        /// The resolved value
        /// </summary>
        private string resolvedValue;

        /// <summary>
        /// The value is unique?
        /// </summary>
        private bool unique;

        /// <summary>
        /// The current value of uniqueness key
        /// </summary>
        private int uniquenessKey;

        /// <summary>
        /// The index of current unique value being tested
        /// </summary>
        private int valueIndex;

        /// <summary>
        /// The maximum loop count
        /// </summary>
        private int maxLoopCount;

        /// <summary>
        /// The optimize UniquenessKey will be true if the Conflict Filter makes use of starts-with() function
        /// e.g. /Person[starts-with(AccountName, 'TestUser')]
        /// </summary>
        private bool optimizeUniquenessKey;

        /// <summary>
        /// The resolved value without the uniqueness seed
        /// </summary>
        private string resolvedValueWithoutUniquenessSeed;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateUniqueValue"/> class.
        /// </summary>
        public GenerateUniqueValue()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueConstructor);

            try
            {
                this.InitializeComponent();

                if (this.ActivityExpressionEvaluator == null)
                {
                    this.ActivityExpressionEvaluator = new ExpressionEvaluator();
                }

                if (this.FoundIds == null)
                {
                    this.FoundIds = new List<Guid>();
                }

                if (this.FoundResources == null)
                {
                    this.FoundResources = new List<ResourceType>();
                }

                if (this.LdapQueries == null)
                {
                    this.LdapQueries = new List<Definition>();
                }

                if (!int.TryParse(ConfigurationManager.AppSettings["GenerateUniqueValueActivity_MaxLoopCount"], out this.maxLoopCount))
                {
                    this.maxLoopCount = 512;
                }

                if (!bool.TryParse(ConfigurationManager.AppSettings["GenerateUniqueValueActivity_OptimizeUniquenessKey"], out this.optimizeUniquenessKey))
                {
                    this.optimizeUniquenessKey = false;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the display name of the activity.
        /// </summary>
        [Description("ActivityDisplayName")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ActivityDisplayName
        {
            get
            {
                return (string)this.GetValue(ActivityDisplayNameProperty);
            }

            set
            {
                this.SetValue(ActivityDisplayNameProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the target for unique value publication.
        /// </summary>
        [Description("PublicationTarget")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PublicationTarget
        {
            get
            {
                return (string)this.GetValue(PublicationTargetProperty);
            }

            set
            {
                this.SetValue(PublicationTargetProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        [Description("Activity Execution Condition")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ActivityExecutionCondition
        {
            get
            {
                return (string)this.GetValue(ActivityExecutionConditionProperty);
            }

            set
            {
                this.SetValue(ActivityExecutionConditionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the conflict filter.
        /// </summary>
        [Description("ConflictFilter")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ConflictFilter
        {
            get
            {
                return (string)this.GetValue(ConflictFilterProperty);
            }

            set
            {
                this.SetValue(ConflictFilterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to query LDAP.
        /// </summary>
        [Description("QueryLdap")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool QueryLdap
        {
            get
            {
                return (bool)this.GetValue(QueryLdapProperty);
            }

            set
            {
                this.SetValue(QueryLdapProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the LDAP queries hash table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("LdapQueriesTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable LdapQueriesTable
        {
            get
            {
                return (Hashtable)this.GetValue(LdapQueriesTableProperty);
            }

            set
            {
                this.SetValue(LdapQueriesTableProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the uniqueness seed.
        /// </summary>
        [Description("UniquenessSeed")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int UniquenessSeed
        {
            get
            {
                return (int)this.GetValue(UniquenessSeedProperty);
            }

            set
            {
                this.SetValue(UniquenessSeedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value expressions.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("ValueExpressions")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ArrayList ValueExpressions
        {
            get
            {
                return (ArrayList)this.GetValue(ValueExpressionsProperty);
            }

            set
            {
                this.SetValue(ValueExpressionsProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the activity.
        /// </summary>
        /// <param name="executionContext">The execution context of the activity.</param>
        /// <returns>The <see cref="ActivityExecutionStatus"/> of the activity after executing the activity.</returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueExecute);

            try
            {
                // Ideally we would set CallContext in OnActivityExecutionContextLoad instead here in Execute
                // as OnActivityExecutionContextLoad gets called on each hydration and rehydration of the workflow instance
                // but looks like it's invoked on a different thread context than the rest of the workflow instance execution.
                // To minimize the loss of the CallContext on rehydration, we'll set it in the Execute of every WAL child activities.
                // It will still get lost (momentarily) when the workflow is persisted in the middle of the execution of a replicator activity, for example.
                Logger.SetContextItem(this, this.WorkflowInstanceId);

                return base.Execute(executionContext);
            }
            catch (Exception ex)
            {
                throw Logger.Instance.ReportError(EventIdentifier.GenerateUniqueValueExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueExecute);
            }
        }

        /// <summary>
        /// Determines the action taken when the activity has completed execution.
        /// </summary>
        /// <param name="executionContext">The execution context of the activity.</param>
        protected override void OnSequenceComplete(ActivityExecutionContext executionContext)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueOnSequenceComplete);

            try
            {
                // Clear the variable cache for the expression evaluator
                // so that any variables, such as SqlParameter, not marked as serializable does not cause dehyration issues.
                if (this.ActivityExpressionEvaluator != null
                    && this.ActivityExpressionEvaluator.VariableCache != null
                    && this.ActivityExpressionEvaluator.VariableCache.Keys != null)
                {
                    List<string> variables = this.ActivityExpressionEvaluator.VariableCache.Keys.ToList();
                    foreach (string variable in variables)
                    {
                        this.ActivityExpressionEvaluator.VariableCache[variable] = null;
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueOnSequenceComplete);
            }
        }

        /// <summary>
        /// Resolves the value filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="value">The value.</param>
        /// <returns>The resolved value filter.</returns>
        private static string ResolveValueFilter(string filter, string value)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueResolveValueFilter, "Filter: '{0}'. Value: '{1}'.", filter, value);

            try
            {
                // The [//Value] lookup is not resolvable by the standard activities and must be resolved by this activity
                // Because we expect to see this in the conflict XPath filter, simply replace the lookup with the supplied value
                while (filter.ToUpperInvariant().Contains(Value.ToUpperInvariant()))
                {
                    int start = filter.IndexOf(Value, StringComparison.OrdinalIgnoreCase);
                    filter = filter.Replace(filter.Substring(start, Value.Length), value);
                }

                return filter;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueResolveValueFilter, "Filter: '{0}'.", filter);
            }
        }

        /// <summary>
        /// Checks if the conflict exists in LDAP.
        /// </summary>
        /// <param name="path">The LDAP path.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>true if conflict exists. Otherwise false.</returns>
        private static bool ConflictExistsInLdap(string path, string filter)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueConflictExistsInLdap, "Path: '{0}'. Filter: '{1}'.", path, filter);

            bool found = false;
            try
            {
                // Execute the supplied filter against the LDAP path to determine if it matches an entry in the directory
                // If so, the value is in conflict
                using (DirectoryEntry de = new DirectoryEntry(path))
                {
                    using (DirectorySearcher ds = new DirectorySearcher(de))
                    {
                        ds.SearchScope = SearchScope.Subtree;
                        ds.Filter = filter;

                        found = ds.FindOne() != null;

                        return found;
                    }
                }
            }
            catch (Exception ex)
            {
                // If an exception was thrown while attempting to query LDAP,
                // format the exception and raise it to the request
                throw Logger.Instance.ReportError(EventIdentifier.GenerateUniqueValueConflictExistsInLdapLdapError, new WorkflowActivityLibraryException(Messages.GenerateUniqueValue_LDAPError, ex, path, ex.Message));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueConflictExistsInLdap, "Path: '{0}'. Filter: '{1}'. Returning: '{2}'.", path, filter, found);
            }
        }

        /// <summary>
        /// Sets the attributes to read for conflict resources if the Conflict Filter makes use of starts-with() function
        /// The values read on the conflict resources then used to reposition the uniqueness key.
        /// Also sets the optimizeUniquenessKey to true
        /// </summary>
        private void SetAttributesToReadForConflictResources()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueSetAttributesToReadForConflictResources, "Filter: '{0}'.", this.ConflictFilter);

            this.FindConflict.Attributes = null;
            this.optimizeUniquenessKey = false;
            string filter = this.ConflictFilter;
            try
            {
                // conflict filter : "/Person[starts-with(AccountName,'[//Value]') and not(ObjectID='[//Target/ObjectID]')]"
                string startToken = "starts-with";
                int startTokenLength = startToken.Length;
                string endToken = "'[//Value]'";

                List<string> attributes = new List<string>();
                int startIndex = filter.IndexOf(startToken, StringComparison.Ordinal);
                while (startIndex != -1)
                {
                    int endIndex = filter.IndexOf(endToken, startIndex, StringComparison.OrdinalIgnoreCase);
                    string s = filter.Substring(startIndex + startTokenLength + 1, endIndex - (startIndex + startTokenLength + 1)).Trim(new char[] { ' ', '(', ',' });
                    attributes.Add(s);
                    filter = filter.Substring(endIndex);
                    startIndex = filter.IndexOf(startToken, StringComparison.Ordinal);
                }

                if (attributes.Count > 0)
                {
                    this.optimizeUniquenessKey = true;
                    this.FindConflict.Attributes = attributes.ToArray();
                    Logger.Instance.WriteVerbose(EventIdentifier.GenerateUniqueValueSetAttributesToReadForConflictResources, "Filter: '{0}'. Attributes: '{1}'.", this.ConflictFilter, string.Join(";", attributes.ToArray()));
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueSetAttributesToReadForConflictResources, "Filter: '{0}'.", this.ConflictFilter);
            }
        }

        /// <summary>
        /// Resolves the uniqueness key.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="uniquenessKeyValue">The uniqueness key value.</param>
        /// <param name="resetUniquenessKeyFilter">If true, the UniquenessKey value is not set in the XPath conflict filter</param>
        /// <returns>The resolved uniqueness key expression.</returns>
        private string ResolveUniquenessKey(string expression, int uniquenessKeyValue, bool resetUniquenessKeyFilter)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueResolveUniquenessKey, "Expression: '{0}'. UniquenessKeyValue: '{1}'.", expression, uniquenessKeyValue);

            try
            {
                // The [//UniquenessKey] lookup is not resolvable by the standard activities and must be resolved by this activity
                // Because we expect to see this in an expression, replace it with the supplied uniqueness key, converted to string and
                // in quotation marks, to ensure the value is handled properly by the expression evaluator
                string uniquenessKeyValueString = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", uniquenessKeyValue.ToString(CultureInfo.InvariantCulture));

                // if the optimizeUniquenessKey is required and this.uniquenessKey is not yet repositioned,
                // modify the XPathConflictFilter to not include the [//UniquenessKey] lookup
                if (this.optimizeUniquenessKey && resetUniquenessKeyFilter)
                {
                    uniquenessKeyValueString = "\"\"";
                }

                while (expression.ToUpperInvariant().Contains(UniquenessKey.ToUpperInvariant()))
                {
                    int start = expression.IndexOf(UniquenessKey, StringComparison.OrdinalIgnoreCase);
                    expression = expression.Replace(expression.Substring(start, UniquenessKey.Length), uniquenessKeyValueString);
                }

                return expression;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueResolveUniquenessKey, "Returning: '{0}'.", expression);
            }
        }

        /// <summary>
        /// Checks if the conflict resource is found
        /// </summary>
        /// <returns>True if the conflict resource is found</returns>
        private bool CheckConflictResourceFound()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueCheckConflictResourceFound, "Resolved value: '{0}.' Conflict Resource Count: '{1}'. UniquenessKey: '{2}'.", this.resolvedValue, this.FoundResources.Count, this.uniquenessKey);

            bool conflictResourceFound = false;
            try
            {
                foreach (ResourceType conflictResource in this.FoundResources)
                {
                    foreach (string attribute in this.FindConflict.Attributes)
                    {
                        string value = conflictResource[attribute] as string;

                        if (this.resolvedValue.Equals(value, StringComparison.OrdinalIgnoreCase))
                        {
                            conflictResourceFound = true;
                            Logger.Instance.WriteVerbose(EventIdentifier.GenerateUniqueValueDecideExecuteCode, "Conflict Found. Resource: '{0}'. Type: '{1}'. Value: '{2}'.", conflictResource.ObjectID, conflictResource.ObjectType, this.resolvedValue);
                        }
                    }
                }

                return conflictResourceFound;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueCheckConflictResourceFound, "Resolved value: '{0}.' Conflict Resource Count: '{1}'. UniquenessKey: '{2}'. Conflict found: '{3}'.", this.resolvedValue, this.FoundResources.Count, this.uniquenessKey, conflictResourceFound);
            }
        }

        /// <summary>
        /// Repositions the UniquenessKey to the first missing value in sequence of conflicting values
        /// </summary>
        /// <returns>True if the UniquenessKey was repositioned</returns>
        private bool RepositionUniquenessKey()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueRepositionUniquenessKey, "Resolved value: '{0}.' Conflict Resource Count: '{1}'. UniquenessKey: '{2}'.", this.resolvedValue, this.FoundResources.Count, this.uniquenessKey);

            bool keyRepositioned = false;
            List<int> seeds = new List<int>();
            try
            {
                foreach (ResourceType conflictResource in this.FoundResources)
                {
                    foreach (
                        string value in
                            this.FindConflict.Attributes.Select(attribute => conflictResource[attribute] as string)
                                .Where(value => !string.IsNullOrEmpty(value)))
                    {
                        string seedString = value.ToUpperInvariant().Replace(this.resolvedValueWithoutUniquenessSeed.ToUpperInvariant(), string.Empty);
                        int seed;
                        if (int.TryParse(seedString, out seed))
                        {
                            seeds.Add(seed);
                        }
                    }
                }

                seeds.Sort();

                Logger.Instance.WriteVerbose(EventIdentifier.GenerateUniqueValueDecideExecuteCode, "Current UniquenessKey: '{0}'. Seeds used: '{1}'.", this.uniquenessKey, string.Join(";", seeds.ConvertAll(i => i.ToString(CultureInfo.InvariantCulture)).ToArray()));

                foreach (int seed in seeds)
                {
                    if (seed < this.uniquenessKey)
                    {
                        continue;
                    }

                    if (seed == this.uniquenessKey)
                    {
                        ++this.uniquenessKey;
                        keyRepositioned = true;
                    }
                    else
                    {
                        break;
                    }
                }

                return keyRepositioned;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueRepositionUniquenessKey, "Resolved value: '{0}.' Conflict Resource Count: '{1}'. UniquenessKey: '{2}'. UniquenessKey Repositioned: '{3}'.", this.resolvedValue, this.FoundResources.Count, this.uniquenessKey, keyRepositioned);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ParseExpressions CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ParseExpressions_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueParseExpressionsExecuteCode);

            try
            {
                // If the activity is configured for conditional execution, parse the associated expression
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);

                // Load each value expression into the evaluator so associated lookups can be loaded into the cache for resolution
                // Before parsing the value expression, be sure to replace any instance of the [//UniquenessKey] lookup with
                // a random value to prevent it from being loaded to the cache
                // Standard resolution of this lookup would result in an exception
                foreach (string valueExpression in this.ValueExpressions)
                {
                    this.ActivityExpressionEvaluator.ParseExpression(this.ResolveUniquenessKey(valueExpression, 0, false));
                }

                // If the activity is configured to query LDAP for conflicts,
                // convert the LDAP queries hash table to a list of definitions and then parse each query
                if (!this.QueryLdap || this.LdapQueriesTable == null || this.LdapQueriesTable.Count <= 0)
                {
                    return;
                }

                DefinitionsConverter queriesConverter = new DefinitionsConverter(this.LdapQueriesTable);
                this.LdapQueries = queriesConverter.Definitions;

                // Parse the expressions in the LDAP Queries, if any
                for (int i = 0; i < this.LdapQueries.Count; ++i)
                {
                    this.ActivityExpressionEvaluator.ParseIfExpression(this.LdapQueries[i].Left);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueParseExpressionsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValuePrepareExecuteCode);

            try
            {
                // Find the attributes to read on potentially conflicing resources
                // so that the uniqueness seed can be repositioned instead of simply incremented
                // when the conflict filter XPath uses a starts-with fuction
                if (this.optimizeUniquenessKey)
                {
                    this.SetAttributesToReadForConflictResources();
                }

                // Default the uniqueness key to the specified uniqueness seed,
                // resolve the first value expression in the list, and use that value to resolve
                // the conflict filter which will verify its uniqueness
                // If the first value expression resolves to null, find the first expression that does not
                // The last expression will never resolve to null because it always includes the [//UniquenessKey] lookup
                this.uniquenessKey = this.UniquenessSeed;
                bool resetUniquenessKeyFilter = this.optimizeUniquenessKey;
                this.valueIndex = 0;
                foreach (object resolved in from string s in this.ValueExpressions select this.ActivityExpressionEvaluator.ResolveExpression(this.ResolveUniquenessKey(s, this.uniquenessKey, false)))
                {
                    if (resolved != null)
                    {
                        this.resolvedValue = resolved.ToString();
                        break;
                    }

                    this.valueIndex += 1;
                }

                // Format the XPath conflict filter appropriately using the resolved value if the UniquenessKey Filter require reset
                if (this.valueIndex + 1 == this.ValueExpressions.Count && resetUniquenessKeyFilter)
                {
                    // we are at the last value expression and it always includes the [//UniquenessKey] lookup. 
                    // so we'll modify the XPath filter to be e.g. /Person[starts-with(AccountName, 'TestUser')] 
                    // instead of /Person[starts-with(AccountName, 'TestUser2')]
                    object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.ResolveUniquenessKey(this.ValueExpressions[this.valueIndex].ToString(), this.uniquenessKey, true));
                    this.resolvedValueWithoutUniquenessSeed = resolved as string;
                    this.XPathConflictFilter = ResolveValueFilter(this.ConflictFilter, this.resolvedValueWithoutUniquenessSeed);
                }
                else
                {
                    // No modification of XPath filter is needed as it does not use starts-with() function.
                    this.resolvedValueWithoutUniquenessSeed = null;
                    this.XPathConflictFilter = ResolveValueFilter(this.ConflictFilter, this.resolvedValue);
                }

                // resolve the Expressions in the LDAP Queries, if any
                for (int i = 0; i < this.LdapQueries.Count; ++i)
                {
                    if (ExpressionEvaluator.IsExpression(this.LdapQueries[i].Left))
                    {
                        this.LdapQueries[i] = new Definition(this.ActivityExpressionEvaluator.ResolveExpression(this.LdapQueries[i].Left) as string, this.LdapQueries[i].Right, this.LdapQueries[i].Check);
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValuePrepareExecuteCode, "Conflict Filter XPath: '{0}'.", this.XPathConflictFilter);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachLdap ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachLdap_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueForEachLdapChildInitialized);

            string stringForResolution = null;
            try
            {
                // Prepare for the execution of the LDAP query by pulling the filter
                // from the definition and assigning it to the resolve lookup string
                // Resolve the [//Value] lookup with the current value before resolution
                Definition definition = e.InstanceData as Definition;
                ResolveLookupString resolveFilter = e.Activity as ResolveLookupString;
                if (resolveFilter == null || definition == null)
                {
                    return;
                }

                stringForResolution = ResolveValueFilter(definition.Right, this.resolvedValue);
                resolveFilter.StringForResolution = stringForResolution;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueForEachLdapChildInitialized, "StringForResolution: '{0}'.", stringForResolution);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachLdap ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachLdap_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueForEachLdapChildCompleted);

            try
            {
                // Using the resolved filter, execute the LDAP query to determine if a conflict exists
                Definition definition = e.InstanceData as Definition;
                ResolveLookupString resolveFilter = e.Activity as ResolveLookupString;
                if (resolveFilter == null || definition == null)
                {
                    return;
                }

                if (ConflictExistsInLdap(definition.Left, resolveFilter.Resolved))
                {
                    this.ldapConflict = true;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueForEachLdapChildCompleted, "LDAP Conflict: '{0}'.", this.ldapConflict);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Decide CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Decide_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueDecideExecuteCode, "Resolved value: '{0}.' Conflict Filter XPath: '{1}'. Conflict Count: '{2}'. Ldap Conflict: '{3}'. UniquenessKey: '{4}'.", this.resolvedValue, this.XPathConflictFilter, this.FoundResources.Count, this.ldapConflict, this.uniquenessKey);

            try
            {
                if (!this.ldapConflict)
                {
                    bool conflictResourceFound = false;

                    if (this.FoundIds.Count > 0 && this.optimizeUniquenessKey)
                    {
                        conflictResourceFound = this.CheckConflictResourceFound();
                    }

                    if (this.FoundIds.Count == 0 || (this.optimizeUniquenessKey && !conflictResourceFound))
                    {
                        // If the value is unique,
                        // stop the loop and prepare to publish the unique value
                        this.unique = true;
                        List<UpdateLookupDefinition> lookupUpdates = new List<UpdateLookupDefinition>
                        {
                            new UpdateLookupDefinition(this.PublicationTarget, this.resolvedValue, UpdateMode.Modify)
                        };
                        this.Publish.UpdateLookupDefinitions = lookupUpdates;

                        Logger.Instance.WriteVerbose(EventIdentifier.GenerateUniqueValueDecideExecuteCode, "No conflict found for value: '{0}'. Returning it as the unique value.", this.resolvedValue);
                        return;
                    }
                }

                Logger.Instance.WriteVerbose(EventIdentifier.GenerateUniqueValueDecideExecuteCode, "Conflict Found for value: '{0}'. UniquenessKey: '{1}'. ValueExpression Index: '{2}'.", this.resolvedValue, this.uniquenessKey, this.valueIndex);

                // Always reset the ldap conflict flag to ensure the next value
                // is properly evaluated
                this.ldapConflict = false;

                bool resetUniquenessKeyFilter;

                // If the value is not unique, we need to decide on the next value to attempt
                // If we have not yet exhausted the value expression list, move on to the next value
                // Otherwise, increment the uniqueness key on the last resort value expression
                if (this.valueIndex + 1 < this.ValueExpressions.Count)
                {
                    this.valueIndex += 1;

                    // We are evaluating if the XPath filter makes use of starts-with() function. 
                    // we'll always modify the XPath filter to be e.g. /Person[starts-with(AccountName, 'TestUser')] 
                    // instead of /Person[starts-with(AccountName, 'TestUser2')]
                    resetUniquenessKeyFilter = this.valueIndex + 1 == this.ValueExpressions.Count && this.optimizeUniquenessKey;
                }
                else
                {
                    this.uniquenessKey += 1;

                    // if we are incrementing the last resort value expression and the XPath filter makes use of starts-with() function. 
                    // so we'll need to modify the XPath filter to be e.g. /Person[starts-with(AccountName, 'TestUser')] 
                    // instead of /Person[starts-with(AccountName, 'TestUser2')]
                    resetUniquenessKeyFilter = this.optimizeUniquenessKey;

                    if (this.optimizeUniquenessKey && this.FoundIds.Count > 1)
                    {
                        this.RepositionUniquenessKey();
                    }
                }

                // Resolve the new value and use it to resolve the conflict filter which will verify its uniqueness
                object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.ResolveUniquenessKey(this.ValueExpressions[this.valueIndex].ToString(), this.uniquenessKey, false));
                if (resolved != null)
                {
                    this.resolvedValue = resolved.ToString();
                }

                // Format the XPath conflict filter appropriately using the resolved value if the UniquenessKey Filter was reset
                if (resetUniquenessKeyFilter)
                {
                    resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.ResolveUniquenessKey(this.ValueExpressions[this.valueIndex].ToString(), this.uniquenessKey, true));
                    this.resolvedValueWithoutUniquenessSeed = resolved as string;
                    this.XPathConflictFilter = ResolveValueFilter(this.ConflictFilter, this.resolvedValueWithoutUniquenessSeed);
                }
                else
                {
                    this.resolvedValueWithoutUniquenessSeed = null;
                    this.XPathConflictFilter = ResolveValueFilter(this.ConflictFilter, this.resolvedValue);
                }

                // If we've tried over MaxLoopCount values, assume that we're in a non-terminating loop and kill the activity
                // This will protect against misconfiguration
                this.iterations += 1;
                if (this.iterations > this.maxLoopCount)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.GenerateUniqueValueDecideExecuteCodeExceededMaxLoopCountError, new WorkflowActivityLibraryException(Messages.GenerateUniqueValue_ExceededMaxLoopCountError, this.maxLoopCount));
                }
                else if (this.iterations % 20 == 0)
                {
                    Logger.Instance.WriteWarning(EventIdentifier.GenerateUniqueValueDecideExecuteCodeLoopCountWarning, Messages.GenerateUniqueValue_LoopCountWarning, this.iterations, this.maxLoopCount);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueDecideExecuteCode, "Unique: '{0}'. Conflict Filter XPath: '{1}'. UniquenessKey: '{2}'.", this.unique, this.XPathConflictFilter, this.uniquenessKey);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the WhileNotUnique condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void WhileNotUnique_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueWhileNotUniqueCondition);

            try
            {
                e.Result = !this.unique;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueWhileNotUniqueCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActivityExecutionConditionSatisfied condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActivityExecutionConditionSatisfied_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.GenerateUniqueValueActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'.", this.ActivityExecutionCondition);

            try
            {
                // Determine if requests should be submitted based on whether or not a condition was supplied
                // and if that condition resolves to true
                if (string.IsNullOrEmpty(this.ActivityExecutionCondition))
                {
                    e.Result = true;
                }
                else
                {
                    object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.ActivityExecutionCondition);
                    if (resolved is bool && (bool)resolved)
                    {
                        e.Result = true;
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.GenerateUniqueValueActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ActivityExecutionCondition, e.Result);
            }
        }

        #endregion

        #endregion
    }
}