//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestApproval.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// RequestApproval Activity. This activity adapts and decorates the FIM Approval activity.  
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
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// RequestApproval Activity. This activity adapts and decorates the FIM Approval activity.
    /// </summary>
    public partial class RequestApproval : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApprovalCompleteEmailTemplateProperty =
            DependencyProperty.Register("ApprovalCompleteEmailTemplate", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApprovalDeniedEmailTemplateProperty =
            DependencyProperty.Register("ApprovalDeniedEmailTemplate", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApprovalEmailTemplateProperty =
            DependencyProperty.Register("ApprovalEmailTemplate", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApprovalTimeoutEmailTemplateProperty =
            DependencyProperty.Register("ApprovalTimeoutEmailTemplate", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApproversProperty =
            DependencyProperty.Register("Approvers", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty EscalationProperty =
            DependencyProperty.Register("Escalation", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty EscalationEmailTemplateProperty =
            DependencyProperty.Register("EscalationEmailTemplate", typeof(string), typeof(RequestApproval));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(string), typeof(RequestApproval));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The approval complete email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public int ThresholdNumber;

        /// <summary>
        /// The approval complete email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public TimeSpan DurationTimeSpan;

        /// <summary>
        /// The approval complete email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ApprovalCompleteEmailTemplateGuid;

        /// <summary>
        /// The approval denied email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ApprovalDeniedEmailTemplateGuid;

        /// <summary>
        /// The approval email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ApprovalEmailTemplateGuid;

        /// <summary>
        /// The approval timeout email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ApprovalTimeoutEmailTemplateGuid;

        /// <summary>
        /// The escalation email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid EscalationEmailTemplateGuid;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestApproval"/> class.
        /// </summary>
        public RequestApproval()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalConstructor);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalConstructor);
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
        /// Gets or sets approval complete email template
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string ApprovalCompleteEmailTemplate
        {
            get
            {
                return (string)this.GetValue(ApprovalCompleteEmailTemplateProperty);
            }

            set
            {
                this.SetValue(ApprovalCompleteEmailTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets approval denied email template
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string ApprovalDeniedEmailTemplate
        {
            get
            {
                return (string)this.GetValue(ApprovalDeniedEmailTemplateProperty);
            }

            set
            {
                this.SetValue(ApprovalDeniedEmailTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets approval pending email template
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string ApprovalEmailTemplate
        {
            get
            {
                return (string)this.GetValue(ApprovalEmailTemplateProperty);
            }

            set
            {
                this.SetValue(ApprovalEmailTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets approval timeout email template
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string ApprovalTimeoutEmailTemplate
        {
            get
            {
                return (string)this.GetValue(ApprovalTimeoutEmailTemplateProperty);
            }

            set
            {
                this.SetValue(ApprovalTimeoutEmailTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets approvers
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string Approvers
        {
            get
            {
                return (string)this.GetValue(ApproversProperty);
            }

            set
            {
                this.SetValue(ApproversProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets approval duration
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string Duration
        {
            get
            {
                return (string)this.GetValue(DurationProperty);
            }

            set
            {
                this.SetValue(DurationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets escalated approvers
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string Escalation
        {
            get
            {
                return (string)this.GetValue(EscalationProperty);
            }

            set
            {
                this.SetValue(EscalationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets escalation email template
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string EscalationEmailTemplate
        {
            get
            {
                return (string)this.GetValue(EscalationEmailTemplateProperty);
            }

            set
            {
                this.SetValue(EscalationEmailTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets approval threshold
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Approval")]
        public string Threshold
        {
            get
            {
                return (string)this.GetValue(ThresholdProperty);
            }

            set
            {
                this.SetValue(ThresholdProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalExecute);
            }
        }

        /// <summary>
        /// Determines the action taken when the activity has completed execution.
        /// </summary>
        /// <param name="executionContext">The execution context of the activity.</param>
        protected override void OnSequenceComplete(ActivityExecutionContext executionContext)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalOnSequenceComplete);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalOnSequenceComplete);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ParseExpressions CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ParseExpressions_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalParseExpressionsExecuteCode);

            try
            {
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Approvers);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Threshold);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Duration);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Escalation);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ApprovalEmailTemplate);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.EscalationEmailTemplate);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ApprovalCompleteEmailTemplate);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ApprovalDeniedEmailTemplate);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ApprovalTimeoutEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalParseExpressionsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActivityExecutionConditionSatisfied condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActivityExecutionConditionSatisfied_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'.", this.ActivityExecutionCondition);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ActivityExecutionCondition, e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ApproversIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ApproversIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalApproversIsXPathCondition, "Condition: '{0}'.", this.Approvers);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.Approvers);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalApproversIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.Approvers, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckApproverResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckApproverResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalCheckApproverResourcesExecuteCode, "Approvers count: {0}. Approvers: '{1}'.", this.FindApprovers.FoundIds.Count, this.Approvers);

            try
            {
                if (this.FindApprovers.FoundIds.Count == 0)
                {
                    this.Approvers = null;
                }
                else
                {
                    string[] approvers = (from id in this.FindApprovers.FoundIds select id.ToString()).ToArray();
                    this.Approvers = string.Join(";", approvers);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalCheckApproverResourcesExecuteCode, "Approvers count: {0}. Approvers: '{1}'.", this.FindApprovers.FoundIds.Count, this.Approvers);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveApprovers CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveApprovers_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveApproversExecuteCode, "Approvers: '{0}'.", this.Approvers);

            try
            {
                this.Approvers = this.FormatRecipient(this.Approvers);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveApproversExecuteCode, "Approvers: '{0}'.", this.Approvers);
            }
        }

        /// <summary>
        /// Handles the Condition event of the EscalationIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void EscalationIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalEscalationIsXPathCondition, "Condition: '{0}'.", this.Escalation);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.Escalation);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalEscalationIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.Escalation, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckApproverResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEscalationResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalCheckEscalationResourcesExecuteCode, "Escalation count: {0}. Escalation: '{1}'.", this.FindEscalation.FoundIds.Count, this.Escalation);

            try
            {
                if (this.FindEscalation.FoundIds.Count == 0)
                {
                    this.Escalation = null;
                }
                else
                {
                    string[] escalations = (from id in this.FindEscalation.FoundIds select id.ToString()).ToArray();
                    this.Escalation = string.Join(";", escalations);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalCheckEscalationResourcesExecuteCode, "Escalation count: {0}. Escalation: '{1}'.", this.FindEscalation.FoundIds.Count, this.Escalation);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveEscalation CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveEscalation_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveEscalationExecuteCode, "Escalation: '{0}'.", this.Escalation);

            try
            {
                this.Escalation = this.FormatRecipient(this.Escalation);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveEscalationExecuteCode, "Escalation: '{0}'.", this.Escalation);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ApprovalEmailTemplateIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ApprovalEmailTemplateIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalApprovalEmailTemplateIsXPathCondition, "Condition: '{0}'.", this.ApprovalEmailTemplate);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.ApprovalEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalApprovalEmailTemplateIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ApprovalEmailTemplate, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckApprovalEmailTemplateResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckApprovalEmailTemplateResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalCheckApprovalEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.ApprovalEmailTemplate);

            try
            {
                if (this.FindApprovalEmailTemplate.FoundIds.Count == 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MissingApprovalEmailTemplateError, this.ApprovalEmailTemplate));
                }

                if (this.FindApprovalEmailTemplate.FoundIds.Count > 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MultipleApprovalEmailTemplatesError, this.ApprovalEmailTemplate));
                }

                this.ApprovalEmailTemplateGuid = this.FindApprovalEmailTemplate.FoundIds[0];
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalCheckApprovalEmailTemplateResourceExecuteCode, "Email template: '{0}'. Guid: '{0}'.", this.ApprovalEmailTemplate, this.ApprovalEmailTemplateGuid);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveApprovalEmailTemplate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveApprovalEmailTemplate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveApprovalEmailTemplateExecuteCode, "Email template: '{0}'.", this.ApprovalEmailTemplate);

            try
            {
                this.ApprovalEmailTemplateGuid = this.GetEmailTemplateGuid(this.ApprovalEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveApprovalEmailTemplateExecuteCode, "Email template: '{0}'. Guid: '{0}'.", this.ApprovalEmailTemplate, this.ApprovalEmailTemplateGuid);
            }
        }

        /// <summary>
        /// Handles the Condition event of the EscalationEmailTemplateIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void EscalationEmailTemplateIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalEscalationEmailTemplateIsXPathCondition, "Condition: '{0}'.", this.EscalationEmailTemplate);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.EscalationEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalEscalationEmailTemplateIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.EscalationEmailTemplate, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckEscalationEmailTemplateResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEscalationEmailTemplateResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalCheckEscalationEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.EscalationEmailTemplate);

            try
            {
                if (this.FindEscalationEmailTemplate.FoundIds.Count == 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckEscalationEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MissingEscalationEmailTemplateError, this.EscalationEmailTemplate));
                }

                if (this.FindEscalationEmailTemplate.FoundIds.Count > 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckEscalationEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MultipleEscalationEmailTemplatesError, this.EscalationEmailTemplate));
                }

                this.EscalationEmailTemplateGuid = this.FindEscalationEmailTemplate.FoundIds[0];
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalCheckEscalationEmailTemplateResourceExecuteCode, "Email template: '{0}'. Guid: '{0}'.", this.EscalationEmailTemplate, this.EscalationEmailTemplateGuid);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveEscalationEmailTemplate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveEscalationEmailTemplate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveEscalationEmailTemplateExecuteCode, "Email template: '{0}'.", this.EscalationEmailTemplate);

            try
            {
                if (!string.IsNullOrEmpty(this.EscalationEmailTemplate))
                {
                    this.EscalationEmailTemplateGuid = this.GetEmailTemplateGuid(this.EscalationEmailTemplate);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveEscalationEmailTemplateExecuteCode, "Email template: '{0}'. Guid: '{0}'.", this.EscalationEmailTemplate, this.EscalationEmailTemplateGuid);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ApprovalCompleteEmailTemplateIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ApprovalCompleteEmailTemplateIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalApprovalCompleteEmailTemplateIsXPathCondition, "Condition: '{0}'.", this.ApprovalCompleteEmailTemplate);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.ApprovalCompleteEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalApprovalCompleteEmailTemplateIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ApprovalCompleteEmailTemplate, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckApprovalCompleteEmailTemplateResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckApprovalCompleteEmailTemplateResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalCheckApprovalCompleteEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.ApprovalCompleteEmailTemplate);

            try
            {
                if (this.FindApprovalCompleteEmailTemplate.FoundIds.Count == 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalCompleteEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MissingApprovalCompleteEmailTemplateError, this.ApprovalCompleteEmailTemplate));
                }

                if (this.FindApprovalCompleteEmailTemplate.FoundIds.Count > 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalCompleteEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MultipleApprovalCompleteEmailTemplatesError, this.ApprovalCompleteEmailTemplate));
                }

                this.ApprovalCompleteEmailTemplateGuid = this.FindApprovalCompleteEmailTemplate.FoundIds[0];
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalCheckApprovalCompleteEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.ApprovalCompleteEmailTemplate);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveApprovalCompleteEmailTemplate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveApprovalCompleteEmailTemplate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveApprovalCompleteEmailTemplateExecuteCode, "Email template: '{0}'.", this.ApprovalCompleteEmailTemplate);

            try
            {
                this.ApprovalCompleteEmailTemplateGuid = this.GetEmailTemplateGuid(this.ApprovalCompleteEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveApprovalCompleteEmailTemplateExecuteCode, "Email template: '{0}'.", this.ApprovalCompleteEmailTemplate);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ApprovalDeniedEmailTemplateIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ApprovalDeniedEmailTemplateIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalApprovalDeniedEmailTemplateIsXPathCondition, "Condition: '{0}'.", this.ApprovalDeniedEmailTemplate);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.ApprovalDeniedEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalApprovalDeniedEmailTemplateIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ApprovalDeniedEmailTemplate, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckApprovalDeniedEmailTemplateResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckApprovalDeniedEmailTemplateResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalCheckApprovalDeniedEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.ApprovalDeniedEmailTemplate);

            try
            {
                if (this.FindApprovalDeniedEmailTemplate.FoundIds.Count == 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalDeniedEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MissingApprovalDeniedEmailTemplateError, this.ApprovalDeniedEmailTemplate));
                }

                if (this.FindApprovalDeniedEmailTemplate.FoundIds.Count > 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalDeniedEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MultipleApprovalDeniedEmailTemplatesError, this.ApprovalDeniedEmailTemplate));
                }

                this.ApprovalDeniedEmailTemplateGuid = this.FindApprovalDeniedEmailTemplate.FoundIds[0];
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalCheckApprovalDeniedEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.ApprovalDeniedEmailTemplate);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveApprovalDeniedEmailTemplate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveApprovalDeniedEmailTemplate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveApprovalDeniedEmailTemplateExecuteCode, "Email template: '{0}'.", this.ApprovalDeniedEmailTemplate);

            try
            {
                this.ApprovalDeniedEmailTemplateGuid = this.GetEmailTemplateGuid(this.ApprovalDeniedEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveApprovalDeniedEmailTemplateExecuteCode, "Email template: '{0}'.", this.ApprovalDeniedEmailTemplate);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ApprovalTimeoutEmailTemplateIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ApprovalTimeoutEmailTemplateIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalApprovalTimeoutEmailTemplateIsXPathCondition, "Condition: '{0}'.", this.ApprovalTimeoutEmailTemplate);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.ApprovalTimeoutEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalApprovalTimeoutEmailTemplateIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ApprovalTimeoutEmailTemplate, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckApprovalTimeoutEmailTemplateResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckApprovalTimeoutEmailTemplateResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalCheckApprovalTimeoutEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.ApprovalTimeoutEmailTemplate);

            try
            {
                if (this.FindApprovalTimeoutEmailTemplate.FoundIds.Count == 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalTimeoutEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MissingApprovalTimeoutEmailTemplateError, this.ApprovalTimeoutEmailTemplate));
                }

                if (this.FindApprovalTimeoutEmailTemplate.FoundIds.Count > 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalCheckApprovalTimeoutEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.RequestApproval_MultipleApprovalTimeoutEmailTemplatesError, this.ApprovalTimeoutEmailTemplate));
                }

                this.ApprovalTimeoutEmailTemplateGuid = this.FindApprovalTimeoutEmailTemplate.FoundIds[0];
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalCheckApprovalTimeoutEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.ApprovalTimeoutEmailTemplate);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveApprovalTimeoutEmailTemplate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveApprovalTimeoutEmailTemplate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveApprovalTimeoutEmailTemplateExecuteCode, "Email template: '{0}'.", this.ApprovalTimeoutEmailTemplate);

            try
            {
                this.ApprovalTimeoutEmailTemplateGuid = this.GetEmailTemplateGuid(this.ApprovalTimeoutEmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveApprovalTimeoutEmailTemplateExecuteCode, "Email template: '{0}'.", this.ApprovalTimeoutEmailTemplate);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveThreshold CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveThreshold_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveThresholdExecuteCode, "Threshold: '{0}'.", this.Threshold);

            try
            {
                if (ExpressionEvaluator.IsExpression(this.Threshold))
                {
                    this.ThresholdNumber = Convert.ToInt32(this.ActivityExpressionEvaluator.ResolveExpression(this.Threshold), CultureInfo.InvariantCulture);
                }
                else
                {
                    this.ThresholdNumber = Convert.ToInt32(this.Threshold, CultureInfo.InvariantCulture);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveThresholdExecuteCode, "Threshold: '{0}'. Resolved Threshold: '{1}'.", this.Threshold, this.ThresholdNumber);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveDuration CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveDuration_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalResolveDurationExecuteCode, "Duration: '{0}'.", this.Duration);

            try
            {
                object timeoutDuration;

                if (ExpressionEvaluator.IsExpression(this.Duration))
                {
                    timeoutDuration = this.ActivityExpressionEvaluator.ResolveExpression(this.Duration);
                }
                else
                {
                    timeoutDuration = this.Duration;
                }

                if (timeoutDuration is TimeSpan)
                {
                    this.DurationTimeSpan = (TimeSpan)timeoutDuration;
                }
                else
                {
                    TimeSpan parsedTimeSpan;
                    if (!TimeSpan.TryParse(Convert.ToString(timeoutDuration, CultureInfo.InvariantCulture), out parsedTimeSpan))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.AddDelayDelayInitializeTimeoutDurationError, new WorkflowActivityLibraryException(Messages.AddDelayActivity_TimeoutDurationValidationError, timeoutDuration));
                    }

                    this.DurationTimeSpan = parsedTimeSpan;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalResolveThresholdExecuteCode, "Duration: '{0}'. Resolved Duration: '{1}'.", this.Duration, this.DurationTimeSpan);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the TraceCreateApproval CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TraceCreateApproval_ExecuteCode(object sender, EventArgs e)
        {
            var traceData = new object[]
            {
                this.Approvers, this.Threshold, this.Duration, this.Escalation,
                this.ApprovalEmailTemplate, this.EscalationEmailTemplate, this.ApprovalCompleteEmailTemplate,
                this.ApprovalDeniedEmailTemplate, this.ApprovalTimeoutEmailTemplate
            };

            try
            {
                Logger.Instance.WriteMethodEntry(
                    EventIdentifier.RequestApprovalTraceCreateApprovalExecuteCode,
                    "Approvers: '{0}'. Threshold: '{1}'. Duration: '{2}'. Escalation: '{3}'. ApprovalEmailTemplate: '{4}'. EscalationEmailTemplate: '{5}'. ApprovalCompleteEmailTemplate: '{6}'. ApprovalDeniedEmailTemplate: '{7}'. ApprovalTimeoutEmailTemplate: '{8}'.",
                    traceData);

                traceData = new object[]
                {
                    this.Approvers, this.ThresholdNumber, this.DurationTimeSpan, this.Escalation,
                    this.ApprovalEmailTemplateGuid, this.EscalationEmailTemplateGuid, this.ApprovalCompleteEmailTemplateGuid,
                    this.ApprovalDeniedEmailTemplateGuid, this.ApprovalTimeoutEmailTemplateGuid
                };

                Logger.Instance.WriteInfo(
                    EventIdentifier.RequestApprovalTraceCreateApprovalExecuteCode,
                    "Approvers: '{0}'. Threshold: '{1}'. Duration: '{2}'. Escalation: '{3}'. ApprovalEmailTemplate: '{4}'. EscalationEmailTemplate: '{5}'. ApprovalCompleteEmailTemplate: '{6}'. ApprovalDeniedEmailTemplate: '{7}'. ApprovalTimeoutEmailTemplate: '{8}'.",
                    traceData);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(
                    EventIdentifier.RequestApprovalTraceCreateApprovalExecuteCode,
                    "Approvers: '{0}'. Threshold: '{1}'. Duration: '{2}'. Escalation: '{3}'. ApprovalEmailTemplate: '{4}'. EscalationEmailTemplate: '{5}'. ApprovalCompleteEmailTemplate: '{6}'. ApprovalDeniedEmailTemplate: '{7}'. ApprovalTimeoutEmailTemplate: '{8}'.",
                    traceData);
            }
        }

        /// <summary>
        /// Converts the specified email template expression into a Guid object
        /// </summary>
        /// <param name="emailTemplate">An email template expression</param>
        /// <returns>The Guid of the specified email template expression</returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        private Guid GetEmailTemplateGuid(string emailTemplate)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalGetEmailTemplateGuid, "Email Template: '{0}'.", emailTemplate);

            Guid templateGuid = Guid.Empty;

            try
            {
                object templateObject = ExpressionEvaluator.IsExpression(emailTemplate) ? this.ActivityExpressionEvaluator.ResolveExpression(emailTemplate) : emailTemplate;

                if (templateObject is byte[])
                {
                    templateGuid = new Guid(templateObject as byte[]);
                }
                else if (templateObject is Guid)
                {
                    templateGuid = (Guid)templateObject;
                }
                else if (templateObject is string && !string.IsNullOrEmpty(templateObject as string))
                {
                    templateGuid = new Guid(emailTemplate);
                }
                else
                {
                    throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalGetEmailTemplateGuidError, new WorkflowActivityLibraryException(Messages.RequestApproval_InvalidEmailTemplateFormatError, emailTemplate));
                }
            }
            catch (Exception)
            {
                throw Logger.Instance.ReportError(EventIdentifier.RequestApprovalGetEmailTemplateGuidError, new WorkflowActivityLibraryException(Messages.RequestApproval_InvalidEmailTemplateFormatError, emailTemplate));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalGetEmailTemplateGuid, "Email Template: '{0}'. Guid: '{1}'.", emailTemplate, templateGuid);
            }

            return templateGuid;
        }

        /// <summary>
        /// Formats Recipient object as string
        /// </summary>
        /// <param name="recipient">Guid or email address of the recipient</param>
        /// <returns>A string representation of the recipient object</returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        private string FormatRecipient(string recipient)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RequestApprovalFormatRecipient, "Recipient: '{0}'.", recipient);

            string emailAddress = recipient; // This handles the case of ";" or "," separated list of email addresses.

            try
            {
                if (ExpressionEvaluator.IsExpression(recipient))
                {
                    object recipientObject = this.ActivityExpressionEvaluator.ResolveExpression(recipient);

                    // Handle recipient specified as a Guid.
                    if (recipientObject is byte[])
                    {
                        emailAddress = new Guid(recipientObject as byte[]).ToString();
                    }
                    else if (recipientObject is Guid)
                    {
                        emailAddress = ((Guid)recipientObject).ToString();
                    }
                    else if (recipientObject != null && recipientObject.GetType() == typeof(List<Guid>))
                    {
                        emailAddress = string.Join(";", (from object o in (IEnumerable)recipientObject select o.ToString()).ToArray());
                    }
                    else
                    {
                        emailAddress = recipientObject as string;
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RequestApprovalFormatRecipient, "Recipient: '{0}'. Returning: '{1}'.", recipient, emailAddress);
            }

            return emailAddress;
        }

        #endregion
    }
}
