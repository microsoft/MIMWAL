//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateResources.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// UpdateResources Activity 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;

    #endregion

    /// <summary>
    /// Updates resources
    /// </summary>
    public partial class UpdateResources : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty UpdatesTableProperty =
            DependencyProperty.Register("UpdatesTable", typeof(Hashtable), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AdvancedProperty =
            DependencyProperty.Register("Advanced", typeof(bool), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResourcesProperty =
            DependencyProperty.Register("QueryResources", typeof(bool), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueriesTableProperty =
            DependencyProperty.Register("QueriesTable", typeof(Hashtable), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty IterationProperty =
            DependencyProperty.Register("Iteration", typeof(string), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorTypeProperty =
            DependencyProperty.Register("ActorType", typeof(ActorType), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorStringProperty =
            DependencyProperty.Register("ActorString", typeof(string), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApplyAuthorizationPolicyProperty =
            DependencyProperty.Register("ApplyAuthorizationPolicy", typeof(bool), typeof(UpdateResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ResolveDynamicGrammarProperty =
            DependencyProperty.Register("ResolveDynamicGrammar", typeof(bool), typeof(UpdateResources));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The lookup update definitions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<UpdateLookupDefinition> LookupUpdates;

        /// <summary>
        /// The query definitions
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Definition> Queries;

        /// <summary>
        /// The value.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public object Value;

        /// <summary>
        /// The value expressions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<string, object> ValueExpressions;

        /// <summary>
        /// The Dynamic Strings for Resolution
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<string> DynamicStringsForResolution = new List<string>();

        /// <summary>
        /// The update definitions
        /// </summary>
        private List<Definition> updates = new List<Definition>();

        /// <summary>
        /// The number of iterations preformed so far
        /// </summary>
        private int iterations;

        /// <summary>
        /// Break iteration if true
        /// </summary>
        private bool breakIteration;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResources"/> class.
        /// </summary>
        public UpdateResources()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesConstructor);

            try
            {
                this.InitializeComponent();

                if (this.ActivityExpressionEvaluator == null)
                {
                    this.ActivityExpressionEvaluator = new ExpressionEvaluator();
                }

                if (this.LookupUpdates == null)
                {
                    this.LookupUpdates = new List<UpdateLookupDefinition>();
                }

                if (this.Queries == null)
                {
                    this.Queries = new List<Definition>();
                }

                if (this.ValueExpressions == null)
                {
                    this.ValueExpressions = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesConstructor);
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
        /// Gets or sets the updates hash table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("UpdatesTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable UpdatesTable
        {
            get
            {
                return (Hashtable)this.GetValue(UpdatesTableProperty);
            }

            set
            {
                this.SetValue(UpdatesTableProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether advanced checkbox is checked.
        /// </summary>
        [Description("Advanced")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Advanced
        {
            get
            {
                return (bool)this.GetValue(AdvancedProperty);
            }

            set
            {
                this.SetValue(AdvancedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to query resources.
        /// </summary>
        [Description("QueryResources")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool QueryResources
        {
            get
            {
                return (bool)this.GetValue(QueryResourcesProperty);
            }

            set
            {
                this.SetValue(QueryResourcesProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the queries hash table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("QueriesTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable QueriesTable
        {
            get
            {
                return (Hashtable)this.GetValue(QueriesTableProperty);
            }

            set
            {
                this.SetValue(QueriesTableProperty, value);
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
        /// Gets or sets the iteration.
        /// </summary>
        [Description("Iteration")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Iteration
        {
            get
            {
                return (string)this.GetValue(IterationProperty);
            }

            set
            {
                this.SetValue(IterationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the actor.
        /// </summary>
        [Description("ActorType")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ActorType ActorType
        {
            get
            {
                return (ActorType)this.GetValue(ActorTypeProperty);
            }

            set
            {
                this.SetValue(ActorTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the actor string.
        /// </summary>
        [Description("ActorString")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ActorString
        {
            get
            {
                return (string)this.GetValue(ActorStringProperty);
            }

            set
            {
                this.SetValue(ActorStringProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to apply authorization policy.
        /// </summary>
        [Description("ApplyAuthorizationPolicy")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ApplyAuthorizationPolicy
        {
            get
            {
                return (bool)this.GetValue(ApplyAuthorizationPolicyProperty);
            }

            set
            {
                this.SetValue(ApplyAuthorizationPolicyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to apply authorization policy.
        /// </summary>
        [Description("Resolve Dynamic Grammar such as [//Queries/EmailTemplate/EmailSubject] or proper replacement of EvaluateExpression")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ResolveDynamicGrammar
        {
            get
            {
                return (bool)this.GetValue(ResolveDynamicGrammarProperty);
            }

            set
            {
                this.SetValue(ResolveDynamicGrammarProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.UpdateResourcesExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesPrepareExecuteCode);

            try
            {
                // If advanced options were not specified,
                // clear any supplied advanced settings so they do not impact activity execution
                if (!this.Advanced)
                {
                    this.QueryResources = false;
                    this.ActivityExecutionCondition = null;
                    this.Iteration = null;
                    this.ActorType = ActorType.Service;
                    this.ApplyAuthorizationPolicy = false;
                    this.ResolveDynamicGrammar = false;
                }

                // If the activity is configured to query for resources,
                // convert the queries hash table to a list of definitions that will feed the activity responsible
                // for their execution
                if (this.QueryResources && this.QueriesTable != null && this.QueriesTable.Count > 0)
                {
                    DefinitionsConverter queriesConverter = new DefinitionsConverter(this.QueriesTable);
                    this.Queries = queriesConverter.Definitions;
                }

                // If the activity is configured for iteration or conditional execution, parse the associated expressions
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Iteration);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);

                // Definitions are supplied to the workflow activity in the form of a hash table
                // This is necessary due to deserialization issues with lists and custom classes
                // Convert the updates hash table to a list of definitions that is easier to work with
                DefinitionsConverter updatesConverter = new DefinitionsConverter(this.UpdatesTable);
                this.updates = updatesConverter.Definitions;

                // Load each source expression into the evaluator so associated lookups can be loaded into the cache for resolution
                // For updates, the left side of the definition represents the source expression
                foreach (Definition updateDefinition in this.updates)
                {
                    this.ActivityExpressionEvaluator.ParseExpression(updateDefinition.Left);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesPrepareExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareIteration CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareIteration_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesPrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'.", this.Iteration, this.ActivityExecutionCondition);

            bool submitRequests = false;
            List<object> iterationValues = new List<object>();
            try
            {
                // Determine if requests should be submitted based on the configuration for conditional execution
                // If a condition was specified and that condition resolves to false, no values will be added for iteration
                if (string.IsNullOrEmpty(this.ActivityExecutionCondition))
                {
                    submitRequests = true;
                }
                else
                {
                    object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.ActivityExecutionCondition);
                    if (resolved is bool && (bool)resolved)
                    {
                        submitRequests = true;
                    }
                }

                if (!submitRequests)
                {
                    return;
                }

                // If the activity is not configured for iteration, a null value is added to the list
                // to ensure a single update is submitted based on the update definitions
                if (string.IsNullOrEmpty(this.Iteration))
                {
                    iterationValues.Add(null);
                }
                else
                {
                    // If the activity is configured for iteration, resolve the associated expression
                    object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.Iteration);

                    // If the expression resolved to one or more values, add those values to the list for iteration
                    if (resolved != null)
                    {
                        if (resolved.GetType().IsGenericType && resolved.GetType().GetGenericTypeDefinition() == typeof(List<>))
                        {
                            iterationValues.AddRange(((IEnumerable)resolved).Cast<object>());
                        }
                        else
                        {
                            iterationValues.Add(resolved);
                        }
                    }

                    // Pull any [//Value] or [//WorkflowData] expressions from the expression evaluator's lookup cache for
                    // resolution during iteration
                    foreach (string key in from key in this.ActivityExpressionEvaluator.LookupCache.Keys let lookup = new LookupEvaluator(key) where lookup.Parameter == LookupParameter.Value || lookup.Parameter == LookupParameter.WorkflowData select key)
                    {
                        this.ValueExpressions.Add(key, null);
                    }
                }

                // Add the iteration values to the replicator activity if requests should be submitted
                this.ForEachIteration.InitialChildData = iterationValues;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesPrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'. Submit Request: '{2}'. Total Iterations: '{3}'.", this.Iteration, this.ActivityExecutionCondition, submitRequests, iterationValues.Count);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);

            try
            {
                // Get the instance value so it can be used to resolve associated expressions
                // and clear previous resolutions
                this.Value = e.InstanceData;

                // Increment current iteration count
                this.iterations += 1;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesForEachIterationChildCompleted, "Iteration: '{0}' of '{1}'. ", this.iterations, this.ForEachIteration.InitialChildData.Count);

            try
            {
                var variableCache = this.ActivityExpressionEvaluator.VariableCache;
                this.breakIteration = Convert.ToBoolean(variableCache[ExpressionEvaluator.ReservedVariableBreakIteration], CultureInfo.InvariantCulture);

                // Reset to the original update definitions as ResolveDynamicGrammar updates the working definitions.
                // The If check is really not needed.
                if (this.ResolveDynamicGrammar)
                {
                    DefinitionsConverter updatesConverter = new DefinitionsConverter(this.UpdatesTable);
                    this.updates = updatesConverter.Definitions;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesForEachIterationChildCompleted, "Iteration: '{0}' of '{1}'. Break Iteration '{2}'.", this.iterations, this.ForEachIteration.InitialChildData.Count, this.breakIteration);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareDynamicResolution CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareDynamicGrammarResolution_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesForEachDynamicStringForResolutionInitialized);

            this.DynamicStringsForResolution = new List<string>(); // clear any previous values
            try
            {
                // Load resolved value expressions to the expression evaluator
                foreach (string key in this.ValueExpressions.Keys)
                {
                    this.ActivityExpressionEvaluator.LookupCache[key] = this.ValueExpressions[key];
                }

                var newUpdates = new List<Definition>();

                // Clear the variable cache for the expression evaluator
                List<string> variables = this.ActivityExpressionEvaluator.VariableCache.Keys.ToList();
                foreach (string variable in variables)
                {
                    this.ActivityExpressionEvaluator.VariableCache[variable] = null;
                }

                foreach (Definition updateDefinition in this.updates)
                {
                    // Determine if we are targeting a variable
                    // If so, publish the variable
                    bool targetVariable = ExpressionEvaluator.DetermineParameterType(updateDefinition.Right) == ParameterType.Variable;
                    if (targetVariable)
                    {
                        object resolved = this.ActivityExpressionEvaluator.ResolveExpression(updateDefinition.Left);
                        this.ActivityExpressionEvaluator.PublishVariable(updateDefinition.Right, resolved, UpdateMode.Modify);
                    }
                }

                foreach (Definition updateDefinition in this.updates)
                {
                    // Resolve the source expression, including any functions or concatenation,
                    // to retrieve the typed value that should be assigned to the target attribute
                    object resolved = this.ActivityExpressionEvaluator.ResolveExpression(updateDefinition.Left);

                    if (resolved is string)
                    {
                        string updateDefinitionLeft = resolved as string;
                        newUpdates.Add(new Definition(updateDefinitionLeft, updateDefinition.Right, updateDefinition.Check));

                        if (!this.ActivityExpressionEvaluator.ParseIfExpression(updateDefinitionLeft))
                        {
                            this.DynamicStringsForResolution.Add(updateDefinitionLeft);
                        }
                    }
                    else
                    {
                        newUpdates.Add(updateDefinition);
                    }
                }

                this.updates = newUpdates;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesForEachDynamicStringForResolutionInitialized, "DynamicStringsForResolution Count: '{0}'.", this.DynamicStringsForResolution.Count);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachDynamicStringForResolution ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachDynamicStringForResolution_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesForEachDynamicStringForResolutionChildInitialized);

            string stringForResolution = e.InstanceData as string;
            try
            {
                ResolveLookupString resolveDynamicLookupString = e.Activity as ResolveLookupString;
                if (resolveDynamicLookupString == null || string.IsNullOrEmpty(stringForResolution))
                {
                    return;
                }

                resolveDynamicLookupString.StringForResolution = stringForResolution;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesForEachDynamicStringForResolutionChildInitialized, "StringForResolution: '{0}'.", stringForResolution);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachDynamicStringForResolution ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachDynamicStringForResolution_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesForEachDynamicStringForResolutionChildCompleted);

            string stringForResolution = e.InstanceData as string;
            string resolved = null;
            try
            {
                // Using the resolved filter, execute the LDAP query to determine if a conflict exists
                ResolveLookupString resolveDynamicLookupString = e.Activity as ResolveLookupString;
                if (resolveDynamicLookupString == null || string.IsNullOrEmpty(stringForResolution))
                {
                    return;
                }

                resolved = resolveDynamicLookupString.Resolved;

                // Load resolved value expressions to the expression evaluator
                this.ActivityExpressionEvaluator.LookupCache[stringForResolution] = resolved;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesForEachDynamicStringForResolutionChildCompleted, "StringForResolution: '{0}'. Resolved: '{1}'.", stringForResolution, resolved);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareUpdate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareUpdate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesPrepareUpdateExecuteCode);

            try
            {
                // Load resolved value expressions to the expression evaluator
                foreach (string key in this.ValueExpressions.Keys)
                {
                    this.ActivityExpressionEvaluator.LookupCache[key] = this.ValueExpressions[key];
                }

                // Clear the variable cache for the expression evaluator
                List<string> variables = this.ActivityExpressionEvaluator.VariableCache.Keys.ToList();
                foreach (string variable in variables)
                {
                    this.ActivityExpressionEvaluator.VariableCache[variable] = null;
                }

                // Loop through each attribute update definition to build the
                // update resource parameters which will be used to update each target resource
                this.LookupUpdates = new List<UpdateLookupDefinition>();
                foreach (Definition updateDefinition in this.updates)
                {
                    // Resolve the source expression, including any functions or concatenation,
                    // to retrieve the typed value that should be assigned to the target attribute
                    object resolved = null;
                    if (!ExpressionEvaluator.IsExpression(updateDefinition.Left))
                    {
                        // This is a dynamic string for resolution already resolved
                        // so just retrive the value from cache directly
                        resolved = this.ActivityExpressionEvaluator.LookupCache[updateDefinition.Left];
                    }
                    else
                    {
                        resolved = this.ActivityExpressionEvaluator.ResolveExpression(updateDefinition.Left);
                    }

                    // Determine if we are targeting a variable
                    // If not, assume we are targeting an expression which should result in requests or update
                    // to the workflow dictionary
                    bool targetVariable = ExpressionEvaluator.DetermineParameterType(updateDefinition.Right) == ParameterType.Variable;

                    // Only create an update lookup definition if the value is not null, or if the
                    // update definition is configured to allow null values to be transferred to the target(s)
                    if (resolved == null && updateDefinition.Check)
                    {
                        if (targetVariable)
                        {
                            this.ActivityExpressionEvaluator.PublishVariable(updateDefinition.Right, null, UpdateMode.Modify);
                        }
                        else
                        {
                            this.LookupUpdates.Add(new UpdateLookupDefinition(updateDefinition.Right, null, UpdateMode.Modify));
                        }
                    }
                    else if (resolved != null)
                    {
                        if (resolved.GetType() == typeof(InsertedValuesCollection))
                        {
                            // If the resolved object is an InsertedValues collection, the source for the update definition includes the InsertValues function
                            // All associated values should be added to the target
                            foreach (object o in (InsertedValuesCollection)resolved)
                            {
                                if (targetVariable)
                                {
                                    this.ActivityExpressionEvaluator.PublishVariable(updateDefinition.Right, o, UpdateMode.Insert);
                                }
                                else
                                {
                                    this.LookupUpdates.Add(new UpdateLookupDefinition(updateDefinition.Right, o, UpdateMode.Insert));
                                }
                            }
                        }
                        else if (resolved.GetType() == typeof(RemovedValuesCollection))
                        {
                            // If the resolved object is a RemovedValues collection, the source for the update definition includes the RemoveValues function
                            // All associated values should be removed from the target
                            foreach (object o in (RemovedValuesCollection)resolved)
                            {
                                if (targetVariable)
                                {
                                    this.ActivityExpressionEvaluator.PublishVariable(updateDefinition.Right, o, UpdateMode.Remove);
                                }
                                else
                                {
                                    this.LookupUpdates.Add(new UpdateLookupDefinition(updateDefinition.Right, o, UpdateMode.Remove));
                                }
                            }
                        }
                        else
                        {
                            // For all other conditions, update the variable or build a new update request parameter for the target attribute
                            if (targetVariable)
                            {
                                this.ActivityExpressionEvaluator.PublishVariable(updateDefinition.Right, resolved, UpdateMode.Modify);
                            }
                            else
                            {
                                this.LookupUpdates.Add(new UpdateLookupDefinition(updateDefinition.Right, resolved, UpdateMode.Modify));
                            }
                        }
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesPrepareUpdateExecuteCode);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the UntilCondition event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_UntilCondition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesForEachIterationUntilCondition, "Iteration: '{0}' of '{1}'. ", this.iterations, this.ForEachIteration.InitialChildData == null ? 0 : this.ForEachIteration.InitialChildData.Count);

            int maxIterations = 0;
            try
            {
                maxIterations = this.ForEachIteration.InitialChildData == null ? 0 : this.ForEachIteration.InitialChildData.Count;
                if (this.iterations == maxIterations || this.breakIteration)
                {
                    e.Result = true;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesForEachIterationUntilCondition, "Iteration: '{0}' of '{1}'. Condition evaluated '{2}'.", this.iterations, maxIterations, e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActorIsNotValueExpression Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActorIsNotValueExpression_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesActorIsNotValueExpressionCondition);

            try
            {
                e.Result = !ExpressionEvaluator.IsValueExpression(this.ActorString);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesActorIsNotValueExpressionCondition, "Condition evaluated '{0}'. Actor String: '{1}'.", e.Result, this.ActorString);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ResolveDynamicGrammar Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void DynamicGrammarResolutionNeed_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateResourcesDynamicGrammarResolutionNeedCondition);

            try
            {
                e.Result = this.ResolveDynamicGrammar;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateResourcesDynamicGrammarResolutionNeedCondition, "Condition evaluated '{0}'. Actor String: '{1}'.", e.Result, this.ActorString);
            }
        }

        #endregion Conditions

        #endregion
    }
}