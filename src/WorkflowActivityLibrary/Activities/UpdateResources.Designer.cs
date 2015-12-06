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
            System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Guid>> dictionary_21 = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Guid>>();
            this.Update = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups();
            this.PrepareUpdate = new System.Workflow.Activities.CodeActivity();
            this.ResolveForValue = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.Submit = new System.Workflow.Activities.SequenceActivity();
            this.ForEachIteration = new System.Workflow.Activities.ReplicatorActivity();
            this.PrepareIteration = new System.Workflow.Activities.CodeActivity();
            this.GetActor = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.RunQueries = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // Update
            // 
            activitybind1.Name = "GetActor";
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
            // PrepareUpdate
            // 
            this.PrepareUpdate.Name = "PrepareUpdate";
            this.PrepareUpdate.ExecuteCode += new System.EventHandler(this.PrepareUpdate_ExecuteCode);
            // 
            // ResolveForValue
            // 
            this.ResolveForValue.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind6.Name = "UpdateResources";
            activitybind6.Path = "ValueExpressions";
            this.ResolveForValue.Name = "ResolveForValue";
            this.ResolveForValue.QueryResults = null;
            activitybind7.Name = "UpdateResources";
            activitybind7.Path = "Value";
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // Submit
            // 
            this.Submit.Activities.Add(this.ResolveForValue);
            this.Submit.Activities.Add(this.PrepareUpdate);
            this.Submit.Activities.Add(this.Update);
            this.Submit.Name = "Submit";
            // 
            // ForEachIteration
            // 
            this.ForEachIteration.Activities.Add(this.Submit);
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
            activitybind8.Name = "UpdateResources";
            activitybind8.Path = "ActorString";
            activitybind9.Name = "UpdateResources";
            activitybind9.Path = "ActorType";
            this.GetActor.Name = "GetActor";
            activitybind10.Name = "RunQueries";
            activitybind10.Path = "QueryResults";
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorStringProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind11.Name = "UpdateResources";
            activitybind11.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            activitybind12.Name = "RunQueries";
            activitybind12.Path = "QueryResults";
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            // 
            // RunQueries
            // 
            this.RunQueries.Name = "RunQueries";
            activitybind13.Name = "UpdateResources";
            activitybind13.Path = "Queries";
            this.RunQueries.QueryResults = dictionary_21;
            this.RunQueries.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.QueryDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
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
            this.Activities.Add(this.GetActor);
            this.Activities.Add(this.PrepareIteration);
            this.Activities.Add(this.ForEachIteration);
            this.Name = "UpdateResources";
            this.CanModifyActivities = false;

        }

        #endregion

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
