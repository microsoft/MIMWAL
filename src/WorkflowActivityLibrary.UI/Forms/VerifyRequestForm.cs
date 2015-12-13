//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="VerifyRequestForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// VerifyRequest Activity Form 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Workflow.ComponentModel;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController;

    #endregion

    /// <summary>
    /// VerifyRequest Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class VerifyRequestForm : ActivitySettingsPart
    {
        #region Declarations

        /// <summary>
        /// The activity display name textbox
        /// </summary>
        private readonly ActivityTextBox activityDisplayName;

        /// <summary>
        /// The advanced checkbox
        /// </summary>
        private readonly ActivityCheckBox advanced;

        /// <summary>
        /// The activity execution condition textbox
        /// </summary>
        private readonly ActivityTextBox activityExecutionCondition;

        /// <summary>
        /// The check for conflict checkbox
        /// </summary>
        private readonly ActivityCheckBox checkForConflict;

        /// <summary>
        /// The check for request conflict checkbox
        /// </summary>
        private readonly ActivityCheckBox checkForRequestConflict;

        /// <summary>
        /// The condition textbox
        /// </summary>
        private readonly DefinitionsController conditions;

        /// <summary>
        /// The conflict denial message
        /// </summary>
        private readonly ActivityTextBox conflictDenialMessage;

        /// <summary>
        /// The conflict filter textbox
        /// </summary>
        private readonly ActivityTextBox conflictFilter;

        /// <summary>
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.VerifyRequest);

        /// <summary>
        /// The request conflict advanced filter  textbox
        /// </summary>
        private readonly ActivityTextBox requestConflictAdvancedFilter;

        /// <summary>
        /// The request conflict denial message textbox
        /// </summary>
        private readonly ActivityTextBox requestConflictDenialMessage;

        /// <summary>
        /// The request conflict match condition textbox
        /// </summary>
        private readonly ActivityTextBox requestConflictMatchCondition;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyRequestForm"/> class.
        /// </summary>
        public VerifyRequestForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add the standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);
                
                this.advanced = this.controller.AddCheckBox(ActivitySettings.AdvancedFeatures, ActivitySettings.AdvancedFeaturesHelpText);
                this.advanced.CheckBoxControl.CheckedChanged += this.Advanced_CheckedChanged;
                this.advanced.CheckBoxControl.AutoPostBack = true;

                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, false);
                this.checkForConflict = this.controller.AddCheckBox(ActivitySettings.VerifyResourceUniqueness, ActivitySettings.VerifyResourceUniquenessHelpText);
                this.conflictFilter = this.controller.AddTextBox(ActivitySettings.ConflictingResourceSearchFilter, ActivitySettings.ConflictingResourceSearchFilterHelpText2, false, false);
                this.conflictDenialMessage = this.controller.AddTextBox(ActivitySettings.ConflictDenialMessage, ActivitySettings.ConflictDenialMessageHelpText, false, false);
                this.checkForRequestConflict = this.controller.AddCheckBox(ActivitySettings.VerifyRequestUniqueness, ActivitySettings.VerifyRequestUniquenessHelpText);
                this.requestConflictAdvancedFilter = this.controller.AddTextBox(ActivitySettings.AdvancedRequestSearchFilter, ActivitySettings.AdvancedRequestSearchFilterHelpText, false, false);
                this.requestConflictMatchCondition = this.controller.AddTextBox(ActivitySettings.RequestConflictMatchCondition, ActivitySettings.RequestConflictMatchConditionHelpText, false, false);
                this.requestConflictDenialMessage = this.controller.AddTextBox(ActivitySettings.RequestConflictDenialMessage, ActivitySettings.RequestConflictDenialMessageHelpText, false, false);

                // Create a new definitions controller to allow for the specification
                // of attribute update definitions
                this.conditions = new DefinitionsController("Conditions", 250, 350, 0)
                {
                    DisplayName = ActivitySettings.RequiredRequestConditions,
                    Description = ActivitySettings.RequiredRequestConditionsHelpText,
                    LeftHeader = ActivitySettings.RequiredRequestConditionsLeftHeader,
                    RightHeader = ActivitySettings.RequiredRequestConditionsRightHeader
                };
                this.controller.ActivityControlTable.Rows.Add(this.conditions.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.conditions.TableRow);

                // Modify the width of some of the controls
                this.conflictFilter.TextBoxControl.Width = 300;
                this.conflictDenialMessage.TextBoxControl.Width = 300;
                this.requestConflictAdvancedFilter.TextBoxControl.Width = 300;
                this.requestConflictMatchCondition.TextBoxControl.Width = 300;
                this.requestConflictDenialMessage.TextBoxControl.Width = 300;

                // Add event handlers to check boxes which manage the visibility of other controls
                this.checkForConflict.CheckBoxControl.CheckedChanged += this.CheckForConflict_CheckedChanged;
                this.checkForConflict.CheckBoxControl.AutoPostBack = true;
                this.checkForRequestConflict.CheckBoxControl.CheckedChanged += this.CheckForRequestConflict_CheckedChanged;
                this.checkForRequestConflict.CheckBoxControl.AutoPostBack = true;
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
                // Create a new instance of the VerifyRequest activity and assign
                // dependenty property values based on inputs to standard activity controls
                VerifyRequest wfa = new VerifyRequest
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    Advanced = this.advanced.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    CheckForConflict = this.checkForConflict.Value,
                    ConflictFilter = this.conflictFilter.Value,
                    ConflictDenialMessage = this.conflictDenialMessage.Value,
                    CheckForRequestConflict = this.checkForRequestConflict.Value,
                    RequestConflictAdvancedFilter = this.requestConflictAdvancedFilter.Value,
                    RequestConflictMatchCondition = this.requestConflictMatchCondition.Value,
                    RequestConflictDenialMessage = this.requestConflictDenialMessage.Value
                };

                // Convert the definition listings (web controls) to a hash table which can be serialized to the XOML workflow definition
                // A hash table is used due to issues with deserialization of lists and other structured data
                DefinitionsConverter converter = new DefinitionsConverter(this.conditions.DefinitionListings);
                wfa.ConditionsTable = converter.DefinitionsTable;

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
                // Cast the supplied activity as a VerifyRequest activity
                VerifyRequest wfa = activity as VerifyRequest;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.advanced.Value = wfa.Advanced;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.checkForConflict.Value = wfa.CheckForConflict;
                this.conflictFilter.Value = wfa.ConflictFilter;
                this.conflictDenialMessage.Value = wfa.ConflictDenialMessage;
                this.checkForRequestConflict.Value = wfa.CheckForRequestConflict;
                this.requestConflictAdvancedFilter.Value = wfa.RequestConflictAdvancedFilter;
                this.requestConflictMatchCondition.Value = wfa.RequestConflictMatchCondition;
                this.requestConflictDenialMessage.Value = wfa.RequestConflictDenialMessage;
                this.conditions.LoadActivitySettings(wfa.ConditionsTable);
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
                // Use the controller to persist the settings for standard activity controls
                // The definitions controller will manage persistance of associated values
                ActivitySettingsPartData data = this.controller.PersistSettings();
                return this.conditions.PersistSettings(data);
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
                // Use the controller and definitions controller to restore settings for activity controls
                this.controller.RestoreSettings(data);
                this.conditions.RestoreSettings(data);
                this.ManageConflictControls();
                this.ManageAdvancedControls();
                this.ManageRequestConflictControls();
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
                // Use the controller to enable/disable standard activity controls
                // the definitions controller will manage the mode of associated controls
                this.controller.SwitchMode(mode);
                this.conditions.SwitchMode(mode);
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
                // Use the controller to validate standard activity controls
                // and return false if a problem was identified
                if (!this.controller.ValidateInputs())
                {
                    return false;
                }

                // If standard activity controls are valid,
                // loop through all active definition listings and make sure they are valid
                ExpressionEvaluator evaluator = new ExpressionEvaluator();
                foreach (DefinitionListing condition in this.conditions.DefinitionListings.Where(condition => condition.Active))
                {
                    if (condition.Definition == null)
                    {
                        // If a value is missing for the left or right listing fields, the definition
                        // will be null and the listing fails validation
                        // Because the activity allows for verification of uniqueness only without additional conditions,
                        // we need to check if this is the only active listing for the form and, if so, 
                        // verify that all fields have been left blank before failing validation
                        int countActive = this.conditions.DefinitionListings.Count(l => l.Active);
                        if (countActive != 1 ||
                            !string.IsNullOrEmpty(condition.State.Left) ||
                            !string.IsNullOrEmpty(condition.State.Right))
                        {
                            this.controller.ValidationError = ActivitySettings.ConditionDefinitionValidationError;
                            return false;
                        }
                    }
                    else
                    {
                        // Attempt to parse the condition and fail validation
                        // if an exception is thrown by the expression evaluator
                        try
                        {
                            evaluator.ParseExpression(condition.Definition.Left);
                        }
                        catch (WorkflowActivityLibraryException ex)
                        {
                            this.controller.ValidationError = ex.Message;
                            return false;
                        }

                        // Verify that the condition resolves to a Boolean value
                        bool parseBoolean;
                        if (!evaluator.IsBooleanExpression(condition.Definition.Left) &&
                            !bool.TryParse(condition.Definition.Left, out parseBoolean))
                        {
                            this.controller.ValidationError = ActivitySettings.ConditionEvaluationValidationError;
                            return false;
                        }
                    }
                }

                try
                {
                    if (this.advanced.Value)
                    {
                        if (!string.IsNullOrEmpty(this.activityExecutionCondition.Value))
                        {
                            // Verify that the activity execution condition resolves to a Boolean value
                            if (!evaluator.IsBooleanExpression(this.activityExecutionCondition.Value))
                            {
                                this.controller.ValidationError = ActivitySettings.ActivityExecutionConditionValidationError;
                                return false;
                            }
                        }
                    }
                }
                catch (WorkflowActivityLibraryException ex)
                {
                    this.controller.ValidationError = ex.Message;
                    return false;
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
        /// Handles the CheckedChanged event of the CheckForConflict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void CheckForConflict_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageConflictControls();
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CheckForRequestConflict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void CheckForRequestConflict_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageRequestConflictControls();
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
        /// Manages the advanced controls.
        /// </summary>
        private void ManageAdvancedControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.activityExecutionCondition.Visible = this.advanced.Value;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the conflict controls.
        /// </summary>
        private void ManageConflictControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                if (this.checkForConflict.Value)
                {
                    this.conflictFilter.Visible = true;
                    this.conflictFilter.Required = true;
                    this.conflictDenialMessage.Visible = true;
                    this.conflictDenialMessage.Required = true;
                }
                else
                {
                    this.conflictFilter.Visible = false;
                    this.conflictFilter.Required = false;
                    this.conflictDenialMessage.Visible = false;
                    this.conflictDenialMessage.Required = false;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the request conflict controls.
        /// </summary>
        private void ManageRequestConflictControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                if (this.checkForRequestConflict.Value)
                {
                    this.requestConflictAdvancedFilter.Visible = true;
                    this.requestConflictMatchCondition.Visible = true;
                    this.requestConflictMatchCondition.Required = true;
                    this.requestConflictDenialMessage.Visible = true;
                    this.requestConflictDenialMessage.Required = true;
                }
                else
                {
                    this.requestConflictAdvancedFilter.Visible = false;
                    this.requestConflictMatchCondition.Visible = false;
                    this.requestConflictMatchCondition.Required = false;
                    this.requestConflictDenialMessage.Visible = false;
                    this.requestConflictDenialMessage.Required = false;
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