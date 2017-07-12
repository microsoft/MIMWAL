//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolveQueries.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ResolveQueries class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    #region Namespaces Declarations

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;

    #endregion

    /// <summary>
    /// Resolves query expressions
    /// </summary>
    internal partial class ResolveQueries : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryDefinitionsProperty =
            DependencyProperty.Register("QueryDefinitions", typeof(List<Definition>), typeof(ResolveQueries));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResultsProperty =
            DependencyProperty.Register("QueryResults", typeof(Dictionary<string, List<Guid>>), typeof(ResolveQueries));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(ResolveQueries));

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveQueries"/> class.
        /// </summary>
        public ResolveQueries()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveQueriesConstructor);

            try
            {
                this.InitializeComponent();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveQueriesConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the list of query definitions to be resolved.
        /// </summary>
        [Description("The collection of query definitions to be resolved.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<Definition> QueryDefinitions
        {
            get
            {
                return (List<Definition>)this.GetValue(QueryDefinitionsProperty);
            }

            set
            {
                this.SetValue(QueryDefinitionsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the dictionary of query results.
        /// </summary>
        [Description("The keyed dictionary of query results")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Dictionary<string, List<Guid>> QueryResults
        {
            get
            {
                return (Dictionary<string, List<Guid>>)this.GetValue(QueryResultsProperty);
            }

            set
            {
                this.SetValue(QueryResultsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value which should be used for [//Value/...] lookup resolution.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Reviewed")]
        [Description("The value which should be used for [//Value/...] lookup resolution.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Value
        {
            get
            {
                return this.GetValue(ValueProperty);
            }

            set
            {
                this.SetValue(ValueProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveQueriesExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.ResolveQueriesExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveQueriesExecute);
            }
        }

        /// <summary>
        /// Handles the Initialized event of the ForEachQuery ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ForEachQuery_Initialized(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveQueriesForEachQueryInitialized);

            try
            {
                // initialize this afresh at the start of the processing
                this.QueryResults = new Dictionary<string, List<Guid>>(StringComparer.OrdinalIgnoreCase);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveQueriesForEachQueryInitialized);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachQuery ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachQuery_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveQueriesForEachQueryChildInitialized);

            string queryXPathFilter = null;
            try
            {
                // Prepare for the execution of the query by pulling the XPath filter
                // from the definition and assigning it to the find resources activity
                Definition definition = e.InstanceData as Definition;
                FindResources runQuery = e.Activity as FindResources;

                if (runQuery == null || definition == null)
                {
                    return;
                }

                queryXPathFilter = definition.Right;
                runQuery.XPathFilter = queryXPathFilter;

                // Also add results from any previous queries
                // so that the first query could also be used in the filter criteria for the second query
                // providing for the ability to further refine the result set.
                runQuery.QueryResults = this.QueryResults;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveQueriesForEachQueryChildInitialized, "Query XPath filter: '{0}'.", queryXPathFilter);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachQuery ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachQuery_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveQueriesForEachQueryChildCompleted);

            try
            {
                // Load the results into the query results dictionary
                // Results will be added even if no resources were found
                Definition definition = e.InstanceData as Definition;
                FindResources runQuery = e.Activity as FindResources;

                if (definition != null && runQuery != null)
                {
                    this.QueryResults.Add(definition.Left, runQuery.FoundIds);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveQueriesForEachQueryChildCompleted);
            }
        }

        #endregion
    }
}