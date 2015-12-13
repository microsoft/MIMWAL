//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="GenerateUniqueValueForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// GenerateUniqueValue Activity Form 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Workflow.ComponentModel;
    using Microsoft.IdentityManagement.WebUI.Controls;
    using Microsoft.ResourceManagement.Workflow.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Definitions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.FormController;

    #endregion

    /// <summary>
    /// GenerateUniqueValue Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class GenerateUniqueValueForm : ActivitySettingsPart
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
        /// The conflict filter textbox
        /// </summary>
        private readonly ActivityTextBox conflictFilter;

        /// <summary>
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.GenerateUniqueValue);

        /// <summary>
        /// The LDAP queries definitions controller
        /// </summary>
        private readonly DefinitionsController ldapQueries;

        /// <summary>
        /// The publication target textbox
        /// </summary>
        private readonly ActivityTextBox publicationTarget;

        /// <summary>
        /// The query LDAP checkbox
        /// </summary>
        private readonly ActivityCheckBox queryLdap;

        /// <summary>
        /// The uniqueness seed numeric textbox
        /// </summary>
        private readonly ActivityTextBoxNumeric uniquenessSeed;

        /// <summary>
        /// The value expressions definitions controller
        /// </summary>
        private readonly DefinitionsController valueExpressions;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateUniqueValueForm"/> class.
        /// </summary>
        public GenerateUniqueValueForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add the standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);
                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, true);
                this.publicationTarget = this.controller.AddTextBox(ActivitySettings.TargetForGeneratedValue, ActivitySettings.TargetForGeneratedValueHelpText, true);
                this.conflictFilter = this.controller.AddTextBox(ActivitySettings.ConflictFilter, ActivitySettings.ConflictFilterHelpText, true);
                this.queryLdap = this.controller.AddCheckBox(ActivitySettings.QueryLdapForConflicts, ActivitySettings.QueryLdapForConflictsHelpText);

                // Create a new instance of the definitions controller to capture ldap query definitions
                // The visibility of the queries control will be governed by the Query LDAP checkbox
                this.ldapQueries = new DefinitionsController("LDAPQueries", 250, 330, 0)
                {
                    DisplayName = ActivitySettings.LdapQueries,
                    Description = ActivitySettings.LdapQueriesHelpText,
                    LeftHeader = ActivitySettings.LdapQueriesLeftHeader,
                    RightHeader = ActivitySettings.LdapQueriesRightHeader
                };
                this.ldapQueries.HeaderRow.Visible = false;
                this.ldapQueries.TableRow.Visible = false;
                this.controller.ActivityControlTable.Rows.Add(this.ldapQueries.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.ldapQueries.TableRow);
                this.uniquenessSeed = this.controller.AddTextBoxNumeric(ActivitySettings.UniquenessKeySeed, ActivitySettings.UniquenessKeySeedHelpText, true);
                this.uniquenessSeed.DisplayZeroValue = true;

                // Create a new definitions controller to allow for the specification
                // of value expressions
                this.valueExpressions = new DefinitionsController("ValueExpressions", 500, 0, 0)
                {
                    DisplayName = ActivitySettings.ValueExpressions,
                    Description = ActivitySettings.ValueExpressionsHelpText,
                    LeftHeader = ActivitySettings.ValueExpressionsLeftHeader
                };
                this.controller.ActivityControlTable.Rows.Add(this.valueExpressions.HeaderRow);
                this.controller.ActivityControlTable.Rows.Add(this.valueExpressions.TableRow);

                this.queryLdap.CheckBoxControl.CheckedChanged += this.QueryLdap_CheckedChanged;
                this.queryLdap.CheckBoxControl.AutoPostBack = true;
                this.conflictFilter.TextBoxControl.Width = 300;
                this.uniquenessSeed.Value = 2;
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
                // Create a new instance of the GenerateUniqueValue activity and assign
                // dependenty property values based on inputs to standard activity controls
                GenerateUniqueValue wfa = new GenerateUniqueValue
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    PublicationTarget = this.publicationTarget.Value,
                    ConflictFilter = this.conflictFilter.Value,
                    QueryLdap = this.queryLdap.Value,
                    UniquenessSeed = this.uniquenessSeed.Value,
                    ValueExpressions = this.FetchValueExpressions()
                };

                DefinitionsConverter ldapQueriesConverter = new DefinitionsConverter(this.ldapQueries.DefinitionListings);
                wfa.LdapQueriesTable = ldapQueriesConverter.DefinitionsTable;

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
                // Cast the supplied activity as a GenerateUniqueValue activity
                GenerateUniqueValue wfa = activity as GenerateUniqueValue;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.publicationTarget.Value = wfa.PublicationTarget;
                this.conflictFilter.Value = wfa.ConflictFilter;
                this.queryLdap.Value = wfa.QueryLdap;
                this.uniquenessSeed.Value = wfa.UniquenessSeed;
                this.LoadValueExpressions(wfa.ValueExpressions);
                this.ldapQueries.LoadActivitySettings(wfa.LdapQueriesTable);
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
                ActivitySettingsPartData data = this.controller.PersistSettings();
                data = this.ldapQueries.PersistSettings(data);
                data["ValueExpressions"] = this.FetchValueExpressions();
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
                // Use the controller and definitions controller to restore settings for activity controls
                this.controller.RestoreSettings(data);
                this.ldapQueries.RestoreSettings(data);
                if (data != null && data["ValueExpressions"] != null)
                {
                    this.LoadValueExpressions((ArrayList)data["ValueExpressions"]);
                }

                this.ManageQueryLdapControls();
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
                // The definitions controller will manage the mode of associated controls
                this.controller.SwitchMode(mode);
                this.ldapQueries.SwitchMode(mode);
                this.valueExpressions.SwitchMode(mode);
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

                try
                {
                    // Verify that the target lookup is valid
                    LookupEvaluator targetLookup = new LookupEvaluator(this.publicationTarget.Value);
                    if (!targetLookup.IsValidTarget)
                    {
                        this.controller.ValidationError = string.Format(CultureInfo.CurrentUICulture, ActivitySettings.TargetLookupValidationError, this.publicationTarget.Value);
                        return false;
                    }
                }
                catch (WorkflowActivityLibraryException ex)
                {
                    // If an exception was thrown while attempting to parse lookups, report the error
                    this.controller.ValidationError = ex.Message;
                    return false;
                }

                // Verify that the supplied conflict filter is a valid XPath query
                if (!ExpressionEvaluator.IsXPath(this.conflictFilter.Value))
                {
                    this.controller.ValidationError = ActivitySettings.ConflictFilterValidationError;
                    return false;
                }

                // Verify that the supplied conflict filter contains the [//Value] lookup
                if (!this.conflictFilter.Value.ToUpperInvariant().Contains("[//VALUE]"))
                {
                    this.controller.ValidationError = ActivitySettings.ConflictFilterValueLookupValidationError;
                    return false;
                }

                ExpressionEvaluator evaluator = new ExpressionEvaluator();

                if (this.queryLdap.Value)
                {
                    // Loop through all active query listings and make sure they are valid
                    foreach (DefinitionListing query in this.ldapQueries.DefinitionListings.Where(query => query.Active))
                    {
                        // If a value is missing for key or query, the definition
                        // will be null and the listing fails validation
                        if (query.Definition == null)
                        {
                            this.controller.ValidationError = ActivitySettings.LdapQueryDefinitionValidationError;
                            return false;
                        }

                        // Make sure that the specified XPath filter is properly formatted
                        if (!query.Definition.Right.ToUpperInvariant().Contains("[//VALUE]"))
                        {
                            this.controller.ValidationError = ActivitySettings.LdapQueryDefinitionValueLookupValidationError;
                            return false;
                        }

                        // If it's an expression, make sure it's properly formatted
                        if (ExpressionEvaluator.IsExpression(query.Definition.Left))
                        {
                            try
                            {
                                evaluator.ParseExpression(query.Definition.Left);
                            }
                            catch (WorkflowActivityLibraryException ex)
                            {
                                this.controller.ValidationError = ex.Message;
                                return false;
                            }
                        }
                    }
                }

                // Count the active value expressions
                int count = this.valueExpressions.DefinitionListings.Count(valueExpression => valueExpression.Active);

                // Loop through all active update listings and make sure they are valid
                int i = 1;
                foreach (DefinitionListing valueExpression in this.valueExpressions.DefinitionListings.Where(valueExpression => valueExpression.Active))
                {
                    if (string.IsNullOrEmpty(valueExpression.State.Left))
                    {
                        // If a value is missing for the expression, fail validation
                        this.controller.ValidationError = ActivitySettings.ValueExpressionValidationError;
                        return false;
                    }

                    // Attempt to parse the value expression
                    try
                    {
                        evaluator.ParseExpression(valueExpression.State.Left);
                    }
                    catch (WorkflowActivityLibraryException ex)
                    {
                        this.controller.ValidationError = ex.Message;
                        return false;
                    }

                    // Verify that the [//UniquenessKey] lookup is only present in the last value expression
                    bool containsKey = valueExpression.State.Left.ToUpperInvariant().Contains("[//UNIQUENESSKEY]");
                    if (i < count && containsKey)
                    {
                        this.controller.ValidationError = ActivitySettings.ValueExpressionUniquenessKeyValidationError;
                        return false;
                    }

                    if (i == count && !containsKey)
                    {
                        this.controller.ValidationError = ActivitySettings.ValueExpressionMissingUniquenessKeyValidationError;
                        return false;
                    }

                    i += 1;
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
        /// Handles the CheckedChanged event of the QueryLDAP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void QueryLdap_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ManageQueryLdapControls();
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
        /// Manages the query LDAP controls.
        /// </summary>
        private void ManageQueryLdapControls()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ldapQueries.HeaderRow.Visible = this.queryLdap.Value;
                this.ldapQueries.TableRow.Visible = this.queryLdap.Value;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Fetches the value expressions from the value expressions definition listings.
        /// </summary>
        /// <returns>The ArrayList of value expressions</returns>
        private ArrayList FetchValueExpressions()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                ArrayList valueExpressionList = new ArrayList();
                foreach (DefinitionListing listing in this.valueExpressions.DefinitionListings.Where(listing => listing.State != null && !string.IsNullOrEmpty(listing.State.Left)))
                {
                    valueExpressionList.Add(listing.State.Left);
                }

                return valueExpressionList;
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Loads the value expressions into the value expressions definition listings.
        /// </summary>
        /// <param name="valueExpressionList">The value expression list.</param>
        private void LoadValueExpressions(ArrayList valueExpressionList)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                for (int i = 0; i < valueExpressionList.Count; i++)
                {
                    this.valueExpressions.DefinitionListings[i].State = new DefinitionListingState(false, valueExpressionList[i].ToString(), string.Empty, false);
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