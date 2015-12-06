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
    internal partial class UpdateLookups
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            this.Update = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.AsyncUpdate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousUpdateResource();
            this.Standard = new System.Workflow.Activities.IfElseBranchActivity();
            this.Authorization = new System.Workflow.Activities.IfElseBranchActivity();
            this.SwitchSubmissionType = new System.Workflow.Activities.IfElseActivity();
            this.Read = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.ForEachRequest = new System.Workflow.Activities.ReplicatorActivity();
            this.PrepareUpdate = new System.Workflow.Activities.CodeActivity();
            this.ForEachPending = new System.Workflow.Activities.ReplicatorActivity();
            this.BuildRequests = new System.Workflow.Activities.CodeActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // Update
            // 
            activitybind1.Name = "UpdateLookups";
            activitybind1.Path = "Actor";
            this.Update.ApplyAuthorizationPolicy = false;
            this.Update.Name = "Update";
            activitybind2.Name = "UpdateLookups";
            activitybind2.Path = "CurrentTargetResource";
            activitybind3.Name = "UpdateLookups";
            activitybind3.Path = "CurrentRequestParameters";
            this.Update.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Update.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.Update.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity.UpdateParametersProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // AsyncUpdate
            // 
            activitybind4.Name = "UpdateLookups";
            activitybind4.Path = "Actor";
            this.AsyncUpdate.ApplyAuthorizationPolicy = true;
            this.AsyncUpdate.Name = "AsyncUpdate";
            activitybind5.Name = "UpdateLookups";
            activitybind5.Path = "CurrentTargetResource";
            activitybind6.Name = "UpdateLookups";
            activitybind6.Path = "CurrentRequestParameters";
            this.AsyncUpdate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousUpdateResource.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.AsyncUpdate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousUpdateResource.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.AsyncUpdate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousUpdateResource.UpdateParametersProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // Standard
            // 
            this.Standard.Activities.Add(this.Update);
            this.Standard.Name = "Standard";
            // 
            // Authorization
            // 
            this.Authorization.Activities.Add(this.AsyncUpdate);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.Authorization_Condition);
            this.Authorization.Condition = codecondition1;
            this.Authorization.Name = "Authorization";
            // 
            // SwitchSubmissionType
            // 
            this.SwitchSubmissionType.Activities.Add(this.Authorization);
            this.SwitchSubmissionType.Activities.Add(this.Standard);
            this.SwitchSubmissionType.Name = "SwitchSubmissionType";
            // 
            // Read
            // 
            activitybind7.Name = "UpdateLookups";
            activitybind7.Path = "ServiceActor";
            this.Read.Name = "Read";
            this.Read.Resource = null;
            this.Read.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.Read.SelectionAttributes = null;
            this.Read.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // ForEachRequest
            // 
            this.ForEachRequest.Activities.Add(this.SwitchSubmissionType);
            this.ForEachRequest.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachRequest.Name = "ForEachRequest";
            this.ForEachRequest.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachRequest_ChildInitialized);
            // 
            // PrepareUpdate
            // 
            this.PrepareUpdate.Name = "PrepareUpdate";
            this.PrepareUpdate.ExecuteCode += new System.EventHandler(this.PrepareUpdate_ExecuteCode);
            // 
            // ForEachPending
            // 
            this.ForEachPending.Activities.Add(this.Read);
            this.ForEachPending.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachPending.Name = "ForEachPending";
            this.ForEachPending.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachPending_ChildInitialized);
            this.ForEachPending.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachPending_ChildCompleted);
            // 
            // BuildRequests
            // 
            this.BuildRequests.Name = "BuildRequests";
            this.BuildRequests.ExecuteCode += new System.EventHandler(this.BuildRequests_ExecuteCode);
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind8.Name = "UpdateLookups";
            activitybind8.Path = "TargetLookups";
            this.Resolve.Name = "Resolve";
            activitybind9.Name = "UpdateLookups";
            activitybind9.Path = "QueryResults";
            activitybind10.Name = "UpdateLookups";
            activitybind10.Path = "Value";
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // UpdateLookups
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.BuildRequests);
            this.Activities.Add(this.ForEachPending);
            this.Activities.Add(this.PrepareUpdate);
            this.Activities.Add(this.ForEachRequest);
            this.Name = "UpdateLookups";
            this.CanModifyActivities = false;

        }

        #endregion

        private AsynchronousUpdateResource AsyncUpdate;

        private IfElseBranchActivity Standard;

        private IfElseBranchActivity Authorization;

        private IfElseActivity SwitchSubmissionType;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity Read;

        private ReplicatorActivity ForEachPending;

        private CodeActivity PrepareUpdate;

        private ResolveLookups Resolve;

        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity Update;

        private ReplicatorActivity ForEachRequest;

        private CodeActivity BuildRequests;

        private CodeActivity Prepare;


































    }
}
