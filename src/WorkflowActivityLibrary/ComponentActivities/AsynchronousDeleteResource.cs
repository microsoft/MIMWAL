//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="AsynchronousDeleteResource.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// AsynchronousDeleteResource class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    #region Namespaces Declarations

    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Reflection;
    using System.Workflow.ComponentModel;
    using System.Workflow.Runtime;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Asynchronously submits Delete Resource request
    /// </summary>
    internal partial class AsynchronousDeleteResource : Activity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorIdProperty =
            DependencyProperty.Register("ActorId", typeof(Guid), typeof(AsynchronousDeleteResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ResourceIdProperty =
            DependencyProperty.Register("ResourceId", typeof(Guid), typeof(AsynchronousDeleteResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApplyAuthorizationPolicyProperty =
            DependencyProperty.Register("ApplyAuthorizationPolicy", typeof(bool), typeof(AsynchronousDeleteResource));

        #endregion

        #region Declarations

        /// <summary>
        /// The data access service
        /// </summary>
        [NonSerialized]
        private object dataAccessService;

        /// <summary>
        /// The data access service type
        /// </summary>
        [NonSerialized]
        private Type dataAccessServiceType;

        /// <summary>
        /// The workflow queuing service
        /// </summary>
        [NonSerialized]
        private WorkflowQueuingService queuingService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AsynchronousDeleteResource"/> class.
        /// </summary>
        public AsynchronousDeleteResource()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncDeleteResourceConstructor);

            try
            {
                this.InitializeComponent();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncDeleteResourceConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the actor identifier to use for the delete request.
        /// </summary>
        [Description("The resource ID of the actor to use for the delete request.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ActorId
        {
            get
            {
                return (Guid)this.GetValue(ActorIdProperty);
            }

            set
            {
                this.SetValue(ActorIdProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the resource identifier of the resource to be deleted.
        /// </summary>
        [Description("The ID of the resource to be deleted.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ResourceId
        {
            get
            {
                return (Guid)this.GetValue(ResourceIdProperty);
            }

            set
            {
                this.SetValue(ResourceIdProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether child requests will be subjected to authorization workflow..
        /// </summary>
        [Description("When enabled, child requests will be subjected to authorization workflow.")]
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
        /// This method is called during the creation of an ActivityExecutionContext as well as every time the ActivityExecutionContext is reincarnated when a workflow instance is loaded from persistent storage.
        /// </summary>
        /// <param name="provider">The <see cref="IServiceProvider"/> that provides the service.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Reviewed and overruled.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ResourceManagement", Justification = "Reviewed and retained.")]
        protected override void OnActivityExecutionContextLoad(IServiceProvider provider)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncDeleteResourceOnActivityExecutionContextLoad);

            try
            {
                Logger.SetContextItem(this, this.WorkflowInstanceId);

                base.OnActivityExecutionContextLoad(provider);

                // Load the data access service via reflection
                Assembly assembly = Assembly.GetAssembly(typeof(UpdateResourceActivity));

                const string TypeName = "Microsoft.ResourceManagement.Workflow.Runtime.DataAccessService";
                this.dataAccessServiceType = assembly.GetType(TypeName);
                if (this.dataAccessServiceType == null)
                {
                    throw Logger.Instance.ReportError(new WorkflowActivityLibraryException(Messages.DefinitionsConverter_NullOrEmptyDefinitionsTableError, TypeName));
                }

                this.dataAccessService = provider.GetService(this.dataAccessServiceType);
                this.queuingService = (WorkflowQueuingService)provider.GetService(typeof(WorkflowQueuingService));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncDeleteResourceOnActivityExecutionContextLoad);
            }
        }

        /// <summary>
        /// Executes the activity.
        /// </summary>
        /// <param name="executionContext">The execution context.</param>
        /// <returns>The <see cref="ActivityExecutionStatus"/> object.</returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncDeleteResourceExecute);

            try
            {
                // Ideally we would set CallContext in OnActivityExecutionContextLoad instead here in Execute
                // as OnActivityExecutionContextLoad gets called on each hydration and rehydration of the workflow instance
                // but looks like it's invoked on a different thread context than the rest of the workflow instance execution.
                // To minimize the loss of the CallContext on rehydration, we'll set it in the Execute of every WAL child activities.
                // It will still get lost (momentarily) when the workflow is persisted in the middle of the execution of a replicator activity, for example.
                Logger.SetContextItem(this, this.WorkflowInstanceId);

                SequentialWorkflow parentWorkflow;
                if (!SequentialWorkflow.TryGetContainingWorkflow(this, out parentWorkflow))
                {
                    throw Logger.Instance.ReportError(new InvalidOperationException(Messages.UnableToGetParentWorkflowError));
                }

                // Create the workflow program queue so we can facilitate asynchronous
                // submission of requests
                WorkflowQueue queue = CreateWorkflowProgramQueue(this.queuingService, this);

                object[] args = new object[] { this.ActorId, this.ResourceId, parentWorkflow.RequestId, this.ApplyAuthorizationPolicy, 0, queue.QueueName };

                Logger.Instance.WriteVerbose(EventIdentifier.AsyncDeleteResourceExecute, "Invoking DataAccessService.Delete() for ActorId: '{0}', ResourceId: '{1}', ParentRequestId: '{2}' ApplyAuthorizationPolicy: '{3}' and QueueName: '{5}'.", args);

                // Submit the request via reflection and cleanup the queue
                // Added the AuthorizationTimeOut value of 0, which is "Fire and Forget"
                this.dataAccessServiceType.InvokeMember(
                    "Delete",
                    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                    null,
                    this.dataAccessService,
                    args,
                    CultureInfo.InvariantCulture);

                DeleteWorkflowProgramQueue(this.queuingService, queue);

                return base.Execute(executionContext);
            }
            catch (Exception ex)
            {
                throw Logger.Instance.ReportError(EventIdentifier.AsyncDeleteResourceExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncDeleteResourceExecute);
            }
        }

        /// <summary>
        /// Creates the workflow program queue.
        /// </summary>
        /// <param name="workflowQueuingService">The workflow queuing service.</param>
        /// <param name="activity">The activity to be used for deriving the queue name.</param>
        /// <returns>The <see cref="WorkflowQueue"/> object.</returns>
        private static WorkflowQueue CreateWorkflowProgramQueue(WorkflowQueuingService workflowQueuingService, Activity activity)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncDeleteResourceCreateWorkflowProgramQueue);

            try
            {
                string queueName = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", activity.QualifiedName, Guid.NewGuid());

                Logger.Instance.WriteVerbose(EventIdentifier.AsyncDeleteResourceCreateWorkflowProgramQueue, "AsynchronousDeleteResource workflow queue name is: '{0}'.", queueName);

                return workflowQueuingService.CreateWorkflowQueue(queueName, false);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncDeleteResourceCreateWorkflowProgramQueue);
            }
        }

        /// <summary>
        /// Deletes the workflow program queue.
        /// </summary>
        /// <param name="workflowQueuingService">The workflow queuing service.</param>
        /// <param name="queue">The queue name.</param>
        private static void DeleteWorkflowProgramQueue(WorkflowQueuingService workflowQueuingService, WorkflowQueue queue)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncDeleteResourceDeleteWorkflowProgramQueue);

            try
            {
                if (workflowQueuingService.Exists(queue.QueueName))
                {
                    workflowQueuingService.DeleteWorkflowQueue(queue.QueueName);

                    Logger.Instance.WriteVerbose(EventIdentifier.AsyncDeleteResourceDeleteWorkflowProgramQueue, "AsynchronousDeleteResource deleted workflow queue: '{0}'.", queue.QueueName);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncDeleteResourceDeleteWorkflowProgramQueue);
            }
        }

        #endregion
    }
}