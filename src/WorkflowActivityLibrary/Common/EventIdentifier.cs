//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="EventIdentifier.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// EventIdentifier class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// The event identifiers for various events
    /// The format of the event identifiers is event type (first digit), class (next two digits), method (next two digits)
    /// </summary>
    public static class EventIdentifier
    {
        #region "Default Events"

        /// <summary>
        /// The default event identifier for verbose events
        /// </summary>
        public const int Verbose = 10000;

        /// <summary>
        /// The default event identifier for informational events
        /// </summary>
        public const int Info = 20000;

        /// <summary>
        /// The default event identifier for warning events
        /// </summary>
        public const int Warning = 30000;

        /// <summary>
        /// The default event identifier for error events
        /// </summary>
        public const int Error = 40000;

        #endregion

        #region "Verbose Events"

        /// <summary>
        /// The event identifier for CreateResource constructor events
        /// </summary>
        public const int CreateResourceConstructor = 10101;

        /// <summary>
        /// The event identifier for CreateResource Execute events
        /// </summary>
        public const int CreateResourceExecute = 10102;

        /// <summary>
        /// The event identifier for CreateResource ParseDefinitions_ExecuteCode events
        /// </summary>
        public const int CreateResourceParseDefinitionsExecuteCode = 10103;

        /// <summary>
        /// The event identifier for CreateResource PrepareIteration_ExecuteCode events
        /// </summary>
        public const int CreateResourcePrepareIterationExecuteCode = 10104;

        /// <summary>
        /// The event identifier for CreateResource ForEachIteration_ChildInitialized events
        /// </summary>
        public const int CreateResourceForEachIterationChildInitialized = 10105;

        /// <summary>
        /// The event identifier for Create Resource EvaluateResults_ExecuteCode events
        /// </summary>
        public const int CreateResourceEvaluateResultsExecuteCode = 10106;

        /// <summary>
        /// The event identifier for Create Resource PrepareCreate_ExecuteCode events
        /// </summary>
        public const int CreateResourcePrepareCreateExecuteCode = 10107;

        /// <summary>
        /// The event identifier for CreateResource PublishCreated_ExecuteCode events
        /// </summary>
        public const int CreateResourcePublishCreatedExecuteCode = 10108;

        /// <summary>
        /// The event identifier for CreateResource Finish_ExecuteCode events
        /// </summary>
        public const int CreateResourceFinishExecuteCode = 10109;

        /// <summary>
        /// The event identifier for CreateResource CheckConflict_Condition events
        /// </summary>
        public const int CreateResourceCheckConflictCondition = 10110;

        /// <summary>
        /// The event identifier for CreateResource Unique_Condition events
        /// </summary>
        public const int CreateResourceUniqueCondition = 10111;

        /// <summary>
        /// The event identifier for CreateResource ContentToPublish_Condition events
        /// </summary>
        public const int CreateResourceContentToPublishCondition = 10112;

        /// <summary>
        /// The event identifier for CreateResource Authorization_Condition events
        /// </summary>
        public const int CreateResourceAuthorizationCondition = 10112;

        /// <summary>
        /// The event identifier for CreateResource ForEachIteration_ChildCompleted events
        /// </summary>
        public const int CreateResourceForEachIterationChildCompleted = 10113;

        /// <summary>
        /// The event identifier for CreateResource ForEachIteration_UntilCondition events
        /// </summary>
        public const int CreateResourceForEachIterationUntilCondition = 10114;

        /// <summary>
        /// The event identifier for CreateResource ActorIsNotValueExpression_Condition events
        /// </summary>
        public const int CreateResourceActorIsNotValueExpressionCondition = 10115;

        /// <summary>
        /// The event identifier for DeleteResources Constructor events
        /// </summary>
        public const int DeleteResourcesConstructor = 10201;

        /// <summary>
        /// The event identifier for DeleteResources Execute events
        /// </summary>
        public const int DeleteResourcesExecute = 10202;

        /// <summary>
        /// The event identifier for DeleteResources PrepareResolve_ExecuteCode events
        /// </summary>
        public const int DeleteResourcesPrepareResolveExecuteCode = 10203;

        /// <summary>
        /// The event identifier for DeleteResources PrepareIteration_ExecuteCode events
        /// </summary>
        public const int DeleteResourcesPrepareIterationExecuteCode = 10204;

        /// <summary>
        /// The event identifier for DeleteResources ForEachIteration_ChildInitialized events
        /// </summary>
        public const int DeleteResourcesForEachIterationChildInitialized = 10205;

        /// <summary>
        /// The event identifier for DeleteResources PrepareTarget_ExecuteCode events
        /// </summary>
        public const int DeleteResourcesPrepareTargetExecuteCode = 10206;

        /// <summary>
        /// The event identifier for DeleteResources PrepareDelete_ExecuteCode events
        /// </summary>
        public const int DeleteResourcesPrepareDeleteExecuteCode = 10207;

        /// <summary>
        /// The event identifier for DeleteResources ForEachTarget_ChildInitialized events
        /// </summary>
        public const int DeleteResourcesForEachTargetChildInitialized = 10208;

        /// <summary>
        /// The event identifier for DeleteResources SearchForTarget_Condition events
        /// </summary>
        public const int DeleteResourcesSearchForTargetCondition = 10209;

        /// <summary>
        /// The event identifier for DeleteResources ResolveTarget_Condition events
        /// </summary>
        public const int DeleteResourcesResolveTargetCondition = 10210;

        /// <summary>
        /// The event identifier for DeleteResources Authorization_Condition events
        /// </summary>
        public const int DeleteResourcesAuthorizationCondition = 10211;

        /// <summary>
        /// The event identifier for DeleteResources ActorIsNotValueExpression_Condition events
        /// </summary>
        public const int DeleteResourcesActorIsNotValueExpressionCondition = 10212;

        /// <summary>
        /// The event identifier for GenerateUniqueValue Constructor events
        /// </summary>
        public const int GenerateUniqueValueConstructor = 10301;

        /// <summary>
        /// The event identifier for GenerateUniqueValue Execute events
        /// </summary>
        public const int GenerateUniqueValueExecute = 10302;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ResolveUniquenessKey events
        /// </summary>
        public const int GenerateUniqueValueResolveUniquenessKey = 10303;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ResolveValueFilter events
        /// </summary>
        public const int GenerateUniqueValueResolveValueFilter = 10304;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ConflictExistsInLdap events
        /// </summary>
        public const int GenerateUniqueValueConflictExistsInLdap = 10305;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ParseExpressions_ExecuteCode events
        /// </summary>
        public const int GenerateUniqueValueParseExpressionsExecuteCode = 10306;

        /// <summary>
        /// The event identifier for GenerateUniqueValue Prepare_ExecuteCode events
        /// </summary>
        public const int GenerateUniqueValuePrepareExecuteCode = 10307;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ForEachLdap_ChildInitialized events
        /// </summary>
        public const int GenerateUniqueValueForEachLdapChildInitialized = 10308;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ForEachLdap_ChildCompleted events
        /// </summary>
        public const int GenerateUniqueValueForEachLdapChildCompleted = 10309;

        /// <summary>
        /// The event identifier for GenerateUniqueValue Decide_ExecuteCode events
        /// </summary>
        public const int GenerateUniqueValueDecideExecuteCode = 10310;

        /// <summary>
        /// The event identifier for GenerateUniqueValue WhileNotUnique_Condition events
        /// </summary>
        public const int GenerateUniqueValueWhileNotUniqueCondition = 10311;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ActivityExecutionConditionSatisfied_Condition events
        /// </summary>
        public const int GenerateUniqueValueActivityExecutionConditionSatisfiedCondition = 10312;

        /// <summary>
        /// The event identifier for GenerateUniqueValue SetAttributesToReadForConflictResources  events
        /// </summary>
        public const int GenerateUniqueValueSetAttributesToReadForConflictResources = 10313;

        /// <summary>
        /// The event identifier for GenerateUniqueValue CheckConflictResourceFound events
        /// </summary>
        public const int GenerateUniqueValueCheckConflictResourceFound = 10314;

        /// <summary>
        /// The event identifier for GenerateUniqueValue RepositionUniquenessKey events
        /// </summary>
        public const int GenerateUniqueValueRepositionUniquenessKey = 10315;

        /// <summary>
        /// The event identifier for RunPowerShellScript Constructor events
        /// </summary>
        public const int RunPowerShellScriptConstructor = 10401;

        /// <summary>
        /// The event identifier for RunPowerShellScript Execute events
        /// </summary>
        public const int RunPowerShellScriptExecute = 10402;

        /// <summary>
        /// The event identifier for RunPowerShellScript RunScript events
        /// </summary>
        public const int RunPowerShellScriptRunScript = 10403;

        /// <summary>
        /// The event identifier for RunPowerShellScript SetupStreamEventHandlers events
        /// </summary>
        public const int RunPowerShellScriptSetupStreamEventHandlers = 10404;

        /// <summary>
        /// The event identifier for RunPowerShellScript SetupStreamEventHandlers events
        /// </summary>
        public const int RunPowerShellScriptSetupStreamEventHandlersEvents = 10405;

        /// <summary>
        /// The event identifier for RunPowerShellScript Prepare_ExecuteCode events
        /// </summary>
        public const int RunPowerShellScriptPrepareExecuteCode = 10406;

        /// <summary>
        /// The event identifier for RunPowerShellScript Run_ExecuteCode events
        /// </summary>
        public const int RunPowerShellScriptRunExecuteCode = 10407;

        /// <summary>
        /// The event identifier for RunPowerShellScript ActivityExecutionConditionSatisfied_Condition events
        /// </summary>
        public const int RunPowerShellScriptActivityExecutionConditionSatisfiedCondition = 10408;

        /// <summary>
        /// The event identifier for UpdateResources Constructor events
        /// </summary>
        public const int UpdateResourcesConstructor = 10501;

        /// <summary>
        /// The event identifier for UpdateResources Execute events
        /// </summary>
        public const int UpdateResourcesExecute = 10502;

        /// <summary>
        /// The event identifier for UpdateResources Prepare_ExecuteCode events
        /// </summary>
        public const int UpdateResourcesPrepareExecuteCode = 10503;

        /// <summary>
        /// The event identifier for UpdateResources PrepareIteration_ExecuteCode events
        /// </summary>
        public const int UpdateResourcesPrepareIterationExecuteCode = 10504;

        /// <summary>
        /// The event identifier for UpdateResources ForEachIteration_ChildInitialized events
        /// </summary>
        public const int UpdateResourcesForEachIterationChildInitialized = 10505;

        /// <summary>
        /// The event identifier for UpdateResources PrepareUpdate_ExecuteCode events
        /// </summary>
        public const int UpdateResourcesPrepareUpdateExecuteCode = 10506;

        /// <summary>
        /// The event identifier for UpdateResources ForEachIteration_ChildCompleted events
        /// </summary>
        public const int UpdateResourcesForEachIterationChildCompleted = 10507;

        /// <summary>
        /// The event identifier for UpdateResources ForEachIteration_UntilCondition events
        /// </summary>
        public const int UpdateResourcesForEachIterationUntilCondition = 10508;

        /// <summary>
        /// The event identifier for UpdateResources ActorIsNotValueExpression_Condition events
        /// </summary>
        public const int UpdateResourcesActorIsNotValueExpressionCondition = 10509;

        /// <summary>
        /// The event identifier for UpdateResources DynamicGrammarResolutionNeed_Condition events
        /// </summary>
        public const int UpdateResourcesDynamicGrammarResolutionNeedCondition = 10510;

        /// <summary>
        /// The event identifier for UpdateResources ForEachDynamicStringForResolution_Initialized events
        /// </summary>
        public const int UpdateResourcesForEachDynamicStringForResolutionInitialized = 10511;

        /// <summary>
        /// The event identifier for UpdateResources ForEachDynamicStringForResolution_ChildInitialized events
        /// </summary>
        public const int UpdateResourcesForEachDynamicStringForResolutionChildInitialized = 10512;

        /// <summary>
        /// The event identifier for UpdateResources ForEachDynamicStringForResolution_ChildCompleted events
        /// </summary>
        public const int UpdateResourcesForEachDynamicStringForResolutionChildCompleted = 10513;

        /// <summary>
        /// The event identifier for VerifyRequest Constructor events
        /// </summary>
        public const int VerifyRequestConstructor = 10601;

        /// <summary>
        /// The event identifier for VerifyRequest Execute events
        /// </summary>
        public const int VerifyRequestExecute = 10602;

        /// <summary>
        /// The event identifier for VerifyRequest Prepare_ExecuteCode events
        /// </summary>
        public const int VerifyRequestPrepareExecuteCode = 10603;

        /// <summary>
        /// The event identifier for VerifyRequest CheckResource_ExecuteCode events
        /// </summary>
        public const int VerifyRequestCheckResourceExecuteCode = 10604;

        /// <summary>
        /// The event identifier for VerifyRequest CheckRequest_ExecuteCode events
        /// </summary>
        public const int VerifyRequestCheckRequestExecuteCode = 10605;

        /// <summary>
        /// The event identifier for VerifyRequest ParseConditions_ExecuteCode events
        /// </summary>
        public const int VerifyRequestParseConditionsExecuteCode = 10606;

        /// <summary>
        /// The event identifier for VerifyRequest EnforceConditions_ExecuteCode events
        /// </summary>
        public const int VerifyRequestEnforceConditionsExecuteCode = 10607;

        /// <summary>
        /// The event identifier for VerifyRequest DenyRequest_ExecuteCode events
        /// </summary>
        public const int VerifyRequestDenyRequestExecuteCode = 10608;

        /// <summary>
        /// The event identifier for VerifyRequest CheckConflict_Condition events
        /// </summary>
        public const int VerifyRequestCheckConflictCondition = 10609;

        /// <summary>
        /// The event identifier for VerifyRequest CheckRequestConflict_Condition events
        /// </summary>
        public const int VerifyRequestCheckRequestConflictCondition = 10610;

        /// <summary>
        /// The event identifier for VerifyRequest Unique_Condition events
        /// </summary>
        public const int VerifyRequestUniqueCondition = 10611;

        /// <summary>
        /// The event identifier for VerifyRequest DenialPending_Condition events
        /// </summary>
        public const int VerifyRequestDenialPendingCondition = 10612;

        /// <summary>
        /// The event identifier for VerifyRequest ActivityExecutionConditionSatisfied_Condition events
        /// </summary>
        public const int VerifyRequestActivityExecutionConditionSatisfiedCondition = 10613;

        /// <summary>
        /// The event identifier for AsynchronousCreateResource Constructor events
        /// </summary>
        public const int AsyncCreateResourceConstructor = 10701;

        /// <summary>
        /// The event identifier for AsynchronousCreateResource OnActivityExecutionContextLoad events
        /// </summary>
        public const int AsyncCreateResourceOnActivityExecutionContextLoad = 10702;

        /// <summary>
        /// The event identifier for AsynchronousCreateResource Execute events
        /// </summary>
        public const int AsyncCreateResourceExecute = 10703;

        /// <summary>
        /// The event identifier for AsynchronousCreateResource CreateWorkflowProgramQueue events
        /// </summary>
        public const int AsyncCreateResourceCreateWorkflowProgramQueue = 10704;

        /// <summary>
        /// The event identifier for AsynchronousCreateResource DeleteWorkflowProgramQueue events
        /// </summary>
        public const int AsyncCreateResourceDeleteWorkflowProgramQueue = 10705;

        /// <summary>
        /// The event identifier for AsynchronousDeleteResource Constructor events
        /// </summary>
        public const int AsyncDeleteResourceConstructor = 10801;

        /// <summary>
        /// The event identifier for AsynchronousDeleteResource OnActivityExecutionContextLoad events
        /// </summary>
        public const int AsyncDeleteResourceOnActivityExecutionContextLoad = 10802;

        /// <summary>
        /// The event identifier for AsynchronousDeleteResource Execute events
        /// </summary>
        public const int AsyncDeleteResourceExecute = 10803;

        /// <summary>
        /// The event identifier for AsynchronousDeleteResource CreateWorkflowProgramQueue events
        /// </summary>
        public const int AsyncDeleteResourceCreateWorkflowProgramQueue = 10804;

        /// <summary>
        /// The event identifier for AsynchronousDeleteResource DeleteWorkflowProgramQueue events
        /// </summary>
        public const int AsyncDeleteResourceDeleteWorkflowProgramQueue = 10805;

        /// <summary>
        /// The event identifier for AsynchronousUpdateResource Constructor events
        /// </summary>
        public const int AsyncUpdateResourceConstructor = 10901;

        /// <summary>
        /// The event identifier for AsynchronousUpdateResource OnActivityExecutionContextLoad events
        /// </summary>
        public const int AsyncUpdateResourceOnActivityExecutionContextLoad = 10902;

        /// <summary>
        /// The event identifier for AsynchronousUpdateResource Execute events
        /// </summary>
        public const int AsyncUpdateResourceExecute = 10903;

        /// <summary>
        /// The event identifier for AsynchronousUpdateResource CreateWorkflowProgramQueue events
        /// </summary>
        public const int AsyncUpdateResourceCreateWorkflowProgramQueue = 10904;

        /// <summary>
        /// The event identifier for AsynchronousUpdateResource DeleteWorkflowProgramQueue events
        /// </summary>
        public const int AsyncUpdateResourceDeleteWorkflowProgramQueue = 10905;

        /// <summary>
        /// The event identifier for DetermineActor Constructor events
        /// </summary>
        public const int DetermineActorConstructor = 11001;

        /// <summary>
        /// The event identifier for DetermineActor Execute events
        /// </summary>
        public const int DetermineActorExecute = 11002;

        /// <summary>
        /// The event identifier for DetermineActor PrepareResolveActor_ExecuteCode events
        /// </summary>
        public const int DetermineActorPrepareResolveActorExecuteCode = 11003;

        /// <summary>
        /// The event identifier for DetermineActor Decide_ExecuteCode events
        /// </summary>
        public const int DetermineActorDecideExecuteCode = 11004;

        /// <summary>
        /// The event identifier for DetermineActor ResolveActor_Condition events
        /// </summary>
        public const int DetermineActorResolveActorCondition = 11005;

        /// <summary>
        /// The event identifier for DetermineActor AccountActor_Condition events
        /// </summary>
        public const int DetermineActorAccountActorCondition = 11006;

        /// <summary>
        /// The event identifier for DetermineActor ActorIsNotSet_Condition events
        /// </summary>
        public const int DetermineActorActorIsNotSetCondition = 11007;

        /// <summary>
        /// The event identifier for DetermineActor PrepareAccountActor_ExecuteCode events
        /// </summary>
        public const int DetermineActorPrepareAccountActorExecuteCode = 11008;

        /// <summary>
        /// The event identifier for FindRequestConflict Constructor events
        /// </summary>
        public const int FindRequestConflictConstructor = 11101;

        /// <summary>
        /// The event identifier for FindRequestConflict Execute events
        /// </summary>
        public const int FindRequestConflictExecute = 11102;

        /// <summary>
        /// The event identifier for FindRequestConflict Prepare_ExecuteCode events
        /// </summary>
        public const int FindRequestConflictPrepareExecuteCode = 11103;

        /// <summary>
        /// The event identifier for FindRequestConflict ForEachRequest_ChildInitialized events
        /// </summary>
        public const int FindRequestConflictForEachRequestChildInitialized = 11104;

        /// <summary>
        /// The event identifier for FindRequestConflict ForEachRequest_ChildCompleted events
        /// </summary>
        public const int FindRequestConflictForEachRequestChildCompleted = 11105;

        /// <summary>
        /// The event identifier for FindResources Constructor events
        /// </summary>
        public const int FindResourcesConstructor = 11201;

        /// <summary>
        /// The event identifier for FindResources Execute events
        /// </summary>
        public const int FindResourcesExecute = 11202;

        /// <summary>
        /// The event identifier for FindResources Prepare_ExecuteCode events
        /// </summary>
        public const int FindResourcesPrepareExecuteCode = 11203;

        /// <summary>
        /// The event identifier for FindResources ReadFoundResource_ExecuteCode events
        /// </summary>
        public const int FindResourcesReadFoundResourceExecuteCode = 11204;

        /// <summary>
        /// The event identifier for FindResources ResolvedFilterIsNotNull_Condition events
        /// </summary>
        public const int FindResourcesResolvedFilterIsNotNullCondition = 11205;

        /// <summary>
        /// The event identifier for FindResources PrepareResolvedFilterList_ExecuteCode events
        /// </summary>
        public const int FindResourcesPrepareResolvedFilterListExecuteCode = 11206;

        /// <summary>
        /// The event identifier for FindResources ForEachResolvedFilter_ChildInitialized events
        /// </summary>
        public const int FindResourcesForEachResolvedFilterChildInitialized = 11207;

        /// <summary>
        /// The event identifier for ResolveLookups Constructor events
        /// </summary>
        public const int ResolveLookupsConstructor = 11301;

        /// <summary>
        /// The event identifier for ResolveLookups Execute events
        /// </summary>
        public const int ResolveLookupsExecute = 11302;

        /// <summary>
        /// The event identifier for ResolveLookups Prepare_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsPrepareExecuteCode = 11303;

        /// <summary>
        /// The event identifier for ResolveLookups ResolveComparedRequest_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsResolveComparedRequestExecuteCode = 11304;

        /// <summary>
        /// The event identifier for ResolveLookups ReadEnumeratedApproval_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsReadEnumeratedApprovalExecuteCode = 11305;

        /// <summary>
        /// The event identifier for ResolveLookups ForEachResponse_ChildInitialized events
        /// </summary>
        public const int ResolveLookupsForEachResponseChildInitialized = 11306;

        /// <summary>
        /// The event identifier for ResolveLookups ForEachResponse_ChildCompleted events
        /// </summary>
        public const int ResolveLookupsForEachResponseChildCompleted = 11307;

        /// <summary>
        /// The event identifier for ResolveLookups ResolveApprovers_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsResolveApproversExecuteCode = 11308;

        /// <summary>
        /// The event identifier for ResolveLookups ResolveRequestParameter_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsResolveRequestParameterExecuteCode = 11309;

        /// <summary>
        /// The event identifier for ResolveLookups ForEachRead_ChildInitialized events
        /// </summary>
        public const int ResolveLookupsForEachReadChildInitialized = 11310;

        /// <summary>
        /// The event identifier for ResolveLookups ForEachResource_ChildInitialized events
        /// </summary>
        public const int ResolveLookupsForEachResourceChildInitialized = 11311;

        /// <summary>
        /// The event identifier for ResolveLookups ForEachResource_ChildCompleted events
        /// </summary>
        public const int ResolveLookupsForEachResourceChildCompleted = 11312;

        /// <summary>
        /// The event identifier for ResolveLookups Publish events
        /// </summary>
        public const int ResolveLookupsPublish = 11313;

        /// <summary>
        /// The event identifier for ResolveLookups Resolve_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsResolveExecuteCode = 11314;

        /// <summary>
        /// The event identifier for ResolveLookups PublishRequestDelta events
        /// </summary>
        public const int ResolveLookupsPublishRequestDelta = 11315;

        /// <summary>
        /// The event identifier for ResolveLookups ComparedRequest_Condition events
        /// </summary>
        public const int ResolveLookupsComparedRequestCondition = 11316;

        /// <summary>
        /// The event identifier for ResolveLookups Approvers_Condition events
        /// </summary>
        public const int ResolveLookupsApproversCondition = 11317;

        /// <summary>
        /// The event identifier for ResolveLookups RequestParameter_Condition events
        /// </summary>
        public const int ResolveLookupsRequestParameterCondition = 11318;

        /// <summary>
        /// The event identifier for ResolveLookups PerformRead_Condition events
        /// </summary>
        public const int ResolveLookupsPerformReadCondition = 11319;

        /// <summary>
        /// The event identifier for ResolveLookups CompositeRequest_Condition events
        /// </summary>
        public const int ResolveLookupsCompositeRequestCondition = 11320;

        /// <summary>
        /// The event identifier for ResolveLookups ResolveParentRequestExecuteCode events
        /// </summary>
        public const int ResolveLookupsResolveParentRequestExecuteCode = 11321;

        /// <summary>
        /// The event identifier for ResolveLookupString Constructor events
        /// </summary>
        public const int ResolveLookupStringConstructor = 11401;

        /// <summary>
        /// The event identifier for ResolveLookupString Execute events
        /// </summary>
        public const int ResolveLookupStringExecute = 11402;

        /// <summary>
        /// The event identifier for ResolveLookupString GenerateLookupKey events
        /// </summary>
        public const int ResolveLookupStringGenerateLookupKey = 11403;

        /// <summary>
        /// The event identifier for ResolveLookupString Parse_ExecuteCode events
        /// </summary>
        public const int ResolveLookupStringParseExecuteCode = 11404;

        /// <summary>
        /// The event identifier for ResolveLookupString ResolveString_ExecuteCode events
        /// </summary>
        public const int ResolveLookupStringResolveStringExecuteCode = 11405;

        /// <summary>
        /// The event identifier for ResolveLookupString ParseLookupsStringForResolution events
        /// </summary>
        public const int ResolveLookupStringParseStringForResolutionLookups = 11406;

        /// <summary>
        /// The event identifier for ResolveLookupString ResolveStringForResolutionLookups events
        /// </summary>
        public const int ResolveLookupStringResolveStringForResolutionLookups = 11407;

        /// <summary>
        /// The event identifier for ExpressionEvaluator DetermineParameterType events
        /// </summary>
        public const int ExpressionEvaluatorDetermineParameterType = 11501;

        /// <summary>
        /// The event identifier for ExpressionEvaluator IsBooleanExpression events
        /// </summary>
        public const int ExpressionEvaluatorIsBooleanExpression = 11502;

        /// <summary>
        /// The event identifier for ExpressionEvaluator ParseExpression events
        /// </summary>
        public const int ExpressionEvaluatorParseExpression = 11503;

        /// <summary>
        /// The event identifier for ExpressionEvaluator ResolveExpression events
        /// </summary>
        public const int ExpressionEvaluatorResolveExpression = 11504;

        /// <summary>
        /// The event identifier for ExpressionEvaluator PublishVariable events
        /// </summary>
        public const int ExpressionEvaluatorPublishVariable = 11505;

        /// <summary>
        /// The event identifier for ExpressionEvaluator PublishVariable events
        /// </summary>
        public const int ExpressionEvaluatorIdentifyExpressionComponents = 11506;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EscapeString events
        /// </summary>
        public const int ExpressionEvaluatorEscapeString = 11507;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateExpression events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateExpression = 11508;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateExpressionComponent events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateExpressionComponent = 11509;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateExpressionComponent events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateFunction = 11510;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateExpressionComponent events
        /// </summary>
        public const int ExpressionEvaluatorValidateLookupParameterType = 11511;

        /// <summary>
        /// The event identifier for ExpressionEvaluator IsXPath events
        /// </summary>
        public const int ExpressionEvaluatorIsXPath = 11512;

        /// <summary>
        /// The event identifier for ExpressionEvaluator IsExpression events
        /// </summary>
        public const int ExpressionEvaluatorIsExpression = 11513;

        /// <summary>
        /// The event identifier for ExpressionEvaluator IsEmailAddress events
        /// </summary>
        public const int ExpressionEvaluatorIsEmailAddress = 11514;

        /// <summary>
        /// The event identifier for ExpressionEvaluator ParseIfExpression events
        /// </summary>
        public const int ExpressionEvaluatorParseIfExpression = 11515;

        /// <summary>
        /// The event identifier for ExpressionEvaluator IsValueExpression events
        /// </summary>
        public const int ExpressionEvaluatorIsValueExpression = 11516;

        /// <summary>
        /// The event identifier for ExpressionFunction Constructor events
        /// </summary>
        public const int ExpressionFunctionConstructor = 11601;

        /// <summary>
        /// The event identifier for ExpressionFunction Run events
        /// </summary>
        public const int ExpressionFunctionRun = 11602;

        /// <summary>
        /// The event identifier for ExpressionFunction VerifyType events
        /// </summary>
        public const int ExpressionFunctionVerifyType = 11603;

        /// <summary>
        /// The event identifier for ExpressionFunction After events
        /// </summary>
        public const int ExpressionFunctionAfter = 11604;

        /// <summary>
        /// The event identifier for ExpressionFunction And events
        /// </summary>
        public const int ExpressionFunctionAnd = 11605;

        /// <summary>
        /// The event identifier for ExpressionFunction Before events
        /// </summary>
        public const int ExpressionFunctionBefore = 11606;

        /// <summary>
        /// The event identifier for ExpressionFunction Contains events
        /// </summary>
        public const int ExpressionFunctionContains = 11607;

        /// <summary>
        /// The event identifier for ExpressionFunction Equal events
        /// </summary>
        public const int ExpressionFunctionEqual = 11608;

        /// <summary>
        /// The event identifier for ExpressionFunction GreaterThan events
        /// </summary>
        public const int ExpressionFunctionGreaterThan = 11609;

        /// <summary>
        /// The event identifier for ExpressionFunction IsPresent events
        /// </summary>
        public const int ExpressionFunctionIsPresent = 11610;

        /// <summary>
        /// The event identifier for ExpressionFunction LessThan events
        /// </summary>
        public const int ExpressionFunctionLessThan = 11611;

        /// <summary>
        /// The event identifier for ExpressionFunction Not events
        /// </summary>
        public const int ExpressionFunctionNot = 11612;

        /// <summary>
        /// The event identifier for ExpressionFunction Or events
        /// </summary>
        public const int ExpressionFunctionOr = 11613;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersContain events
        /// </summary>
        public const int ExpressionFunctionParametersContain = 11614;

        /// <summary>
        /// The event identifier for ExpressionFunction RegexMatch events
        /// </summary>
        public const int ExpressionFunctionRegexMatch = 11615;

        /// <summary>
        /// The event identifier for ExpressionFunction Add events
        /// </summary>
        public const int ExpressionFunctionAdd = 11616;

        /// <summary>
        /// The event identifier for ExpressionFunction ConcatenateMultivaluedString events
        /// </summary>
        public const int ExpressionFunctionConcatenateMultivaluedString = 11617;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToUniqueIdentifier events
        /// </summary>
        public const int ExpressionFunctionConvertToUniqueIdentifier = 11618;

        /// <summary>
        /// The event identifier for ExpressionFunction Count events
        /// </summary>
        public const int ExpressionFunctionCount = 11619;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeAdd events
        /// </summary>
        public const int ExpressionFunctionDateTimeAdd = 11620;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeFormat events
        /// </summary>
        public const int ExpressionFunctionDateTimeFormat = 11621;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeNow events
        /// </summary>
        public const int ExpressionFunctionDateTimeNow = 11622;

        /// <summary>
        /// The event identifier for ExpressionFunction First events
        /// </summary>
        public const int ExpressionFunctionFirst = 11623;

        /// <summary>
        /// The event identifier for ExpressionFunction GenerateRandomPassword events
        /// </summary>
        public const int ExpressionFunctionGenerateRandomPassword = 11624;

        /// <summary>
        /// The event identifier for ExpressionFunction IIF events
        /// </summary>
        public const int ExpressionFunctionIIF = 11625;

        /// <summary>
        /// The event identifier for ExpressionFunction InsertValues events
        /// </summary>
        public const int ExpressionFunctionInsertValues = 11626;

        /// <summary>
        /// The event identifier for ExpressionFunction Last events
        /// </summary>
        public const int ExpressionFunctionLast = 11627;

        /// <summary>
        /// The event identifier for ExpressionFunction Left events
        /// </summary>
        public const int ExpressionFunctionLeft = 11628;

        /// <summary>
        /// The event identifier for ExpressionFunction LeftPad events
        /// </summary>
        public const int ExpressionFunctionLeftPad = 11629;

        /// <summary>
        /// The event identifier for ExpressionFunction Length events
        /// </summary>
        public const int ExpressionFunctionLength = 11630;

        /// <summary>
        /// The event identifier for ExpressionFunction LowerCase events
        /// </summary>
        public const int ExpressionFunctionLowerCase = 11631;

        /// <summary>
        /// The event identifier for ExpressionFunction LTrim events
        /// </summary>
        public const int ExpressionFunctionLTrim = 11632;

        /// <summary>
        /// The event identifier for ExpressionFunction Mid events
        /// </summary>
        public const int ExpressionFunctionMid = 11633;

        /// <summary>
        /// The event identifier for ExpressionFunction Null events
        /// </summary>
        public const int ExpressionFunctionNull = 11634;

        /// <summary>
        /// The event identifier for ExpressionFunction ProperCase events
        /// </summary>
        public const int ExpressionFunctionProperCase = 11635;

        /// <summary>
        /// The event identifier for ExpressionFunction RandomNum events
        /// </summary>
        public const int ExpressionFunctionRandomNumber = 11636;

        /// <summary>
        /// The event identifier for ExpressionFunction RegexReplace events
        /// </summary>
        public const int ExpressionFunctionRegexReplace = 11637;

        /// <summary>
        /// The event identifier for ExpressionFunction RemoveValues events
        /// </summary>
        public const int ExpressionFunctionRemoveValues = 11638;

        /// <summary>
        /// The event identifier for ExpressionFunction ReplaceString events
        /// </summary>
        public const int ExpressionFunctionReplaceString = 11639;

        /// <summary>
        /// The event identifier for ExpressionFunction Right events
        /// </summary>
        public const int ExpressionFunctionRight = 11640;

        /// <summary>
        /// The event identifier for ExpressionFunction RightPad events
        /// </summary>
        public const int ExpressionFunctionRightPad = 11641;

        /// <summary>
        /// The event identifier for ExpressionFunction RTrim events
        /// </summary>
        public const int ExpressionFunctionRTrim = 11642;

        /// <summary>
        /// The event identifier for ExpressionFunction Trim events
        /// </summary>
        public const int ExpressionFunctionTrim = 11643;

        /// <summary>
        /// The event identifier for ExpressionFunction UpperCase events
        /// </summary>
        public const int ExpressionFunctionUpperCase = 11644;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByIndex events
        /// </summary>
        public const int ExpressionFunctionValueByIndex = 11645;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueType events
        /// </summary>
        public const int ExpressionFunctionValueType = 11646;

        /// <summary>
        /// The event identifier for ExpressionFunction Word events
        /// </summary>
        public const int ExpressionFunctionWord = 11647;

        /// <summary>
        /// The event identifier for ExpressionFunction WrapXPathFilter events
        /// </summary>
        public const int ExpressionFunctionWrapXPathFilter = 11648;

        /// <summary>
        /// The event identifier for ExpressionFunction NormalizeString events
        /// </summary>
        public const int ExpressionFunctionNormalizeString = 11649;

        /// <summary>
        /// The event identifier for ExpressionFunction BitAnd events
        /// </summary>
        public const int ExpressionFunctionBitAnd = 11650;

        /// <summary>
        /// The event identifier for ExpressionFunction BitOr events
        /// </summary>
        public const int ExpressionFunctionBitOr = 11651;

        /// <summary>
        /// The event identifier for ExpressionFunction Concatenate events
        /// </summary>
        public const int ExpressionFunctionConcatenate = 11652;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertSidToString events
        /// </summary>
        public const int ExpressionFunctionConvertSidToString = 11653;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertStringToGuid events
        /// </summary>
        public const int ExpressionFunctionConvertStringToGuid = 11654;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToBoolean events
        /// </summary>
        public const int ExpressionFunctionConvertToBoolean = 11655;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToNumber events
        /// </summary>
        public const int ExpressionFunctionConvertToNumber = 11656;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToString events
        /// </summary>
        public const int ExpressionFunctionConvertToString = 11657;

        /// <summary>
        /// The event identifier for ExpressionFunction CRLF events
        /// </summary>
        public const int ExpressionFunctionCrlf = 11658;

        /// <summary>
        /// The event identifier for ExpressionFunction EscapeDNComponent events
        /// </summary>
        public const int ExpressionFunctionEscapeDNComponent = 11659;

        /// <summary>
        /// The event identifier for ExpressionFunction SplitString events
        /// </summary>
        public const int ExpressionFunctionSplitString = 11660;

        /// <summary>
        /// The event identifier for ExpressionFunction RemoveDuplicates events
        /// </summary>
        public const int ExpressionFunctionRemoveDuplicates = 11661;

        /// <summary>
        /// The event identifier for ExpressionFunction BitNot events
        /// </summary>
        public const int ExpressionFunctionBitNot = 11662;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertFromBase64 events
        /// </summary>
        public const int ExpressionFunctionConvertFromBase64 = 11663;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToBase64 events
        /// </summary>
        public const int ExpressionFunctionConvertToBase64 = 11664;

        /// <summary>
        /// The event identifier for ExpressionFunction TitleCase events
        /// </summary>
        public const int ExpressionFunctionTitleCase = 11665;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeFromFileTimeUtc events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromFileTimeUtc = 11666;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeToFileTimeUtc events
        /// </summary>
        public const int ExpressionFunctionDateTimeToFileTimeUtc = 11667;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeSubtract events
        /// </summary>
        public const int ExpressionFunctionDateTimeSubtract = 11668;

        /// <summary>
        /// The event identifier for ExpressionFunction Subtract events
        /// </summary>
        public const int ExpressionFunctionSubtract = 11669;

        /// <summary>
        /// The event identifier for ExpressionFunction SortList events
        /// </summary>
        public const int ExpressionFunctionSortList = 11670;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersList events
        /// </summary>
        public const int ExpressionFunctionParametersList = 11671;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersValue events
        /// </summary>
        public const int ExpressionFunctionParameterValue = 11672;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueAdded events
        /// </summary>
        public const int ExpressionFunctionParameterValueAdded = 11673;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueRemoved events
        /// </summary>
        public const int ExpressionFunctionParameterValueRemoved = 11674;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersTable events
        /// </summary>
        public const int ExpressionFunctionParametersTable = 11675;

        /// <summary>
        /// The event identifier for ExpressionFunction FormatMultivaluedList events
        /// </summary>
        public const int ExpressionFunctionFormatMultivaluedList = 11676;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameter = 11677;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter2 events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameter2 = 11678;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlScalar events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlScalar = 11679;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlNonQuery events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlNonQuery = 11680;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByKey events
        /// </summary>
        public const int ExpressionFunctionValueByKey = 11681;

        /// <summary>
        /// The event identifier for ExpressionFunction DataTimeFromString events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromString = 11682;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertNumberToList events
        /// </summary>
        public const int ExpressionFunctionConvertNumberToList = 11683;

        /// <summary>
        /// The event identifier for ExpressionFunction Multiply events
        /// </summary>
        public const int ExpressionFunctionMultiply = 11684;

        /// <summary>
        /// The event identifier for ExpressionFunction Divide events
        /// </summary>
        public const int ExpressionFunctionDivide = 11685;

        /// <summary>
        /// The event identifier for ExpressionFunction Mod events
        /// </summary>
        public const int ExpressionFunctionMod = 11686;

        /// <summary>
        /// The event identifier for ExpressionFunction IndexByValue events
        /// </summary>
        public const int ExpressionFunctionIndexByValue = 11687;

        /// <summary>
        /// The event identifier for LookupEvaluator Constructor events
        /// </summary>
        public const int LookupEvaluatorConstructor = 11701;

        /// <summary>
        /// The event identifier for LookupEvaluator ValidateLookupGrammar events
        /// </summary>
        public const int LookupEvaluatorValidateLookupGrammar = 11702;

        /// <summary>
        /// The event identifier for LookupEvaluator ResolveLookupParameter events
        /// </summary>
        public const int LookupEvaluatorDetermineLookupParameter = 11703;

        /// <summary>
        /// The event identifier for LookupEvaluator GenerateRandomPassword events
        /// </summary>
        public const int PasswordGeneratorGenerateRandomPassword = 11801;

        /// <summary>
        /// The event identifier for LookupEvaluator GetRandomCharacter events
        /// </summary>
        public const int PasswordGeneratorGetRandomCharacter = 11802;

        /// <summary>
        /// The event identifier for ResolveQueries Constructor events
        /// </summary>
        public const int ResolveQueriesConstructor = 11901;

        /// <summary>
        /// The event identifier for ResolveQueries Execute events
        /// </summary>
        public const int ResolveQueriesExecute = 11902;

        /// <summary>
        /// The event identifier for ResolveQueries ForEachQuery_ChildInitialized events
        /// </summary>
        public const int ResolveQueriesForEachQueryChildInitialized = 11903;

        /// <summary>
        /// The event identifier for ResolveQueries ForEachQuery_ChildCompleted events
        /// </summary>
        public const int ResolveQueriesForEachQueryChildCompleted = 11904;

        /// <summary>
        /// The event identifier for ResolveQueries ForEachQuery_Initialized events
        /// </summary>
        public const int ResolveQueriesForEachQueryInitialized = 11905;

        /// <summary>
        /// The event identifier for UpdateLookups Constructor events
        /// </summary>
        public const int UpdateLookupsConstructor = 12001;

        /// <summary>
        /// The event identifier for UpdateLookups Execute events
        /// </summary>
        public const int UpdateLookupsExecute = 12002;

        /// <summary>
        /// The event identifier for UpdateLookups Prepare_ExecuteCode events
        /// </summary>
        public const int UpdateLookupsPrepareExecuteCode = 12003;

        /// <summary>
        /// The event identifier for UpdateLookups BuildRequests_ExecuteCode events
        /// </summary>
        public const int UpdateLookupsBuildRequestsExecuteCode = 12004;

        /// <summary>
        /// The event identifier for UpdateLookups ForEachPending_ChildInitialized events
        /// </summary>
        public const int UpdateLookupsForEachPendingChildInitialized = 12005;

        /// <summary>
        /// The event identifier for UpdateLookups ForEachPending_ChildCompleted events
        /// </summary>
        public const int UpdateLookupsForEachPendingChildCompleted = 12006;

        /// <summary>
        /// The event identifier for UpdateLookups PrepareUpdate_ExecuteCode events
        /// </summary>
        public const int UpdateLookupsPrepareUpdateExecuteCode = 12007;

        /// <summary>
        /// The event identifier for UpdateLookups ForEachRequest_ChildInitialized events
        /// </summary>
        public const int UpdateLookupsForEachRequestChildInitialized = 12008;

        /// <summary>
        /// The event identifier for UpdateLookups Authorization_Condition events
        /// </summary>
        public const int UpdateLookupsAuthorizationCondition = 12009;

        /// <summary>
        /// The event identifier for LogonUser Constructor events
        /// </summary>
        public const int LogOnUserConstructor = 12101;

        /// <summary>
        /// The event identifier for LogonUser Destructor events
        /// </summary>
        public const int LogOnUserDestructor = 12102;

        /// <summary>
        /// The event identifier for LogonUser Dispose events
        /// </summary>
        public const int LogOnUserDispose = 12103;

        /// <summary>
        /// The event identifier for LogonUser LogOn events
        /// </summary>
        public const int LogOnUserLogOn = 12104;

        /// <summary>
        /// The event identifier for SafeRegistryHandle Constructor events
        /// </summary>
        public const int SafeRegistryHandleConstructor = 12201;

        /// <summary>
        /// The event identifier for SafeRegistryHandle ReleaseHandle events
        /// </summary>
        public const int SafeRegistryHandleReleaseHandle = 12202;

        /// <summary>
        /// The event identifier for SafeTokenHandle Constructor events
        /// </summary>
        public const int SafeTokenHandleConstructor = 12301;

        /// <summary>
        /// The event identifier for SafeTokenHandle ReleaseHandle events
        /// </summary>
        public const int SafeTokenHandleReleaseHandle = 12302;

        /// <summary>
        /// The event identifier for ProtectedData GetCertificate events
        /// </summary>
        public const int ProtectedDataGetCertificate = 12401;

        /// <summary>
        /// The event identifier for ProtectedData GetCertificatePublicKeyXml events
        /// </summary>
        public const int ProtectedDataGetCertificatePublicKeyXml = 12402;

        /// <summary>
        /// The event identifier for ProtectedData EncryptData events
        /// </summary>
        public const int ProtectedDataEncryptData = 12403;

        /// <summary>
        /// The event identifier for ProtectedData DecryptData events
        /// </summary>
        public const int ProtectedDataDecryptData = 12404;

        /// <summary>
        /// The event identifier for ProtectedData ConvertToSecureString events
        /// </summary>
        public const int ProtectedDataConvertToSecureString = 12405;

        /// <summary>
        /// The event identifier for ProtectedData ConvertToUnsecureString events
        /// </summary>
        public const int ProtectedDataConvertToUnsecureString = 12406;

        /// <summary>
        /// The event identifier for ProtectedData DecryptDataUsingCertificate events
        /// </summary>
        public const int ProtectedDataDecryptDataUsingCertificate = 12407;

        /// <summary>
        /// The event identifier for DelayActivity Constructor events
        /// </summary>
        public const int AddDelayConstructor = 12501;

        /// <summary>
        /// The event identifier for DelayActivity Execute events
        /// </summary>
        public const int AddDelayExecute = 12502;

        /// <summary>
        /// The event identifier for DelayActivity ParseExpressions_ExecuteCode events
        /// </summary>
        public const int AddDelayParseExpressionsExecuteCode = 12503;

        /// <summary>
        /// The event identifier for DelayActivity ActivityExecutionConditionSatisfied_Condition events
        /// </summary>
        public const int AddDelayActivityExecutionConditionSatisfiedCondition = 12504;

        /// <summary>
        /// The event identifier for DelayActivity AddDelay_InitializeTimeoutDuration events
        /// </summary>
        public const int AddDelayDelayInitializeTimeoutDuration = 12505;

        /// <summary>
        /// The event identifier for DelayActivity TraceWakeup_ExecuteCode events
        /// </summary>
        public const int AddDelayTraceWakeupExecuteCode = 12506;

        /// <summary>
        /// The event identifier for SendEmailNotification Constructor events
        /// </summary>
        public const int SendEmailNotificationConstructor = 12601;

        /// <summary>
        /// The event identifier for SendEmailNotification Execute events
        /// </summary>
        public const int SendEmailNotificationExecute = 12602;

        /// <summary>
        /// The event identifier for SendEmailNotification ParseExpressions_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationParseExpressionsExecuteCode = 12603;

        /// <summary>
        /// The event identifier for SendEmailNotification ActivityExecutionConditionSatisfied_Condition events
        /// </summary>
        public const int SendEmailNotificationActivityExecutionConditionSatisfiedCondition = 12604;

        /// <summary>
        /// The event identifier for SendEmailNotification EmailTemplateIsXPath_Condition events
        /// </summary>
        public const int SendEmailNotificationEmailTemplateIsXPathCondition = 12605;

        /// <summary>
        /// The event identifier for SendEmailNotification CheckResource_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationCheckEmailTemplateResourceExecuteCode = 12606;

        /// <summary>
        /// The event identifier for SendEmailNotification ResolveEmailTemplate_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationResolveEmailTemplateExecuteCode = 12607;

        /// <summary>
        /// The event identifier for SendEmailNotification EmailToIsXPath_Condition events
        /// </summary>
        public const int SendEmailNotificationEmailToIsXPathCondition = 12608;

        /// <summary>
        /// The event identifier for SendEmailNotification CheckEmailToRecipientResources_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationCheckEmailToRecipientResourcesExecuteCode = 12609;

        /// <summary>
        /// The event identifier for SendEmailNotification ResolveEmailToRecipients_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationResolveEmailToRecipientsExecuteCode = 12610;

        /// <summary>
        /// The event identifier for SendEmailNotification EmailCcIsXPath_Condition events
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Cc", Justification = "Reviewed")]
        public const int SendEmailNotificationEmailCcIsXPathCondition = 12611;

        /// <summary>
        /// The event identifier for SendEmailNotification CheckEmailCcRecipientResources_ExecuteCode events
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Cc", Justification = "Reviewed")]
        public const int SendEmailNotificationCheckEmailCcRecipientResourcesExecuteCode = 12612;

        /// <summary>
        /// The event identifier for SendEmailNotification ResolveEmailCcRecipients_ExecuteCode events
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Cc", Justification = "Reviewed")]
        public const int SendEmailNotificationResolveEmailCcRecipientsExecuteCode = 12613;

        /// <summary>
        /// The event identifier for SendEmailNotification EmailBccIsXPath_Condition events
        /// </summary>
        public const int SendEmailNotificationEmailBccIsXPathCondition = 12614;

        /// <summary>
        /// The event identifier for SendEmailNotification CheckEmailBccRecipientResources_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationCheckEmailBccRecipientResourcesExecuteCode = 12615;

        /// <summary>
        /// The event identifier for SendEmailNotification ResolveEmailBccRecipients_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationResolveEmailBccRecipientsExecuteCode = 12616;

        /// <summary>
        /// The event identifier for SendEmailNotification ParseRecipient events
        /// </summary>
        public const int SendEmailNotificationParseRecipient = 12617;

        /// <summary>
        /// The event identifier for SendEmailNotification ParseEmailTemplate events
        /// </summary>
        public const int SendEmailNotificationParseEmailTemplate = 12618;

        /// <summary>
        /// The event identifier for SendEmailNotification FormatRecipient events
        /// </summary>
        public const int SendEmailNotificationFormatRecipient = 12619;

        /// <summary>
        /// The event identifier for SendEmailNotification TraceEmailProperties_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationTraceSendMailExecuteCode = 12620;

        /// <summary>
        /// The event identifier for SendEmailNotification IsXPath events
        /// </summary>
        public const int SendEmailNotificationIsXPath = 12621;

        /// <summary>
        /// The event identifier for SendEmailNotification IsExpression events
        /// </summary>
        public const int SendEmailNotificationIsExpression = 12622;

        /// <summary>
        /// The event identifier for SendEmailNotification IsEmailAddress events
        /// </summary>
        public const int SendEmailNotificationIsEmailAddress = 12623;

        /// <summary>
        /// The event identifier for SendEmailNotification GetEmailTemplateGuid events
        /// </summary>
        public const int SendEmailNotificationGetEmailTemplateGuid = 12624;

        /// <summary>
        /// The event identifier for SendEmailNotification PrepareIterationExecuteCode events
        /// </summary>
        public const int SendEmailNotificationPrepareIterationExecuteCode = 12625;

        /// <summary>
        /// The event identifier for SendEmailNotification ForEachIterationChildInitialized events
        /// </summary>
        public const int SendEmailNotificationForEachIterationChildInitialized = 12626;

        /// <summary>
        /// The event identifier for SendEmailNotification ForEachIterationChildCompleted events
        /// </summary>
        public const int SendEmailNotificationForEachIterationChildCompleted = 12627;

        /// <summary>
        /// The event identifier for SendEmailNotification ForEachIterationUntilCondition events
        /// </summary>
        public const int SendEmailNotificationForEachIterationUntilCondition = 12628;

        /// <summary>
        /// The event identifier for SendEmailNotification PrepareMailTemplate_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationPrepareUpdateExecuteCode = 12629;

        /// <summary>
        /// The event identifier for SendEmailNotification QueriesHaveNoValueExpressionsCondition events
        /// </summary>
        public const int SendEmailNotificationQueriesHaveNoValueExpressionsCondition = 12630;

        /// <summary>
        /// The event identifier for RequestApproval Constructor events
        /// </summary>
        public const int RequestApprovalConstructor = 12701;

        /// <summary>
        /// The event identifier for RequestApproval Execute events
        /// </summary>
        public const int RequestApprovalExecute = 12702;

        /// <summary>
        /// The event identifier for RequestApproval ParseExpressions_ExecuteCode events
        /// </summary>
        public const int RequestApprovalParseExpressionsExecuteCode = 12703;

        /// <summary>
        /// The event identifier for RequestApproval ActivityExecutionConditionSatisfied_Condition events
        /// </summary>
        public const int RequestApprovalActivityExecutionConditionSatisfiedCondition = 12704;

        /// <summary>
        /// The event identifier for RequestApproval ApproversIsXPath_Condition events
        /// </summary>
        public const int RequestApprovalApproversIsXPathCondition = 12705;

        /// <summary>
        /// The event identifier for RequestApproval CheckApproverResources_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApproverResourcesExecuteCode = 12706;

        /// <summary>
        /// The event identifier for RequestApproval ResolveApprovers_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveApproversExecuteCode = 12707;

        /// <summary>
        /// The event identifier for RequestApproval EscalationIsXPath_Condition events
        /// </summary>
        public const int RequestApprovalEscalationIsXPathCondition = 12708;

        /// <summary>
        /// The event identifier for RequestApproval CheckEscalationResources_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckEscalationResourcesExecuteCode = 12708;

        /// <summary>
        /// The event identifier for RequestApproval ResolveEscalation_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveEscalationExecuteCode = 12709;

        /// <summary>
        /// The event identifier for RequestApproval ApprovalEmailTemplateIsXPath_Condition events
        /// </summary>
        public const int RequestApprovalApprovalEmailTemplateIsXPathCondition = 12710;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalEmailTemplateResourceExecuteCode = 12711;

        /// <summary>
        /// The event identifier for RequestApproval ResolveApprovalEmailTemplate_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveApprovalEmailTemplateExecuteCode = 12712;

        /// <summary>
        /// The event identifier for RequestApproval EscalationEmailTemplateIsXPath_Condition events
        /// </summary>
        public const int RequestApprovalEscalationEmailTemplateIsXPathCondition = 12713;

        /// <summary>
        /// The event identifier for RequestApproval CheckEscalationEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckEscalationEmailTemplateResourceExecuteCode = 12714;

        /// <summary>
        /// The event identifier for RequestApproval ResolveEscalationEmailTemplate_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveEscalationEmailTemplateExecuteCode = 12715;

        /// <summary>
        /// The event identifier for RequestApproval ApprovalCompleteEmailTemplateIsXPath_Condition events
        /// </summary>
        public const int RequestApprovalApprovalCompleteEmailTemplateIsXPathCondition = 12716;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalCompleteEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalCompleteEmailTemplateResourceExecuteCode = 12717;

        /// <summary>
        /// The event identifier for RequestApproval ResolveApprovalCompleteEmailTemplate_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveApprovalCompleteEmailTemplateExecuteCode = 12718;

        /// <summary>
        /// The event identifier for RequestApproval ApprovalDeniedEmailTemplateIsXPath_Condition events
        /// </summary>
        public const int RequestApprovalApprovalDeniedEmailTemplateIsXPathCondition = 12719;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalDeniedEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalDeniedEmailTemplateResourceExecuteCode = 12720;

        /// <summary>
        /// The event identifier for RequestApproval ResolveApprovalDeniedEmailTemplate_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveApprovalDeniedEmailTemplateExecuteCode = 12721;

        /// <summary>
        /// The event identifier for RequestApproval ApprovalTimeoutEmailTemplateIsXPath_Condition events
        /// </summary>
        public const int RequestApprovalApprovalTimeoutEmailTemplateIsXPathCondition = 12722;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalTimeoutEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalTimeoutEmailTemplateResourceExecuteCode = 12723;

        /// <summary>
        /// The event identifier for RequestApproval ResolveApprovalTimeoutEmailTemplate_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveApprovalTimeoutEmailTemplateExecuteCode = 12724;

        /// <summary>
        /// The event identifier for RequestApproval ResolveThreshold_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveThresholdExecuteCode = 12725;

        /// <summary>
        /// The event identifier for RequestApproval ResolveDuration_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveDurationExecuteCode = 12726;

        /// <summary>
        /// The event identifier for RequestApproval TraceCreateApproval_ExecuteCode events
        /// </summary>
        public const int RequestApprovalTraceCreateApprovalExecuteCode = 12727;

        /// <summary>
        /// The event identifier for RequestApproval GetEmailTemplateGuid events
        /// </summary>
        public const int RequestApprovalGetEmailTemplateGuid = 12728;

        /// <summary>
        /// The event identifier for RequestApproval FormatRecipient events
        /// </summary>
        public const int RequestApprovalFormatRecipient = 12729;

        #endregion

        #region "Informational Events"

        /// <summary>
        /// The event identifier for RunPowerShellScript SetupStreamEventHandlers events
        /// </summary>
        public const int RunPowerShellScriptSetupStreamEventHandlersDebugEvents = 20405;

        /// <summary>
        /// The event identifier for RunPowerShellScript SetupStreamEventHandlers events
        /// </summary>
        public const int RunPowerShellScriptSetupStreamEventHandlersProgressEvents = 20405;

        #endregion

        #region "Warning Events"

        /// <summary>
        /// The event identifier for GenerateUniqueValue Decide_ExecuteCode events
        /// </summary>
        public const int GenerateUniqueValueDecideExecuteCodeLoopCountWarning = 30310;

        /// <summary>
        /// The event identifier for RunPowerShellScript SetupStreamEventHandlers events
        /// </summary>
        public const int RunPowerShellScriptSetupStreamEventHandlersWarningEvents = 30405;

        /// <summary>
        /// The event identifier for RunPowerShellScript Run_ExecuteCode events
        /// </summary>
        public const int RunPowerShellScriptRunExecuteCodeNullReturnValueWarning = 30407;

        /// <summary>
        /// The event identifier for RunPowerShellScript Run_ExecuteCode events
        /// </summary>
        public const int RunPowerShellScriptRunExecuteCodeNullHashtableReturnValueWarning = 30407;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateFunction events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateFunctionDeprecatedFunctionWarning = 31510;

        /// <summary>
        /// The event identifier for ExpressionFunction Run events
        /// </summary>
        public const int ExpressionFunctionRunDeprecatedFunctionWarning = 31602;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersContain events
        /// </summary>
        public const int ExpressionFunctionParametersContainWarning = 31614;

        /// <summary>
        /// The event identifier for LookupEvaluator GenerateRandomPassword events
        /// </summary>
        public const int PasswordGeneratorGenerateRandomPasswordWarning = 31801;

        /// <summary>
        /// The event identifier for SendEmailNotification CheckEmailToRecipientResources_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationCheckEmailToRecipientResourcesExecuteCodeWarning = 32609;

        /// <summary>
        /// The event identifier for SendEmailNotification ResolveEmailToRecipients_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationResolveEmailToRecipientsExecuteCodeWarning = 32610;

        #endregion

        #region "Error Events"

        /// <summary>
        /// The event identifier for CreateResource Execute events
        /// </summary>
        public const int CreateResourceExecuteError = 40102;

        /// <summary>
        /// The event identifier for CreateResource Finish_ExecuteCode events
        /// </summary>
        public const int CreateResourceFinishExecuteCodeFailedOnConflictError = 40109;

        /// <summary>
        /// The event identifier for DeleteResources Execute events
        /// </summary>
        public const int DeleteResourcesExecuteError = 40202;

        /// <summary>
        /// The event identifier for GenerateUniqueValue Execute events
        /// </summary>
        public const int GenerateUniqueValueExecuteError = 40302;

        /// <summary>
        /// The event identifier for GenerateUniqueValue ConflictExistsInLdap events
        /// </summary>
        public const int GenerateUniqueValueConflictExistsInLdapLdapError = 40305;

        /// <summary>
        /// The event identifier for GenerateUniqueValue Decide_ExecuteCode events
        /// </summary>
        public const int GenerateUniqueValueDecideExecuteCodeExceededMaxLoopCountError = 40310;

        /// <summary>
        /// The event identifier for RunPowerShellScript Execute events
        /// </summary>
        public const int RunPowerShellScriptExecuteError = 40402;

        /// <summary>
        /// The event identifier for RunPowerShellScript RunScript events
        /// </summary>
        public const int RunPowerShellScriptRunScriptInvocationError = 40403;

        /// <summary>
        /// The event identifier for RunPowerShellScript RunScript events
        /// </summary>
        public const int RunPowerShellScriptRunScriptInconsistentScriptReturnTypeError = 40403;

        /// <summary>
        /// The event identifier for RunPowerShellScript RunScript events
        /// </summary>
        public const int RunPowerShellScriptRunScriptExecutionFailedError = 40403;

        /// <summary>
        /// The event identifier for RunPowerShellScript SetupStreamEventHandlers events
        /// </summary>
        public const int RunPowerShellScriptSetupStreamEventHandlersErrorEvents = 40405;

        /// <summary>
        /// The event identifier for RunPowerShellScript Run_ExecuteCode events
        /// </summary>
        public const int RunPowerShellScriptRunExecuteCodeScriptNotFoundError = 40407;

        /// <summary>
        /// The event identifier for UpdateResources Execute events
        /// </summary>
        public const int UpdateResourcesExecuteError = 40502;

        /// <summary>
        /// The event identifier for VerifyRequest Execute events
        /// </summary>
        public const int VerifyRequestExecuteError = 40602;

        /// <summary>
        /// The event identifier for VerifyRequest DenyRequest_ExecuteCode events
        /// </summary>
        public const int VerifyRequestDenyRequestExecuteCodeDenialRequestError = 40608;

        /// <summary>
        /// The event identifier for AsynchronousCreateResource Execute events
        /// </summary>
        public const int AsyncCreateResourceExecuteError = 40703;

        /// <summary>
        /// The event identifier for AsynchronousDeleteResource Execute events
        /// </summary>
        public const int AsyncDeleteResourceExecuteError = 40803;

        /// <summary>
        /// The event identifier for AsynchronousUpdateResource Execute events
        /// </summary>
        public const int AsyncUpdateResourceExecuteError = 40903;

        /// <summary>
        /// The event identifier for DetermineActor Execute events
        /// </summary>
        public const int DetermineActorExecuteError = 41002;

        /// <summary>
        /// The event identifier for DetermineActor Decide_ExecuteCode events
        /// </summary>
        public const int DetermineActorDecideExecuteCodeNotFoundActorAccountError = 41004;

        /// <summary>
        /// The event identifier for DetermineActor Decide_ExecuteCode events
        /// </summary>
        public const int DetermineActorDecideExecuteCodeMultipleActorAccountsError = 41004;

        /// <summary>
        /// The event identifier for FindRequestConflict Execute events
        /// </summary>
        public const int FindRequestConflictExecuteError = 41102;

        /// <summary>
        /// The event identifier for FindResources Execute events
        /// </summary>
        public const int FindResourcesExecuteError = 41202;

        /// <summary>
        /// The event identifier for FindResources Prepare_ExecuteCode events
        /// </summary>
        public const int FindResourcesPrepareExecuteCodeMissingXPathError = 41203;

        /// <summary>
        /// The event identifier for ResolveLookups Execute events
        /// </summary>
        public const int ResolveLookupsExecuteError = 41302;

        /// <summary>
        /// The event identifier for ResolveLookups Prepare_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsPrepareExecuteCodeUnableToGetParentWorkflowError = 41303;

        /// <summary>
        /// The event identifier for ResolveLookups Resolve_ExecuteCode events
        /// </summary>
        public const int ResolveLookupsResolveExecuteCodeInconsistentTypeError = 41314;

        /// <summary>
        /// The event identifier for ResolveLookupString Execute events
        /// </summary>
        public const int ResolveLookupStringExecuteError = 41402;

        /// <summary>
        /// The event identifier for ResolveLookupString ResolveString_ExecuteCode events
        /// </summary>
        public const int ResolveLookupStringResolveStringExecuteCodeError = 41405;

        /// <summary>
        /// The event identifier for ExpressionEvaluator DetermineParameterType events
        /// </summary>
        public const int ExpressionEvaluatorDetermineParameterTypeValidationError = 41501;

        /// <summary>
        /// The event identifier for ExpressionEvaluator IsBooleanExpression events
        /// </summary>
        public const int ExpressionEvaluatorIsBooleanExpressionException = 41502;

        /// <summary>
        /// The event identifier for ExpressionEvaluator PublishVariable events
        /// </summary>
        public const int ExpressionEvaluatorPublishVariableInsertVariableError = 41505;

        /// <summary>
        /// The event identifier for ExpressionEvaluator PublishVariable events
        /// </summary>
        public const int ExpressionEvaluatorIdentifyExpressionComponentsQuotesValidationError = 41506;

        /// <summary>
        /// The event identifier for ExpressionEvaluator PublishVariable events
        /// </summary>
        public const int ExpressionEvaluatorIdentifyExpressionComponentsParenthesisValidationError = 41506;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EscapeString events
        /// </summary>
        public const int ExpressionEvaluatorEscapeStringQuotesValidationError = 41507;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateExpressionComponent events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateExpressionComponentLookupCacheValidationError = 41509;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateExpressionComponent events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateExpressionComponentVariableCacheValidationError = 41509;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateExpressionComponent events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateExpressionComponentParameterTypeValidationError = 41509;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateFunction events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateFunctionQuotesValidationError = 41510;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateFunction events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateFunctionParenthesisValidationError = 41510;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateFunction events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateFunctionLookupCacheValidationError = 41510;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateFunction events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateFunctionVariableCacheValidationError = 41510;

        /// <summary>
        /// The event identifier for ExpressionEvaluator EvaluateFunction events
        /// </summary>
        public const int ExpressionEvaluatorEvaluateFunctionParameterTypeValidationError = 41510;

        /// <summary>
        /// The event identifier for ExpressionEvaluator ValidateLookupParameterType events
        /// </summary>
        public const int ExpressionEvaluatorValidateLookupParameterTypeUnsupportedTypeError = 41511;

        /// <summary>
        /// The event identifier for ExpressionFunction Run events
        /// </summary>
        public const int ExpressionFunctionRunUnsupportedFunctionError = 41602;

        /// <summary>
        /// The event identifier for ExpressionFunction Run events
        /// </summary>
        public const int ExpressionFunctionRunFunctionSyntaxValidationError = 41602;

        /// <summary>
        /// The event identifier for ExpressionFunction Run events
        /// </summary>
        public const int ExpressionFunctionRunUnknownFunctionExecutionError = 41602;

        /// <summary>
        /// The event identifier for ExpressionFunction After events
        /// </summary>
        public const int ExpressionFunctionAfterInvalidFunctionParameterCountError = 41604;

        /// <summary>
        /// The event identifier for ExpressionFunction After events
        /// </summary>
        public const int ExpressionFunctionAfterInvalidFirstFunctionParameterTypeError = 41604;

        /// <summary>
        /// The event identifier for ExpressionFunction After events
        /// </summary>
        public const int ExpressionFunctionAfterInvalidSecondFunctionParameterTypeError = 41604;

        /// <summary>
        /// The event identifier for ExpressionFunction And events
        /// </summary>
        public const int ExpressionFunctionAndInvalidFunctionParameterCountError = 41605;

        /// <summary>
        /// The event identifier for ExpressionFunction And events
        /// </summary>
        public const int ExpressionFunctionAndInvalidFirstFunctionParameterTypeError = 41605;

        /// <summary>
        /// The event identifier for ExpressionFunction And events
        /// </summary>
        public const int ExpressionFunctionAndInvalidSecondFunctionParameterTypeError = 41605;

        /// <summary>
        /// The event identifier for ExpressionFunction Before events
        /// </summary>
        public const int ExpressionFunctionBeforeInvalidFunctionParameterCountError = 41606;

        /// <summary>
        /// The event identifier for ExpressionFunction Before events
        /// </summary>
        public const int ExpressionFunctionBeforeInvalidFirstFunctionParameterTypeError = 41606;

        /// <summary>
        /// The event identifier for ExpressionFunction Before events
        /// </summary>
        public const int ExpressionFunctionBeforeInvalidSecondFunctionParameterTypeError = 41606;

        /// <summary>
        /// The event identifier for ExpressionFunction Contains events
        /// </summary>
        public const int ExpressionFunctionContainsInvalidFunctionParameterCountError = 41607;

        /// <summary>
        /// The event identifier for ExpressionFunction Equal events
        /// </summary>
        public const int ExpressionFunctionEqualInvalidFunctionParameterCountError = 41608;

        /// <summary>
        /// The event identifier for ExpressionFunction Equal events
        /// </summary>
        public const int ExpressionFunctionEqualInvalidThirdFunctionParameterTypeError = 41608;

        /// <summary>
        /// The event identifier for ExpressionFunction Equal events
        /// </summary>
        public const int ExpressionFunctionEqualException = 41608;

        /// <summary>
        /// The event identifier for ExpressionFunction GreaterThan events
        /// </summary>
        public const int ExpressionFunctionGreaterThanInvalidFunctionParameterCountError = 41609;

        /// <summary>
        /// The event identifier for ExpressionFunction GreaterThan events
        /// </summary>
        public const int ExpressionFunctionGreaterThanInvalidFirstFunctionParameterTypeError = 41609;

        /// <summary>
        /// The event identifier for ExpressionFunction GreaterThan events
        /// </summary>
        public const int ExpressionFunctionGreaterThanInvalidSecondFunctionParameterTypeError = 41609;

        /// <summary>
        /// The event identifier for ExpressionFunction IsPresent events
        /// </summary>
        public const int ExpressionFunctionIsPresentInvalidFunctionParameterCountError = 41610;

        /// <summary>
        /// The event identifier for ExpressionFunction LessThan events
        /// </summary>
        public const int ExpressionFunctionLessThanInvalidFunctionParameterCountError = 41611;

        /// <summary>
        /// The event identifier for ExpressionFunction LessThan events
        /// </summary>
        public const int ExpressionFunctionLessThanInvalidFirstFunctionParameterTypeError = 41611;

        /// <summary>
        /// The event identifier for ExpressionFunction LessThan events
        /// </summary>
        public const int ExpressionFunctionLessThanInvalidSecondFunctionParameterTypeError = 41611;

        /// <summary>
        /// The event identifier for ExpressionFunction Not events
        /// </summary>
        public const int ExpressionFunctionNotInvalidFunctionParameterCountError = 41612;

        /// <summary>
        /// The event identifier for ExpressionFunction Not events
        /// </summary>
        public const int ExpressionFunctionNotInvalidFirstFunctionParameterTypeError = 41612;

        /// <summary>
        /// The event identifier for ExpressionFunction Or events
        /// </summary>
        public const int ExpressionFunctionOrInvalidFunctionParameterCountError = 41613;

        /// <summary>
        /// The event identifier for ExpressionFunction Or events
        /// </summary>
        public const int ExpressionFunctionOrInvalidFirstFunctionParameterTypeError = 41613;

        /// <summary>
        /// The event identifier for ExpressionFunction Or events
        /// </summary>
        public const int ExpressionFunctionOrInvalidSecondFunctionParameterTypeError = 41613;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersContain events
        /// </summary>
        public const int ExpressionFunctionParametersContainInvalidFunctionParameterCountError = 41614;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersContain events
        /// </summary>
        public const int ExpressionFunctionParametersContainNullFunctionParameterError = 41614;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersContain events
        /// </summary>
        public const int ExpressionFunctionParametersContainInvalidFirstFunctionParameterTypeError = 41614;

        /// <summary>
        /// The event identifier for ExpressionFunction RegexMatch events
        /// </summary>
        public const int ExpressionFunctionRegexMatchInvalidFunctionParameterCountError = 41615;

        /// <summary>
        /// The event identifier for ExpressionFunction RegexMatch events
        /// </summary>
        public const int ExpressionFunctionRegexMatchNullFunctionParameterError = 41615;

        /// <summary>
        /// The event identifier for ExpressionFunction Add events
        /// </summary>
        public const int ExpressionFunctionAddInvalidFunctionParameterCountError = 41616;

        /// <summary>
        /// The event identifier for ExpressionFunction Add events
        /// </summary>
        public const int ExpressionFunctionAddNullFunctionParameterError = 41616;

        /// <summary>
        /// The event identifier for ExpressionFunction Add events
        /// </summary>
        public const int ExpressionFunctionAddInvalidFirstFunctionParameterTypeError = 41616;

        /// <summary>
        /// The event identifier for ExpressionFunction Add events
        /// </summary>
        public const int ExpressionFunctionAddInvalidSecondFunctionParameterTypeError = 41616;

        /// <summary>
        /// The event identifier for ExpressionFunction ConcatenateMultivaluedString events
        /// </summary>
        public const int ExpressionFunctionConcatenateMultivaluedStringInvalidFunctionParameterCountError = 41617;

        /// <summary>
        /// The event identifier for ExpressionFunction ConcatenateMultivaluedString events
        /// </summary>
        public const int ExpressionFunctionConcatenateMultivaluedStringNullFunctionParameterError = 41617;

        /// <summary>
        /// The event identifier for ExpressionFunction ConcatenateMultivaluedString events
        /// </summary>
        public const int ExpressionFunctionConcatenateMultivaluedStringInvalidFirstFunctionParameterTypeError = 41617;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToUniqueIdentifier events
        /// </summary>
        public const int ExpressionFunctionConvertToUniqueIdentifierInvalidFunctionParameterCountError = 41618;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToUniqueIdentifier events
        /// </summary>
        public const int ExpressionFunctionConvertToUniqueIdentifierInvalidFirstFunctionParameterTypeError = 41618;

        /// <summary>
        /// The event identifier for ExpressionFunction Count events
        /// </summary>
        public const int ExpressionFunctionCountInvalidFunctionParameterCountError = 41619;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeAdd events
        /// </summary>
        public const int ExpressionFunctionDateTimeAddInvalidFunctionParameterCountError = 41620;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeAdd events
        /// </summary>
        public const int ExpressionFunctionDateTimeAddNullFunctionParameterError = 41620;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeAdd events
        /// </summary>
        public const int ExpressionFunctionDateTimeAddInvalidFirstFunctionParameterTypeError = 41620;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeAdd events
        /// </summary>
        public const int ExpressionFunctionDateTimeAddInvalidSecondFunctionParameterTypeError = 41620;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeFormat events
        /// </summary>
        public const int ExpressionFunctionDateTimeFormatInvalidFunctionParameterCountError = 41621;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeFormat events
        /// </summary>
        public const int ExpressionFunctionDateTimeFormatNullFunctionParameterError = 41621;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeFormat events
        /// </summary>
        public const int ExpressionFunctionDateTimeFormatInvalidFirstFunctionParameterTypeError = 41621;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeNow events
        /// </summary>
        public const int ExpressionFunctionDateTimeNowInvalidFunctionParameterCountError = 41622;

        /// <summary>
        /// The event identifier for ExpressionFunction First events
        /// </summary>
        public const int ExpressionFunctionFirstInvalidFunctionParameterCountError = 41623;

        /// <summary>
        /// The event identifier for ExpressionFunction GenerateRandomPassword events
        /// </summary>
        public const int ExpressionFunctionGenerateRandomPasswordInvalidFunctionParameterCountError = 41624;

        /// <summary>
        /// The event identifier for ExpressionFunction GenerateRandomPassword events
        /// </summary>
        public const int ExpressionFunctionGenerateRandomPasswordNullFunctionParameterError = 41624;

        /// <summary>
        /// The event identifier for ExpressionFunction GenerateRandomPassword events
        /// </summary>
        public const int ExpressionFunctionGenerateRandomPasswordInvalidFirstFunctionParameterTypeError = 41624;

        /// <summary>
        /// The event identifier for ExpressionFunction IIF events
        /// </summary>
        public const int ExpressionFunctionIIFInvalidFunctionParameterCountError = 41625;

        /// <summary>
        /// The event identifier for ExpressionFunction IIF events
        /// </summary>
        public const int ExpressionFunctionIIFInvalidFirstFunctionParameterTypeError = 41625;

        /// <summary>
        /// The event identifier for ExpressionFunction InsertValues events
        /// </summary>
        public const int ExpressionFunctionInsertValuesInvalidFunctionParameterCountError = 41626;

        /// <summary>
        /// The event identifier for ExpressionFunction Last events
        /// </summary>
        public const int ExpressionFunctionLastInvalidFunctionParameterCountError = 41627;

        /// <summary>
        /// The event identifier for ExpressionFunction Left events
        /// </summary>
        public const int ExpressionFunctionLeftInvalidFunctionParameterCountError = 41628;

        /// <summary>
        /// The event identifier for ExpressionFunction Left events
        /// </summary>
        public const int ExpressionFunctionLeftNullFunctionParameterError = 41628;

        /// <summary>
        /// The event identifier for ExpressionFunction Left events
        /// </summary>
        public const int ExpressionFunctionLeftInvalidSecondFunctionParameterTypeError = 41628;

        /// <summary>
        /// The event identifier for ExpressionFunction LeftPad events
        /// </summary>
        public const int ExpressionFunctionLeftPadInvalidFunctionParameterCountError = 41629;

        /// <summary>
        /// The event identifier for ExpressionFunction LeftPad events
        /// </summary>
        public const int ExpressionFunctionLeftPadNullFunctionParameterError = 41629;

        /// <summary>
        /// The event identifier for ExpressionFunction LeftPad events
        /// </summary>
        public const int ExpressionFunctionLeftPadInvalidSecondFunctionParameterTypeError = 41629;

        /// <summary>
        /// The event identifier for ExpressionFunction LeftPad events
        /// </summary>
        public const int ExpressionFunctionLeftPadInvalidThirdFunctionParameterTypeError = 41629;

        /// <summary>
        /// The event identifier for ExpressionFunction Length events
        /// </summary>
        public const int ExpressionFunctionLengthInvalidFunctionParameterCountError = 41630;

        /// <summary>
        /// The event identifier for ExpressionFunction LowerCase events
        /// </summary>
        public const int ExpressionFunctionLowerCaseInvalidFunctionParameterCountError = 41631;

        /// <summary>
        /// The event identifier for ExpressionFunction LTrim events
        /// </summary>
        public const int ExpressionFunctionLTrimInvalidFunctionParameterCountError = 41632;

        /// <summary>
        /// The event identifier for ExpressionFunction Mid events
        /// </summary>
        public const int ExpressionFunctionMidInvalidFunctionParameterCountError = 41633;

        /// <summary>
        /// The event identifier for ExpressionFunction Mid events
        /// </summary>
        public const int ExpressionFunctionMidNullFunctionParameterError = 41633;

        /// <summary>
        /// The event identifier for ExpressionFunction Mid events
        /// </summary>
        public const int ExpressionFunctionMidInvalidSecondFunctionParameterTypeError = 41633;

        /// <summary>
        /// The event identifier for ExpressionFunction Mid events
        /// </summary>
        public const int ExpressionFunctionMidInvalidThirdFunctionParameterTypeError = 41633;

        /// <summary>
        /// The event identifier for ExpressionFunction Null events
        /// </summary>
        public const int ExpressionFunctionNullInvalidFunctionParameterCountError = 41634;

        /// <summary>
        /// The event identifier for ExpressionFunction ProperCase events
        /// </summary>
        public const int ExpressionFunctionProperCaseInvalidFunctionParameterCountError = 41635;

        /// <summary>
        /// The event identifier for ExpressionFunction RandomNum events
        /// </summary>
        public const int ExpressionFunctionRandomNumberInvalidFunctionParameterCountError = 41636;

        /// <summary>
        /// The event identifier for ExpressionFunction RandomNum events
        /// </summary>
        public const int ExpressionFunctionRandomNumberNullFunctionParameterError = 41636;

        /// <summary>
        /// The event identifier for ExpressionFunction RandomNum events
        /// </summary>
        public const int ExpressionFunctionRandomNumberInvalidFirstFunctionParameterTypeError = 41636;

        /// <summary>
        /// The event identifier for ExpressionFunction RandomNum events
        /// </summary>
        public const int ExpressionFunctionRandomNumberInvalidSecondFunctionParameterTypeError = 41636;

        /// <summary>
        /// The event identifier for ExpressionFunction RegexReplace events
        /// </summary>
        public const int ExpressionFunctionRegexReplaceInvalidFunctionParameterCountError = 41637;

        /// <summary>
        /// The event identifier for ExpressionFunction RegexReplace events
        /// </summary>
        public const int ExpressionFunctionRegexReplaceNullFunctionParameterError = 41637;

        /// <summary>
        /// The event identifier for ExpressionFunction RemoveValues events
        /// </summary>
        public const int ExpressionFunctionRemoveValuesInvalidFunctionParameterCountError = 41638;

        /// <summary>
        /// The event identifier for ExpressionFunction ReplaceString events
        /// </summary>
        public const int ExpressionFunctionReplaceStringInvalidFunctionParameterCountError = 41639;

        /// <summary>
        /// The event identifier for ExpressionFunction ReplaceString events
        /// </summary>
        public const int ExpressionFunctionReplaceStringNullFunctionParameterError = 41639;

        /// <summary>
        /// The event identifier for ExpressionFunction Right events
        /// </summary>
        public const int ExpressionFunctionRightInvalidFunctionParameterCountError = 41640;

        /// <summary>
        /// The event identifier for ExpressionFunction Right events
        /// </summary>
        public const int ExpressionFunctionRightNullFunctionParameterError = 41640;

        /// <summary>
        /// The event identifier for ExpressionFunction Right events
        /// </summary>
        public const int ExpressionFunctionRightInvalidSecondFunctionParameterTypeError = 41640;

        /// <summary>
        /// The event identifier for ExpressionFunction RightPad events
        /// </summary>
        public const int ExpressionFunctionRightPadInvalidFunctionParameterCountError = 41641;

        /// <summary>
        /// The event identifier for ExpressionFunction RightPad events
        /// </summary>
        public const int ExpressionFunctionRightPadNullFunctionParameterError = 41641;

        /// <summary>
        /// The event identifier for ExpressionFunction RightPad events
        /// </summary>
        public const int ExpressionFunctionRightPadInvalidSecondFunctionParameterTypeError = 41641;

        /// <summary>
        /// The event identifier for ExpressionFunction RightPad events
        /// </summary>
        public const int ExpressionFunctionRightPadInvalidThirdFunctionParameterTypeError = 41641;

        /// <summary>
        /// The event identifier for ExpressionFunction RTrim events
        /// </summary>
        public const int ExpressionFunctionRTrimInvalidFunctionParameterCountError = 41642;

        /// <summary>
        /// The event identifier for ExpressionFunction Trim events
        /// </summary>
        public const int ExpressionFunctionTrimInvalidFunctionParameterCountError = 41643;

        /// <summary>
        /// The event identifier for ExpressionFunction UpperCase events
        /// </summary>
        public const int ExpressionFunctionUpperCaseInvalidFunctionParameterCountError = 41644;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByIndex events
        /// </summary>
        public const int ExpressionFunctionValueByIndexInvalidFunctionParameterCountError = 41645;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByIndex events
        /// </summary>
        public const int ExpressionFunctionValueByIndexNullFunctionParameterError = 41645;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByIndex events
        /// </summary>
        public const int ExpressionFunctionValueByIndexInvalidSecondFunctionParameterTypeError = 41645;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueType events
        /// </summary>
        public const int ExpressionFunctionValueTypeInvalidFunctionParameterCountError = 41646;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueType events
        /// </summary>
        public const int ExpressionFunctionValueTypeNullFunctionParameterError = 41646;

        /// <summary>
        /// The event identifier for ExpressionFunction Word events
        /// </summary>
        public const int ExpressionFunctionWordInvalidFunctionParameterCountError = 41647;

        /// <summary>
        /// The event identifier for ExpressionFunction Word events
        /// </summary>
        public const int ExpressionFunctionWordNullFunctionParameterError = 41647;

        /// <summary>
        /// The event identifier for ExpressionFunction Word events
        /// </summary>
        public const int ExpressionFunctionWordInvalidSecondFunctionParameterTypeError = 41647;

        /// <summary>
        /// The event identifier for ExpressionFunction Word events
        /// </summary>
        public const int ExpressionFunctionWordInvalidThirdFunctionParameterTypeError = 41647;

        /// <summary>
        /// The event identifier for ExpressionFunction WrapXPathFilter events
        /// </summary>
        public const int ExpressionFunctionWrapXPathFilterInvalidFunctionParameterCountError = 41648;

        /// <summary>
        /// The event identifier for ExpressionFunction NormalizeString events
        /// </summary>
        public const int ExpressionFunctionNormalizeStringInvalidFunctionParameterCountError = 41649;

        /// <summary>
        /// The event identifier for ExpressionFunction NormalizeString events
        /// </summary>
        public const int ExpressionFunctionNormalizeStringNullFunctionParameterError = 41649;

        /// <summary>
        /// The event identifier for ExpressionFunction BitAnd events
        /// </summary>
        public const int ExpressionFunctionBitAndInvalidFunctionParameterCountError = 41650;

        /// <summary>
        /// The event identifier for ExpressionFunction BitAnd events
        /// </summary>
        public const int ExpressionFunctionBitAndNullFunctionParameterError = 41650;

        /// <summary>
        /// The event identifier for ExpressionFunction BitAnd events
        /// </summary>
        public const int ExpressionFunctionBitAndInvalidFirstFunctionParameterTypeError = 41650;

        /// <summary>
        /// The event identifier for ExpressionFunction BitAnd events
        /// </summary>
        public const int ExpressionFunctionBitAndInvalidSecondFunctionParameterTypeError = 41650;

        /// <summary>
        /// The event identifier for ExpressionFunction BitOr events
        /// </summary>
        public const int ExpressionFunctionBitOrInvalidFunctionParameterCountError = 41651;

        /// <summary>
        /// The event identifier for ExpressionFunction BitOr events
        /// </summary>
        public const int ExpressionFunctionBitOrNullFunctionParameterError = 41651;

        /// <summary>
        /// The event identifier for ExpressionFunction BitOr events
        /// </summary>
        public const int ExpressionFunctionBitOrInvalidFirstFunctionParameterTypeError = 41651;

        /// <summary>
        /// The event identifier for ExpressionFunction BitOr events
        /// </summary>
        public const int ExpressionFunctionBitOrInvalidSecondFunctionParameterTypeError = 41651;

        /// <summary>
        /// The event identifier for ExpressionFunction Concatenate events
        /// </summary>
        public const int ExpressionFunctionConcatenateInvalidFunctionParameterCountError = 41652;

        /// <summary>
        /// The event identifier for ExpressionFunction Concatenate events
        /// </summary>
        public const int ExpressionFunctionConcatenateInvalidFunctionParameterTypeError = 41652;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertSidToString events
        /// </summary>
        public const int ExpressionFunctionConvertSidToStringInvalidFunctionParameterCountError = 41653;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertSidToString events
        /// </summary>
        public const int ExpressionFunctionConvertSidToStringNullFunctionParameterError = 41653;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertSidToString events
        /// </summary>
        public const int ExpressionFunctionConvertSidToStringInvalidFirstFunctionParameterTypeError = 41653;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertStringToGuid events
        /// </summary>
        public const int ExpressionFunctionConvertStringToGuidInvalidFunctionParameterCountError = 41654;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertStringToGuid events
        /// </summary>
        public const int ExpressionFunctionConvertStringToGuidNullFunctionParameterError = 41654;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertStringToGuid events
        /// </summary>
        public const int ExpressionFunctionConvertStringToGuidInvalidFirstFunctionParameterTypeError = 41654;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToBoolean events
        /// </summary>
        public const int ExpressionFunctionConvertToBooleanInvalidFunctionParameterCountError = 41655;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToNumber events
        /// </summary>
        public const int ExpressionFunctionConvertToNumberInvalidFunctionParameterCountError = 41656;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToNumber events
        /// </summary>
        public const int ExpressionFunctionConvertToNumberNullFunctionParameterError = 41656;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToNumber events
        /// </summary>
        public const int ExpressionFunctionConvertToNumberInvalidFirstFunctionParameterTypeError = 41656;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToString events
        /// </summary>
        public const int ExpressionFunctionConvertToStringInvalidFunctionParameterCountError = 41657;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToString events
        /// </summary>
        public const int ExpressionFunctionConvertToStringNullFunctionParameterError = 41657;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToString events
        /// </summary>
        public const int ExpressionFunctionConvertToStringInvalidFirstFunctionParameterTypeError = 41657;

        /// <summary>
        /// The event identifier for ExpressionFunction CRLF events
        /// </summary>
        public const int ExpressionFunctionCrlfInvalidFunctionParameterCountError = 41658;

        /// <summary>
        /// The event identifier for ExpressionFunction EscapeDNComponent events
        /// </summary>
        public const int ExpressionFunctionEscapeDNComponentInvalidFunctionParameterCountError = 41659;

        /// <summary>
        /// The event identifier for ExpressionFunction EscapeDNComponent events
        /// </summary>
        public const int ExpressionFunctionEscapeDNComponentNullFunctionParameterError = 41659;

        /// <summary>
        /// The event identifier for ExpressionFunction EscapeDNComponent events
        /// </summary>
        public const int ExpressionFunctionEscapeDNComponentInvalidFirstFunctionParameterTypeError = 41659;

        /// <summary>
        /// The event identifier for ExpressionFunction EscapeDNComponent events
        /// </summary>
        public const int ExpressionFunctionEscapeDNComponentInvalidFunctionParameterError = 41659;

        /// <summary>
        /// The event identifier for ExpressionFunction SplitString events
        /// </summary>
        public const int ExpressionFunctionSplitStringInvalidFunctionParameterCountError = 41660;

        /// <summary>
        /// The event identifier for ExpressionFunction SplitString events
        /// </summary>
        public const int ExpressionFunctionSplitStringNullFunctionParameterError = 41660;

        /// <summary>
        /// The event identifier for ExpressionFunction SplitString events
        /// </summary>
        public const int ExpressionFunctionSplitStringInvalidFirstFunctionParameterTypeError = 41660;

        /// <summary>
        /// The event identifier for ExpressionFunction RemoveDuplicates events
        /// </summary>
        public const int ExpressionFunctionRemoveDuplicatesInvalidFunctionParameterCountError = 41661;

        /// <summary>
        /// The event identifier for ExpressionFunction RemoveDuplicates events
        /// </summary>
        public const int ExpressionFunctionRemoveDuplicatesNullFunctionParameterError = 41661;

        /// <summary>
        /// The event identifier for ExpressionFunction RemoveDuplicates events
        /// </summary>
        public const int ExpressionFunctionRemoveDuplicatesInvalidFirstFunctionParameterTypeError = 41661;

        /// <summary>
        /// The event identifier for ExpressionFunction BitNot events
        /// </summary>
        public const int ExpressionFunctionBitNotInvalidFunctionParameterCountError = 41662;

        /// <summary>
        /// The event identifier for ExpressionFunction BitNot events
        /// </summary>
        public const int ExpressionFunctionBitNotInvalidFirstFunctionParameterTypeError = 41662;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertFromBase64 events
        /// </summary>
        public const int ExpressionFunctionConvertFromBase64InvalidFunctionParameterCountError = 41663;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertFromBase64 events
        /// </summary>
        public const int ExpressionFunctionConvertFromBase64NullFunctionParameterError = 41663;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertFromBase64 events
        /// </summary>
        public const int ExpressionFunctionConvertFromBase64InvalidFirstFunctionParameterTypeError = 41663;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToBase64 events
        /// </summary>
        public const int ExpressionFunctionConvertToBase64InvalidFunctionParameterCountError = 41664;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToBase64 events
        /// </summary>
        public const int ExpressionFunctionConvertToBase64NullFunctionParameterError = 41664;

        /// <summary>
        /// The event identifier for ExpressionFunction TitleCase events
        /// </summary>
        public const int ExpressionFunctionConvertToBase64InvalidFirstFunctionParameterTypeError = 41664;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertToBase64 events
        /// </summary>
        public const int ExpressionFunctionTitleCaseInvalidFunctionParameterCountError = 41665;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeFromFileTimeUtc events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromFileTimeUtcInvalidFunctionParameterCountError = 41666;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeFromFileTimeUtc events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromFileTimeUtcInvalidFirstFunctionParameterTypeError = 41666;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeToFileTimeUtc events
        /// </summary>
        public const int ExpressionFunctionDateTimeToFileTimeUtcInvalidFunctionParameterCountError = 41667;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeToFileTimeUtc events
        /// </summary>
        public const int ExpressionFunctionDateTimeToFileTimeUtcInvalidFirstFunctionParameterTypeError = 41667;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeSubtract events
        /// </summary>
        public const int ExpressionFunctionDateTimeSubtractInvalidFunctionParameterCountError = 41668;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeSubtract events
        /// </summary>
        public const int ExpressionFunctionDateTimeSubtractNullFunctionParameterError = 41668;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeSubtract events
        /// </summary>
        public const int ExpressionFunctionDateTimeSubtractInvalidFirstFunctionParameterTypeError = 41668;

        /// <summary>
        /// The event identifier for ExpressionFunction DateTimeSubtract events
        /// </summary>
        public const int ExpressionFunctionDateTimeSubtractInvalidSecondFunctionParameterTypeError = 41668;

        /// <summary>
        /// The event identifier for ExpressionFunction Subtract events
        /// </summary>
        public const int ExpressionFunctionSubtractInvalidFunctionParameterCountError = 41669;

        /// <summary>
        /// The event identifier for ExpressionFunction Subtract events
        /// </summary>
        public const int ExpressionFunctionSubtractNullFunctionParameterError = 41669;

        /// <summary>
        /// The event identifier for ExpressionFunction Subtract events
        /// </summary>
        public const int ExpressionFunctionSubtractInvalidFirstFunctionParameterTypeError = 41669;

        /// <summary>
        /// The event identifier for ExpressionFunction Subtract events
        /// </summary>
        public const int ExpressionFunctionSubtractInvalidSecondFunctionParameterTypeError = 41669;

        /// <summary>
        /// The event identifier for ExpressionFunction SortList events
        /// </summary>
        public const int ExpressionFunctionSortListInvalidFunctionParameterCountError = 41670;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersList events
        /// </summary>
        public const int ExpressionFunctionParametersListInvalidFunctionParameterCountError = 41671;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersList events
        /// </summary>
        public const int ExpressionFunctionParametersListInvalidFirstFunctionParameterTypeError = 41671;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersValue events
        /// </summary>
        public const int ExpressionFunctionParameterValueInvalidFunctionParameterCountError = 41672;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersValue events
        /// </summary>
        public const int ExpressionFunctionParameterValueNullFunctionParameterError = 41672;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersValue events
        /// </summary>
        public const int ExpressionFunctionParameterValueInvalidFirstFunctionParameterTypeError = 41672;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersValue events
        /// </summary>
        public const int ExpressionFunctionParameterValueInvalidFunctionUseError = 41672;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueAdded events
        /// </summary>
        public const int ExpressionFunctionParameterValueAddedInvalidFunctionParameterCountError = 41673;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueAdded events
        /// </summary>
        public const int ExpressionFunctionParameterValueAddedNullFunctionParameterError = 41673;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueAdded events
        /// </summary>
        public const int ExpressionFunctionParameterValueAddedInvalidFirstFunctionParameterTypeError = 41673;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueAdded events
        /// </summary>
        public const int ExpressionFunctionParameterValueAddedInvalidFunctionUseError = 41673;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueRemoved events
        /// </summary>
        public const int ExpressionFunctionParameterValueRemovedInvalidFunctionParameterCountError = 41674;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueRemoved events
        /// </summary>
        public const int ExpressionFunctionParameterValueRemovedNullFunctionParameterError = 41674;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueRemoved events
        /// </summary>
        public const int ExpressionFunctionParameterValueRemovedInvalidFirstFunctionParameterTypeError = 41674;

        /// <summary>
        /// The event identifier for ExpressionFunction ParameterValueRemoved events
        /// </summary>
        public const int ExpressionFunctionParameterValueRemovedInvalidFunctionUseError = 41674;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersTable events
        /// </summary>
        public const int ExpressionFunctionParametersTableInvalidFunctionParameterCountError = 41675;

        /// <summary>
        /// The event identifier for ExpressionFunction ParametersTable events
        /// </summary>
        public const int ExpressionFunctionParametersTableInvalidFirstFunctionParameterTypeError = 41675;

        /// <summary>
        /// The event identifier for ExpressionFunction FormatMultivaluedList events
        /// </summary>
        public const int ExpressionFunctionExpressionFunctionFormatMultivaluedListInvalidFunctionParameterCountError = 41676;

        /// <summary>
        /// The event identifier for ExpressionFunction FormatMultivaluedList events
        /// </summary>
        public const int ExpressionFunctionFormatMultivaluedListInvalidFunctionParameterCountError = 41676;

        /// <summary>
        /// The event identifier for ExpressionFunction FormatMultivaluedList events
        /// </summary>
        public const int ExpressionFunctionFormatMultivaluedListNullFunctionParameterError = 41676;

        /// <summary>
        /// The event identifier for ExpressionFunction FormatMultivaluedList events
        /// </summary>
        public const int ExpressionFunctionFormatMultivaluedListInvalidSecondFunctionParameterTypeError = 41676;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameterInvalidFunctionParameterCountError = 41677;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameterInvalidFunctionParameterError = 41677;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameterNullFunctionParameterError = 41677;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameterInvalidFunctionParameterTypeError = 41677;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter2 events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterCountError = 41678;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter2 events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameter2NullFunctionParameterError = 41678;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter2 events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterError = 41678;

        /// <summary>
        /// The event identifier for ExpressionFunction CreateSqlParameter2 events
        /// </summary>
        public const int ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterTypeError = 41678;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlScalar events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlScalarInvalidFunctionParameterCountError = 41679;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlScalar events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlScalarNullFunctionParameterError = 41679;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlScalar events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlScalarInvalidFunctionParameterTypeError = 41679;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlNonQuery events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlNonQueryInvalidFunctionParameterCountError = 41680;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlNonQuery events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlNonQueryNullFunctionParameterError = 41680;

        /// <summary>
        /// The event identifier for ExpressionFunction ExecuteSqlNonQuery events
        /// </summary>
        public const int ExpressionFunctionExecuteSqlNonQueryInvalidFunctionParameterTypeError = 41680;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByKey events
        /// </summary>
        public const int ExpressionFunctionValueByKeyInvalidFunctionParameterCountError = 41681;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByKey events
        /// </summary>
        public const int ExpressionFunctionValueByKeyNullFunctionParameterError = 41681;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByKey events
        /// </summary>
        public const int ExpressionFunctionValueByKeyInvalidFirstFunctionParameterTypeError = 41681;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByKey events
        /// </summary>
        public const int ExpressionFunctionValueByKeyInvalidSecondFunctionParameterTypeError = 41681;

        /// <summary>
        /// The event identifier for ExpressionFunction DataTimeFromString events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromStringInvalidFunctionParameterCountError = 41682;

        /// <summary>
        /// The event identifier for ExpressionFunction DataTimeFromString events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromStringNullFunctionParameterError = 41682;

        /// <summary>
        /// The event identifier for ExpressionFunction DataTimeFromString events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromStringInvalidFirstFunctionParameterTypeError = 41682;

        /// <summary>
        /// The event identifier for ExpressionFunction DataTimeFromString events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromStringInvalidSecondFunctionParameterTypeError = 41682;

        /// <summary>
        /// The event identifier for ExpressionFunction DataTimeFromString events
        /// </summary>
        public const int ExpressionFunctionDateTimeFromStringInvalidFunctionParametersError = 41682;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertNumberToList events
        /// </summary>
        public const int ExpressionFunctionConvertNumberToListInvalidFunctionParameterCountError = 41683;

        /// <summary>
        /// The event identifier for ExpressionFunction ConvertNumberToList events
        /// </summary>
        public const int ExpressionFunctionConvertNumberToListInvalidFirstFunctionParameterTypeError = 41683;

        /// <summary>
        /// The event identifier for ExpressionFunction Multiply events
        /// </summary>
        public const int ExpressionFunctionMultiplyInvalidFunctionParameterCountError = 41684;

        /// <summary>
        /// The event identifier for ExpressionFunction Multiply events
        /// </summary>
        public const int ExpressionFunctionMultiplyInvalidFirstFunctionParameterTypeError = 41684;

        /// <summary>
        /// The event identifier for ExpressionFunction Multiply events
        /// </summary>
        public const int ExpressionFunctionMultiplyInvalidSecondFunctionParameterTypeError = 41684;

        /// <summary>
        /// The event identifier for ExpressionFunction Divide events
        /// </summary>
        public const int ExpressionFunctionDivideInvalidFunctionParameterCountError = 41685;

        /// <summary>
        /// The event identifier for ExpressionFunction Divide events
        /// </summary>
        public const int ExpressionFunctionDivideInvalidFirstFunctionParameterTypeError = 41685;

        /// <summary>
        /// The event identifier for ExpressionFunction Divide events
        /// </summary>
        public const int ExpressionFunctionDivideInvalidSecondFunctionParameterTypeError = 41685;

        /// <summary>
        /// The event identifier for ExpressionFunction Mod events
        /// </summary>
        public const int ExpressionFunctionModInvalidFunctionParameterCountError = 41686;

        /// <summary>
        /// The event identifier for ExpressionFunction Mod events
        /// </summary>
        public const int ExpressionFunctionModInvalidFirstFunctionParameterTypeError = 41686;

        /// <summary>
        /// The event identifier for ExpressionFunction Mod events
        /// </summary>
        public const int ExpressionFunctionModInvalidSecondFunctionParameterTypeError = 41686;

        /// <summary>
        /// The event identifier for ExpressionFunction ValueByIndex events
        /// </summary>
        public const int ExpressionFunctionIndexByValueInvalidFunctionParameterCountError = 41687;

        /// <summary>
        /// The event identifier for ExpressionFunction IndexByValue events
        /// </summary>
        public const int ExpressionFunctionIndexByValueNullFunctionParameterError = 41645;

        /// <summary>
        /// The event identifier for LookupEvaluator Constructor events
        /// </summary>
        public const int LookupEvaluatorConstructorLookupParameterExpressionValidationError = 41701;

        /// <summary>
        /// The event identifier for LookupEvaluator Constructor events
        /// </summary>
        public const int LookupEvaluatorConstructorLookupParameterValidationError = 41701;

        /// <summary>
        /// The event identifier for LookupEvaluator ResolveLookupParameter events
        /// </summary>
        public const int LookupEvaluatorDetermineLookupParameterError = 41703;

        /// <summary>
        /// The event identifier for ResolveQueries Execute events
        /// </summary>
        public const int ResolveQueriesExecuteError = 41902;

        /// <summary>
        /// The event identifier for UpdateLookups Execute events
        /// </summary>
        public const int UpdateLookupsExecuteError = 42002;

        /// <summary>
        /// The event identifier for DelayActivity Execute events
        /// </summary>
        public const int AddDelayExecuteError = 42502;

        /// <summary>
        /// The event identifier for DelayActivity AddDelay_InitializeTimeoutDuration events
        /// </summary>
        public const int AddDelayDelayInitializeTimeoutDurationError = 42506;

        /// <summary>
        /// The event identifier for SendEmailNotification Execute events
        /// </summary>
        public const int SendEmailNotificationExecuteError = 42602;

        /// <summary>
        /// The event identifier for SendEmailNotification ParseExpressions_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationParseExpressionsExecuteCodeError = 42603;

        /// <summary>
        /// The event identifier for SendEmailNotification CheckResource_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationCheckEmailTemplateResourceExecuteCodeError = 42606;

        /// <summary>
        /// The event identifier for SendEmailNotification ResolveEmailTemplate_ExecuteCode events
        /// </summary>
        public const int SendEmailNotificationResolveEmailTemplateExecuteCodeError = 42607;

        /// <summary>
        /// The event identifier for SendEmailNotification ParseRecipient events
        /// </summary>
        public const int SendEmailNotificationParseRecipientError = 42617;

        /// <summary>
        /// The event identifier for SendEmailNotification ParseEmailTemplate events
        /// </summary>
        public const int SendEmailNotificationParseEmailTemplateError = 42618;

        /// <summary>
        /// The event identifier for SendEmailNotification GetEmailTemplateGuid events
        /// </summary>
        public const int SendEmailNotificationGetEmailTemplateGuidError = 42624;

        /// <summary>
        /// The event identifier for RequestApproval Execute events
        /// </summary>
        public const int RequestApprovalExecuteError = 42702;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalEmailTemplateResourceExecuteCodeError = 42711;

        /// <summary>
        /// The event identifier for RequestApproval ResolveApprovalEmailTemplate_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveApprovalEmailTemplateExecuteCodeError = 42712;

        /// <summary>
        /// The event identifier for RequestApproval CheckEscalationEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckEscalationEmailTemplateResourceExecuteCodeError = 42714;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalCompleteEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalCompleteEmailTemplateResourceExecuteCodeError = 42717;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalDeniedEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalDeniedEmailTemplateResourceExecuteCodeError = 42720;

        /// <summary>
        /// The event identifier for RequestApproval CheckApprovalTimeoutEmailTemplateResource_ExecuteCode events
        /// </summary>
        public const int RequestApprovalCheckApprovalTimeoutEmailTemplateResourceExecuteCodeError = 42723;

        /// <summary>
        /// The event identifier for RequestApproval ResolveDuration_ExecuteCode events
        /// </summary>
        public const int RequestApprovalResolveDurationExecuteCodeError = 42726;

        /// <summary>
        /// The event identifier for RequestApproval GetEmailTemplateGuid events
        /// </summary>
        public const int RequestApprovalGetEmailTemplateGuidError = 42728;

        #endregion
    }
}
