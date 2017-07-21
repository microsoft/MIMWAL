using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities
{
    public partial class SendEmailNotification
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("", "")]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind22 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind23 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind24 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind25 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind26 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind27 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind28 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind29 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind30 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind31 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition7 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind32 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind33 = new System.Workflow.ComponentModel.ActivityBind();
            this.ResolveEmailBccRecipients = new System.Workflow.Activities.CodeActivity();
            this.CheckEmailBccRecipientResources = new System.Workflow.Activities.CodeActivity();
            this.FindEmailBccRecipients = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveEmailCcRecipients = new System.Workflow.Activities.CodeActivity();
            this.CheckEmailCcRecipientResources = new System.Workflow.Activities.CodeActivity();
            this.FindEmailCcRecipients = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveEmailToRecipients = new System.Workflow.Activities.CodeActivity();
            this.CheckEmailToRecipientResources = new System.Workflow.Activities.CodeActivity();
            this.FindEmailToRecipients = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveEmailTemplate = new System.Workflow.Activities.CodeActivity();
            this.CheckEmailTemplateResource = new System.Workflow.Activities.CodeActivity();
            this.FindEmailTemplate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.RunQueriesIterative = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries();
            this.EmailBccIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailBccIsXPathOrExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailCcIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailCcIsXPathOrExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailToIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailToIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailTemplateIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailTemplateIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.QueriesDoHaveValueExpressions = new System.Workflow.Activities.IfElseBranchActivity();
            this.QueriesDoNotHaveValueExpressions = new System.Workflow.Activities.IfElseBranchActivity();
            this.SendMail = new Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity();
            this.TraceSendMail = new System.Workflow.Activities.CodeActivity();
            this.IfEmailBccIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEmailCcIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEmailToIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEmailTemplateIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.Update = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups();
            this.PrepareUpdate = new System.Workflow.Activities.CodeActivity();
            this.ResolveForValue = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.IfQueriesHaveValueExpressions = new System.Workflow.Activities.IfElseActivity();
            this.RunQueriesOnce = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries();
            this.ProcessMail = new System.Workflow.Activities.SequenceActivity();
            this.QueriesHaveNoValueExpressions = new System.Workflow.Activities.IfElseBranchActivity();
            this.ForEachIteration = new System.Workflow.Activities.ReplicatorActivity();
            this.PrepareIteration = new System.Workflow.Activities.CodeActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.IfQueriesHaveNoValueExpressions = new System.Workflow.Activities.IfElseActivity();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // ResolveEmailBccRecipients
            // 
            this.ResolveEmailBccRecipients.Name = "ResolveEmailBccRecipients";
            this.ResolveEmailBccRecipients.ExecuteCode += new System.EventHandler(this.ResolveEmailBccRecipients_ExecuteCode);
            // 
            // CheckEmailBccRecipientResources
            // 
            this.CheckEmailBccRecipientResources.Name = "CheckEmailBccRecipientResources";
            this.CheckEmailBccRecipientResources.ExecuteCode += new System.EventHandler(this.CheckEmailBccRecipientResources_ExecuteCode);
            // 
            // FindEmailBccRecipients
            // 
            this.FindEmailBccRecipients.Attributes = null;
            this.FindEmailBccRecipients.ExcludeWorkflowTarget = false;
            activitybind1.Name = "SendEmailNotification";
            activitybind1.Path = "EmailBccRecipientsFoundIds";
            this.FindEmailBccRecipients.FoundResources = null;
            this.FindEmailBccRecipients.Name = "FindEmailBccRecipients";
            activitybind2.Name = "SendEmailNotification";
            activitybind2.Path = "QueryResults";
            activitybind3.Name = "SendEmailNotification";
            activitybind3.Path = "Value";
            activitybind4.Name = "SendEmailNotification";
            activitybind4.Path = "Bcc";
            this.FindEmailBccRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.FindEmailBccRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.FindEmailBccRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.FindEmailBccRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundIdsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ResolveEmailCcRecipients
            // 
            this.ResolveEmailCcRecipients.Name = "ResolveEmailCcRecipients";
            this.ResolveEmailCcRecipients.ExecuteCode += new System.EventHandler(this.ResolveEmailCcRecipients_ExecuteCode);
            // 
            // CheckEmailCcRecipientResources
            // 
            this.CheckEmailCcRecipientResources.Name = "CheckEmailCcRecipientResources";
            this.CheckEmailCcRecipientResources.ExecuteCode += new System.EventHandler(this.CheckEmailCcRecipientResources_ExecuteCode);
            // 
            // FindEmailCcRecipients
            // 
            this.FindEmailCcRecipients.Attributes = null;
            this.FindEmailCcRecipients.ExcludeWorkflowTarget = false;
            activitybind5.Name = "SendEmailNotification";
            activitybind5.Path = "EmailCcRecipientsFoundIds";
            this.FindEmailCcRecipients.FoundResources = null;
            this.FindEmailCcRecipients.Name = "FindEmailCcRecipients";
            activitybind6.Name = "SendEmailNotification";
            activitybind6.Path = "QueryResults";
            activitybind7.Name = "SendEmailNotification";
            activitybind7.Path = "Value";
            activitybind8.Name = "SendEmailNotification";
            activitybind8.Path = "CC";
            this.FindEmailCcRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.FindEmailCcRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.FindEmailCcRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.FindEmailCcRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundIdsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // ResolveEmailToRecipients
            // 
            this.ResolveEmailToRecipients.Name = "ResolveEmailToRecipients";
            this.ResolveEmailToRecipients.ExecuteCode += new System.EventHandler(this.ResolveEmailToRecipients_ExecuteCode);
            // 
            // CheckEmailToRecipientResources
            // 
            this.CheckEmailToRecipientResources.Name = "CheckEmailToRecipientResources";
            this.CheckEmailToRecipientResources.ExecuteCode += new System.EventHandler(this.CheckEmailToRecipientResources_ExecuteCode);
            // 
            // FindEmailToRecipients
            // 
            this.FindEmailToRecipients.Attributes = null;
            this.FindEmailToRecipients.ExcludeWorkflowTarget = false;
            activitybind9.Name = "SendEmailNotification";
            activitybind9.Path = "EmailToRecipientsFoundIds";
            this.FindEmailToRecipients.FoundResources = null;
            this.FindEmailToRecipients.Name = "FindEmailToRecipients";
            activitybind10.Name = "SendEmailNotification";
            activitybind10.Path = "QueryResults";
            activitybind11.Name = "SendEmailNotification";
            activitybind11.Path = "Value";
            activitybind12.Name = "SendEmailNotification";
            activitybind12.Path = "To";
            this.FindEmailToRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.FindEmailToRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.FindEmailToRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.FindEmailToRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundIdsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // ResolveEmailTemplate
            // 
            this.ResolveEmailTemplate.Name = "ResolveEmailTemplate";
            this.ResolveEmailTemplate.ExecuteCode += new System.EventHandler(this.ResolveEmailTemplate_ExecuteCode);
            // 
            // CheckEmailTemplateResource
            // 
            this.CheckEmailTemplateResource.Name = "CheckEmailTemplateResource";
            this.CheckEmailTemplateResource.ExecuteCode += new System.EventHandler(this.CheckEmailTemplateResource_ExecuteCode);
            // 
            // FindEmailTemplate
            // 
            this.FindEmailTemplate.Attributes = null;
            this.FindEmailTemplate.ExcludeWorkflowTarget = false;
            activitybind13.Name = "SendEmailNotification";
            activitybind13.Path = "EmailTemplateFoundIds";
            this.FindEmailTemplate.FoundResources = null;
            this.FindEmailTemplate.Name = "FindEmailTemplate";
            activitybind14.Name = "SendEmailNotification";
            activitybind14.Path = "QueryResults";
            activitybind15.Name = "SendEmailNotification";
            activitybind15.Path = "Value";
            activitybind16.Name = "SendEmailNotification";
            activitybind16.Path = "EmailTemplate";
            this.FindEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.FindEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            this.FindEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.FindEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundIdsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            // 
            // RunQueriesIterative
            // 
            this.RunQueriesIterative.Name = "RunQueriesIterative";
            activitybind17.Name = "SendEmailNotification";
            activitybind17.Path = "Queries";
            activitybind18.Name = "SendEmailNotification";
            activitybind18.Path = "QueryResults";
            activitybind19.Name = "SendEmailNotification";
            activitybind19.Path = "Value";
            this.RunQueriesIterative.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.QueryDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.RunQueriesIterative.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            this.RunQueriesIterative.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            // 
            // EmailBccIsExpression
            // 
            this.EmailBccIsExpression.Activities.Add(this.ResolveEmailBccRecipients);
            this.EmailBccIsExpression.Name = "EmailBccIsExpression";
            // 
            // EmailBccIsXPathOrExpression
            // 
            this.EmailBccIsXPathOrExpression.Activities.Add(this.FindEmailBccRecipients);
            this.EmailBccIsXPathOrExpression.Activities.Add(this.CheckEmailBccRecipientResources);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.EmailBccIsXPath_Condition);
            this.EmailBccIsXPathOrExpression.Condition = codecondition1;
            this.EmailBccIsXPathOrExpression.Name = "EmailBccIsXPathOrExpression";
            // 
            // EmailCcIsExpression
            // 
            this.EmailCcIsExpression.Activities.Add(this.ResolveEmailCcRecipients);
            this.EmailCcIsExpression.Name = "EmailCcIsExpression";
            // 
            // EmailCcIsXPathOrExpression
            // 
            this.EmailCcIsXPathOrExpression.Activities.Add(this.FindEmailCcRecipients);
            this.EmailCcIsXPathOrExpression.Activities.Add(this.CheckEmailCcRecipientResources);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.EmailCcIsXPath_Condition);
            this.EmailCcIsXPathOrExpression.Condition = codecondition2;
            this.EmailCcIsXPathOrExpression.Name = "EmailCcIsXPathOrExpression";
            // 
            // EmailToIsExpression
            // 
            this.EmailToIsExpression.Activities.Add(this.ResolveEmailToRecipients);
            this.EmailToIsExpression.Name = "EmailToIsExpression";
            // 
            // EmailToIsXPath
            // 
            this.EmailToIsXPath.Activities.Add(this.FindEmailToRecipients);
            this.EmailToIsXPath.Activities.Add(this.CheckEmailToRecipientResources);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.EmailToIsXPath_Condition);
            this.EmailToIsXPath.Condition = codecondition3;
            this.EmailToIsXPath.Name = "EmailToIsXPath";
            // 
            // EmailTemplateIsExpression
            // 
            this.EmailTemplateIsExpression.Activities.Add(this.ResolveEmailTemplate);
            this.EmailTemplateIsExpression.Name = "EmailTemplateIsExpression";
            // 
            // EmailTemplateIsXPath
            // 
            this.EmailTemplateIsXPath.Activities.Add(this.FindEmailTemplate);
            this.EmailTemplateIsXPath.Activities.Add(this.CheckEmailTemplateResource);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.EmailTemplateIsXPath_Condition);
            this.EmailTemplateIsXPath.Condition = codecondition4;
            this.EmailTemplateIsXPath.Name = "EmailTemplateIsXPath";
            // 
            // QueriesDoHaveValueExpressions
            // 
            this.QueriesDoHaveValueExpressions.Activities.Add(this.RunQueriesIterative);
            this.QueriesDoHaveValueExpressions.Name = "QueriesDoHaveValueExpressions";
            // 
            // QueriesDoNotHaveValueExpressions
            // 
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.QueriesHaveNoValueExpressions_Condition);
            this.QueriesDoNotHaveValueExpressions.Condition = codecondition5;
            this.QueriesDoNotHaveValueExpressions.Name = "QueriesDoNotHaveValueExpressions";
            // 
            // SendMail
            // 
            activitybind20.Name = "SendEmailNotification";
            activitybind20.Path = "EmailNotificationBccRecipients";
            activitybind21.Name = "SendEmailNotification";
            activitybind21.Path = "EmailNotificationCcRecipients";
            activitybind22.Name = "SendEmailNotification";
            activitybind22.Path = "EmailTemplateGuid";
            this.SendMail.Name = "SendMail";
            this.SendMail.SuppressException = false;
            activitybind23.Name = "SendEmailNotification";
            activitybind23.Path = "EmailNotificationToRecipients";
            this.SendMail.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity.BccProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            this.SendMail.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity.CCProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            this.SendMail.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity.ToProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind23)));
            this.SendMail.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity.EmailTemplateProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind22)));
            // 
            // TraceSendMail
            // 
            this.TraceSendMail.Name = "TraceSendMail";
            this.TraceSendMail.ExecuteCode += new System.EventHandler(this.TraceSendMail_ExecuteCode);
            // 
            // IfEmailBccIsXPathOrExpression
            // 
            this.IfEmailBccIsXPathOrExpression.Activities.Add(this.EmailBccIsXPathOrExpression);
            this.IfEmailBccIsXPathOrExpression.Activities.Add(this.EmailBccIsExpression);
            this.IfEmailBccIsXPathOrExpression.Name = "IfEmailBccIsXPathOrExpression";
            // 
            // IfEmailCcIsXPathOrExpression
            // 
            this.IfEmailCcIsXPathOrExpression.Activities.Add(this.EmailCcIsXPathOrExpression);
            this.IfEmailCcIsXPathOrExpression.Activities.Add(this.EmailCcIsExpression);
            this.IfEmailCcIsXPathOrExpression.Name = "IfEmailCcIsXPathOrExpression";
            // 
            // IfEmailToIsXPathOrExpression
            // 
            this.IfEmailToIsXPathOrExpression.Activities.Add(this.EmailToIsXPath);
            this.IfEmailToIsXPathOrExpression.Activities.Add(this.EmailToIsExpression);
            this.IfEmailToIsXPathOrExpression.Name = "IfEmailToIsXPathOrExpression";
            // 
            // IfEmailTemplateIsXPathOrExpression
            // 
            this.IfEmailTemplateIsXPathOrExpression.Activities.Add(this.EmailTemplateIsXPath);
            this.IfEmailTemplateIsXPathOrExpression.Activities.Add(this.EmailTemplateIsExpression);
            this.IfEmailTemplateIsXPathOrExpression.Name = "IfEmailTemplateIsXPathOrExpression";
            // 
            // Update
            // 
            this.Update.Actor = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.Update.ApplyAuthorizationPolicy = false;
            this.Update.Name = "Update";
            activitybind24.Name = "SendEmailNotification";
            activitybind24.Path = "QueryResults";
            activitybind25.Name = "SendEmailNotification";
            activitybind25.Path = "LookupUpdates";
            activitybind26.Name = "SendEmailNotification";
            activitybind26.Path = "Value";
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind24)));
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.UpdateLookupDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind25)));
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind26)));
            // 
            // PrepareUpdate
            // 
            this.PrepareUpdate.Name = "PrepareUpdate";
            this.PrepareUpdate.ExecuteCode += new System.EventHandler(this.PrepareUpdate_ExecuteCode);
            // 
            // ResolveForValue
            // 
            this.ResolveForValue.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind27.Name = "SendEmailNotification";
            activitybind27.Path = "ValueExpressions";
            this.ResolveForValue.Name = "ResolveForValue";
            activitybind28.Name = "SendEmailNotification";
            activitybind28.Path = "QueryResults";
            activitybind29.Name = "SendEmailNotification";
            activitybind29.Path = "Value";
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind27)));
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind29)));
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind28)));
            // 
            // IfQueriesHaveValueExpressions
            // 
            this.IfQueriesHaveValueExpressions.Activities.Add(this.QueriesDoNotHaveValueExpressions);
            this.IfQueriesHaveValueExpressions.Activities.Add(this.QueriesDoHaveValueExpressions);
            this.IfQueriesHaveValueExpressions.Name = "IfQueriesHaveValueExpressions";
            // 
            // RunQueriesOnce
            // 
            this.RunQueriesOnce.Name = "RunQueriesOnce";
            activitybind30.Name = "SendEmailNotification";
            activitybind30.Path = "Queries";
            activitybind31.Name = "SendEmailNotification";
            activitybind31.Path = "QueryResults";
            this.RunQueriesOnce.Value = null;
            this.RunQueriesOnce.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.QueryDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind30)));
            this.RunQueriesOnce.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind31)));
            // 
            // ProcessMail
            // 
            this.ProcessMail.Activities.Add(this.IfQueriesHaveValueExpressions);
            this.ProcessMail.Activities.Add(this.ResolveForValue);
            this.ProcessMail.Activities.Add(this.PrepareUpdate);
            this.ProcessMail.Activities.Add(this.Update);
            this.ProcessMail.Activities.Add(this.IfEmailTemplateIsXPathOrExpression);
            this.ProcessMail.Activities.Add(this.IfEmailToIsXPathOrExpression);
            this.ProcessMail.Activities.Add(this.IfEmailCcIsXPathOrExpression);
            this.ProcessMail.Activities.Add(this.IfEmailBccIsXPathOrExpression);
            this.ProcessMail.Activities.Add(this.TraceSendMail);
            this.ProcessMail.Activities.Add(this.SendMail);
            this.ProcessMail.Name = "ProcessMail";
            // 
            // QueriesHaveNoValueExpressions
            // 
            this.QueriesHaveNoValueExpressions.Activities.Add(this.RunQueriesOnce);
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.QueriesHaveNoValueExpressions_Condition);
            this.QueriesHaveNoValueExpressions.Condition = codecondition6;
            this.QueriesHaveNoValueExpressions.Name = "QueriesHaveNoValueExpressions";
            // 
            // ForEachIteration
            // 
            this.ForEachIteration.Activities.Add(this.ProcessMail);
            this.ForEachIteration.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachIteration.Name = "ForEachIteration";
            codecondition7.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ForEachIteration_UntilCondition);
            this.ForEachIteration.UntilCondition = codecondition7;
            this.ForEachIteration.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachIteration_ChildInitialized);
            this.ForEachIteration.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachIteration_ChildCompleted);
            // 
            // PrepareIteration
            // 
            this.PrepareIteration.Name = "PrepareIteration";
            this.PrepareIteration.ExecuteCode += new System.EventHandler(this.PrepareIteration_ExecuteCode);
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind32.Name = "SendEmailNotification";
            activitybind32.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            activitybind33.Name = "SendEmailNotification";
            activitybind33.Path = "QueryResults";
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind32)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind33)));
            // 
            // IfQueriesHaveNoValueExpressions
            // 
            this.IfQueriesHaveNoValueExpressions.Activities.Add(this.QueriesHaveNoValueExpressions);
            this.IfQueriesHaveNoValueExpressions.Name = "IfQueriesHaveNoValueExpressions";
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // SendEmailNotification
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.IfQueriesHaveNoValueExpressions);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.PrepareIteration);
            this.Activities.Add(this.ForEachIteration);
            this.Name = "SendEmailNotification";
            this.CanModifyActivities = false;

        }

        #endregion

        private ComponentActivities.ResolveLookups ResolveLookups;
        private ComponentActivities.ResolveQueries ResolveQueries;
        private IfElseBranchActivity QueriesDoHaveValueExpressions;
        private ComponentActivities.ResolveQueries RunQueriesOnce;
        private IfElseBranchActivity QueriesHaveNoValueExpressions;
        private IfElseActivity IfQueriesHaveNoValueExpressions;
        private IfElseBranchActivity QueriesDoNotHaveValueExpressions;
        private IfElseActivity IfQueriesHaveValueExpressions;
        private ComponentActivities.ResolveLookups Resolve;
        private ComponentActivities.UpdateLookups Update;
        private CodeActivity TraceSendMail;
        private CodeActivity PrepareUpdate;
        private ComponentActivities.ResolveQueries RunQueriesIterative;
        private CodeActivity PrepareIteration;
        private ComponentActivities.ResolveLookups ResolveForValue;
        private SequenceActivity ProcessMail;
        private ReplicatorActivity ForEachIteration;
        private Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity SendMail;
        private CodeActivity ResolveEmailBccRecipients;
        private CodeActivity CheckEmailBccRecipientResources;
        private ComponentActivities.FindResources FindEmailBccRecipients;
        private IfElseBranchActivity EmailBccIsExpression;
        private IfElseBranchActivity EmailBccIsXPathOrExpression;
        private IfElseActivity IfEmailBccIsXPathOrExpression;
        private CodeActivity ResolveEmailCcRecipients;
        private CodeActivity CheckEmailCcRecipientResources;
        private ComponentActivities.FindResources FindEmailCcRecipients;
        private IfElseBranchActivity EmailCcIsExpression;
        private IfElseBranchActivity EmailCcIsXPathOrExpression;
        private IfElseActivity IfEmailCcIsXPathOrExpression;
        private CodeActivity ResolveEmailToRecipients;
        private CodeActivity CheckEmailToRecipientResources;
        private ComponentActivities.FindResources FindEmailToRecipients;
        private IfElseBranchActivity EmailToIsExpression;
        private IfElseBranchActivity EmailToIsXPath;
        private IfElseActivity IfEmailToIsXPathOrExpression;
        private CodeActivity ResolveEmailTemplate;
        private CodeActivity CheckEmailTemplateResource;
        private IfElseBranchActivity EmailTemplateIsExpression;
        private IfElseBranchActivity EmailTemplateIsXPath;
        private IfElseActivity IfEmailTemplateIsXPathOrExpression;
        private ComponentActivities.FindResources FindEmailTemplate;
        private CodeActivity Prepare;
    }
}
