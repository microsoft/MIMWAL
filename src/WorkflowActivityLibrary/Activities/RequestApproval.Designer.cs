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
    public partial class RequestApproval
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition7 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition8 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            this.ResolveApprovalTimeoutEmailTemplate = new System.Workflow.Activities.CodeActivity();
            this.CheckApprovalTimeoutEmailTemplateResource = new System.Workflow.Activities.CodeActivity();
            this.FindApprovalTimeoutEmailTemplate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveApprovalDeniedEmailTemplate = new System.Workflow.Activities.CodeActivity();
            this.CheckApprovalDeniedEmailTemplateResource = new System.Workflow.Activities.CodeActivity();
            this.FindApprovalDeniedEmailTemplate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveApprovalCompleteEmailTemplate = new System.Workflow.Activities.CodeActivity();
            this.CheckApprovalCompleteEmailTemplateResource = new System.Workflow.Activities.CodeActivity();
            this.FindApprovalCompleteEmailTemplate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveEscalationEmailTemplate = new System.Workflow.Activities.CodeActivity();
            this.CheckEscalationEmailTemplateResource = new System.Workflow.Activities.CodeActivity();
            this.FindEscalationEmailTemplate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveApprovalEmailTemplate = new System.Workflow.Activities.CodeActivity();
            this.CheckApprovalEmailTemplateResource = new System.Workflow.Activities.CodeActivity();
            this.FindApprovalEmailTemplate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveEscalation = new System.Workflow.Activities.CodeActivity();
            this.CheckEscalationResources = new System.Workflow.Activities.CodeActivity();
            this.FindEscalation = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveApprovers = new System.Workflow.Activities.CodeActivity();
            this.CheckApproverResources = new System.Workflow.Activities.CodeActivity();
            this.FindApprovers = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ApprovalTimeoutEmailTemplateIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApprovalTimeoutEmailTemplateIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApprovalDeniedEmailTemplateIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApprovalDeniedEmailTemplateIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApprovalCompleteEmailTemplateIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApprovalCompleteEmailTemplateIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.EscalationEmailTemplateIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EscalationEmailTemplateIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApprovalEmailTemplateIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApprovalEmailTemplateIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.EscalationIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.EscalationIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApproversIsExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.ApproversIsXPath = new System.Workflow.Activities.IfElseBranchActivity();
            this.CreateApproval = new Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity();
            this.TraceCreateApproval = new System.Workflow.Activities.CodeActivity();
            this.IfApprovalTimeoutEmailTemplateIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfApprovalDeniedEmailTemplateIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfApprovalCompleteEmailTemplateIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEscalationEmailTemplateIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfApprovalEmailTemplateIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfEscalationIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.IfApproversIsXPathOrExpression = new System.Workflow.Activities.IfElseActivity();
            this.ResolveDuration = new System.Workflow.Activities.CodeActivity();
            this.ResolveThreshold = new System.Workflow.Activities.CodeActivity();
            this.ConditionSatisfied = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.ParseExpressions = new System.Workflow.Activities.CodeActivity();
            // 
            // ResolveApprovalTimeoutEmailTemplate
            // 
            this.ResolveApprovalTimeoutEmailTemplate.Name = "ResolveApprovalTimeoutEmailTemplate";
            this.ResolveApprovalTimeoutEmailTemplate.ExecuteCode += new System.EventHandler(this.ResolveApprovalTimeoutEmailTemplate_ExecuteCode);
            // 
            // CheckApprovalTimeoutEmailTemplateResource
            // 
            this.CheckApprovalTimeoutEmailTemplateResource.Name = "CheckApprovalTimeoutEmailTemplateResource";
            this.CheckApprovalTimeoutEmailTemplateResource.ExecuteCode += new System.EventHandler(this.CheckApprovalTimeoutEmailTemplateResource_ExecuteCode);
            // 
            // FindApprovalTimeoutEmailTemplate
            // 
            this.FindApprovalTimeoutEmailTemplate.Attributes = null;
            this.FindApprovalTimeoutEmailTemplate.ExcludeWorkflowTarget = false;
            this.FindApprovalTimeoutEmailTemplate.FoundIds = null;
            this.FindApprovalTimeoutEmailTemplate.FoundResources = null;
            this.FindApprovalTimeoutEmailTemplate.Name = "FindApprovalTimeoutEmailTemplate";
            this.FindApprovalTimeoutEmailTemplate.Value = null;
            activitybind1.Name = "RequestApproval";
            activitybind1.Path = "ApprovalTimeoutEmailTemplate";
            this.FindApprovalTimeoutEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ResolveApprovalDeniedEmailTemplate
            // 
            this.ResolveApprovalDeniedEmailTemplate.Name = "ResolveApprovalDeniedEmailTemplate";
            this.ResolveApprovalDeniedEmailTemplate.ExecuteCode += new System.EventHandler(this.ResolveApprovalDeniedEmailTemplate_ExecuteCode);
            // 
            // CheckApprovalDeniedEmailTemplateResource
            // 
            this.CheckApprovalDeniedEmailTemplateResource.Name = "CheckApprovalDeniedEmailTemplateResource";
            this.CheckApprovalDeniedEmailTemplateResource.ExecuteCode += new System.EventHandler(this.CheckApprovalDeniedEmailTemplateResource_ExecuteCode);
            // 
            // FindApprovalDeniedEmailTemplate
            // 
            this.FindApprovalDeniedEmailTemplate.Attributes = null;
            this.FindApprovalDeniedEmailTemplate.ExcludeWorkflowTarget = false;
            this.FindApprovalDeniedEmailTemplate.FoundIds = null;
            this.FindApprovalDeniedEmailTemplate.FoundResources = null;
            this.FindApprovalDeniedEmailTemplate.Name = "FindApprovalDeniedEmailTemplate";
            this.FindApprovalDeniedEmailTemplate.Value = null;
            activitybind2.Name = "RequestApproval";
            activitybind2.Path = "ApprovalDeniedEmailTemplate";
            this.FindApprovalDeniedEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // ResolveApprovalCompleteEmailTemplate
            // 
            this.ResolveApprovalCompleteEmailTemplate.Name = "ResolveApprovalCompleteEmailTemplate";
            this.ResolveApprovalCompleteEmailTemplate.ExecuteCode += new System.EventHandler(this.ResolveApprovalCompleteEmailTemplate_ExecuteCode);
            // 
            // CheckApprovalCompleteEmailTemplateResource
            // 
            this.CheckApprovalCompleteEmailTemplateResource.Name = "CheckApprovalCompleteEmailTemplateResource";
            this.CheckApprovalCompleteEmailTemplateResource.ExecuteCode += new System.EventHandler(this.CheckApprovalCompleteEmailTemplateResource_ExecuteCode);
            // 
            // FindApprovalCompleteEmailTemplate
            // 
            this.FindApprovalCompleteEmailTemplate.Attributes = null;
            this.FindApprovalCompleteEmailTemplate.ExcludeWorkflowTarget = false;
            this.FindApprovalCompleteEmailTemplate.FoundIds = null;
            this.FindApprovalCompleteEmailTemplate.FoundResources = null;
            this.FindApprovalCompleteEmailTemplate.Name = "FindApprovalCompleteEmailTemplate";
            this.FindApprovalCompleteEmailTemplate.Value = null;
            activitybind3.Name = "RequestApproval";
            activitybind3.Path = "ApprovalCompleteEmailTemplate";
            this.FindApprovalCompleteEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // ResolveEscalationEmailTemplate
            // 
            this.ResolveEscalationEmailTemplate.Name = "ResolveEscalationEmailTemplate";
            this.ResolveEscalationEmailTemplate.ExecuteCode += new System.EventHandler(this.ResolveEscalationEmailTemplate_ExecuteCode);
            // 
            // CheckEscalationEmailTemplateResource
            // 
            this.CheckEscalationEmailTemplateResource.Name = "CheckEscalationEmailTemplateResource";
            this.CheckEscalationEmailTemplateResource.ExecuteCode += new System.EventHandler(this.CheckEscalationEmailTemplateResource_ExecuteCode);
            // 
            // FindEscalationEmailTemplate
            // 
            this.FindEscalationEmailTemplate.Attributes = null;
            this.FindEscalationEmailTemplate.ExcludeWorkflowTarget = false;
            this.FindEscalationEmailTemplate.FoundIds = null;
            this.FindEscalationEmailTemplate.FoundResources = null;
            this.FindEscalationEmailTemplate.Name = "FindEscalationEmailTemplate";
            this.FindEscalationEmailTemplate.Value = null;
            activitybind4.Name = "RequestApproval";
            activitybind4.Path = "EscalationEmailTemplate";
            this.FindEscalationEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // ResolveApprovalEmailTemplate
            // 
            this.ResolveApprovalEmailTemplate.Name = "ResolveApprovalEmailTemplate";
            this.ResolveApprovalEmailTemplate.ExecuteCode += new System.EventHandler(this.ResolveApprovalEmailTemplate_ExecuteCode);
            // 
            // CheckApprovalEmailTemplateResource
            // 
            this.CheckApprovalEmailTemplateResource.Name = "CheckApprovalEmailTemplateResource";
            this.CheckApprovalEmailTemplateResource.ExecuteCode += new System.EventHandler(this.CheckApprovalEmailTemplateResource_ExecuteCode);
            // 
            // FindApprovalEmailTemplate
            // 
            this.FindApprovalEmailTemplate.Attributes = null;
            this.FindApprovalEmailTemplate.ExcludeWorkflowTarget = false;
            this.FindApprovalEmailTemplate.FoundIds = null;
            this.FindApprovalEmailTemplate.FoundResources = null;
            this.FindApprovalEmailTemplate.Name = "FindApprovalEmailTemplate";
            this.FindApprovalEmailTemplate.Value = null;
            activitybind5.Name = "RequestApproval";
            activitybind5.Path = "ApprovalEmailTemplate";
            this.FindApprovalEmailTemplate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // ResolveEscalation
            // 
            this.ResolveEscalation.Name = "ResolveEscalation";
            this.ResolveEscalation.ExecuteCode += new System.EventHandler(this.ResolveEscalation_ExecuteCode);
            // 
            // CheckEscalationResources
            // 
            this.CheckEscalationResources.Name = "CheckEscalationResources";
            this.CheckEscalationResources.ExecuteCode += new System.EventHandler(this.CheckEscalationResources_ExecuteCode);
            // 
            // FindEscalation
            // 
            this.FindEscalation.Attributes = null;
            this.FindEscalation.ExcludeWorkflowTarget = false;
            this.FindEscalation.FoundIds = null;
            this.FindEscalation.FoundResources = null;
            this.FindEscalation.Name = "FindEscalation";
            this.FindEscalation.Value = null;
            activitybind6.Name = "RequestApproval";
            activitybind6.Path = "Escalation";
            this.FindEscalation.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // ResolveApprovers
            // 
            this.ResolveApprovers.Name = "ResolveApprovers";
            this.ResolveApprovers.ExecuteCode += new System.EventHandler(this.ResolveApprovers_ExecuteCode);
            // 
            // CheckApproverResources
            // 
            this.CheckApproverResources.Name = "CheckApproverResources";
            this.CheckApproverResources.ExecuteCode += new System.EventHandler(this.CheckApproverResources_ExecuteCode);
            // 
            // FindApprovers
            // 
            this.FindApprovers.Attributes = null;
            this.FindApprovers.ExcludeWorkflowTarget = false;
            this.FindApprovers.FoundIds = null;
            this.FindApprovers.FoundResources = null;
            this.FindApprovers.Name = "FindApprovers";
            this.FindApprovers.Value = null;
            activitybind7.Name = "RequestApproval";
            activitybind7.Path = "Approvers";
            this.FindApprovers.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // ApprovalTimeoutEmailTemplateIsExpression
            // 
            this.ApprovalTimeoutEmailTemplateIsExpression.Activities.Add(this.ResolveApprovalTimeoutEmailTemplate);
            this.ApprovalTimeoutEmailTemplateIsExpression.Name = "ApprovalTimeoutEmailTemplateIsExpression";
            // 
            // ApprovalTimeoutEmailTemplateIsXPath
            // 
            this.ApprovalTimeoutEmailTemplateIsXPath.Activities.Add(this.FindApprovalTimeoutEmailTemplate);
            this.ApprovalTimeoutEmailTemplateIsXPath.Activities.Add(this.CheckApprovalTimeoutEmailTemplateResource);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ApprovalTimeoutEmailTemplateIsXPath_Condition);
            this.ApprovalTimeoutEmailTemplateIsXPath.Condition = codecondition1;
            this.ApprovalTimeoutEmailTemplateIsXPath.Name = "ApprovalTimeoutEmailTemplateIsXPath";
            // 
            // ApprovalDeniedEmailTemplateIsExpression
            // 
            this.ApprovalDeniedEmailTemplateIsExpression.Activities.Add(this.ResolveApprovalDeniedEmailTemplate);
            this.ApprovalDeniedEmailTemplateIsExpression.Name = "ApprovalDeniedEmailTemplateIsExpression";
            // 
            // ApprovalDeniedEmailTemplateIsXPath
            // 
            this.ApprovalDeniedEmailTemplateIsXPath.Activities.Add(this.FindApprovalDeniedEmailTemplate);
            this.ApprovalDeniedEmailTemplateIsXPath.Activities.Add(this.CheckApprovalDeniedEmailTemplateResource);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ApprovalDeniedEmailTemplateIsXPath_Condition);
            this.ApprovalDeniedEmailTemplateIsXPath.Condition = codecondition2;
            this.ApprovalDeniedEmailTemplateIsXPath.Name = "ApprovalDeniedEmailTemplateIsXPath";
            // 
            // ApprovalCompleteEmailTemplateIsExpression
            // 
            this.ApprovalCompleteEmailTemplateIsExpression.Activities.Add(this.ResolveApprovalCompleteEmailTemplate);
            this.ApprovalCompleteEmailTemplateIsExpression.Name = "ApprovalCompleteEmailTemplateIsExpression";
            // 
            // ApprovalCompleteEmailTemplateIsXPath
            // 
            this.ApprovalCompleteEmailTemplateIsXPath.Activities.Add(this.FindApprovalCompleteEmailTemplate);
            this.ApprovalCompleteEmailTemplateIsXPath.Activities.Add(this.CheckApprovalCompleteEmailTemplateResource);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ApprovalCompleteEmailTemplateIsXPath_Condition);
            this.ApprovalCompleteEmailTemplateIsXPath.Condition = codecondition3;
            this.ApprovalCompleteEmailTemplateIsXPath.Name = "ApprovalCompleteEmailTemplateIsXPath";
            // 
            // EscalationEmailTemplateIsExpression
            // 
            this.EscalationEmailTemplateIsExpression.Activities.Add(this.ResolveEscalationEmailTemplate);
            this.EscalationEmailTemplateIsExpression.Name = "EscalationEmailTemplateIsExpression";
            // 
            // EscalationEmailTemplateIsXPath
            // 
            this.EscalationEmailTemplateIsXPath.Activities.Add(this.FindEscalationEmailTemplate);
            this.EscalationEmailTemplateIsXPath.Activities.Add(this.CheckEscalationEmailTemplateResource);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.EscalationEmailTemplateIsXPath_Condition);
            this.EscalationEmailTemplateIsXPath.Condition = codecondition4;
            this.EscalationEmailTemplateIsXPath.Name = "EscalationEmailTemplateIsXPath";
            // 
            // ApprovalEmailTemplateIsExpression
            // 
            this.ApprovalEmailTemplateIsExpression.Activities.Add(this.ResolveApprovalEmailTemplate);
            this.ApprovalEmailTemplateIsExpression.Name = "ApprovalEmailTemplateIsExpression";
            // 
            // ApprovalEmailTemplateIsXPath
            // 
            this.ApprovalEmailTemplateIsXPath.Activities.Add(this.FindApprovalEmailTemplate);
            this.ApprovalEmailTemplateIsXPath.Activities.Add(this.CheckApprovalEmailTemplateResource);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ApprovalEmailTemplateIsXPath_Condition);
            this.ApprovalEmailTemplateIsXPath.Condition = codecondition5;
            this.ApprovalEmailTemplateIsXPath.Name = "ApprovalEmailTemplateIsXPath";
            // 
            // EscalationIsExpression
            // 
            this.EscalationIsExpression.Activities.Add(this.ResolveEscalation);
            this.EscalationIsExpression.Name = "EscalationIsExpression";
            // 
            // EscalationIsXPath
            // 
            this.EscalationIsXPath.Activities.Add(this.FindEscalation);
            this.EscalationIsXPath.Activities.Add(this.CheckEscalationResources);
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.EscalationIsXPath_Condition);
            this.EscalationIsXPath.Condition = codecondition6;
            this.EscalationIsXPath.Name = "EscalationIsXPath";
            // 
            // ApproversIsExpression
            // 
            this.ApproversIsExpression.Activities.Add(this.ResolveApprovers);
            this.ApproversIsExpression.Name = "ApproversIsExpression";
            // 
            // ApproversIsXPath
            // 
            this.ApproversIsXPath.Activities.Add(this.FindApprovers);
            this.ApproversIsXPath.Activities.Add(this.CheckApproverResources);
            codecondition7.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ApproversIsXPath_Condition);
            this.ApproversIsXPath.Condition = codecondition7;
            this.ApproversIsXPath.Name = "ApproversIsXPath";
            // 
            // CreateApproval
            // 
            activitybind8.Name = "RequestApproval";
            activitybind8.Path = "ApprovalCompleteEmailTemplateGuid";
            activitybind9.Name = "RequestApproval";
            activitybind9.Path = "ApprovalDeniedEmailTemplateGuid";
            activitybind10.Name = "RequestApproval";
            activitybind10.Path = "ApprovalEmailTemplateGuid";
            this.CreateApproval.ApprovalObject = null;
            this.CreateApproval.ApprovalObjectId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.CreateApproval.ApprovalResponseCreateParameters = null;
            activitybind11.Name = "RequestApproval";
            activitybind11.Path = "ApprovalTimeoutEmailTemplateGuid";
            this.CreateApproval.ApproverId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind12.Name = "RequestApproval";
            activitybind12.Path = "Approvers";
            this.CreateApproval.AutomatedResponseObjectId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.CreateApproval.CurrentApprovalResponse = null;
            this.CreateApproval.CurrentApprovalResponseActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind13.Name = "RequestApproval";
            activitybind13.Path = "DurationTimeSpan";
            activitybind14.Name = "RequestApproval";
            activitybind14.Path = "Escalation";
            activitybind15.Name = "RequestApproval";
            activitybind15.Path = "EscalationEmailTemplateGuid";
            this.CreateApproval.IsApproved = null;
            this.CreateApproval.Name = "CreateApproval";
            this.CreateApproval.ReceivedApprovals = 0;
            this.CreateApproval.RejectedReason = null;
            this.CreateApproval.Request = null;
            this.CreateApproval.RequestTimedOut = false;
            activitybind16.Name = "RequestApproval";
            activitybind16.Path = "ThresholdNumber";
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.ApprovalCompleteEmailTemplateProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.ApprovalDeniedEmailTemplateProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.ApprovalEmailTemplateProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.ApprovalTimeoutEmailTemplateProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.ApproversProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.ThresholdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.EscalationEmailTemplateProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.DurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.CreateApproval.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity.EscalationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // TraceCreateApproval
            // 
            this.TraceCreateApproval.Name = "TraceCreateApproval";
            this.TraceCreateApproval.ExecuteCode += new System.EventHandler(this.TraceCreateApproval_ExecuteCode);
            // 
            // IfApprovalTimeoutEmailTemplateIsXPathOrExpression
            // 
            this.IfApprovalTimeoutEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalTimeoutEmailTemplateIsXPath);
            this.IfApprovalTimeoutEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalTimeoutEmailTemplateIsExpression);
            this.IfApprovalTimeoutEmailTemplateIsXPathOrExpression.Name = "IfApprovalTimeoutEmailTemplateIsXPathOrExpression";
            // 
            // IfApprovalDeniedEmailTemplateIsXPathOrExpression
            // 
            this.IfApprovalDeniedEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalDeniedEmailTemplateIsXPath);
            this.IfApprovalDeniedEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalDeniedEmailTemplateIsExpression);
            this.IfApprovalDeniedEmailTemplateIsXPathOrExpression.Name = "IfApprovalDeniedEmailTemplateIsXPathOrExpression";
            // 
            // IfApprovalCompleteEmailTemplateIsXPathOrExpression
            // 
            this.IfApprovalCompleteEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalCompleteEmailTemplateIsXPath);
            this.IfApprovalCompleteEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalCompleteEmailTemplateIsExpression);
            this.IfApprovalCompleteEmailTemplateIsXPathOrExpression.Name = "IfApprovalCompleteEmailTemplateIsXPathOrExpression";
            // 
            // IfEscalationEmailTemplateIsXPathOrExpression
            // 
            this.IfEscalationEmailTemplateIsXPathOrExpression.Activities.Add(this.EscalationEmailTemplateIsXPath);
            this.IfEscalationEmailTemplateIsXPathOrExpression.Activities.Add(this.EscalationEmailTemplateIsExpression);
            this.IfEscalationEmailTemplateIsXPathOrExpression.Name = "IfEscalationEmailTemplateIsXPathOrExpression";
            // 
            // IfApprovalEmailTemplateIsXPathOrExpression
            // 
            this.IfApprovalEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalEmailTemplateIsXPath);
            this.IfApprovalEmailTemplateIsXPathOrExpression.Activities.Add(this.ApprovalEmailTemplateIsExpression);
            this.IfApprovalEmailTemplateIsXPathOrExpression.Name = "IfApprovalEmailTemplateIsXPathOrExpression";
            // 
            // IfEscalationIsXPathOrExpression
            // 
            this.IfEscalationIsXPathOrExpression.Activities.Add(this.EscalationIsXPath);
            this.IfEscalationIsXPathOrExpression.Activities.Add(this.EscalationIsExpression);
            this.IfEscalationIsXPathOrExpression.Name = "IfEscalationIsXPathOrExpression";
            // 
            // IfApproversIsXPathOrExpression
            // 
            this.IfApproversIsXPathOrExpression.Activities.Add(this.ApproversIsXPath);
            this.IfApproversIsXPathOrExpression.Activities.Add(this.ApproversIsExpression);
            this.IfApproversIsXPathOrExpression.Name = "IfApproversIsXPathOrExpression";
            // 
            // ResolveDuration
            // 
            this.ResolveDuration.Name = "ResolveDuration";
            this.ResolveDuration.ExecuteCode += new System.EventHandler(this.ResolveDuration_ExecuteCode);
            // 
            // ResolveThreshold
            // 
            this.ResolveThreshold.Name = "ResolveThreshold";
            this.ResolveThreshold.ExecuteCode += new System.EventHandler(this.ResolveThreshold_ExecuteCode);
            // 
            // ConditionSatisfied
            // 
            this.ConditionSatisfied.Activities.Add(this.ResolveThreshold);
            this.ConditionSatisfied.Activities.Add(this.ResolveDuration);
            this.ConditionSatisfied.Activities.Add(this.IfApproversIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfEscalationIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfApprovalEmailTemplateIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfEscalationEmailTemplateIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfApprovalCompleteEmailTemplateIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfApprovalDeniedEmailTemplateIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.IfApprovalTimeoutEmailTemplateIsXPathOrExpression);
            this.ConditionSatisfied.Activities.Add(this.TraceCreateApproval);
            this.ConditionSatisfied.Activities.Add(this.CreateApproval);
            codecondition8.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActivityExecutionConditionSatisfied_Condition);
            this.ConditionSatisfied.Condition = codecondition8;
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
            activitybind17.Name = "RequestApproval";
            activitybind17.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            this.Resolve.QueryResults = null;
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            // 
            // ParseExpressions
            // 
            this.ParseExpressions.Name = "ParseExpressions";
            this.ParseExpressions.ExecuteCode += new System.EventHandler(this.ParseExpressions_ExecuteCode);
            // 
            // RequestApproval
            // 
            this.Activities.Add(this.ParseExpressions);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.IfActivityExecutionConditionSatisfied);
            this.Name = "RequestApproval";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity ResolveDuration;
        private CodeActivity ResolveThreshold;
        private CodeActivity ResolveApprovalTimeoutEmailTemplate;
        private CodeActivity CheckApprovalTimeoutEmailTemplateResource;
        private ComponentActivities.FindResources FindApprovalTimeoutEmailTemplate;
        private IfElseBranchActivity ApprovalTimeoutEmailTemplateIsExpression;
        private IfElseBranchActivity ApprovalTimeoutEmailTemplateIsXPath;
        private IfElseActivity IfApprovalTimeoutEmailTemplateIsXPathOrExpression;
        private CodeActivity TraceCreateApproval;
        private CodeActivity ResolveApprovalCompleteEmailTemplate;
        private CodeActivity CheckApprovalCompleteEmailTemplateResource;
        private ComponentActivities.FindResources FindApprovalCompleteEmailTemplate;
        private IfElseBranchActivity ApprovalCompleteEmailTemplateIsExpression;
        private IfElseBranchActivity ApprovalCompleteEmailTemplateIsXPath;
        private IfElseActivity IfApprovalCompleteEmailTemplateIsXPathOrExpression;
        private CodeActivity ResolveApprovalDeniedEmailTemplate;
        private CodeActivity CheckApprovalDeniedEmailTemplateResource;
        private ComponentActivities.FindResources FindApprovalDeniedEmailTemplate;
        private IfElseBranchActivity ApprovalDeniedEmailTemplateIsExpression;
        private IfElseBranchActivity ApprovalDeniedEmailTemplateIsXPath;
        private IfElseActivity IfApprovalDeniedEmailTemplateIsXPathOrExpression;
        private CodeActivity ResolveEscalationEmailTemplate;
        private CodeActivity CheckEscalationEmailTemplateResource;
        private ComponentActivities.FindResources FindEscalationEmailTemplate;
        private IfElseBranchActivity EscalationEmailTemplateIsExpression;
        private IfElseBranchActivity EscalationEmailTemplateIsXPath;
        private IfElseActivity IfEscalationEmailTemplateIsXPathOrExpression;
        private CodeActivity ResolveApprovalEmailTemplate;
        private CodeActivity CheckApprovalEmailTemplateResource;
        private ComponentActivities.FindResources FindApprovalEmailTemplate;
        private IfElseBranchActivity ApprovalEmailTemplateIsExpression;
        private IfElseBranchActivity ApprovalEmailTemplateIsXPath;
        private IfElseActivity IfApprovalEmailTemplateIsXPathOrExpression;
        private CodeActivity ResolveEscalation;
        private CodeActivity CheckEscalationResources;
        private ComponentActivities.FindResources FindEscalation;
        private IfElseBranchActivity EscalationIsExpression;
        private IfElseBranchActivity EscalationIsXPath;
        private IfElseActivity IfEscalationIsXPathOrExpression;
        private CodeActivity ResolveApprovers;
        private CodeActivity ParseExpressions;
        private ComponentActivities.ResolveLookups Resolve;
        private IfElseBranchActivity ConditionSatisfied;
        private IfElseActivity IfActivityExecutionConditionSatisfied;
        private IfElseBranchActivity ApproversIsExpression;
        private IfElseBranchActivity ApproversIsXPath;
        private IfElseActivity IfApproversIsXPathOrExpression;
        private ComponentActivities.FindResources FindApprovers;
        private CodeActivity CheckApproverResources;
        private Microsoft.ResourceManagement.Workflow.Activities.ApprovalActivity CreateApproval;
    }
}
