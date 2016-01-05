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
    internal partial class FindResources
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
            this.ReadFoundResource = new System.Workflow.Activities.CodeActivity();
            this.Find = new Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity();
            this.ResolveFilter = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // ReadFoundResource
            // 
            this.ReadFoundResource.Name = "ReadFoundResource";
            this.ReadFoundResource.ExecuteCode += new System.EventHandler(this.ReadFoundResource_ExecuteCode);
            // 
            // Find
            // 
            this.Find.Activities.Add(this.ReadFoundResource);
            activitybind1.Name = "FindResources";
            activitybind1.Path = "ServiceActor";
            this.Find.Name = "Find";
            this.Find.PageSize = 100;
            this.Find.Selection = null;
            this.Find.SortingAttributes = null;
            this.Find.TotalResultsCount = 0;
            activitybind2.Name = "ResolveFilter";
            activitybind2.Path = "Resolved";
            this.Find.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Find.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // ResolveFilter
            // 
            this.ResolveFilter.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ResolveFilter.Name = "ResolveFilter";
            this.ResolveFilter.QueryResults = null;
            this.ResolveFilter.Resolved = null;
            activitybind3.Name = "FindResources";
            activitybind3.Path = "XPathFilter";
            activitybind4.Name = "FindResources";
            activitybind4.Path = "Value";
            activitybind5.Name = "FindResources";
            activitybind5.Path = "QueryResults";
            this.ResolveFilter.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString.StringForResolutionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.ResolveFilter.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.ResolveFilter.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // FindResources
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.ResolveFilter);
            this.Activities.Add(this.Find);
            this.Name = "FindResources";
            this.CanModifyActivities = false;

        }

        #endregion

        private ResolveLookupString ResolveFilter;

        private Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity Find;

        private CodeActivity Prepare;

        private CodeActivity ReadFoundResource;


















    }
}
