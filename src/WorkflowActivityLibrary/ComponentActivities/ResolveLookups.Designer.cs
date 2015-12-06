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

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    internal partial class ResolveLookups
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            this.ReadResponse = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.ReadEnumeratedApproval = new System.Workflow.Activities.CodeActivity();
            this.Read = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.ResolveParentRequest = new System.Workflow.Activities.CodeActivity();
            this.ReadParentRequest = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.ResolveRequestParameter = new System.Workflow.Activities.CodeActivity();
            this.ResolveStandard2 = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.ResolveStandard = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.ResolveApprovers = new System.Workflow.Activities.CodeActivity();
            this.ForEachResponse = new System.Workflow.Activities.ReplicatorActivity();
            this.FindApprovals = new Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity();
            this.ResolveComparedRequest = new System.Workflow.Activities.CodeActivity();
            this.ReadComparedRequest = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.ForEachResource = new System.Workflow.Activities.ReplicatorActivity();
            this.CompositeRequest = new System.Workflow.Activities.IfElseBranchActivity();
            this.RequestParameter = new System.Workflow.Activities.IfElseBranchActivity();
            this.Approvers = new System.Workflow.Activities.IfElseBranchActivity();
            this.ComparedRequest = new System.Workflow.Activities.IfElseBranchActivity();
            this.PerformRead = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfCompositeRequest = new System.Workflow.Activities.IfElseActivity();
            this.IfRequestParameter = new System.Workflow.Activities.IfElseActivity();
            this.IfApprovers = new System.Workflow.Activities.IfElseActivity();
            this.IfComparedRequest = new System.Workflow.Activities.IfElseActivity();
            this.IfPerformRead = new System.Workflow.Activities.IfElseActivity();
            this.CompositeRequestResolution = new System.Workflow.Activities.SequenceActivity();
            this.RequestParameterResolution = new System.Workflow.Activities.SequenceActivity();
            this.ApproversResolution = new System.Workflow.Activities.SequenceActivity();
            this.ComparedRequestResolution = new System.Workflow.Activities.SequenceActivity();
            this.Resolve = new System.Workflow.Activities.CodeActivity();
            this.ForEachRead = new System.Workflow.Activities.ReplicatorActivity();
            this.SpecialParameterResolution = new System.Workflow.Activities.ParallelActivity();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            this.Request = new Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity();
            // 
            // ReadResponse
            // 
            activitybind1.Name = "ResolveLookups";
            activitybind1.Path = "ServiceActor";
            this.ReadResponse.Name = "ReadResponse";
            this.ReadResponse.Resource = null;
            activitybind2.Name = "ResolveLookups";
            activitybind2.Path = "ApprovalResponse";
            this.ReadResponse.SelectionAttributes = new string[] {
        "ObjectID",
        "Creator",
        "CreatedTime",
        "Decision"};
            this.ReadResponse.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.ReadResponse.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ReadEnumeratedApproval
            // 
            this.ReadEnumeratedApproval.Name = "ReadEnumeratedApproval";
            this.ReadEnumeratedApproval.ExecuteCode += new System.EventHandler(this.ReadEnumeratedApproval_ExecuteCode);
            // 
            // Read
            // 
            activitybind3.Name = "ResolveLookups";
            activitybind3.Path = "ServiceActor";
            this.Read.Name = "Read";
            this.Read.Resource = null;
            activitybind4.Name = "ResolveLookups";
            activitybind4.Path = "ReadResource";
            activitybind5.Name = "ResolveLookups";
            activitybind5.Path = "ReadAttributes";
            this.Read.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.Read.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.Read.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.SelectionAttributesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // ResolveParentRequest
            // 
            this.ResolveParentRequest.Name = "ResolveParentRequest";
            this.ResolveParentRequest.ExecuteCode += new System.EventHandler(this.ResolveParentRequest_ExecuteCode);
            // 
            // ReadParentRequest
            // 
            activitybind6.Name = "ResolveLookups";
            activitybind6.Path = "ServiceActor";
            this.ReadParentRequest.Name = "ReadParentRequest";
            this.ReadParentRequest.Resource = null;
            activitybind7.Name = "ResolveLookups";
            activitybind7.Path = "ParentRequest";
            this.ReadParentRequest.SelectionAttributes = new string[] {
        "ObjectID",
        "Operation",
        "RequestParameter"};
            this.ReadParentRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.ReadParentRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // ResolveRequestParameter
            // 
            this.ResolveRequestParameter.Name = "ResolveRequestParameter";
            this.ResolveRequestParameter.ExecuteCode += new System.EventHandler(this.ResolveRequestParameter_ExecuteCode);
            // 
            // ResolveStandard2
            // 
            this.ResolveStandard2.GrammarExpression = "[//RequestParameter/AllChangesActionTable]";
            this.ResolveStandard2.Name = "ResolveStandard2";
            this.ResolveStandard2.ResolvedExpression = null;
            this.ResolveStandard2.WorkflowDictionaryKey = null;
            // 
            // ResolveStandard
            // 
            this.ResolveStandard.GrammarExpression = "[//RequestParameter/AllChangesAuthorizationTable]";
            this.ResolveStandard.Name = "ResolveStandard";
            this.ResolveStandard.ResolvedExpression = null;
            this.ResolveStandard.WorkflowDictionaryKey = null;
            // 
            // ResolveApprovers
            // 
            this.ResolveApprovers.Name = "ResolveApprovers";
            this.ResolveApprovers.ExecuteCode += new System.EventHandler(this.ResolveApprovers_ExecuteCode);
            activitybind8.Name = "ResolveLookups";
            activitybind8.Path = "ApprovalResponses";
            // 
            // ForEachResponse
            // 
            this.ForEachResponse.Activities.Add(this.ReadResponse);
            this.ForEachResponse.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachResponse.Name = "ForEachResponse";
            this.ForEachResponse.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachResponse_ChildInitialized);
            this.ForEachResponse.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachResponse_ChildCompleted);
            this.ForEachResponse.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // FindApprovals
            // 
            this.FindApprovals.Activities.Add(this.ReadEnumeratedApproval);
            activitybind9.Name = "ResolveLookups";
            activitybind9.Path = "ServiceActor";
            this.FindApprovals.Name = "FindApprovals";
            this.FindApprovals.PageSize = 100;
            this.FindApprovals.Selection = new string[] {
        "ObjectID",
        "ApprovalResponse"};
            this.FindApprovals.SortingAttributes = null;
            this.FindApprovals.TotalResultsCount = 0;
            this.FindApprovals.XPathFilter = null;
            this.FindApprovals.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // ResolveComparedRequest
            // 
            this.ResolveComparedRequest.Name = "ResolveComparedRequest";
            this.ResolveComparedRequest.ExecuteCode += new System.EventHandler(this.ResolveComparedRequest_ExecuteCode);
            // 
            // ReadComparedRequest
            // 
            activitybind10.Name = "ResolveLookups";
            activitybind10.Path = "ServiceActor";
            this.ReadComparedRequest.Name = "ReadComparedRequest";
            this.ReadComparedRequest.Resource = null;
            activitybind11.Name = "ResolveLookups";
            activitybind11.Path = "ComparedRequestId";
            this.ReadComparedRequest.SelectionAttributes = new string[] {
        "ObjectID",
        "Operation",
        "RequestParameter"};
            this.ReadComparedRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.ReadComparedRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            activitybind12.Name = "ResolveLookups";
            activitybind12.Path = "ReadResources";
            // 
            // ForEachResource
            // 
            this.ForEachResource.Activities.Add(this.Read);
            this.ForEachResource.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachResource.Name = "ForEachResource";
            this.ForEachResource.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachResource_ChildInitialized);
            this.ForEachResource.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachResource_ChildCompleted);
            this.ForEachResource.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // CompositeRequest
            // 
            this.CompositeRequest.Activities.Add(this.ReadParentRequest);
            this.CompositeRequest.Activities.Add(this.ResolveParentRequest);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.CompositeRequest_Condition);
            this.CompositeRequest.Condition = codecondition1;
            this.CompositeRequest.Name = "CompositeRequest";
            // 
            // RequestParameter
            // 
            this.RequestParameter.Activities.Add(this.ResolveStandard);
            this.RequestParameter.Activities.Add(this.ResolveStandard2);
            this.RequestParameter.Activities.Add(this.ResolveRequestParameter);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.RequestParameter_Condition);
            this.RequestParameter.Condition = codecondition2;
            this.RequestParameter.Name = "RequestParameter";
            // 
            // Approvers
            // 
            this.Approvers.Activities.Add(this.FindApprovals);
            this.Approvers.Activities.Add(this.ForEachResponse);
            this.Approvers.Activities.Add(this.ResolveApprovers);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.Approvers_Condition);
            this.Approvers.Condition = codecondition3;
            this.Approvers.Name = "Approvers";
            // 
            // ComparedRequest
            // 
            this.ComparedRequest.Activities.Add(this.ReadComparedRequest);
            this.ComparedRequest.Activities.Add(this.ResolveComparedRequest);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ComparedRequest_Condition);
            this.ComparedRequest.Condition = codecondition4;
            this.ComparedRequest.Name = "ComparedRequest";
            // 
            // PerformRead
            // 
            this.PerformRead.Activities.Add(this.ForEachResource);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.PerformRead_Condition);
            this.PerformRead.Condition = codecondition5;
            this.PerformRead.Name = "PerformRead";
            // 
            // IfCompositeRequest
            // 
            this.IfCompositeRequest.Activities.Add(this.CompositeRequest);
            this.IfCompositeRequest.Name = "IfCompositeRequest";
            // 
            // IfRequestParameter
            // 
            this.IfRequestParameter.Activities.Add(this.RequestParameter);
            this.IfRequestParameter.Name = "IfRequestParameter";
            // 
            // IfApprovers
            // 
            this.IfApprovers.Activities.Add(this.Approvers);
            this.IfApprovers.Name = "IfApprovers";
            // 
            // IfComparedRequest
            // 
            this.IfComparedRequest.Activities.Add(this.ComparedRequest);
            this.IfComparedRequest.Name = "IfComparedRequest";
            // 
            // IfPerformRead
            // 
            this.IfPerformRead.Activities.Add(this.PerformRead);
            this.IfPerformRead.Name = "IfPerformRead";
            // 
            // CompositeRequestResolution
            // 
            this.CompositeRequestResolution.Activities.Add(this.IfCompositeRequest);
            this.CompositeRequestResolution.Name = "CompositeRequestResolution";
            // 
            // RequestParameterResolution
            // 
            this.RequestParameterResolution.Activities.Add(this.IfRequestParameter);
            this.RequestParameterResolution.Name = "RequestParameterResolution";
            // 
            // ApproversResolution
            // 
            this.ApproversResolution.Activities.Add(this.IfApprovers);
            this.ApproversResolution.Name = "ApproversResolution";
            // 
            // ComparedRequestResolution
            // 
            this.ComparedRequestResolution.Activities.Add(this.IfComparedRequest);
            this.ComparedRequestResolution.Name = "ComparedRequestResolution";
            // 
            // Resolve
            // 
            this.Resolve.Name = "Resolve";
            this.Resolve.ExecuteCode += new System.EventHandler(this.Resolve_ExecuteCode);
            // 
            // ForEachRead
            // 
            this.ForEachRead.Activities.Add(this.IfPerformRead);
            this.ForEachRead.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachRead.Name = "ForEachRead";
            this.ForEachRead.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachRead_ChildInitialized);
            // 
            // SpecialParameterResolution
            // 
            this.SpecialParameterResolution.Activities.Add(this.ComparedRequestResolution);
            this.SpecialParameterResolution.Activities.Add(this.ApproversResolution);
            this.SpecialParameterResolution.Activities.Add(this.RequestParameterResolution);
            this.SpecialParameterResolution.Activities.Add(this.CompositeRequestResolution);
            this.SpecialParameterResolution.Name = "SpecialParameterResolution";
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // Request
            // 
            this.Request.CurrentRequest = null;
            this.Request.Name = "Request";
            // 
            // ResolveLookups
            // 
            this.Activities.Add(this.Request);
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.SpecialParameterResolution);
            this.Activities.Add(this.ForEachRead);
            this.Activities.Add(this.Resolve);
            this.Name = "ResolveLookups";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveStandard2;

        private CodeActivity ResolveParentRequest;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadParentRequest;

        private SequenceActivity CompositeRequestResolution;

        private IfElseBranchActivity CompositeRequest;

        private IfElseActivity IfCompositeRequest;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadResponse;

        private CodeActivity ReadEnumeratedApproval;

        private CodeActivity ResolveApprovers;

        private ReplicatorActivity ForEachResponse;

        private Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity FindApprovals;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadComparedRequest;

        private CodeActivity ResolveComparedRequest;

        private CodeActivity ResolveRequestParameter;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveStandard;

        private Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity Request;

        private IfElseBranchActivity RequestParameter;

        private IfElseBranchActivity Approvers;

        private IfElseBranchActivity ComparedRequest;

        private IfElseActivity IfRequestParameter;

        private IfElseActivity IfApprovers;

        private IfElseActivity IfComparedRequest;

        private SequenceActivity RequestParameterResolution;

        private SequenceActivity ApproversResolution;

        private SequenceActivity ComparedRequestResolution;

        private CodeActivity Resolve;

        private ParallelActivity SpecialParameterResolution;

        private ReplicatorActivity ForEachRead;

        private IfElseBranchActivity PerformRead;

        private IfElseActivity IfPerformRead;

        private ReplicatorActivity ForEachResource;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity Read;

        private CodeActivity Prepare;






































































    }
}
