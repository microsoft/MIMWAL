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
    public partial class DeleteResources
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
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            this.Delete = new Microsoft.ResourceManagement.Workflow.Activities.DeleteResourceActivity();
            this.AsyncDelete = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousDeleteResource();
            this.Standard = new System.Workflow.Activities.IfElseBranchActivity();
            this.Authorization = new System.Workflow.Activities.IfElseBranchActivity();
            this.Find = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.SwitchSubmissionType = new System.Workflow.Activities.IfElseActivity();
            this.ResolveTarget = new System.Workflow.Activities.IfElseBranchActivity();
            this.SearchForTarget = new System.Workflow.Activities.IfElseBranchActivity();
            this.ForEachTarget = new System.Workflow.Activities.ReplicatorActivity();
            this.PrepareDelete = new System.Workflow.Activities.CodeActivity();
            this.SwitchTargetType = new System.Workflow.Activities.IfElseActivity();
            this.PrepareTarget = new System.Workflow.Activities.CodeActivity();
            this.ResolveForValue = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.ProcessDelete = new System.Workflow.Activities.SequenceActivity();
            this.ForEachIteration = new System.Workflow.Activities.ReplicatorActivity();
            this.PrepareIteration = new System.Workflow.Activities.CodeActivity();
            this.GetActor = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // Delete
            // 
            activitybind1.Name = "GetActor";
            activitybind1.Path = "Actor";
            this.Delete.ApplyAuthorizationPolicy = false;
            this.Delete.AuthorizationWaitTimeInSeconds = -1;
            this.Delete.Name = "Delete";
            activitybind2.Name = "DeleteResources";
            activitybind2.Path = "Target";
            this.Delete.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.DeleteResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Delete.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.DeleteResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // AsyncDelete
            // 
            activitybind3.Name = "GetActor";
            activitybind3.Path = "Actor";
            this.AsyncDelete.ApplyAuthorizationPolicy = true;
            this.AsyncDelete.Name = "AsyncDelete";
            activitybind4.Name = "DeleteResources";
            activitybind4.Path = "Target";
            this.AsyncDelete.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousDeleteResource.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.AsyncDelete.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousDeleteResource.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // Standard
            // 
            this.Standard.Activities.Add(this.Delete);
            this.Standard.Name = "Standard";
            // 
            // Authorization
            // 
            this.Authorization.Activities.Add(this.AsyncDelete);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.Authorization_Condition);
            this.Authorization.Condition = codecondition1;
            this.Authorization.Name = "Authorization";
            // 
            // Find
            // 
            this.Find.Attributes = null;
            this.Find.ExcludeWorkflowTarget = false;
            activitybind5.Name = "DeleteResources";
            activitybind5.Path = "Targets";
            this.Find.FoundResources = list_11;
            this.Find.Name = "Find";
            activitybind6.Name = "DeleteResources";
            activitybind6.Path = "Value";
            activitybind7.Name = "DeleteResources";
            activitybind7.Path = "TargetExpression";
            this.Find.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.Find.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.Find.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundIdsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // SwitchSubmissionType
            // 
            this.SwitchSubmissionType.Activities.Add(this.Authorization);
            this.SwitchSubmissionType.Activities.Add(this.Standard);
            this.SwitchSubmissionType.Name = "SwitchSubmissionType";
            // 
            // ResolveTarget
            // 
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ResolveTarget_Condition);
            this.ResolveTarget.Condition = codecondition2;
            this.ResolveTarget.Name = "ResolveTarget";
            // 
            // SearchForTarget
            // 
            this.SearchForTarget.Activities.Add(this.Find);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.SearchForTarget_Condition);
            this.SearchForTarget.Condition = codecondition3;
            this.SearchForTarget.Name = "SearchForTarget";
            activitybind8.Name = "DeleteResources";
            activitybind8.Path = "Targets";
            // 
            // ForEachTarget
            // 
            this.ForEachTarget.Activities.Add(this.SwitchSubmissionType);
            this.ForEachTarget.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachTarget.Name = "ForEachTarget";
            this.ForEachTarget.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachTarget_ChildInitialized);
            this.ForEachTarget.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // PrepareDelete
            // 
            this.PrepareDelete.Name = "PrepareDelete";
            this.PrepareDelete.ExecuteCode += new System.EventHandler(this.PrepareDelete_ExecuteCode);
            // 
            // SwitchTargetType
            // 
            this.SwitchTargetType.Activities.Add(this.SearchForTarget);
            this.SwitchTargetType.Activities.Add(this.ResolveTarget);
            this.SwitchTargetType.Name = "SwitchTargetType";
            // 
            // PrepareTarget
            // 
            this.PrepareTarget.Name = "PrepareTarget";
            this.PrepareTarget.ExecuteCode += new System.EventHandler(this.PrepareTarget_ExecuteCode);
            // 
            // ResolveForValue
            // 
            this.ResolveForValue.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind9.Name = "DeleteResources";
            activitybind9.Path = "ValueExpressions";
            this.ResolveForValue.Name = "ResolveForValue";
            this.ResolveForValue.QueryResults = null;
            activitybind10.Name = "DeleteResources";
            activitybind10.Path = "Value";
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // ProcessDelete
            // 
            this.ProcessDelete.Activities.Add(this.ResolveForValue);
            this.ProcessDelete.Activities.Add(this.PrepareTarget);
            this.ProcessDelete.Activities.Add(this.SwitchTargetType);
            this.ProcessDelete.Activities.Add(this.PrepareDelete);
            this.ProcessDelete.Activities.Add(this.ForEachTarget);
            this.ProcessDelete.Name = "ProcessDelete";
            // 
            // ForEachIteration
            // 
            this.ForEachIteration.Activities.Add(this.ProcessDelete);
            this.ForEachIteration.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachIteration.Name = "ForEachIteration";
            this.ForEachIteration.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachIteration_ChildInitialized);
            // 
            // PrepareIteration
            // 
            this.PrepareIteration.Name = "PrepareIteration";
            this.PrepareIteration.ExecuteCode += new System.EventHandler(this.PrepareIteration_ExecuteCode);
            // 
            // GetActor
            // 
            this.GetActor.Actor = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind11.Name = "DeleteResources";
            activitybind11.Path = "ActorString";
            activitybind12.Name = "DeleteResources";
            activitybind12.Path = "ActorType";
            this.GetActor.Name = "GetActor";
            this.GetActor.QueryResults = null;
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorStringProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind13.Name = "DeleteResources";
            activitybind13.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            this.Resolve.QueryResults = null;
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.PrepareResolve_ExecuteCode);
            // 
            // DeleteResources
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.GetActor);
            this.Activities.Add(this.PrepareIteration);
            this.Activities.Add(this.ForEachIteration);
            this.Name = "DeleteResources";
            this.CanModifyActivities = false;

        }

        #endregion

        private ComponentActivities.ResolveLookups ResolveForValue;

        private CodeActivity PrepareTarget;

        private SequenceActivity ProcessDelete;

        private ReplicatorActivity ForEachIteration;

        private CodeActivity PrepareIteration;

        private ComponentActivities.ResolveLookups Resolve;

        private ComponentActivities.AsynchronousDeleteResource AsyncDelete;

        private ComponentActivities.DetermineActor GetActor;

        private IfElseBranchActivity Standard;

        private IfElseBranchActivity Authorization;

        private IfElseActivity SwitchSubmissionType;

        private CodeActivity Prepare;

        private ReplicatorActivity ForEachTarget;

        private IfElseBranchActivity ResolveTarget;

        private CodeActivity PrepareDelete;

        private IfElseBranchActivity SearchForTarget;

        private IfElseActivity SwitchTargetType;

        private ComponentActivities.FindResources Find;

        private Microsoft.ResourceManagement.Workflow.Activities.DeleteResourceActivity Delete;


























































































    }
}
