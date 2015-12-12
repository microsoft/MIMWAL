//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateResourcesForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// UpdateResources Activity Form 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms
{
    #region Namespaces Declarations

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
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
    /// UpdateResources Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class UpdateResourcesForm : ActivitySettingsPart
    {
        #region Declarations

        /// <summary>
        /// The regex pattern for FIM resource type and attribute validation
        /// </summary>
        private const string RegexPattern = @"^[(a-z)(A-Z)(_)(:)][(a-z)(A-Z)(0-9)(\-)(.)(_)(:)]*$";

        /// <summary>
        /// The activity display name
        /// </summary>
        private readonly ActivityTextBox activityDisplayName;

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
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.UpdateResources);

        /// <summary>
        /// The iteration textbox
        /// </summary>
        private readonly ActivityTextBox iteration;

        /// <summary>
        /// The queries definitions controller
        /// </summary>
        private readonly DefinitionsController queries;

        /// <summary>
        /// The query resources checkbox
        /// </summary>
        private readonly ActivityCheckBox queryResources;

        /// <summary>
        /// The updates definitions controller
        /// </summary>
        private readonly DefinitionsController updates;
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResourcesForm"/> class.
        /// </summary>
        public UpdateResourcesForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);

                this.advanced = this.controller.AddCheckBox(ActivitySettings.AdvancedFeatures, ActivitySettings.AdvancedFeaturesHelpText);
                this.advanced.CheckBoxControl.CheckedChanged += this.Advanced_CheckedChanged;
                this.advanced.CheckBoxControl.AutoPostBack = true;

                this.queryResources = this.controller.AddCheckBox(ActivitySettings.QueryResources, ActivitySettings.QueryResourcesHelpText, false, false);
                this.queryResources.CheckBoxControl.CheckedChanged += this.QueryResources_CheckedChanged;
                this.queryResources.CheckBoxControl.AutoPostBack = true;

                // Create a new instance of the definitions controller to capture query definitions
                // The visibility of the queries control will be governed by the Query Resources checkbox
                this.queries = new DefinitionsController("Queries", 150, 430, 0)
                {
                    DisplayName = ActivitySettings.Queries,
                    Description = ActivitySettings.QueriesHelpText,
                    LeftHeader = ActivitySettings.QueriesLeftHeader,
                    RightHeader = ActivitySettings.QueriesRightHeader
                };
                this.queries.HeaderRow.Visible = false;
                this.queries.TableRow.Visible = false;
                this.controller.ActivityControlTable.Rows.Add(this.queries.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.queries.TableRow);

                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, false);
                this.iteration = this.controller.AddTextBox(ActivitySettings.Iteration, ActivitySettings.IterationHelpText, false, false);

                this.actorType = this.controller.AddDropDownList(ActivitySettings.RequestActor, false, false);
                this.actorType.AddListItem(ActivitySettings.ServiceAccount, ActorType.Service.ToString());
                this.actorType.AddListItem(ActivitySettings.Requestor, ActorType.Requestor.ToString());
                this.actorType.AddListItem(ActivitySettings.ResolveFromExpression, ActorType.Resolve.ToString());
                this.actorType.AddListItem(ActivitySettings.SearchByAccountName, ActorType.Account.ToString());
                this.actorType.DropDownListControl.SelectedIndexChanged += this.ActorType_SelectedIndexChanged;
                this.actorType.DropDownListControl.AutoPostBack = true;

                this.actorString = this.controller.AddTextBox(ActivitySettings.ActorString, false, false);
                this.applyAuthorizationPolicy = this.controller.AddCheckBox(ActivitySettings.ApplyAuthorizationPolicy, ActivitySettings.ApplyAuthorizationPolicyHelpText, false, false);

                // Create a new definitions controller to capture update definitions
                this.updates = new DefinitionsController("Updates", 330, 250, 70)
                {
                    DisplayName = ActivitySettings.Updates,
                    Description = ActivitySettings.UpdatesHelpText,
                    LeftHeader = ActivitySettings.UpdatesLeftHeader,
                    RightHeader = ActivitySettings.UpdatesRightHeader,
                    CheckHeader = ActivitySettings.UpdatesCheckHeader
                };
                this.controller.ActivityControlTable.Rows.Add(this.updates.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.updates.TableRow);
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
                // Create a new instance of the UpdateResources activity and assign
                // dependenty property values based on inputs to standard activity controls
                UpdateResources wfa = new UpdateResources
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    QueryResources = this.queryResources.Value,
                    Advanced = this.advanced.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    Iteration = this.iteration.Value,
                    ActorType = GetActorType(this.actorType.Value),
                    ActorString = this.actorString.Value,
                    ApplyAuthorizationPolicy = this.applyAuthorizationPolicy.Value
                };

                // Convert the definition listings (web controls) to hash tables which can be serialized to the XOML workflow definition
                // A hash table is used due to issues with deserialization of lists and other structured data
                DefinitionsConverter queriesConverter = new DefinitionsConverter(this.queries.DefinitionListings);
                DefinitionsConverter updatesConverter = new DefinitionsConverter(this.updates.DefinitionListings);
                wfa.QueriesTable = queriesConverter.DefinitionsTable;
                wfa.UpdatesTable = updatesConverter.DefinitionsTable;

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
                // Cast the supplied activity as a UpdateResources activity
                UpdateResources wfa = activity as UpdateResources;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.queryResources.Value = wfa.QueryResources;
                this.advanced.Value = wfa.Advanced;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.iteration.Value = wfa.Iteration;
                this.actorType.Value = wfa.ActorType.ToString();
                this.actorString.Value = wfa.ActorString;
                this.applyAuthorizationPolicy.Value = wfa.ApplyAuthorizationPolicy;
                this.queries.LoadActivitySettings(wfa.QueriesTable);
                this.updates.LoadActivitySettings(wfa.UpdatesTable);
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
                // The definitions controllers will manage persistance of associated values
                ActivitySettingsPartData data = this.controller.PersistSettings();
                data = this.queries.PersistSettings(data);
                data = this.updates.PersistSettings(data);
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
                // Use the controller and definitions controllers to restore settings for activity controls
                // and apply the logic which manages control visibility
                this.controller.RestoreSettings(data);
                this.queries.RestoreSettings(data);
                this.updates.RestoreSettings(data);
                this.ManageQueryControls();
                this.ManageActorControls();
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
                // Use the controller to enable/disable standard activity controls
                // The definitions controllers will manage the mode of associated controls
                this.controller.SwitchMode(mode);
                this.queries.SwitchMode(mode);
                this.updates.SwitchMode(mode);
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

                ExpressionEvaluator evaluator = new ExpressionEvaluator();

                if (this.advanced.Value)
                {
                    if (this.queryResources.Value)
                    {
                        // Loop through all active query listings and make sure they are valid
                        foreach (DefinitionListing query in this.queries.DefinitionListings.Where(query => query.Active))
                        {
                            // If a value is missing for key or query, the definition
                            // will be null and the listing fails validation
                            if (query.Definition == null)
                            {
                                this.controller.ValidationError = ActivitySettings.QueryDefinitionValidationError;
                                return false;
                            }

                            // Make sure that the specified query key is properly formatted
                            if (!Regex.Match(query.Definition.Left, RegexPattern).Success)
                            {
                                this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.QueryDefintionLeftValidationError, query.Definition.Left);
                                return false;
                            }

                            // Make sure that the specified XPath filter is properly formatted
                            if (!ExpressionEvaluator.IsXPath(query.Definition.Right))
                            {
                                this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.QueryDefintionRightValidationError, query.Definition.Left);
                                return false;
                            }
                        }
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(this.activityExecutionCondition.Value))
                        {
                            evaluator.ParseExpression(this.activityExecutionCondition.Value);
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
                        this.controller.ValidationError = ex.Message;
                        return false;
                    }

                    if (this.applyAuthorizationPolicy.Value && GetActorType(this.actorType.Value) == ActorType.Service)
                    {
                        this.controller.ValidationError = ActivitySettings.RequestActorValidationError;
                        return false;
                    }
                }

                // Loop through all active update listings and make sure they are valid
                foreach (DefinitionListing update in this.updates.DefinitionListings.Where(update => update.Active))
                {
                    if (update.Definition == null)
                    {
                        // If a value is missing for source or target, the definition
                        // will be null and the listing fails validation
                        this.controller.ValidationError = ActivitySettings.UpdateDefinitionValidationError;
                        return false;
                    }

                    // Attempt to parse the source expression and target lookup or variable
                    // Fail validation if an exception is thrown for either
                    try
                    {
                        evaluator.ParseExpression(update.Definition.Left);
                        ParameterType targetType = ParameterType.Lookup;
                        try
                        {
                            targetType = ExpressionEvaluator.DetermineParameterType(update.Definition.Right);
                        }
                        catch (WorkflowActivityLibraryException)
                        {
                        }

                        // Target variables are valid
                        // Target lookups require further evaluation to determine if they represent a valid target
                        if (targetType != ParameterType.Variable)
                        {
                            LookupEvaluator lookup = new LookupEvaluator(update.Definition.Right);
                            if (!lookup.IsValidTarget)
                            {
                                this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.TargetLookupValidationError, update.Definition.Right);
                                return false;
                            }
                        }
                    }
                    catch (WorkflowActivityLibraryException ex)
                    {
                        this.controller.ValidationError = ex.Message;
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

                if (!this.queryResources.Value && containsQueryExpressions)
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
        /// Handles the CheckedChanged event of the QueryResources control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void QueryResources_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageQueryControls();
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
        /// Gets the <see cref="ActorType"/> enumeration value that corresponds to the supplied string.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The <see cref="ActorType"/> that corresponds to the supplied string</returns>
        private static ActorType GetActorType(string value)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Fetch the enumeration value which corresponds to the supplied string
                foreach (FieldInfo fi in typeof(ActorType).GetFields())
                {
                    if (fi.Name.Equals(value))
                    {
                        return (ActorType)fi.GetValue(null);
                    }
                }

                throw Logger.Instance.ReportError(new InvalidActorTypeException(ActivitySettings.InvalidActorType));
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
                this.queryResources.Visible = this.advanced.Value;
                this.activityExecutionCondition.Visible = this.advanced.Value;
                this.iteration.Visible = this.advanced.Value;
                this.actorType.Visible = this.advanced.Value;
                this.applyAuthorizationPolicy.Visible = this.advanced.Value;

                if (!this.advanced.Value)
                {
                    this.queries.HeaderRow.Visible = false;
                    this.queries.TableRow.Visible = false;
                    this.actorString.Visible = false;
                }
                else
                {
                    this.ManageQueryControls();
                    this.ManageActorControls();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Manages the query controls.
        /// </summary>
        private void ManageQueryControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.queries.HeaderRow.Visible = this.queryResources.Value;
                this.queries.TableRow.Visible = this.queryResources.Value;
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