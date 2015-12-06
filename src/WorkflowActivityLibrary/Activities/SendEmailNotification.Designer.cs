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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
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
            this.EmailBccIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailBccIsXPathOrExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailCcIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailCcIsXPathOrExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailToIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailToIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailTemplateIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EmailTemplateIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.SendMail = new Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity();
            this.PrepareSendMail = new System.Workflow.Activities.CodeActivity();
            this.IfEmailBccIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEmailCcIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEmailToIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEmailTemplateIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.ConditionSatisfied = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.ParseExpressions = new System.Workflow.Activities.CodeActivity();
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
            this.FindEmailBccRecipients.FoundIds = null;
            this.FindEmailBccRecipients.FoundResources = null;
            this.FindEmailBccRecipients.Name = "FindEmailBccRecipients";
            this.FindEmailBccRecipients.Value = null;
            activitybind1.Name = "SendEmailNotification";
            activitybind1.Path = "Bcc";
            this.FindEmailBccRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
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
            this.FindEmailCcRecipients.FoundIds = null;
            this.FindEmailCcRecipients.FoundResources = null;
            this.FindEmailCcRecipients.Name = "FindEmailCcRecipients";
            this.FindEmailCcRecipients.Value = null;
            activitybind2.Name = "SendEmailNotification";
            activitybind2.Path = "CC";
            this.FindEmailCcRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
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
            this.FindEmailToRecipients.FoundIds = null;
            this.FindEmailToRecipients.FoundResources = null;
            this.FindEmailToRecipients.Name = "FindEmailToRecipients";
            this.FindEmailToRecipients.Value = null;
            activitybind3.Name = "SendEmailNotification";
            activitybind3.Path = "To";
            this.FindEmailToRecipients.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
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
            this.FindEmailTemplate.FoundIds = null;
            this.FindEmailTemplate.FoundResources = null;
            this.FindEmailTemplate.Name = "FindEmailTemplate";
            this.FindEmailTemplate.Value = null;
            activitybind4.Name = "SendEmailNotification";
            activitybind4.Path = "EmailTemplate";
            this.FindEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
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
            // SendMail
            // 
            this.SendMail.Bcc = null;
            this.SendMail.CC = null;
            this.SendMail.EmailTemplate = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.SendMail.Name = "SendMail";
            this.SendMail.SuppressException = false;
            this.SendMail.To = null;
            // 
            // PrepareSendMail
            // 
            this.PrepareSendMail.Name = "PrepareSendMail";
            this.PrepareSendMail.ExecuteCode += new System.EventHandler(this.PrepareSendMail_ExecuteCode);
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
            // ConditionSatisfied
            // 
            this.ConditionSatisfied.Activities.Add(this.IfEmailTemplateIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfEmailToIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfEmailCcIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfEmailBccIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.PrepareSendMail);
            this.ConditionSatisfied.Activities.Add(this.SendMail);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActivityExecutionConditionSatisfied_Condition);
            this.ConditionSatisfied.Condition = codecondition5;
            this.ConditionSatisfied.Name = "ConditionSatisfied";
            // 
            // IfActivityExecutionConditionSatisfied
            // 
            this.IfActivityExecutionConditionSatisfied.Activities.Add(this.ConditionSatisfied);
            this.IfActivityExecutionConditionSatisfied.Name = "IfActivityExecutionConditionSatisfied";
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind5.Name = "SendEmailNotification";
            activitybind5.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            this.Resolve.QueryResults = null;
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // ParseExpressions
            // 
            this.ParseExpressions.Name = "ParseExpressions";
            this.ParseExpressions.ExecuteCode += new System.EventHandler(this.ParseExpressions_ExecuteCode);
            // 
            // SendEmailNotification
            // 
            this.Activities.Add(this.ParseExpressions);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.IfActivityExecutionConditionSatisfied);
            this.Name = "SendEmailNotification";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.ResourceManagement.Workflow.Activities.EmailNotificationActivity SendMail;
        private CodeActivity PrepareSendMail;
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
        private ComponentActivities.ResolveLookups Resolve;
        private CodeActivity ResolveEmailTemplate;
        private CodeActivity CheckEmailTemplateResource;
        private IfElseBranchActivity EmailTemplateIsExpression;
        private IfElseBranchActivity EmailTemplateIsXPath;
        private IfElseActivity IfEmailTemplateIsXPathOrExpression;
        private ComponentActivities.FindResources FindEmailTemplate;
        private CodeActivity ParseExpressions;
        private IfElseBranchActivity ConditionSatisfied;
        private IfElseActivity IfActivityExecutionConditionSatisfied;
    }
}
