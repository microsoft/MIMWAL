//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolveLookupString.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ResolveLookupString class.
// Resolves a lookup string using ResolveLooup activity.
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.ComponentActivities
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Text;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;

    #endregion

    /// <summary>
    /// Resolves Lookup string
    /// </summary>
    internal partial class ResolveLookupString : SequenceActivity
    {
        #region Windows Workflow Designer generated code

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty StringForResolutionProperty =
            DependencyProperty.Register("StringForResolution", typeof(string), typeof(ResolveLookupString));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ResolvedProperty =
            DependencyProperty.Register("Resolved", typeof(string), typeof(ResolveLookupString));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty QueryResultsProperty =
            DependencyProperty.Register("QueryResults", typeof(Dictionary<string, List<Guid>>), typeof(ResolveLookupString));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ComparedRequestIdProperty =
            DependencyProperty.Register("ComparedRequestId", typeof(Guid), typeof(ResolveLookupString));

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(ResolveLookupString));

        #endregion

        #region Declarations

        /// <summary>
        /// The expression evaluator.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. VS designer renders public properties as dependency property.")]
        public ExpressionEvaluator ActivityExpressionEvaluator;

        /// <summary>
        /// The work-in-progress expression for resolution
        /// </summary>
        private string working;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveLookupString"/> class.
        /// </summary>
        public ResolveLookupString()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupStringConstructor);

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
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupStringConstructor);
            }
        }

        #region Dependancy Property Properties

        /// <summary>
        /// Gets or sets the lookup expression to be resolved.
        /// </summary>
        [Description("The lookup string to be resolved by the activity.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string StringForResolution
        {
            get
            {
                return (string)this.GetValue(StringForResolutionProperty);
            }

            set
            {
                this.SetValue(StringForResolutionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the resolved string.
        /// </summary>
        [Description("The resolved string.")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Resolved
        {
            get
            {
                return (string)this.GetValue(ResolvedProperty);
            }

            set
            {
                this.SetValue(ResolvedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the results for any queries which should be used for [//Queries/...] lookup resolution.
        /// </summary>
        /// <value>
        /// The query results.
        /// </value>
        [Description("The results for any queries which should be used for [//Queries/...] lookup resolution.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Dictionary<string, List<Guid>> QueryResults
        {
            get
            {
                return (Dictionary<string, List<Guid>>)this.GetValue(QueryResultsProperty);
            }

            set
            {
                this.SetValue(QueryResultsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the compared request identifier which should be used for [//ComparedRequest/...] lookup resolution.
        /// </summary>
        /// <value>
        /// The compared request identifier.
        /// </value>
        [Description("The compared request which should be used for [//ComparedRequest/...] lookup resolution.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ComparedRequestId
        {
            get
            {
                return (Guid)this.GetValue(ComparedRequestIdProperty);
            }

            set
            {
                this.SetValue(ComparedRequestIdProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value which should be used for [//Value/...] lookup resolution.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        [Description("The value which should be used for [//Value/...] lookup resolution.")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Value
        {
            get
            {
                return this.GetValue(ValueProperty);
            }

            set
            {
                this.SetValue(ValueProperty, value);
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
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupStringExecute);

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
                throw Logger.Instance.ReportError(EventIdentifier.ResolveLookupStringExecuteError, ex);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupStringExecute);
            }
        }

        /// <summary>
        /// Generates the lookup key which will replace the lookup in the working string.
        /// </summary>
        /// <param name="lookup">The lookup string.</param>
        /// <returns>The lookup key</returns>
        private static string GenerateLookupKey(string lookup)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupStringGenerateLookupKey, "Lookup: '{0}'.", lookup);

            string lookupKey = lookup;

            try
            {
                // Generate a lookup key which will replace the lookup in the working string
                // To make the string identifiable, the lookup will be made upper case and the "[//...]" syntax will be replaced with "~//...~"
                // This will ensure the while loop condition does not find the string and attempt to reprocess it
                // For example, "[//Delta/ExplicitMember/Added/DisplayName]" becomes "~//DELTA/EXLICITMEMBER/ADDED/DISPLAYNAME~"
                lookupKey = lookup.ToUpperInvariant().Replace("[", "~").Replace("]", "~");
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupStringGenerateLookupKey, "Returning: '{0}'.", lookupKey);
            }

            return lookupKey;
        }

        /// <summary>
        /// Handles the ExecuteCode event of the Parse CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Parse_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupStringParseExecuteCode, "StringForResolution: '{0}'.", this.StringForResolution);

            try
            {
                if (ExpressionEvaluator.IsXPath(this.StringForResolution))
                {
                    // We'll support XPath expressions /EmailTemplate[DisplayName='[//WorkflowData/EmailTemplate]']
                    // as well as /"EmailTemplate[DisplayName='" + [//WorkflowData/EmailTemplate] + "']"
                    string expression = this.StringForResolution.Substring(1);
                    if (!ExpressionEvaluator.IsExpression(expression))
                    {
                        // StringForResolution is of format /EmailTemplate[DisplayName='[//WorkflowData/EmailTemplate]']
                        this.ParseStringForResolutionLookups();
                    }
                    else
                    {
                        // StringForResolution is of format /"EmailTemplate[DisplayName='" + [//WorkflowData/EmailTemplate] + "']"
                        this.ActivityExpressionEvaluator.ParseIfExpression(expression);
                    }
                }
                else if (ExpressionEvaluator.IsExpression(this.StringForResolution))
                {
                    // StringForResolution is an expression
                    this.ActivityExpressionEvaluator.ParseExpression(this.StringForResolution);
                }
                else
                {
                    // StringForResolution is an not an XPath or an Expression.
                    // e.g. LDAP Filter: '(samAccountName=tuser1)'.
                    this.ParseStringForResolutionLookups();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupStringParseExecuteCode, "StringForResolution: '{0}'. Working string for resolution: '{1}'.", this.StringForResolution, this.working);
            }
        }

        /// <summary>
        /// Handles the ExecuteCode event of the ResolveString CodeActivity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResolveString_ExecuteCode(object sender, EventArgs e)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupStringResolveStringExecuteCode, "StringForResolution: '{0}'. Working string for resolution: '{1}'.", this.StringForResolution, this.working);

            try
            {
                if (ExpressionEvaluator.IsXPath(this.StringForResolution))
                {
                    // We'll support XPath expressions /EmailTemplate[DisplayName='[//WorkflowData/EmailTemplate]']
                    // as well as /"EmailTemplate[DisplayName='" + [//WorkflowData/EmailTemplate] + "']"
                    string expression = this.StringForResolution.Substring(1);
                    if (!ExpressionEvaluator.IsExpression(expression))
                    {
                        // StringForResolution is of format /EmailTemplate[DisplayName='[//WorkflowData/EmailTemplate]']
                        this.Resolved = this.ResolveStringForResolutionLookups();
                    }
                    else
                    {
                        // StringForResolution is of format /"EmailTemplate[DisplayName='" + [//WorkflowData/EmailTemplate] + "']"
                        this.Resolved = string.Format(CultureInfo.InvariantCulture, "/{0}", this.ActivityExpressionEvaluator.ResolveExpression(expression) as string);
                    }
                }
                else if (ExpressionEvaluator.IsExpression(this.StringForResolution))
                {
                    // StringForResolution is an expression
                    this.Resolved = this.ActivityExpressionEvaluator.ResolveExpression(this.StringForResolution) as string;
                }
                else
                {
                    // StringForResolution is an not an XPath or an Expression.
                    // e.g. LDAP Filter: '(samAccountName=tuser1)'.
                    this.Resolved = this.ResolveStringForResolutionLookups();
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupStringResolveStringExecuteCode, "StringForResolution: '{0}'. Resolved String: '{1}'.", this.StringForResolution, this.Resolved);
            }
        }

        /// <summary>
        /// Parses lookups in the string for resolution
        /// </summary>
        private void ParseStringForResolutionLookups()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupStringParseStringForResolutionLookups, "StringForResolution: '{0}'.", this.StringForResolution);

            try
            {
                // The resolution string will start with the supplied grammar expression
                // If the supplied expression is null, default to an empty string
                this.working = this.StringForResolution ?? string.Empty;

                // As we find lookups which match this criteria, they will be parsed and removed from the string, replaced with an identifiable replacement string
                // Therefor, loop until all lookups of this type have been removed from the expression
                while (this.working.Contains("[//") && this.working.Contains("]"))
                {
                    // Find the index of the first lookup that will be evaluated
                    int open = this.working.IndexOf("[//", StringComparison.OrdinalIgnoreCase);

                    // Find the closing index of the lookup by looking for the next ] character in the string
                    // If we find another [ character before the ] character, the lookup is invalid
                    int close = open;
                    bool valid = true;
                    bool closed = false;
                    foreach (char c in this.working.Substring(open, this.working.Length - open))
                    {
                        if (c.Equals('[') && close != open)
                        {
                            valid = false;
                            break;
                        }

                        if (c.Equals(']'))
                        {
                            closed = true;
                            break;
                        }

                        close += 1;
                    }

                    // If a ] character was not found, the lookup is invalid
                    // If there is a problem with lookup, break the loop to prevent any further
                    // resolution lookups
                    if (!closed)
                    {
                        valid = false;
                    }

                    if (!valid)
                    {
                        break;
                    }

                    // Using the identified indexes, pull the lookup from the string
                    // and determine if the lookup already exists in the lookups dictionary
                    // If not, add it
                    string lookup = this.working.Substring(open, close - open + 1);

                    // Replace the lookup with a string which will be replaced later after resolution
                    this.working = this.working.Replace(lookup, GenerateLookupKey(lookup));

                    // Store it in the ActivityExpressionEvaluator LookupCache
                    this.ActivityExpressionEvaluator.ParseExpression(lookup);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupStringParseStringForResolutionLookups, "StringForResolution: '{0}'. Working string for resolution: '{1}'.", this.StringForResolution, this.working);
            }
        }

        /// <summary>
        /// Resolves lookups in the string for resolution
        /// </summary>
        /// <returns>The resolved string</returns>
        private string ResolveStringForResolutionLookups()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ResolveLookupStringResolveStringForResolutionLookups, "StringForResolution: '{0}'.", this.StringForResolution);

            string resolvedString = this.working;

            try
            {
                // Resolve the lookup string by replacing the lookup replacement strings
                // with the associated resolved values
                foreach (string lookup in this.ActivityExpressionEvaluator.LookupCache.Keys)
                {
                    string valueString = string.Empty;
                    object value = this.ActivityExpressionEvaluator.LookupCache[lookup];
                    if (value != null)
                    {
                        // Based on the type for the resolved value, build the value string
                        // If the value is a list, loop through it to build a ; delimited list of values
                        // For all other object types, simply cast the value to a string
                        if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(List<>))
                        {
                            StringBuilder valueStringBuilder = new StringBuilder();
                            foreach (object o in (IEnumerable)value)
                            {
                                if (valueStringBuilder.Length > 0)
                                {
                                    valueStringBuilder.Append("; ");
                                }

                                valueStringBuilder.Append(o);
                            }

                            valueString = valueStringBuilder.ToString();
                        }
                        else
                        {
                            valueString = value.ToString();
                        }
                    }

                    // Replace the lookup key in the working string with the
                    // resolved value string
                    this.working = this.working.Replace(GenerateLookupKey(lookup), valueString);
                }

                resolvedString = this.working;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ResolveLookupStringResolveStringForResolutionLookups, "StringForResolution: '{0}'. Resolved String: '{1}'.", this.StringForResolution, resolvedString);
            }

            return resolvedString;
        }

        #endregion
    }
}