//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="RunPowerShellScriptForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// RunPowerShellScript Activity Form 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Web.UI.WebControls;
    using System.Workflow.ComponentModel;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController;

    #endregion

    /// <summary>
    /// RunPowerShellScript Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class RunPowerShellScriptForm : ActivitySettingsPart
    {
        #region Declarations

        /// <summary>
        /// The activity display name textbox
        /// </summary>
        private readonly ActivityTextBox activityDisplayName;

        /// <summary>
        /// The activity execution condition textbox
        /// </summary>
        private readonly ActivityTextBox activityExecutionCondition;

        /// <summary>
        /// The script arguments definitions controller
        /// </summary>
        private readonly DefinitionsController arguments;

        /// <summary>
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.RunPowerShellScript);

        /// <summary>
        /// The fail on missing checkbox
        /// </summary>
        private readonly ActivityCheckBox failOnMissing;

        /// <summary>
        /// The script input type dropdown list
        /// </summary>
        private readonly ActivityDropDownList inputType;

        /// <summary>
        /// The script parameters definitions controller
        /// </summary>
        private readonly DefinitionsController parameters;

        /// <summary>
        /// The script return type dropdown list
        /// </summary>
        private readonly ActivityDropDownList returnType;

        /// <summary>
        /// The script return value lookup textbox
        /// </summary>
        private readonly ActivityTextBox returnValueLookup;

        /// <summary>
        /// The script textbox
        /// </summary>
        private readonly ActivityTextBox script;

        /// <summary>
        /// The script location dropdown list
        /// </summary>
        private readonly ActivityDropDownList scriptLocation;

        /// <summary>
        /// The advanced checkbox
        /// </summary>
        private readonly ActivityCheckBox advanced;

        /// <summary>
        /// The Impersonate PowerShell User checkbox
        /// </summary>
        private readonly ActivityCheckBox impersonatePowerShellUser;

        /// <summary>
        /// The load user profile checkbox
        /// </summary>
        private readonly ActivityCheckBox loadUserProfile;

        /// <summary>
        /// The impersonation logon type dropdown list
        /// </summary>
        private readonly ActivityDropDownList impersonationLogOnType;

        /// <summary>
        /// The PowerShell user textbox
        /// </summary>
        private readonly ActivityTextBox powerShellUser;

        /// <summary>
        /// The PowerShell user password textbox
        /// </summary>
        private readonly ActivityTextBox powerShellUserPassword;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RunPowerShellScriptForm"/> class.
        /// </summary>
        public RunPowerShellScriptForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);

                this.advanced = this.controller.AddCheckBox(ActivitySettings.AdvancedFeatures, ActivitySettings.AdvancedFeaturesHelpText);
                this.advanced.CheckBoxControl.CheckedChanged += this.Advanced_CheckedChanged;
                this.advanced.CheckBoxControl.AutoPostBack = true;

                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, false);

                this.powerShellUser = this.controller.AddTextBox(ActivitySettings.PowerShellUser, ActivitySettings.PowerShellUserHelpText, false, false);
                this.powerShellUserPassword = this.controller.AddTextBox(ActivitySettings.PowerShellUserPassword, ActivitySettings.PowerShellUserPasswordHelpText, false, false);

                this.impersonatePowerShellUser = this.controller.AddCheckBox(ActivitySettings.ImpersonatePowerShellUser, ActivitySettings.ImpersonatePowerShellUserHelpText, false, false);
                this.impersonatePowerShellUser.CheckBoxControl.CheckedChanged += this.ImpersonatePowerShellUser_CheckedChanged;
                this.impersonatePowerShellUser.CheckBoxControl.AutoPostBack = true;

                this.loadUserProfile = this.controller.AddCheckBox(ActivitySettings.LoadUserProfile, ActivitySettings.LoadUserProfileHelpText, false, false);
                this.impersonationLogOnType = this.controller.AddDropDownList(ActivitySettings.ImpersonationLogOnType, ActivitySettings.ImpersonationLogonTypeHelpText, false, false);

                this.scriptLocation = this.controller.AddDropDownList(ActivitySettings.PowerShellScriptLocation, ActivitySettings.PowerShellScriptLocationHelpText);
                this.script = this.controller.AddTextBox(ActivitySettings.PowerShellScript, ActivitySettings.PowerShellScriptHelpText, true);
                this.failOnMissing = this.controller.AddCheckBox(ActivitySettings.FailOnMissingScript, ActivitySettings.FailOnMissingScriptHelpText, false, false);
                this.inputType = this.controller.AddDropDownList(ActivitySettings.PowerShellScriptInputType, ActivitySettings.PowerShellScriptInputTypeHelpText);

                // Create a new instance of the definitions controller to capture parameters
                // The visibility of the parameters control will be based on the input type selection
                this.parameters = new DefinitionsController("Parameters", 150, 430, 0)
                {
                    DisplayName = ActivitySettings.NamedParameters,
                    Description = ActivitySettings.NamedParametersHelpText,
                    LeftHeader = ActivitySettings.NamedParametersLeftHeader,
                    RightHeader = ActivitySettings.NamedParametersRightHeader
                };
                this.parameters.HeaderRow.Visible = false;
                this.parameters.TableRow.Visible = false;
                this.controller.ActivityControlTable.Rows.Add(this.parameters.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.parameters.TableRow);

                // Create a new instance of the definitions controller to capture arguments
                // The visibility of the arguments control will be based on the input type selection
                this.arguments = new DefinitionsController("Arguments", 500, 0, 0)
                {
                    DisplayName = ActivitySettings.ScriptArguments,
                    Description = ActivitySettings.ScriptArgumentsHelpText,
                    LeftHeader = ActivitySettings.ScriptArgumentsLeftHeader
                };
                this.arguments.HeaderRow.Visible = false;
                this.arguments.TableRow.Visible = false;
                this.controller.ActivityControlTable.Rows.Add(this.arguments.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.arguments.TableRow);

                // Now that input controls have been added, add controls for return type
                // The return value lookup control will only be presented when explicit return is selected
                this.returnType = this.controller.AddDropDownList(ActivitySettings.ScriptReturnType, ActivitySettings.ScriptReturnTypeHelpText);
                this.returnValueLookup = this.controller.AddTextBox(ActivitySettings.ScriptReturnValueLookup, ActivitySettings.ScriptReturnValueLookupHelpText, false, false);

                // Configure controls
                this.script.TextBoxControl.TextMode = TextBoxMode.MultiLine;
                this.script.TextBoxControl.Wrap = false;
                this.script.TextBoxControl.Width = 300;
                this.script.TextBoxControl.Rows = 10;

                this.failOnMissing.Value = true;

                this.scriptLocation.AddListItem(ActivitySettings.ScriptLocationWorkflowDefinition, PowerShellScriptLocation.WorkflowDefinition.ToString());
                this.scriptLocation.AddListItem(ActivitySettings.ScriptLocationDisk, PowerShellScriptLocation.Disk.ToString());
                this.scriptLocation.AddListItem(ActivitySettings.ScriptLocationResolveLookup, PowerShellScriptLocation.Resource.ToString());
                this.scriptLocation.DropDownListControl.SelectedIndexChanged += this.ScriptLocation_SelectedIndexChanged;
                this.scriptLocation.DropDownListControl.AutoPostBack = true;

                this.inputType.AddListItem(ActivitySettings.PowerShellInputTypeNone, PowerShellInputType.None.ToString());
                this.inputType.AddListItem(ActivitySettings.PowerShellInputTypeNamedParameters, PowerShellInputType.Parameters.ToString());
                this.inputType.AddListItem(ActivitySettings.PowerShellInputTypeArguments, PowerShellInputType.Arguments.ToString());
                this.inputType.DropDownListControl.SelectedIndexChanged += this.InputType_SelectedIndexChanged;
                this.inputType.DropDownListControl.AutoPostBack = true;

                this.returnType.AddListItem(ActivitySettings.PowerShellReturnTypeNone, PowerShellReturnType.None.ToString());
                this.returnType.AddListItem(ActivitySettings.PowerShellReturnTypeSingleValue, PowerShellReturnType.Explicit.ToString());
                this.returnType.AddListItem(ActivitySettings.PowerShellReturnTypeTable, PowerShellReturnType.Table.ToString());
                this.returnType.DropDownListControl.SelectedIndexChanged += this.ReturnType_SelectedIndexChanged;
                this.returnType.DropDownListControl.AutoPostBack = true;

                this.impersonationLogOnType.AddListItem(ActivitySettings.ImpersonationLogOnTypeLogOnBatch, LogOnType.LogOnBatch.ToString());
                this.impersonationLogOnType.AddListItem(ActivitySettings.ImpersonationLogOnTypeLogOnNetwork, LogOnType.LogOnNetwork.ToString());
                this.impersonationLogOnType.AddListItem(ActivitySettings.ImpersonationLogOnTypeLogOnService, LogOnType.LogOnService.ToString());
                this.impersonationLogOnType.AddListItem(ActivitySettings.ImpersonationLogOnTypeLogOnNetworkClearText, LogOnType.LogOnNetworkClearText.ToString());
                this.impersonationLogOnType.AddListItem(ActivitySettings.ImpersonationLogOnTypeLogOnInteractive, LogOnType.LogOnInteractive.ToString());
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        #region Properties

        /// <summary>
        /// Gets the title of the activity.
        /// </summary>
        public override string Title
        {
            get
            {
                return this.controller.Title;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method is called when a user clicks the Save button in the Workflow Designer.
        /// </summary>
        /// <param name="workflow">Represents the parent workflow of the activity.</param>
        /// <returns>Return an instance of the activity that has its properties set to the values entered into the controls used in the UI of the activity.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Workflow Activities will be disposed when the Workflow is disposed")]
        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Generate a new instance of the Run PowerShell Script activity
                // and add properties based on specified values
                RunPowerShellScript wfa = new RunPowerShellScript
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    Advanced = this.advanced.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    PowerShellUser = this.powerShellUser.Value,
                    PowerShellUserPassword = this.powerShellUserPassword.Value,
                    ImpersonatePowerShellUser = this.impersonatePowerShellUser.Value,
                    ImpersonatePowerShellUserLoadUserProfile = this.loadUserProfile.Value,
                    ImpersonatePowerShellUserLogOnType = this.impersonatePowerShellUser.Value ? GetImpersonationLogOnType(this.impersonationLogOnType.Value) : LogOnType.None,
                    ScriptLocation = GetScriptLocation(this.scriptLocation.Value),
                    Script = this.script.Value,
                    FailOnMissing = this.failOnMissing.Value,
                    InputType = GetInputType(this.inputType.Value),
                    ReturnType = GetReturnType(this.returnType.Value),
                    ReturnValueLookup = this.returnValueLookup.Value
                };

                // Depending on the input type selection,
                // the activity will be configured with parameters or arguments, but not both
                switch (wfa.InputType)
                {
                    case PowerShellInputType.Parameters:
                        {
                            DefinitionsConverter parametersConverter = new DefinitionsConverter(this.parameters.DefinitionListings);
                            wfa.ParametersTable = parametersConverter.DefinitionsTable;
                        }

                        break;
                    case PowerShellInputType.Arguments:
                        {
                            wfa.Arguments = this.FetchArguments();
                        }

                        break;
                }

                return wfa;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// This method initializes activity UI controls to their default values. 
        /// </summary>
        /// <param name="activity">An instance of the current workflow activity. This provides a way to extract the values of the properties to display in the UI.</param>
        public override void LoadActivitySettings(Activity activity)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Cast the supplied activity as a RunPowerShellScript activity
                // and load the activity controls based on dependency property settings
                RunPowerShellScript wfa = activity as RunPowerShellScript;
                if (wfa == null)
                {
                    return;
                }

                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.advanced.Value = wfa.Advanced;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.powerShellUser.Value = wfa.PowerShellUser;
                this.powerShellUserPassword.Value = wfa.PowerShellUserPassword;
                this.impersonatePowerShellUser.Value = wfa.ImpersonatePowerShellUser;
                this.loadUserProfile.Value = wfa.ImpersonatePowerShellUserLoadUserProfile;
                this.impersonationLogOnType.Value = wfa.ImpersonatePowerShellUser ? wfa.ImpersonatePowerShellUserLogOnType.ToString() : null;
                this.scriptLocation.Value = wfa.ScriptLocation.ToString();
                this.script.Value = wfa.Script;
                this.failOnMissing.Value = wfa.FailOnMissing;
                this.inputType.Value = wfa.InputType.ToString();
                if (wfa.ParametersTable != null)
                {
                    this.parameters.LoadActivitySettings(wfa.ParametersTable);
                }

                this.LoadArguments(wfa.Arguments);
                this.returnType.Value = wfa.ReturnType.ToString();
                this.returnValueLookup.Value = wfa.ReturnValueLookup;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Saves the current UI control settings.
        /// </summary>
        /// <returns>Returns <see cref="ActivitySettingsPartData"/>.</returns>
        public override ActivitySettingsPartData PersistSettings()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // The standard form controller will persist most settings
                // Parameters will be persisted using the definitions controller,
                // but arguments implement custom logic because we're only interested in an array list of values
                ActivitySettingsPartData data = this.controller.PersistSettings();
                this.parameters.PersistSettings(data);
                data["Arguments"] = this.FetchArguments();
                return data;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Restores UI control settings stored in the data parameter. 
        /// </summary>
        /// <param name="data">Contains data about the values of UI controls.</param>
        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Restore settings for activity controls and apply the logic which
                // manages the control visibility and configuration
                this.controller.RestoreSettings(data);
                this.parameters.RestoreSettings(data);
                if (data != null && data["Arguments"] != null)
                {
                    this.LoadArguments((ArrayList)data["Arguments"]);
                }

                this.ManageScriptControl();
                this.ManageInputControls();
                this.ManageReturnValueLookupControl();
                this.ManageImpersonationControls();
                this.ManageAdvancedControls();
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Switches the activity UI between read only mode and edit mode.
        /// </summary>
        /// <param name="mode">Represents read only mode or edit mode.</param>
        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            Logger.Instance.WriteMethodEntry("Switch Mode: '{0}'.", mode);

            try
            {
                // Switch the mode on all controls
                this.controller.SwitchMode(mode);
                this.parameters.SwitchMode(mode);
                this.arguments.SwitchMode(mode);
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit("Switch Mode: '{0}'.", mode);
            }
        }

        /// <summary>
        /// Validates the inputs. Returns true if all of the UI controls contain valid values. Otherwise, returns false.
        /// </summary>
        /// <returns>true if all of the UI controls contain valid values. Otherwise, false.</returns>
        public override bool ValidateInputs()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                if (!this.controller.ValidateInputs())
                {
                    return false;
                }

                ExpressionEvaluator evaluator = new ExpressionEvaluator();
                switch (GetInputType(this.inputType.Value))
                {
                    case PowerShellInputType.Parameters:
                        foreach (DefinitionListing parameter in this.parameters.DefinitionListings.Where(parameter => parameter.Active))
                        {
                            if (parameter.Definition == null)
                            {
                                // If a value is missing for parameter name or value expression, the definition
                                // will be null and the listing fails validation
                                this.controller.ValidationError = ActivitySettings.ScriptParameterDefintionValidationError;
                                return false;
                            }

                            // Attempt to parse the value expression
                            try
                            {
                                evaluator.ParseExpression(parameter.Definition.Right);
                            }
                            catch (WorkflowActivityLibraryException ex)
                            {
                                this.controller.ValidationError = ex.Message;
                                return false;
                            }
                        }

                        break;
                    case PowerShellInputType.Arguments:
                        foreach (DefinitionListing argument in this.arguments.DefinitionListings.Where(argument => argument.Active))
                        {
                            if (string.IsNullOrEmpty(argument.State.Left))
                            {
                                // If a value is missing for the expression, fail validation
                                this.controller.ValidationError = ActivitySettings.ScriptArgumentValidationError;
                                return false;
                            }

                            // Attempt to parse the value expression
                            try
                            {
                                evaluator.ParseExpression(argument.State.Left);
                            }
                            catch (WorkflowActivityLibraryException ex)
                            {
                                this.controller.ValidationError = ex.Message;
                                return false;
                            }
                        }

                        break;
                }

                try
                {
                    if (!string.IsNullOrEmpty(this.activityExecutionCondition.Value))
                    {
                        evaluator.ParseExpression(this.activityExecutionCondition.Value);

                        // Verify that the activity execution condition resolves to a Boolean value
                        if (!evaluator.IsBooleanExpression(this.activityExecutionCondition.Value))
                        {
                            this.controller.ValidationError = ActivitySettings.ActivityExecutionConditionValidationError;
                            return false;
                        }
                    }
                }
                catch (WorkflowActivityLibraryException ex)
                {
                    this.controller.ValidationError = ex.Message;
                    return false;
                }

                if (this.impersonatePowerShellUser.Value)
                {
                    if (string.IsNullOrEmpty(this.powerShellUser.Value) ||
                        string.IsNullOrEmpty(this.powerShellUserPassword.Value))
                    {
                        this.controller.ValidationError = ActivitySettings.PowerShellImpersonationSettingsValidationError;
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(this.powerShellUser.Value))
                {
                    if (this.impersonatePowerShellUser.Value)
                    {
                        if (!this.powerShellUser.Value.Contains(@"\") && !this.powerShellUser.Value.Contains("@"))
                        {
                            this.controller.ValidationError = ActivitySettings.PowerShellUserFormatValidationError;
                            return false;
                        }
                    }

                    if (string.IsNullOrEmpty(this.powerShellUserPassword.Value))
                    {
                        this.controller.ValidationError = ActivitySettings.PowerShellUserPasswordValidationError;
                        return false;
                    }

                    // Limited value in this check as the code runs under the context of submitter instead of FIMService plus
                    // code run on the Portal Server which may not be co-located with FIMService server.
                    ////try
                    ////{
                    ////    ProtectedData.DecryptData(this.powerShellUserPassword.Value);
                    ////}
                    ////catch (WorkflowActivityLibraryException ex)
                    ////{
                    ////    this.controller.ValidationError = ex.Message;
                    ////    return false;
                    ////}
                }

                // If no errors were found, clear any validation error and return true
                this.controller.ValidationError = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Advanced control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void Advanced_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageAdvancedControls();
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Advanced control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ImpersonatePowerShellUser_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageImpersonationControls();
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ScriptLocation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ScriptLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageScriptControl();
                this.script.Value = string.Empty;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the InputType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void InputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageInputControls();
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ReturnType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageReturnValueLookupControl();
                this.returnValueLookup.Value = string.Empty;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Creates the child controls.
        /// </summary>
        protected override void CreateChildControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.Controls.Add(this.controller.ControlTable);
                base.CreateChildControls();
            }
            catch (Exception e)
            {
                Logger.Instance.ReportError(e);
                throw;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Gets the <see cref="PowerShellScriptLocation"/> enumeration value that corresponds to the supplied string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The <see cref="PowerShellScriptLocation"/> that corresponds to the supplied string</returns>
        private static PowerShellScriptLocation GetScriptLocation(string value)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Fetch the enumeration value which corresponds to the supplied string
                foreach (FieldInfo fi in typeof(PowerShellScriptLocation).GetFields().Where(fi => fi.Name.Equals(value)))
                {
                    return (PowerShellScriptLocation)fi.GetValue(null);
                }

                throw Logger.Instance.ReportError(new InvalidPowerShellScriptLocationException(ActivitySettings.InvalidPowerShellScriptLocation));
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Gets the <see cref="PowerShellInputType"/> enumeration value that corresponds to the supplied string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The <see cref="PowerShellInputType"/> that corresponds to the supplied string</returns>
        private static PowerShellInputType GetInputType(string value)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Fetch the enumeration value which corresponds to the supplied string
                foreach (FieldInfo fi in typeof(PowerShellInputType).GetFields().Where(fi => fi.Name.Equals(value)))
                {
                    return (PowerShellInputType)fi.GetValue(null);
                }

                throw Logger.Instance.ReportError(new InvalidPowerShellInputTypeException(ActivitySettings.InvalidPowerShellInputType));
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Gets the <see cref="PowerShellReturnType"/> enumeration value that corresponds to the supplied string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The <see cref="PowerShellReturnType"/> that corresponds to the supplied string</returns>
        private static PowerShellReturnType GetReturnType(string value)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Fetch the enumeration value which corresponds to the supplied string
                foreach (FieldInfo fi in typeof(PowerShellReturnType).GetFields().Where(fi => fi.Name.Equals(value)))
                {
                    return (PowerShellReturnType)fi.GetValue(null);
                }

                throw Logger.Instance.ReportError(new InvalidPowerShellReturnTypeException(ActivitySettings.InvalidPowerShellReturnType));
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Gets the <see cref="LogOnType"/> enumeration value that corresponds to the supplied string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The <see cref="LogOnType"/> that corresponds to the supplied string</returns>
        private static LogOnType GetImpersonationLogOnType(string value)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Fetch the enumeration value which corresponds to the supplied string
                foreach (FieldInfo fi in typeof(LogOnType).GetFields().Where(fi => fi.Name.Equals(value)))
                {
                    return (LogOnType)fi.GetValue(null);
                }

                throw Logger.Instance.ReportError(new InvalidPowerShellReturnTypeException(ActivitySettings.InvalidLogOnType));
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the advanced controls.
        /// </summary>
        private void ManageAdvancedControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.impersonatePowerShellUser.Visible = this.advanced.Value;
                this.activityExecutionCondition.Visible = this.advanced.Value;
                this.powerShellUser.Visible = this.advanced.Value;
                this.powerShellUserPassword.Visible = this.advanced.Value;

                if (!this.advanced.Value)
                {
                    this.impersonatePowerShellUser.Value = false;
                }

                this.ManageImpersonationControls();
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the input controls.
        /// </summary>
        private void ManageImpersonationControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.loadUserProfile.Visible = this.impersonatePowerShellUser.Value;
                this.impersonationLogOnType.Visible = this.impersonatePowerShellUser.Value;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the input controls.
        /// </summary>
        private void ManageInputControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Based on the value selected for input type,
                // configure the parameters and arguments controls
                switch (GetInputType(this.inputType.Value))
                {
                    case PowerShellInputType.None:
                        this.parameters.HeaderRow.Visible = false;
                        this.parameters.TableRow.Visible = false;
                        this.arguments.HeaderRow.Visible = false;
                        this.arguments.TableRow.Visible = false;
                        break;
                    case PowerShellInputType.Parameters:
                        this.parameters.HeaderRow.Visible = true;
                        this.parameters.TableRow.Visible = true;
                        this.arguments.HeaderRow.Visible = false;
                        this.arguments.TableRow.Visible = false;
                        break;
                    case PowerShellInputType.Arguments:
                        this.parameters.HeaderRow.Visible = false;
                        this.parameters.TableRow.Visible = false;
                        this.arguments.HeaderRow.Visible = true;
                        this.arguments.TableRow.Visible = true;
                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the script control.
        /// </summary>
        private void ManageScriptControl()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Based on the value selected for script location,
                // configure the script and fail on missing controls
                switch (GetScriptLocation(this.scriptLocation.Value))
                {
                    case PowerShellScriptLocation.WorkflowDefinition:
                        this.script.DisplayName = ActivitySettings.PowerShellScript;
                        this.script.Description = ActivitySettings.PowerShellScriptHelpText;
                        this.script.TextBoxControl.TextMode = TextBoxMode.MultiLine;
                        this.script.TextBoxControl.Rows = 10;
                        this.failOnMissing.Visible = false;
                        break;
                    case PowerShellScriptLocation.Disk:
                        this.script.DisplayName = ActivitySettings.PowerShellScriptPath;
                        this.script.Description = ActivitySettings.PowerShellScriptPathHelpText;
                        this.script.TextBoxControl.TextMode = TextBoxMode.SingleLine;
                        this.script.TextBoxControl.Rows = 1;
                        this.failOnMissing.Visible = false;
                        break;
                    case PowerShellScriptLocation.Resource:
                        this.script.DisplayName = ActivitySettings.PowerShellScriptLookup;
                        this.script.Description = ActivitySettings.PowerShellScriptLookupHelpText;
                        this.script.TextBoxControl.TextMode = TextBoxMode.SingleLine;
                        this.script.TextBoxControl.Rows = 1;
                        this.failOnMissing.Visible = true;
                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the return value lookup control.
        /// </summary>
        private void ManageReturnValueLookupControl()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Based on the value selected for return type,
                // manage visibility of the return value lookup control
                switch (GetReturnType(this.returnType.Value))
                {
                    case PowerShellReturnType.None:
                        this.returnValueLookup.Visible = false;
                        this.returnValueLookup.Required = false;
                        break;
                    case PowerShellReturnType.Explicit:
                        this.returnValueLookup.Visible = true;
                        this.returnValueLookup.Required = true;
                        break;
                    case PowerShellReturnType.Table:
                        this.returnValueLookup.Visible = false;
                        this.returnValueLookup.Required = false;
                        break;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Fetches the script arguments from the arguments definition listings.
        /// </summary>
        /// <returns>The ArrayList of the script arguments from the definition listings</returns>
        private ArrayList FetchArguments()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Build an array list of arguments from the definition listings
                ArrayList argumentList = new ArrayList();
                foreach (DefinitionListing listing in this.arguments.DefinitionListings.Where(listing => listing.State != null && !string.IsNullOrEmpty(listing.State.Left)))
                {
                    argumentList.Add(listing.State.Left);
                }

                return argumentList;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Loads the script arguments into arguments definition listings.
        /// </summary>
        /// <param name="argumentList">The script arguments list.</param>
        private void LoadArguments(IList argumentList)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                if (argumentList == null)
                {
                    return;
                }

                // Populate the definition listings control with the values of the array list
                for (int i = 0; i < argumentList.Count; i++)
                {
                    this.arguments.DefinitionListings[i].State = new DefinitionListingState(false, argumentList[i].ToString(), string.Empty, false);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        #endregion
    }
}