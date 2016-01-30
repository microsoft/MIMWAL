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
    public partial class UpdateResources
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Guid>> dictionary_21 = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Guid>>();
            this.Update = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups();
            this.GetActorForChildRequest = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor();
            this.PrepareUpdate = new System.Workflow.Activities.CodeActivity();
            this.ResolveForValue = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.GetActor = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor();
            this.Submit = new System.Workflow.Activities.SequenceActivity();
            this.ActorIsNotValueExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.ForEachIteration = new System.Workflow.Activities.ReplicatorActivity();
            this.PrepareIteration = new System.Workflow.Activities.CodeActivity();
            this.IfActorIsNotValueExpression = new System.Workflow.Activities.IfElseActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.RunQueries = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // Update
            // 
            activitybind1.Name = "GetActorForChildRequest";
            activitybind1.Path = "Actor";
            activitybind2.Name = "UpdateResources";
            activitybind2.Path = "ApplyAuthorizationPolicy";
            this.Update.Name = "Update";
            activitybind3.Name = "RunQueries";
            activitybind3.Path = "QueryResults";
            activitybind4.Name = "UpdateResources";
            activitybind4.Path = "LookupUpdates";
            activitybind5.Name = "UpdateResources";
            activitybind5.Path = "Value";
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.UpdateLookupDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.ActorProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.Update.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.ApplyAuthorizationPolicyProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // GetActorForChildRequest
            // 
            activitybind6.Name = "GetActor";
            activitybind6.Path = "Actor";
            activitybind7.Name = "UpdateResources";
            activitybind7.Path = "ActorString";
            activitybind8.Name = "UpdateResources";
            activitybind8.Path = "ActorType";
            this.GetActorForChildRequest.Name = "GetActorForChildRequest";
            activitybind9.Name = "RunQueries";
            activitybind9.Path = "QueryResults";
            activitybind10.Name = "UpdateResources";
            activitybind10.Path = "Value";
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorStringProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // PrepareUpdate
            // 
            this.PrepareUpdate.Name = "PrepareUpdate";
            this.PrepareUpdate.ExecuteCode += new System.EventHandler(this.PrepareUpdate_ExecuteCode);
            // 
            // ResolveForValue
            // 
            this.ResolveForValue.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind11.Name = "UpdateResources";
            activitybind11.Path = "ValueExpressions";
            this.ResolveForValue.Name = "ResolveForValue";
            this.ResolveForValue.QueryResults = null;
            activitybind12.Name = "UpdateResources";
            activitybind12.Path = "Value";
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // GetActor
            // 
            this.GetActor.Actor = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind13.Name = "UpdateResources";
            activitybind13.Path = "ActorString";
            activitybind14.Name = "UpdateResources";
            activitybind14.Path = "ActorType";
            this.GetActor.Name = "GetActor";
            activitybind15.Name = "RunQueries";
            activitybind15.Path = "QueryResults";
            this.GetActor.Value = null;
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorStringProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // Submit
            // 
            this.Submit.Activities.Add(this.ResolveForValue);
            this.Submit.Activities.Add(this.PrepareUpdate);
            this.Submit.Activities.Add(this.GetActorForChildRequest);
            this.Submit.Activities.Add(this.Update);
            this.Submit.Name = "Submit";
            // 
            // ActorIsNotValueExpression
            // 
            this.ActorIsNotValueExpression.Activities.Add(this.GetActor);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActorIsNotValueExpression_Condition);
            this.ActorIsNotValueExpression.Condition = codecondition1;
            this.ActorIsNotValueExpression.Name = "ActorIsNotValueExpression";
            // 
            // ForEachIteration
            // 
            this.ForEachIteration.Activities.Add(this.Submit);
            this.ForEachIteration.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachIteration.Name = "ForEachIteration";
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ForEachIteration_UntilCondition);
            this.ForEachIteration.UntilCondition = codecondition2;
            this.ForEachIteration.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachIteration_ChildInitialized);
            this.ForEachIteration.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachIteration_ChildCompleted);
            // 
            // PrepareIteration
            // 
            this.PrepareIteration.Name = "PrepareIteration";
            this.PrepareIteration.ExecuteCode += new System.EventHandler(this.PrepareIteration_ExecuteCode);
            // 
            // IfActorIsNotValueExpression
            // 
            this.IfActorIsNotValueExpression.Activities.Add(this.ActorIsNotValueExpression);
            this.IfActorIsNotValueExpression.Name = "IfActorIsNotValueExpression";
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind16.Name = "UpdateResources";
            activitybind16.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            activitybind17.Name = "RunQueries";
            activitybind17.Path = "QueryResults";
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            // 
            // RunQueries
            // 
            this.RunQueries.Name = "RunQueries";
            activitybind18.Name = "UpdateResources";
            activitybind18.Path = "Queries";
            this.RunQueries.QueryResults = dictionary_21;
            this.RunQueries.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.QueryDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // UpdateResources
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.RunQueries);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.IfActorIsNotValueExpression);
            this.Activities.Add(this.PrepareIteration);
            this.Activities.Add(this.ForEachIteration);
            this.Name = "UpdateResources";
            this.CanModifyActivities = false;

        }

















































































        #endregion

        private ComponentActivities.DetermineActor GetActorForChildRequest;
        private IfElseBranchActivity ActorIsNotValueExpression;
        private IfElseActivity IfActorIsNotValueExpression;
        private ComponentActivities.ResolveLookups ResolveForValue;
        private CodeActivity PrepareUpdate;
        private SequenceActivity Submit;
        private ReplicatorActivity ForEachIteration;
        private ComponentActivities.DetermineActor GetActor;
        private ComponentActivities.ResolveLookups Resolve;
        private ComponentActivities.UpdateLookups Update;
        private ComponentActivities.ResolveQueries RunQueries;
        private CodeActivity Prepare;
        private CodeActivity PrepareIteration;
    }
}
