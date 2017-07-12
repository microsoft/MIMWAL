//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SendEmailNotification.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
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
    using System.Globalization;
    using System.Linq;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
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
        public static DependencyProperty AdvancedProperty =
            DependencyProperty.Register("Advanced", typeof(bool), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResourcesProperty =
            DependencyProperty.Register("QueryResources", typeof(bool), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueriesTableProperty =
            DependencyProperty.Register("QueriesTable", typeof(Hashtable), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty IterationProperty =
            DependencyProperty.Register("Iteration", typeof(string), typeof(SendEmailNotification));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty WorkflowDataVariablesTableProperty =
            DependencyProperty.Register("WorkflowDataVariablesTable", typeof(Hashtable), typeof(SendEmailNotification));

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
        /// The query definitions
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Definition> Queries;

        /// <summary>
        /// The value.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public object Value;

        /// <summary>
        /// The value expressions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<string, object> ValueExpressions;

        /// <summary>
        /// The email template Guid.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid EmailTemplateGuid;

        /// <summary>
        /// The list of unique identifiers of matching email templates found.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> EmailTemplateFoundIds;

        /// <summary>
        /// The list of unique identifiers of matching "To" recipients found.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> EmailToRecipientsFoundIds;

        /// <summary>
        /// The list of unique identifiers of matching "CC" recipients found.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> EmailCcRecipientsFoundIds;

        /// <summary>
        /// The list of unique identifiers of matching "Bcc" recipients found.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> EmailBccRecipientsFoundIds;

        /// <summary>
        /// The list of "To" recipients.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public string EmailNotificationToRecipients;

        /// <summary>
        /// The list of "Cc" recipients.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public string EmailNotificationCcRecipients;

        /// <summary>
        /// The list of "Bcc" recipients.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public string EmailNotificationBccRecipients;

        /// <summary>
        /// The lookup update definitions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<UpdateLookupDefinition> LookupUpdates;

        /// <summary>
        /// The results of any defined queries.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Used only internally to bind to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<string, List<Guid>> QueryResults;

        /// <summary>
        /// The workflowDataVariables definitions
        /// </summary>
        private List<Definition> workflowDataVariables = new List<Definition>();

        /// <summary>
        /// The number of iterations preformed so far
        /// </summary>
        private int iterations;

        /// <summary>
        /// Break iteration if true
        /// </summary>
        private bool breakIteration;

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

                if (this.Queries == null)
                {
                    this.Queries = new List<Definition>();
                }

                if (this.ValueExpressions == null)
                {
                    this.ValueExpressions = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }

                if (this.LookupUpdates == null)
                {
                    this.LookupUpdates = new List<UpdateLookupDefinition>();
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
        /// Gets or sets a value indicating whether to query resources.
        /// </summary>
        [Description("QueryResources")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool QueryResources
        {
            get
            {
                return (bool)this.GetValue(QueryResourcesProperty);
            }

            set
            {
                this.SetValue(QueryResourcesProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the queries hash table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("QueriesTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable QueriesTable
        {
            get
            {
                return (Hashtable)this.GetValue(QueriesTableProperty);
            }

            set
            {
                this.SetValue(QueriesTableProperty, value);
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
        /// Gets or sets the WorkflowData variables hash table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("WorkflowDataVariablesTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable WorkflowDataVariablesTable
        {
            get
            {
                return (Hashtable)this.GetValue(WorkflowDataVariablesTableProperty);
            }

            set
            {
                this.SetValue(WorkflowDataVariablesTableProperty, value);
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
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
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
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
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
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationParseExpressionsExecuteCode);

            try
            {
                // If advanced options were not specified,
                // clear any supplied advanced settings so they do not impact activity execution
                if (!this.Advanced)
                {
                    this.QueryResources = false;
                    this.ActivityExecutionCondition = null;
                    this.Iteration = null;
                    this.CC = null;
                    this.Bcc = null;
                    this.SuppressException = false;
                }

                // If the activity is configured to query for resources,
                // convert the queries hash table to a list of definitions that will feed the activity responsible
                // for their execution
                if (this.QueryResources && this.QueriesTable != null && this.QueriesTable.Count > 0)
                {
                    DefinitionsConverter queriesConverter = new DefinitionsConverter(this.QueriesTable);
                    this.Queries = queriesConverter.Definitions;
                }

                // If the activity is configured for iteration or conditional execution, parse the associated expressions
                this.ActivityExpressionEvaluator.ParseIfExpression(this.Iteration);
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);

                // Definitions are supplied to the workflow activity in the form of a hash table
                // This is necessary due to deserialization issues with lists and custom classes
                // Convert the WorkflowData variables hash table to a list of definitions that is easier to work with
                DefinitionsConverter workflowDataVariablesConverter = new DefinitionsConverter(this.WorkflowDataVariablesTable);
                this.workflowDataVariables = workflowDataVariablesConverter.Definitions;

                // Load each source expression into the evaluator so associated lookups can be loaded into the cache for resolution
                // For WorkflowData variables, the left side of the definition represents the source expression
                foreach (Definition workflowDataVariablesDefinition in this.workflowDataVariables)
                {
                    this.ActivityExpressionEvaluator.ParseExpression(workflowDataVariablesDefinition.Left);
                }

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
        /// Handles the ExecuteCode event of the PrepareIteration CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareIteration_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationPrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'.", this.Iteration, this.ActivityExecutionCondition);

            bool submitRequests = false;
            List<object> iterationValues = new List<object>();
            try
            {
                // Determine if requests should be submitted based on the configuration for conditional execution
                // If a condition was specified and that condition resolves to false, no values will be added for iteration
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
                // to ensure a single email is sent
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

                    // Pull any [//Value] or [//WorkflowData] or [//Queries] expressions from the expression evaluator's lookup cache for
                    // resolution during iteration
                    foreach (string key in from key in this.ActivityExpressionEvaluator.LookupCache.Keys let lookup = new LookupEvaluator(key) where lookup.Parameter == LookupParameter.Value || lookup.Parameter == LookupParameter.WorkflowData || lookup.Parameter == LookupParameter.Queries select key)
                    {
                        this.ValueExpressions.Add(key, null);
                    }
                }

                // Add the iteration values to the replicator activity if requests should be submitted
                this.ForEachIteration.InitialChildData = iterationValues;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationPrepareIterationExecuteCode, "Iteration: '{0}'. Condition: '{1}'. Submit Request: '{2}'. Total Iterations: '{3}'.", this.Iteration, this.ActivityExecutionCondition, submitRequests, iterationValues.Count);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);

            try
            {
                // Get the instance value so it can be used to resolve associated expressions
                // and clear previous resolutions
                this.Value = e.InstanceData;

                // Increment current iteration count
                this.iterations += 1;

                // Since the WF desinger does not allow binding boolean property SupressException we do it here
                foreach (var childActivity in (e.Activity as SequenceActivity).EnabledActivities)
                {
                    var sendMailActivity = childActivity as EmailNotificationActivity;
                    if (sendMailActivity != null)
                    {
                        sendMailActivity.SuppressException = this.SuppressException;
                        break;
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationForEachIterationChildInitialized, "Current Iteration Value: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationForEachIterationChildCompleted, "Iteration: '{0}' of '{1}'. ", this.iterations, this.ForEachIteration.InitialChildData.Count);

            try
            {
                var variableCache = this.ActivityExpressionEvaluator.VariableCache;
                this.breakIteration = Convert.ToBoolean(variableCache[ExpressionEvaluator.ReservedVariableBreakIteration], CultureInfo.InvariantCulture);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationForEachIterationChildCompleted, "Iteration: '{0}' of '{1}'. Break Iteration '{2}'.", this.iterations, this.ForEachIteration.InitialChildData.Count, this.breakIteration);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareMailTemplate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareUpdate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationPrepareUpdateExecuteCode);

            // For this activity, these are supposed to be only WorkflowData Variable updates
            try
            {
                // Load resolved value expressions to the expression evaluator
                foreach (string key in this.ValueExpressions.Keys)
                {
                    this.ActivityExpressionEvaluator.LookupCache[key] = this.ValueExpressions[key];
                }

                // Clear the variable cache for the expression evaluator
                List<string> variables = this.ActivityExpressionEvaluator.VariableCache.Keys.ToList();
                foreach (string variable in variables)
                {
                    this.ActivityExpressionEvaluator.VariableCache[variable] = null;
                }

                // Loop through each workflow data variable definition to build the
                // update resource parameters which will be used to update each target resource
                this.LookupUpdates = new List<UpdateLookupDefinition>();
                foreach (Definition workflowDataVariableDefinition in this.workflowDataVariables)
                {
                    // Resolve the source expression, including any functions or concatenation,
                    // to retrieve the typed value that should be assigned to the target attribute
                    object resolved = null;
                    if (!ExpressionEvaluator.IsExpression(workflowDataVariableDefinition.Left))
                    {
                        // This is a dynamic string for resolution already resolved
                        // so just retrive the value from cache directly
                        resolved = this.ActivityExpressionEvaluator.LookupCache[workflowDataVariableDefinition.Left];
                    }
                    else
                    {
                        resolved = this.ActivityExpressionEvaluator.ResolveExpression(workflowDataVariableDefinition.Left);
                    }

                    // Determine if we are targeting a variable
                    // If not, assume we are targeting an expression which should result in requests or update
                    // to the workflow dictionary
                    bool targetVariable = ExpressionEvaluator.DetermineParameterType(workflowDataVariableDefinition.Right) == ParameterType.Variable;

                    // Only create an update lookup definition if the value is not null, or if the
                    // update definition is configured to allow null values to be transferred to the target(s)
                    if (resolved == null && workflowDataVariableDefinition.Check)
                    {
                        if (targetVariable)
                        {
                            this.ActivityExpressionEvaluator.PublishVariable(workflowDataVariableDefinition.Right, null, UpdateMode.Modify);
                        }
                        else
                        {
                            this.LookupUpdates.Add(new UpdateLookupDefinition(workflowDataVariableDefinition.Right, null, UpdateMode.Modify));
                        }
                    }
                    else if (resolved != null)
                    {
                        if (resolved.GetType() == typeof(InsertedValuesCollection))
                        {
                            // If the resolved object is an InsertedValues collection, the source for the update definition includes the InsertValues function
                            // All associated values should be added to the target
                            foreach (object o in (InsertedValuesCollection)resolved)
                            {
                                if (targetVariable)
                                {
                                    this.ActivityExpressionEvaluator.PublishVariable(workflowDataVariableDefinition.Right, o, UpdateMode.Insert);
                                }
                                else
                                {
                                    this.LookupUpdates.Add(new UpdateLookupDefinition(workflowDataVariableDefinition.Right, o, UpdateMode.Insert));
                                }
                            }
                        }
                        else if (resolved.GetType() == typeof(RemovedValuesCollection))
                        {
                            // If the resolved object is a RemovedValues collection, the source for the update definition includes the RemoveValues function
                            // All associated values should be removed from the target
                            foreach (object o in (RemovedValuesCollection)resolved)
                            {
                                if (targetVariable)
                                {
                                    this.ActivityExpressionEvaluator.PublishVariable(workflowDataVariableDefinition.Right, o, UpdateMode.Remove);
                                }
                                else
                                {
                                    this.LookupUpdates.Add(new UpdateLookupDefinition(workflowDataVariableDefinition.Right, o, UpdateMode.Remove));
                                }
                            }
                        }
                        else
                        {
                            // For all other conditions, update the variable or build a new update request parameter for the target attribute
                            if (targetVariable)
                            {
                                this.ActivityExpressionEvaluator.PublishVariable(workflowDataVariableDefinition.Right, resolved, UpdateMode.Modify);
                            }
                            else
                            {
                                this.LookupUpdates.Add(new UpdateLookupDefinition(workflowDataVariableDefinition.Right, resolved, UpdateMode.Modify));
                            }
                        }
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationPrepareUpdateExecuteCode);
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
                if (this.EmailTemplateFoundIds.Count == 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationCheckEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_MissingEmailTemplateError, this.EmailTemplate));
                }

                if (this.EmailTemplateFoundIds.Count > 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.SendEmailNotificationCheckEmailTemplateResourceExecuteCodeError, new WorkflowActivityLibraryException(Messages.SendEmailNotification_MultipleEmailTemplatesError, this.EmailTemplate));
                }

                this.EmailTemplateGuid = this.EmailTemplateFoundIds[0];
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
        /// Handles the ExecuteCode event of the CheckEmailToRecipientResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEmailToRecipientResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationCheckEmailToRecipientResourcesExecuteCode, "To recipients count: {0}. Recipient: '{1}'.", this.EmailToRecipientsFoundIds.Count, this.To);

            try
            {
                if (this.EmailToRecipientsFoundIds.Count == 0)
                {
                    Logger.Instance.WriteWarning(EventIdentifier.SendEmailNotificationCheckEmailToRecipientResourcesExecuteCodeWarning, Messages.SendEmailNotification_ToRecipientNotFoundError, this.To);
                    this.EmailNotificationToRecipients = null;
                }
                else
                {
                    string[] recipients = (from id in this.EmailToRecipientsFoundIds select id.ToString()).ToArray();
                    this.EmailNotificationToRecipients = string.Join(";", recipients);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationCheckEmailToRecipientResourcesExecuteCode, "To recipients count: {0}. Recipient: '{1}'. Returning: '{2}'.", this.EmailToRecipientsFoundIds.Count, this.To, this.EmailNotificationToRecipients);
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

                this.EmailNotificationToRecipients = recipient;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationResolveEmailToRecipientsExecuteCode, "To Recipients: '{0}'. Returning: '{1}'.", this.To, this.EmailNotificationToRecipients);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckEmailCcRecipientResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEmailCcRecipientResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationCheckEmailCcRecipientResourcesExecuteCode, "CC recipients count: {0}. Recipient: '{1}'.", this.EmailCcRecipientsFoundIds.Count, this.CC);

            try
            {
                if (this.EmailCcRecipientsFoundIds.Count == 0)
                {
                    this.EmailNotificationCcRecipients = null;
                }
                else
                {
                    string[] recipients = (from id in this.EmailCcRecipientsFoundIds select id.ToString()).ToArray();
                    this.EmailNotificationCcRecipients = string.Join(";", recipients);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationCheckEmailCcRecipientResourcesExecuteCode, "CC recipients count: {0}. Recipient: '{1}'. Returning: '{2}'.", this.EmailCcRecipientsFoundIds.Count, this.CC, this.EmailNotificationCcRecipients);
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
                this.EmailNotificationCcRecipients = this.FormatRecipient(this.CC);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationResolveEmailCcRecipientsExecuteCode, "CC Recipients: '{0}'. Returning: '{1}'.", this.CC, this.EmailNotificationCcRecipients);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the CheckEmailBccRecipientResources CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckEmailBccRecipientResources_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationCheckEmailBccRecipientResourcesExecuteCode, "Bcc recipients count: {0}. Recipient: '{1}'.", this.EmailBccRecipientsFoundIds.Count, this.Bcc);

            try
            {
                if (this.EmailBccRecipientsFoundIds.Count == 0)
                {
                    this.EmailNotificationBccRecipients = null;
                }
                else
                {
                    string[] recipients = (from id in this.EmailBccRecipientsFoundIds select id.ToString()).ToArray();
                    this.EmailNotificationBccRecipients = string.Join(";", recipients);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationCheckEmailBccRecipientResourcesExecuteCode, "Bcc recipients count: {0}. Recipient: '{1}'. Returning: '{2}'.", this.EmailBccRecipientsFoundIds.Count, this.Bcc, this.EmailNotificationBccRecipients);
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
                this.EmailNotificationBccRecipients = this.FormatRecipient(this.Bcc);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationResolveEmailBccRecipientsExecuteCode, "Bcc Recipients: '{0}'. Returning: '{1}'.", this.Bcc, this.EmailNotificationBccRecipients);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareSendMail CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TraceSendMail_ExecuteCode(object sender, EventArgs e)
        {
            var traceData = new object[] { this.EmailNotificationToRecipients, this.EmailNotificationCcRecipients, this.EmailNotificationBccRecipients, this.EmailTemplate, this.EmailTemplateGuid, this.SuppressException };

            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationTraceSendMailExecuteCode, "To: '{0}'. CC: '{1}'. BCC: '{2}'. Email Template: '{3}'. Email Template Guid: '{4}'. Suppress Exception: '{5}'.", traceData);

            try
            {
                Logger.Instance.WriteInfo(EventIdentifier.SendEmailNotificationTraceSendMailExecuteCode, "To: '{0}'. CC: '{1}'. BCC: '{2}'. Email Template: '{3}'. Email Template Guid: '{4}'. Suppress Exception: '{5}'.", traceData);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationTraceSendMailExecuteCode, "To: '{0}'. CC: '{1}'. BCC: '{2}'. Email Template: '{3}'. Email Template Guid: '{4}'. Suppress Exception: '{5}'.", traceData);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the QueriesHaveNoValueExpressions condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void QueriesHaveNoValueExpressions_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationQueriesHaveNoValueExpressionsCondition, "QueryResources: '{0}'. ", this.QueryResources);

            e.Result = true;
            try
            {
                if (this.QueryResources && this.Queries.Count > 0)
                {
                    e.Result = !this.Queries.Any(query => query.Right.IndexOf("[//" + LookupParameter.Value.ToString(), StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationQueriesHaveNoValueExpressionsCondition, "QueryResources: '{0}'. Condition evaluated '{1}'.", this.QueryResources, e.Result);
            }
        }

        /// <summary>
        /// Handles the UntilCondition event of the ForEachIteration ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ForEachIteration_UntilCondition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SendEmailNotificationForEachIterationUntilCondition, "Iteration: '{0}' of '{1}'. ", this.iterations, this.ForEachIteration.InitialChildData == null ? 0 : this.ForEachIteration.InitialChildData.Count);

            int maxIterations = 0;
            try
            {
                maxIterations = this.ForEachIteration.InitialChildData == null ? 0 : this.ForEachIteration.InitialChildData.Count;
                if (this.iterations == maxIterations || this.breakIteration)
                {
                    e.Result = true;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SendEmailNotificationForEachIterationUntilCondition, "Iteration: '{0}' of '{1}'. Condition evaluated '{2}'.", this.iterations, maxIterations, e.Result);
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
        /// Handles the Condition event of the EmailToIsXPath condition.
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
        /// Handles the Condition event of the EmailCcIsXPath condition.
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
        /// Handles the Condition event of the EmailBccIsXPath condition.
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

        #endregion

        #endregion
    }
}
