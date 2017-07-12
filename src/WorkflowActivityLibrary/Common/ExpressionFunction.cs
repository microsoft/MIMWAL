//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionFunction.cs" company="Microsoft">
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
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.Odbc;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using System.Xml.XPath;
    using Microsoft.ResourceManagement.WebServices;
    using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Represents a function expression
    /// </summary>
    public class ExpressionFunction
    {
        #region Declarations

        /// <summary>
        /// The function
        /// </summary>
        private readonly string function;

        /// <summary>
        /// The evaluation mode
        /// </summary>
        private readonly EvaluationMode mode;

        /// <summary>
        /// The function parameters
        /// </summary>
        private readonly ArrayList parameters;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionFunction" /> class.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mode">The mode.</param>
        public ExpressionFunction(string function, ArrayList parameters, EvaluationMode mode)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConstructor, "Function: '{0}'. Evaluation Mode: '{1}'.", function, mode);

            this.function = function;
            this.parameters = parameters;
            this.mode = mode;

            Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConstructor, "Function: '{0}'. Evaluation Mode: '{1}'.", function, mode);
        }

        #region Methods

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>The result of the function evaluation.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Reviewed and retained as this is part of the exception message formatting.")]
        public object Run()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRun, "Function: '{0}'. Evaluation Mode: '{1}'.", this.function, this.mode);

            try
            {
                // Based on the function name,
                // call the appropriate method and return the results
                switch (this.function.ToUpperInvariant())
                {
                    case "ADD":
                        return this.Add();

                    case "AFTER":
                        return this.After();

                    case "AND":
                        return this.And();

                    case "BEFORE":
                        return this.Before();

                    case "BITAND":
                        return this.BitAnd();

                    case "BITNOT":
                        return this.BitNot();

                    case "BITOR":
                        return this.BitOr();

                    case "CONCATENATE":
                        return this.Concatenate();

                    case "CONCATINATEMULTIVALUEDSTRING":
                        Logger.Instance.WriteWarning(EventIdentifier.ExpressionFunctionRunDeprecatedFunctionWarning, Messages.ExpressionFunction_DeprecatedFunctionWarning, "ConcatinateMultivaluedString", "ConcatenateMultivaluedString");
                        return this.ConcatenateMultivaluedString();

                    case "CONCATENATEMULTIVALUEDSTRING":
                        return this.ConcatenateMultivaluedString();

                    case "CONTAINS":
                        return this.Contains();

                    case "CONVERTFROMBASE64":
                        return this.ConvertFromBase64();

                    case "CONVERTSIDTOSTRING":
                        return this.ConvertSidToString();

                    case "CONVERTSTRINGTOGUID":
                    case "CONVERTTOGUID":
                        return this.ConvertStringToGuid();

                    case "CONVERTTOBASE64":
                        return this.ConvertToBase64();

                    case "CONVERTTOBOOLEAN":
                        return this.ConvertToBoolean();

                    case "CONVERTTONUMBER":
                        return this.ConvertToNumber();

                    case "CONVERTTOSTRING":
                        return this.ConvertToString();

                    case "CONVERTTOUNIQUEIDENTIFIER":
                        return this.ConvertToUniqueIdentifier();

                    case "COUNT":
                        return this.Count();

                    case "CREATESQLPARAMETER":
                        return this.CreateSqlParameter();

                    case "CREATESQLPARAMETER2":
                        return this.CreateSqlParameter2();

                    case "CRLF":
                        return this.CRLF();

                    case "DATETIMEADD":
                        return this.DateTimeAdd();

                    case "DATETIMEFORMAT":
                        return this.DateTimeFormat();

                    case "DATETIMEFROMFILETIMEUTC":
                        return this.DateTimeFromFileTimeUtc();

                    case "DATETIMENOW":
                        return this.DateTimeNow();

                    case "DATETIMETOFILETIMEUTC":
                        return this.DateTimeToFileTimeUtc();

                    case "DATETIMESUBTRACT":
                        return this.DateTimeSubtract();

                    case "EQ":
                        return this.Eq();

                    case "ESCAPEDNCOMPONENT":
                        return this.EscapeDNComponent();

                    case "EXECUTESQLNONQUERY":
                        return this.ExecuteSqlNonQuery();

                    case "EXECUTESQLSCALAR":
                        return this.ExecuteSqlScalar();

                    case "FIRST":
                        return this.First();

                    case "FORMATMULTIVALUEDLIST":
                        return this.FormatMultivaluedList();

                    case "GENERATERANDOMPASSWORD":
                        return this.GenerateRandomPassword();

                    case "GREATERTHAN":
                        return this.GreaterThan();

                    case "IIF":
                        return this.IIF();

                    case "INSERTVALUES":
                        return this.InsertValues();

                    case "ISPRESENT":
                        return this.IsPresent();

                    case "LAST":
                        return this.Last();

                    case "LEFT":
                        return this.Left();

                    case "LEFTPAD":
                        return this.LeftPad();

                    case "LENGTH":
                        return this.Length();

                    case "LESSTHAN":
                        return this.LessThan();

                    case "LOWERCASE":
                        return this.LowerCase();

                    case "LTRIM":
                        return this.LTrim();

                    case "MID":
                        return this.Mid();

                    case "NORMALIZESTRING":
                        return this.NormalizeString();

                    case "NOT":
                        return this.Not();

                    case "NULL":
                        return this.Null();

                    case "OR":
                        return this.Or();

                    case "PARAMETERSCONTAIN":
                        return this.ParametersContain();

                    case "PARAMETERSLIST":
                        return this.ParametersList();

                    case "PARAMETERSTABLE":
                        return this.ParametersTable();

                    case "PARAMETERVALUE":
                        return this.ParameterValue();

                    case "PARAMETERVALUEADDED":
                        return this.ParameterValueAdded();

                    case "PARAMETERVALUEREMOVED":
                        return this.ParameterValueRemoved();

                    case "PROPERCASE":
                        return this.ProperCase();

                    case "SORTLIST":
                        return this.SortList();

                    case "SPLITSTRING":
                        return this.SplitString();

                    case "SUBTRACT":
                        return this.Subtract();

                    case "RANDOMNUM":
                        return this.RandomNum();

                    case "REGEXMATCH":
                        return this.RegexMatch();

                    case "REGEXREPLACE":
                        return this.RegexReplace();

                    case "REMOVEDUPLICATES":
                        return this.RemoveDuplicates();

                    case "REMOVEVALUES":
                        return this.RemoveValues();

                    case "REPLACESTRING":
                        return this.ReplaceString();

                    case "RIGHT":
                        return this.Right();

                    case "RIGHTPAD":
                        return this.RightPad();

                    case "RTRIM":
                        return this.RTrim();

                    case "TITLECASE":
                        return this.TitleCase();

                    case "TRIM":
                        return this.Trim();

                    case "UPPERCASE":
                        return this.UpperCase();

                    case "VALUEBYINDEX":
                        return this.ValueByIndex();

                    case "VALUEBYKEY":
                        return this.ValueByKey();

                    case "VALUETYPE":
                        return this.ValueType();

                    case "WORD":
                        return this.Word();

                    case "WRAPXPATHFILTER":
                        return this.WrapXPathFilter();

                    default:
                        throw new NotSupportedFunctionException();
                }
            }
            catch (NotSupportedFunctionException ex)
            {
                throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRunUnsupportedFunctionError, new NotSupportedFunctionException(Messages.ExpressionFunction_UnsupportedFunctionError, ex, this.function, this.mode));
            }
            catch (InvalidFunctionFormatException ex)
            {
                throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRunFunctionSyntaxValidationError, new InvalidFunctionFormatException(Messages.ExpressionFunction_FunctionSyntaxValidationError, ex, this.function, this.mode));
            }
            catch (Exception ex)
            {
                // dump all function parameters in an error log entry
                string paramString = string.Empty;
                if (this.mode != EvaluationMode.Parse)
                {
                    try
                    {
                        if (this.parameters != null && this.parameters.Count > 0)
                        {
                            for (var i = 0; i < this.parameters.Count; ++i)
                            {
                                var parameter = this.parameters[i];
                                paramString += "'";
                                if (parameter != null)
                                {
                                    try
                                    {
                                        paramString += Convert.ToString(parameter, CultureInfo.InvariantCulture) + "',";
                                    }
                                    catch (Exception)
                                    {
                                        // let it print without the ending single quote as an indication of the conversion failure
                                    }
                                }
                                else
                                {
                                    paramString += "',";
                                }
                            }
                        }

                        paramString = "(" + paramString.TrimEnd(',') + ")";
                    }
                    catch (Exception)
                    {
                        // do nothing - silenty ignore
                    }
                }

                throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRunUnknownFunctionExecutionError, new InvalidFunctionOperationException(Messages.ExpressionFunction_UnknownFunctionExecutionError, this.mode.ToString().ToLowerInvariant(), ex, this.function + paramString, ex.Message));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRun, "Function: '{0}'. Evaluation Mode: '{1}'.", this.function, this.mode);
            }
        }

        /// <summary>
        /// Deserializes the XML document into an object of specified type.
        /// </summary>
        /// <typeparam name="T">Type of the object to be deserialized</typeparam>
        /// <param name="xml">Serialized representation of the specified type</param>
        /// <returns>The Object being deserialized.</returns>
        internal static T DeserializeObject<T>(string xml)
        where T : class
        {
            T t;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (TextReader stringReader = new StringReader(xml))
            {
                using (XmlTextReader xmlTextReader = new XmlTextReader(stringReader))
                {
                    xmlTextReader.ProhibitDtd = true;
                    t = xmlSerializer.Deserialize(xmlTextReader) as T;
                }
            }

            return t;
        }

        /// <summary>
        /// Verifies value is of the specified type.
        /// If the value is of the specified type,
        /// or if a validation is being performed and the type is Function ParameterType
        /// to indicate that it is a lookup pending resolution, the type is valid
        /// We also allow the value to be null to accommodate null attribute transfer
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns>true if the value is of the specified type. Otherwise false.</returns>
        private bool VerifyType(object value, Type type)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionVerifyType, "Value: '{0}'. Type: '{1}'. Evaluation Mode: '{2}'.", value, type, this.mode);

            bool verified = false;
            try
            {
                verified = value == null || value.GetType() == type
                    || (type == typeof(int) && value.GetType() == typeof(long)) || (type == typeof(long) && value.GetType() == typeof(int))
                    || (this.mode == EvaluationMode.Parse && value.GetType() == typeof(ParameterType));

                return verified;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionVerifyType, "Value: '{0}'. Type: '{1}'. Verified: '{2}'. Evaluation Mode: '{3}'.", value, type, verified, this.mode);
            }
        }

        #region Boolean Functions

        /// <summary>
        /// This function is used to compare two DateTime values, returns true if the first DateTime occurs after the second
        /// Function Syntax: After(value:DateTime, comparison:DateTime)
        /// </summary>
        /// <returns>true if the first DateTime occurs after the second. Otherwise false.</returns>
        private bool After()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionAfter, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAfterInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(DateTime);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAfterInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAfterInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || this.parameters[1] == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = (DateTime)this.parameters[0] > (DateTime)this.parameters[1];
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionAfter, "After('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionAfter, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to logically and two Boolean conditions.
        /// Function Syntax: And(condition:boolean, condition:boolean)
        /// </summary>
        /// <returns>true if both conditions resolve to true. Otherwise false.</returns>
        private bool And()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionAnd, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAndInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(bool);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAndInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAndInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || this.parameters[1] == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = (bool)this.parameters[0] && (bool)this.parameters[1];
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionAnd, "And('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionAnd, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to compare two DateTime values, returns true if the first DateTime occurs before the second.
        /// Function Syntax: Before(value:DateTime, comparison:DateTime)
        /// </summary>
        /// <returns>true if the first DateTime occurs before the second. Otherwise false.</returns>
        private bool Before()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionBefore, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBeforeInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(DateTime);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBeforeInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBeforeInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || this.parameters[1] == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = (DateTime)this.parameters[0] < (DateTime)this.parameters[1];
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionBefore, "Before('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionBefore, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine if the specified list contains the specified value.
        /// Function Syntax: Contains(values:List, value:object)
        /// </summary>
        /// <returns>true if the list contains given value. Otherwise false.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.String.Equals(System.String,System.StringComparison)", Justification = "Reviewed and retained InvariantCultureIgnoreCase")]
        private bool Contains()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionContains, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionContainsInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || this.parameters[1] == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = false;

                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            foreach (object o in (IEnumerable)this.parameters[0])
                            {
                                if (o is string && this.parameters[1] is string)
                                {
                                    if (o.ToString().Equals(this.parameters[1].ToString(), StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                                else if (o.Equals(this.parameters[1]))
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            result = this.Eq();
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionContains, "Contains('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionContains, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine the equivalence between two objects or values, returns true if they are equal.
        /// Function Syntax: Eq(value:object, value:object, boolean:CompareCase)
        /// </summary>
        /// <returns>true if the two objects or values are equal. Otherwise false.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.String.Equals(System.String,System.StringComparison)", Justification = "Reviewed and retained InvariantCultureIgnoreCase")]
        private bool Eq()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionEqual, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2 && this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionEqualInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError2, this.function, 2, 3, this.parameters.Count));
                }

                if (this.parameters.Count == 3)
                {
                    Type parameterType = typeof(bool);
                    object parameter = this.parameters[2];
                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionEqualInvalidThirdFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidThirdFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                    }
                }

                try
                {
                    bool result;

                    if (this.mode != EvaluationMode.Parse)
                    {
                        if (this.parameters[0] == null || this.parameters[1] == null)
                        {
                            result = this.parameters[0] == null && this.parameters[1] == null;
                        }
                        else if (this.VerifyType(this.parameters[0], typeof(string)) && this.VerifyType(this.parameters[1], typeof(string)))
                        {
                            if (this.parameters.Count == 2 || !(bool)this.parameters[2])
                            {
                                result = this.parameters[0].ToString().Equals(this.parameters[1].ToString(), StringComparison.InvariantCultureIgnoreCase);
                            }
                            else
                            {
                                result = this.parameters[0].ToString().Equals(this.parameters[1].ToString());
                            }
                        }
                        else if (this.VerifyType(this.parameters[0], typeof(long)) && this.VerifyType(this.parameters[1], typeof(long)))
                        {
                            result = Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture) == Convert.ToInt64(this.parameters[1], CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            result = this.parameters[0].Equals(this.parameters[1]);
                        }

                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionEqual, "Eq('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                    }
                    else
                    {
                        result = false;
                    }

                    return result;
                }
                catch (WorkflowActivityLibraryException ex)
                {
                    Logger.Instance.WriteInfo(EventIdentifier.ExpressionFunctionEqualException, "Eq('{0}', '{1}') evaluated false due to error: '{2}'.", this.parameters[0], this.parameters[1], ex);
                    return false;
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionEqual, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine if the first integer value is greater than the second.
        /// Function Syntax: GreaterThan(value:integer, comparison:integer)
        /// </summary>
        /// <returns>true if the first integer value is greater than the second. Otherwise false.</returns>
        private bool GreaterThan()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionGreaterThan, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionGreaterThanInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionGreaterThanInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionGreaterThanInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || this.parameters[1] == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture) > Convert.ToInt64(this.parameters[1], CultureInfo.InvariantCulture);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionGreaterThan, "GreaterThan('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionGreaterThan, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine if the object specified is non-null.
        /// Function Syntax: IsPresent(value:object)
        /// </summary>
        /// <returns>true if the object referenced in not null. Otherwise false.</returns>
        private bool IsPresent()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionIsPresent, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionIsPresentInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                bool result = this.parameters[0] != null;

                if (this.mode != EvaluationMode.Parse)
                {
                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionIsPresent, "IsPresent('{0}') evaluated '{1}'.", this.parameters[0], result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionIsPresent, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine if the first integer value is smaller than the second.
        /// Function Syntax: LessThan(value:integer, comparison:integer)
        /// </summary>
        /// <returns>true if the first integer value is smaller than the second. Otherwise false.</returns>
        private bool LessThan()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionLessThan, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLessThanInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLessThanInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLessThanInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || this.parameters[1] == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture) < Convert.ToInt64(this.parameters[1], CultureInfo.InvariantCulture);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLessThan, "LessThan('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionLessThan, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function negates the value of Boolean condition.
        /// Function Syntax: Not(condition:boolean)
        /// </summary>
        /// <returns>true if the specified values if false. Otherwise true.</returns>
        private bool Not()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionNot, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionNotInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(bool);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionNotInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = true;
                    }
                    else
                    {
                        result = !(bool)this.parameters[0];
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionNot, "Not('{0}') evaluated '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionNot, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to logically or two Boolean conditions.
        /// Function Syntax: Or(condition:boolean, condition:boolean)
        /// </summary>
        /// <returns>true if both the specified conditions are true. Otherwise false.</returns>
        private bool Or()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionOr, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionOrInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(bool);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionOrInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionOrInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || this.parameters[1] == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = (bool)this.parameters[0] || (bool)this.parameters[1];
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionOr, "Or('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionOr, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine if the request parameters of the current request contain the specified attribute name.
        /// This function is useful to verify a request.
        /// Function Syntax: ParametersContain(RequestParameter:List<RequestParameter>, attribute:string)
        /// </summary>
        /// <returns>
        /// true if the request parameters contain the specified attribute name. Otherwise false.
        /// </returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml", Justification = "Reviewed.")]
        private bool ParametersContain()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionParametersContain, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParametersContainInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParametersContainNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(List<string>);
                Type parameterType2 = typeof(string);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParametersContainInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                bool result = false;

                if (this.mode != EvaluationMode.Parse)
                {
                    List<string> requestParameters = new List<string>();

                    if (this.parameters[0] is string)
                    {
                        requestParameters.Add(this.parameters[0].ToString());
                    }
                    else
                    {
                        requestParameters = (List<string>)this.parameters[0];
                    }

                    if (requestParameters.Count != 0)
                    {
                        foreach (string s in requestParameters)
                        {
                            // Consider: ParametersContain("[//Request/RequestParameter]", "AccountName")
                            if (s.Trim().StartsWith("<RequestParameter", StringComparison.OrdinalIgnoreCase))
                            {
                                RequestParameter requestParameter = DeserializeObject<RequestParameter>(s);

                                // check for UpdateRequestParameter before CreateRequestParameter as UpdateRequestParameter is CreateRequestParameter
                                // though it should not really matter for our purpose here
                                if (requestParameter is UpdateRequestParameter)
                                {
                                    if (((UpdateRequestParameter)requestParameter).PropertyName.Equals(this.parameters[1]))
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                                else if (requestParameter is CreateRequestParameter)
                                {
                                    if (((CreateRequestParameter)requestParameter).PropertyName.Equals(this.parameters[1]))
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // Consider: ParametersContain("[//RequestParameter/AllChangesAuthorizationTable]", "AccountName")
                                // We have now deprecated this usage in favour of [//Request/RequestParameter] but process the code for backward compatibility.
                                Logger.Instance.WriteWarning(EventIdentifier.ExpressionFunctionParametersContainWarning, Messages.ExpressionFunction_ParametersContainDeprecatedParameterWarning, LookupParameter.RequestParameter, LookupParameter.Request);

                                // AllChangesAuthorizationTable and AllChangesActionTable are not well-formed XML documents.
                                try
                                {
                                    string xpath = "//td[@class='AttributeStyle' and text() ='" + this.parameters[1] + "']/parent::node()/td[@class='NewValueStyle']";
                                    XElement element = XElement.Parse(this.parameters[0].ToString()).XPathSelectElement(xpath);
                                    result = element != null;
                                }
                                catch (XmlException e)
                                {
                                    Logger.Instance.WriteWarning(EventIdentifier.ExpressionFunctionParametersContainWarning, "ParametersContain function for attribute '{0}' threw exception while parsing xml. '{1}'.", this.parameters[1], e);

                                    // fallback to simple string search
                                    result = this.parameters[0].ToString().Contains(">" + this.parameters[1] + "<");
                                }
                            }
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionParametersContain, "ParametersContain function for attribute '{0}' evaluated '{1}'.", this.parameters[1], result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionParametersContain, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return the list of attribute names in the request parameters of an arbitrary request.
        /// Function Syntax: ParametersList(RequestParameter:List<RequestParameter>)
        /// </summary>
        /// <returns>
        /// List of the attribute names in the request parameters.
        /// </returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml", Justification = "Reviewed.")]
        private List<string> ParametersList()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionParametersList, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParametersListInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(List<string>);
                Type parameterType2 = typeof(string);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParametersListInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                List<string> result = new List<string>();

                if (this.mode != EvaluationMode.Parse)
                {
                    List<string> requestParameters = new List<string>();

                    if (this.parameters[0] is string)
                    {
                        requestParameters.Add(this.parameters[0].ToString());
                    }
                    else
                    {
                        requestParameters = (List<string>)this.parameters[0];
                    }

                    if (requestParameters.Count != 0)
                    {
                        foreach (string s in requestParameters)
                        {
                            RequestParameter requestParameter = DeserializeObject<RequestParameter>(s);
                            string attributeName = null;

                            // check for UpdateRequestParameter before CreateRequestParameter as UpdateRequestParameter is CreateRequestParameter
                            // though it should not really matter for our purpose here
                            if (requestParameter is UpdateRequestParameter)
                            {
                                attributeName = ((UpdateRequestParameter)requestParameter).PropertyName;
                            }
                            else if (requestParameter is CreateRequestParameter)
                            {
                                attributeName = ((CreateRequestParameter)requestParameter).PropertyName;
                            }

                            if (!string.IsNullOrEmpty(attributeName))
                            {
                                result.Add(attributeName);
                            }
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionParametersList, "ParametersList function returned '{0}'.", string.Join(";", result.ToArray()));
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionParametersList, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to read the value of a request parameter of an arbitrary request.
        /// When the request is for an update of a multi-valued attribute, ParameterValueAdded() and ParameterValueRemoved() must be used.
        /// Function Syntax: ParameterValue(RequestParameter:List<RequestParameter>, attribute:string)
        /// </summary>
        /// <returns>
        /// The value of the attribute in the request parameters.
        /// </returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml", Justification = "Reviewed.")]
        private object ParameterValue()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionParameterValue, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(List<string>);
                Type parameterType2 = typeof(string);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    List<string> requestParameters = new List<string>();
                    List<object> valueList = new List<object>();

                    if (this.parameters[0] is string)
                    {
                        requestParameters.Add(this.parameters[0].ToString());
                    }
                    else
                    {
                        requestParameters = (List<string>)this.parameters[0];
                    }

                    if (requestParameters.Count != 0)
                    {
                        foreach (string s in requestParameters)
                        {
                            RequestParameter requestParameter = DeserializeObject<RequestParameter>(s);

                            // check for UpdateRequestParameter before CreateRequestParameter as UpdateRequestParameter is CreateRequestParameter
                            // though it should not really matter for our purpose here
                            if (requestParameter is UpdateRequestParameter)
                            {
                                UpdateRequestParameter updateRequestParameter = (UpdateRequestParameter)requestParameter;
                                if (updateRequestParameter.PropertyName.Equals(this.parameters[1]))
                                {
                                    if (updateRequestParameter.Mode == UpdateMode.Modify)
                                    {
                                        if (updateRequestParameter.Value != null)
                                        {
                                            valueList.Add(updateRequestParameter.Value);
                                        }
                                    }
                                    else
                                    {
                                        Logger.Instance.WriteError(EventIdentifier.ExpressionFunctionParameterValueInvalidFunctionUseError, Messages.ExpressionFunction_InvalidParameterValueFunctionUseError, this.parameters[1]);
                                    }
                                }
                            }
                            else if (requestParameter is CreateRequestParameter)
                            {
                                CreateRequestParameter createRequestParameter = (CreateRequestParameter)requestParameter;
                                if (createRequestParameter.PropertyName.Equals(this.parameters[1]))
                                {
                                    valueList.Add(createRequestParameter.Value);
                                }
                            }
                        }
                    }

                    if (valueList.Count == 1)
                    {
                        result = valueList[0];
                    }
                    else if (valueList.Count != 0)
                    {
                        result = valueList;
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionParameterValue, "ParameterValue() for attribute '{0}' returned '{1}'.", this.parameters[1], result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionParameterValue, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to read the added values of a multi-valued request parameter of an arbitrary request.
        /// Function Syntax: ParameterValueAdded(RequestParameter:List<RequestParameter>, attribute:string)
        /// </summary>
        /// <returns>
        /// The added value of the multi-valued attribute in the request parameters.
        /// </returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml", Justification = "Reviewed.")]
        private object ParameterValueAdded()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionParameterValueAdded, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueAddedInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueAddedNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(List<string>);
                Type parameterType2 = typeof(string);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueAddedInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    List<string> requestParameters = new List<string>();
                    List<object> valueList = new List<object>();

                    if (this.parameters[0] is string)
                    {
                        requestParameters.Add(this.parameters[0].ToString());
                    }
                    else
                    {
                        requestParameters = (List<string>)this.parameters[0];
                    }

                    if (requestParameters.Count != 0)
                    {
                        foreach (string s in requestParameters)
                        {
                            RequestParameter requestParameter = DeserializeObject<RequestParameter>(s);

                            // check for UpdateRequestParameter before CreateRequestParameter as UpdateRequestParameter is CreateRequestParameter
                            // though it should not really matter for our purpose here
                            if (requestParameter is UpdateRequestParameter)
                            {
                                UpdateRequestParameter updateRequestParameter = (UpdateRequestParameter)requestParameter;
                                if (updateRequestParameter.PropertyName.Equals(this.parameters[1]))
                                {
                                    if (updateRequestParameter.Mode == UpdateMode.Insert)
                                    {
                                        if (updateRequestParameter.Value != null)
                                        {
                                            valueList.Add(updateRequestParameter.Value);
                                        }
                                    }
                                    else if (updateRequestParameter.Mode == UpdateMode.Modify)
                                    {
                                        Logger.Instance.WriteError(EventIdentifier.ExpressionFunctionParameterValueAddedInvalidFunctionUseError, Messages.ExpressionFunction_InvalidParameterValueAddedFunctionUseError, this.parameters[1]);
                                    }
                                }
                            }
                            else if (requestParameter is CreateRequestParameter)
                            {
                                // ideally, ParameterValue() should be used instead, but we'll ignore the improper use of this function.
                                CreateRequestParameter createRequestParameter = (CreateRequestParameter)requestParameter;
                                if (createRequestParameter.PropertyName.Equals(this.parameters[1]))
                                {
                                    valueList.Add(createRequestParameter.Value);
                                }
                            }
                        }
                    }

                    if (valueList.Count == 1)
                    {
                        result = valueList[0];
                    }
                    else if (valueList.Count != 0)
                    {
                        result = valueList;
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionParameterValueAdded, "ParameterValueAdded() for attribute '{0}' returned '{1}'.", this.parameters[1], result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionParameterValueAdded, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to read the removed values of a multi-valued request parameter of an arbitrary request.
        /// Function Syntax: ParameterValueRemoved(RequestParameter:List<RequestParameter>, attribute:string)
        /// </summary>
        /// <returns>
        /// The removed value of the multi-valued attribute in the request parameters.
        /// </returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml", Justification = "Reviewed.")]
        private object ParameterValueRemoved()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionParameterValueRemoved, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueRemovedInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueRemovedNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(List<string>);
                Type parameterType2 = typeof(string);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParameterValueRemovedInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    List<string> requestParameters = new List<string>();
                    List<object> valueList = new List<object>();

                    if (this.parameters[0] is string)
                    {
                        requestParameters.Add(this.parameters[0].ToString());
                    }
                    else
                    {
                        requestParameters = (List<string>)this.parameters[0];
                    }

                    if (requestParameters.Count != 0)
                    {
                        foreach (string s in requestParameters)
                        {
                            RequestParameter requestParameter = DeserializeObject<RequestParameter>(s);

                            // check for UpdateRequestParameter before CreateRequestParameter as UpdateRequestParameter is CreateRequestParameter
                            // though it should not really matter for our purpose here
                            if (requestParameter is UpdateRequestParameter)
                            {
                                UpdateRequestParameter updateRequestParameter = (UpdateRequestParameter)requestParameter;
                                if (updateRequestParameter.PropertyName.Equals(this.parameters[1]))
                                {
                                    if (updateRequestParameter.Mode == UpdateMode.Remove)
                                    {
                                        if (updateRequestParameter.Value != null)
                                        {
                                            valueList.Add(updateRequestParameter.Value);
                                        }
                                    }
                                    else if (updateRequestParameter.Mode == UpdateMode.Modify)
                                    {
                                        Logger.Instance.WriteError(EventIdentifier.ExpressionFunctionParameterValueRemovedInvalidFunctionUseError, Messages.ExpressionFunction_InvalidParameterValueRemovedFunctionUseError, this.parameters[1]);
                                    }
                                }
                            }
                        }
                    }

                    if (valueList.Count == 1)
                    {
                        result = valueList[0];
                    }
                    else if (valueList.Count != 0)
                    {
                        result = valueList;
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionParameterValueRemoved, "ParameterValueRemoved() for attribute '{0}' returned '{1}'.", this.parameters[1], result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionParameterValueRemoved, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a table of of attribute name, modification type and modified values in the request parameters of an arbitrary request.
        /// Function Syntax: ParametersTable(RequestParameter:[string])
        /// </summary>
        /// <returns>
        /// A table of of attribute name, modification type and modified values in the request parameters.
        /// </returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed. Required.")]
        private string ParametersTable()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionParametersTable, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParametersTableInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(List<string>);
                Type parameterType2 = typeof(string);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionParametersTableInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                StringBuilder result = new StringBuilder();

                if (this.mode != EvaluationMode.Parse)
                {
                    List<string> requestParameters = new List<string>();

                    if (this.parameters[0] is string)
                    {
                        requestParameters.Add(this.parameters[0].ToString());
                    }
                    else
                    {
                        requestParameters = (List<string>)this.parameters[0];
                    }

                    if (requestParameters.Count != 0)
                    {
                        foreach (string s in requestParameters)
                        {
                            RequestParameter requestParameter = DeserializeObject<RequestParameter>(s);

                            // check for UpdateRequestParameter before CreateRequestParameter as UpdateRequestParameter is CreateRequestParameter
                            if (requestParameter is UpdateRequestParameter)
                            {
                                UpdateRequestParameter updateRequestParameter = (UpdateRequestParameter)requestParameter;
                                string parameterValue = updateRequestParameter.Value == null ? null : HttpUtility.HtmlEncode(updateRequestParameter.Value.ToString());
                                result.AppendFormat("<tr><td class=\"AttributeStyle\">{0}</td><td class=\"AttributeModificationStyle\">{1}</td><td class=\"NewValueStyle\">{2}</td></tr>", updateRequestParameter.PropertyName, updateRequestParameter.Mode, parameterValue);
                            }
                            else if (requestParameter is CreateRequestParameter)
                            {
                                CreateRequestParameter createRequestParameter = (CreateRequestParameter)requestParameter;
                                string parameterValue = createRequestParameter.Value == null ? null : HttpUtility.HtmlEncode(createRequestParameter.Value.ToString());
                                result.AppendFormat("<tr><td class=\"AttributeStyle\">{0}</td><td class=\"AttributeModificationStyle\">{1}</td><td class=\"NewValueStyle\">{2}</td></tr>", createRequestParameter.PropertyName, createRequestParameter.Operation, parameterValue);
                            }
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionParametersTable, "ParametersTable() function returned '{0}'.", result);
                }

                return result.ToString();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionParametersTable, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine if the first string matches the regular expression pattern specified in the second string.
        /// Function Syntax: RegexMatch(value:string, pattern:string)
        /// </summary>
        /// <returns>true if the first string matches the regular expression pattern specified in the second string. Otherwise false.</returns>
        private bool RegexMatch()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRegexMatch, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRegexMatchInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRegexMatchNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0] != null && Regex.Match(this.parameters[0].ToString(), this.parameters[1].ToString()).Success;

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRegexMatch, "RegexMatch('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRegexMatch, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        #endregion

        #region Value Functions

        /// <summary>
        /// This function is used to add two integer values.
        /// Function Syntax: Add(value:integer, value:integer)
        /// </summary>
        /// <returns>The result of the addition of the two integer values.</returns>
        private long Add()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionAdd, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAddInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAddInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionAddInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                long result;
                if (this.mode != EvaluationMode.Parse)
                {
                    ////result = (long)this.parameters[0] + (long)this.parameters[1]; // leads to InvalidCastException in one of these is actually NOT of type object (long)
                    result = Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture) + Convert.ToInt64(this.parameters[1], CultureInfo.InvariantCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionAdd, "Add('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionAdd, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to subtract the second integer value from the first.
        /// Function Syntax: Subtract(value:integer, value:integer)
        /// </summary>
        /// <returns>The result of the subtraction of the second integer value from the first.</returns>
        private long Subtract()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionSubtract, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSubtractInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSubtractInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSubtractInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                long result;
                if (this.mode != EvaluationMode.Parse)
                {
                    ////result = (long)this.parameters[0] - (long)this.parameters[1]; // leads to InvalidCastException in one of these is actually NOT of type object (long)
                    result = Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture) - Convert.ToInt64(this.parameters[1], CultureInfo.InvariantCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionSubtract, "Subtract('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionSubtract, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// Concatenates  the multivalued string.
        /// Function Syntax: ConcatenateMultivaluedString(values:List of string, delimiter:string)
        /// </summary>
        /// <returns>The concatenated string.</returns>
        private string ConcatenateMultivaluedString()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConcatenateMultivaluedString, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConcatenateMultivaluedStringInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConcatenateMultivaluedStringNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(List<string>);
                Type parameterType2 = typeof(string);
                parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConcatenateMultivaluedStringInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else if (this.parameters[0] is string)
                    {
                        result = this.parameters[0].ToString();
                    }
                    else if (((List<string>)this.parameters[0]).Count == 0)
                    {
                        result = null;
                    }
                    else
                    {
                        StringBuilder concatenated = new StringBuilder();
                        foreach (string s in (List<string>)this.parameters[0])
                        {
                            concatenated.AppendFormat("{0}{1}", s, this.parameters[1]);
                        }

                        result = concatenated.ToString().Substring(0, concatenated.Length - this.parameters[1].ToString().Length);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConcatenateMultivaluedString, "ConcatenateMultivaluedString() returned '{0}'.", result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConcatenateMultivaluedString, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// Formats the multivalued list as per the specified format.
        /// Function Syntax: FormatMultivaluedString(format:string, value1:list or object [, value2:list or object, ...] )
        /// If more than one value lists are supplied as parameters, they will be treated as if they are of the length equal to that of the largest list with `null` as additional items. 
        /// If one of the parameters supplied is a single / non-list object, e.g. a string, that parameter will be treated as if it is a list of length equal to that of the largest list filled with each item being the same object.
        /// </summary>
        /// <returns>The formatted list.</returns>
        private object FormatMultivaluedList()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionFormatMultivaluedList, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionFormatMultivaluedListInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinCountError, this.function, 2, this.parameters.Count));
                }

                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionFormatMultivaluedListNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                // At least one argument to format string function should not be null
                bool allArgsNull = true;
                for (int i = 1; i < this.parameters.Count; ++i)
                {
                    parameter = this.parameters[i];
                    if (parameter != null)
                    {
                        allArgsNull = false;
                        break;
                    }
                }

                if (allArgsNull)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionFormatMultivaluedListNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError2, this.function, 2));
                }

                object result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    var maxListItemCount = 0;
                    for (int i = 1; i < this.parameters.Count; ++i)
                    {
                        Type paramType = this.parameters[i].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var listItemCount = ((IEnumerable)this.parameters[i]).Cast<object>().ToList().Count;

                            maxListItemCount = listItemCount > maxListItemCount ? listItemCount : maxListItemCount;
                        }
                    }

                    if (maxListItemCount == 0)
                    {
                        // treat all (empty) lists as null and all non-lists as is
                        var args = new object[this.parameters.Count - 1];
                        for (int i = 1; i < this.parameters.Count; ++i)
                        {
                            Type paramType = this.parameters[i].GetType();
                            if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                            {
                                args[i - 1] = null;
                            }
                            else
                            {
                                args[i - 1] = this.parameters[i];
                            }
                        }

                        result = string.Format(CultureInfo.InvariantCulture, this.parameters[0].ToString(), args);
                    }
                    else
                    {
                        result = new List<string>(maxListItemCount);

                        // We'll conceptually stretch all non-list and list paramters to be a list of maxListItem
                        var args = new object[this.parameters.Count - 1];
                        for (int n = 0; n < maxListItemCount; ++n)
                        {
                            for (int i = 1; i < this.parameters.Count; ++i)
                            {
                                Type paramType = this.parameters[i].GetType();
                                if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                                {
                                    List<object> list = ((IEnumerable)this.parameters[i]).Cast<object>().ToList();

                                    args[i - 1] = list.Count > n ? list[n] : null;
                                }
                                else
                                {
                                    args[i - 1] = this.parameters[i];
                                }
                            }

                            ((List<string>)result).Add(string.Format(CultureInfo.InvariantCulture, this.parameters[0].ToString(), args));
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConcatenateMultivaluedString, "FormatMultivaluedList() returned '{0}'.", result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionFormatMultivaluedList, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// Converts a specified value to an equivalent FIM UniqueIdentifier value.
        /// Function Syntax: ConvertToUniqueIdentifier(guid(s):[list or object])
        /// </summary>
        /// <returns>The UniqueIdentifier representation of the value.</returns>
        private object ConvertToUniqueIdentifier()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertToUniqueIdentifier, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToUniqueIdentifierInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(List<Guid>);
                Type parameterType2 = typeof(Guid);
                Type parameterType3 = typeof(byte[]);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2) && !this.VerifyType(parameter, parameterType3))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToUniqueIdentifierInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameterType2.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            result = (from object o in (IEnumerable)this.parameters[0] select new UniqueIdentifier((Guid)o)).ToList();
                        }
                        else
                        {
                            result = this.parameters[0] is byte[] ? new UniqueIdentifier(new Guid((byte[])this.parameters[0])) : new UniqueIdentifier((Guid)this.parameters[0]);
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertToUniqueIdentifier, "ConvertToUniqueIdentifier() returned '{0}'.", result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertToUniqueIdentifier, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine the number of elements in a specified list.
        /// Function Syntax: Count(values:[list or object])
        /// </summary>
        /// <returns>The number of elements in the input list. If the input is not a list, returns 1.</returns>
        private int Count()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionCount, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCountInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                int result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = 0;
                    }
                    else
                    {
                        Type paramType = this.parameters[0].GetType();
                        int count = 1;

                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            count = ((IEnumerable)this.parameters[0]).Cast<object>().Count();
                        }

                        result = count;
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionCount, "Count('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionCount, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to add the value of the specified second TimeSpan string to first DateTime or string value.
        /// Function Syntax: DateTimeAdd(date:DateTime, timespan:string)
        /// </summary>
        /// <returns>A DateTime whose value is the sum of the date and time represented by first parameter and the time interval represented by second parameter.</returns>
        private object DateTimeAdd()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionDateTimeAdd, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeAddInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeAddNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(DateTime);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeAddInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameterType = typeof(string);
                Type parameterType2 = typeof(TimeSpan);
                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeAddInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError3, this.function, parameterType.Name, parameterType2.Name, parameter.GetType().Name));
                }

                object result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (parameter is TimeSpan)
                        {
                            result = ((DateTime)this.parameters[0]).Add((TimeSpan)this.parameters[1]);
                        }
                        else
                        {
                            // do additional parameter validation check during execution as it may be a lookup expression than a design-time value. 
                            parameterType = typeof(TimeSpan);
                            parameter = this.parameters[1];
                            TimeSpan parseTimeSpan;
                            if (!TimeSpan.TryParse(parameter as string, out parseTimeSpan))
                            {
                                throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeAddInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError2, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name, parameter));
                            }

                            result = ((DateTime)this.parameters[0]).Add(TimeSpan.Parse(this.parameters[1].ToString()));
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionDateTimeAdd, "DateTimeAdd('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionDateTimeAdd, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to format the value of the first DateTime parameter in the format specified in the second string parameter.
        /// Function Syntax: DateTimeFormat(date:DateTime, format:string)
        /// </summary>
        /// <returns>The string representation of the first DateTime parameter in the format specified in the second string parameter.</returns>
        private string DateTimeFormat()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionDateTimeFormat, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeFormatInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeFormatNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(DateTime);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeFormatInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                string result;
                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        result = ((DateTime)this.parameters[0]).ToString(this.parameters[1].ToString(), CultureInfo.InvariantCulture);

                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionDateTimeFormat, "DateTimeFormat('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionDateTimeFormat, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert the specified Windows file time to an equivalent UTC time..
        /// Function Syntax: DateTimeFromFileTimeUtc(long:fileTime)
        /// </summary>
        /// <returns>An object that represents the UTC time equivalent of the date and time represented by the fileTime parameter.</returns>
        private object DateTimeFromFileTimeUtc()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionDateTimeFromFileTimeUtc, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeFromFileTimeUtcInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeFromFileTimeUtcInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result;
                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        result = DateTime.FromFileTimeUtc(Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture));

                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionDateTimeFromFileTimeUtc, "DateTimeFromFileTimeUtc('{0}') returned '{1}'.", this.parameters[0], result);
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionDateTimeFromFileTimeUtc, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to get a DateTime object that is set to the current date and time on FIM server, expressed as the Coordinated Universal Time (UTC).
        /// Function Syntax: DateTimeNow()
        /// </summary>
        /// <returns>A DateTime whose value is the current UTC date and time.</returns>
        private DateTime DateTimeNow()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionDateTimeNow, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeNowInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 0, this.parameters.Count));
                }

                DateTime result = DateTime.UtcNow;

                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionDateTimeNow, "DateTimeNow() returned '{0}'.", result);

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionDateTimeNow, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert a date in the Windows file time format.
        /// Function Syntax: DateTimeToFileTimeUtc(date:DateTime)
        /// </summary>
        /// <returns>The value of the specified date expressed as a Windows file time.</returns>
        private object DateTimeToFileTimeUtc()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionDateTimeToFileTimeUtc, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeToFileTimeUtcInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(DateTime);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeToFileTimeUtcInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result;
                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        result = ((DateTime)this.parameters[0]).ToFileTimeUtc();

                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionDateTimeToFileTimeUtc, "DateTimeToFileTimeUtc('{0}') returned '{1}'.", this.parameters[0], result);
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionDateTimeToFileTimeUtc, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to get the timespan between the two dates.
        /// Function Syntax: DateTimeSubtract(date:DateTimeEndDate, date:DateTimeStartDate)
        /// </summary>
        /// <returns>The value of the specified date expressed as a Windows file time.</returns>
        private object DateTimeSubtract()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionDateTimeSubtract, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeSubtractInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                if (this.parameters[0] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeSubtractNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeSubtractNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                Type parameterType = typeof(DateTime);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeSubtractInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionDateTimeSubtractInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result;
                if (this.mode != EvaluationMode.Parse)
                {
                    result = ((DateTime)this.parameters[0]).Subtract((DateTime)this.parameters[1]);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionDateTimeSubtract, "DateTimeSubtract('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionDateTimeSubtract, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to retrieve the first member of the specified List.
        /// Function Syntax: First(values:[list or object])
        /// </summary>
        /// <returns>The first member of the input list. If the input is not a list, returns the input.</returns>
        private object First()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionFirst, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                // First(values:[list or object])
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionFirstInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                object result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        result = null;
                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            foreach (object o in (IEnumerable)this.parameters[0])
                            {
                                result = o;
                                break;
                            }
                        }
                        else
                        {
                            result = this.parameters[0];
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionFirst, "First() returned '{0}'.", result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionFirst, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to generates a random password of specified length.
        /// Function Syntax: GenerateRandomPassword(length:integer)
        /// </summary>
        /// <returns>A randomly generated string of specified length.</returns>
        private string GenerateRandomPassword()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionGenerateRandomPassword, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionGenerateRandomPasswordInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                if (this.parameters[0] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionGenerateRandomPasswordNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionGenerateRandomPasswordInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    // Instantiate a new class of the password generator class
                    // and return a randomly generated complex password of the specified length
                    int length = Convert.ToInt32(this.parameters[0], CultureInfo.InvariantCulture);

                    result = (new PasswordGenerator()).GenerateRandomPassword(length);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionGenerateRandomPassword, "GenerateRandomPassword('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionGenerateRandomPassword, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This Inline if function is used to return the second or third parameter based on the evaluation of the first parameter.
        /// Function Syntax: IIF(condition:boolean, trueReturn:object, falseReturn:object)
        /// </summary>
        /// <returns>The second input parameter if the first input condition is true. Otherwise the third input parameter.</returns>
        private object IIF()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionIIF, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionIIFInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 3, this.parameters.Count));
                }

                Type parameterType = typeof(bool);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionIIFInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                object result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null || !(bool)this.parameters[0])
                    {
                        result = this.parameters[2];
                    }
                    else
                    {
                        result = this.parameters[1];
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionIIF, "IIF('{0}', '{1}', '{2}') returned '{3}'.", this.parameters[0], this.parameters[1], this.parameters[2], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionIIF, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to create a new InsertedValues collection which contains all values from the specified list.
        /// This InsertedValues collection will be used by the activity so that it can generate update request parameters accordingly.
        /// Function Syntax: InsertValues(value(s):[list or object])
        /// </summary>
        /// <returns>A new InsertedValues object which contains all values from the input list. If the input is not a list, the value of the input object is added.</returns>
        private InsertedValuesCollection InsertValues()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionInsertValues, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionInsertValuesInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                InsertedValuesCollection result = new InsertedValuesCollection();

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] != null)
                    {
                        // Build a new InsertedValues collection which contains all values to be added
                        // This collection type will be caught by the activity so that it can generate update request parameters accordingly
                        // If a List<> was passed to the function, add each value
                        // For all other data types, add the value directly
                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            result.AddRange(((IEnumerable)this.parameters[0]).Cast<object>());

                            Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionInsertValues, "InsertValues() inserted {0} values.", result.Count);
                        }
                        else
                        {
                            result.Add(this.parameters[0]);

                            Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionInsertValues, "InsertValues() inserted value '{0}'.", this.parameters[0]);
                        }
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionInsertValues, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to retrieve the last member of the specified List.
        /// Function Syntax: Last(values:[list or object])
        /// </summary>
        /// <returns>The last member of the input list. If the input is not a list, returns the input.</returns>
        private object Last()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionLast, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLastInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                object result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            object last = null;
                            foreach (object o in (IEnumerable)this.parameters[0])
                            {
                                last = o;
                            }

                            result = last;
                        }
                        else
                        {
                            result = this.parameters[0];
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLast, "Last() returned '{0}'.", result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionLast, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a string formed with the leading characters of the string value specified in the first parameter and the length specified in the second parameter.
        /// Function Syntax: Left(value:string, length:integer)
        /// </summary>
        /// <returns>The leading substring of specified length. If the specified length is greater than the input string, the entire input string is returned.</returns>
        private string Left()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionLeft, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    int length = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);
                    result = this.parameters[0] == null ? null : this.parameters[0].ToString().PadRight(length).Substring(0, length).Trim();

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLeft, "Left('{0}', '{1}') returned {2}.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionLeft, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a string with characters Right-aligned, padding with the specified Unicode character or spaces on the left for a specified total length.
        /// Function Syntax: LeftPad(value:string, length:integer, padding:char)
        /// </summary>
        /// <returns>A new string that is equivalent to string specified in the first input parameter, but right-aligned and padded on the left with as many padding characters as needed to create the specified length.</returns>
        private string LeftPad()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionLeftPad, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftPadInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 3, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftPadNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftPadInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[2];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftPadNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 3));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    int length = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);

                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        // do additional parameter validation check during execution as it may be a lookup expression than a design-time value. 
                        parameter = this.parameters[2];
                        parameterType = typeof(char);
                        if (parameter.ToString().ToCharArray().Length != 1)
                        {
                            throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLeftPadInvalidThirdFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidThirdFunctionParameterTypeError2, this.function, parameterType.Name, parameter.GetType().Name, parameter));
                        }

                        result = this.parameters[0].ToString().PadLeft(length, this.parameters[2].ToString().ToCharArray()[0]);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLeftPad, "LeftPad('{0}', '{1}', '{2}') returned {3}.", this.parameters[0], this.parameters[1], this.parameters[2], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionLeftPad, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine the length of the specified input string.
        /// Function Syntax: Length(value:string)
        /// </summary>
        /// <returns>The length of the input string.</returns>
        private int Length()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionLength, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLengthInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                int result;
                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0] == null ? 0 : this.parameters[0].ToString().Length;

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLength, "Length('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionLength, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert all characters in a string to the lower case based on current culture.
        /// Function Syntax: LowerCase(value:string)
        /// </summary>
        /// <returns>A copy of the input string converted to lowercase using the casing rules of the invariant culture.</returns>
        private string LowerCase()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionLowerCase, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLowerCaseInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                string result;
                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0] == null ? null : this.parameters[0].ToString().ToLower(CultureInfo.CurrentCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLowerCase, "LowerCase('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionLowerCase, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to remove leading white spaces or specified characters from a string.
        /// Function Syntax: LTrim(value:string, [trimChars:string])
        /// </summary>
        /// <returns>A copy of the input string with all leading white spaces or specified characters removed.</returns>
        private string LTrim()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionLTrim, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 1 || this.parameters.Count > 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionLTrimInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinMaxCountError, this.function, 1, 2, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    char[] trimChars = this.parameters.Count == 2 && this.parameters[1] != null ? this.parameters[1].ToString().ToCharArray() : null;
                    result = this.parameters[0] == null ? null : this.parameters[0].ToString().TrimStart(trimChars);

                    if (this.parameters.Count == 1)
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLTrim, "LTrim('{0}') returned '{1}'.", this.parameters[0], result);
                    }
                    else
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionLTrim, "LTrim('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionLTrim, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a specified number of characters from a specified position in a string.
        /// Function Syntax: Mid(value:string, start:integer, length:integer)
        /// </summary>
        /// <returns>A copy of the input string with the specified number of characters from the specified position in the input string.</returns>
        private string Mid()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionMid, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionMidInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 3, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionMidNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionMidInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[2];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionMidNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionMidInvalidThirdFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidThirdFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    int startIndex = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);
                    int length = Convert.ToInt32(this.parameters[2], CultureInfo.InvariantCulture);

                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else if (this.parameters[0].ToString().Length <= startIndex)
                    {
                        result = null;
                    }
                    else if (startIndex + length > this.parameters[0].ToString().Length)
                    {
                        result = this.parameters[0].ToString().Substring(startIndex);
                    }
                    else
                    {
                        result = this.parameters[0].ToString().Substring(startIndex, length);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionMid, "Mid('{0}', '{1}', '{2}') returned '{3}'.", this.parameters[0], this.parameters[1], this.parameters[2], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionMid, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to normalize the first string, first by replacing the character substitutions specfied in the second string 
        /// and then removing all diacritics using the .NET string normalization function.
        /// The substitution string is ':' and '|' separated in the form oldString1:newString1|oldString2:newString2...
        /// Function Syntax: NormalizeString(value:string, substitutions:string)
        /// e.g. NormalizeString("ﬀßæÁÂÃÄÅÇÈÉàáâãäåèéêëìíîïòóôõ", "Å:AA|Ä:AE|Ö:OE|å:aa|ä:ae|ö:oe|ß:ss|æ:ae")
        /// </summary>
        /// <returns>true if the first string matches the regular expression pattern specified in the second string. Otherwise false.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        private string NormalizeString()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionNormalizeString, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 1 || this.parameters.Count > 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionNormalizeStringInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinMaxCountError, this.function, 1, 2, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    string input = this.parameters[0] as string;
                    string substitutions = this.parameters.Count > 1 ? this.parameters[1] as string : string.Empty;

                    if (string.IsNullOrEmpty(input))
                    {
                        result = input;
                        return result;
                    }

                    // First do substitutions
                    if (!string.IsNullOrEmpty(substitutions))
                    {
                        string[] substituionPairs = substitutions.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                        // Changed StringSplitOptions for substituionPairs from RemoveEmptyEntries to None so that soft and hard signs such as"Ь" can be simply dropped
                        foreach (string[] substitution in substituionPairs.Select(substituionPair => substituionPair.Split(new string[] { ":" }, StringSplitOptions.None)).Where(substitution => substitution.Length == 2))
                        {
                            input = input.Replace(substitution[0].Trim(), substitution[1].Trim());
                            input = input.Replace(substitution[0], substitution[1].Trim());
                        }
                    }

                    // Now normalize
                    input = input.Normalize(NormalizationForm.FormKD);

                    StringBuilder normalizedString = new StringBuilder();

                    foreach (char ch in input)
                    {
                        var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(ch);
                        if (unicodeCategory == UnicodeCategory.NonSpacingMark)
                        {
                            continue;
                        }

                        normalizedString.Append(ch);

                        if (ch > 128)
                        {
                            if (this.parameters.Count == 1)
                            {
                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionNormalizeString, "NormalizeString('{0}'). Character '{1}' is not a 7-bit ASCII Character.", this.parameters[0], ch);
                            }
                            else
                            {
                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionNormalizeString, "NormalizeString('{0}', '{1}'). Character '{2}' is not a 7-bit ASCII Character.", this.parameters[0], this.parameters[1], ch);
                            }
                        }
                    }

                    result = normalizedString.ToString();

                    if (this.parameters.Count == 1)
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionNormalizeString, "NormalizeString('{0}') evaluated '{1}'.", this.parameters[0], result);
                    }
                    else
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionNormalizeString, "NormalizeString('{0}', '{1}') evaluated '{2}'.", this.parameters[0], this.parameters[1], result);
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionNormalizeString, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a null object.
        /// It is used in conjunction with Update Resources whenever you wish to delete an existing value when the Allow Nulls option is selected.
        /// Function Syntax: Null()
        /// </summary>
        /// <returns>A Null object.</returns>
        private object Null()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionNull, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionNullInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 0, this.parameters.Count));
                }

                return null;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionNull, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert the first character of each space-delimited word in a string to upper case and all other characters are converted to lower case.
        /// Function Syntax: ProperCase(value:string)
        /// </summary>
        /// <returns>Returns a copy of the input string with the first character of each space-delimited word in a string in upper case and all other characters in lower case.</returns>
        private string ProperCase()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionProperCase, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionProperCaseInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (string.IsNullOrEmpty(this.parameters[0] as string))
                    {
                        result = this.parameters[0] as string;
                    }
                    else
                    {
                        // FIM function does not behave as ToTileCase
                        ////result = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(this.parameters[0].ToString());

                        // FIM implementation 
                        string input = this.parameters[0].ToString();
                        bool lastCharWhiteSpace = true;
                        bool lastCharUpperCase = false;
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < input.Length; i++)
                        {
                            if (!char.IsWhiteSpace(input[i]))
                            {
                                if (!char.IsLetter(input[i]) || (!lastCharWhiteSpace && lastCharUpperCase))
                                {
                                    stringBuilder.Append(char.ToLower(input[i], CultureInfo.CurrentCulture));
                                }
                                else
                                {
                                    stringBuilder.Append(char.ToUpper(input[i], CultureInfo.CurrentCulture));
                                    lastCharUpperCase = true;
                                }

                                lastCharWhiteSpace = false;
                            }
                            else
                            {
                                stringBuilder.Append(input[i]);
                                lastCharWhiteSpace = true;
                                lastCharUpperCase = false;
                            }
                        }

                        result = stringBuilder.ToString();
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionProperCase, "ProperCase('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionProperCase, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a random number within a specified interval.
        /// Function Syntax: RandomNum(start:integer, end:integer)
        /// </summary>
        /// <returns>A random number within a specified interval.</returns>
        private int RandomNum()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRandomNumber, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRandomNumberInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRandomNumberNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRandomNumberInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRandomNumberNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRandomNumberInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                int result;

                if (this.mode != EvaluationMode.Parse)
                {
                    int minValue = Convert.ToInt32(this.parameters[0], CultureInfo.InvariantCulture);
                    int maxValue = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);

                    Random random = new Random();
                    result = random.Next(minValue, maxValue);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRandomNumber, "RandomNum('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRandomNumber, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to replace all occurrences of a regular expression pattern in a string to another string.
        /// Function Syntax: RegexReplace(value:string, pattern:string, with:string)
        /// </summary>
        /// <returns>A copy of the input string with all occurrences of the specified second input regular expression pattern in the first input string replaced with the third input string.</returns>
        private string RegexReplace()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRegexReplace, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRegexReplaceInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 3, this.parameters.Count));
                }

                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRegexReplaceNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                parameter = this.parameters[2];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRegexReplaceNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 3));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0] == null ? null : Regex.Replace(this.parameters[0].ToString(), this.parameters[1].ToString(), this.parameters[2].ToString());

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRegexReplace, "RegexReplace('{0}', '{1}', '{2}') returned '{3}'.", this.parameters[0], this.parameters[1], this.parameters[2], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRegexReplace, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to a create new RemovedValues collection which contains all values removed from the specified list.
        /// This RemovedValues collection will be used by the activity so that it can generate update request parameters accordingly.
        /// Function Syntax: RemoveValues(value(s):[list or object])
        /// </summary>
        /// <returns>A new RemovedValues object which contains all values removed from the input list. If the input is not a list, the value of the input object is removed.</returns>
        private RemovedValuesCollection RemoveValues()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRemoveValues, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRemoveValuesInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                RemovedValuesCollection result = new RemovedValuesCollection();

                if (this.mode != EvaluationMode.Parse)
                {
                    // Build a new RemovedValues collection which contains all values to be removed
                    // This collection type will be caught by the activity so that it can generate update request parameters accordingly
                    // If a List<> was passed to the function, add each value
                    // For all other data types, add the value directly
                    if (this.parameters[0] != null)
                    {
                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            result.AddRange(((IEnumerable)this.parameters[0]).Cast<object>());

                            Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRemoveValues, "RemoveValues() removed {0} values.", result.Count);
                        }
                        else
                        {
                            result.Add(this.parameters[0]);

                            Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRemoveValues, "RemoveValues() removed value '{0}'.", this.parameters[0]);
                        }
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRemoveValues, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to replace all occurrences of a string to another string.
        /// Function Syntax: ReplaceString(value:string, replace:string, with:string)
        /// </summary>
        /// <returns>A copy of the input string with all occurrences of a string replaced with another string.</returns>
        private string ReplaceString()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionReplaceString, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionReplaceStringInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 3, this.parameters.Count));
                }

                if (this.parameters[1] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionReplaceStringNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (this.parameters[2] == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionReplaceStringNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 3));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0] == null ? null : this.parameters[0].ToString().Replace(this.parameters[1].ToString(), this.parameters[2].ToString());

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionReplaceString, "ReplaceString('{0}', '{1}', '{2}') returned '{3}'.", this.parameters[0], this.parameters[1], this.parameters[2], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionReplaceString, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a string formed with the trailing characters of the string value specified in the first parameter and the length specified in the second parameter.
        /// Function Syntax: Right(value:string, length:integer)
        /// </summary>
        /// <returns>The trailing substring of specified length. If the specified length is greater than the input string, the entire input string is returned.</returns>
        private string Right()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRight, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    int length = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else if (this.parameters[0].ToString().Length <= length)
                    {
                        result = this.parameters[0].ToString();
                    }
                    else
                    {
                        result = this.parameters[0].ToString().Substring(this.parameters[0].ToString().Length - length, length);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRight, "Right('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRight, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a string with characters left-aligned, padding with the specified Unicode character or spaces on the right for a specified total length.
        /// Function Syntax: RightPad(value:string, length:integer, padding:char)
        /// </summary>
        /// <returns>A new string that is equivalent to string specified in the first input parameter, but left-aligned and padded on the right with as many padding characters as needed to create the specified length.</returns>
        private string RightPad()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRightPad, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightPadInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 3, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightPadNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightPadInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[2];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightPadNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 3));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    int totalWidth = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);

                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        // do additional parameter validation check during execution as it may be a lookup expression than a design-time value. 
                        parameter = this.parameters[2];
                        parameterType = typeof(char);
                        if (parameter.ToString().ToCharArray().Length != 1)
                        {
                            throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRightPadInvalidThirdFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidThirdFunctionParameterTypeError2, this.function, parameterType.Name, parameter.GetType().Name, parameter));
                        }

                        result = this.parameters[0].ToString().PadRight(totalWidth, this.parameters[2].ToString().ToCharArray()[0]);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRightPad, "RightPad('{0}', '{1}', '{2}') returned '{3}'.", this.parameters[0], this.parameters[1], this.parameters[2], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRightPad, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to remove trailing white spaces or specified characters from a string.
        /// Function Syntax: RTrim(value:string, [trimChars:string])
        /// </summary>
        /// <returns>A copy of the input string with all trailing white spaces or specified characters removed.</returns>
        private string RTrim()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRTrim, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 1 || this.parameters.Count > 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRTrimInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinMaxCountError, this.function, 1, 2, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    char[] trimChars = this.parameters.Count == 2 && this.parameters[1] != null ? this.parameters[1].ToString().ToCharArray() : null;
                    result = this.parameters[0] == null ? null : this.parameters[0].ToString().TrimEnd(trimChars);

                    if (this.parameters.Count == 1)
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRTrim, "RTrim('{0}') returned '{1}'.", this.parameters[0], result);
                    }
                    else
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRTrim, "RTrim('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRTrim, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is to convert the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).
        /// Function Syntax: TitleCase(value:string)
        /// </summary>
        /// <returns>Returns a copy of the input string with the first character of each space-delimited word in a string in upper case and all other characters in lower case.</returns>
        private string TitleCase()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionTitleCase, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionTitleCaseInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        result = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(this.parameters[0].ToString());
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionTitleCase, "TitleCase('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionTitleCase, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to remove leading and trailing white spaces or specified characters from a string.
        /// Function Syntax: Trim(value:string, [trimChars:string])
        /// </summary>
        /// <returns>A copy of the input string with all leading and trailing white spaces or specified characters removed.</returns>
        private string Trim()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionTrim, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 1 || this.parameters.Count > 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionTrimInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinMaxCountError, this.function, 1, 2, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    char[] trimChars = this.parameters.Count == 2 && this.parameters[1] != null ? this.parameters[1].ToString().ToCharArray() : null;
                    result = this.parameters[0] == null ? null : this.parameters[0].ToString().Trim(trimChars);

                    if (this.parameters.Count == 1)
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionTrim, "Trim('{0}') returned '{1}'.", this.parameters[0], result);
                    }
                    else
                    {
                        Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionTrim, "Trim('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                    }
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionTrim, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert all characters in a string to the upper case based on current culture.
        /// Function Syntax: UpperCase(value:string)
        /// </summary>
        /// <returns>A copy of the input string converted to uppercase using the casing rules of the invariant culture.</returns>
        private string UpperCase()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionUpperCase, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionUpperCaseInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0] == null ? null : this.parameters[0].ToString().ToUpper(CultureInfo.CurrentCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionUpperCase, "UpperCase('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionUpperCase, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to retrieve the value at the specified index in the input list.
        /// Function Syntax: ValueByIndex(values:[list or object], index:integer)
        /// </summary>
        /// <returns>The object at the specified index in the input list. If the specified index is greater than the count in the list, null is returned.</returns>
        private object ValueByIndex()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionValueByIndex, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueByIndexInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueByIndexNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueByIndexInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                object result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        int index = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);

                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            int i = 0;
                            object value = null;
                            foreach (object o in (IEnumerable)this.parameters[0])
                            {
                                if (i == index)
                                {
                                    value = o;
                                    break;
                                }

                                i += 1;
                            }

                            result = value;
                        }
                        else if (index == 0)
                        {
                            result = this.parameters[0];
                        }
                        else
                        {
                            result = null;
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionValueByIndex, "ValueByIndex('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionValueByIndex, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to retrieve the value at the specified key in the input Dictionary of string/object key/pair.
        /// Function Syntax: ValueByKey(dictionary:[Dictionary], key:string)
        /// </summary>
        /// <returns>The object associated with the specified key. If the specified key does not exist, null is returned.</returns>
        private object ValueByKey()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionValueByKey, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueByKeyInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(string);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueByKeyNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueByKeyInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                object result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        string key = this.parameters[1] as string;

                        parameterType = typeof(Dictionary<string, object>);
                        parameter = this.parameters[0];
                        if (parameter.GetType() == parameterType)
                        {
                            var dictionary = parameter as Dictionary<string, object>;
                            if (dictionary != null && dictionary.ContainsKey(key))
                            {
                                result = dictionary[key];
                            }
                        }
                        else
                        {
                            throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueByKeyInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionValueByKey, "ValueByKey('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionValueByKey, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to determine the type of the supplied input object.
        /// Function Syntax: ValueType(value:object)
        /// </summary>
        /// <returns>The System.Type of the input object.</returns>
        private string ValueType()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionValueType, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueTypeInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionValueTypeNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0].GetType().ToString();

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionValueType, "ValueType('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionValueType, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to return a word contained within a string, based on parameters describing the delimiters to use and the word number to return.
        /// Function Syntax: Word(value:string, index:integer, delimiter:char)
        /// </summary>
        /// <returns>A string containing the word at the specified position. If string contains less than number of words, or string does not contain any words identified by delimiters, a null string is returned.</returns>
        private string Word()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionWord, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionWordInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 3, this.parameters.Count));
                }

                Type parameterType = typeof(int);
                object parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionWordNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionWordInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[2];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionWordNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 3));
                }

                parameterType = typeof(char);
                if (parameter.ToString().ToCharArray().Length != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionWordInvalidThirdFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidThirdFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        string[] split = this.parameters[0].ToString().Split(this.parameters[2].ToString().ToCharArray()[0]);
                        int index = Convert.ToInt32(this.parameters[1], CultureInfo.InvariantCulture);
                        result = index < split.Length ? split[index] : null;
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionWord, "Word('{0}', '{1}', '{2}') returned '{3}'.", this.parameters[0], this.parameters[1], this.parameters[2], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionWord, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to wrap the xpath filter into a FIM filter.
        /// Function Syntax: WrapXPathFilter(filter:string)
        /// </summary>
        /// <returns>The XPath that conforms to FIM filter syntax</returns>
        private string WrapXPathFilter()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionWrapXPathFilter, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionWrapXPathFilterInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = this.parameters[0] == null ? null : string.Format(CultureInfo.InvariantCulture, "<Filter xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" Dialect=\"http://schemas.microsoft.com/2006/11/XPathFilterDialect\" xmlns=\"http://schemas.xmlsoap.org/ws/2004/09/enumeration\">{0}</Filter>", this.parameters[0]);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionWrapXPathFilter, "WrapXPathFilter('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionWrapXPathFilter, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to set a specified bit on a flag to 0.
        /// Function Syntax: BitAnd(mask:integer, value:flag)
        /// </summary>
        /// <returns>A new version of flag with the bits specified in mask set to 0.</returns>
        private long BitAnd()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionBitAnd, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitAndInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitAndNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitAndInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitAndNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitAndInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                long result;
                if (this.mode != EvaluationMode.Parse)
                {
                    result = Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture) & Convert.ToInt64(this.parameters[1], CultureInfo.InvariantCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionBitAnd, "BitAnd('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionBitAnd, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to set each bit to 1 only if the corresponding bit in binary number is 0 and to 0 if it's 1.
        /// Function Syntax: Not(value:flag)
        /// </summary>
        /// <returns>A new version of flag with the bits reversed.</returns>
        private long BitNot()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionBitNot, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitNotInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitNotInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter == null ? "null" : parameter.GetType().Name));
                }

                long result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = ~Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionBitNot, "BitNot('{0}') evaluated '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionBitNot, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to set a specified bit on a flag to 1.
        /// Function Syntax: BitOr(mask:integer, value:flag)
        /// </summary>
        /// <returns>A new version of flag with the bits specified in mask set to 1.</returns>
        private long BitOr()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionBitOr, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitOrInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 2, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitOrNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitOrInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                parameter = this.parameters[1];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitOrNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 2));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionBitOrInvalidSecondFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidSecondFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                long result;
                if (this.mode != EvaluationMode.Parse)
                {
                    result = Convert.ToInt64(this.parameters[0], CultureInfo.InvariantCulture) | Convert.ToInt64(this.parameters[1], CultureInfo.InvariantCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionBitOr, "BitOr('{0}', '{1}') returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionBitOr, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to concatenate two or more strings.
        /// Function Syntax: Concatenate(string1:string, string2:string, ....)
        /// </summary>
        /// <returns>All input string parameters are concatenated with each other.</returns>
        private string Concatenate()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConcatenate, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConcatenateInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinCountError, this.function, "2", this.parameters.Count));
                }

                Type parameterType = typeof(string);
                for (int i = 0; i < this.parameters.Count; ++i)
                {
                    object parameter = this.parameters[i];
                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConcatenateInvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, i, parameterType.Name, parameter.GetType().Name));
                    }
                }

                string result;
                if (this.mode != EvaluationMode.Parse)
                {
                    StringBuilder concatenated = new StringBuilder();

                    foreach (object parameter in this.parameters)
                    {
                        concatenated.Append(Convert.ToString(parameter, CultureInfo.InvariantCulture));
                    }

                    result = concatenated.ToString();

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConcatenate, "Concatenate('{0}', '{1}', ...) returned '{2}'.", this.parameters[0], this.parameters[1], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConcatenate, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert the string representation of a GUID to a binary representation of the GUID.
        /// Function Syntax: ConvertStringToGuid(value:string)
        /// </summary>
        /// <returns>The string GUID converted to its binary representation.</returns>
        private byte[] ConvertStringToGuid()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertStringToGuid, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertStringToGuidInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(string);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertStringToGuidNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertStringToGuidInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                byte[] result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = (new Guid((string)parameter)).ToByteArray();

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertStringToGuid, "ConvertStringToGuid('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertStringToGuid, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert a base64 string to a byte array.
        /// Function Syntax: ConvertFromBase64(base64string:string)
        /// </summary>
        /// <returns>A byte array representation of the given string.</returns>
        private byte[] ConvertFromBase64()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertFromBase64, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertFromBase64InvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(string);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertFromBase64NullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertFromBase64InvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                byte[] result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = Convert.FromBase64String((string)parameter);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertFromBase64, "ConvertFromBase64('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertFromBase64, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert a byte array containing a security identifier to a string.
        /// Function Syntax: ConvertSidToString(sid:binary)
        /// </summary>
        /// <returns>A string representation of the SID.</returns>
        private string ConvertSidToString()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertSidToString, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertSidToStringInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(byte[]);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertSidToStringNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertSidToStringInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    SecurityIdentifier si = new SecurityIdentifier((byte[])parameter, 0);
                    result = si.ToString();

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertSidToString, "ConvertSidToString('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertSidToString, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert a byte array to a base64 encoded string.
        /// Function Syntax: ConvertToBase64(input:binary)
        /// </summary>
        /// <returns>A base64 encoded string representation of the input.</returns>
        private string ConvertToBase64()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertToBase64, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToBase64InvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(byte[]);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToBase64NullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToBase64InvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = Convert.ToBase64String((byte[])parameter);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertToBase64, "ConvertToBase64('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertToBase64, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert a string or a number to a boolean value.
        /// Function Syntax: ConvertToBoolean(value:string) or ConvertToBoolean(value:integer)
        /// </summary>
        /// <returns>true or false, which reflects the value returned by invoking the IConvertible.ToBoolean method for the underlying type of value. If value is null, the method returns false.</returns>
        private bool ConvertToBoolean()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertToBoolean, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToBooleanInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                bool result;

                if (this.mode != EvaluationMode.Parse)
                {
                    object parameter = this.parameters[0];
                    result = Convert.ToBoolean(parameter, CultureInfo.InvariantCulture);

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertToBoolean, "ConvertToBoolean('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = false;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertToBoolean, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert a string or a boolean to a number.
        /// Function Syntax: ConvertToNumber(value:string) or ConvertToNumber(value:boolean)
        /// </summary>
        /// <returns>A number representation of the given value.</returns>
        private long ConvertToNumber()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertToNumber, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToNumberInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(string);
                Type parameterType2 = typeof(bool);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToNumberNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2) && !this.VerifyType(parameter, typeof(long)))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToNumberInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError2, this.function, parameterType.Name, parameterType2.Name, parameter.GetType().Name));
                }

                long result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = Convert.ToInt64(parameter, CultureInfo.InvariantCulture);
                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertToNumber, "ConvertToNumber('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = 0;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertToNumber, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to convert a value to its string representation.
        /// Function Syntax: ConvertToString(value:integer) or ConvertToString(value:boolean) or ConvertToString(value:guid)
        /// </summary>
        /// <returns>A string representation of the given value.</returns>
        private string ConvertToString()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionConvertToString, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToStringInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(long);
                Type parameterType2 = typeof(bool);
                Type parameterType3 = typeof(Guid);
                Type parameterType4 = typeof(byte[]);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToStringNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2) && !this.VerifyType(parameter, parameterType3) && !this.VerifyType(parameter, parameterType4) && !this.VerifyType(parameter, typeof(string)))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionConvertToStringInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError3, this.function, parameterType.Name, parameterType2.Name, parameterType3.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = Convert.ToString(parameter, CultureInfo.InvariantCulture);
                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionConvertToString, "ConvertToString('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionConvertToString, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to generate a Carriage Return/Line Feed.
        /// Function Syntax: CRLF()
        /// </summary>
        /// <returns>A CRLF is the output.</returns>
        private string CRLF()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionCrlf, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 0)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCrlfInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 0, this.parameters.Count));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    result = "\r\n";
                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionCrlf, "CRLF() returned '{0}'.", result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionCrlf, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to escape the DN component of a distinguished name specified in LDAP format.
        /// Function Syntax: EscapeDNComponent(dnComponent:string)
        /// </summary>
        /// <returns>The escaped DN component.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        private string EscapeDNComponent()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionEscapeDNComponent, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionEscapeDNComponentInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(string);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionEscapeDNComponentNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionEscapeDNComponentInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                string result;

                if (this.mode != EvaluationMode.Parse)
                {
                    string[] parts = ((string)parameter).Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionEscapeDNComponentInvalidFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterError, this.function, 1, "X=y", parameter));
                    }

                    string dnComponent = parts[0].Trim() + "=" + parts[1].Trim();
                    StringBuilder stringBuilder = new StringBuilder();
                    bool flag = true;

                    foreach (char chr in dnComponent.Trim())
                    {
                        switch (chr)
                        {
                            case '=':
                                if (flag)
                                {
                                    stringBuilder.Append(chr);
                                    flag = false;
                                }
                                else
                                {
                                    stringBuilder.Append("\\");
                                    stringBuilder.Append(chr);
                                }

                                break;
                            case '"':
                            case '\\':
                            case ';':
                            case '#':
                            case '>':
                            case '<':
                            case '+':
                            case ',':
                                stringBuilder.Append("\\");
                                stringBuilder.Append(chr);
                                break;
                            default:
                                stringBuilder.Append(chr);
                                break;
                        }
                    }

                    result = stringBuilder.ToString();

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionEscapeDNComponent, "EscapeDNComponent('{0}') returned '{1}'.", this.parameters[0], result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionEscapeDNComponent, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to split the string into substring delimited by the specified separator.
        /// Function Syntax: SplitString(input:string, separator:string, [count:integer])
        /// </summary>
        /// <returns>A list whose elements contain the substrings in this string that are delimited by the separator.</returns>
        private List<string> SplitString()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionSplitString, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 1 || this.parameters.Count > 3)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSplitStringInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinMaxCountError, this.function, "1", "3", this.parameters.Count));
                }

                Type parameterType = typeof(string);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSplitStringNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSplitStringInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, 1, parameterType.Name, parameter.GetType().Name));
                }

                string input = parameter as string;
                string separator = null;
                int count = int.MaxValue;

                if (this.parameters.Count > 1)
                {
                    parameterType = typeof(string);
                    parameter = this.parameters[1];
                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSplitStringInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, 2, parameterType.Name, parameter.GetType().Name));
                    }

                    separator = parameter as string;
                }

                if (this.parameters.Count == 3)
                {
                    parameterType = typeof(int);
                    parameter = this.parameters[2];
                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSplitStringInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, 3, parameterType.Name, parameter.GetType().Name));
                    }

                    count = Convert.ToInt32(parameter, CultureInfo.InvariantCulture);
                }

                List<string> result;
                if (this.mode != EvaluationMode.Parse)
                {
                    result = !string.IsNullOrEmpty(separator) ? input.Split(new string[] { separator }, count, StringSplitOptions.RemoveEmptyEntries).ToList() : input.Split(default(string[]), count, StringSplitOptions.RemoveEmptyEntries).ToList();

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionSplitString, "SplitString('{0}', '{1}', '{2}') returned a list with '{3}' items.", input, separator, count, result.Count);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionSplitString, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to remove duplicate values from a list of items.
        /// Function Syntax: RemoveDuplicates(input:list of string)
        /// </summary>
        /// <returns>A list without any duplicates.</returns>
        private List<string> RemoveDuplicates()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionRemoveDuplicates, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRemoveDuplicatesInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                Type parameterType = typeof(List<string>);
                object parameter = this.parameters[0];
                if (parameter == null)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRemoveDuplicatesNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, 1));
                }

                if (!this.VerifyType(parameter, parameterType))
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionRemoveDuplicatesInvalidFirstFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFirstFunctionParameterTypeError, this.function, parameterType.Name, parameter.GetType().Name));
                }

                List<string> result;

                if (this.mode != EvaluationMode.Parse)
                {
                    List<string> input = (List<string>)parameter;
                    result = new List<string>(input.Count);

                    foreach (string item in input.Where(item => !result.Contains(item, StringComparer.OrdinalIgnoreCase)))
                    {
                        result.Add(item);
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionRemoveDuplicates, "RemoveDuplicates('{0}') returned a list with '{1}' items removed.", this.parameters[0], input.Count - result.Count);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionRemoveDuplicates, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to sort the specified List.
        /// Function Syntax: SortList(values:[list or object])
        /// </summary>
        /// <returns>The sorted the input list. If the input is not a list, returns the input.</returns>
        private object SortList()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionSortList, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count != 1)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionSortListInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterCountError, this.function, 1, this.parameters.Count));
                }

                object result;

                if (this.mode != EvaluationMode.Parse)
                {
                    if (this.parameters[0] == null)
                    {
                        result = null;
                    }
                    else
                    {
                        Type paramType = this.parameters[0].GetType();
                        if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            List<object> list = ((IEnumerable)this.parameters[0]).Cast<object>().ToList();

                            list.Sort();

                            result = this.parameters[0];
                        }
                        else
                        {
                            result = this.parameters[0];
                        }
                    }

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionSortList, "SortList() returned '{0}'.", result);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionSortList, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to create a <see cref="DbParameter" /> object for use in various ExecuteSql functions.
        /// Function Syntax: CreateSqlParameter(sqlConnectionStringConfigKey:string, parameterName:string, parameterValue:object [,parameterDirection:string])
        /// </summary>
        /// <returns>The <see cref="DbParameter" /> object.</returns>
        private DbParameter CreateSqlParameter()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionCreateSqlParameter, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 3 || this.parameters.Count > 4)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinMaxCountError, this.function, 3, 4, this.parameters.Count));
                }

                for (int i = 0; i < 2; ++i)
                {
                    object parameter = this.parameters[i]; // 0 = SQL Connection String Config Key, 1 = Parameter Name
                    Type parameterType = typeof(string);
                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterInvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                    }

                    if (this.mode != EvaluationMode.Parse)
                    {
                        switch (i)
                        {
                            case 0: // SQL Connection String Config Key
                                {
                                    var connectionStringConfigKey = this.parameters[0] as string;
                                    var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringConfigKey];
                                    var providerName = connectionStringConfig != null ? connectionStringConfig.ProviderName : null;
                                    if (string.IsNullOrEmpty(providerName))
                                    {
                                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterInvalidFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidConfigKeyConfiguration, this.function, connectionStringConfigKey));
                                    }

                                    try
                                    {
                                        DbProviderFactories.GetFactory(providerName);
                                    }
                                    catch (DbException)
                                    {
                                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterInvalidFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidConfigKeyConfiguration, this.function, connectionStringConfigKey));
                                    }
                                }

                                break;
                            case 1: // SQL Parameter Name
                                var parameterName = parameter as string;
                                if (!parameterName.StartsWith("@", StringComparison.OrdinalIgnoreCase))
                                {
                                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterInvalidFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterError, this.function, i, "@" + parameterName, parameterName));
                                }

                                break;
                        }
                    }
                }

                if (this.parameters.Count == 4)
                {
                    var i = 3;
                    object parameter = this.parameters[i]; // 3 = Parameter Direction
                    Type parameterType = typeof(string);
                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterInvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                    }

                    if (this.mode != EvaluationMode.Parse)
                    {
                        try
                        {
                            Enum.Parse(typeof(ParameterDirection), parameter as string, true);
                        }
                        catch (ArgumentException e)
                        {
                            throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterInvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, e, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                        }
                    }
                }

                DbParameter result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    string parameterName = this.parameters[1] as string;
                    object parameterValue = this.parameters[2];
                    var parameterDirection = ParameterDirection.Input;

                    if (this.parameters.Count == 4)
                    {
                        parameterDirection = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), this.parameters[3] as string, true);
                    }

                    var connectionStringConfigKey = this.parameters[0] as string;
                    var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringConfigKey];
                    var providerName = connectionStringConfig != null ? connectionStringConfig.ProviderName : null;
                    if (string.IsNullOrEmpty(providerName))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameterNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidConfigKeyConfiguration, this.function, connectionStringConfigKey));
                    }

                    var factory = DbProviderFactories.GetFactory(providerName);

                    result = factory.CreateParameter();
                    result.ParameterName = parameterName;
                    result.Direction = parameterDirection;
                    result.Value = (parameterValue == null) ? DBNull.Value : parameterValue;

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionCreateSqlParameter, "CreateSqlParameter('{0}', '{1}', '{2}', '{3}') returned '{4}'.", this.parameters[0], result.ParameterName, result.Value, result.Direction, result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionCreateSqlParameter, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to create a <see cref="DbParameter" /> object for use in various ExecuteSql functions.
        /// Function Syntax: CreateSqlParameter2(sqlConnectionStringConfigKey:string, parameterName:string, parameterDirection:string, parameterType:string [, parameterSize:integer, parameterValue:object])
        /// </summary>
        /// <returns>The <see cref="DbParameter" /> object.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Reviewed.")]
        private DbParameter CreateSqlParameter2()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionCreateSqlParameter2, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 4 || this.parameters.Count > 6)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinMaxCountError, this.function, 4, 6, this.parameters.Count));
                }

                for (int i = 0; i < 4; ++i)
                {
                    object parameter = this.parameters[i]; // 0 = SQL Connection String Config Key, 1 = Parameter Name, 2 = Parameter Direction, 3 = Parameter Type
                    Type parameterType = typeof(string);
                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2NullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                    }

                    if (this.mode != EvaluationMode.Parse)
                    {
                        switch (i)
                        {
                            case 0: // SQL Connection String Config Key
                                {
                                    var connectionStringConfigKey = this.parameters[0] as string;
                                    var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringConfigKey];
                                    var providerName = connectionStringConfig != null ? connectionStringConfig.ProviderName : null;
                                    if (string.IsNullOrEmpty(providerName))
                                    {
                                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2NullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidConfigKeyConfiguration, this.function, connectionStringConfigKey));
                                    }

                                    try
                                    {
                                        DbProviderFactories.GetFactory(providerName);
                                    }
                                    catch (DbException)
                                    {
                                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2NullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidConfigKeyConfiguration, this.function, connectionStringConfigKey));
                                    }
                                }

                                break;
                            case 1: // SQL Parameter Name
                                var parameterName = parameter as string;
                                if (!parameterName.StartsWith("@", StringComparison.OrdinalIgnoreCase))
                                {
                                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterError, this.function, i, "@" + parameterName, parameterName));
                                }

                                break;
                            case 2: // SQL Parameter Direction
                                {
                                    try
                                    {
                                        Enum.Parse(typeof(ParameterDirection), parameter as string, true);
                                    }
                                    catch (ArgumentException e)
                                    {
                                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, e, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                                    }
                                }

                                break;
                            case 3: // SQL Parameter Type
                                {
                                    var connectionStringConfigKey = this.parameters[0] as string;
                                    var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringConfigKey];
                                    var providerName = connectionStringConfig != null ? connectionStringConfig.ProviderName : null;
                                    var factory = DbProviderFactories.GetFactory(providerName);

                                    var dbParameter = factory.CreateParameter();
                                    if (dbParameter is SqlParameter)
                                    {
                                        try
                                        {
                                            Enum.Parse(typeof(SqlDbType), parameter as string, true);
                                        }
                                        catch (ArgumentException)
                                        {
                                            try
                                            {
                                                Enum.Parse(typeof(DbType), parameter as string, true);
                                            }
                                            catch (ArgumentException e)
                                            {
                                                throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError2, e, this.function, i + 1, typeof(SqlDbType), typeof(DbType), parameter.GetType().Name));
                                            }
                                        }
                                    }
                                    else if (dbParameter is OdbcParameter)
                                    {
                                        try
                                        {
                                            Enum.Parse(typeof(OdbcType), parameter as string, true);
                                        }
                                        catch (ArgumentException)
                                        {
                                            try
                                            {
                                                Enum.Parse(typeof(DbType), parameter as string, true);
                                            }
                                            catch (ArgumentException e)
                                            {
                                                throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError2, e, this.function, i + 1, typeof(OdbcType), typeof(DbType), parameter.GetType().Name));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            Enum.Parse(typeof(DbType), parameter as string, true);
                                        }
                                        catch (ArgumentException e)
                                        {
                                            throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, e, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                                        }
                                    }
                                }

                                break;
                        }
                    }
                }

                if (this.parameters.Count > 4)
                {
                    var i = 4;
                    object parameter = this.parameters[i]; // SQL Parameter Size
                    Type parameterType = typeof(int);
                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2NullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionCreateSqlParameter2InvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                    }
                }

                DbParameter result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    var connectionStringConfigKey = this.parameters[0] as string;
                    var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringConfigKey];
                    var providerName = connectionStringConfig != null ? connectionStringConfig.ProviderName : null;
                    var factory = DbProviderFactories.GetFactory(providerName);

                    var parameterName = this.parameters[1] as string;
                    var parameterDirection = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), this.parameters[2] as string, true);
                    var parameterDbType = this.parameters[3] as string;

                    var parameterSize = 0;
                    if (this.parameters.Count > 4)
                    {
                        parameterSize = Convert.ToInt32(this.parameters[4], CultureInfo.InvariantCulture); // SQL Parameter Size
                    }

                    object parameterValue = null;
                    if (this.parameters.Count > 5)
                    {
                        parameterValue = this.parameters[5]; // SQL Parameter Value
                    }

                    result = factory.CreateParameter();
                    result.ParameterName = parameterName;
                    result.Direction = parameterDirection;

                    if (result is SqlParameter)
                    {
                        try
                        {
                            ((SqlParameter)result).SqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), parameterDbType, true);
                        }
                        catch (ArgumentException)
                        {
                            result.DbType = (DbType)Enum.Parse(typeof(DbType), parameterDbType, true);
                        }
                    }
                    else if (result is OdbcParameter)
                    {
                        try
                        {
                            ((OdbcParameter)result).OdbcType = (OdbcType)Enum.Parse(typeof(OdbcType), parameterDbType, true);
                        }
                        catch (ArgumentException)
                        {
                            result.DbType = (DbType)Enum.Parse(typeof(DbType), parameterDbType, true);
                        }
                    }
                    else
                    {
                        result.DbType = (DbType)Enum.Parse(typeof(DbType), parameterDbType, true);
                    }

                    if (parameterSize != 0)
                    {
                        result.Size = parameterSize;
                    }

                    result.Value = (parameterValue == null) ? DBNull.Value : parameterValue;

                    Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionCreateSqlParameter2, "CreateSqlParameter2('{0}', '{1}', '{2}', '{3}', '{4}', '{5}') returned '{6}'.", connectionStringConfigKey, result.ParameterName, result.Direction, result.DbType, result.Size, result.Value, result);
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionCreateSqlParameter2, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to execute a SQL stored procedure or query against a SQL database.
        /// Function Syntax: ExecuteSqlScalar(sqlConnectionStringConfigKey:string, sqlStatement:string [, parameter1:SqlParameter, parameter2:SqlParameter, ...])
        /// </summary>
        /// <returns>The first column of the first row in the result set.</returns>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "The query does not contain end-user input as well as it's parameterised.")]
        private object ExecuteSqlScalar()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionExecuteSqlScalar, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlScalarInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinCountError, this.function, 2, this.parameters.Count));
                }

                for (int i = 0; i < 2; ++i)
                {
                    object parameter = this.parameters[i];
                    Type parameterType = typeof(string);

                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlScalarNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlScalarNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                    }
                }

                for (int i = 2; i < this.parameters.Count; ++i)
                {
                    object parameter = this.parameters[i];
                    Type parameterType = typeof(DbParameter);
                    Type parameterType2 = typeof(SqlParameter);
                    Type parameterType3 = typeof(OdbcParameter);

                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlScalarNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2) && !this.VerifyType(parameter, parameterType3))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlScalarInvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError3, this.function, i + 1, parameterType.Name, parameterType2.Name, parameterType3.Name, parameter.GetType().Name));
                    }
                }

                object result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    var connectionStringConfigKey = this.parameters[0] as string;
                    var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringConfigKey];
                    var connectionString = connectionStringConfig != null ? connectionStringConfig.ConnectionString : null;
                    var providerName = connectionStringConfig != null ? connectionStringConfig.ProviderName : null;

                    if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(providerName))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlScalarNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidConfigKeyConfiguration, this.function, connectionStringConfigKey));
                    }

                    var commandTimeout = 30;
                    commandTimeout = int.TryParse(ConfigurationManager.AppSettings["WAL_DBCommandTimeout"], out commandTimeout) ? commandTimeout : 30;

                    try
                    {
                        var factory = DbProviderFactories.GetFactory(providerName);
                        using (var dbConnection = factory.CreateConnection())
                        {
                            dbConnection.ConnectionString = connectionString;
                            using (var dbCommand = factory.CreateCommand())
                            {
                                dbCommand.Connection = dbConnection;
                                var dbCmdText = (this.parameters[1] as string).Trim();
                                dbCommand.CommandText = dbCmdText;

                                // If the command text has spaces inbetween, assume it's a query, else stored procedure as we won't support anything else.
                                dbCommand.CommandType = dbCmdText.Contains(" ") ? CommandType.Text : CommandType.StoredProcedure;
                                dbCommand.CommandTimeout = commandTimeout;

                                string paramString = string.Empty;
                                for (var i = 2; i < this.parameters.Count; ++i)
                                {
                                    var parameter = this.parameters[i] as DbParameter;
                                    dbCommand.Parameters.Add(parameter);
                                    paramString += parameter.ParameterName + "='" + parameter.Value + "',";
                                }

                                paramString = paramString.TrimEnd(',');

                                if (providerName.Equals("System.Data.Odbc", StringComparison.OrdinalIgnoreCase) && dbCommand.CommandType == CommandType.StoredProcedure)
                                {
                                    var paramCount = dbCommand.Parameters.Count;
                                    var paramPlaceholder = string.Empty;
                                    for (var i = 0; i < paramCount; ++i)
                                    {
                                        paramPlaceholder += string.IsNullOrEmpty(paramPlaceholder) ? "?" : ",?";
                                    }

                                    if (!string.IsNullOrEmpty(paramPlaceholder))
                                    {
                                        dbCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "{0} CALL {1}({2}) {3}", "{", dbCmdText, paramPlaceholder, "}");
                                    }
                                }

                                dbConnection.Open();
                                result = dbCommand.ExecuteScalar();

                                if (result == DBNull.Value)
                                {
                                    result = null;
                                }

                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionExecuteSqlScalar, "ExecuteSqlScalar('{0}', '{1}, '{2}') returned '{3}'.", connectionStringConfigKey, dbCommand.CommandText, paramString, result);
                            }
                        }
                    }
                    catch (DbException e)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlScalarNullFunctionParameterError, e);
                    }
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionExecuteSqlScalar, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        /// <summary>
        /// This function is used to execute a SQL stored procedure or insert, update, delete statements against a SQL database.
        /// Function Syntax: ExecuteSqlNonQuery(sqlConnectionStringConfigKey:string, sqlStatement:string [, parameter1:SqlParameter, parameter2:SqlParameter, ...])
        /// </summary>
        /// <returns>The dictionary of output parameter names and their values.</returns>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "The query does not contain end-user input as well as it's parameterised.")]
        private Dictionary<string, object> ExecuteSqlNonQuery()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ExpressionFunctionExecuteSqlNonQuery, "Evaluation Mode: '{0}'.", this.mode);

            try
            {
                if (this.parameters.Count < 2)
                {
                    throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlNonQueryInvalidFunctionParameterCountError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterMinCountError, this.function, 2, this.parameters.Count));
                }

                for (int i = 0; i < 2; ++i)
                {
                    object parameter = this.parameters[i];
                    Type parameterType = typeof(string);

                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlNonQueryNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlNonQueryNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError, this.function, i + 1, parameterType.Name, parameter.GetType().Name));
                    }
                }

                for (int i = 2; i < this.parameters.Count; ++i)
                {
                    object parameter = this.parameters[i];
                    Type parameterType = typeof(DbParameter);
                    Type parameterType2 = typeof(SqlParameter);
                    Type parameterType3 = typeof(OdbcParameter);

                    if (parameter == null)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlNonQueryInvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_NullFunctionParameterError, this.function, i + 1));
                    }

                    if (!this.VerifyType(parameter, parameterType) && !this.VerifyType(parameter, parameterType2) && !this.VerifyType(parameter, parameterType3))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlNonQueryInvalidFunctionParameterTypeError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidFunctionParameterTypeError3, this.function, i + 1, parameterType.Name, parameterType2.Name, parameterType3.Name, parameter.GetType().Name));
                    }
                }

                Dictionary<string, object> result = null;

                if (this.mode != EvaluationMode.Parse)
                {
                    var connectionStringConfigKey = this.parameters[0] as string;
                    var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringConfigKey];
                    var connectionString = connectionStringConfig != null ? connectionStringConfig.ConnectionString : null;
                    var providerName = connectionStringConfig != null ? connectionStringConfig.ProviderName : null;

                    if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(providerName))
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlNonQueryNullFunctionParameterError, new InvalidFunctionFormatException(Messages.ExpressionFunction_InvalidConfigKeyConfiguration, this.function, connectionStringConfigKey));
                    }

                    var commandTimeout = 30;
                    commandTimeout = int.TryParse(ConfigurationManager.AppSettings["WAL_DBCommandTimeout"], out commandTimeout) ? commandTimeout : 30;

                    try
                    {
                        var factory = DbProviderFactories.GetFactory(providerName);
                        using (var dbConnection = factory.CreateConnection())
                        {
                            dbConnection.ConnectionString = connectionString;
                            using (var dbCommand = factory.CreateCommand())
                            {
                                dbCommand.Connection = dbConnection;
                                var dbCmdText = (this.parameters[1] as string).Trim();
                                dbCommand.CommandText = dbCmdText;

                                // If the command text has spaces inbetween, assume it's a query, else stored procedure as we won't support anything else.
                                dbCommand.CommandType = dbCmdText.Contains(" ") ? CommandType.Text : CommandType.StoredProcedure;
                                dbCommand.CommandTimeout = commandTimeout;

                                string paramString = string.Empty;
                                for (var i = 2; i < this.parameters.Count; ++i)
                                {
                                    var parameter = this.parameters[i] as DbParameter;
                                    if (parameter != null)
                                    {
                                        dbCommand.Parameters.Add(parameter);
                                        paramString += parameter.ParameterName + "='" + parameter.Value + "',";
                                    }
                                }

                                paramString = paramString.TrimEnd(',');

                                if (providerName.Equals("System.Data.Odbc", StringComparison.OrdinalIgnoreCase) && dbCommand.CommandType == CommandType.StoredProcedure)
                                {
                                    var paramCount = dbCommand.Parameters.Count;
                                    var paramPlaceholder = string.Empty;
                                    for (var i = 0; i < paramCount; ++i)
                                    {
                                        paramPlaceholder += string.IsNullOrEmpty(paramPlaceholder) ? "?" : ",?";
                                    }

                                    if (!string.IsNullOrEmpty(paramPlaceholder))
                                    {
                                        dbCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "{0} CALL {1}({2}) {3}", "{", dbCmdText, paramPlaceholder, "}");
                                    }
                                }

                                dbConnection.Open();
                                var rowsAffected = dbCommand.ExecuteNonQuery();

                                result = new Dictionary<string, object>();
                                result.Add("@RowsAffected", rowsAffected);
                                foreach (DbParameter param in dbCommand.Parameters)
                                {
                                    if (param.Direction != ParameterDirection.Input)
                                    {
                                        result.Add(param.ParameterName, param.Value);
                                    }
                                }

                                string paramOutString = string.Join(",", result.Select(kvp => string.Format(CultureInfo.InvariantCulture, "{0}='{1}'", kvp.Key, kvp.Value)).ToArray());

                                Logger.Instance.WriteVerbose(EventIdentifier.ExpressionFunctionExecuteSqlNonQuery, "ExecuteSqlNonQuery('{0}', '{1}, '{2}') returned a dictionary of '{3}' parameter/value pairs.", connectionStringConfigKey, dbCommand.CommandText, paramString, paramOutString);
                            }
                        }
                    }
                    catch (DbException e)
                    {
                        throw Logger.Instance.ReportError(EventIdentifier.ExpressionFunctionExecuteSqlNonQueryNullFunctionParameterError, e);
                    }
                }

                return result;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ExpressionFunctionExecuteSqlNonQuery, "Evaluation Mode: '{0}'.", this.mode);
            }
        }

        #endregion

        #endregion
    }
}