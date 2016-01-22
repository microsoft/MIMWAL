//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolveLookups.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ResolveLookups class
// Resolves Lookup expressions
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.WebServices;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Resolves Lookup expressions
    /// </summary>
    internal partial class ResolveLookups : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty LookupsProperty =
            DependencyProperty.Register("Lookups", typeof(Dictionary<string, object>), typeof(ResolveLookups));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResultsProperty =
            DependencyProperty.Register("QueryResults", typeof(Dictionary<string, List<Guid>>), typeof(ResolveLookups));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ComparedRequestIdProperty =
            DependencyProperty.Register("ComparedRequestId", typeof(Guid), typeof(ResolveLookups));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(ResolveLookups));

        #endregion

        #region Declarations

        /// <summary>
        /// The list of unique identifiers of approval responses
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> ApprovalResponses;

        /// <summary>
        /// The unique identifier of current approval response
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ApprovalResponse;

        /// <summary>
        /// The attributes to read
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public string[] ReadAttributes;

        /// <summary>
        /// The unique identifier of current resource to read
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ReadResource;

        /// <summary>
        /// The list of unique identifiers of resources to read
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public List<Guid> ReadResources;

        /// <summary>
        /// The service actor
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ServiceActor;

        /// <summary>
        /// The parent request which generated the current SystemEvent request
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ParentRequest;

        /// <summary>
        /// The dictionary of alternate lookups for original lookups
        /// </summary>
        private readonly Dictionary<string, string> alternateLookups = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The dictionary of unique identifiers of approvers and approval dates
        /// </summary>
        private readonly Dictionary<Guid, DateTime> approvers = new Dictionary<Guid, DateTime>();

        /// <summary>
        /// The list of lookup parameters
        /// </summary>
        private readonly List<LookupParameter> parameters = new List<LookupParameter>();

        /// <summary>
        /// The dictionary of resource xpath and lookup reads to ensure the minimum number of reads are performed
        /// </summary>
        private readonly Dictionary<string, List<string>> reads = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The dictionary of resources and their unique identifiers. 
        /// FIM inconsistently returns reference values as either UniqueIdentifiers or Guids
        /// To prevent repetitive checks, convert all UniqueIdentifiers to Guids,
        /// and any List<UniqueIdentifier/> to List<Guid/> and store in this dictionary.
        /// </summary>
        private readonly Dictionary<string, List<Guid>> resources = new Dictionary<string, List<Guid>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The dictionary of lookups and their values.
        /// </summary>
        private readonly Dictionary<string, List<object>> values = new Dictionary<string, List<object>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Indicates if the parent workflow is an authorization workflow running against a create request
        /// </summary>
        private bool authZCreate;

        /// <summary>
        /// Indicates if the read resource activity actually needs to perform read
        /// </summary>
        private bool performRead;

        /// <summary>
        /// The key of the resource from "resources" dictionary for which to perform read
        /// </summary>
        private string resourceKey;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveLookups"/> class.
        /// </summary>
        public ResolveLookups()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsConstructor);

            try
            {
                this.InitializeComponent();

                // This GUID should be bound to the ActorID property on FIM activities which require elevated privilege for requests
                // If the default ActorID is used for FIM activities, resultant requests will be executed under the context of the 
                // original actor (User or Built-in Synchronization Account)
                this.ServiceActor = WellKnownGuids.FIMServiceAccount;

                if (this.Lookups == null)
                {
                    this.Lookups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }

                if (this.ReadResources == null)
                {
                    this.ReadResources = new List<Guid>();
                }

                if (this.ApprovalResponses == null)
                {
                    this.ApprovalResponses = new List<Guid>();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the Lookups DependencyProperty.
        /// </summary>
        /// <value>
        /// The lookups.
        /// </value>
        [Description("The dictionary of lookups to be resolved.")]
        [Category("Input/Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Dictionary<string, object> Lookups
        {
            get
            {
                return (Dictionary<string, object>)this.GetValue(LookupsProperty);
            }

            set
            {
                this.SetValue(LookupsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the QueryResults DependencyProperty.
        /// </summary>
        /// <value>
        /// The query results.
        /// </value>
        [Description("The results for any queries which should be used for [//Queries/...] lookup resolution.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Dictionary<string, List<Guid>> QueryResults
        {
            get
            {
                return (Dictionary<string, List<Guid>>)this.GetValue(QueryResultsProperty);
            }

            set
            {
                this.SetValue(QueryResultsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the ComparedRequestId DependencyProperty.
        /// </summary>
        /// <value>
        /// The compared request identifier.
        /// </value>
        [Description("The compared request which should be used for [//ComparedRequest/...] lookup resolution.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ComparedRequestId
        {
            get
            {
                return (Guid)this.GetValue(ComparedRequestIdProperty);
            }

            set
            {
                this.SetValue(ComparedRequestIdProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Value DependencyProperty.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Description("The value which should be used for [//Value/...] lookup resolution.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Value
        {
            get
            {
                return this.GetValue(ValueProperty);
            }

            set
            {
                this.SetValue(ValueProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the activity.
        /// </summary>
        /// <param name="executionContext">The execution context of the activity.</param>
        /// <returns>The <see cref="ActivityExecutionStatus"/> of the activity after executing the activity.</returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsExecute);

            try
            {
                // Ideally we would set CallContext in OnActivityExecutionContextLoad instead here in Execute
                // as OnActivityExecutionContextLoad gets called on each hydration and rehydration of the workflow instance
                // but looks like it's invoked on a different thread context than the rest of the workflow instance execution.
                // To minimize the loss of the CallContext on rehydration, we'll set it in the Execute of every WAL child activities.
                // It will still get lost (momentarily) when the workflow is persisted in the middle of the execution of a replicator activity, for example.
                Logger.SetContextItem(this, this.WorkflowInstanceId);

                return base.Execute(executionContext);
            }
            catch (Exception ex)
            {
                throw Logger.Instance.ReportError(EventIdentifier.ResolveLookupsExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsPrepareExecuteCode);

            try
            {
                // Get the parent workflow so we can use it to pull request target, requestor, etc.
                // and access the workflow dictionary
                SequentialWorkflow parentWorkflow;
                if (!SequentialWorkflow.TryGetContainingWorkflow(this, out parentWorkflow))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ResolveLookupsPrepareExecuteCodeUnableToGetParentWorkflowError, new InvalidOperationException(Messages.UnableToGetParentWorkflowError));
                }

                // Determine if the parent workflow is an authorization workflow running against a create request
                // In this situation, lookups against target will fail because the resource does not yet exist.
                // We will need to convert [//Target/...] to [//Delta/...] to facilitate resolution
                // This behavior replicates FIM's native behavior
                if (this.Request.CurrentRequest.Operation == OperationType.Create &&
                    this.Request.CurrentRequest.AuthorizationWorkflowInstances != null)
                {
                    foreach (UniqueIdentifier ui in this.Request.CurrentRequest.AuthorizationWorkflowInstances)
                    {
                        Guid authZWorkflowInstanceId;
                        if (ui.TryGetGuid(out authZWorkflowInstanceId) && authZWorkflowInstanceId == this.WorkflowInstanceId)
                        {
                            this.authZCreate = true;
                            break;
                        }
                    }
                }

                foreach (string s in this.Lookups.Keys)
                {
                    Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsPrepareExecuteCode, "Constructing lookup evaluator for lookup: '{0}'.", s);

                    LookupEvaluator originalLookup = new LookupEvaluator(s);
                    LookupEvaluator resolvableLookup = new LookupEvaluator(s);

                    // There are certain conditions which will prompt the activity to dynamically replace the original lookup with an alternate
                    // This is done at the paramater, swapping [//Effective/...] with [//Delta/...] or [//Target/...], for example
                    if (this.authZCreate && (originalLookup.Parameter == LookupParameter.Target || originalLookup.Parameter == LookupParameter.Effective))
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsPrepareExecuteCode, "The lookup '{0}' will be processed as [//Delta] lookup.", s);

                        // When resolving a [//Target/...] or [//Effective/...] lookup during authZ on a create request,
                        // we will always pull the value from the request delta because the target resource will not yet be available for read
                        string lookupString = s.Replace(string.Format(CultureInfo.InvariantCulture, "[//{0}/", s.Substring(3, originalLookup.Parameter.ToString().Length)), string.Format(CultureInfo.InvariantCulture, "[//{0}/", LookupParameter.Delta));
                        resolvableLookup = new LookupEvaluator(lookupString);
                    }
                    else if (originalLookup.Parameter == LookupParameter.Effective)
                    {
                        // When resolving a [//Effective/...] lookup, we need to determine if we should resolve against the request delta or the target resource
                        // If the attribute tied to the Effective parameter exists in the request parameters, the associated value will always supersede the 
                        // attribute value on the target resource
                        bool useDelta = false;
                        if (originalLookup.Components.Count > 1)
                        {
                            ReadOnlyCollection<CreateRequestParameter> requestParameters = this.Request.CurrentRequest.ParseParameters<CreateRequestParameter>();
                            var query = from p in requestParameters where p.PropertyName.Equals(originalLookup.Components[1], StringComparison.OrdinalIgnoreCase) select p.PropertyName;
                            if (query.Any())
                            {
                                useDelta = true;

                                Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsPrepareExecuteCode, "The [//Effective] lookup '{0}' will be processed as [//Delta] lookup.", s);
                            }
                        }

                        // Either replace [//Effective/...] with [//Delta/...] or [//Target/...] to facilitate resolution
                        // The activity will look for this lookup when resolving the original effective lookup
                        string lookupString = s.Replace(string.Format(CultureInfo.InvariantCulture, "[//{0}/", s.Substring(3, originalLookup.Parameter.ToString().Length)), useDelta ? string.Format(CultureInfo.InvariantCulture, "[//{0}/", LookupParameter.Delta) : string.Format(CultureInfo.InvariantCulture, "[//{0}/", LookupParameter.Target));

                        resolvableLookup = new LookupEvaluator(lookupString);
                    }

                    // Add the lookup parameter to the list of parameters for the activity so we know which resolutions are required
                    // Also establish a new entry in the values dictionary
                    // If the resolvable lookup is different than the original, add the pair to the alternates dictionary so we can find it later during resolution
                    if (!this.parameters.Contains(resolvableLookup.Parameter))
                    {
                        this.parameters.Add(resolvableLookup.Parameter);
                    }

                    if (!this.values.ContainsKey(resolvableLookup.Lookup))
                    {
                        this.values.Add(resolvableLookup.Lookup, new List<object>());
                    }

                    if (originalLookup.Lookup != resolvableLookup.Lookup)
                    {
                        this.alternateLookups.Add(originalLookup.Lookup, resolvableLookup.Lookup);
                    }

                    // Add each of the lookup's reads to the read dictionary which represents all lookups
                    // This single dictionary will ensure the minimum number of reads are performed
                    foreach (string key in resolvableLookup.Reads.Keys)
                    {
                        if (!this.reads.ContainsKey(key))
                        {
                            this.reads.Add(key, new List<string> { resolvableLookup.Reads[key] });
                        }
                        else if (!this.reads[key].Contains(resolvableLookup.Reads[key]))
                        {
                            this.reads[key].Add(resolvableLookup.Reads[key]);
                        }
                    }

                    // If resolving a [//WorkflowData/...] lookup, publish the value of the specified
                    // workflow dictionary key to the resources or values dictionary
                    // Make sure we only do this once for each workflow dictionary key because the publish method will stack values if it's executed more than once
                    if (resolvableLookup.Parameter == LookupParameter.WorkflowData &&
                        resolvableLookup.Components.Count > 1 &&
                        parentWorkflow.WorkflowDictionary.ContainsKey(resolvableLookup.Components[1]) &&
                        parentWorkflow.WorkflowDictionary[resolvableLookup.Components[1]] != null &&
                        !this.values.ContainsKey(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", LookupParameter.WorkflowData, resolvableLookup.Components[1])))
                    {
                        this.Publish(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", LookupParameter.WorkflowData, resolvableLookup.Components[1]), parentWorkflow.WorkflowDictionary[resolvableLookup.Components[1]]);
                    }
                }

                // If we have reads, add the keys of the read dictionary to the for each loop which will perform them
                if (this.reads.Count > 0)
                {
                    this.ForEachRead.InitialChildData = this.reads.Keys.ToArray();
                }

                // Publish Request, Requestor, and Target to the resource and values dictionary
                // regardless of lookup parameter
                this.Publish(LookupParameter.Request.ToString(), parentWorkflow.RequestId);
                this.Publish(LookupParameter.Requestor.ToString(), parentWorkflow.ActorId);
                if (this.parameters.Contains(LookupParameter.Target))
                {
                    this.Publish(LookupParameter.Target.ToString(), parentWorkflow.TargetId);
                }

                // Publish the supplied value for [//Value]
                if (this.parameters.Contains(LookupParameter.Value))
                {
                    this.Publish(LookupParameter.Value.ToString(), this.Value);
                }

                // Resolve lookups associated with the [//Delta/...] parameter
                if (this.parameters.Contains(LookupParameter.Delta))
                {
                    if (this.Request.CurrentRequest.Operation != OperationType.SystemEvent)
                    {
                        this.PublishRequestDelta(this.Request.CurrentRequest, false);
                    }
                }

                // If necessary, add the supplied query results to the resource dictionary
                // to support resolution of [//Queries/...] lookups
                if (this.parameters.Contains(LookupParameter.Queries))
                {
                    if (this.QueryResults != null)
                    {
                        foreach (string query in this.QueryResults.Keys)
                        {
                            this.Publish(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", LookupParameter.Queries, query), this.QueryResults[query]);
                        }
                    }
                    else
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsPrepareExecuteCode, "The [//Queries/...] lookup(s) don't have any supplied results.");
                    }
                }

                // Preemptively build the XPath filter which identifies approvals associated with the request
                // The activity will only be used if we are resolving a [//Approvers/...] lookup
                this.FindApprovals.XPathFilter = string.Format(CultureInfo.InvariantCulture, "/Approval[Request = '{0}']", parentWorkflow.RequestId);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsPrepareExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveComparedRequest CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveComparedRequest_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsResolveComparedRequestExecuteCode, "ComparedRequestId: '{0}'.", this.ComparedRequestId);

            try
            {
                // Resolution of [//ComparedRequest/...] lookups is not possible 
                // if we could not read the resource
                if (this.ReadComparedRequest.Resource == null)
                {
                    return;
                }

                // Resolve lookups associated with the [//ComparedRequest/Delta/...] parameter
                this.Publish(LookupParameter.ComparedRequest.ToString(), this.ComparedRequestId);
                this.PublishRequestDelta(this.ReadComparedRequest.Resource as RequestType, true);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsResolveComparedRequestExecuteCode, "ComparedRequestId: '{0}'.", this.ComparedRequestId);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ReadEnumeratedApproval CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReadEnumeratedApproval_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsReadEnumeratedApprovalExecuteCode);

            try
            {
                // The enumerate resources activity will iterate through a collection of approvals
                // Only evaluate those which have an associated approval response
                ResourceType approval = EnumerateResourcesActivity.GetCurrentIterationItem((CodeActivity)sender) as ResourceType;
                if (approval == null || approval["ApprovalResponse"] == null)
                {
                    return;
                }

                // Add each associated approval response to the list to be evaluated
                foreach (UniqueIdentifier ui in (UniqueIdentifier[])approval["ApprovalResponse"])
                {
                    Guid approvalResponse;
                    if (!ui.TryGetGuid(out approvalResponse))
                    {
                        continue;
                    }

                    this.ApprovalResponses.Add(approvalResponse);

                    Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsReadEnumeratedApprovalExecuteCode, "Added item '{0}' to the ApprovalResponses List.", approvalResponse);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsReadEnumeratedApprovalExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachResponse ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachResponse_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsForEachResponseChildInitialized, "ApprovalResponse: '{0}'.", e.InstanceData);

            try
            {
                // The instance data represents the Guid of the approval response
                // Assign this Guid as the resource to be read by the ReadResponse read resource activity
                if (e.InstanceData != null)
                {
                    this.ApprovalResponse = (Guid)((Guid?)e.InstanceData);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsForEachResponseChildInitialized, "ApprovalResponse: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachResponse ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachResponse_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsForEachResponseChildCompleted);

            try
            {
                // Evaluate the approval response to determine if its creator 
                // represents an approver
                ResourceType approvalResponse = ((ReadResourceActivity)e.Activity).Resource;
                if (approvalResponse == null || approvalResponse["Decision"] == null || approvalResponse["Creator"] == null ||
                    approvalResponse["CreatedTime"] == null ||
                    !approvalResponse["Decision"].ToString().Equals("Approved", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                // Get the approver and the approval date from the approval response
                // and add the information to the dictionary
                Guid approver;
                ((UniqueIdentifier)approvalResponse["Creator"]).TryGetGuid(out approver);
                DateTime approvalDate = (DateTime)approvalResponse["CreatedTime"];
                if (approver == Guid.Empty)
                {
                    return;
                }

                // Check if approver approved multiple times
                if (!this.approvers.ContainsKey(approver))
                {
                    this.approvers.Add(approver, approvalDate);
                }
                else
                {
                    if (approvalDate > this.approvers[approver])
                    {
                        this.approvers[approver] = approvalDate;
                    }
                }

                Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsForEachResponseChildCompleted, "The approval '{0}' - '{1}' added to the approver dictionary.", approver, approvalDate);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsForEachResponseChildCompleted);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveApprovers CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveApprovers_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsResolveApproversExecuteCode);

            try
            {
                // Publish the list of identified approvers
                // after sorting the list based on approval date
                // This will provide consistency when performing resolution against multiple approvers
                if (this.approvers.Count <= 0)
                {
                    return;
                }

                List<Guid> sortedApprovers = (from key in this.approvers.Keys orderby this.approvers[key] ascending select key).ToList<Guid>();
                this.Publish("Approvers", sortedApprovers);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsResolveApproversExecuteCode, "Total Approver Count: '{0}'.", this.approvers.Count);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveRequestParameter CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveRequestParameter_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsResolveRequestParameterExecuteCode, "ResolvedExpression: AllChangesAuthorizationTable = '{0}' AllChangesActionTable = {1}.", this.ResolveStandard.ResolvedExpression, this.ResolveStandard2.ResolvedExpression);

            try
            {
                // The only lookup supported for the [//RequestParameter/...] parameter is [//RequestParameter/AllChangesAuthorizationTable] and [//RequestParameter/AllChangesActionTable]
                // This lookup will be resolved by the native FIM grammar resolution activity
                // Decode the resolved value before publishing it
                if (!string.IsNullOrEmpty(this.ResolveStandard.ResolvedExpression))
                {
                    string allChangesAuthorizationTable = "<table>" + HttpUtility.HtmlDecode(this.ResolveStandard.ResolvedExpression) + "</table>";
                    this.Publish(this.ResolveStandard.GrammarExpression.Substring(3, this.ResolveStandard.GrammarExpression.Length - 4), allChangesAuthorizationTable);
                }

                if (!string.IsNullOrEmpty(this.ResolveStandard2.ResolvedExpression))
                {
                    string allChangesActionTable = "<table>" + HttpUtility.HtmlDecode(this.ResolveStandard2.ResolvedExpression) + "</table>";
                    this.Publish(this.ResolveStandard2.GrammarExpression.Substring(3, this.ResolveStandard2.GrammarExpression.Length - 4), allChangesActionTable);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsResolveRequestParameterExecuteCode, "ResolvedExpression: AllChangesAuthorizationTable = '{0}' AllChangesActionTable = {1}.", this.ResolveStandard.ResolvedExpression, this.ResolveStandard2.ResolvedExpression);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveParentRequest CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveParentRequest_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsResolveParentRequestExecuteCode, "ParentRequestId: '{0}'.", this.ParentRequest);

            try
            {
                if (this.ReadParentRequest.Resource == null)
                {
                    return;
                }

                this.PublishRequestDelta(this.ReadParentRequest.Resource as RequestType, false);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsResolveParentRequestExecuteCode, "ParentRequestId: '{0}'.", this.ParentRequest);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachRead ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachRead_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsForEachReadChildInitialized, "ResourceKey: '{0}'.", e.InstanceData);

            try
            {
                // Since we are looping through keys in the read dictionary, we will recieve the resource key to be read
                // For example: Target/Manager
                // If we didn't find a matching Guid for the resource, or if there are no attributes to be read,
                // stop the read resource activity from being executed
                // Otherwise, prepare to read the attribute from each associated resource
                this.resourceKey = e.InstanceData.ToString();
                if (!this.resources.ContainsKey(this.resourceKey) || this.resources[this.resourceKey].Count == 0 || this.reads[this.resourceKey].Count == 0)
                {
                    this.performRead = false;
                }
                else
                {
                    this.performRead = true;
                    this.ReadResources = this.resources[this.resourceKey];
                    this.ReadAttributes = this.reads[this.resourceKey].ToArray();

                    Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsForEachReadChildInitialized, "ResourceKey: '{0}'. Read Attributes: '{1}'.", e.InstanceData, string.Join(",", this.ReadAttributes)); 
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsForEachReadChildInitialized, "ResourceKey: '{0}'. Perform Read: '{1}'.", e.InstanceData, this.performRead);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachResource ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachResource_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsForEachResourceChildInitialized, "ReadResource: '{0}'.", e.InstanceData);

            try
            {
                // The instance data represents the Guid of the resource to read
                // Assign this Guid as the resource to be read by the read resource activity
                if (e.InstanceData != null)
                {
                    this.ReadResource = (Guid)((Guid?)e.InstanceData);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsForEachResourceChildInitialized, "ReadResource: '{0}'.  Read Attributes: '{1}'.", e.InstanceData, this.ReadAttributes == null ? null : string.Join(",", this.ReadAttributes));
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachResource ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachResource_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsForEachResourceChildCompleted, "ReadResource: '{0}'. PerformRead: '{1}'.", e.InstanceData, this.performRead);

            try
            {
                // Evaluate the read resource
                ReadResourceActivity read = e.Activity as ReadResourceActivity;
                if (!this.performRead || read == null || read.Resource == null)
                {
                    return;
                }

                foreach (string attribute in this.reads[this.resourceKey].Where(attribute => read.Resource[attribute] != null))
                {
                    this.Publish(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", this.resourceKey, attribute), read.Resource[attribute]);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsForEachResourceChildCompleted, "ReadResource: '{0}'. PerformRead: '{1}'.", e.InstanceData, this.performRead);
            }
        }

        /// <summary>
        /// Publishes the specified resource key and it's values to the resources and values dictionaries
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="value">The resource value.</param>
        private void Publish(string key, object value)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsPublish, "Key: '{0}'. Value: '{1}'.", key, value);

            try
            {
                // Do not accept null values
                if (string.IsNullOrEmpty(key) || value == null)
                {
                    return;
                }

                // FIM inconsistently returns reference values as either UniqueIdentifiers or Guids
                // To prevent repetitive checks, convert all UniqueIdentifiers to Guids,
                // and any List<UniqueIdentifier> to List<Guid>
                if (value.GetType() == typeof(UniqueIdentifier))
                {
                    value = ((UniqueIdentifier)value).GetGuid();
                }
                else if (value.GetType() == typeof(List<UniqueIdentifier>))
                {
                    List<Guid> guidList = (from id in (List<UniqueIdentifier>)value select id.GetGuid()).ToList();
                    value = guidList;
                }

                // Determine if the key exists in the read dictionary
                // If it does, it assumes the lookup being published is a resource to be read
                if (this.reads.ContainsKey(key))
                {
                    // Determine if the resource key already exists in the resources dictionary
                    // If not, add it now
                    if (!this.resources.ContainsKey(key))
                    {
                        this.resources.Add(key, new List<Guid>());
                    }

                    // Based on the type of the supplied value, determine if/how the associated resource ID
                    // should be added to the list of Guids associated with the resource key
                    if (value is Guid && !this.resources[key].Contains((Guid)value))
                    {
                        this.resources[key].Add((Guid)value);
                    }
                    else if (value.GetType() == typeof(List<Guid>))
                    {
                        foreach (Guid g in ((List<Guid>)value).Where(g => !this.resources[key].Contains(g)))
                        {
                            this.resources[key].Add(g);
                        }
                    }

                    // If the value was not added to the resource dictionary, presumably
                    // because it is an invalid type, delete the entry
                    if (this.resources[key].Count == 0)
                    {
                        this.resources.Remove(key);
                    }
                }

                // Determine if the supplied key matches a lookup for the activity and, if it does,
                // add the value to the list of resolved values
                string lookup = string.Format(CultureInfo.InvariantCulture, "[//{0}]", key);
                if (!this.values.ContainsKey(lookup))
                {
                    return;
                }

                // If the value is a list, indicating the lookup resolves to a multivalued attribute,
                // loop through each value and add it to the resolved values collection
                // If the value is not a list, add it directly
                if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(List<>))
                {
                    foreach (object o in (IEnumerable)value)
                    {
                        this.values[lookup].Add(o);
                    }
                }
                else
                {
                    this.values[lookup].Add(value);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsPublish, "Key: '{0}'. Value: '{1}'.", key, value);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Resolve CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Resolve_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsResolveExecuteCode);

            try
            {
                List<string> lookups = this.Lookups.Keys.ToList();
                foreach (string s in lookups)
                {
                    // If the original lookup has been replaced with an alternate to faciliate resolution,
                    // make sure we're looking for the resolvable lookup in the values dictionary
                    string resolvableLookup = s;
                    if (this.alternateLookups.ContainsKey(s))
                    {
                        resolvableLookup = this.alternateLookups[s];
                    }

                    // Determine how the lookup should be resolved based on the number of identified values
                    switch (this.values[resolvableLookup].Count)
                    {
                        case 0:

                            // If no value was found for the lookup, return null
                            this.Lookups[s] = null;
                            break;
                        case 1:

                            // If a single value was found for the lookup, return that value directly
                            // It will be returned as the appropriate type (string, bool, etc.)
                            this.Lookups[s] = this.values[resolvableLookup][0];
                            break;
                        default:

                            // If multiple values were found for the lookup, verify that they are of a consistent type
                            Type type = null;
                            bool consistentType = true;
                            foreach (object value in this.values[resolvableLookup])
                            {
                                if (type == null)
                                {
                                    type = value.GetType();
                                }
                                else if (value.GetType() != type)
                                {
                                    consistentType = false;
                                }
                            }

                            // If we have multiple values of an inconsistent type, there is a problem
                            // which needs to be addressed by the administrator
                            if (!consistentType)
                            {
                                throw Logger.Instance.ReportError(EventIdentifier.ResolveLookupsResolveExecuteCodeInconsistentTypeError, new WorkflowActivityLibraryException(Messages.ResolveLookups_InconsistentTypeError, s));
                            }

                            // Because we have multiple values to be returned for the lookup, 
                            // we want to return them in the form of a strongly-typed list
                            // For example: List<string> instead of List<object>
                            // Use reflection to create a new strongly-typed list
                            Type listType = typeof(List<>).MakeGenericType(new Type[] { type });
                            var typedList = Activator.CreateInstance(listType);

                            // Using reflection, fetch the add method for the new list
                            // and invoke it to add each value from the original List<object> to the new collection
                            // Return the strongly-typed list
                            MethodInfo add = listType.GetMethod("Add");
                            foreach (object value in this.values[resolvableLookup])
                            {
                                add.Invoke(typedList, new object[] { value });
                            }

                            this.Lookups[s] = typedList;

                            break;
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsResolveExecuteCode);
            }
        }

        /// <summary>
        /// Publishes the request delta.
        /// </summary>
        /// <param name="request">The request type.</param>
        /// <param name="comparedRequest">if set to <c>true</c>, lookups associated with the [//ComparedRequest/Delta/...] parameter.</param>
        private void PublishRequestDelta(RequestType request, bool comparedRequest)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsPublishRequestDelta, "Request Operation: '{0}'. ComparedRequest: '{1}'.", request.Operation, comparedRequest);

            try
            {
                // Action will be determined based on the request type
                // Put requests support additional modes (Insert, Remove)
                switch (request.Operation)
                {
                    case OperationType.Put:
                        {
                            // Pull the update request parameters from the request and loop through them
                            ReadOnlyCollection<UpdateRequestParameter> requestParameters = request.ParseParameters<UpdateRequestParameter>();
                            foreach (UpdateRequestParameter parameter in requestParameters)
                            {
                                string propertyKey = string.Empty;

                                UniqueIdentifier target = comparedRequest ? request.Target : this.Request.CurrentRequest.Target;
                                if (parameter.Target == target)
                                {
                                    // Build the property key based on the mode for the update
                                    // This property key will be used to identify matching keys in the read and expression dictionaries
                                    switch (parameter.Mode)
                                    {
                                        case UpdateMode.Modify:
                                            propertyKey = string.Format(CultureInfo.InvariantCulture, "Delta/{0}", parameter.PropertyName);
                                            break;
                                        case UpdateMode.Insert:
                                            propertyKey = string.Format(CultureInfo.InvariantCulture, "Delta/{0}/Added", parameter.PropertyName);
                                            break;
                                        case UpdateMode.Remove:
                                            propertyKey = string.Format(CultureInfo.InvariantCulture, "Delta/{0}/Removed", parameter.PropertyName);
                                            break;
                                    }
                                }

                                // If this is a compared request, append the "ComparedRequest/" prefix to the delta property key
                                // Publish the delta property to the resource and expression dictionaries
                                if (comparedRequest)
                                {
                                    propertyKey = string.Format(CultureInfo.InvariantCulture, "ComparedRequest/{0}", propertyKey);
                                }

                                this.Publish(propertyKey, parameter.Value);
                            }
                        }

                        break;
                    case OperationType.Create:
                        {
                            // Pull the create request parameters from the request and loop through them
                            ReadOnlyCollection<CreateRequestParameter> requestParameters = request.ParseParameters<CreateRequestParameter>();
                            foreach (CreateRequestParameter parameter in requestParameters)
                            {
                                string propertyKey = string.Empty;

                                UniqueIdentifier target = comparedRequest ? request.Target : this.Request.CurrentRequest.Target;
                                if (parameter.Target == target)
                                {
                                    // Build the property key which will be used to identify matching keys 
                                    // in the read and expression dictionaries
                                    propertyKey = string.Format(CultureInfo.InvariantCulture, "Delta/{0}", parameter.PropertyName);
                                }

                                // If this is a compared request, append the "ComparedRequest/" prefix to the delta property key
                                // Publish the delta property to the resource and expression dictionaries
                                if (comparedRequest)
                                {
                                    propertyKey = string.Format(CultureInfo.InvariantCulture, "ComparedRequest/{0}", propertyKey);
                                }

                                this.Publish(propertyKey, parameter.Value);
                            }
                        }

                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsPublishRequestDelta, "Request Operation: '{0}'. ComparedRequest: '{1}'.", request.Operation, comparedRequest);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the ComparedRequest Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ComparedRequest_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsComparedRequestCondition, "ComparedRequestId: '{0}'.", this.ComparedRequestId);

            try
            {
                e.Result = this.parameters.Contains(LookupParameter.ComparedRequest) && this.ComparedRequestId != Guid.Empty;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsComparedRequestCondition, "ComparedRequestId: '{0}'. Condition evaluated '{1}'.", this.ComparedRequestId, e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the Approvers Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void Approvers_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsApproversCondition);

            try
            {
                e.Result = this.parameters.Contains(LookupParameter.Approvers);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsApproversCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the RequestParameter Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void RequestParameter_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsRequestParameterCondition);

            try
            {
                e.Result = this.parameters.Contains(LookupParameter.RequestParameter);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsRequestParameterCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the CompositeRequest Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void CompositeRequest_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsCompositeRequestCondition);

            try
            {
                e.Result = false;

                if (this.Request.CurrentRequest.Operation != OperationType.SystemEvent)
                {
                    return;
                }

                Guid parent;
                if (!this.Request.CurrentRequest.ParentRequest.TryGetGuid(out parent))
                {
                    return;
                }

                Logger.Instance.WriteVerbose(EventIdentifier.ResolveLookupsCompositeRequestCondition, "Parent request for the current SystemEvent request target {0} is {1}.", this.Request.CurrentRequest.Target, parent);

                this.ParentRequest = parent;
                e.Result = true;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsCompositeRequestCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        /// <summary>
        /// Handles the Condition event of the PerformRead Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void PerformRead_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupsPerformReadCondition);

            try
            {
                e.Result = this.performRead;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupsPerformReadCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        #endregion

        #endregion
    }
}