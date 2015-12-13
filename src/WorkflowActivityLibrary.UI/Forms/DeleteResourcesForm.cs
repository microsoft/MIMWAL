//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteResourcesForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// DeleteResources Activity Form 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Workflow.ComponentModel;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController;

    #endregion

    /// <summary>
    /// DeleteResources Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class DeleteResourcesForm : ActivitySettingsPart
    {
        #region Declarations

        /// <summary>
        /// The activity display name textbox
        /// </summary>
        private readonly ActivityTextBox activityDisplayName;

        /// <summary>
        /// The controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.DeleteResources);

        /// <summary>
        /// The actor string textbox
        /// </summary>
        private readonly ActivityTextBox actorString;

        /// <summary>
        /// The actor type dropdown list
        /// </summary>
        private readonly ActivityDropDownList actorType;

        /// <summary>
        /// The advanced checkbox
        /// </summary>
        private readonly ActivityCheckBox advanced;

        /// <summary>
        /// The apply authorization policy checkbox
        /// </summary>
        private readonly ActivityCheckBox applyAuthorizationPolicy;

        /// <summary>
        /// The activity execution condition textbox
        /// </summary>
        private readonly ActivityTextBox activityExecutionCondition;

        /// <summary>
        /// The iteration textbox
        /// </summary>
        private readonly ActivityTextBox iteration;

        /// <summary>
        /// The target expression
        /// </summary>
        private readonly ActivityTextBox targetExpression;

        /// <summary>
        /// The target type dropdown list
        /// </summary>
        private readonly ActivityDropDownList targetType;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteResourcesForm"/> class.
        /// </summary>
        public DeleteResourcesForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add the standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);
                this.targetType = this.controller.AddDropDownList(ActivitySettings.TargetForDeletion, ActivitySettings.TargetForDeletionHelpText);

                this.advanced = this.controller.AddCheckBox(ActivitySettings.AdvancedFeatures, ActivitySettings.AdvancedFeaturesHelpText);
                this.advanced.CheckBoxControl.CheckedChanged += this.Advanced_CheckedChanged;
                this.advanced.CheckBoxControl.AutoPostBack = true;

                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, false);
                this.iteration = this.controller.AddTextBox(ActivitySettings.Iteration, ActivitySettings.IterationHelpText, false, false);
                this.iteration.TextBoxControl.AutoPostBack = true;

                this.actorType = this.controller.AddDropDownList(ActivitySettings.RequestActor, false, false);
                this.actorType.AddListItem(ActivitySettings.ServiceAccount, ActorType.Service.ToString());
                this.actorType.AddListItem(ActivitySettings.Requestor, ActorType.Requestor.ToString());
                this.actorType.AddListItem(ActivitySettings.ResolveFromExpression, ActorType.Resolve.ToString());
                this.actorType.AddListItem(ActivitySettings.SearchByAccountName, ActorType.Account.ToString());
                this.actorType.DropDownListControl.SelectedIndexChanged += this.ActorType_SelectedIndexChanged;
                this.actorType.DropDownListControl.AutoPostBack = true;

                this.actorString = this.controller.AddTextBox(ActivitySettings.ActorString, false, false);
                this.applyAuthorizationPolicy = this.controller.AddCheckBox(ActivitySettings.ApplyAuthorizationPolicy, ActivitySettings.ApplyAuthorizationPolicyHelpText, false, false);
                this.applyAuthorizationPolicy.CheckBoxControl.AutoPostBack = true;

                this.targetExpression = this.controller.AddTextBox(ActivitySettings.TargetExpression, false, false);
                this.targetExpression.TextBoxControl.Width = 300;

                // Add list items to the target type drop down list and set an event handler
                this.targetType.AddListItem(ActivitySettings.UseWorkflowTarget, DeleteResourcesTargetType.WorkflowTarget.ToString());
                this.targetType.AddListItem(ActivitySettings.ResolveTargets, DeleteResourcesTargetType.ResolveTarget.ToString());
                this.targetType.AddListItem(ActivitySettings.SearchForTargets, DeleteResourcesTargetType.SearchForTarget.ToString());
                this.targetType.DropDownListControl.SelectedIndexChanged += this.TargetType_SelectedIndexChanged;
                this.targetType.DropDownListControl.AutoPostBack = true;
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
                // Create a new instance of the DeleteResources activity and assign
                // dependenty property values based on inputs to standard activity controls
                DeleteResources wfa = new DeleteResources
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    Advanced = this.advanced.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    Iteration = this.iteration.Value,
                    ActorType = GetActorType(this.actorType.Value),
                    ActorString = this.actorString.Value,
                    ApplyAuthorizationPolicy = this.applyAuthorizationPolicy.Value,
                    TargetExpression = this.targetExpression.Value
                };

                if (this.targetType.Value == DeleteResourcesTargetType.WorkflowTarget.ToString())
                {
                    wfa.TargetType = DeleteResourcesTargetType.WorkflowTarget;
                }
                else if (this.targetType.Value == DeleteResourcesTargetType.ResolveTarget.ToString())
                {
                    wfa.TargetType = DeleteResourcesTargetType.ResolveTarget;
                }
                else if (this.targetType.Value == DeleteResourcesTargetType.SearchForTarget.ToString())
                {
                    wfa.TargetType = DeleteResourcesTargetType.SearchForTarget;
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
                // Cast the supplied activity as a DeleteResources activity
                DeleteResources wfa = activity as DeleteResources;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.targetType.Value = wfa.TargetType.ToString();
                this.advanced.Value = wfa.Advanced;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.iteration.Value = wfa.Iteration;
                this.actorType.Value = wfa.ActorType.ToString();
                this.actorString.Value = wfa.ActorString;
                this.applyAuthorizationPolicy.Value = wfa.ApplyAuthorizationPolicy;
                this.targetExpression.Value = wfa.TargetExpression;
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
                return this.controller.PersistSettings();
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
                this.controller.RestoreSettings(data);
                this.ManageAdvancedControls();
                this.ManageTargetControl();
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
                this.controller.SwitchMode(mode);
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

                if (this.advanced.Value)
                {
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

                        if (!string.IsNullOrEmpty(this.iteration.Value))
                        {
                            evaluator.ParseExpression(this.iteration.Value);
                        }

                        if (GetActorType(this.actorType.Value) == ActorType.Resolve)
                        {
                            evaluator.ParseExpression(this.actorString.Value);
                        }
                    }
                    catch (WorkflowActivityLibraryException ex)
                    {
                        // If an exception was thrown while attempting to parse lookups, report the error
                        this.controller.ValidationError = ex.Message;
                        return false;
                    }

                    if (this.applyAuthorizationPolicy.Value && GetActorType(this.actorType.Value) == ActorType.Service)
                    {
                        this.controller.ValidationError = ActivitySettings.RequestActorValidationError;
                        return false;
                    }
                }

                if (this.targetType.Value == DeleteResourcesTargetType.ResolveTarget.ToString())
                {
                    if (!string.IsNullOrEmpty(this.targetExpression.Value))
                    {
                        try
                        {
                            evaluator.ParseExpression(this.targetExpression.Value);
                        }
                        catch (WorkflowActivityLibraryException ex)
                        {
                            // If an exception was thrown while attempting to parse lookups, report the error
                            this.controller.ValidationError = ex.Message;
                            return false;
                        }
                    }
                    else
                    {
                        this.controller.ValidationError = ActivitySettings.TargetLookupExpressionMissingValidationError;
                        return false;
                    }
                }
                else if (this.targetType.Value == DeleteResourcesTargetType.SearchForTarget.ToString())
                {
                    if (string.IsNullOrEmpty(this.targetExpression.Value))
                    {
                        this.controller.ValidationError = ActivitySettings.TargetXPathExpressionMissingValidationError;
                        return false;
                    }
                }

                // Verify that no [//Query/...] or [//Value/...] expressions exist
                // if the query resources or iteration options are not enabled, respectively
                bool containsQueryExpressions = false;
                bool containsValueExpressions = false;

                foreach (LookupEvaluator lookup in evaluator.LookupCache.Keys.Select(key => new LookupEvaluator(key)))
                {
                    if (lookup.Parameter == LookupParameter.Queries)
                    {
                        containsQueryExpressions = true;
                    }

                    if (lookup.Parameter == LookupParameter.Value)
                    {
                        containsValueExpressions = true;
                    }
                }

                if (containsQueryExpressions)
                {
                    this.controller.ValidationError = ActivitySettings.QueryResourcesValidationError;
                    return false;
                }

                if (string.IsNullOrEmpty(this.iteration.Value) && containsValueExpressions)
                {
                    this.controller.ValidationError = ActivitySettings.IterationValidationError;
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
        /// Handles the SelectedIndexChanged event of the ActorType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ActorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageActorControls();
                this.actorString.Value = string.Empty;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the TargetType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void TargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageTargetControl();
                this.targetExpression.Value = string.Empty;
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
        /// Gets the enumeration value that corresponds to the supplied string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The <see cref="ActorType"/> that corresponds to the supplied string</returns>
        private static ActorType GetActorType(string value)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Fetch the enumeration value which corresponds to the supplied string
                foreach (FieldInfo fi in typeof(ActorType).GetFields().Where(fi => fi.Name.Equals(value)))
                {
                    return (ActorType)fi.GetValue(null);
                }

                throw Logger.Instance.ReportError(new InvalidActorTypeException(ActivitySettings.InvalidActorType));
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the target control.
        /// </summary>
        private void ManageTargetControl()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                if (this.targetType.Value == DeleteResourcesTargetType.WorkflowTarget.ToString())
                {
                    this.targetExpression.Visible = false;
                    this.targetExpression.Required = false;
                }
                else if (this.targetType.Value == DeleteResourcesTargetType.ResolveTarget.ToString())
                {
                    this.targetExpression.Visible = true;
                    this.targetExpression.Required = true;
                    this.targetExpression.DisplayName = ActivitySettings.TargetLookup;
                    this.targetExpression.Description = ActivitySettings.TargetLookupHelpText;
                }
                else if (this.targetType.Value == DeleteResourcesTargetType.SearchForTarget.ToString())
                {
                    this.targetExpression.Visible = true;
                    this.targetExpression.Required = true;
                    this.targetExpression.DisplayName = ActivitySettings.TargetSearchFilter;
                    this.targetExpression.Description = ActivitySettings.TargetSearchFilterHelpText;
                }
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
                this.iteration.Visible = this.advanced.Value;
                this.actorType.Visible = this.advanced.Value;
                this.applyAuthorizationPolicy.Visible = this.advanced.Value;

                if (!this.advanced.Value)
                {
                    this.actorString.Visible = false;
                }
                else
                {
                    this.ManageActorControls();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the actor controls.
        /// </summary>
        private void ManageActorControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                switch (GetActorType(this.actorType.Value))
                {
                    case ActorType.Resolve:

                        this.actorString.DisplayName = ActivitySettings.ActorTypeExpression;
                        this.actorString.Description = ActivitySettings.ActorTypeExpressionHelpText;
                        this.actorString.Visible = true;
                        this.actorString.Required = true;
                        break;
                    case ActorType.Account:

                        this.actorString.DisplayName = ActivitySettings.ActorTypeAccountName;
                        this.actorString.Description = ActivitySettings.ActorTypeAccountNameHelpText;
                        this.actorString.Visible = true;
                        this.actorString.Required = true;
                        break;
                    default:

                        this.actorString.Visible = false;
                        this.actorString.Required = false;
                        break;
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