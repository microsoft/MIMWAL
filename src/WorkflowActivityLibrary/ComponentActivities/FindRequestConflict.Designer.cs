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
    internal partial class FindRequestConflict
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
            System.Collections.Generic.List<System.Guid> list_11 = new System.Collections.Generic.List<System.Guid>();
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_12 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            this.ResolveCompared = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.ForEachRequest = new System.Workflow.Activities.ReplicatorActivity();
            this.Find = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ResolveStandard = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            this.Request = new Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity();
            // 
            // ResolveCompared
            // 
            this.ResolveCompared.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind1.Name = "FindRequestConflict";
            activitybind1.Path = "ComparedRequestLookups";
            this.ResolveCompared.Name = "ResolveCompared";
            this.ResolveCompared.QueryResults = null;
            this.ResolveCompared.Value = null;
            this.ResolveCompared.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            activitybind2.Name = "Find";
            activitybind2.Path = "FoundIds";
            // 
            // ForEachRequest
            // 
            this.ForEachRequest.Activities.Add(this.ResolveCompared);
            this.ForEachRequest.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachRequest.Name = "ForEachRequest";
            this.ForEachRequest.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachRequest_ChildInitialized);
            this.ForEachRequest.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachRequest_ChildCompleted);
            this.ForEachRequest.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // Find
            // 
            this.Find.Attributes = null;
            this.Find.ExcludeWorkflowTarget = false;
            this.Find.FoundIds = list_11;
            this.Find.FoundResources = list_12;
            this.Find.Name = "Find";
            this.Find.Value = null;
            this.Find.XPathFilter = null;
            // 
            // ResolveStandard
            // 
            this.ResolveStandard.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind3.Name = "FindRequestConflict";
            activitybind3.Path = "ExpressionEvaluator.LookupCache";
            this.ResolveStandard.Name = "ResolveStandard";
            this.ResolveStandard.QueryResults = null;
            this.ResolveStandard.Value = null;
            this.ResolveStandard.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
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
            // FindRequestConflict
            // 
            this.Activities.Add(this.Request);
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.ResolveStandard);
            this.Activities.Add(this.Find);
            this.Activities.Add(this.ForEachRequest);
            this.Name = "FindRequestConflict";
            this.CanModifyActivities = false;

        }

        #endregion

        private ResolveLookups ResolveCompared;

        private ResolveLookups ResolveStandard;

        private FindResources Find;

        private CodeActivity Prepare;

        private ReplicatorActivity ForEachRequest;

        private Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity Request;














































    }
}
