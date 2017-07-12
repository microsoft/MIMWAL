//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionEvaluator.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ExpressionEvaluator class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Evaluates Expressions
    /// </summary>
    [Serializable]
    public class ExpressionEvaluator
    {
        #region Declarations

        /// <summary>
        /// The reserved variable break iteration
        /// </summary>
        public const string ReservedVariableBreakIteration = "$__BREAK_ITERATION__";

        /// <summary>
        /// The lookup cache
        /// </summary>
        private readonly Dictionary<string, object> lookupCache = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The variable cache
        /// </summary>
        private readonly Dictionary<string, object> variableCache = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase) { { ExpressionEvaluator.ReservedVariableBreakIteration, false } };

        #endregion

        #region Properties

        /// <summary>
        /// Gets the lookup cache, a dictionary of lookup expressions and their values (when resolved).
        /// </summary>
        public Dictionary<string, object> LookupCache
        {
            get
            {
                return this.lookupCache;
            }
        }

        /// <summary>
        /// Gets the variable cache, a dictionary of variable expressions and their values (when resolved).
        /// </summary>
        public Dictionary<string, object> VariableCache
        {
            get
            {
                return this.variableCache;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines the type of the parameter.
        /// </summary>
        /// <param name="parameter">The function parameter expression.</param>
        /// <param name="suppressValidationError">Indicates whether to suppress the validation error or not.</param>
        /// <returns>The ParameterType.</returns>
        public static ParameterType DetermineParameterType(string parameter, bool suppressValidationError)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorDetermineParameterType, "Parameter: '{0}'. SuppressValidationError: '{1}'.", parameter, suppressValidationError);

            if (parameter == null)
            {
                throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorDetermineParameterTypeValidationError, new WorkflowActivityLibraryException(Messages.ExpressionEvaluator_ExpressionParameterTypeValidationError, "null"));
            }

            ParameterType parameterType = ParameterType.Unknown;

            try
            {
                long parseInteger;
                bool parseBoolean;

                // Determine what type of function parameter we are looking at based on its characteristics
                // Expression: multiple concatenated components
                // String: wrapped in quotation marks
                // Integer: parsable as Integer
                // Boolean: parsable as Boolean
                // Function: contains ( and ends with )
                // Lookup: starts with [// and ends with ]
                // Variable: starts with $ and does not contain invalid characters
                if (IdentifyExpressionComponents(parameter).Count > 1)
                {
                    parameterType = ParameterType.Expression;
                }
                else if (long.TryParse(parameter, out parseInteger))
                {
                    parameterType = ParameterType.Integer;
                }
                else if (bool.TryParse(parameter, out parseBoolean))
                {
                    parameterType = ParameterType.Boolean;
                }
                else if (parameter.StartsWith("\"", StringComparison.OrdinalIgnoreCase) && parameter.EndsWith("\"", StringComparison.OrdinalIgnoreCase))
                {
                    parameterType = ParameterType.String;
                }
                else if (parameter.Contains("(") && !parameter.StartsWith("(", StringComparison.OrdinalIgnoreCase) && parameter.EndsWith(")", StringComparison.OrdinalIgnoreCase))
                {
                    // check added for NOT StartsWith '(' as function got to have a name
                    parameterType = ParameterType.Function;
                }
                else if (parameter.StartsWith("[//", StringComparison.OrdinalIgnoreCase) && parameter.EndsWith("]", StringComparison.OrdinalIgnoreCase))
                {
                    parameterType = ParameterType.Lookup;

                    ValidateLookupParameterType(parameter);
                }
                else if (Regex.Match(parameter, @"^[($)][(a-z)(A-Z)(_)][(a-z)(A-Z)(0-9)(_)]*$").Success)
                {
                    parameterType = ParameterType.Variable;
                }
                else if (suppressValidationError)
                {
                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorDetermineParameterType, Messages.ExpressionEvaluator_ExpressionParameterTypeValidationError, parameter);
                }
                else
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorDetermineParameterTypeValidationError, new InvalidExpressionException(Messages.ExpressionEvaluator_ExpressionParameterTypeValidationError, parameter));
                }

                return parameterType;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorDetermineParameterType, "Parameter: '{0}'. SuppressValidationError: {1}. Parameter Type: '{2}'.", parameter, suppressValidationError, parameterType);
            }
        }

        /// <summary>
        /// Determines the type of the parameter.
        /// </summary>
        /// <param name="parameter">The function parameter expression.</param>
        /// <returns>The ParameterType.</returns>
        public static ParameterType DetermineParameterType(string parameter)
        {
            return DetermineParameterType(parameter, false);
        }

        /// <summary>
        /// Test if the specified input string is a XPath search filter
        /// </summary>
        /// <param name="input">input string to test as XPath</param>
        /// <returns>True if the specified input string is XPath. Otherwise false.</returns>
        public static bool IsXPath(string input)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorIsXPath, "Input: '{0}'.", input);

            bool result = !string.IsNullOrEmpty(input) && input.StartsWith("/", StringComparison.OrdinalIgnoreCase);

            Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorIsXPath, "Input: '{0}'. Returning: {1}.", input, result);

            return result;
        }

        /// <summary>
        /// Test if the specified input string is an expression
        /// </summary>
        /// <param name="input">input string to test as an expression</param>
        /// <returns>True if the specified input string is an expression. Otherwise false.</returns>
        public static bool IsExpression(string input)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorIsExpression, "Input: '{0}'.", input);

            bool result = !string.IsNullOrEmpty(input) && DetermineParameterType(input, true) != ParameterType.Unknown;

            Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorIsExpression, "Input: '{0}'. Returning: {1}.", input, result);

            return result;
        }

        /// <summary>
        /// Test if the specified input string is a value expression
        /// </summary>
        /// <param name="input">input string to test as a value expression</param>
        /// <returns>True if the specified input string is a value expression. Otherwise false.</returns>
        public static bool IsValueExpression(string input)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorIsValueExpression, "Input: '{0}'.", input);

            string valueLookup = string.Format(CultureInfo.InvariantCulture, "[//{0}]", LookupParameter.Value).ToUpperInvariant();
            string valueLookup2 = string.Format(CultureInfo.InvariantCulture, "[//{0}/", LookupParameter.Value).ToUpperInvariant();

            bool result = !string.IsNullOrEmpty(input) && DetermineParameterType(input, true) != ParameterType.Unknown
                && (input.ToUpperInvariant().Contains(valueLookup) || input.ToUpperInvariant().Contains(valueLookup2));

            Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorIsValueExpression, "Input: '{0}'. Returning: {1}.", input, result);

            return result;
        }

        /// <summary>
        /// Test if the specified input string is an email address
        /// </summary>
        /// <param name="input">input string to test as an email address</param>
        /// <returns>True if the specified input string is an email address. Otherwise false.</returns>
        public static bool IsEmailAddress(string input)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorIsEmailAddress, "Input: '{0}'.", input);

            bool result = !string.IsNullOrEmpty(input) && input.Contains("@");

            Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorIsEmailAddress, "Input: '{0}'. Returning: {1}.", input, result);

            return result;
        }

        /// <summary>
        /// Determines whether the specified expression is a boolean expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>True if the expression is a boolean expression. Otherwise returns false.</returns>
        public bool IsBooleanExpression(string expression)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorIsBooleanExpression, "Expression: '{0}'.", expression);

            bool booleanExpression = false;

            try
            {
                booleanExpression = this.EvaluateExpression(expression, EvaluationMode.Parse) is bool;

                return booleanExpression;
            }
            catch (WorkflowActivityLibraryException ex)
            {
                Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorIsBooleanExpressionException, ex);
                return false;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorIsBooleanExpression, "The expression: '{0}' is boolean '{1}'.", expression, booleanExpression);
            }
        }

        /// <summary>
        /// Parses the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public void ParseExpression(string expression)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorParseExpression, "Expression: '{0}'.", expression);

            try
            {
                this.EvaluateExpression(expression, EvaluationMode.Parse);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorParseExpression, "Expression: '{0}'.", expression);
            }
        }

        /// <summary>
        /// Parses the expression if it's a valid expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>True if the input is a valid expression. Otherwise returns false.</returns>
        public bool ParseIfExpression(string expression)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorParseIfExpression, "Expression: '{0}'.", expression);

            bool isExpression = false;

            try
            {
                isExpression = IsExpression(expression);
                if (isExpression)
                {
                    this.ParseExpression(expression);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorParseIfExpression, "Expression: '{0}'. IsExpression: {1}.", expression, isExpression);
            }

            return isExpression;
        }

        /// <summary>
        /// Resolves the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the resolved expression.</returns>
        public object ResolveExpression(string expression)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorResolveExpression, "Expression: '{0}'.", expression);

            object result = null;
            try
            {
                result = this.EvaluateExpression(expression, EvaluationMode.Resolve);

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorResolveExpression, "Expression: '{0}'. Returning: '{1}'.", expression, result);
            }
        }

        /// <summary>
        /// Publishes the variable by storing it in the variableCache field.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="value">The value.</param>
        /// <param name="mode">The mode.</param>
        public void PublishVariable(string variable, object value, UpdateMode mode)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorPublishVariable, "Variable: '{0}'. Value: '{1}'. Update Mode: '{2}'.", variable, value, mode);

            try
            {
                // If the supplied variable doesn't match an entry in the variable cache,
                // do nothing
                if (!this.variableCache.ContainsKey(variable))
                {
                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "The variable '{0}' doesn't match an entry in the variable cache.", variable);
                    return;
                }

                // For modify, simply publish the value to the variable cache
                if (mode == UpdateMode.Modify)
                {
                    this.variableCache[variable] = value;

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "Modified variable '{0}' with value '{1}' in the variable cache.", variable, value);
                }
                else if (value != null)
                {
                    // Use reflection to determine the expected List<> type based on the value
                    // Also get the Add and Remove methods for the list
                    Type listType = typeof(List<>).MakeGenericType(new Type[] { value.GetType() });
                    MethodInfo add = listType.GetMethod("Add");
                    MethodInfo remove = listType.GetMethod("Remove");

                    switch (mode)
                    {
                        case UpdateMode.Insert:
                            if (this.variableCache[variable] == null)
                            {
                                this.variableCache[variable] = value;

                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "Inserted variable '{0}' with value '{1}' into the variable cache.", variable, value);
                            }
                            else if (this.variableCache[variable].GetType() == value.GetType())
                            {
                                // Single value, create a new instance of the appropriate List<> type
                                // and add both values: existing and new
                                object existingValue = this.variableCache[variable];
                                this.variableCache[variable] = Activator.CreateInstance(listType);
                                add.Invoke(this.variableCache[variable], new object[] { existingValue });
                                add.Invoke(this.variableCache[variable], new object[] { value });

                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "Inserted variable '{0}' with second value '{1}' into the variable cache.", variable, value);
                            }
                            else if (this.variableCache[variable].GetType() == listType)
                            {
                                // The variable is a list of the expected type, add the value
                                add.Invoke(this.variableCache[variable], new object[] { value });

                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "Inserted variable '{0}' with value '{1}' into the list in the variable cache.", variable, value);
                            }
                            else
                            {
                                // We have a problem and need to report an error
                                throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorPublishVariableInsertVariableError, new WorkflowActivityLibraryException(Messages.ExpressionEvaluator_PublishVariableInsertVariableError, value.GetType(), variable, this.variableCache[variable].GetType()));
                            }

                            break;
                        case UpdateMode.Remove:
                            if (this.variableCache[variable] == null)
                            {
                                // Do nothing
                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "Nothing to remove since the variable cache does not contain variable '{0}'.", variable);
                            }
                            else if (this.variableCache[variable].Equals(value))
                            {
                                // A single matching value exists, clear the variable
                                this.variableCache[variable] = null;

                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "Remove variable '{0}' with value '{1}' from the variable cache.", variable, value);
                            }
                            else if (this.variableCache[variable].GetType() == listType)
                            {
                                // The variable is a list of the expected type, attempt to remove the value
                                remove.Invoke(this.variableCache[variable], new object[] { value });

                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorPublishVariable, "Remove variable '{0}' with value '{1}' from the list in the variable cache.", variable, value);

                                // Check the count on the list to determine if we are down to a single value or have no value
                                // If so, adjust the value of the variable accordingly to eliminate the list
                                object listValue = null;
                                int i = 0;
                                foreach (object o in (IEnumerable)this.variableCache[variable])
                                {
                                    i += 1;
                                    listValue = o;
                                }

                                if (i <= 1)
                                {
                                    this.variableCache[variable] = listValue;
                                }
                            }

                            break;
                    }
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorPublishVariable, "Variable: '{0}'. Value: '{1}'. Update Mode: '{2}'.", variable, value, mode);
            }
        }

        /// <summary>
        /// Identifies the expression components.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The ArrayList of expression components.</returns>
        private static ArrayList IdentifyExpressionComponents(string expression)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorIdentifyExpressionComponents, "Expression: '{0}'.", expression);

            ArrayList components = new ArrayList();

            try
            {
                // Determine if there is an open string or function
                // by evaluating quotation mark and parentheses characters
                int openFunctions = 0;
                bool openString = false;
                foreach (char c in expression)
                {
                    if (c.Equals('\"'))
                    {
                        openString = !openString;
                    }

                    if (c.Equals('(') && !openString)
                    {
                        openFunctions += 1;
                    }

                    if (c.Equals(')') && !openString)
                    {
                        openFunctions -= 1;
                    }
                }

                // If there is an open string or the number of open and close 
                // parentheses characters do not match, throw an exception
                if (openString)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorIdentifyExpressionComponentsQuotesValidationError, new InvalidExpressionException(Messages.ExpressionEvaluator_ExpressionQuotesValidationError, expression));
                }

                if (openFunctions != 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorIdentifyExpressionComponentsParenthesisValidationError, new InvalidExpressionException(Messages.ExpressionEvaluator_ExpressionParenthesisValidationError, expression));
                }

                // The function expression could contain + characters which are wrapped in quotations
                // Consequently, we can't assume that a split by + returns each expression component
                // We need to loop through each and determine if a component needs to be reassembled
                StringBuilder reassembled = new StringBuilder();

                foreach (string s in expression.Split('+'))
                {
                    // Toggle the open string flag every time we encounter
                    // a quotation mark
                    foreach (char c in s)
                    {
                        if (c.Equals('\"'))
                        {
                            openString = !openString;
                        }

                        if (c.Equals('(') && !openString)
                        {
                            openFunctions += 1;
                        }

                        if (c.Equals(')') && !openString)
                        {
                            openFunctions -= 1;
                        }
                    }

                    // Add the string to the reassembled string builder
                    // and determine how to proceed based on whether or not
                    // we are currently reassembling component fragments
                    reassembled.Append(s);
                    if (openString || openFunctions != 0)
                    {
                        reassembled.Append("+");
                    }
                    else
                    {
                        // If a component is not open, it either means that no
                        // reassembly was required or we have completed reassembly
                        // Either way, add the component to the list of components
                        // and reset the string builder
                        components.Add(reassembled.ToString().Trim());
                        reassembled = new StringBuilder();
                    }
                }

                return components;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorIdentifyExpressionComponents, "The expression '{0}' is identified to have {1} components.", expression, components.Count);
            }
        }

        /// <summary>
        /// Escapes the string.
        /// A string is escaped by removing the quotation marks at its start and finish and
        /// replacing double quotes "" with a single quotation mark "
        /// </summary>
        /// <param name="value">The string to be escaped.</param>
        /// <returns>The escaped string.</returns>
        private static string EscapeString(string value)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorEscapeString, "Value: '{0}'.", value);

            string escapedString = string.Empty;
            try
            {
                if (!value.StartsWith("\"", StringComparison.OrdinalIgnoreCase) ||
                    !value.EndsWith("\"", StringComparison.OrdinalIgnoreCase))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEscapeStringQuotesValidationError, new InvalidExpressionException(Messages.ExpressionEvaluator_StringQuotesValidationError, value));
                }

                escapedString = value.Substring(1, value.Length - 2).Replace("\"\"", "\"");

                return escapedString;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorEscapeString, "The string '{0}' is escaped as: '{1}'.", value, escapedString);
            }
        }

        /// <summary>
        /// Validates the parameter of type lookup.
        /// </summary>
        /// <param name="parameter">The parameter of type Lookup.</param>
        private static void ValidateLookupParameterType(string parameter)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorValidateLookupParameterType, "Parameter: '{0}'.", parameter);

            try
            {
                bool lookupFound = Enum.GetValues(typeof(LookupParameter)).Cast<LookupParameter>().Select(lookupParameter => string.Format(CultureInfo.InvariantCulture, "[//{0}", lookupParameter.ToString())).Any(lookupSyntax => parameter.StartsWith(lookupSyntax, StringComparison.OrdinalIgnoreCase));
                if (!lookupFound)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorValidateLookupParameterTypeUnsupportedTypeError, new InvalidExpressionException(Messages.ExpressionEvaluator_ExpressionParameterTypeUnsupportedTypeError, parameter));
                }

                string syntax = string.Format(CultureInfo.InvariantCulture, "[//{0}", LookupParameter.RequestParameter);
                if (parameter.StartsWith(syntax, StringComparison.OrdinalIgnoreCase))
                {
                    // The only supported syntax is [//RequestParameter/AllChangesAuthorizationTable] or [//RequestParameter/AllChangesActionTable]
                    string syntax2 = string.Format(CultureInfo.InvariantCulture, "[//{0}/AllChangesAuthorizationTable]", LookupParameter.RequestParameter);
                    string syntax3 = string.Format(CultureInfo.InvariantCulture, "[//{0}/AllChangesActionTable]", LookupParameter.RequestParameter);
                    if (!parameter.Equals(syntax2, StringComparison.OrdinalIgnoreCase) && !parameter.Equals(syntax3, StringComparison.OrdinalIgnoreCase))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorValidateLookupParameterTypeUnsupportedTypeError, new InvalidExpressionException(Messages.ExpressionEvaluator_ExpressionParameterTypeUnsupportedTypeError, parameter));
                    }
                }

                /*
                // We support Delta as well as normal attributes
                [//ComparedRequest/Creator/DisplayName] 
                [//ComparedRequest/Delta/DisplayName] 
                [//ComparedRequest/Delta/Manager/DisplayName] 

                syntax = string.Format(CultureInfo.InvariantCulture, "[//{0}", LookupParameter.ComparedRequest);
                if (parameter.StartsWith(syntax, StringComparison.OrdinalIgnoreCase))
                {
                    // The only supported syntax is [//ComparedRequest/Delta/...]
                    string syntax2 = string.Format(CultureInfo.InvariantCulture, "[//{0}/{1}/", LookupParameter.ComparedRequest, LookupParameter.Delta);
                    if (!parameter.StartsWith(syntax2, StringComparison.OrdinalIgnoreCase))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorValidateLookupParameterTypeUnsupportedTypeError, new InvalidExpressionException(Messages.ExpressionEvaluator_ExpressionParameterTypeUnsupportedTypeError, parameter));
                    }
                }
                */
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorValidateLookupParameterType, "Parameter: '{0}'.", parameter);
            }
        }

        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>The result of the evaluated expression.</returns>
        private object EvaluateExpression(string expression, EvaluationMode mode)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorEvaluateExpression, "Expression: '{0}'. Evaluation Mode: '{1}'.", expression, mode);

            object result = null;
            try
            {
                ArrayList components = IdentifyExpressionComponents(expression);

                // If no concatenation is required, return the resolved expression
                if (components.Count == 1)
                {
                    result = this.EvaluateExpressionComponent(expression, mode);
                    return result;
                }

                // If the expression requires concatenation,
                // evaluate each component and resolve, if necessary
                if (mode == EvaluationMode.Parse)
                {
                    foreach (string component in components)
                    {
                        this.EvaluateExpressionComponent(component, mode);
                    }

                    return null;
                }

                StringBuilder concatenated = new StringBuilder();
                foreach (object value in components.Cast<string>().Select(component => this.EvaluateExpressionComponent(component, mode)).Where(value => value != null))
                {
                    concatenated.Append(value);
                }

                result = concatenated.Length > 0 ? concatenated.ToString() : null;
                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorEvaluateExpression, "Expression: '{0}'. Evaluation Mode: '{1}'. Returning: '{2}'.", expression, mode, result);
            }
        }

        /// <summary>
        /// Evaluates the expression component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>The result of the evaluated expression component.</returns>
        private object EvaluateExpressionComponent(string component, EvaluationMode mode)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorEvaluateExpressionComponent, "Component: '{0}'. Evaluation Mode: '{1}'.", component, mode);

            object returnValue = null;
            try
            {
                ParameterType type = DetermineParameterType(component);
                if (mode == EvaluationMode.Parse)
                {
                    // When performing a parse,
                    // the function should only return null when the component is valid
                    // Exceptions will be thrown otherwise to indicate invalidity
                    switch (type)
                    {
                        case ParameterType.Lookup:

                            // Load the lookup to the cache
                            if (!this.lookupCache.ContainsKey(component))
                            {
                                this.lookupCache.Add(component, null);
                            }

                            return null;

                        case ParameterType.Variable:

                            // Load the variable to the cache
                            if (!this.variableCache.ContainsKey(component))
                            {
                                this.variableCache.Add(component, null);
                            }

                            return null;

                        case ParameterType.Function:

                            // Evaluate the function to ensure its validity
                            return this.EvaluateFunction(component, mode);

                        default:

                            // For all other types, no further evaluation is needed
                            // and we can return null
                            return null;
                    }
                }

                // When performing a resolution,
                // the function should return the resolved expression component
                switch (type)
                {
                    case ParameterType.String:

                        // Return the string after removing the quotation marks
                        returnValue = EscapeString(component);
                        return returnValue;

                    case ParameterType.Integer:

                        // Return the long as FIM datatype is System.Int64
                        returnValue = long.Parse(component, CultureInfo.InvariantCulture);
                        return returnValue;

                    case ParameterType.Boolean:

                        // Return the Boolean value
                        returnValue = bool.Parse(component);
                        return returnValue;

                    case ParameterType.Lookup:

                        // Check the cache for the lookup and return its value if present
                        if (this.lookupCache.ContainsKey(component))
                        {
                            returnValue = this.lookupCache[component];
                            return returnValue;
                        }

                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateExpressionComponentLookupCacheValidationError, new InvalidExpressionException(Messages.ExpressionEvaluator_LookupCacheValidationError, component));

                    case ParameterType.Variable:

                        // Check the cache for the variable and return its value if present
                        if (this.variableCache.ContainsKey(component))
                        {
                            returnValue = this.variableCache[component];
                            return returnValue;
                        }

                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateExpressionComponentVariableCacheValidationError, new InvalidExpressionException(Messages.ExpressionEvaluator_VariableCacheValidationError, component));

                    case ParameterType.Function:

                        // Pass the function off to the EvaluateFunction method for resolution
                        // and return the result
                        returnValue = this.EvaluateFunction(component, mode);
                        return returnValue;

                    default:
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateExpressionComponentParameterTypeValidationError, new InvalidExpressionException());
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorEvaluateExpressionComponent, "Component: '{0}'. Evaluation Mode: '{1}'. Return Value: '{2}'.", component, mode, returnValue);
            }
        }

        /// <summary>
        /// Evaluates the function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>The result of the evaluated function.</returns>
        private object EvaluateFunction(string function, EvaluationMode mode)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionEvaluatorEvaluateFunction, "Function: '{0}'. Evaluation Mode: '{1}'.", function, mode);

            object result = null;
            try
            {
                // Locate the opening and closing () characters for the function
                // by looking for the first and last instance of each character
                // This will ignore any nested functions which may be used as a parameter
                int open = function.IndexOf('(');
                int close = function.LastIndexOf(')');

                // Break apart the function expression to identify the function name
                // and parameter string (content between parentheses)
                string functionName = function.Substring(0, open);
                string parameterString = function.Substring(open + 1, close - open - 1);

                // Create new array lists to hold the unresolved and resolved parameters for the function
                // Only evaluate parameters if the parameter string is not empty, as it will
                // be for functions like DateTimeNow() and Null()
                ArrayList unresolvedParameters = new ArrayList();
                ArrayList parameters = new ArrayList();
                if (!string.IsNullOrEmpty(parameterString.Trim()))
                {
                    // The function expression could contain nested functions with their own commas
                    // For example, Trim(Left(ReplaceString(attribute, "This", "That"), 8))
                    // Consequently, we can't assume that a split by comma returns each parameter
                    // We need to loop through each and determine if a parameter needs to be reassembled
                    // based on the positioning of ( and ) characters and quotation marks
                    StringBuilder reassembled = new StringBuilder();
                    bool openString = false;
                    int openFunctions = 0;

                    // First, make sure that there are an appropriate number of ( and ) characters
                    // and also make sure there is no open string in the function
                    foreach (char c in parameterString)
                    {
                        if (c.Equals('\"'))
                        {
                            openString = !openString;
                        }

                        if (c.Equals('(') && !openString)
                        {
                            openFunctions += 1;
                        }

                        if (c.Equals(')') && !openString)
                        {
                            openFunctions -= 1;
                        }
                    }

                    // If there is an open string or the number of open and close 
                    // parantheses characters do not match, throw an exception
                    if (openString)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateFunctionQuotesValidationError, new InvalidFunctionFormatException(Messages.ExpressionEvaluator_FunctionParameterQuotesValidationError, functionName));
                    }

                    if (openFunctions != 0)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateFunctionParenthesisValidationError, new InvalidFunctionFormatException(Messages.ExpressionEvaluator_FunctionParameterParenthesisValidationError, functionName));
                    }

                    // Loop through each parameter fragment, split by comma,
                    // and determine if reassembly is required
                    foreach (string s in parameterString.Split(','))
                    {
                        // Count the number of ( and ) characters in the current string
                        // Only consider the parentheses relevent if it is not in an open string
                        foreach (char c in s)
                        {
                            if (c.Equals('\"'))
                            {
                                openString = !openString;
                            }
                            else if (c.Equals('(') && !openString)
                            {
                                openFunctions += 1;
                            }
                            else if (c.Equals(')') && !openString)
                            {
                                openFunctions -= 1;
                            }
                        }

                        // Add the string to the reassembled string builder
                        // and determine how to proceed based on whether or not
                        // we are currently reassembling parameter fragments
                        reassembled.Append(s);
                        if (openFunctions > 0 || openString)
                        {
                            reassembled.Append(",");
                        }
                        else
                        {
                            // If a parameter is not open, it either means that no
                            // reassembly was required or we have completed reassembly
                            // Either way, add the parameter to the list of unresolved parameters
                            // and reset the string builder
                            unresolvedParameters.Add(reassembled.ToString().Trim());
                            reassembled = new StringBuilder();
                        }
                    }

                    foreach (string s in unresolvedParameters)
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionEvaluatorEvaluateFunction, "Resolving unresolved function parameter '{0}'.", s);

                        ParameterType type = DetermineParameterType(s);

                        switch (type)
                        {
                            case ParameterType.String:
                                // Add the string value after trimming the quotes
                                parameters.Add(EscapeString(s));
                                break;
                            case ParameterType.Integer:
                                // Add the parsed value as long as FIM datatype is System.Int64
                                parameters.Add(long.Parse(s, CultureInfo.InvariantCulture));
                                break;
                            case ParameterType.Boolean:
                                // Add the parsed Boolean value
                                parameters.Add(bool.Parse(s));
                                break;
                            case ParameterType.Lookup:
                                if (mode == EvaluationMode.Parse)
                                {
                                    // For parse, add the lookup to the cache 
                                    // Mark the grammar in the parameter list by adding the appropriate enum value
                                    if (!this.lookupCache.ContainsKey(s))
                                    {
                                        this.lookupCache.Add(s, null);
                                    }

                                    parameters.Add(ParameterType.Lookup);
                                }
                                else
                                {
                                    // For resolution, pull the value from the lookup cache and
                                    // add it to the parameter list
                                    if (this.lookupCache.ContainsKey(s))
                                    {
                                        parameters.Add(this.lookupCache[s]);
                                    }
                                    else
                                    {
                                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateFunctionLookupCacheValidationError, new InvalidFunctionFormatException(Messages.ExpressionEvaluator_LookupCacheValidationError, s));
                                    }
                                }

                                break;
                            case ParameterType.Variable:
                                if (mode == EvaluationMode.Parse)
                                {
                                    // For parse, add the variable to the cache 
                                    // Mark the variable in the parameter list by adding the appropriate enum value
                                    if (!this.variableCache.ContainsKey(s))
                                    {
                                        this.variableCache.Add(s, null);
                                    }

                                    parameters.Add(ParameterType.Variable);
                                }
                                else
                                {
                                    // For resolution, pull the value from the variable cache and
                                    // add it to the parameter list
                                    if (this.variableCache.ContainsKey(s))
                                    {
                                        parameters.Add(this.variableCache[s]);
                                    }
                                    else
                                    {
                                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateFunctionVariableCacheValidationError, new InvalidFunctionFormatException(Messages.ExpressionEvaluator_VariableCacheValidationError, s));
                                    }
                                }

                                break;
                            case ParameterType.Function:
                                if (mode == EvaluationMode.Parse)
                                {
                                    // For parse, recursively evaluate the function and any nested functions
                                    // Mark the function in the parameter list by adding the appropriate enum value
                                    this.EvaluateFunction(s, mode);
                                    parameters.Add(ParameterType.Function);
                                }
                                else
                                {
                                    // For resolution, recursively resolve the function and any nested functions
                                    // and add the end result to the parameter list for the current function
                                    parameters.Add(this.EvaluateFunction(s, mode));
                                }

                                break;
                            case ParameterType.Expression:

                                if (mode == EvaluationMode.Parse)
                                {
                                    // For parse, recursively evaluate the expression and any nested functions
                                    // Mark the expression in the paramter list by adding the appropriate enum value
                                    this.EvaluateExpression(s, mode);
                                    parameters.Add(ParameterType.Expression);
                                }
                                else
                                {
                                    // For resolution, recursively resolve the expression and any nested functions
                                    // and add the end result to the parameter list for the current function
                                    parameters.Add(this.EvaluateExpression(s, mode));
                                }

                                break;
                            default:
                                throw Logger.Instance.ReportError(EventIdentifier.ExpressionEvaluatorEvaluateFunctionParameterTypeValidationError, new InvalidFunctionFormatException());
                        }
                    }
                }

                // Special handling for EvaluateExpression() function so that the lookups in the expression to evaluate are resolved
                // Assumption: the lookups used in the expression are already used in some other expressions
                // e.g. Consider [//Query/Site/xUserTemplateExpression] returning IIF(Eq([//Target/Department],"HR"),"Template1","Template2")
                // We want EvaluateExpression([//Query/Site/xUserTemplateExpression]). 
                // In this case caller activity need to ensure that [//Target/Department] is already used any other expression
                // so that it's part of LookupCache.
                if (functionName.Equals(ParameterType.EvaluateExpression.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    Logger.Instance.WriteWarning(EventIdentifier.ExpressionEvaluatorEvaluateFunctionDeprecatedFunctionWarning, Messages.ExpressionFunction_DeprecatedFunctionWarning, ParameterType.EvaluateExpression.ToString(), "Resolve Dynamic Grammar capability of UpdateResources activity");

                    string expression = this.EvaluateExpression(parameterString, mode) as string; // so e.g. IIF(Eq([//Target/Department],"HR"),"Template1","Template2")
                    if (mode != EvaluationMode.Parse && !string.IsNullOrEmpty(expression))
                    {
                        // now evalaute the actual expression i.e. e.g. IIF(Eq([//Target/Department],"HR"),"Template1","Template2")
                        result = this.EvaluateExpression(expression, mode);
                    }
                }
                else
                {
                    // Evaluate the function to make sure it is properly formatted,
                    // all required parameters are available, and parameters are of the appropriate type
                    // For most functions, the root value is allowed to be null to handle 
                    // scenarios when an attribute or expression is resolved to null
                    // In this circumstance, an exception should not be thrown but the function should resolve to null
                    ExpressionFunction expressionFunction = new ExpressionFunction(functionName, parameters, mode);
                    result = expressionFunction.Run();
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionEvaluatorEvaluateFunction, "Function: '{0}'. Evaluation Mode: '{1}'. Returning: '{2}'.", function, mode, result);
            }
        }

        #endregion
    }
}