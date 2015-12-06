//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="FindResources.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// FindResources class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    #region Namespaces Declarations

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;

    #endregion
    
    /// <summary>
    /// Finds resources
    /// </summary>
    internal partial class FindResources : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty XPathFilterProperty =
            DependencyProperty.Register("XPathFilter", typeof(string), typeof(FindResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(FindResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AttributesProperty =
            DependencyProperty.Register("Attributes", typeof(string[]), typeof(FindResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ExcludeWorkflowTargetProperty =
            DependencyProperty.Register("ExcludeWorkflowTarget", typeof(bool), typeof(FindResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty FoundResourcesProperty =
            DependencyProperty.Register("FoundResources", typeof(List<ResourceType>), typeof(FindResources));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty FoundIdsProperty =
            DependencyProperty.Register("FoundIds", typeof(List<Guid>), typeof(FindResources));

        #endregion

        #region Declarations

        /// <summary>
        /// The service actor.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Bound to an activity.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public Guid ServiceActor;

        /// <summary>
        /// The workflow target
        /// </summary>
        private Guid workflowTarget;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FindResources"/> class.
        /// </summary>
        public FindResources()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindResourcesConstructor);

            try
            {
                this.InitializeComponent();

                this.ServiceActor = WellKnownGuids.FIMServiceAccount;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindResourcesConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the XPath filter used to find resources..
        /// </summary>
        [Description("The resolvable XPath filter used to find resources.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string XPathFilter
        {
            get
            {
                return (string)this.GetValue(XPathFilterProperty);
            }

            set
            {
                this.SetValue(XPathFilterProperty, value);
            }
        }
        
        /// <summary>
        /// Gets or sets the value which should be used for [//Value/...] lookup resolution.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Reviewed")]
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
        /// Gets or sets the attributes which should be included on returned resources. If not specified, only ObjectID will be included.
        /// </summary>
        [Description("The attributes which should be included on returned resources. If not specified, only ObjectID will be included.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string[] Attributes
        {
            get
            {
                return (string[])this.GetValue(AttributesProperty);
            }

            set
            {
                this.SetValue(AttributesProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the workflow target should be excluded from returned resources if found in the result set.
        /// </summary>
        [Description("Specifies whether the workflow target should be excluded from returned resources if found in the result set.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ExcludeWorkflowTarget
        {
            get
            {
                return (bool)this.GetValue(ExcludeWorkflowTargetProperty);
            }

            set
            {
                this.SetValue(ExcludeWorkflowTargetProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the resources found by the activity with all specified attributes.
        /// </summary>
        [Description("The list of resources found by the activity with all specified attributes.")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<ResourceType> FoundResources
        {
            get
            {
                return (List<ResourceType>)this.GetValue(FoundResourcesProperty);
            }

            set
            {
                this.SetValue(FoundResourcesProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the list of unique identifiers for the resources found by the activity..
        /// </summary>
        [Description("The list of Guid identifiers for the resources found by the activity.")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<Guid> FoundIds
        {
            get
            {
                return (List<Guid>)this.GetValue(FoundIdsProperty);
            }

            set
            {
                this.SetValue(FoundIdsProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindResourcesExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.FindResourcesExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindResourcesExecute);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindResourcesPrepareExecuteCode);

            try
            {
                // Prepare the output lists
                this.FoundResources = new List<ResourceType>();
                this.FoundIds = new List<Guid>();

                // Retrieve the target resource for the workflow
                SequentialWorkflow parentWorkflow;
                if (SequentialWorkflow.TryGetContainingWorkflow(this, out parentWorkflow))
                {
                    this.workflowTarget = parentWorkflow.TargetId;
                }

                // Throw an exception if the XPath filter was not supplied for the activity
                if (string.IsNullOrEmpty(this.XPathFilter))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.FindResourcesPrepareExecuteCodeMissingXPathError, new InvalidOperationException(Messages.FindResources_MissingXPathError));
                }

                // Configure the selection attributes for the Enumerate Resources activity
                // If attributes were not supplied for the activity, default to just the ObjectID to reduce the overhead
                // associated with retrieving all attributes for enumerated resources
                if (this.Attributes != null && this.Attributes.Length > 0)
                {
                    this.Find.Selection = this.Attributes;
                }
                else
                {
                    this.Find.Selection = new string[] { "ObjectID" };
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindResourcesPrepareExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ReadFoundResource CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReadFoundResource_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.FindResourcesReadFoundResourceExecuteCode, "XPathFilter: '{0}'.", this.XPathFilter);

            try
            {
                // This method is called for each result returned by the enumerate resources activity
                // Cast the returned object as a resource and retrieve the Guid
                ResourceType resource = EnumerateResourcesActivity.GetCurrentIterationItem((CodeActivity)sender) as ResourceType;
                if (resource == null)
                {
                    Logger.Instance.WriteVerbose(EventIdentifier.FindResourcesReadFoundResourceExecuteCode, "No resource found matching XPath filter '{0}'.", this.XPathFilter);
                    return;
                }

                Guid identifier;
                resource.ObjectID.TryGetGuid(out identifier);

                // If the resource is not the workflow target, or if the activity is not configured
                // to exclude the workflow target from the result set, add the resource and associated Guid
                // to the lists which will be returned by the activity
                if (identifier == Guid.Empty || (identifier == this.workflowTarget && this.ExcludeWorkflowTarget))
                {
                    Logger.Instance.WriteVerbose(EventIdentifier.FindResourcesReadFoundResourceExecuteCode, "The resource is the workflow target and the activity is configured to exclude workflow target from the found resources result set.");
                    return;
                }

                this.FoundResources.Add(resource);
                this.FoundIds.Add(identifier);

                Logger.Instance.WriteVerbose(EventIdentifier.FindResourcesReadFoundResourceExecuteCode, "The resource '{0}' of type '{1}' is added to the found resources collection.", resource.ObjectID, resource.ObjectType);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.FindResourcesReadFoundResourceExecuteCode, "XPathFilter: '{0}'.", this.XPathFilter);
            }
        }
    
        #endregion
    }
}