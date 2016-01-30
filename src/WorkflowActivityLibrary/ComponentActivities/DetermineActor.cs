//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DetermineActor.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DetermineActor class
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
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Determines the actor to be used for FIM data access activities
    /// </summary>
    internal partial class DetermineActor : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorTypeProperty =
            DependencyProperty.Register("ActorType", typeof(ActorType), typeof(DetermineActor));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorStringProperty =
            DependencyProperty.Register("ActorString", typeof(string), typeof(DetermineActor));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorProperty =
            DependencyProperty.Register("Actor", typeof(Guid), typeof(DetermineActor));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResultsProperty =
            DependencyProperty.Register("QueryResults", typeof(Dictionary<string, List<Guid>>), typeof(DetermineActor));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(DetermineActor));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DetermineActor"/> class.
        /// </summary>
        public DetermineActor()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorConstructor);

            try
            {
                this.InitializeComponent();

                if (this.ActivityExpressionEvaluator == null)
                {
                    this.ActivityExpressionEvaluator = new ExpressionEvaluator();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the type of the actor.
        /// </summary>
        [Description("ActorType")]
        [Category("Input")]
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
        /// Gets or sets the string value of actor.
        /// </summary>
        [Description("ActorString")]
        [Category("Input")]
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
        /// Gets or sets the results of the query used to determine actor.
        /// </summary>
        [Description("QueryResults")]
        [Category("Input")]
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
        /// Gets or sets the unique identifier of the actor.
        /// </summary>
        [Description("Actor")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid Actor
        {
            get
            {
                return (Guid)this.GetValue(ActorProperty);
            }

            set
            {
                this.SetValue(ActorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value which should be used for [//Value/...] lookup resolution.
        /// </summary>
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.DetermineActorExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareResolveActor_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorPrepareResolveActorExecuteCode, "ActorType: '{0}'.", this.ActorType);

            try
            {
                switch (this.ActorType)
                {
                    case ActorType.Resolve:
                        this.ActivityExpressionEvaluator.ParseExpression(this.ActorString);

                        Logger.Instance.WriteVerbose(EventIdentifier.DetermineActorPrepareResolveActorExecuteCode, "The actor expression is: '{0}'.", this.ActorString);

                        break;
                    case ActorType.Account:
                        if (ExpressionEvaluator.IsValueExpression(this.ActorString))
                        {
                            this.ActivityExpressionEvaluator.ParseExpression(this.ActorString);
                        }

                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorPrepareResolveActorExecuteCode, "ActorType: '{0}'.", this.ActorType);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareAccountActor_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorPrepareAccountActorExecuteCode, "ActorType: '{0}'. ActorString: '{1}'.", this.ActorType, this.ActorString);

            string actor = this.ActorString;

            try
            {
                switch (this.ActorType)
                {
                    case ActorType.Account:
                        {
                            if (ExpressionEvaluator.IsValueExpression(this.ActorString))
                            {
                                actor = this.ActivityExpressionEvaluator.ResolveExpression(this.ActorString) as string;
                                if (string.IsNullOrEmpty(actor))
                                {
                                    throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(Messages.DetermineActor_NullResolvedActorError));
                                }
                            }

                            string filter;
                            if (!actor.Contains(@"\"))
                            {
                                filter = string.Format(CultureInfo.InvariantCulture, "AccountName = '{0}'", actor);
                            }
                            else
                            {
                                char[] delim = @"\".ToCharArray();
                                string[] actorParts = actor.Split(delim, 2, StringSplitOptions.None);
                                filter = string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Domain = '{0}' and AccountName = '{1}'",
                                    actorParts[0],
                                    actorParts[1]);
                            }

                            this.Query.XPathFilter = string.Format(CultureInfo.InvariantCulture, "/Person[{0}]", filter);
                        }

                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorPrepareAccountActorExecuteCode, "ActorType: '{0}'. Actor: '{1}'. Actor XPath Filter: '{2}'.", this.ActorType, actor, this.Query.XPathFilter);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Decide CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Decide_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorDecideExecuteCode, "ActorType: '{0}'.", this.ActorType);

            try
            {
                switch (this.ActorType)
                {
                    case ActorType.Service:
                        this.Actor = WellKnownGuids.FIMServiceAccount;
                        break;
                    case ActorType.Requestor:
                        SequentialWorkflow parentWorkflow;
                        SequentialWorkflow.TryGetContainingWorkflow(this, out parentWorkflow);
                        this.Actor = parentWorkflow.ActorId;
                        break;
                    case ActorType.Resolve:
                        object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.ActorString);
                        if (resolved is Guid)
                        {
                            this.Actor = (Guid)resolved;
                        }
                        else if (resolved == null)
                        {
                            throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(Messages.DetermineActor_NullResolvedActorError));
                        }
                        else if (resolved.GetType().IsGenericType && resolved.GetType().GetGenericTypeDefinition() == typeof(List<>))
                        {
                            throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(Messages.DetermineActor_MultipleResolvedActorsError));
                        }
                        else if (resolved is string && !string.IsNullOrEmpty(resolved as string))
                        {
                            try
                            {
                                this.Actor = new Guid(resolved as string);
                            }
                            catch (Exception ex)
                            {
                                throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(Messages.DetermineActor_InvalidActorGuidFormatError, ex));
                            }
                        }
                        else
                        {
                            throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(Messages.DetermineActor_UnresolvedActorError));
                        }

                        break;
                    case ActorType.Account:
                        switch (this.Query.FoundIds.Count)
                        {
                            case 1:
                                this.Actor = this.Query.FoundIds[0];
                                break;
                            case 0:
                                throw Logger.Instance.ReportError(EventIdentifier.DetermineActorDecideExecuteCodeNotFoundActorAccountError, new WorkflowActivityLibraryException(Messages.DetermineActor_NotFoundActorAccountError));
                            default:
                                throw Logger.Instance.ReportError(EventIdentifier.DetermineActorDecideExecuteCodeMultipleActorAccountsError, new WorkflowActivityLibraryException(Messages.DetermineActor_MultipleActorAccountsError));
                        }

                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorDecideExecuteCode, "ActorType: '{0}'. Actor: '{1}'. ", this.ActorType, this.Actor);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the ResolveActor Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ResolveActor_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorResolveActorCondition);

            try
            {
                e.Result = this.ActorType == ActorType.Resolve || ExpressionEvaluator.IsValueExpression(this.ActorString);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorResolveActorCondition, "Condition evaluated '{0}'. Actor Type: '{1}'.", e.Result, this.ActorType);
            }
        }

        /// <summary>
        /// Handles the Condition event of the AccountActor Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void AccountActor_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorAccountActorCondition);

            try
            {
                e.Result = this.ActorType == ActorType.Account;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorAccountActorCondition, "Condition evaluated '{0}'. Actor Type: '{1}'.", e.Result, this.ActorType);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActorIsNotSet Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActorIsNotSet_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.DetermineActorActorIsNotSetCondition);

            try
            {
                e.Result = this.Actor == Guid.Empty || ExpressionEvaluator.IsValueExpression(this.ActorString);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.DetermineActorActorIsNotSetCondition, "Condition evaluated '{0}'. Actor: '{1}'. Actor String: '{2}'.", e.Result, this.Actor, this.ActorString);
            }
        }

        #endregion

        #endregion
    }
}