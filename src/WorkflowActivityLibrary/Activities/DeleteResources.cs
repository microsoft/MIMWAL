//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteResources.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DeleteResources Activity 
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
    using System.Linq;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;

    #endregion

    /// <summary>
    /// Deletes resources
    /// </summary>
    public partial class DeleteResources : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty TargetTypeProperty =
            DependencyProperty.Register("TargetType", typeof(DeleteResourcesTargetType), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty TargetExpressionProperty =
            DependencyProperty.Register("TargetExpression", typeof(string), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AdvancedProperty =
            DependencyProperty.Register("Advanced", typeof(bool), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty IterationProperty =
            DependencyProperty.Register("Iteration", typeof(string), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorTypeProperty =
            DependencyProperty.Register("ActorType", typeof(ActorType), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorStringProperty =
            DependencyProperty.Register("ActorString", typeof(string), typeof(DeleteResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApplyAuthorizationPolicyProperty =
            DependencyProperty.Register("ApplyAuthorizationPolicy", typeof(bool), typeof(DeleteResources));

        #endregion

        #region Declarations

        /// <summary>
        /// The current target.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid Target;

        /// <summary>
        /// The list of targets.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> Targets;

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

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

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteResources"/> class.
        /// </summary>
        public DeleteResources()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesConstructor);

            try
            {
                this.InitializeComponent();

                if (this.Targets == null)
                {
                    this.Targets = new List<Guid>();
                }

                if (this.ActivityExpressionEvaluator == null)
                {
                    this.ActivityExpressionEvaluator = new ExpressionEvaluator();
                }

                if (this.ValueExpressions == null)
                {
                    this.ValueExpressions = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesConstructor);
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
        /// Gets or sets the type of the target.
        /// </summary>
        [Description("TargetType")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DeleteResourcesTargetType TargetType
        {
            get
            {
                return (DeleteResourcesTargetType)this.GetValue(TargetTypeProperty);
            }

            set
            {
                this.SetValue(TargetTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the target expression.
        /// </summary>
        /// <value>
        /// The target expression.
        /// </value>
        [Description("TargetExpression")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TargetExpression
        {
            get
            {
                return (string)this.GetValue(TargetExpressionProperty);
            }

            set
            {
                this.SetValue(TargetExpressionProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.DeleteResourcesExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareResolve CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareResolve_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesPrepareResolveExecuteCode);

            try
            {
                // If advanced options were not specified,
                // clear any supplied advanced settings so they do not impact activity execution
                if (!this.Advanced)
                {
                    this.ActivityExecutionCondition = null;
                    this.Iteration = null;
                    this.ActorType = ActorType.Service;
                    this.ApplyAuthorizationPolicy = false;
                }

                // Add the target expression to the lookup dictionary to be resolved
                if (this.TargetType == DeleteResourcesTargetType.ResolveTarget && !string.IsNullOrEmpty(this.TargetExpression))
                {
                    this.ActivityExpressionEvaluator.ParseExpression(this.TargetExpression);
                }

                // If the activity is configured for iteration or conditional execution, parse the associated expressions
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Iteration);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesPrepareResolveExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareIteration CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareIteration_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesPrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'.", this.Iteration, this.ActivityExecutionCondition);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesPrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'. Submit Request: '{2}'. Total Iterations: '{3}'.", this.Iteration, this.ActivityExecutionCondition, submitRequests, iterationValues.Count);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);

            try
            {
                // Get the instance value so it can be used to resolve associated expressions
                // and clear previous resolutions
                this.Value = e.InstanceData;
            }
            finally
            {
                Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareTarget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareTarget_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesPrepareTargetExecuteCode);

            try
            {
                // Load resolved value expressions to the expression evaluator
                foreach (string key in this.ValueExpressions.Keys)
                {
                    this.ActivityExpressionEvaluator.LookupCache[key] = this.ValueExpressions[key];
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesPrepareTargetExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareDelete CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareDelete_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesPrepareDeleteExecuteCode, "TargetType: '{0}'.", this.TargetType);

            try
            {
                // Determine the resources to be deleted based on the target type
                switch (this.TargetType)
                {
                    case DeleteResourcesTargetType.WorkflowTarget:
                        // If the activity is configured to delete the workflow target,
                        // load the target from the parent workflow
                        SequentialWorkflow parentWorkflow;
                        if (SequentialWorkflow.TryGetContainingWorkflow(this, out parentWorkflow))
                        {
                            this.Targets.Add(parentWorkflow.TargetId);
                        }

                        break;
                    case DeleteResourcesTargetType.SearchForTarget:
                        // If the activity is configured to search for the target(s),
                        // the targets should be the bound results of the find resources activity
                        break;
                    case DeleteResourcesTargetType.ResolveTarget:
                        // If the activity is configured to resolve target(s),
                        // verify that the supplied lookup resolved to a Guid or List<Guid> and assign the resolved
                        // values as targets
                        if (this.Resolve.Lookups.ContainsKey(this.TargetExpression) &&
                            this.Resolve.Lookups[this.TargetExpression] != null)
                        {
                            if (this.Resolve.Lookups[this.TargetExpression] is Guid)
                            {
                                this.Targets.Add((Guid)this.Resolve.Lookups[this.TargetExpression]);
                            }
                            else if (this.Resolve.Lookups[this.TargetExpression].GetType() == typeof(List<Guid>))
                            {
                                this.Targets = (List<Guid>)this.Resolve.Lookups[this.TargetExpression];
                            }
                        }

                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesPrepareDeleteExecuteCode, "TargetType: '{0}'. Target Count: {1}.", this.TargetType, this.Targets.Count);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachTarget ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachTarget_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesForEachTargetChildInitialized, "Target: '{0}'.", e.InstanceData);

            try
            {
                // Cast the instance data as a Guid and prepare the delete resource activity
                // by assigning a resource ID
                Guid target = new Guid();
                if (e.InstanceData != null)
                {
                    target = (Guid)((Guid?)e.InstanceData);
                }

                this.Target = target;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesForEachTargetChildInitialized, "Target: '{0}'.", e.InstanceData);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the SearchForTarget condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void SearchForTarget_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesSearchForTargetCondition);

            try
            {
                e.Result = this.TargetType == DeleteResourcesTargetType.SearchForTarget;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesSearchForTargetCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ResolveTarget condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ResolveTarget_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesResolveTargetCondition);

            try
            {
                e.Result = this.TargetType == DeleteResourcesTargetType.ResolveTarget;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesResolveTargetCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the Authorization condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void Authorization_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DeleteResourcesAuthorizationCondition);

            try
            {
                e.Result = this.ApplyAuthorizationPolicy;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DeleteResourcesAuthorizationCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        #endregion

        #endregion
    }
}