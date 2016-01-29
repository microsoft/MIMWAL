//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateResource.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// CreateResource Activity 
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
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Creates resource
    /// </summary>
    public partial class CreateResource : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ResourceTypeProperty =
            DependencyProperty.Register("ResourceType", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AdvancedProperty =
            DependencyProperty.Register("Advanced", typeof(bool), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty CreatedResourceIdTargetProperty =
            DependencyProperty.Register("CreatedResourceIdTarget", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty CheckForConflictProperty =
            DependencyProperty.Register("CheckForConflict", typeof(bool), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConflictFilterProperty =
            DependencyProperty.Register("ConflictFilter", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty FailOnConflictProperty =
            DependencyProperty.Register("FailOnConflict", typeof(bool), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConflictingResourceIdTargetProperty =
            DependencyProperty.Register("ConflictingResourceIdTarget", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResourcesProperty =
            DependencyProperty.Register("QueryResources", typeof(bool), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueriesTableProperty =
            DependencyProperty.Register("QueriesTable", typeof(Hashtable), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AttributesTableProperty =
            DependencyProperty.Register("AttributesTable", typeof(Hashtable), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty IterationProperty =
            DependencyProperty.Register("Iteration", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorTypeProperty =
            DependencyProperty.Register("ActorType", typeof(ActorType), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorStringProperty =
            DependencyProperty.Register("ActorString", typeof(string), typeof(CreateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApplyAuthorizationPolicyProperty =
            DependencyProperty.Register("ApplyAuthorizationPolicy", typeof(bool), typeof(CreateResource));

        #endregion

        #region Declarations

        /// <summary>
        /// The list of unique identifier of conflicts
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> Conflicts;

        /// <summary>
        /// The create request parameters.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public CreateRequestParameter[] CreateParameters;

        /// <summary>
        /// The created resource identifier.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid CreatedResourceId;

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The update lookup definitions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<UpdateLookupDefinition> LookupUpdates;

        /// <summary>
        /// The query definitions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Definition> Queries;

        /// <summary>
        /// The current iterated value.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public object Value;

        /// <summary>
        /// The value ([//Value]) expressions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<string, object> ValueExpressions;

        /// <summary>
        /// The conflict found during one or more iterations.
        /// </summary>
        private bool activityFoundConflict;

        /// <summary>
        /// The attribute definitions
        /// </summary>
        private List<Definition> attributes = new List<Definition>();

        /// <summary>
        /// The conflict found during current iteration.
        /// </summary>
        private bool conflict;

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
        /// Initializes a new instance of the <see cref="CreateResource"/> class.
        /// </summary>
        public CreateResource()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceConstructor);

            try
            {
                this.InitializeComponent();

                if (this.Conflicts == null)
                {
                    this.Conflicts = new List<Guid>();
                }

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
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceConstructor);
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
        /// Gets or sets the type of the resource to be created.
        /// </summary>
        [Description("ResourceType")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ResourceType
        {
            get
            {
                return (string)this.GetValue(ResourceTypeProperty);
            }

            set
            {
                this.SetValue(ResourceTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether advanced checkbox is selected.
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
        /// Gets or sets the target of the created resource identifier.
        /// </summary>
        [Description("CreatedResourceIdTarget")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CreatedResourceIdTarget
        {
            get
            {
                return (string)this.GetValue(CreatedResourceIdTargetProperty);
            }

            set
            {
                this.SetValue(CreatedResourceIdTargetProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to check for conflict.
        /// </summary>
        [Description("CheckForConflict")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CheckForConflict
        {
            get
            {
                return (bool)this.GetValue(CheckForConflictProperty);
            }

            set
            {
                this.SetValue(CheckForConflictProperty, value);
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
        /// Gets or sets a value indicating whether to fail on conflict.
        /// </summary>
        [Description("FailOnConflict")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool FailOnConflict
        {
            get
            {
                return (bool)this.GetValue(FailOnConflictProperty);
            }

            set
            {
                this.SetValue(FailOnConflictProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the target for conflicting resource identifier.
        /// </summary>
        [Description("ConflictingResourceIdTarget")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ConflictingResourceIdTarget
        {
            get
            {
                return (string)this.GetValue(ConflictingResourceIdTargetProperty);
            }

            set
            {
                this.SetValue(ConflictingResourceIdTargetProperty, value);
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
        /// Gets or sets the attributes table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("AttributesTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable AttributesTable
        {
            get
            {
                return (Hashtable)this.GetValue(AttributesTableProperty);
            }

            set
            {
                this.SetValue(AttributesTableProperty, value);
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

        #endregion

        #region Methods

        /// <summary>
        /// Executes the activity.
        /// </summary>
        /// <param name="executionContext">The execution context of the activity.</param>
        /// <returns>The <see cref="ActivityExecutionStatus"/> of the activity after executing the activity.</returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.CreateResourceExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ParseDefinitions CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ParseDefinitions_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceParseDefinitionsExecuteCode);

            try
            {
                // If advanced options were not specified,
                // clear any supplied advanced settings so they do not impact activity execution
                if (!this.Advanced)
                {
                    this.QueryResources = false;
                    this.CreatedResourceIdTarget = null;
                    this.CheckForConflict = false;
                    this.ActivityExecutionCondition = null;
                    this.Iteration = null;
                    this.ActorType = ActorType.Service;
                    this.ApplyAuthorizationPolicy = false;
                }

                // Definitions are supplied to the workflow activity in the form of a hash table
                // This is necessary due to deserialization issues with lists and custom classes
                // Convert the attributes hash table to a list of definitions that is easier to work with
                DefinitionsConverter updatesConverter = new DefinitionsConverter(this.AttributesTable);
                this.attributes = updatesConverter.Definitions;

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

                // Load each source expression into the evaluator so associated lookups can be loaded into the cache for resolution
                // For attributes, the left side of the definition represents the source expression
                foreach (Definition attributeDefinition in this.attributes)
                {
                    this.ActivityExpressionEvaluator.ParseExpression(attributeDefinition.Left);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceParseDefinitionsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareIteration CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareIteration_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourcePrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'.", this.Iteration, this.ActivityExecutionCondition);

            bool submitRequests = false;
            List<object> iterationValues = new List<object>();
            try
            {
                // Determine if requests should be submitted based on whether or not a condition was supplied
                // and if that condition resolves to true
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

                    // Pull any [//Value] expressions from the expression evaluator's lookup cache for
                    // resolution during iteration
                    foreach (string key in from key in this.ActivityExpressionEvaluator.LookupCache.Keys let lookup = new LookupEvaluator(key) where lookup.Parameter == LookupParameter.Value select key)
                    {
                        this.ValueExpressions.Add(key, null);
                    }
                }

                // Add the iteration values to the replicator activity if requests should be submitted
                this.ForEachIteration.InitialChildData = iterationValues;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourcePrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'. Submit Request: '{2}'. Total Iterations: '{3}'.", this.Iteration, this.ActivityExecutionCondition, submitRequests, iterationValues.Count);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceForEachIterationChildCompleted, "Iteration: '{0}' of '{1}'. ", this.iterations, this.ForEachIteration.InitialChildData.Count);

            try
            {
                var variableCache = this.ActivityExpressionEvaluator.VariableCache;
                this.breakIteration = Convert.ToBoolean(variableCache[ExpressionEvaluator.ReservedVariableBreakIteration], CultureInfo.InvariantCulture);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceForEachIterationChildCompleted, "Iteration: '{0}' of '{1}'. Break Iteration '{2}'.", this.iterations, this.ForEachIteration.InitialChildData.Count, this.breakIteration);
            }
        }

        /// <summary>
        /// Handles the UntilCondition event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_UntilCondition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceForEachIterationUntilCondition, "Iteration: '{0}' of '{1}'. ", this.iterations, this.ForEachIteration.InitialChildData == null ? 0 : this.ForEachIteration.InitialChildData.Count);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceForEachIterationUntilCondition, "Iteration: '{0}' of '{1}'. Condition evaluated '{2}'.", this.iterations, maxIterations, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the EvaluateResults CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EvaluateResults_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceEvaluateResultsExecuteCode);

            try
            {
                // Determine if a conflict was identified
                if (this.Conflicts.Count == 0)
                {
                    this.conflict = false;
                }
                else
                {
                    this.conflict = true;
                    this.activityFoundConflict = true;

                    // Determine if the conflict should and can be published
                    // We cannot publish the conflicting resource if we are iterating (indicated by the presense of an iteration value)
                    if (string.IsNullOrEmpty(this.ConflictingResourceIdTarget) || this.Value != null)
                    {
                        return;
                    }

                    // If configured to publish the conflict,
                    // build a new update lookup definition and add it to the list of updates for the publish activity
                    this.LookupUpdates.Add(new UpdateLookupDefinition(this.ConflictingResourceIdTarget, this.FindConflict.FoundIds[0], UpdateMode.Modify));
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceEvaluateResultsExecuteCode, "Conflict: '{0}'. Activity Found Conflict: '{1}'.", this.conflict, this.activityFoundConflict);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareCreate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareCreate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourcePrepareCreateExecuteCode, "ResourceType: '{0}'.", this.ResourceType);

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

                // Create a new collection of create request parameters and
                // specify the resource type for the new resource
                List<CreateRequestParameter> createParameters = new List<CreateRequestParameter>
                    {
                        new CreateRequestParameter("ObjectType", this.ResourceType)
                    };

                // Loop through each attribute assignment definition to build the
                // create resource parameters which will be used to create the new resource
                foreach (Definition attributeDefinition in this.attributes)
                {
                    // Resolve the source expression, including any functions or concatenation,
                    // to retrieve the typed value that should be assigned to the target attribute
                    object resolved = this.ActivityExpressionEvaluator.ResolveExpression(attributeDefinition.Left);

                    // Determine if we are targeting a variable
                    // If not, assume we are targeting an attribute which should result in a create request parameter
                    bool targetVariable = false;
                    try
                    {
                        targetVariable = ExpressionEvaluator.DetermineParameterType(attributeDefinition.Right, true) == ParameterType.Variable;
                    }
                    catch (WorkflowActivityLibraryException)
                    {
                    }

                    if (resolved == null)
                    {
                        continue;
                    }

                    if (resolved.GetType() == typeof(InsertedValuesCollection))
                    {
                        // If the resolved object is an InsertedValues collection, the source includes the InsertValues function
                        // All associated values should be added to the target variable
                        // If the target is an attribute, simply build a create request parameter for each
                        foreach (object o in (InsertedValuesCollection)resolved)
                        {
                            if (targetVariable)
                            {
                                this.ActivityExpressionEvaluator.PublishVariable(attributeDefinition.Right, o, UpdateMode.Insert);
                            }
                            else
                            {
                                createParameters.Add(new CreateRequestParameter(attributeDefinition.Right, o));
                            }
                        }
                    }
                    else if (resolved.GetType() == typeof(RemovedValuesCollection))
                    {
                        // If the resolved object is a RemovedValues collection, the source includes the RemoveValues function
                        // All associated values should be removed from the target, but only when the target is a variable
                        if (!targetVariable)
                        {
                            continue;
                        }

                        foreach (object o in (RemovedValuesCollection)resolved)
                        {
                            this.ActivityExpressionEvaluator.PublishVariable(attributeDefinition.Right, o, UpdateMode.Remove);
                        }
                    }
                    else if (resolved.GetType().IsGenericType && resolved.GetType().GetGenericTypeDefinition() == typeof(List<>))
                    {
                        // If the resolved object is a List<>, we are attempting to populate a multivalued attribute
                        // We need to create a new create request parameter for each value or write the list to the variable cache
                        if (targetVariable)
                        {
                            this.ActivityExpressionEvaluator.PublishVariable(attributeDefinition.Right, resolved, UpdateMode.Modify);
                        }
                        else
                        {
                            createParameters.AddRange(from object o in (IEnumerable)resolved select new CreateRequestParameter(attributeDefinition.Right, o));
                        }
                    }
                    else
                    {
                        // For all other conditions, build a new create request parameter for the target attribute or publish the variable
                        if (targetVariable)
                        {
                            this.ActivityExpressionEvaluator.PublishVariable(attributeDefinition.Right, resolved, UpdateMode.Modify);
                        }
                        else
                        {
                            createParameters.Add(new CreateRequestParameter(attributeDefinition.Right, resolved));
                        }
                    }
                }

                // Publish the create parameters to the create resource activities
                this.CreateParameters = createParameters.ToArray();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourcePrepareCreateExecuteCode, "ResourceType: '{0}'.", this.ResourceType);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PublishCreated CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PublishCreated_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourcePublishCreatedExecuteCode, "CreatedResourceIdTarget: '{0}'. CreatedResourceId: '{1}'. Value: '{2}'.", this.CreatedResourceIdTarget, this.CreatedResourceId, this.Value);

            try
            {
                // If a target for the created resource id was supplied,
                // build a new update lookup definition and add it to the list of updates for the publish activity
                // We can not publish the created resource when the child request is submitted to authorization, or if we are iterating
                if (!string.IsNullOrEmpty(this.CreatedResourceIdTarget) && this.CreatedResourceId != Guid.Empty && this.Value == null)
                {
                    this.LookupUpdates.Add(new UpdateLookupDefinition(this.CreatedResourceIdTarget, this.CreatedResourceId, UpdateMode.Modify));
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourcePublishCreatedExecuteCode, "CreatedResourceIdTarget: '{0}'. CreatedResourceId: '{1}'. Value: '{2}'.", this.CreatedResourceIdTarget, this.CreatedResourceId, this.Value);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Finish CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Finish_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceFinishExecuteCode, "ActivityFoundConflict: '{0}'. FailOnConflict: '{1}'.", this.activityFoundConflict, this.FailOnConflict);

            try
            {
                // If configured to fail on conflict and a conflict exists, throw an exception
                // This is done as the last step to allow for the publication of a conflict
                if (this.activityFoundConflict && this.FailOnConflict)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.CreateResourceFinishExecuteCodeFailedOnConflictError, new WorkflowActivityLibraryException(Messages.CreateResource_FailedOnConflictError));
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceFinishExecuteCode, "ActivityFoundConflict: '{0}'. FailOnConflict: '{1}'.", this.activityFoundConflict, this.FailOnConflict);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the CheckConflict condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void CheckConflict_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceCheckConflictCondition);

            try
            {
                e.Result = this.CheckForConflict && !string.IsNullOrEmpty(this.ConflictFilter);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceCheckConflictCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the Unique condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void Unique_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceUniqueCondition);

            try
            {
                e.Result = !this.conflict;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceUniqueCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ContentToPublish condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ContentToPublish_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceContentToPublishCondition);

            try
            {
                e.Result = this.LookupUpdates.Count > 0;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceContentToPublishCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the Authorization condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void Authorization_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceAuthorizationCondition);

            try
            {
                e.Result = this.ApplyAuthorizationPolicy;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceAuthorizationCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActorIsNotValueExpression Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActorIsNotValueExpression_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.CreateResourceActorIsNotValueExpressionCondition);

            try
            {
                e.Result = !ExpressionEvaluator.IsValueExpression(this.ActorString);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.CreateResourceActorIsNotValueExpressionCondition, "Condition evaluated '{0}'. Actor String: '{1}'.", e.Result, this.ActorString);
            }
        }

        #endregion

        #endregion
    }
}