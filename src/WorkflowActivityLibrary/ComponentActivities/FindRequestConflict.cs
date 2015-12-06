//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="FindRequestConflict.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// FindRequestConflict class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    #region Namespaces Declarations

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;

    #endregion

    /// <summary>
    /// Finds if the request is conflicting with any other requests
    /// </summary>
    internal partial class FindRequestConflict : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AdvancedRequestFilterProperty =
            DependencyProperty.Register("AdvancedRequestFilter", typeof(string), typeof(FindRequestConflict));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty MatchConditionProperty =
            DependencyProperty.Register("MatchCondition", typeof(string), typeof(FindRequestConflict));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConflictFoundProperty =
            DependencyProperty.Register("ConflictFound", typeof(bool), typeof(FindRequestConflict));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConflictingRequestProperty =
            DependencyProperty.Register("ConflictingRequest", typeof(Guid), typeof(FindRequestConflict));

        #endregion

        #region Declarations

        /// <summary>
        /// The compared request lookups.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<string, object> ComparedRequestLookups;

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ExpressionEvaluator;

        /// <summary>
        /// The service actor.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ServiceActor;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FindRequestConflict"/> class.
        /// </summary>
        public FindRequestConflict()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindRequestConflictConstructor);

            try
            {
                this.InitializeComponent();

                this.ServiceActor = WellKnownGuids.FIMServiceAccount;

                if (this.ComparedRequestLookups == null)
                {
                    this.ComparedRequestLookups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }

                if (this.ExpressionEvaluator == null)
                {
                    this.ExpressionEvaluator = new ExpressionEvaluator();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindRequestConflictConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the advanced request filter which is used to identify requests which should be evaluated as potential conflicts.
        /// </summary>
        [Description("Optionally supply a custom XPath filter to identify requests which should be evaluated as potential conflicts.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AdvancedRequestFilter
        {
            get
            {
                return (string)this.GetValue(AdvancedRequestFilterProperty);
            }

            set
            {
                this.SetValue(AdvancedRequestFilterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the match condition which determines if an evaluated request is in conflict.
        /// </summary>
        [Description("The condition which determines if an evaluated request is in conflict.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MatchCondition
        {
            get
            {
                return (string)this.GetValue(MatchConditionProperty);
            }

            set
            {
                this.SetValue(MatchConditionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a conflicting request was found.
        /// </summary>
        [Description("Indicates whether or not a conflicting request was found.")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ConflictFound
        {
            get
            {
                return (bool)this.GetValue(ConflictFoundProperty);
            }

            set
            {
                this.SetValue(ConflictFoundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the conflicting request which was found to be in conflict, if one was identified..
        /// </summary>
        /// <value>
        /// The conflicting request.
        /// </value>
        [Description("The request which was found to be in conflict, if one was identified.")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ConflictingRequest
        {
            get
            {
                return (Guid)this.GetValue(ConflictingRequestProperty);
            }

            set
            {
                this.SetValue(ConflictingRequestProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindRequestConflictExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.FindRequestConflictExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindRequestConflictExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindRequestConflictPrepareExecuteCode);

            try
            {
                // If a custom XPath filter was supplied to identify the requests which should be evaluated for conflict,
                // it will be used for the request search
                if (!string.IsNullOrEmpty(this.AdvancedRequestFilter))
                {
                    this.Find.XPathFilter = this.AdvancedRequestFilter;
                }
                else
                {
                    // If a custom filter was not supplied, default to an XPath filter which returns any requests in a 
                    // state of "Authorizing" and a target resource type and operation which matches that of the current request
                    this.Find.XPathFilter = string.Format(
                        CultureInfo.InvariantCulture,
                        "/Request[RequestStatus = 'Authorizing' and TargetObjectType = '{0}' and Operation = '{1}']",
                        this.Request.CurrentRequest.TargetObjectType,
                        this.Request.CurrentRequest.Operation);
                }

                Logger.Instance.WriteVerbose(EventIdentifier.FindRequestConflictPrepareExecuteCode, "The XPath filter to find conflicting resources is: '{0}'.", this.Find.XPathFilter);

                // Using the expression evaluator, parse the match condition
                // so that all associated expressions are loaded to the cache for resolution
                this.ExpressionEvaluator.ParseExpression(this.MatchCondition);

                // Find all [//ComparedRequest/...] lookups included in the match condition
                foreach (string lookup in from lookup in this.ExpressionEvaluator.LookupCache.Keys let lookupEvaluator = new LookupEvaluator(lookup) where lookupEvaluator.Parameter == LookupParameter.ComparedRequest select lookup)
                {
                    this.ComparedRequestLookups.Add(lookup, null);

                    Logger.Instance.WriteVerbose(EventIdentifier.FindRequestConflictPrepareExecuteCode, "Added item '{0}' to the ComparedRequestLookups dictionary.", lookup);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindRequestConflictPrepareExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachRequest ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachRequest_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindRequestConflictForEachRequestChildInitialized, "ComparedRequest: '{0}'.", e.InstanceData);

            try
            {
                // Purge any resolved values for [//ComparedRequest/...] lookups
                // from the expression evaluator so they can be repopulated via resolution
                // against the request currently being evaluated
                List<string> comparedRequestLookups = this.ComparedRequestLookups.Keys.ToList();
                foreach (string lookup in comparedRequestLookups)
                {
                    this.ExpressionEvaluator.LookupCache[lookup] = null;
                    this.ComparedRequestLookups[lookup] = null;
                }

                // Retrieve the Guid of the request from the instance data
                // Fetch the resolve lookups activity and supply the currently evaluated request as the compared request
                // This will ensure it is used for the resolution of all [//ComparedRequest/...] expressions
                Guid request = new Guid();
                if (e.InstanceData != null)
                {
                    request = (Guid)(Guid?)e.InstanceData;
                }

                ResolveLookups resolveCompared = e.Activity as ResolveLookups;

                if (resolveCompared == null)
                {
                    return;
                }

                resolveCompared.ComparedRequestId = request;

                Logger.Instance.WriteVerbose(EventIdentifier.FindRequestConflictForEachRequestChildInitialized, "The resolve lookups activity will use the request '{0}' to resolve all [//ComparedRequest/...] expressions.", request);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindRequestConflictForEachRequestChildInitialized, "ComparedRequest: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachRequest ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachRequest_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindRequestConflictForEachRequestChildCompleted, "ComparedRequest: '{0}'.", e.InstanceData);

            try
            {
                // Retrieve the Guid of the request from the instance data
                Guid request = new Guid();
                if (e.InstanceData != null)
                {
                    request = (Guid)(Guid?)e.InstanceData;
                }

                // Compared request lookups will be resolved to the dedicated dictionary to prevent repeated resolution of standard lookups
                // Add the resolved values to the expression evaluator lookup cache to facilitate match condition evaluation
                foreach (string lookup in this.ComparedRequestLookups.Keys)
                {
                    this.ExpressionEvaluator.LookupCache[lookup] = this.ComparedRequestLookups[lookup];
                }

                // Now that all [//ComparedRequest/...] lookups have been resolved against
                // the request currently being evaluated, determine if the supplied match condition
                // is satisfied
                object resolved = this.ExpressionEvaluator.ResolveExpression(this.MatchCondition);
                if (!(bool)resolved)
                {
                    return;
                }

                this.ConflictFound = true;
                this.ConflictingRequest = request;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindRequestConflictForEachRequestChildCompleted, "ComparedRequest: '{0}'. Conflict Found: '{1}'.", e.InstanceData, this.ConflictFound);
            }
        }

        #endregion
    }
}