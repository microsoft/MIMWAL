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
    public partial class GenerateUniqueValue
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
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            this.ResolveFilter = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookupString();
            this.Decide = new System.Workflow.Activities.CodeActivity();
            this.ForEachLdap = new System.Workflow.Activities.ReplicatorActivity();
            this.FindConflict = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.TryValue = new System.Workflow.Activities.SequenceActivity();
            this.Publish = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups();
            this.WhileNotUnique = new System.Workflow.Activities.WhileActivity();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            this.ActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.ParseExpressions = new System.Workflow.Activities.CodeActivity();
            // 
            // ResolveFilter
            // 
            this.ResolveFilter.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ResolveFilter.Name = "ResolveFilter";
            this.ResolveFilter.QueryResults = null;
            this.ResolveFilter.Resolved = null;
            this.ResolveFilter.StringForResolution = null;
            this.ResolveFilter.Value = null;
            // 
            // Decide
            // 
            this.Decide.Name = "Decide";
            this.Decide.ExecuteCode += new System.EventHandler(this.Decide_ExecuteCode);
            activitybind1.Name = "GenerateUniqueValue";
            activitybind1.Path = "LdapQueries";
            // 
            // ForEachLdap
            // 
            this.ForEachLdap.Activities.Add(this.ResolveFilter);
            this.ForEachLdap.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachLdap.Name = "ForEachLdap";
            this.ForEachLdap.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachLdap_ChildInitialized);
            this.ForEachLdap.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.ForEachLdap_ChildCompleted);
            this.ForEachLdap.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // FindConflict
            // 
            this.FindConflict.Attributes = null;
            this.FindConflict.ExcludeWorkflowTarget = false;
            activitybind2.Name = "GenerateUniqueValue";
            activitybind2.Path = "FoundIds";
            activitybind5.Name = "GenerateUniqueValue";
            activitybind5.Path = "FoundResources";
            this.FindConflict.Name = "FindConflict";
            this.FindConflict.Value = null;
            activitybind3.Name = "GenerateUniqueValue";
            activitybind3.Path = "XPathConflictFilter";
            this.FindConflict.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.FindConflict.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundIdsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.FindConflict.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundResourcesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // TryValue
            // 
            this.TryValue.Activities.Add(this.FindConflict);
            this.TryValue.Activities.Add(this.ForEachLdap);
            this.TryValue.Activities.Add(this.Decide);
            this.TryValue.Name = "TryValue";
            // 
            // Publish
            // 
            this.Publish.Actor = new System.Guid("e05d1f1b-3d5e-4014-baa6-94dee7d68c89");
            this.Publish.ApplyAuthorizationPolicy = false;
            this.Publish.Name = "Publish";
            this.Publish.QueryResults = null;
            this.Publish.UpdateLookupDefinitions = null;
            this.Publish.Value = null;
            // 
            // WhileNotUnique
            // 
            this.WhileNotUnique.Activities.Add(this.TryValue);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.WhileNotUnique_Condition);
            this.WhileNotUnique.Condition = codecondition1;
            this.WhileNotUnique.Name = "WhileNotUnique";
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // ActivityExecutionConditionSatisfied
            // 
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.Prepare);
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.WhileNotUnique);
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.Publish);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActivityExecutionConditionSatisfied_Condition);
            this.ActivityExecutionConditionSatisfied.Condition = codecondition2;
            this.ActivityExecutionConditionSatisfied.Name = "ConditionSatisfied";
            // 
            // IfActivityExecutionConditionSatisfied
            // 
            this.IfActivityExecutionConditionSatisfied.Activities.Add(this.ActivityExecutionConditionSatisfied);
            this.IfActivityExecutionConditionSatisfied.Name = "IfActivityExecutionConditionSatisfied";
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind4.Name = "GenerateUniqueValue";
            activitybind4.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            this.Resolve.QueryResults = null;
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // ParseExpressions
            // 
            this.ParseExpressions.Name = "ParseExpressions";
            this.ParseExpressions.ExecuteCode += new System.EventHandler(this.ParseExpressions_ExecuteCode);
            // 
            // GenerateUniqueValue
            // 
            this.Activities.Add(this.ParseExpressions);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.IfActivityExecutionConditionSatisfied);
            this.Name = "GenerateUniqueValue";
            this.CanModifyActivities = false;

        }

        #endregion

        private ReplicatorActivity ForEachLdap;

        private IfElseBranchActivity ActivityExecutionConditionSatisfied;

        private IfElseActivity IfActivityExecutionConditionSatisfied;

        private ComponentActivities.ResolveLookupString ResolveFilter;

        private ComponentActivities.ResolveLookups Resolve;

        private ComponentActivities.UpdateLookups Publish;

        private CodeActivity ParseExpressions;

        private CodeActivity Decide;

        private WhileActivity WhileNotUnique;

        private SequenceActivity TryValue;

        private ComponentActivities.FindResources FindConflict;

        private CodeActivity Prepare;



























    }
}
