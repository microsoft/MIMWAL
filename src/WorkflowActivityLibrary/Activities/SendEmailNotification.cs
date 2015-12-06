//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SendEmailNotification.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// SendEmailNotification Activity. This activity adapts and decorates the FIM Notification activity.  
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
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// SendEmailNotification Activity. This activity adapts and decorates the FIM Notification activity.
    /// </summary>
    public partial class SendEmailNotification : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AdvancedProperty =
            DependencyProperty.Register("Advanced", typeof(bool), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty EmailTemplateProperty =
            DependencyProperty.Register("EmailTemplate", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty CCProperty =
            DependencyProperty.Register("CC", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty BccProperty =
            DependencyProperty.Register("Bcc", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty SuppressExceptionProperty =
            DependencyProperty.Register("SuppressException", typeof(bool), typeof(SendEmailNotification));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid EmailTemplateGuid;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmailNotification"/> class.
        /// </summary>
        public SendEmailNotification()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationConstructor);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationConstructor);
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
        /// Gets or sets notification email template
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Email")]
        public string EmailTemplate
        {
            get
            {
                return (string)this.GetValue(EmailTemplateProperty);
            }

            set
            {
                this.SetValue(EmailTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets email 'To' recipient
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Email")]
        public string To
        {
            get
            {
                return (string)this.GetValue(ToProperty);
            }

            set
            {
                this.SetValue(ToProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets email 'CC' recipient
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Email")]
        public string CC
        {
            get
            {
                return (string)this.GetValue(CCProperty);
            }

            set
            {
                this.SetValue(CCProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets email 'Bcc' recipient
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Email")]
        public string Bcc
        {
            get
            {
                return (string)this.GetValue(BccProperty);
            }

            set
            {
                this.SetValue(BccProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether suppress exception checkbox is selected.
        /// </summary>
        [Description("Supresses notification failure from the EmailNotification Activity")]
        [Category("Email")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool SuppressException
        {
            get
            {
                return (bool)this.GetValue(SuppressExceptionProperty);
            }

            set
            {
                this.SetValue(SuppressExceptionProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationExecute);
            }
        }

        /// <summary>
        /// Converts the specified email template resolved expression into a Guid object
        /// </summary>
        /// <param name="emailTemplate">An email template resolved expression</param>
        /// <returns>The Guid of the specified email template</returns>
        private Guid GetEmailTemplateGuid(string emailTemplate)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationGetEmailTemplateGuid, "Email Template: '{0}'.", emailTemplate);

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
                else if (templateObject != null && templateObject.GetType() == typeof(List<Guid>))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationGetEmailTemplateGuidError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_MultipleEmailTemplatesError, emailTemplate));
                }
                else
                {
                    throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationGetEmailTemplateGuidError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_InvalidEmailTemplateFormatError, emailTemplate));
                }
            }
            catch (Exception)
            {
                throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationGetEmailTemplateGuidError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_InvalidEmailTemplateFormatError, emailTemplate));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationGetEmailTemplateGuid, "Email Template: '{0}'. Guid: '{1}'.", emailTemplate, templateGuid);
            }

            return templateGuid;
        }

        /// <summary>
        /// Formats Recipient object as string
        /// </summary>
        /// <param name="recipient">Guid or email address of the recipient</param>
        /// <returns>A string representation of the recipient object</returns>
        private string FormatRecipient(string recipient)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationFormatRecipient, "Recipient: '{0}'.", recipient);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationFormatRecipient, "Recipient: '{0}'. Returning: {1}.", recipient, emailAddress);
            }

            return emailAddress;
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ParseExpressions CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ParseExpressions_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationParseExpressionsExecuteCode);

            try
            {
                // If advanced options were not specified,
                // clear any supplied advanced settings so they do not impact activity execution
                if (!this.Advanced)
                {
                    this.ActivityExecutionCondition = null;
                    this.CC = null;
                    this.Bcc = null;
                    this.SuppressException = false;
                }

                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.To);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.CC);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Bcc);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.EmailTemplate);

                ////if (string.IsNullOrEmpty(this.To) && string.IsNullOrEmpty(this.CC) && string.IsNullOrEmpty(this.Bcc))
                if (string.IsNullOrEmpty(this.To))
                {
                    if (!this.SuppressException)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationParseExpressionsExecuteCodeError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_RecipientValidationError));
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationParseExpressionsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the Condition event of the ActivityExecutionConditionSatisfied condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActivityExecutionConditionSatisfied_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'.", this.ActivityExecutionCondition);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ActivityExecutionCondition, e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the EmailTemplateIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void EmailTemplateIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationEmailTemplateIsXPathCondition, "Condition: '{0}'.", this.EmailTemplate);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.EmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationEmailTemplateIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.EmailTemplate, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckEmailTemplateResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEmailTemplateResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationCheckEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.EmailTemplate);

            try
            {
                if (this.FindEmailTemplate.FoundIds.Count == 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationCheckEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_MissingEmailTemplateError, this.EmailTemplate));
                }

                if (this.FindEmailTemplate.FoundIds.Count > 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationCheckEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_MultipleEmailTemplatesError, this.EmailTemplate));
                }

                this.EmailTemplateGuid = this.FindEmailTemplate.FoundIds[0];
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationCheckEmailTemplateResourceExecuteCode, "Email template: '{0}'.", this.EmailTemplate);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveEmailTemplate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveEmailTemplate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationResolveEmailTemplateExecuteCode, "Email template: '{0}'.", this.EmailTemplate);

            try
            {
                this.EmailTemplateGuid = this.GetEmailTemplateGuid(this.EmailTemplate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationResolveEmailTemplateExecuteCode, "Email template: '{0}'.", this.EmailTemplate);
            }
        }

        /// <summary>
        /// Handles the Condition event of the EmailToIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void EmailToIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationEmailToIsXPathCondition, "Condition: '{0}'.", this.To);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.To);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationEmailToIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.To, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckEmailToRecipientResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEmailToRecipientResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationCheckEmailToRecipientResourcesExecuteCode, "To recipients count: {0}. Recipient: '{1}'.", this.FindEmailToRecipients.FoundIds.Count, this.To);

            try
            {
                if (this.FindEmailToRecipients.FoundIds.Count == 0)
                {
                    Logger.Instance.WriteWarning(EventIdentifier.SendEmailNotificationCheckEmailToRecipientResourcesExecuteCodeWarning, Messages.SendEmailNotification_ToRecipientNotFoundError, this.To);
                    this.To = null;
                }
                else
                {
                    string[] recipients = (from id in this.FindEmailToRecipients.FoundIds select id.ToString()).ToArray();
                    this.To = string.Join(";", recipients);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationCheckEmailToRecipientResourcesExecuteCode, "To recipients count: {0}. Recipient: '{1}'.", this.FindEmailToRecipients.FoundIds.Count, this.To);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveEmailToRecipients CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveEmailToRecipients_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationResolveEmailToRecipientsExecuteCode, "To Recipients: '{0}'.", this.To);

            try
            {
                string recipient = this.FormatRecipient(this.To);

                if (string.IsNullOrEmpty(recipient))
                {
                    Logger.Instance.WriteWarning(EventIdentifier.SendEmailNotificationResolveEmailToRecipientsExecuteCodeWarning, Messages.SendEmailNotification_ToRecipientNotFoundError, this.To);
                }

                this.To = recipient;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationResolveEmailToRecipientsExecuteCode, "To Recipients: '{0}'.", this.To);
            }
        }

        /// <summary>
        /// Handles the Condition event of the EmailCcIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void EmailCcIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationEmailCcIsXPathCondition, "Condition: '{0}'.", this.CC);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.CC);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationEmailCcIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.CC, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckEmailCcRecipientResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEmailCcRecipientResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationCheckEmailCcRecipientResourcesExecuteCode, "CC recipients count: {0}. Recipient: '{1}'.", this.FindEmailCcRecipients.FoundIds.Count, this.CC);

            try
            {
                if (this.FindEmailCcRecipients.FoundIds.Count == 0)
                {
                    this.CC = null;
                }
                else
                {
                    string[] recipients = (from id in this.FindEmailCcRecipients.FoundIds select id.ToString()).ToArray();
                    this.CC = string.Join(";", recipients);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationCheckEmailCcRecipientResourcesExecuteCode, "CC recipients count: {0}. Recipient: '{1}'.", this.FindEmailCcRecipients.FoundIds.Count, this.CC);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveEmailCcRecipients CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveEmailCcRecipients_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationResolveEmailCcRecipientsExecuteCode, "CC Recipients: '{0}'.", this.CC);

            try
            {
                this.CC = this.FormatRecipient(this.CC);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationResolveEmailCcRecipientsExecuteCode, "CC Recipients: '{0}'.", this.CC);
            }
        }

        /// <summary>
        /// Handles the Condition event of the EmailBccIsXPath_Condition condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void EmailBccIsXPath_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationEmailBccIsXPathCondition, "Condition: '{0}'.", this.Bcc);

            try
            {
                e.Result = ExpressionEvaluator.IsXPath(this.Bcc);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationEmailBccIsXPathCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.Bcc, e.Result);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckEmailBccRecipientResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEmailBccRecipientResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationCheckEmailBccRecipientResourcesExecuteCode, "Bcc recipients count: {0}. Recipient: '{1}'.", this.FindEmailTemplate.FoundIds.Count, this.Bcc);

            try
            {
                if (this.FindEmailBccRecipients.FoundIds.Count == 0)
                {
                    this.Bcc = null;
                }
                else
                {
                    string[] recipients = (from id in this.FindEmailBccRecipients.FoundIds select id.ToString()).ToArray();
                    this.Bcc = string.Join(";", recipients);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationCheckEmailBccRecipientResourcesExecuteCode, "Bcc recipients count: {0}. Recipient: '{1}'.", this.FindEmailTemplate.FoundIds.Count, this.Bcc);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveEmailBccRecipients CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveEmailBccRecipients_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationResolveEmailBccRecipientsExecuteCode, "Bcc Recipients: '{0}'.", this.Bcc);

            try
            {
                this.Bcc = this.FormatRecipient(this.Bcc);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationResolveEmailBccRecipientsExecuteCode, "Bcc Recipients: '{0}'.", this.Bcc);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareSendMail CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareSendMail_ExecuteCode(object sender, EventArgs e)
        {
            var traceData = new object[] { this.To, this.CC, this.Bcc, this.EmailTemplate, this.EmailTemplateGuid, this.SuppressException };

            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationPrepareSendMailExecuteCode, "To: '{0}'. CC: '{1}'. BCC: '{2}'. Email Template: '{3}'. Email Template Guid: '{4}'. Suppress Exception: '{5}'.", traceData);

            // Since the WF desinger does not allow binding boolean property SupressException
            // we might as well do tracing as well as all binding here.
            try
            {
                this.SendMail.To = this.To;
                this.SendMail.CC = this.CC;
                this.SendMail.Bcc = this.Bcc;
                this.SendMail.EmailTemplate = this.EmailTemplateGuid;
                this.SendMail.SuppressException = this.SuppressException;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationPrepareSendMailExecuteCode, "To: '{0}'. CC: '{1}'. BCC: '{2}'. Email Template: '{3}'. Email Template Guid: '{4}'. Suppress Exception: '{5}'.", traceData);
            }
        }

        #endregion
    }
}
