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
    public partial class VerifyRequest
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
            System.Collections.Generic.List<System.Guid> list_11 = new System.Collections.Generic.List<System.Guid>();
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_12 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            this.DenyRequest = new System.Workflow.Activities.CodeActivity();
            this.ResolveMessage = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString();
            this.EnforceConditions = new System.Workflow.Activities.CodeActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.ParseConditions = new System.Workflow.Activities.CodeActivity();
            this.CheckRequest = new System.Workflow.Activities.CodeActivity();
            this.FindConflictRequest = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindRequestConflict();
            this.CheckResource = new System.Workflow.Activities.CodeActivity();
            this.FindConflict = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.DenialPending = new System.Workflow.Activities.IfElseBranchActivity();
            this.Unique = new System.Workflow.Activities.IfElseBranchActivity();
            this.CheckRequestConflict = new System.Workflow.Activities.IfElseBranchActivity();
            this.CheckConflict = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfDenialPending = new System.Workflow.Activities.IfElseActivity();
            this.IfUnique = new System.Workflow.Activities.IfElseActivity();
            this.IfCheckRequestConflict = new System.Workflow.Activities.IfElseActivity();
            this.IfCheckConflict = new System.Workflow.Activities.IfElseActivity();
            this.ActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseActivity();
            this.ResolveExecutionCondition = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // DenyRequest
            // 
            this.DenyRequest.Name = "DenyRequest";
            this.DenyRequest.ExecuteCode += new System.EventHandler(this.DenyRequest_ExecuteCode);
            // 
            // ResolveMessage
            // 
            activitybind1.Name = "VerifyRequest";
            activitybind1.Path = "ConflictingRequest";
            this.ResolveMessage.Name = "ResolveMessage";
            this.ResolveMessage.QueryResults = null;
            this.ResolveMessage.Resolved = "";
            activitybind2.Name = "VerifyRequest";
            activitybind2.Path = "DenialMessage";
            this.ResolveMessage.Value = null;
            this.ResolveMessage.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString.ComparedRequestIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.ResolveMessage.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString.StringForResolutionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // EnforceConditions
            // 
            this.EnforceConditions.Name = "EnforceConditions";
            this.EnforceConditions.ExecuteCode += new System.EventHandler(this.EnforceConditions_ExecuteCode);
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind3.Name = "VerifyRequest";
            activitybind3.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            this.Resolve.QueryResults = null;
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // ParseConditions
            // 
            this.ParseConditions.Name = "ParseConditions";
            this.ParseConditions.ExecuteCode += new System.EventHandler(this.ParseConditions_ExecuteCode);
            // 
            // CheckRequest
            // 
            this.CheckRequest.Name = "CheckRequest";
            this.CheckRequest.ExecuteCode += new System.EventHandler(this.CheckRequest_ExecuteCode);
            // 
            // FindConflictRequest
            // 
            activitybind4.Name = "VerifyRequest";
            activitybind4.Path = "RequestConflictAdvancedFilter";
            this.FindConflictRequest.ConflictFound = false;
            this.FindConflictRequest.ConflictingRequest = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind5.Name = "VerifyRequest";
            activitybind5.Path = "RequestConflictMatchCondition";
            this.FindConflictRequest.Name = "FindConflictRequest";
            this.FindConflictRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindRequestConflict.AdvancedRequestFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.FindConflictRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindRequestConflict.MatchConditionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // CheckResource
            // 
            this.CheckResource.Name = "CheckResource";
            this.CheckResource.ExecuteCode += new System.EventHandler(this.CheckResource_ExecuteCode);
            // 
            // FindConflict
            // 
            this.FindConflict.Attributes = null;
            this.FindConflict.ExcludeWorkflowTarget = true;
            this.FindConflict.FoundIds = list_11;
            this.FindConflict.FoundResources = list_12;
            this.FindConflict.Name = "FindConflict";
            this.FindConflict.Value = null;
            activitybind6.Name = "VerifyRequest";
            activitybind6.Path = "ConflictFilter";
            this.FindConflict.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // DenialPending
            // 
            this.DenialPending.Activities.Add(this.ResolveMessage);
            this.DenialPending.Activities.Add(this.DenyRequest);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.DenialPending_Condition);
            this.DenialPending.Condition = codecondition1;
            this.DenialPending.Name = "DenialPending";
            // 
            // Unique
            // 
            this.Unique.Activities.Add(this.ParseConditions);
            this.Unique.Activities.Add(this.Resolve);
            this.Unique.Activities.Add(this.EnforceConditions);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.Unique_Condition);
            this.Unique.Condition = codecondition2;
            this.Unique.Name = "Unique";
            // 
            // CheckRequestConflict
            // 
            this.CheckRequestConflict.Activities.Add(this.FindConflictRequest);
            this.CheckRequestConflict.Activities.Add(this.CheckRequest);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.CheckRequestConflict_Condition);
            this.CheckRequestConflict.Condition = codecondition3;
            this.CheckRequestConflict.Name = "CheckRequestConflict";
            // 
            // CheckConflict
            // 
            this.CheckConflict.Activities.Add(this.FindConflict);
            this.CheckConflict.Activities.Add(this.CheckResource);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.CheckConflict_Condition);
            this.CheckConflict.Condition = codecondition4;
            this.CheckConflict.Name = "CheckConflict";
            // 
            // IfDenialPending
            // 
            this.IfDenialPending.Activities.Add(this.DenialPending);
            this.IfDenialPending.Name = "IfDenialPending";
            // 
            // IfUnique
            // 
            this.IfUnique.Activities.Add(this.Unique);
            this.IfUnique.Name = "IfUnique";
            // 
            // IfCheckRequestConflict
            // 
            this.IfCheckRequestConflict.Activities.Add(this.CheckRequestConflict);
            this.IfCheckRequestConflict.Name = "IfCheckRequestConflict";
            // 
            // IfCheckConflict
            // 
            this.IfCheckConflict.Activities.Add(this.CheckConflict);
            this.IfCheckConflict.Name = "IfCheckConflict";
            // 
            // ActivityExecutionConditionSatisfied
            // 
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.IfCheckConflict);
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.IfCheckRequestConflict);
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.IfUnique);
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.IfDenialPending);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActivityExecutionConditionSatisfied_Condition);
            this.ActivityExecutionConditionSatisfied.Condition = codecondition5;
            this.ActivityExecutionConditionSatisfied.Name = "ActivityExecutionConditionSatisfied";
            // 
            // IfActivityExecutionConditionSatisfied
            // 
            this.IfActivityExecutionConditionSatisfied.Activities.Add(this.ActivityExecutionConditionSatisfied);
            this.IfActivityExecutionConditionSatisfied.Name = "IfActivityExecutionConditionSatisfied";
            // 
            // ResolveExecutionCondition
            // 
            this.ResolveExecutionCondition.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind7.Name = "VerifyRequest";
            activitybind7.Path = "ActivityExpressionEvaluator.LookupCache";
            this.ResolveExecutionCondition.Name = "ResolveExecutionCondition";
            this.ResolveExecutionCondition.QueryResults = null;
            this.ResolveExecutionCondition.Value = null;
            this.ResolveExecutionCondition.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // VerifyRequest
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.ResolveExecutionCondition);
            this.Activities.Add(this.IfActivityExecutionConditionSatisfied);
            this.Name = "VerifyRequest";
            this.CanModifyActivities = false;

        }

        #endregion

        private ComponentActivities.ResolveLookups ResolveExecutionCondition;

        private CodeActivity Prepare;

        private IfElseBranchActivity ActivityExecutionConditionSatisfied;

        private IfElseActivity IfActivityExecutionConditionSatisfied;

        private ComponentActivities.ResolveLookups Resolve;

        private ComponentActivities.ResolveLookupString ResolveMessage;

        private ComponentActivities.FindRequestConflict FindConflictRequest;

        private ComponentActivities.FindResources FindConflict;

        private CodeActivity CheckRequest;

        private IfElseBranchActivity CheckRequestConflict;

        private IfElseActivity IfCheckRequestConflict;

        private CodeActivity CheckResource;

        private IfElseBranchActivity Unique;

        private IfElseActivity IfUnique;

        private IfElseBranchActivity CheckConflict;

        private IfElseActivity IfCheckConflict;

        private IfElseBranchActivity DenialPending;

        private IfElseActivity IfDenialPending;

        private CodeActivity DenyRequest;

        private CodeActivity EnforceConditions;

        private CodeActivity ParseConditions;






















































    }
}
