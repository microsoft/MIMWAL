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
    internal partial class DetermineActor
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
            System.Collections.Generic.List<System.Guid> list_11 = new System.Collections.Generic.List<System.Guid>();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Collections.Generic.List<System.Guid> list_12 = new System.Collections.Generic.List<System.Guid>();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            this.Query = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.AccountActor = new System.Workflow.Activities.IfElseBranchActivity();
            this.ResolveActor = new System.Workflow.Activities.IfElseBranchActivity();
            this.Decide = new System.Workflow.Activities.CodeActivity();
            this.SwitchActorType = new System.Workflow.Activities.IfElseActivity();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // Query
            // 
            this.Query.Attributes = null;
            this.Query.ExcludeWorkflowTarget = false;
            this.Query.FoundIds = null;
            this.Query.FoundResources = null;
            this.Query.Name = "Query";
            this.Query.Value = null;
            this.Query.XPathFilter = null;
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind1.Name = "DetermineActor";
            activitybind1.Path = "ExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            activitybind2.Name = "DetermineActor";
            activitybind2.Path = "QueryResults";
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // AccountActor
            // 
            this.AccountActor.Activities.Add(this.Query);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.AccountActor_Condition);
            this.AccountActor.Condition = codecondition1;
            this.AccountActor.Name = "AccountActor";
            // 
            // ResolveActor
            // 
            this.ResolveActor.Activities.Add(this.Resolve);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ResolveActor_Condition);
            this.ResolveActor.Condition = codecondition2;
            this.ResolveActor.Name = "ResolveActor";
            // 
            // Decide
            // 
            this.Decide.Name = "Decide";
            this.Decide.ExecuteCode += new System.EventHandler(this.Decide_ExecuteCode);
            // 
            // SwitchActorType
            // 
            this.SwitchActorType.Activities.Add(this.ResolveActor);
            this.SwitchActorType.Activities.Add(this.AccountActor);
            this.SwitchActorType.Name = "SwitchActorType";
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // DetermineActor
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.SwitchActorType);
            this.Activities.Add(this.Decide);
            this.Name = "DetermineActor";
            this.CanModifyActivities = false;

        }

        #endregion

        private FindResources Query;

        private CodeActivity Prepare;

        private ResolveLookups Resolve;

        private IfElseBranchActivity AccountActor;

        private IfElseBranchActivity ResolveActor;

        private CodeActivity Decide;

        private IfElseActivity SwitchActorType;










    }
}
