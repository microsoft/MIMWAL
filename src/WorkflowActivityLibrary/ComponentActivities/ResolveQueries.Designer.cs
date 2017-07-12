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
    internal partial class ResolveQueries
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
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_12 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            this.RunQuery = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ForEachQuery = new System.Workflow.Activities.ReplicatorActivity();
            // 
            // RunQuery
            // 
            this.RunQuery.Attributes = null;
            this.RunQuery.ExcludeWorkflowTarget = false;
            this.RunQuery.FoundIds = list_11;
            this.RunQuery.FoundResources = list_12;
            this.RunQuery.Name = "RunQuery";
            this.RunQuery.QueryResults = null;
            activitybind1.Name = "ResolveQueries";
            activitybind1.Path = "Value";
            this.RunQuery.XPathFilter = null;
            this.RunQuery.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            activitybind2.Name = "ResolveQueries";
            activitybind2.Path = "QueryDefinitions";
            // 
            // ForEachQuery
            // 
            this.ForEachQuery.Activities.Add(this.RunQuery);
            this.ForEachQuery.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachQuery.Name = "ForEachQuery";
            this.ForEachQuery.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachQuery_ChildInitialized);
            this.ForEachQuery.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachQuery_ChildCompleted);
            this.ForEachQuery.Initialized += new System.EventHandler(this.ForEachQuery_Initialized);
            this.ForEachQuery.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // ResolveQueries
            // 
            this.Activities.Add(this.ForEachQuery);
            this.Name = "ResolveQueries";
            this.CanModifyActivities = false;

        }









        #endregion

        private ReplicatorActivity ForEachQuery;
        private FindResources RunQuery;
    }
}
