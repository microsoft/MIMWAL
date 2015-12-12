//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="AsynchronousUpdateResource.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// AsynchronousUpdateResource class
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
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Asynchronously submits Update Resource request
    /// </summary>
    internal partial class AsynchronousUpdateResource : Activity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActorIdProperty =
            DependencyProperty.Register("ActorId", typeof(Guid), typeof(AsynchronousUpdateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ResourceIdProperty =
            DependencyProperty.Register("ResourceId", typeof(Guid), typeof(AsynchronousUpdateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty UpdateParametersProperty =
            DependencyProperty.Register("UpdateParameters", typeof(UpdateRequestParameter[]), typeof(AsynchronousUpdateResource));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ApplyAuthorizationPolicyProperty =
            DependencyProperty.Register("ApplyAuthorizationPolicy", typeof(bool), typeof(AsynchronousUpdateResource));

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
        /// Initializes a new instance of the <see cref="AsynchronousUpdateResource"/> class.
        /// </summary>
        public AsynchronousUpdateResource()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncUpdateResourceConstructor);

            try
            {
                this.InitializeComponent();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncUpdateResourceConstructor);
            } 
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the actor identifier to use for the update request.
        /// </summary>
        [Description("The resource ID of the actor to use for the update request.")]
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
        /// Gets or sets the resource identifier of the target for the update request.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        [Description("The resource ID of the target for the update request.")]
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
        /// Gets or sets the update parameters for the request.
        /// </summary>
        /// <value>
        /// The update parameters.
        /// </value>
        [Description("The update parameters for the request.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public UpdateRequestParameter[] UpdateParameters
        {
            get
            {
                return (UpdateRequestParameter[])this.GetValue(UpdateParametersProperty);
            }

            set
            {
                this.SetValue(UpdateParametersProperty, value);
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
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ResourceManagement", Justification = "Reviewed and overruled as it's part of the type name.")]
        protected override void OnActivityExecutionContextLoad(IServiceProvider provider)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncUpdateResourceOnActivityExecutionContextLoad);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncUpdateResourceOnActivityExecutionContextLoad);
            } 
        }

        /// <summary>
        /// Executes the activity.
        /// </summary>
        /// <param name="executionContext">The execution context.</param>
        /// <returns>The <see cref="ActivityExecutionStatus"/> object.</returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncUpdateResourceExecute);

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

                object[] args = new object[] { this.ActorId, this.ResourceId, this.UpdateParameters, parentWorkflow.RequestId, this.ApplyAuthorizationPolicy, 0, queue.QueueName };

                Logger.Instance.WriteVerbose(EventIdentifier.AsyncUpdateResourceExecute, "Invoking DataAccessService.Update() for ActorId: '{0}', ResourceId: '{1}', ParentRequestId: '{3}' ApplyAuthorizationPolicy: '{4}' and QueueName: '{6}'.", args);

                // Submit the request via reflection and cleanup the queue
                // Added the AuthorizationTimeOut value of 0, which is "Fire and Forget"
                this.dataAccessServiceType.InvokeMember(
                    "Update",
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
                throw Logger.Instance.ReportError(EventIdentifier.AsyncUpdateResourceExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncUpdateResourceExecute);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncUpdateResourceCreateWorkflowProgramQueue);

            try
            {
                string queueName = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", activity.QualifiedName, Guid.NewGuid());

                Logger.Instance.WriteVerbose(EventIdentifier.AsyncUpdateResourceCreateWorkflowProgramQueue, "AsynchronousUpdateResource workflow queue name is: '{0}'.", queueName);

                return workflowQueuingService.CreateWorkflowQueue(queueName, false);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncUpdateResourceCreateWorkflowProgramQueue);
            } 
        }

        /// <summary>
        /// Deletes the workflow program queue.
        /// </summary>
        /// <param name="workflowQueuingService">The workflow queuing service.</param>
        /// <param name="queue">The queue name.</param>
        private static void DeleteWorkflowProgramQueue(WorkflowQueuingService workflowQueuingService, WorkflowQueue queue)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.AsyncUpdateResourceDeleteWorkflowProgramQueue);

            try
            {
                if (workflowQueuingService.Exists(queue.QueueName))
                {
                    workflowQueuingService.DeleteWorkflowQueue(queue.QueueName);

                    Logger.Instance.WriteVerbose(EventIdentifier.AsyncUpdateResourceDeleteWorkflowProgramQueue, "AsynchronousUpdateResource deleted workflow queue: '{0}'.", queue.QueueName);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.AsyncUpdateResourceDeleteWorkflowProgramQueue);
            } 
        }
    
        #endregion
    }
}