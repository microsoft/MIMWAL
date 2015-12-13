//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateResourceForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// CreateResource Activity Form 
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
    /// CreateResource Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class CreateResourceForm : ActivitySettingsPart
    {
        #region Declarations

        /// <summary>
        /// The regex pattern for FIM resource type and attribute validation
        /// </summary>
        private const string RegexPattern = @"^[(a-z)(A-Z)(_)(:)][(a-z)(A-Z)(0-9)(\-)(.)(_)(:)]*$";

        /// <summary>
        /// The activity display name textbox
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
        /// The attributes definitions controller
        /// </summary>
        private readonly DefinitionsController attributes;

        /// <summary>
        /// The check for conflict checkbox
        /// </summary>
        private readonly ActivityCheckBox checkForConflict;

        /// <summary>
        /// The activity execution condition textbox
        /// </summary>
        private readonly ActivityTextBox activityExecutionCondition;

        /// <summary>
        /// The conflict filter textbox
        /// </summary>
        private readonly ActivityTextBox conflictFilter;
        
        /// <summary>
        /// The conflicting resource identifier target textbox
        /// </summary>
        private readonly ActivityTextBox conflictingResourceIdTarget;

        /// <summary>
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.CreateResource);

        /// <summary>
        /// The created resource identifier target textbox
        /// </summary>
        private readonly ActivityTextBox createdResourceIdTarget;

        /// <summary>
        /// The fail on conflict textbox
        /// </summary>
        private readonly ActivityCheckBox failOnConflict;

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
        /// The resource type textbox
        /// </summary>
        private readonly ActivityTextBox resourceType;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateResourceForm"/> class.
        /// </summary>
        public CreateResourceForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);
                this.resourceType = this.controller.AddTextBox(ActivitySettings.ResourceType, ActivitySettings.ResourceTypeHelpText, true);

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
                this.iteration.TextBoxControl.TextChanged += this.Iteration_TextChanged;
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
                this.applyAuthorizationPolicy.CheckBoxControl.CheckedChanged += this.ApplyAuthorizationPolicy_CheckedChanged;
                this.applyAuthorizationPolicy.CheckBoxControl.AutoPostBack = true;

                // Add additional controls
                // The visibility of conflict controls will be governed by the Check for Conflict checkbox
                this.createdResourceIdTarget = this.controller.AddTextBox(ActivitySettings.TargetForCreatedResourceID, ActivitySettings.TargetForCreatedResourceIDHelpText, false, false);
                this.checkForConflict = this.controller.AddCheckBox(ActivitySettings.CheckForConflict, ActivitySettings.CheckForConflictHelpText, false, false);
                this.checkForConflict.CheckBoxControl.CheckedChanged += this.CheckForConflict_CheckedChanged;
                this.checkForConflict.CheckBoxControl.AutoPostBack = true;

                this.conflictFilter = this.controller.AddTextBox(ActivitySettings.ConflictingResourceSearchFilter, ActivitySettings.ConflictingResourceSearchFilterHelpText, false, false);
                this.conflictFilter.TextBoxControl.Width = 300;

                this.conflictingResourceIdTarget = this.controller.AddTextBox(ActivitySettings.TargetForConflictingResourceID, ActivitySettings.TargetForConflictingResourceIDHelpText, false, false);
                this.failOnConflict = this.controller.AddCheckBox(ActivitySettings.FailOnConflict, ActivitySettings.FailOnConflictHelpText, false, false);

                // Create a new instance of hte definitions controller to capture attribute definitions
                this.attributes = new DefinitionsController("Attributes", 380, 200, 0)
                {
                    DisplayName = ActivitySettings.AttributesPopulation,
                    Description = ActivitySettings.AttributesPopulationHelpText,
                    LeftHeader = ActivitySettings.AttributesPopulationLeftHeader,
                    RightHeader = ActivitySettings.AttributesPopulationRightHeader
                };
                this.controller.ActivityControlTable.Rows.Add(this.attributes.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.attributes.TableRow);
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
                // Create a new instance of the CreateResource activity and assign
                // dependency property values based on inputs to standard activity controls
                CreateResource wfa = new CreateResource
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    ResourceType = this.resourceType.Value,
                    Advanced = this.advanced.Value,
                    QueryResources = this.queryResources.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    Iteration = this.iteration.Value,
                    ActorType = GetActorType(this.actorType.Value),
                    ActorString = this.actorString.Value,
                    ApplyAuthorizationPolicy = this.applyAuthorizationPolicy.Value,
                    CreatedResourceIdTarget = this.createdResourceIdTarget.Value,
                    CheckForConflict = this.checkForConflict.Value,
                    ConflictFilter = this.conflictFilter.Value,
                    ConflictingResourceIdTarget = this.conflictingResourceIdTarget.Value,
                    FailOnConflict = this.failOnConflict.Value
                };

                // Convert the definition listings (web controls) to hash tables which can be serialized to the XOML workflow definition
                // A hash table is used due to issues with deserialization of lists and other structured data
                DefinitionsConverter queriesConverter = new DefinitionsConverter(this.queries.DefinitionListings);
                DefinitionsConverter attributesConverter = new DefinitionsConverter(this.attributes.DefinitionListings);
                wfa.QueriesTable = queriesConverter.DefinitionsTable;
                wfa.AttributesTable = attributesConverter.DefinitionsTable;

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
                // Cast the supplied activity as a CreateResource activity
                CreateResource wfa = activity as CreateResource;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.resourceType.Value = wfa.ResourceType;
                this.advanced.Value = wfa.Advanced;
                this.queryResources.Value = wfa.QueryResources;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.iteration.Value = wfa.Iteration;
                this.actorType.Value = wfa.ActorType.ToString();
                this.actorString.Value = wfa.ActorString;
                this.applyAuthorizationPolicy.Value = wfa.ApplyAuthorizationPolicy;
                this.createdResourceIdTarget.Value = wfa.CreatedResourceIdTarget;
                this.checkForConflict.Value = wfa.CheckForConflict;
                this.conflictFilter.Value = wfa.ConflictFilter;
                this.conflictingResourceIdTarget.Value = wfa.ConflictingResourceIdTarget;
                this.failOnConflict.Value = wfa.FailOnConflict;
                this.queries.LoadActivitySettings(wfa.QueriesTable);
                this.attributes.LoadActivitySettings(wfa.AttributesTable);
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
                data = this.attributes.PersistSettings(data);
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
                // Run the methods which manage the visibility of the query and conflict controls
                this.controller.RestoreSettings(data);
                this.queries.RestoreSettings(data);
                this.attributes.RestoreSettings(data);
                this.ManageQueryControls();
                this.ManageActorControls();
                this.ManageConflictControls();
                this.ManageAdvancedControls();
                this.ManageFeatureDisablement();
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
                this.attributes.SwitchMode(mode);
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

                // Verify that the resource type is valid
                if (!Regex.Match(this.resourceType.Value, RegexPattern).Success)
                {
                    this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.ResourceTypeValidationError, this.resourceType.Value);
                    return false;
                }

                if (this.advanced.Value)
                {
                    if (this.queryResources.Value)
                    {
                        // Loop through all active query listings and make sure they are valid
                        foreach (DefinitionListing query in this.queries.DefinitionListings)
                        {
                            if (query.Active)
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
                    }

                    try
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

                        if (!string.IsNullOrEmpty(this.iteration.Value))
                        {
                            evaluator.ParseExpression(this.iteration.Value);
                        }

                        if (GetActorType(this.actorType.Value) == ActorType.Resolve)
                        {
                            evaluator.ParseExpression(this.actorString.Value);
                        }

                        // Verify that the supplied conflict filter is a valid XPath query, if necessary
                        if (this.checkForConflict.Value && !ExpressionEvaluator.IsXPath(this.conflictFilter.Value))
                        {
                            this.controller.ValidationError = ActivitySettings.ConflictResourceSearchFilterValidationError;
                            return false;
                        }

                        // If necessary, parse the created resource ID target lookup to ensure its validity
                        if (!string.IsNullOrEmpty(this.createdResourceIdTarget.Value))
                        {
                            LookupEvaluator createdTargetLookup = new LookupEvaluator(this.createdResourceIdTarget.Value);
                            if (!createdTargetLookup.IsValidTarget)
                            {
                                this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.TargetLookupValidationError, this.createdResourceIdTarget.Value);
                                return false;
                            }
                        }

                        // If necessary, parse the conflicting resource ID target lookup to ensure its validity
                        if (this.checkForConflict.Value && !string.IsNullOrEmpty(this.conflictingResourceIdTarget.Value))
                        {
                            LookupEvaluator conflictingTargetLookup = new LookupEvaluator(this.conflictingResourceIdTarget.Value);
                            if (!conflictingTargetLookup.IsValidTarget)
                            {
                                this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.TargetLookupValidationError, this.conflictingResourceIdTarget.Value);
                                return false;
                            }
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

                // Loop through all active definition listings and make sure they are valid
                foreach (DefinitionListing listing in this.attributes.DefinitionListings)
                {
                    if (!listing.Active)
                    {
                        continue;
                    }

                    if (listing.Definition == null)
                    {
                        // If a value is missing for source or target, the definition
                        // will be null and the listing fails validation
                        // Because the activity allows for the creation of a resource without any attributes,
                        // we need to check if this is the only active listing for the form and, if so, 
                        // verify that all fields have been left blank before failing validation
                        int countActive = this.attributes.DefinitionListings.Count(l => l.Active);
                        if (countActive != 1 ||
                            !string.IsNullOrEmpty(listing.State.Left) ||
                            !string.IsNullOrEmpty(listing.State.Right))
                        {
                            this.controller.ValidationError = ActivitySettings.AttributeDefinitionValidationError;
                            return false;
                        }
                    }
                    else
                    {
                        // Attempt to parse the source expression and fail validation
                        // if an exception is thrown by the expression evaluator
                        try
                        {
                            evaluator.ParseExpression(listing.Definition.Left);
                        }
                        catch (WorkflowActivityLibraryException ex)
                        {
                            this.controller.ValidationError = ex.Message;
                            return false;
                        }

                        ParameterType targetType = ParameterType.String;
                        try
                        {
                            targetType = ExpressionEvaluator.DetermineParameterType(listing.Definition.Right, true);
                        }
                        catch (WorkflowActivityLibraryException)
                        {
                        }

                        // Target variables are valid
                        // For anything else, make sure that the target attribute matches the regular
                        // expression for FIM attributes
                        if (targetType != ParameterType.Variable)
                        {
                            if (!Regex.Match(listing.Definition.Right, RegexPattern).Success)
                            {
                                this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.TargetValidationError, listing.Definition.Right);
                                return false;
                            }
                        }
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
        /// Handles the TextChanged event of the Iteration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void Iteration_TextChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageFeatureDisablement();
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }
        
        /// <summary>
        /// Handles the CheckedChanged event of the ApplyAuthorizationPolicy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ApplyAuthorizationPolicy_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageFeatureDisablement();
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
                this.createdResourceIdTarget.Visible = this.advanced.Value;
                this.checkForConflict.Visible = this.advanced.Value;

                if (!this.advanced.Value)
                {
                    this.queries.HeaderRow.Visible = false;
                    this.queries.TableRow.Visible = false;
                    this.actorString.Visible = false;
                    this.conflictFilter.Visible = false;
                    this.conflictFilter.Required = false;
                    this.conflictingResourceIdTarget.Visible = false;
                    this.failOnConflict.Visible = false;
                }
                else
                {
                    this.ManageQueryControls();
                    this.ManageActorControls();
                    this.ManageConflictControls();
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

        /// <summary>
        /// Manages the feature disablement.
        /// </summary>
        private void ManageFeatureDisablement()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                bool iterate = !string.IsNullOrEmpty(this.iteration.Value);
                bool authorize = this.applyAuthorizationPolicy.Value;

                if (iterate || authorize)
                {
                    this.createdResourceIdTarget.Visible = false;
                }
                else
                {
                    this.createdResourceIdTarget.Visible = this.advanced.Value;
                }

                if (iterate)
                {
                    this.conflictingResourceIdTarget.Visible = false;
                }
                else
                {
                    this.conflictingResourceIdTarget.Visible = this.advanced.Value && this.checkForConflict.Value;
                }
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
                    this.conflictingResourceIdTarget.Visible = true;
                    this.failOnConflict.Visible = true;
                }
                else
                {
                    this.conflictFilter.Visible = false;
                    this.conflictFilter.Required = false;
                    this.conflictingResourceIdTarget.Visible = false;
                    this.failOnConflict.Visible = false;
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