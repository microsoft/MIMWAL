//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SendEmailNotificationForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// SendEmailNotification Activity Form 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Workflow.ComponentModel;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController;

    #endregion

    /// <summary>
    /// SendEmailNotification Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class SendEmailNotificationForm : ActivitySettingsPart
    {
        #region Declarations

        /// <summary>
        /// The activity display name textbox
        /// </summary>
        private readonly ActivityTextBox activityDisplayName;

        /// <summary>
        /// The advanced checkbox
        /// </summary>
        private readonly ActivityCheckBox advanced;

        /// <summary>
        /// The activity execution condition textbox
        /// </summary>
        private readonly ActivityTextBox activityExecutionCondition;

        /// <summary>
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.SendEmailNotification);

        /// <summary>
        /// The email template textbox
        /// </summary>
        private readonly ActivityTextBox emailTemplate;

        /// <summary>
        /// The email To textbox
        /// </summary>
        private readonly ActivityTextBox to;

        /// <summary>
        /// The email CC textbox
        /// </summary>
        private readonly ActivityTextBox cc;

        /// <summary>
        /// The email Bcc textbox
        /// </summary>
        private readonly ActivityTextBox bcc;

        /// <summary>
        /// The suppress exception textbox
        /// </summary>
        private readonly ActivityCheckBox suppressException;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmailNotificationForm"/> class.
        /// </summary>
        public SendEmailNotificationForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add the standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);

                this.advanced = this.controller.AddCheckBox(ActivitySettings.AdvancedFeatures, ActivitySettings.AdvancedFeaturesHelpText);
                this.advanced.CheckBoxControl.CheckedChanged += this.Advanced_CheckedChanged;
                this.advanced.CheckBoxControl.AutoPostBack = true;

                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, false);

                this.emailTemplate = this.controller.AddTextBox(ActivitySettings.SendEmailNotificationEmailTemplate, ActivitySettings.SendEmailNotificationEmailTemplateHelpText, true, true);
                this.emailTemplate.TextBoxControl.Width = 300;

                this.to = this.controller.AddTextBox(ActivitySettings.SendEmailNotificationTo, ActivitySettings.SendEmailNotificationToHelpText, true, true);
                this.to.TextBoxControl.Width = 300;

                this.cc = this.controller.AddTextBox(ActivitySettings.SendEmailNotificationCC, ActivitySettings.SendEmailNotificationCCHelpText, false, false);
                this.cc.TextBoxControl.Width = 300;

                this.bcc = this.controller.AddTextBox(ActivitySettings.SendEmailNotificationBcc, ActivitySettings.SendEmailNotificationBccHelpText, false, false);
                this.bcc.TextBoxControl.Width = 300;

                this.suppressException = this.controller.AddCheckBox(ActivitySettings.SendEmailNotificationSuppressException, ActivitySettings.SendEmailNotificationSuppressExceptionHelpText, false, false);
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
                // Create a new instance of the SendEmailNotification activity and assign
                // dependenty property values based on inputs to standard activity controls
                SendEmailNotification wfa = new SendEmailNotification
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    Advanced = this.advanced.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    EmailTemplate = this.emailTemplate.Value,
                    To = this.to.Value,
                    CC = this.cc.Value,
                    Bcc = this.bcc.Value,
                    SuppressException = this.suppressException.Value,
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
                // Cast the supplied activity as a SendEmailNotification activity
                SendEmailNotification wfa = activity as SendEmailNotification;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.advanced.Value = wfa.Advanced;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.emailTemplate.Value = wfa.EmailTemplate;
                this.to.Value = wfa.To;
                this.cc.Value = wfa.CC;
                this.bcc.Value = wfa.Bcc;
                this.suppressException.Value = wfa.SuppressException;
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
                this.ManageAdvancedControls();
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

                if (string.IsNullOrEmpty(this.to.Value))
                {
                    this.controller.ValidationError = ActivitySettings.SendEmailNotificationRecipientFieldValidationError;
                    return false;
                }

                // If standard activity controls are valid,
                // validate any other advanced configurations
                ExpressionEvaluator evaluator = new ExpressionEvaluator();

                try
                {
                    ParseEmailTemplate(this.emailTemplate.Value);

                    if (this.advanced.Value)
                    {
                        if (!string.IsNullOrEmpty(this.activityExecutionCondition.Value))
                        {
                            evaluator.ParseExpression(this.activityExecutionCondition.Value);
                        }

                        ParseRecipient(this.cc.Value);
                        ParseRecipient(this.bcc.Value);
                    }
                    else
                    {
                        ParseRecipient(this.to.Value);
                    }
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
        /// Handles the CheckedChanged event of the Advanced control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void Advanced_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageAdvancedControls();
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
        private static void ParseRecipient(string recipient)
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
                    throw new WorkflowActivityLibraryException(ActivitySettings.SendEmailNotificationRecipientFieldValidationError2);
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
                        throw new WorkflowActivityLibraryException(ActivitySettings.SendEmailNotificationEmailTemplateValidationError);
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the advanced controls.
        /// </summary>
        private void ManageAdvancedControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.activityExecutionCondition.Visible = this.advanced.Value;
                this.cc.Visible = this.advanced.Value;
                this.bcc.Visible = this.advanced.Value;
                this.suppressException.Visible = this.advanced.Value;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        #endregion
    }
}