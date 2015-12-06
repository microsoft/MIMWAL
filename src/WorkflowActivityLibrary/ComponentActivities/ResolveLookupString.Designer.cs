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
    internal partial class ResolveLookupString
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
            this.ResolveString = new System.Workflow.Activities.CodeActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.Parse = new System.Workflow.Activities.CodeActivity();
            // 
            // ResolveString
            // 
            this.ResolveString.Name = "ResolveString";
            this.ResolveString.ExecuteCode += new System.EventHandler(this.ResolveString_ExecuteCode);
            // 
            // Resolve
            // 
            activitybind1.Name = "ResolveLookupString";
            activitybind1.Path = "ComparedRequestId";
            activitybind2.Name = "ResolveLookupString";
            activitybind2.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            activitybind3.Name = "ResolveLookupString";
            activitybind3.Path = "QueryResults";
            activitybind4.Name = "ResolveLookupString";
            activitybind4.Path = "Value";
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ComparedRequestIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // Parse
            // 
            this.Parse.Name = "Parse";
            this.Parse.ExecuteCode += new System.EventHandler(this.Parse_ExecuteCode);
            // 
            // ResolveLookupString
            // 
            this.Activities.Add(this.Parse);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.ResolveString);
            this.Name = "ResolveLookupString";
            this.CanModifyActivities = false;

        }












        #endregion

        private ResolveLookups Resolve;
        private CodeActivity ResolveString;
        private CodeActivity Parse;
    }
}
