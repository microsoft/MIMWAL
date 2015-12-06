//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="AddDelay.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// AddDelay Activity 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities
{
    #region Namespaces Declarations

    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// AddDelay Activity
    /// </summary>
    public partial class AddDelay : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(AddDelay));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(AddDelay));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty TimeoutDurationProperty =
            DependencyProperty.Register("TimeoutDuration", typeof(string), typeof(AddDelay));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDelay"/> class.
        /// </summary>
        public AddDelay()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AddDelayConstructor);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.AddDelayConstructor);
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
        /// Gets or sets the conflict filter.
        /// </summary>
        [Description("TimeoutDuration")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TimeoutDuration
        {
            get
            {
                return (string)this.GetValue(TimeoutDurationProperty);
            }

            set
            {
                this.SetValue(TimeoutDurationProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.AddDelayExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.AddDelayExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AddDelayExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ParseExpressions CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ParseExpressions_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AddDelayParseExpressionsExecuteCode);

            try
            {
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.TimeoutDuration);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AddDelayParseExpressionsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActivityExecutionConditionSatisfied condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActivityExecutionConditionSatisfied_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AddDelayActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'.", this.ActivityExecutionCondition);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.AddDelayActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ActivityExecutionCondition, e.Result);
            }
        }

        /// <summary>
        /// Handles the InitializeTimeoutDuration event of the Delay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Delay_InitializeTimeoutDuration(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AddDelayDelayInitializeTimeoutDuration);

            try
            {
                object timeoutDuration;

                if (ExpressionEvaluator.IsExpression(this.TimeoutDuration))
                {
                    timeoutDuration = this.ActivityExpressionEvaluator.ResolveExpression(this.TimeoutDuration);
                }
                else
                {
                    timeoutDuration = this.TimeoutDuration;
                }

                if (timeoutDuration is TimeSpan)
                {
                    this.Delay.TimeoutDuration = (TimeSpan)timeoutDuration;
                }
                else
                {
                    TimeSpan parsedTimeSpan;
                    if (!TimeSpan.TryParse(Convert.ToString(timeoutDuration, CultureInfo.InvariantCulture), out parsedTimeSpan))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.AddDelayDelayInitializeTimeoutDurationError, new WorkflowActivityLibraryException(Messages.AddDelayActivity_TimeoutDurationValidationError, timeoutDuration));
                    }

                    this.Delay.TimeoutDuration = parsedTimeSpan;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AddDelayDelayInitializeTimeoutDuration, "TimeoutDuration: '{0}'.", this.Delay.TimeoutDuration);
            }
        }

        #endregion

        /// <summary>
        /// Handles the ExecuteCode event of the TraceWakeup CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TraceWakeup_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AddDelayTraceWakeupExecuteCode, "Waking up after TimeoutDuration: '{0}'.", this.Delay.TimeoutDuration);

            // Let's restore the context so that it gets logged during the exit.
            Logger.SetContextItem(this, this.WorkflowInstanceId);

            Logger.Instance.WriteMethodExit(EventIdentifier.AddDelayTraceWakeupExecuteCode, "Waking up after TimeoutDuration: '{0}'.", this.Delay.TimeoutDuration);
        }
    }
}
