//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="AddDelayForm.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// AddDelay Activity Form 
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
    /// AddDelay Activity UI
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "The internal class will be reflected by the framework and instantiated.")]
    internal class AddDelayForm : ActivitySettingsPart
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
        /// The activity form controller
        /// </summary>
        private readonly ActivityFormController controller = new ActivityFormController(ActivitySettings.AddDelay);

        /// <summary>
        /// The timeout duration textbox
        /// </summary>
        private readonly ActivityTextBox timeoutDuration;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDelayForm"/> class.
        /// </summary>
        public AddDelayForm()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                // Add the standard controls to the form via the activity form controller
                this.activityDisplayName = this.controller.AddDisplayNameTextBox(ActivitySettings.ActivityDisplayName);
                this.activityExecutionCondition = this.controller.AddTextBox(ActivitySettings.ActivityExecutionCondition, ActivitySettings.ConditionHelpText, false, true);
                this.timeoutDuration = this.controller.AddTextBox(ActivitySettings.AddDelayTimeoutDuration, ActivitySettings.AddDelayTimeoutDurationHelpText, true, true);
                this.timeoutDuration.TextBoxControl.Width = 300;
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
                // Create a new instance of the AddDelay activity and assign
                // dependenty property values based on inputs to standard activity controls
                AddDelay wfa = new AddDelay
                {
                    ActivityDisplayName = this.activityDisplayName.Value,
                    ActivityExecutionCondition = this.activityExecutionCondition.Value,
                    TimeoutDuration = this.timeoutDuration.Value,
                };

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
                // Cast the supplied activity as a AddDelay activity
                AddDelay wfa = activity as AddDelay;
                if (wfa == null)
                {
                    return;
                }

                // Set form control values based on the activity's dependency properties
                this.activityDisplayName.Value = wfa.ActivityDisplayName;
                this.activityExecutionCondition.Value = wfa.ActivityExecutionCondition;
                this.timeoutDuration.Value = wfa.TimeoutDuration;
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
                // validate any other advanced configurations
                ExpressionEvaluator evaluator = new ExpressionEvaluator();

                try
                {
                    if (ExpressionEvaluator.IsExpression(this.timeoutDuration.Value))
                    {
                        evaluator.ParseExpression(this.timeoutDuration.Value);
                    }
                    else
                    {
                        TimeSpan parsedTimeSpan;
                        if (!TimeSpan.TryParse(this.timeoutDuration.Value, out parsedTimeSpan))
                        {
                            this.controller.ValidationError = ActivitySettings.AddDelayTimeoutDurationHelpText;
                            return false;
                        }
                    }
                }
                catch (WorkflowActivityLibraryException ex)
                {
                    this.controller.ValidationError = ex.Message;
                    return false;
                }

                try
                {
                    if (!string.IsNullOrEmpty(this.activityExecutionCondition.Value))
                    {
                        evaluator.ParseExpression(this.activityExecutionCondition.Value);
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

        #endregion
    }
}