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
    public partial class CreateResource
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
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
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
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind22 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind23 = new System.Workflow.ComponentModel.ActivityBind();
            System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Guid>> dictionary_21 = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Guid>>();
            this.PublishCreated = new System.Workflow.Activities.CodeActivity();
            this.Create = new Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity();
            this.AsyncCreate = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousCreateResource();
            this.Standard = new System.Workflow.Activities.IfElseBranchActivity();
            this.Authorization = new System.Workflow.Activities.IfElseBranchActivity();
            this.Publish = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups();
            this.SwitchSubmissionType = new System.Workflow.Activities.IfElseActivity();
            this.GetActorForChildRequest = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor();
            this.PrepareCreate = new System.Workflow.Activities.CodeActivity();
            this.ResolveForValue = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.EvaluateResults = new System.Workflow.Activities.CodeActivity();
            this.FindConflict = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources();
            this.ContentToPublish = new System.Workflow.Activities.IfElseBranchActivity();
            this.Unique = new System.Workflow.Activities.IfElseBranchActivity();
            this.CheckConflict = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfContentToPublish = new System.Workflow.Activities.IfElseActivity();
            this.IfUnique = new System.Workflow.Activities.IfElseActivity();
            this.IfCheckConflict = new System.Workflow.Activities.IfElseActivity();
            this.GetActor = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor();
            this.ProcessCreate = new System.Workflow.Activities.SequenceActivity();
            this.ActorIsNotValueExpression = new System.Workflow.Activities.IfElseBranchActivity();
            this.Finish = new System.Workflow.Activities.CodeActivity();
            this.ForEachIteration = new System.Workflow.Activities.ReplicatorActivity();
            this.PrepareIteration = new System.Workflow.Activities.CodeActivity();
            this.IfActorIsNotValueExpression = new System.Workflow.Activities.IfElseActivity();
            this.Resolve = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups();
            this.RunQueries = new MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries();
            this.ParseDefinitions = new System.Workflow.Activities.CodeActivity();
            // 
            // PublishCreated
            // 
            this.PublishCreated.Name = "PublishCreated";
            this.PublishCreated.ExecuteCode += new System.EventHandler(this.PublishCreated_ExecuteCode);
            // 
            // Create
            // 
            activitybind1.Name = "GetActorForChildRequest";
            activitybind1.Path = "Actor";
            this.Create.ApplyAuthorizationPolicy = false;
            this.Create.AuthorizationWaitTimeInSeconds = -1;
            activitybind2.Name = "CreateResource";
            activitybind2.Path = "CreatedResourceId";
            activitybind3.Name = "CreateResource";
            activitybind3.Path = "CreateParameters";
            this.Create.Name = "Create";
            this.Create.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Create.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity.CreateParametersProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.Create.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity.CreatedResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // AsyncCreate
            // 
            activitybind4.Name = "GetActorForChildRequest";
            activitybind4.Path = "Actor";
            this.AsyncCreate.ApplyAuthorizationPolicy = true;
            activitybind5.Name = "CreateResource";
            activitybind5.Path = "CreateParameters";
            this.AsyncCreate.Name = "AsyncCreate";
            this.AsyncCreate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousCreateResource.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.AsyncCreate.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.AsynchronousCreateResource.CreateParametersProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // Standard
            // 
            this.Standard.Activities.Add(this.Create);
            this.Standard.Activities.Add(this.PublishCreated);
            this.Standard.Name = "Standard";
            // 
            // Authorization
            // 
            this.Authorization.Activities.Add(this.AsyncCreate);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.Authorization_Condition);
            this.Authorization.Condition = codecondition1;
            this.Authorization.Name = "Authorization";
            // 
            // Publish
            // 
            this.Publish.Actor = new System.Guid("e05d1f1b-3d5e-4014-baa6-94dee7d68c89");
            this.Publish.ApplyAuthorizationPolicy = false;
            this.Publish.Name = "Publish";
            activitybind6.Name = "RunQueries";
            activitybind6.Path = "QueryResults";
            activitybind7.Name = "CreateResource";
            activitybind7.Path = "LookupUpdates";
            this.Publish.Value = null;
            this.Publish.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.Publish.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.UpdateLookups.UpdateLookupDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // SwitchSubmissionType
            // 
            this.SwitchSubmissionType.Activities.Add(this.Authorization);
            this.SwitchSubmissionType.Activities.Add(this.Standard);
            this.SwitchSubmissionType.Name = "SwitchSubmissionType";
            // 
            // GetActorForChildRequest
            // 
            activitybind8.Name = "GetActor";
            activitybind8.Path = "Actor";
            activitybind9.Name = "CreateResource";
            activitybind9.Path = "ActorString";
            activitybind10.Name = "CreateResource";
            activitybind10.Path = "ActorType";
            this.GetActorForChildRequest.Name = "GetActorForChildRequest";
            activitybind11.Name = "RunQueries";
            activitybind11.Path = "QueryResults";
            activitybind12.Name = "CreateResource";
            activitybind12.Path = "Value";
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorStringProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.GetActorForChildRequest.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // PrepareCreate
            // 
            this.PrepareCreate.Name = "PrepareCreate";
            this.PrepareCreate.ExecuteCode += new System.EventHandler(this.PrepareCreate_ExecuteCode);
            // 
            // ResolveForValue
            // 
            this.ResolveForValue.ComparedRequestId = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind13.Name = "CreateResource";
            activitybind13.Path = "ValueExpressions";
            this.ResolveForValue.Name = "ResolveForValue";
            this.ResolveForValue.QueryResults = null;
            activitybind14.Name = "CreateResource";
            activitybind14.Path = "Value";
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.ResolveForValue.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // EvaluateResults
            // 
            this.EvaluateResults.Name = "EvaluateResults";
            this.EvaluateResults.ExecuteCode += new System.EventHandler(this.EvaluateResults_ExecuteCode);
            // 
            // FindConflict
            // 
            this.FindConflict.Attributes = null;
            this.FindConflict.ExcludeWorkflowTarget = false;
            activitybind15.Name = "CreateResource";
            activitybind15.Path = "Conflicts";
            this.FindConflict.FoundResources = list_11;
            this.FindConflict.Name = "FindConflict";
            this.FindConflict.QueryResults = null;
            activitybind16.Name = "CreateResource";
            activitybind16.Path = "Value";
            activitybind17.Name = "CreateResource";
            activitybind17.Path = "ConflictFilter";
            this.FindConflict.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.FindConflict.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.FindConflict.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.FindResources.FoundIdsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            // 
            // ContentToPublish
            // 
            this.ContentToPublish.Activities.Add(this.Publish);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ContentToPublish_Condition);
            this.ContentToPublish.Condition = codecondition2;
            this.ContentToPublish.Name = "ContentToPublish";
            // 
            // Unique
            // 
            this.Unique.Activities.Add(this.ResolveForValue);
            this.Unique.Activities.Add(this.PrepareCreate);
            this.Unique.Activities.Add(this.GetActorForChildRequest);
            this.Unique.Activities.Add(this.SwitchSubmissionType);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.Unique_Condition);
            this.Unique.Condition = codecondition3;
            this.Unique.Name = "Unique";
            // 
            // CheckConflict
            // 
            this.CheckConflict.Activities.Add(this.FindConflict);
            this.CheckConflict.Activities.Add(this.EvaluateResults);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.CheckConflict_Condition);
            this.CheckConflict.Condition = codecondition4;
            this.CheckConflict.Name = "CheckConflict";
            // 
            // IfContentToPublish
            // 
            this.IfContentToPublish.Activities.Add(this.ContentToPublish);
            this.IfContentToPublish.Name = "IfContentToPublish";
            // 
            // IfUnique
            // 
            this.IfUnique.Activities.Add(this.Unique);
            this.IfUnique.Name = "IfUnique";
            // 
            // IfCheckConflict
            // 
            this.IfCheckConflict.Activities.Add(this.CheckConflict);
            this.IfCheckConflict.Name = "IfCheckConflict";
            // 
            // GetActor
            // 
            this.GetActor.Actor = new System.Guid("00000000-0000-0000-0000-000000000000");
            activitybind18.Name = "CreateResource";
            activitybind18.Path = "ActorString";
            activitybind19.Name = "CreateResource";
            activitybind19.Path = "ActorType";
            this.GetActor.Name = "GetActor";
            activitybind20.Name = "RunQueries";
            activitybind20.Path = "QueryResults";
            this.GetActor.Value = null;
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorStringProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            this.GetActor.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.DetermineActor.ActorTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            // 
            // ProcessCreate
            // 
            this.ProcessCreate.Activities.Add(this.IfCheckConflict);
            this.ProcessCreate.Activities.Add(this.IfUnique);
            this.ProcessCreate.Activities.Add(this.IfContentToPublish);
            this.ProcessCreate.Name = "ProcessCreate";
            // 
            // ActorIsNotValueExpression
            // 
            this.ActorIsNotValueExpression.Activities.Add(this.GetActor);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ActorIsNotValueExpression_Condition);
            this.ActorIsNotValueExpression.Condition = codecondition5;
            this.ActorIsNotValueExpression.Name = "ActorIsNotValueExpression";
            // 
            // Finish
            // 
            this.Finish.Name = "Finish";
            this.Finish.ExecuteCode += new System.EventHandler(this.Finish_ExecuteCode);
            // 
            // ForEachIteration
            // 
            this.ForEachIteration.Activities.Add(this.ProcessCreate);
            this.ForEachIteration.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.ForEachIteration.Name = "ForEachIteration";
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ForEachIteration_UntilCondition);
            this.ForEachIteration.UntilCondition = codecondition6;
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
            activitybind21.Name = "CreateResource";
            activitybind21.Path = "ActivityExpressionEvaluator.LookupCache";
            this.Resolve.Name = "Resolve";
            activitybind22.Name = "RunQueries";
            activitybind22.Path = "QueryResults";
            this.Resolve.Value = null;
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.LookupsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            this.Resolve.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveLookups.QueryResultsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind22)));
            // 
            // RunQueries
            // 
            this.RunQueries.Name = "RunQueries";
            activitybind23.Name = "CreateResource";
            activitybind23.Path = "Queries";
            this.RunQueries.QueryResults = dictionary_21;
            this.RunQueries.SetBinding(MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities.ResolveQueries.QueryDefinitionsProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind23)));
            // 
            // ParseDefinitions
            // 
            this.ParseDefinitions.Name = "ParseDefinitions";
            this.ParseDefinitions.ExecuteCode += new System.EventHandler(this.ParseDefinitions_ExecuteCode);
            // 
            // CreateResource
            // 
            this.Activities.Add(this.ParseDefinitions);
            this.Activities.Add(this.RunQueries);
            this.Activities.Add(this.Resolve);
            this.Activities.Add(this.IfActorIsNotValueExpression);
            this.Activities.Add(this.PrepareIteration);
            this.Activities.Add(this.ForEachIteration);
            this.Activities.Add(this.Finish);
            this.Name = "CreateResource";
            this.CanModifyActivities = false;

        }









































































































        #endregion

        private ComponentActivities.DetermineActor GetActorForChildRequest;
        private ComponentActivities.DetermineActor GetActor;
        private IfElseBranchActivity ActorIsNotValueExpression;
        private IfElseActivity IfActorIsNotValueExpression;
        private ComponentActivities.ResolveLookups ResolveForValue;
        private IfElseBranchActivity Standard;
        private IfElseBranchActivity Authorization;
        private IfElseActivity SwitchSubmissionType;
        private ComponentActivities.AsynchronousCreateResource AsyncCreate;
        private SequenceActivity ProcessCreate;
        private ReplicatorActivity ForEachIteration;
        private CodeActivity PrepareIteration;
        private ComponentActivities.ResolveLookups Resolve;
        private CodeActivity Finish;
        private IfElseBranchActivity ContentToPublish;
        private IfElseActivity IfContentToPublish;
        private CodeActivity PublishCreated;
        private ComponentActivities.UpdateLookups Publish;
        private ComponentActivities.ResolveQueries RunQueries;
        private ComponentActivities.FindResources FindConflict;
        private CodeActivity EvaluateResults;
        private IfElseBranchActivity CheckConflict;
        private IfElseActivity IfCheckConflict;
        private IfElseBranchActivity Unique;
        private IfElseActivity IfUnique;
        private CodeActivity ParseDefinitions;
        private CodeActivity PrepareCreate;
        private Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity Create;
    }
}
