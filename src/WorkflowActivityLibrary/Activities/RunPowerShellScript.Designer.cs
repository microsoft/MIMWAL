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
    public partial class RunPowerShellScript
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            this.PublishOutput = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups();
            this.Run = new System.Workflow.Activities.CodeActivity();
            this.ActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.Prepare = new System.Workflow.Activities.CodeActivity();
            // 
            // PublishOutput
            // 
            this.PublishOutput.Actor = new System.Guid("e05d1f1b-3d5e-4014-baa6-94dee7d68c89");
            this.PublishOutput.ApplyAuthorizationPolicy = false;
            this.PublishOutput.Name = "PublishOutput";
            this.PublishOutput.QueryResults = null;
            this.PublishOutput.UpdateLookupDefinitions = null;
            this.PublishOutput.Value = null;
            // 
            // Run
            // 
            this.Run.Name = "Run";
            this.Run.ExecuteCode += new System.EventHandler(this.Run_ExecuteCode);
            // 
            // ActivityExecutionConditionSatisfied
            // 
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.Run);
            this.ActivityExecutionConditionSatisfied.Activities.Add(this.PublishOutput);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActivityExecutionConditionSatisfied_Condition);
            this.ActivityExecutionConditionSatisfied.Condition = codecondition1;
            this.ActivityExecutionConditionSatisfied.Name = "ActivityExecutionConditionSatisfied";
            // 
            // IfActivityExecutionConditionSatisfied
            // 
            this.IfActivityExecutionConditionSatisfied.Activities.Add(this.ActivityExecutionConditionSatisfied);
            this.IfActivityExecutionConditionSatisfied.Name = "IfActivityExecutionConditionSatisfied";
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind1.Name = "RunPowerShellScript";
            activitybind1.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            this.Resolve.QueryResults = null;
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // Prepare
            // 
            this.Prepare.Name = "Prepare";
            this.Prepare.ExecuteCode += new System.EventHandler(this.Prepare_ExecuteCode);
            // 
            // RunPowerShellScript
            // 
            this.Activities.Add(this.Prepare);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.IfActivityExecutionConditionSatisfied);
            this.Name = "RunPowerShellScript";
            this.CanModifyActivities = false;

        }

        #endregion

        private IfElseBranchActivity ActivityExecutionConditionSatisfied;

        private IfElseActivity IfActivityExecutionConditionSatisfied;

        private ComponentActivities.UpdateLookups PublishOutput;

        private ComponentActivities.ResolveLookups Resolve;

        private CodeActivity Run;

        private CodeActivity Prepare;




















    }
}
