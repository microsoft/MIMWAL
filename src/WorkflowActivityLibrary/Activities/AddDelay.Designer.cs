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
    public partial class AddDelay
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
            this.TraceWakeup = new System.Workflow.Activities.CodeActivity();
            this.Delay = new System.Workflow.Activities.DelayActivity();
            this.ConditionSatisfied = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfActivityExecutionConditionSatisfied = new System.Workflow.Activities.IfElseActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.ParseExpressions = new System.Workflow.Activities.CodeActivity();
            // 
            // TraceWakeup
            // 
            this.TraceWakeup.Name = "TraceWakeup";
            this.TraceWakeup.ExecuteCode += new System.EventHandler(this.TraceWakeup_ExecuteCode);
            // 
            // Delay
            // 
            this.Delay.Name = "Delay";
            this.Delay.TimeoutDuration = System.TimeSpan.Parse("00:00:00");
            this.Delay.InitializeTimeoutDuration += new System.EventHandler(this.Delay_InitializeTimeoutDuration);
            // 
            // ConditionSatisfied
            // 
            this.ConditionSatisfied.Activities.Add(this.Delay);
            this.ConditionSatisfied.Activities.Add(this.TraceWakeup);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActivityExecutionConditionSatisfied_Condition);
            this.ConditionSatisfied.Condition = codecondition1;
            this.ConditionSatisfied.Name = "ConditionSatisfied";
            // 
            // IfActivityExecutionConditionSatisfied
            // 
            this.IfActivityExecutionConditionSatisfied.Activities.Add(this.ConditionSatisfied);
            this.IfActivityExecutionConditionSatisfied.Name = "IfActivityExecutionConditionSatisfied";
            // 
            // Resolve
            // 
            this.Resolve.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind1.Name = "AddDelay";
            activitybind1.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            this.Resolve.QueryResults = null;
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ParseExpressions
            // 
            this.ParseExpressions.Name = "ParseExpressions";
            this.ParseExpressions.ExecuteCode += new System.EventHandler(this.ParseExpressions_ExecuteCode);
            // 
            // AddDelay
            // 
            this.Activities.Add(this.ParseExpressions);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.IfActivityExecutionConditionSatisfied);
            this.Name = "AddDelay";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity TraceWakeup;
        private ComponentActivities.ResolveLookups Resolve;
        private DelayActivity Delay;
        private IfElseBranchActivity ConditionSatisfied;
        private IfElseActivity IfActivityExecutionConditionSatisfied;
        private CodeActivity ParseExpressions;
    }
}
