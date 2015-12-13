//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestApprovalForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// RequestApproval Activity Form 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Workflow.ComponentModel;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController;

    #endregion

    /// <summary>
    /// RequestApproval Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class RequestApprovalForm : ActivitySettingsPart
    {
        #region Declarations

        /// <summary>
        /// The activity display name textbox
        /// </summary>
        private readonly ActivityTextBox activityDisplayName;

        /// <summary>
        /// The activity execution condition textbox
        /// </summary>
        private readonly ActivityTextBox activityExecutionCondition;

        /// <summary>
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.RequestApproval);

        /// <summary>
        /// The approvers textbox
        /// </summary>
        private readonly ActivityTextBox approvers;

        /// <summary>
        /// The  approval threshold textbox
        /// </summary>
        private readonly ActivityTextBox threshold;

        /// <summary>
        /// The approval duration textbox
        /// </summary>
        private readonly ActivityTextBox duration;

        /// <summary>
        /// The escalated approvers textbox
        /// </summary>
        private readonly ActivityTextBox escalation;

        /// <summary>
        /// The  approval email template textbox
        /// </summary>
        private readonly ActivityTextBox approvalEmailTemplate;

        /// <summary>
        /// The  approval escalation email template textbox
        /// </summary>
        private readonly ActivityTextBox approvalEscalationEmailTemplate;

        /// <summary>
        /// The  approval complete email template textbox
        /// </summary>
        private readonly ActivityTextBox approvalCompleteEmailTemplate;

        /// <summary>
        /// The  approval denied email template textbox
        /// </summary>
        private readonly ActivityTextBox approvalDeniedEmailTemplate;

        /// <summary>
        /// The  approval timeout email template textbox
        /// </summary>
        private readonly ActivityTextBox approvalTimeoutEmailTemplate;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestApprovalForm"/> class.
        /// </summary>
        public RequestApprovalForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add the standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);

                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, true);

                this.approvers = this.controller.AddTextBox(ActivitySettings.RequestApprovalApprovers, ActivitySettings.RequestApprovalApproversHelpText, true, true);
                this.approvers.TextBoxControl.Width = 300;

                this.threshold = this.controller.AddTextBox(ActivitySettings.RequestApprovalThreshold, ActivitySettings.RequestApprovalThresholdHelpText, true, true);
                this.threshold.TextBoxControl.Width = 300;

                this.duration = this.controller.AddTextBox(ActivitySettings.RequestApprovalDuration, ActivitySettings.RequestApprovalDurationHelpText, true, true);
                this.duration.TextBoxControl.Width = 300;

                this.escalation = this.controller.AddTextBox(ActivitySettings.RequestApprovalEscalation, ActivitySettings.RequestApprovalEscalationHelpText, false, true);
                this.escalation.TextBoxControl.Width = 300;

                this.approvalEmailTemplate = this.controller.AddTextBox(ActivitySettings.RequestApprovalEmailTemplate, ActivitySettings.RequestApprovalEmailTemplateHelpText, true, true);
                this.approvalEmailTemplate.TextBoxControl.Width = 300;

                this.approvalEscalationEmailTemplate = this.controller.AddTextBox(ActivitySettings.RequestApprovalEscalationEmailTemplate, ActivitySettings.RequestApprovalEscalationEmailTemplateHelpText, false, true);
                this.approvalEscalationEmailTemplate.TextBoxControl.Width = 300;

                this.approvalCompleteEmailTemplate = this.controller.AddTextBox(ActivitySettings.RequestApprovalCompleteEmailTemplate, ActivitySettings.RequestApprovalCompleteEmailTemplateHelpText, true, true);
                this.approvalCompleteEmailTemplate.TextBoxControl.Width = 300;

                this.approvalDeniedEmailTemplate = this.controller.AddTextBox(ActivitySettings.RequestApprovalDeniedEmailTemplate, ActivitySettings.RequestApprovalDeniedEmailTemplateHelpText, true, true);
                this.approvalDeniedEmailTemplate.TextBoxControl.Width = 300;

                this.approvalTimeoutEmailTemplate = this.controller.AddTextBox(ActivitySettings.RequestApprovalTimeoutEmailTemplate, ActivitySettings.RequestApprovalTimeoutEmailTemplateHelpText, true, true);
                this.approvalTimeoutEmailTemplate.TextBoxControl.Width = 300;

                // Populates default values for various fields
                this.threshold.Value = "1";
                this.duration.Value = "7";
                this.approvalEmailTemplate.Value = "/EmailTemplate[DisplayName='Default pending approval email template']";
                this.approvalCompleteEmailTemplate.Value = "/EmailTemplate[DisplayName='Default completed approval email template']";
                this.approvalDeniedEmailTemplate.Value = "/EmailTemplate[DisplayName='Default rejected request email template']";
                this.approvalTimeoutEmailTemplate.Value = "/EmailTemplate[DisplayName='Default timed out request email template']";
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        #region Properties

        /// <summary>
        /// Gets the title of the activity.
        /// </summary>
        public override string Title
        {
            get
            {
                return this.controller.Title;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method is called when a user clicks the Save button in the Workflow Designer.
        /// </summary>
        /// <param name="workflow">Represents the parent workflow of the activity.</param>
        /// <returns>Return an instance of the activity that has its properties set to the values entered into the controls used in the UI of the activity.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Workflow Activities will be disposed when the Workflow is disposed")]
        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Create a new instance of the RequestApproval activity and assign
                // dependenty property values based on inputs to standard activity controls
                RequestApproval wfa = new RequestApproval
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    Approvers = this.approvers.Value,
                    Threshold = this.threshold.Value,
                    Duration = this.duration.Value,
                    Escalation = this.escalation.Value,
                    ApprovalEmailTemplate = this.approvalEmailTemplate.Value,
                    EscalationEmailTemplate = this.approvalEscalationEmailTemplate.Value,
                    ApprovalCompleteEmailTemplate = this.approvalCompleteEmailTemplate.Value,
                    ApprovalDeniedEmailTemplate = this.approvalDeniedEmailTemplate.Value,
                    ApprovalTimeoutEmailTemplate = this.approvalTimeoutEmailTemplate.Value,
                };

                return wfa;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// This method initializes activity UI controls to their default values. 
        /// </summary>
        /// <param name="activity">An instance of the current workflow activity. This provides a way to extract the values of the properties to display in the UI.</param>
        public override void LoadActivitySettings(Activity activity)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Cast the supplied activity as a RequestApproval activity
                RequestApproval wfa = activity as RequestApproval;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.approvers.Value = wfa.Approvers;
                this.threshold.Value = wfa.Threshold;
                this.duration.Value = wfa.Duration;
                this.escalation.Value = wfa.Escalation;
                this.approvalEmailTemplate.Value = wfa.ApprovalEmailTemplate;
                this.approvalEscalationEmailTemplate.Value = wfa.EscalationEmailTemplate;
                this.approvalCompleteEmailTemplate.Value = wfa.ApprovalCompleteEmailTemplate;
                this.approvalDeniedEmailTemplate.Value = wfa.ApprovalDeniedEmailTemplate;
                this.approvalTimeoutEmailTemplate.Value = wfa.ApprovalTimeoutEmailTemplate;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Saves the current UI control settings.
        /// </summary>
        /// <returns>Returns <see cref="ActivitySettingsPartData"/>.</returns>
        public override ActivitySettingsPartData PersistSettings()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Use the controller to persist the settings for standard activity controls
                // The definitions controller will manage persistance of associated values
                ActivitySettingsPartData data = this.controller.PersistSettings();
                return data;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Restores UI control settings stored in the data parameter. 
        /// </summary>
        /// <param name="data">Contains data about the values of UI controls.</param>
        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Use the controller and definitions controller to restore settings for activity controls
                this.controller.RestoreSettings(data);
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Switches the activity UI between read only mode and edit mode.
        /// </summary>
        /// <param name="mode">Represents read only mode or edit mode.</param>
        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            Logger.Instance.WriteMethodEntry("Switch Mode: '{0}'.", mode);

            try
            {
                // Use the controller to enable/disable standard activity controls
                // the definitions controller will manage the mode of associated controls
                this.controller.SwitchMode(mode);
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit("Switch Mode: '{0}'.", mode);
            }
        }

        /// <summary>
        /// Validates the inputs. Returns true if all of the UI controls contain valid values. Otherwise, returns false.
        /// </summary>
        /// <returns>true if all of the UI controls contain valid values. Otherwise, false.</returns>
        public override bool ValidateInputs()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Use the controller to validate standard activity controls
                // and return false if a problem was identified
                if (!this.controller.ValidateInputs())
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.approvers.Value))
                {
                    this.controller.ValidationError = ActivitySettings.RequestApprovalApproversFieldValidationError;
                    return false;
                }

                // If standard activity controls are valid,
                // validate any other advanced configurations
                ExpressionEvaluator evaluator = new ExpressionEvaluator();

                try
                {
                    if (!string.IsNullOrEmpty(this.activityExecutionCondition.Value))
                    {
                        evaluator.ParseExpression(this.activityExecutionCondition.Value);

                        // Verify that the activity execution condition resolves to a Boolean value
                        if (!evaluator.IsBooleanExpression(this.activityExecutionCondition.Value))
                        {
                            this.controller.ValidationError = ActivitySettings.ActivityExecutionConditionValidationError;
                            return false;
                        }
                    }

                    ParseApprovers(this.approvers.Value);

                    if (ExpressionEvaluator.IsExpression(this.threshold.Value))
                    {
                        evaluator.ParseExpression(this.threshold.Value);
                    }
                    else
                    {
                        int thresholdNumber;
                        if (!int.TryParse(this.threshold.Value, out thresholdNumber))
                        {
                            this.controller.ValidationError = ActivitySettings.RequestApprovalThresholdFieldValidationError;
                            return false;
                        }
                    }

                    if (ExpressionEvaluator.IsExpression(this.duration.Value))
                    {
                        evaluator.ParseExpression(this.duration.Value);
                    }
                    else
                    {
                        TimeSpan parsedTimeSpan;
                        if (!TimeSpan.TryParse(this.duration.Value, out parsedTimeSpan))
                        {
                            this.controller.ValidationError = ActivitySettings.RequestApprovalDurationFieldValidationError;
                            return false;
                        }
                    }

                    ParseApprovers(this.escalation.Value);

                    if (!string.IsNullOrEmpty(this.escalation.Value) && string.IsNullOrEmpty(this.approvalEscalationEmailTemplate.Value))
                    {
                        this.controller.ValidationError = ActivitySettings.RequestApprovalEscalationEmailTemplateFieldValidationError;
                        return false;
                    }

                    ParseEmailTemplate(this.approvalEmailTemplate.Value);
                    ParseEmailTemplate(this.approvalEscalationEmailTemplate.Value);
                    ParseEmailTemplate(this.approvalCompleteEmailTemplate.Value);
                    ParseEmailTemplate(this.approvalDeniedEmailTemplate.Value);
                    ParseEmailTemplate(this.approvalTimeoutEmailTemplate.Value);
                }
                catch (WorkflowActivityLibraryException ex)
                {
                    this.controller.ValidationError = ex.Message;
                    return false;
                }

                // If no errors were found, clear any validation error and return true
                this.controller.ValidationError = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Creates the child controls.
        /// </summary>
        protected override void CreateChildControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.Controls.Add(this.controller.ControlTable);
                base.CreateChildControls();
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Parses Recipient
        /// </summary>
        /// <param name="recipient">The XPath search filter or lookup expression for recipient email address</param>
        private static void ParseApprovers(string recipient)
        {
            Logger.Instance.WriteMethodEntry();

            ExpressionEvaluator evaluator = new ExpressionEvaluator();

            try
            {
                if (ExpressionEvaluator.IsExpression(recipient))
                {
                    evaluator.ParseExpression(recipient);
                }
                else if (ExpressionEvaluator.IsXPath(recipient) || ExpressionEvaluator.IsEmailAddress(recipient))
                {
                    // nothing to check
                }
                else if (!string.IsNullOrEmpty(recipient))
                {
                    throw new WorkflowActivityLibraryException(ActivitySettings.RequestApprovalApproversFieldValidationError);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Parses email template
        /// </summary>
        /// <param name="template">The XPath search filter or lookup expression for the email template</param>
        private static void ParseEmailTemplate(string template)
        {
            Logger.Instance.WriteMethodEntry();

            ExpressionEvaluator evaluator = new ExpressionEvaluator();

            try
            {
                if (ExpressionEvaluator.IsExpression(template))
                {
                    // Try to parse it as an expression
                    evaluator.ParseExpression(template);
                }
                else if (ExpressionEvaluator.IsXPath(template))
                {
                    // nothing to check
                }
                else if (!string.IsNullOrEmpty(template))
                {
                    // must be a Guid
                    try
                    {
                        Guid g = new Guid(template);
                    }
                    catch (Exception)
                    {
                        throw new WorkflowActivityLibraryException(ActivitySettings.RequestApprovalEmailTemplateValidationError);
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        #endregion
    }
}