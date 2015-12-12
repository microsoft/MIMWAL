//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="VerifyRequest.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// VerifyRequest Activity 
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
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Verifies request
    /// </summary>
    public partial class VerifyRequest : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AdvancedProperty =
            DependencyProperty.Register("Advanced", typeof(bool), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty CheckForConflictProperty =
            DependencyProperty.Register("CheckForConflict", typeof(bool), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConflictFilterProperty =
            DependencyProperty.Register("ConflictFilter", typeof(string), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConflictDenialMessageProperty =
            DependencyProperty.Register("ConflictDenialMessage", typeof(string), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty CheckForRequestConflictProperty =
            DependencyProperty.Register("CheckForRequestConflict", typeof(bool), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty RequestConflictAdvancedFilterProperty =
            DependencyProperty.Register("RequestConflictAdvancedFilter", typeof(string), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty RequestConflictMatchConditionProperty =
            DependencyProperty.Register("RequestConflictMatchCondition", typeof(string), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty RequestConflictDenialMessageProperty =
            DependencyProperty.Register("RequestConflictDenialMessage", typeof(string), typeof(VerifyRequest));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ConditionsTableProperty =
            DependencyProperty.Register("ConditionsTable", typeof(Hashtable), typeof(VerifyRequest));

        #endregion

        #region Declarations

        /// <summary>
        /// The conflicting request.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ConflictingRequest;

        /// <summary>
        /// The denial message.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public string DenialMessage;

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The condition definitions
        /// </summary>
        private List<Definition> conditions = new List<Definition>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyRequest"/> class.
        /// </summary>
        public VerifyRequest()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestConstructor);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestConstructor);
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
        /// Gets or sets the conflict denial message.
        /// </summary>
        [Description("ConflictDenialMessage")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ConflictDenialMessage
        {
            get
            {
                return (string)this.GetValue(ConflictDenialMessageProperty);
            }

            set
            {
                this.SetValue(ConflictDenialMessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to check for request conflict.
        /// </summary>
        [Description("CheckForRequestConflict")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CheckForRequestConflict
        {
            get
            {
                return (bool)this.GetValue(CheckForRequestConflictProperty);
            }

            set
            {
                this.SetValue(CheckForRequestConflictProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the advanced filter to determine request conflict.
        /// </summary>
        [Description("RequestConflictAdvancedFilter")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RequestConflictAdvancedFilter
        {
            get
            {
                return (string)this.GetValue(RequestConflictAdvancedFilterProperty);
            }

            set
            {
                this.SetValue(RequestConflictAdvancedFilterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets match condition for determining request conflict.
        /// </summary>
        [Description("RequestConflictMatchCondition")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RequestConflictMatchCondition
        {
            get
            {
                return (string)this.GetValue(RequestConflictMatchConditionProperty);
            }

            set
            {
                this.SetValue(RequestConflictMatchConditionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the denial message if request conflict detected.
        /// </summary>
        [Description("RequestConflictDenialMessage")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RequestConflictDenialMessage
        {
            get
            {
                return (string)this.GetValue(RequestConflictDenialMessageProperty);
            }

            set
            {
                this.SetValue(RequestConflictDenialMessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the conditions hash table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("ConditionsTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable ConditionsTable
        {
            get
            {
                return (Hashtable)this.GetValue(ConditionsTableProperty);
            }

            set
            {
                this.SetValue(ConditionsTableProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.VerifyRequestExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestPrepareExecuteCode);

            try
            {
                // If advanced options were not specified,
                // clear any supplied advanced settings so they do not impact activity execution
                if (!this.Advanced)
                {
                    this.ActivityExecutionCondition = null;
                }
                
                // If the activity is configured for conditional execution, parse the associated expression
                if (!string.IsNullOrEmpty(this.ActivityExecutionCondition))
                {
                    this.ActivityExpressionEvaluator.ParseExpression(this.ActivityExecutionCondition);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestPrepareExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestCheckResourceExecuteCode);

            try
            {
                // If a conflict was found,
                // the associated denial message will be thrown as an exception after resolution
                if (this.FindConflict.FoundIds.Count > 0)
                {
                    this.DenialMessage = this.ConflictDenialMessage;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestCheckResourceExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckRequest CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckRequest_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestCheckRequestExecuteCode);

            try
            {
                // If a conflicting request was found,
                // the associated denial message will be thrown as an exception after resolution
                // Mark the conflicting request so that [//ComparedRequest/...] lookups can be resolved from the denial message
                if (!this.FindConflictRequest.ConflictFound)
                {
                    return;
                }

                this.DenialMessage = this.RequestConflictDenialMessage;
                this.ConflictingRequest = this.FindConflictRequest.ConflictingRequest;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestCheckRequestExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ParseConditions CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ParseConditions_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestParseConditionsExecuteCode);

            try
            {
                // Definitions are supplied to the workflow activity in the form of a hash table
                // This is necessary due to deserialization issues with lists and custom classes
                // Convert the hash table to a list of definitions that is easier to work with
                DefinitionsConverter converter = new DefinitionsConverter(this.ConditionsTable);
                this.conditions = converter.Definitions;

                // Load each condition expression into the evaluator so associated lookups
                // can be loaded into the cache for resolution
                foreach (Definition conditionDefinition in this.conditions)
                {
                    this.ActivityExpressionEvaluator.ParseExpression(conditionDefinition.Left);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestParseConditionsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the EnforceConditions CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EnforceConditions_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestEnforceConditionsExecuteCode);

            try
            {
                // Evaluate each condition and use the denial message associated with the first which evaluates to false
                // If no condition evaluates to false, the request is valid
                foreach (Definition conditionDefinition in from conditionDefinition in this.conditions let resolved = this.ActivityExpressionEvaluator.ResolveExpression(conditionDefinition.Left) where resolved is bool && !(bool)resolved select conditionDefinition)
                {
                    this.DenialMessage = conditionDefinition.Right;
                    break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestEnforceConditionsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the DenyRequest CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DenyRequest_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestDenyRequestExecuteCode);

            try
            {
                // If for any reason the resolved denial message is null or empty,
                // use a generic denial message
                string message = this.ResolveMessage.Resolved;
                if (string.IsNullOrEmpty(message))
                {
                    message = Messages.VerifiyRequest_DenialRequestError;
                }

                // Throw a new exception with the appropriate denial message
                throw Logger.Instance.ReportError(EventIdentifier.VerifyRequestDenyRequestExecuteCodeDenialRequestError, new WorkflowActivityLibraryException(message));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestDenyRequestExecuteCode);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestCheckConflictCondition);

            try
            {
                e.Result = this.CheckForConflict && !string.IsNullOrEmpty(this.ConflictFilter) && !string.IsNullOrEmpty(this.ConflictDenialMessage);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestCheckConflictCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the CheckRequestConflict condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void CheckRequestConflict_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestCheckRequestConflictCondition);

            try
            {
                e.Result = string.IsNullOrEmpty(this.DenialMessage) && this.CheckForRequestConflict && !string.IsNullOrEmpty(this.RequestConflictMatchCondition) && !string.IsNullOrEmpty(this.RequestConflictDenialMessage);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestCheckRequestConflictCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the Unique condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void Unique_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestUniqueCondition);

            try
            {
                e.Result = string.IsNullOrEmpty(this.DenialMessage);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestUniqueCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the DenialPending condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void DenialPending_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestDenialPendingCondition);

            try
            {
                e.Result = !string.IsNullOrEmpty(this.DenialMessage);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestDenialPendingCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActivityExecutionConditionSatisfied condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActivityExecutionConditionSatisfied_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.VerifyRequestActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'.", this.ActivityExecutionCondition);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.VerifyRequestActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ActivityExecutionCondition, e.Result);
            }
        }

        #endregion

        #endregion
    }
}