//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateLookups.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// UpdateLookups class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.WebServices;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Processes update lookups
    /// </summary>
    internal partial class UpdateLookups : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty UpdateLookupDefinitionsProperty =
            DependencyProperty.Register("UpdateLookupDefinitions", typeof(List<UpdateLookupDefinition>), typeof(UpdateLookups));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResultsProperty =
            DependencyProperty.Register("QueryResults", typeof(Dictionary<string, List<Guid>>), typeof(UpdateLookups));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(UpdateLookups));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorProperty = DependencyProperty.Register("Actor", typeof(Guid), typeof(UpdateLookups));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApplyAuthorizationPolicyProperty =
            DependencyProperty.Register("ApplyAuthorizationPolicy", typeof(bool), typeof(UpdateLookups));

        #endregion

        #region Declarations

        /// <summary>
        /// The current request parameters.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public UpdateRequestParameter[] CurrentRequestParameters;

        /// <summary>
        /// The current target resource.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid CurrentTargetResource;

        /// <summary>
        /// The pending update requests.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<Guid, List<UpdateRequestParameter>> PendingRequests;

        /// <summary>
        /// The target lookups.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<string, object> TargetLookups;

        /// <summary>
        /// The update requests.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Dictionary<Guid, List<UpdateRequestParameter>> UpdateRequests;

        /// <summary>
        /// The service actor
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ServiceActor;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateLookups"/> class.
        /// </summary>
        public UpdateLookups()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsConstructor);

            try
            {
                this.InitializeComponent();

                // This GUID should be bound to the ActorID property on FIM activities which require elevated privilege for requests
                // If the default ActorID is used for FIM activities, resultant requests will be executed under the context of the 
                // original actor (User or Built-in Synchronization Account)
                this.ServiceActor = WellKnownGuids.FIMServiceAccount;

                if (this.UpdateLookupDefinitions == null)
                {
                    this.UpdateLookupDefinitions = new List<UpdateLookupDefinition>();
                }

                if (this.PendingRequests == null)
                {
                    this.PendingRequests = new Dictionary<Guid, List<UpdateRequestParameter>>();
                }

                if (this.TargetLookups == null)
                {
                    this.TargetLookups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }

                if (this.UpdateRequests == null)
                {
                    this.UpdateRequests = new Dictionary<Guid, List<UpdateRequestParameter>>();
                }

                // If no actor was supplied to the activity, default to the FIM Service account
                if (this.Actor == Guid.Empty)
                {
                    this.Actor = this.ServiceActor;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsConstructor, "Activity Actor: '{0}'.", this.Actor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the update lookup definitions which should be used to generate the necessary update requests.
        /// </summary>
        [Description("The update lookup definitions which should be used to generate the necessary update requests.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<UpdateLookupDefinition> UpdateLookupDefinitions
        {
            get
            {
                return (List<UpdateLookupDefinition>)this.GetValue(UpdateLookupDefinitionsProperty);
            }

            set
            {
                this.SetValue(UpdateLookupDefinitionsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the query results for any queries which should be used for [//Queries/...] lookup resolution.
        /// </summary>
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
        /// Gets or sets the value which should be used for [//Value/...] lookup resolution.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the actor for child requests. An empty GUID indicates that child requests will be submitted under the context of the original request actor.
        /// </summary>
        /// <value>
        /// The actor.
        /// </value>
        [Description("The Guid of the Actor for child requests. An empty Guid indicates that child requests will be submitted under the context of the original request actor.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid Actor
        {
            get
            {
                return (Guid)this.GetValue(ActorProperty);
            }

            set
            {
                this.SetValue(ActorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether child requests will be subjected to authorization policy.
        /// </summary>
        [Description("When enabled, child requests will be subjected to authorization policy.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ApplyAuthorizationPolicy
        {
            get
            {
                return (bool)this.GetValue(ApplyAuthorizationPolicyProperty);
            }

            set
            {
                this.SetValue(ApplyAuthorizationPolicyProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.UpdateLookupsExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsPrepareExecuteCode);

            try
            {
                // If no update lookup definitions were supplied to the activity,
                // instantiate the list now to prevent exception
                if (this.UpdateLookupDefinitions == null)
                {
                    this.UpdateLookupDefinitions = new List<UpdateLookupDefinition>();
                }

                // Build the target lookup dictionary based on the supplied update lookup definitions
                foreach (LookupEvaluator lookup in this.UpdateLookupDefinitions.Select(update => new LookupEvaluator(update.TargetLookup)).Where(lookup => !string.IsNullOrEmpty(lookup.TargetResourceLookup) && !this.TargetLookups.ContainsKey(lookup.TargetResourceLookup)))
                {
                    this.TargetLookups.Add(lookup.TargetResourceLookup, null);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsPrepareExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the BuildRequests CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BuildRequests_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsBuildRequestsExecuteCode);

            try
            {
                foreach (UpdateLookupDefinition update in this.UpdateLookupDefinitions)
                {
                    LookupEvaluator lookup = new LookupEvaluator(update.TargetLookup);
                    if (lookup.TargetIsWorkflowDictionary)
                    {
                        // Get the parent workflow to facilitate access to the workflow dictionary
                        SequentialWorkflow parentWorkflow;
                        SequentialWorkflow.TryGetContainingWorkflow(this, out parentWorkflow);

                        // For simplicity, start by adding the key to the parent workflow dictionary so we can assume that its there
                        if (!parentWorkflow.WorkflowDictionary.ContainsKey(lookup.TargetAttribute))
                        {
                            parentWorkflow.WorkflowDictionary.Add(lookup.TargetAttribute, null);
                        }

                        if (update.Mode == UpdateMode.Modify)
                        {
                            parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] = update.Value;
                        }
                        else if (update.Value != null)
                        {
                            // Use reflection to determine the expected List<> type based on the value
                            // Also get the Add and Remove methods for the list
                            Type listType = typeof(List<>).MakeGenericType(new Type[] { update.Value.GetType() });
                            MethodInfo add = listType.GetMethod("Add");
                            MethodInfo remove = listType.GetMethod("Remove");

                            switch (update.Mode)
                            {
                                case UpdateMode.Insert:
                                    if (parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] == null)
                                    {
                                        parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] = update.Value;
                                    }
                                    else if (parentWorkflow.WorkflowDictionary[lookup.TargetAttribute].GetType() == update.Value.GetType())
                                    {
                                        // Single value, create a new instance of the appropriate List<> type
                                        // and add both values: existing and new
                                        object existingValue = parentWorkflow.WorkflowDictionary[lookup.TargetAttribute];
                                        parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] = Activator.CreateInstance(listType);
                                        add.Invoke(parentWorkflow.WorkflowDictionary[lookup.TargetAttribute], new object[] { existingValue });
                                        add.Invoke(parentWorkflow.WorkflowDictionary[lookup.TargetAttribute], new object[] { update.Value });
                                    }
                                    else if (parentWorkflow.WorkflowDictionary[lookup.TargetAttribute].GetType() == listType)
                                    {
                                        // The dictionary key is a list of the expected type, add the value
                                        add.Invoke(parentWorkflow.WorkflowDictionary[lookup.TargetAttribute], new object[] { update.Value });
                                    }
                                    else
                                    {
                                        // We have a problem and need to report an error
                                        throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(Messages.UpdateLookup_InsertVariableError, update.Value.GetType(), lookup.TargetAttribute, parentWorkflow.WorkflowDictionary[lookup.TargetAttribute].GetType()));
                                    }

                                    break;
                                case UpdateMode.Remove:
                                    if (parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] == null)
                                    {
                                        // Do nothing
                                    }
                                    else if (parentWorkflow.WorkflowDictionary[lookup.TargetAttribute].Equals(update.Value))
                                    {
                                        // A single matching value exists, clear the variable
                                        parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] = null;
                                    }
                                    else if (parentWorkflow.WorkflowDictionary[lookup.TargetAttribute].GetType() == listType)
                                    {
                                        // The variable is a list of the expected type, attempt to remove the value
                                        remove.Invoke(parentWorkflow.WorkflowDictionary[lookup.TargetAttribute], new object[] { update.Value });

                                        // Check the count on the list to determine if we are down to a single value or have no value
                                        // If so, adjust the value of the variable accordingly to eliminate the list
                                        object listValue = null;
                                        int i = 0;
                                        foreach (object o in (IEnumerable)parentWorkflow.WorkflowDictionary[lookup.TargetAttribute])
                                        {
                                            i += 1;
                                            listValue = o;
                                        }

                                        if (i <= 1)
                                        {
                                            parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] = listValue;
                                        }
                                    }

                                    break;
                            }
                        }

                        // If we ended up with a null value in the workflow dictionary key,
                        // remove the key to cleanup after ourselves
                        if (parentWorkflow.WorkflowDictionary[lookup.TargetAttribute] == null)
                        {
                            parentWorkflow.WorkflowDictionary.Remove(lookup.TargetAttribute);
                        }
                    }
                    else if (!string.IsNullOrEmpty(lookup.TargetResourceLookup) &&
                             this.TargetLookups.ContainsKey(lookup.TargetResourceLookup) &&
                             this.TargetLookups[lookup.TargetResourceLookup] != null)
                    {
                        // Based on the type of the resolved target lookup (should be Guid or List<Guid>)
                        // build the list of target resources for the update
                        List<Guid> targets = new List<Guid>();
                        if (this.TargetLookups[lookup.TargetResourceLookup] is Guid)
                        {
                            targets.Add((Guid)this.TargetLookups[lookup.TargetResourceLookup]);
                        }
                        else if (this.TargetLookups[lookup.TargetResourceLookup].GetType() == typeof(List<Guid>))
                        {
                            targets.AddRange((List<Guid>)this.TargetLookups[lookup.TargetResourceLookup]);
                        }

                        foreach (Guid target in targets)
                        {
                            // Add the target to the update requests dictionary, if it doesn't already exist,
                            // and add the new update request parameter
                            if (!this.PendingRequests.ContainsKey(target))
                            {
                                this.PendingRequests.Add(target, new List<UpdateRequestParameter>());
                            }

                            this.PendingRequests[target].Add(new UpdateRequestParameter(lookup.TargetAttribute, update.Mode, update.Value));
                        }
                    }
                }

                // If there are requests that need to be submitted to fulfill the updates,
                // assign the list of targets to the for each loop which will evaluate each change
                // to determine if they will result in changes
                if (this.PendingRequests.Count > 0)
                {
                    this.ForEachPending.InitialChildData = this.PendingRequests.Keys.ToList();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsBuildRequestsExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachPending ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachPending_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsForEachPendingChildInitialized, "Resource: '{0}'.", e.InstanceData);

            try
            {
                // Cast the instance data as a Guid and prepare the read resource activity
                // by assigning a resource ID for update and the attributes to be read
                Guid resource = new Guid();
                if (e.InstanceData != null)
                {
                    resource = (Guid)((Guid?)e.InstanceData);
                }

                ReadResourceActivity read = e.Activity as ReadResourceActivity;

                if (read == null)
                {
                    return;
                }

                read.ResourceId = resource;
                read.SelectionAttributes = this.PendingRequests[resource].Select(parameter => parameter.PropertyName).ToArray();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsForEachPendingChildInitialized, "Resource: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ChildCompleted event of the ForEachPending ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachPending_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsForEachPendingChildCompleted, "Resource: '{0}'.", e.InstanceData);

            try
            {
                Guid resource = new Guid();
                if (e.InstanceData != null)
                {
                    resource = (Guid)((Guid?)e.InstanceData);
                }

                ReadResourceActivity read = e.Activity as ReadResourceActivity;
                if (read == null || read.Resource == null)
                {
                    return;
                }

                List<UpdateRequestParameter> changes = new List<UpdateRequestParameter>();
                foreach (UpdateRequestParameter parameter in this.PendingRequests[resource])
                {
                    // Read the current value of the attribute on the target resource
                    object currentValue = read.Resource[parameter.PropertyName];
                    if (currentValue != null)
                    {
                        // FIM inconsistently returns reference values as either UniqueIdentifiers or Guids
                        // To prevent repetitive checks, convert all UniqueIdentifiers to Guids,
                        // and any List<UniqueIdentifier> to List<Guid>
                        if (currentValue.GetType() == typeof(UniqueIdentifier))
                        {
                            currentValue = ((UniqueIdentifier)currentValue).GetGuid();
                        }
                        else if (currentValue.GetType() == typeof(List<UniqueIdentifier>))
                        {
                            List<Guid> guidList = (from id in (List<UniqueIdentifier>)currentValue select id.GetGuid()).ToList();
                            currentValue = guidList;
                        }
                    }

                    // Determine if the request represents a change based on type
                    bool change = false;
                    if (parameter.Mode == UpdateMode.Modify)
                    {
                        if (parameter.Value == null)
                        {
                            // If the new value is null, only submit the request parameter if the current value is not null
                            change = currentValue != null;
                        }
                        else if (parameter.Value.GetType().IsGenericType && parameter.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
                        {
                            // If the new value is multi-valued, assume we are writing to a multi-valued attribute
                            // When the current value is null, simply add all new values
                            if (currentValue == null)
                            {
                                changes.AddRange(from object o in (IEnumerable)parameter.Value select new UpdateRequestParameter(parameter.PropertyName, UpdateMode.Insert, o));
                            }
                            else
                            {
                                if (!(currentValue is IEnumerable))
                                {
                                    currentValue = new object[] { currentValue };
                                }

                                // Identify all values that exist in the new list but not in the current list
                                // and submit them as insert request parameters
                                changes.AddRange(from object o in (IEnumerable)parameter.Value let contains = ((IEnumerable)currentValue).Cast<object>().Contains(o) where !contains select new UpdateRequestParameter(parameter.PropertyName, UpdateMode.Insert, o));

                                // Identify all values that exist in the current list but not in the new list
                                // and submit them as remove request parameters
                                changes.AddRange(from object o in (IEnumerable)currentValue let contains = ((IEnumerable)parameter.Value).Cast<object>().Contains(o) where !contains select new UpdateRequestParameter(parameter.PropertyName, UpdateMode.Remove, o));
                            }
                        }
                        else
                        {
                            // If the new value is not null and is single-valued,
                            // determine if it represents a change to the existing attribute value
                            if (currentValue == null)
                            {
                                change = true;
                            }
                            else
                            {
                                change = !currentValue.Equals(parameter.Value);
                            }
                        }
                    }
                    else
                    {
                        // Check if the insert or remove operation will result in a change
                        // based on the values currently in the target attribute
                        if (currentValue == null)
                        {
                            change = parameter.Mode == UpdateMode.Insert;
                        }
                        else if (parameter.Value != null && currentValue.GetType().IsGenericType && currentValue.GetType().GetGenericTypeDefinition() == typeof(List<>))
                        {
                            bool contains = ((IEnumerable)currentValue).Cast<object>().Any(o => o.Equals(parameter.Value));

                            switch (parameter.Mode)
                            {
                                case UpdateMode.Insert:
                                    change = !contains;
                                    break;
                                case UpdateMode.Remove:
                                    change = contains;
                                    break;
                            }
                        }
                        else
                        {
                            // Current value is not null and is not multivalued or we are trying to insert or remove a null value
                            // The second possibility should have been caught earlier, while the second indicates that the calling activity has been misconfigured
                            // Let the request through so the FIM Service can throw an error if appropriate
                            change = true;
                        }
                    }

                    // If the request parameter will result in a change, add it to the change list
                    // Note that manipulation of multi-valued attributes via modify (overwrite) will have been
                    // addressed via direct insertion of request parameters to the changes collection
                    // In that scenario, change will always be false to prevent inclusion of the original request parameter
                    if (change)
                    {
                        changes.Add(parameter);
                    }
                }

                // If we have changes, add the request to the list that will be submitted
                if (changes.Count > 0)
                {
                    this.UpdateRequests.Add(resource, changes);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsForEachPendingChildCompleted, "Resource: '{0}'.", e.InstanceData);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the PrepareUpdate CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PrepareUpdate_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsPrepareUpdateExecuteCode, "Update Requests Count: '{0}'.", this.UpdateRequests.Count);

            try
            {
                // If there are requests that need to be submitted to fulfill the updates,
                // assign the list of targets to the for each loop which will submit them
                if (this.UpdateRequests.Count > 0)
                {
                    this.ForEachRequest.InitialChildData = this.UpdateRequests.Keys.ToList();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsPrepareUpdateExecuteCode, "Update Requests Count: '{0}'.", this.UpdateRequests.Count);
            }
        }

        /// <summary>
        /// Handles the ChildInitialized event of the ForEachRequest ReplicatorActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReplicatorChildEventArgs"/> instance containing the event data.</param>
        private void ForEachRequest_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsForEachRequestChildInitialized, "Resource: '{0}'.", e.InstanceData);

            try
            {
                // Cast the instance data as a Guid and prepare the update resource activity
                // by assigning a resource ID for update and the array of update parameters
                Guid resource = new Guid();
                if (e.InstanceData != null)
                {
                    resource = (Guid)((Guid?)e.InstanceData);
                }

                this.CurrentTargetResource = resource;
                this.CurrentRequestParameters = this.UpdateRequests[resource].ToArray();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsForEachRequestChildInitialized, "Resource: '{0}'.", e.InstanceData);
            }
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the Authorization Condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void Authorization_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.UpdateLookupsAuthorizationCondition);

            try
            {
                e.Result = this.ApplyAuthorizationPolicy;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.UpdateLookupsAuthorizationCondition, "Condition evaluated '{0}'.", e.Result);
            }
        }

        #endregion

        #endregion
    }
}