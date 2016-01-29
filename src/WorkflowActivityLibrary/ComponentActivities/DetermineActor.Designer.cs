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
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            this.Query = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.PrepareAccountActor = new System.Workflow.Activities.CodeActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.PrepareResolveActor = new System.Workflow.Activities.CodeActivity();
            this.AccountActor = new System.Workflow.Activities.IfElseBranchActivity();
            this.ResolveActor = new System.Workflow.Activities.IfElseBranchActivity();
            this.Decide = new System.Workflow.Activities.CodeActivity();
            this.IfActorIsAccountActor = new System.Workflow.Activities.IfElseActivity();
            this.IfActorIsExpression = new System.Workflow.Activities.IfElseActivity();
            this.ActorIsNotSet = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfActorIsNotSet = new System.Workflow.Activities.IfElseActivity();
            // 
            // Query
            // 
            this.Query.Attributes = null;
            this.Query.ExcludeWorkflowTarget = false;
            this.Query.FoundIds = null;
            this.Query.FoundResources = null;
            this.Query.Name = "Query";
            this.Query.QueryResults = null;
            this.Query.Value = null;
            this.Query.XPathFilter = null;
            // 
            // PrepareAccountActor
            // 
            this.PrepareAccountActor.Name = "PrepareAccountActor";
            this.PrepareAccountActor.ExecuteCode += new System.EventHandler(this.PrepareAccountActor_ExecuteCode);
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind1.Name = "DetermineActor";
            activitybind1.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            activitybind2.Name = "DetermineActor";
            activitybind2.Path = "QueryResults";
            activitybind3.Name = "DetermineActor";
            activitybind3.Path = "Value";
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // PrepareResolveActor
            // 
            this.PrepareResolveActor.Name = "PrepareResolveActor";
            this.PrepareResolveActor.ExecuteCode += new System.EventHandler(this.PrepareResolveActor_ExecuteCode);
            // 
            // AccountActor
            // 
            this.AccountActor.Activities.Add(this.PrepareAccountActor);
            this.AccountActor.Activities.Add(this.Query);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.AccountActor_Condition);
            this.AccountActor.Condition = codecondition1;
            this.AccountActor.Name = "AccountActor";
            // 
            // ResolveActor
            // 
            this.ResolveActor.Activities.Add(this.PrepareResolveActor);
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
            // IfActorIsAccountActor
            // 
            this.IfActorIsAccountActor.Activities.Add(this.AccountActor);
            this.IfActorIsAccountActor.Name = "IfActorIsAccountActor";
            // 
            // IfActorIsExpression
            // 
            this.IfActorIsExpression.Activities.Add(this.ResolveActor);
            this.IfActorIsExpression.Name = "IfActorIsExpression";
            // 
            // ActorIsNotSet
            // 
            this.ActorIsNotSet.Activities.Add(this.IfActorIsExpression);
            this.ActorIsNotSet.Activities.Add(this.IfActorIsAccountActor);
            this.ActorIsNotSet.Activities.Add(this.Decide);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActorIsNotSet_Condition);
            this.ActorIsNotSet.Condition = codecondition3;
            this.ActorIsNotSet.Name = "ActorIsNotSet";
            // 
            // IfActorIsNotSet
            // 
            this.IfActorIsNotSet.Activities.Add(this.ActorIsNotSet);
            this.IfActorIsNotSet.Name = "IfActorIsNotSet";
            // 
            // DetermineActor
            // 
            this.Activities.Add(this.IfActorIsNotSet);
            this.Name = "DetermineActor";
            this.CanModifyActivities = false;

        }










        #endregion

        private FindResources Query;
        private IfElseBranchActivity AccountActor;
        private IfElseActivity IfActorIsAccountActor;
        private CodeActivity PrepareAccountActor;
        private IfElseBranchActivity ActorIsNotSet;
        private IfElseActivity IfActorIsNotSet;
        private CodeActivity PrepareResolveActor;
        private ResolveLookups Resolve;
        private IfElseBranchActivity ResolveActor;
        private CodeActivity Decide;
        private IfElseActivity IfActorIsExpression;
    }
}
