//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="RunPowerShellScript.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// RunPowerShellScript Activity 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;
    using System.Reflection;
    using System.Security;
    using System.Security.Principal;
    using System.Text;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Runs PowerShell Script
    /// </summary>
    public partial class RunPowerShellScript : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityDisplayNameProperty =
            DependencyProperty.Register("ActivityDisplayName", typeof(string), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ActivityExecutionConditionProperty =
            DependencyProperty.Register("ActivityExecutionCondition", typeof(string), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ScriptLocationProperty =
            DependencyProperty.Register("ScriptLocation", typeof(PowerShellScriptLocation), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ScriptProperty =
            DependencyProperty.Register("Script", typeof(string), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty FailOnMissingProperty =
            DependencyProperty.Register("FailOnMissing", typeof(bool), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty InputTypeProperty =
            DependencyProperty.Register("InputType", typeof(PowerShellInputType), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ParametersTableProperty =
            DependencyProperty.Register("ParametersTable", typeof(Hashtable), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ArgumentsProperty =
            DependencyProperty.Register("Arguments", typeof(ArrayList), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ReturnTypeProperty =
            DependencyProperty.Register("ReturnType", typeof(PowerShellReturnType), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ReturnValueLookupProperty =
            DependencyProperty.Register("ReturnValueLookup", typeof(string), typeof(RunPowerShellScript));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty AdvancedProperty =
            DependencyProperty.Register("Advanced", typeof(bool), typeof(RunPowerShellScript));

        /// <summary>
        /// DependencyProperty for PowerShell User
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty PowerShellUserProperty = DependencyProperty.Register("PowerShellUser", typeof(string), typeof(RunPowerShellScript));

        /// <summary>
        /// DependencyProperty for PowerShell Password
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty PowerShellUserPasswordProperty = DependencyProperty.Register("PowerShellUserPassword", typeof(string), typeof(RunPowerShellScript));

        /// <summary>
        /// DependencyProperty for Impersonate PowerShell User
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ImpersonatePowerShellUserProperty = DependencyProperty.Register("ImpersonatePowerShellUser", typeof(bool), typeof(RunPowerShellScript));

        /// <summary>
        /// DependencyProperty for Impersonate PowerShell User Logon Type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ImpersonatePowerShellUserLogOnTypeProperty = DependencyProperty.Register("ImpersonatePowerShellUserLogOnType", typeof(LogOnType), typeof(RunPowerShellScript));

        /// <summary>
        /// DependencyProperty for Impersonate PowerShell User Load User Profile
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ImpersonatePowerShellUserLoadUserProfileProperty = DependencyProperty.Register("ImpersonatePowerShellUserLoadUserProfile", typeof(bool), typeof(RunPowerShellScript));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The parameter definitions.
        /// </summary>
        private List<Definition> parameters = new List<Definition>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RunPowerShellScript"/> class.
        /// </summary>
        public RunPowerShellScript()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RunPowerShellScriptConstructor);

            try
            {
                this.InitializeComponent();

                if (this.ActivityExpressionEvaluator == null)
                {
                    this.ActivityExpressionEvaluator = new ExpressionEvaluator();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RunPowerShellScriptConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the display name of the activity.
        /// </summary>
        [Description("ActivityDisplayName")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ActivityDisplayName
        {
            get
            {
                return (string)this.GetValue(ActivityDisplayNameProperty);
            }

            set
            {
                this.SetValue(ActivityDisplayNameProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        [Description("Activity Execution Condition")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ActivityExecutionCondition
        {
            get
            {
                return (string)this.GetValue(ActivityExecutionConditionProperty);
            }

            set
            {
                this.SetValue(ActivityExecutionConditionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the script location.
        /// </summary>
        [Description("ScriptLocation")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PowerShellScriptLocation ScriptLocation
        {
            get
            {
                return (PowerShellScriptLocation)this.GetValue(ScriptLocationProperty);
            }

            set
            {
                this.SetValue(ScriptLocationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        [Description("Script")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Script
        {
            get
            {
                return (string)this.GetValue(ScriptProperty);
            }

            set
            {
                this.SetValue(ScriptProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to fail on missing script.
        /// </summary>
        [Description("FailOnMissing")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool FailOnMissing
        {
            get
            {
                return (bool)this.GetValue(FailOnMissingProperty);
            }

            set
            {
                this.SetValue(FailOnMissingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the input to the script.
        /// </summary>
        [Description("InputType")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PowerShellInputType InputType
        {
            get
            {
                return (PowerShellInputType)this.GetValue(InputTypeProperty);
            }

            set
            {
                this.SetValue(InputTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the script parameters table.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("ParametersTable")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Hashtable ParametersTable
        {
            get
            {
                return (Hashtable)this.GetValue(ParametersTableProperty);
            }

            set
            {
                this.SetValue(ParametersTableProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the script arguments.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed.")]
        [Description("Arguments")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ArrayList Arguments
        {
            get
            {
                return (ArrayList)this.GetValue(ArgumentsProperty);
            }

            set
            {
                this.SetValue(ArgumentsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the script return output.
        /// </summary>
        [Description("ReturnType")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PowerShellReturnType ReturnType
        {
            get
            {
                return (PowerShellReturnType)this.GetValue(ReturnTypeProperty);
            }

            set
            {
                this.SetValue(ReturnTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the return value lookup.
        /// </summary>
        [Description("ReturnValueLookup")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ReturnValueLookup
        {
            get
            {
                return (string)this.GetValue(ReturnValueLookupProperty);
            }

            set
            {
                this.SetValue(ReturnValueLookupProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether advanced checkbox is checked.
        /// </summary>
        [Description("Advanced")]
        [Category("Settings")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Advanced
        {
            get
            {
                return (bool)this.GetValue(AdvancedProperty);
            }

            set
            {
                this.SetValue(AdvancedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the return value lookup.
        /// </summary>
        [Description("PowerShellUser")]
        [Category("Alternate Credentials")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PowerShellUser
        {
            get
            {
                return (string)this.GetValue(PowerShellUserProperty);
            }

            set
            {
                this.SetValue(PowerShellUserProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the return value lookup.
        /// </summary>
        [Description("PowerShellUserPassword")]
        [Category("Alternate Credentials")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PowerShellUserPassword
        {
            get
            {
                return (string)this.GetValue(PowerShellUserPasswordProperty);
            }

            set
            {
                this.SetValue(PowerShellUserPasswordProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to impersonate the PowerShell user or not.
        /// </summary>
        [Description("ImpersonatePowerShellUser")]
        [Category("Alternate Credentials")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ImpersonatePowerShellUser
        {
            get
            {
                return (bool)this.GetValue(ImpersonatePowerShellUserProperty);
            }

            set
            {
                this.SetValue(ImpersonatePowerShellUserProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the return value lookup.
        /// </summary>
        [Description("ImpersonatePowerShellUserLogOnType")]
        [Category("Alternate Credentials")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public LogOnType ImpersonatePowerShellUserLogOnType
        {
            get
            {
                return (LogOnType)this.GetValue(ImpersonatePowerShellUserLogOnTypeProperty);
            }

            set
            {
                this.SetValue(ImpersonatePowerShellUserLogOnTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to load user profile or not when impersonating user.
        /// </summary>
        [Description("ImpersonatePowerShellUserLoadUserProfile")]
        [Category("Alternate Credentials")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ImpersonatePowerShellUserLoadUserProfile
        {
            get
            {
                return (bool)this.GetValue(ImpersonatePowerShellUserLoadUserProfileProperty);
            }

            set
            {
                this.SetValue(ImpersonatePowerShellUserLoadUserProfileProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.RunPowerShellScriptExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RunPowerShellScriptExecute);
            }
        }

        /// <summary>
        /// Sets up the PowerShell shell stream event handlers.
        /// </summary>
        /// <param name="shell">The PowerShell shell.</param>
        private static void SetupStreamEventHandlers(PowerShell shell)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RunPowerShellScriptSetupStreamEventHandlers);

            try
            {
                shell.Streams.ClearStreams();
                shell.Streams.Error.DataAdded += delegate(object sender, DataAddedEventArgs e)
                {
                    PSDataCollection<ErrorRecord> errorStream = (PSDataCollection<ErrorRecord>)sender;
                    ErrorRecord record = errorStream[e.Index];
                    if (record == null)
                    {
                        return;
                    }

                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine(record.ToString());

                    if (record.InvocationInfo != null)
                    {
                        builder.AppendLine();
                        builder.AppendLine(record.InvocationInfo.PositionMessage);
                    }

                    Logger.Instance.WriteError(EventIdentifier.RunPowerShellScriptSetupStreamEventHandlersErrorEvents, builder.ToString());
                };
                shell.Streams.Warning.DataAdded += delegate(object sender, DataAddedEventArgs e)
                {
                    PSDataCollection<WarningRecord> warningStream = (PSDataCollection<WarningRecord>)sender;
                    WarningRecord record = warningStream[e.Index];
                    if (record != null)
                    {
                        Logger.Instance.WriteWarning(EventIdentifier.RunPowerShellScriptSetupStreamEventHandlersWarningEvents, record.ToString());
                    }
                };
                shell.Streams.Debug.DataAdded += delegate(object sender, DataAddedEventArgs e)
                {
                    PSDataCollection<DebugRecord> debugStream = (PSDataCollection<DebugRecord>)sender;
                    DebugRecord record = debugStream[e.Index];
                    if (record != null)
                    {
                        Logger.Instance.WriteInfo(EventIdentifier.RunPowerShellScriptSetupStreamEventHandlersDebugEvents, record.ToString());
                    }
                };
                shell.Streams.Verbose.DataAdded += delegate(object sender, DataAddedEventArgs e)
                {
                    PSDataCollection<VerboseRecord> versboseStream = (PSDataCollection<VerboseRecord>)sender;
                    VerboseRecord record = versboseStream[e.Index];
                    if (record != null)
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.RunPowerShellScriptSetupStreamEventHandlersEvents, record.ToString());
                    }
                };
                shell.Streams.Progress.DataAdded += delegate(object sender, DataAddedEventArgs e)
                {
                    PSDataCollection<ProgressRecord> progressStream = (PSDataCollection<ProgressRecord>)sender;
                    ProgressRecord record = progressStream[e.Index];
                    if (record != null)
                    {
                        Logger.Instance.WriteInfo(EventIdentifier.RunPowerShellScriptSetupStreamEventHandlersProgressEvents, record.ToString());
                    }
                };
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RunPowerShellScriptSetupStreamEventHandlers);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Prepare CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Prepare_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RunPowerShellScriptPrepareExecuteCode);

            try
            {
                // If advanced options were not specified,
                // clear any supplied advanced settings so they do not impact activity execution
                if (!this.Advanced)
                {
                    this.ActivityExecutionCondition = null;
                    this.PowerShellUser = null;
                    this.PowerShellUserPassword = null;
                    this.ImpersonatePowerShellUser = false;
                    this.ImpersonatePowerShellUserLoadUserProfile = false;
                    this.ImpersonatePowerShellUserLogOnType = LogOnType.None;
                }

                // If the script needs to be resolved, parse the expression via the
                // expression evaluator to prepare for lookup resolution
                if (this.ScriptLocation == PowerShellScriptLocation.Resource)
                {
                    this.ActivityExpressionEvaluator.ParseExpression(this.Script);
                }

                // If the activity is configured for conditional execution, parse the associated expression
                this.ActivityExpressionEvaluator.ParseIfExpression(this.ActivityExecutionCondition);

                if (this.InputType == PowerShellInputType.Arguments &&
                    this.Arguments != null &&
                    this.Arguments.Count > 0)
                {
                    // If we're supplying arguments to the PowerShell script,
                    // parse each argument in the list to facilitate resolution
                    foreach (string s in this.Arguments)
                    {
                        this.ActivityExpressionEvaluator.ParseExpression(s);
                    }
                }
                else if (this.InputType == PowerShellInputType.Parameters &&
                         this.ParametersTable != null &&
                         this.ParametersTable.Count > 0)
                {
                    // If we're supplying named parameters ot the PowerShell script,
                    // parse the right side of the parameter listing which represents the value
                    DefinitionsConverter parametersConverter = new DefinitionsConverter(this.ParametersTable);
                    this.parameters = parametersConverter.Definitions;
                    foreach (Definition parameter in this.parameters)
                    {
                        this.ActivityExpressionEvaluator.ParseExpression(parameter.Right);
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RunPowerShellScriptPrepareExecuteCode);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Run CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Run_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RunPowerShellScriptRunExecuteCode, "Script Location: '{0}'.", this.ScriptLocation);

            try
            {
                // The value supplied for the script property may represent a number of things
                // Depending on the supplied script location, determine if the value represents a path to a script file,
                // a lookup to be resolved, or the script itself stored in the workflow definition
                string script = string.Empty;
                switch (this.ScriptLocation)
                {
                    case PowerShellScriptLocation.Resource:
                        // If the script needs to be resolved, the supplied script value will be the lookup from which the script should be read
                        // Resolve it using the expression evaluator and throw an error, if configured to do so, when it resolves to null
                        object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.Script);
                        if (resolved != null)
                        {
                            script = resolved.ToString();
                        }
                        else if (this.FailOnMissing)
                        {
                            throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptRunExecuteCodeScriptNotFoundError, new WorkflowActivityLibraryException(Messages.RunPowerShellActivity_ScriptNotFoundError, this.Script));
                        }

                        break;
                    case PowerShellScriptLocation.Disk:
                        // If the script needs to be read from disk, the supplied script value will be the script file path
                        // Attempt to read it and throw an error if it is not found
                        if (File.Exists(this.Script))
                        {
                            using (StreamReader reader = new StreamReader(this.Script))
                            {
                                script = reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptRunExecuteCodeScriptNotFoundError, new WorkflowActivityLibraryException(Messages.RunPowerShellActivity_ScriptNotFoundError, this.Script));
                        }

                        break;
                    case PowerShellScriptLocation.WorkflowDefinition:
                        // When the script is stored in the workflow definition, the supplied script value will be the script itself
                        script = this.Script;
                        break;
                }

                if (string.IsNullOrEmpty(script))
                {
                    return;
                }

                ArrayList arguments = null;
                Dictionary<string, object> scriptParameters = null;
                switch (this.InputType)
                {
                    case PowerShellInputType.Parameters:
                        // If named parameters were supplied as input, resolve each value expression
                        // and add them to the PowerShell instance
                        scriptParameters = new Dictionary<string, object>();
                        foreach (Definition parameter in this.parameters)
                        {
                            object resolved = this.ActivityExpressionEvaluator.ResolveExpression(parameter.Right);
                            if (resolved != null)
                            {
                                scriptParameters.Add(parameter.Left, resolved);
                            }
                        }

                        break;
                    case PowerShellInputType.Arguments:
                        // If arguments were supplied as input, resolve each argument value
                        // and add them to the PowerShell instance in the order in which they were supplied
                        arguments = new ArrayList();
                        foreach (string s in this.Arguments)
                        {
                            arguments.Add(this.ActivityExpressionEvaluator.ResolveExpression(s));
                        }

                        break;
                }

                // Run the PowerShell script and capture the results
                object result = this.RunScript(script, arguments, scriptParameters);
                this.PublishOutput.UpdateLookupDefinitions = new List<UpdateLookupDefinition>();
                switch (this.ReturnType)
                {
                    case PowerShellReturnType.Explicit:
                        // If publishing an explicit result, publish the returned value to the specified lookup
                        if (result == null)
                        {
                            Logger.Instance.WriteWarning(EventIdentifier.RunPowerShellScriptRunExecuteCodeNullReturnValueWarning, "PowerShell script did not return any value for Return Value Lookup: {0}.", this.ReturnValueLookup);
                        }

                        this.PublishOutput.UpdateLookupDefinitions.Add(new UpdateLookupDefinition(this.ReturnValueLookup, result, UpdateMode.Modify));
                        break;
                    case PowerShellReturnType.Table:
                        // If publishing a table of results, ensure a hash table was returned by the script
                        // and publish the value for each key to the workflow dictionary using the hash table key as the dictionary key
                        if (result != null && result.GetType() == typeof(Hashtable))
                        {
                            Hashtable results = (Hashtable)result;
                            foreach (string s in results.Keys)
                            {
                                object value = results[s];

                                // Values may come as PSObject types. In that case, read the base object.
                                if (value.GetType() == typeof(PSObject))
                                {
                                    value = ((PSObject)results[s]).BaseObject;
                                }

                                this.PublishOutput.UpdateLookupDefinitions.Add(new UpdateLookupDefinition(string.Format(CultureInfo.InvariantCulture, "[//WorkflowData/{0}]", s), value, UpdateMode.Modify));
                            }
                        }
                        else
                        {
                            Logger.Instance.WriteWarning(EventIdentifier.RunPowerShellScriptRunExecuteCodeNullHashtableReturnValueWarning, "PowerShell script returned a '{0}' value when the activity was expecting a Hashtable.", result);
                        }

                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RunPowerShellScriptRunExecuteCode, "Script Location: '{0}'.", this.ScriptLocation);
            }
        }

        /// <summary>
        /// Runs the PowerShell script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="scriptArguments">The PowerShell script arguments.</param>
        /// <param name="scriptParameters">The PowerShell script parameters.</param>
        /// <returns>The output of the script.</returns>
        private object RunScript(string script, IEnumerable scriptArguments, Dictionary<string, object> scriptParameters)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RunPowerShellScriptRunScript);

            WindowsImpersonationContext context = null;
            LogOnUser logOnUser = null;

            try
            {
                PSCredential credential = this.GetPowerShellCredential();

                logOnUser = this.LogOnUser(credential);

                if (logOnUser != null)
                {
                    context = logOnUser.Impersonate();
                }

                // Establish a new runspace for the Powershell script
                InitialSessionState initialSessionState = InitialSessionState.CreateDefault();

                bool progressPreference = Logger.Instance.ShouldTrace(TraceEventType.Information);
                bool debugPreference = Logger.Instance.ShouldTrace(TraceEventType.Information);
                bool verbosePreference = Logger.Instance.ShouldTrace(TraceEventType.Verbose);

                initialSessionState.Variables.Add(new SessionStateVariableEntry("ProgressPreference", progressPreference ? "Continue" : "SilentlyContinue", string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("DebugPreference", debugPreference ? "Continue" : "SilentlyContinue", string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("VerbosePreference", verbosePreference ? "Continue" : "SilentlyContinue", string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("AECWorkflowInstanceId", Logger.GetContextItem("WorkflowInstanceId"), string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("AECRequestId", Logger.GetContextItem("RequestId"), string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("AECActorId", Logger.GetContextItem("ActorId"), string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("AECTargetId", Logger.GetContextItem("TargetId"), string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("AECWorkflowDefinitionId", Logger.GetContextItem("WorkflowDefinitionId"), string.Empty));
                initialSessionState.Variables.Add(new SessionStateVariableEntry("Credential", credential, null));

                using (Runspace runspace = RunspaceFactory.CreateRunspace(initialSessionState))
                {
                    runspace.Open();
                    using (PowerShell shell = PowerShell.Create())
                    {
                        shell.Runspace = runspace;

                        SetupStreamEventHandlers(shell);

                        // Add the script to the PowerShell instance to prepare for execution
                        shell.AddScript(script);

                        StringBuilder scriptParams = new StringBuilder();

                        if (scriptParameters != null)
                        {
                            foreach (string s in scriptParameters.Keys)
                            {
                                shell.AddParameter(s, scriptParameters[s]);

                                scriptParams.AppendFormat(CultureInfo.InvariantCulture, "-{0} '{1}' ", s, scriptParameters[s]);
                            }
                        }
                        else if (scriptArguments != null)
                        {
                            foreach (object o in scriptArguments)
                            {
                                shell.AddArgument(o);
                                scriptParams.AppendFormat(CultureInfo.InvariantCulture, "'{0}' ", o);
                            }
                        }

                        if (this.InputType != PowerShellInputType.None)
                        {
                            Logger.Instance.WriteVerbose(EventIdentifier.RunPowerShellScriptRunScript, "The PowerShell script parameters are: '{0}'.", scriptParams);
                        }

                        // Execute the script and identify if any errors occurred
                        // In most circumstances, errors will not be raised as exceptions but will instead
                        // be presented in the Error collection for the PowerShell instance
                        Collection<PSObject> results;

                        try
                        {
                            results = shell.Invoke();
                        }
                        catch (Exception ex)
                        {
                            throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptRunScriptInvocationError, new WorkflowActivityLibraryException(Messages.RunPowerShellScript_ScriptInvocationError, ex, ex.Message));
                        }

                        if (shell.Streams.Error.Count == 0)
                        {
                            if (this.ReturnType == PowerShellReturnType.None)
                            {
                                return null;
                            }

                            if (results != null && results.Count == 1)
                            {
                                return results[0].BaseObject;
                            }

                            if (results == null || results.Count < 1)
                            {
                                return null;
                            }

                            // If multiple values were found for the lookup, verify that they are of a consistent type
                            Type type = null;
                                bool consistentType = true;
                                foreach (PSObject pso in results)
                                {
                                    if (type == null)
                                    {
                                        type = pso.BaseObject.GetType();

                                        Logger.Instance.WriteVerbose(EventIdentifier.RunPowerShellScriptRunScript, "The PowerShell script returned type: '{0}'.", type);
                                    }
                                    else if (pso.BaseObject.GetType() != type)
                                    {
                                        consistentType = false;
                                        Logger.Instance.WriteError(EventIdentifier.RunPowerShellScriptRunScriptInconsistentScriptReturnTypeError, Messages.RunPowerShellScript_InconsistentScriptReturnTypeError, pso.BaseObject.GetType(), type);
                                    }
                                }

                                // If we have multiple values of an inconsistent type, there is a problem
                                // which needs to be addressed by the administrator
                                if (!consistentType)
                                {
                                    throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptRunScriptInconsistentScriptReturnTypeError, new WorkflowActivityLibraryException(Messages.RunPowerShellScript_InvalidReturnTypeError));
                                }

                                // Because we have multiple values returned for the PowerShell script, 
                                // we want to return them in the form of a strongly-typed list
                                // For example: List<string> instead of List<object>
                                // Use reflection to create a new strongly-typed list
                                Type listType = typeof(List<>).MakeGenericType(new Type[] { type });
                                var typedList = Activator.CreateInstance(listType);

                                // Using reflection, fetch the add method for the new list
                                // and invoke it to add each value from the original PSobject collection to the new collection
                                // Return the strongly-typed list
                                MethodInfo add = listType.GetMethod("Add");
                                foreach (PSObject pso in results)
                                {
                                    add.Invoke(typedList, new object[] { pso.BaseObject });
                                }

                                return typedList;
                        }

                        StringBuilder message = new StringBuilder();
                        message.AppendFormat(Messages.RunPowerShellScript_ScriptExecutionFailedError, shell.Streams.Error.Count);
                        foreach (ErrorRecord error in shell.Streams.Error)
                        {
                            message.AppendFormat("{0}\n", error);
                        }

                        throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptRunScriptExecutionFailedError, new WorkflowActivityLibraryException(message.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptRunScriptExecutionFailedError, ex);
            }
            finally
            {
                if (context != null)
                {
                    context.Undo();
                }

                if (logOnUser != null)
                {
                    logOnUser.Dispose();
                }

                Logger.Instance.WriteMethodExit(EventIdentifier.RunPowerShellScriptRunScript);
            }
        }

        /// <summary>
        /// Gets the PowerShell credential.
        /// </summary>
        /// <returns>The PowerShell credential object.</returns>
        private PSCredential GetPowerShellCredential()
        {
            if (string.IsNullOrEmpty(this.PowerShellUser) || string.IsNullOrEmpty(this.PowerShellUserPassword))
            {
                return null;
            }

            string[] userParts = this.PowerShellUser.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            if (userParts.Length != 2)
            {
                throw Logger.Instance.ReportError(EventIdentifier.RunPowerShellScriptRunScriptExecutionFailedError, new WorkflowActivityLibraryException(Messages.RunPowerShellActivity_InvalidUserFormat, this.PowerShellUser));
            }

            SecureString password = ProtectedData.DecryptData(this.PowerShellUserPassword);

            return new PSCredential(this.PowerShellUser, password);
        }

        /// <summary>
        /// Logs on the user.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns>The LogOnUser object.</returns>
        private LogOnUser LogOnUser(PSCredential credential)
        {
            LogOnUser logOnUser = null;

            if (this.ImpersonatePowerShellUser)
            {
                LogOnType logonType = this.ImpersonatePowerShellUserLogOnType;
                bool loadUserProfile = this.ImpersonatePowerShellUserLoadUserProfile;

                WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
                if (windowsIdentity != null)
                {
                    string currentUserLogonName = windowsIdentity.Name;
                    if (!this.PowerShellUser.Equals(currentUserLogonName, StringComparison.OrdinalIgnoreCase))
                    {
                        logOnUser = new LogOnUser(
                            credential.GetNetworkCredential().UserName,
                            credential.GetNetworkCredential().Domain,
                            credential.GetNetworkCredential().Password,
                            logonType,
                            LogOnProvider.ProviderWinNT50,
                            loadUserProfile);
                    }
                }
                else
                {
                    logOnUser = new LogOnUser(
                        credential.GetNetworkCredential().Domain,
                        credential.GetNetworkCredential().UserName,
                        credential.GetNetworkCredential().Password,
                        logonType,
                        LogOnProvider.ProviderWinNT50,
                        loadUserProfile);
                }
            }

            return logOnUser;
        }

        #region Conditions

        /// <summary>
        /// Handles the Condition event of the ActivityExecutionConditionSatisfied condition.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ConditionalEventArgs"/> instance containing the event data.</param>
        private void ActivityExecutionConditionSatisfied_Condition(object sender, ConditionalEventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.RunPowerShellScriptActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'.", this.ActivityExecutionCondition);

            try
            {
                // Determine if requests should be submitted based on whether or not a condition was supplied
                // and if that condition resolves to true
                if (string.IsNullOrEmpty(this.ActivityExecutionCondition))
                {
                    e.Result = true;
                }
                else
                {
                    object resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.ActivityExecutionCondition);
                    if (resolved is bool && (bool)resolved)
                    {
                        e.Result = true;
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.RunPowerShellScriptActivityExecutionConditionSatisfiedCondition, "Condition: '{0}'. Condition evaluated '{1}'.", this.ActivityExecutionCondition, e.Result);
            }
        }

        #endregion

        #endregion
    }
}