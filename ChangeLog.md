## MIMWAL Change Log

All notable changes to MIMWAL project will be documented in this file. The "Unreleased" section at the top is for keeping track of important changes that might make it to upcoming releases.

### Version [Unreleased]

* Support for multi-valued attributes in `[//Effective]` lookup in AuthZ workflows.
* Implement Approve Request Activity.
* Support for `[//Value]` lookups in Query definitions across rest of the activities.
------------

### Version 2.19.0111.0

#### Changed

* [Generate Unique Value Activity][GenerateUniqueValueActivity] now has the Conflict Filter search optimisation logic for the *starts-with* XPath function as documented in the [Wiki](https://github.com/Microsoft/MIMWAL/wiki/Generate-Unique-Value-Activity#conflict-filter) turned off by default.
To get the backward compatible behaviour, define the app setting GenerateUniqueValueActivity_OptimizeUniquenessKey = true in the FIMService app.config.

------------

### Version 2.18.1110.0

#### Changed

* [FormatMultivaluedList][FormatMultivaluedListFunction] now accepts all null values.
* [DateTimeAdd][DateTimeAddFunction] now accepts a null value for timespan parameter. 
* [ConvertStringToGuid][ConvertStringToGuidFunction] returns Empty GUID if the input is a null/empty string.
* [ConvertFromBase64][ConvertFromBase64Function] returns null if the input is a null/empty string.
* [ConvertToBase64][ConvertToBase64Function] returns null if the input is a null/empty string.
* [ConvertToNumber][ConvertToNumberFunction] returns 0 if the input is a null/empty string.
* [SplitString][SplitStringFunction] returns null if the input is a null/empty string.
* [RemoveDuplicates][RemoveDuplicatesFunction] returns null if the input is a null list.
* [Eq][EqFunction] function will return true if one string parameter is null and other string parameter is Empty

#### Removed

* Removed validation check from RunPowerShellScript activity UI from that the PowerShell User Password is Decryptable as the  code runs under the context of submitter instead of FIMService plus the code runs on the Portal Server which may not be co-located with FIMService server.

------------

### Version 2.18.0318.0

#### Added

* [ConvertNumberToList][ConvertNumberToListFunction] function.
* [Multiply][MultiplyFunction] function.
* [Divide][DivideFunction] function.
* [Mod][ModFunction] function.

#### Changed

* [ConvertToString][ConvertToStringFunction] will return null if the input parameter is null instead of throwing an exception.

------------

### Version 2.17.0927.0

#### Fixed

* Bugfix in SortList function.

------------

### Version 2.17.0721.0

#### Added

* Support for Query and Iternation in SendEmailNotification activity.
* [DateTimeFromString][DateTimeFromStringFunction] function.

#### Fixed

* Fixed ParameterValue* functions to return attribute values in the original datatype than as string.
* Correctly setting connection timeout for ODBC connection in ExecuteSqlNonQuery and ExecuteSqlScalar functions.

------------

### Version 2.17.0414.0

#### Added

* Support for executing SQL stored procedures and queries for SQL Server and ODBC Data Sources with the implementation of following new functions: 
	* New function [CreateSqlParameter][CreateSqlParameterFunction] 
	* New function [CreateSqlParameter2][CreateSqlParameter2Function]
	* New function [ExecuteSqlNonQuery][ExecuteSqlNonQueryFunction]
	* New function [ExecuteSqlScalar][ExecuteSqlScalarFunction]
	* New function [ValueByKey][ValueByKeyFunction]

------------

### Version 2.16.1028.0

#### Changed

* [FormatMultivaluedList][FormatMultivaluedListFunction] function now supports list of variable lengths as input to format. It also no more expects the list to be of strings. The input can be any object type.
* [ConvertToString][ConvertToStringFunction] function now supports a Guid as input.

------------

### Version 2.16.0710.0

#### Changed

* RunPowerShellScript User for can now be specified in the UPN format. The Domain\UserName or UPN format is also only required if "Impersonate PowerShell User" option is selected.
* Iteration in CreateResource, DeleteResource and UpdateResources activities now re-evaluates not only `[//Value]` expressions but also `[//WorkflowData]` expressions.

#### Added

* [FormatMultivaluedList][FormatMultivaluedListFunction] function now supports more than one list of strings as input to format.
* [ConvertToUniqueIdentifier][ConvertToUniqueIdentifierFunction] now supports a Guid in byte[] format as input.

------------

### Version 2.16.0320.0

#### Changed

* The `[//Request/RequestParameter]` lookup for a SystemEvent request will resolve to request parameters in the parent request.

------------

### Version 2.16.0315.0

#### Added

* Support for Dynamic Grammar Resolution capability to [Update Resources][UpdateResourcesActivity] activity.
	* The activity can be configured to resolve / evaluate lookups and expressions in the values returned by the expressions in update definitions. e.g. The string returned by `[//Queries/EmailTemplate/EmailBody]` will be parsed and all lookups will be resolved.

#### Changed

* Use of [EvaluateExpression][EvaluateExpressionFunction] function will now log a deprecation warning in the event log in favour of using the newly implemented Dynamic Grammar Resolution capability of the [Update Resources][UpdateResourcesActivity] activity.

#### Fixed

* Error event logging logic in the [Run PowerShell Script][RunPowerShellScriptActivity] activity.

------------

### Version 2.16.0305.0

#### Added

* Support for dropping hard / soft signs and any other symbols in [NormalizeString][NormalizeStringFunction] function .

#### Changed

* [Run PowerShell Script][RunPowerShellScriptActivity] activity will now only abort the workflow execution when an unhandled or explicit exception is thrown from the PowerShell script. Non-terminating errors in the script will only get logged to the event log.

------------

### Version [2.16.0130.0] 

#### Added

* Support for executing a collection of Queries.
* New function [FormatMultivaluedList][FormatMultivaluedList].
* Support for `[//Value]` lookup in Request Actor expressions in CreateResource, DeleteResources and UpdateResources activities.

#### Fixed

* Fixed broken `[//ComparedRequest]` lookup.
* Fix for deleting a definition listing causing deletion / mess up with the remaining listings.

------------

### Version 2.16.0115.0

#### Added

* Support for "/" as a Null Query.

#### Fixed

* Added null check in the ForEachIteration_UntilCondition function of UpdateResources and CreateResource activities to prevent crash when AEC is defined, but Iteration not.

------------

### Version 2.16.0110.0

#### Added

* Re-branded of FIMWAL "2" version 2.15.1016.0 as MIMWAL.
	* AICs are updated to a "WAL: " prefix instead of "FIMWAL: ".
	* The event log name changed from "FIMWAL" to "WAL".
* Added design time check for the Activity Execution Condition to be a boolean function expression.
	* If in FIMWAL "2" you were using an `IIF` function expression as an Activity Execution Condition, you'll need to wrap it around a `ConvertToBoolean` for UI validation to succeed when making any changes to the activity in future.
* Added support to break iteration loop in `UpdateResources` and `CreateResource` activities.
	* Reserved `$__BREAK_ITERATION__` for the name of the boolean variable that needs to be set to true to break the iteration loop.
* Added support for cascaded queries lookup so that data from the first query can be used in the XPath search filter expression of the second query.

#### Changed

* Updated `Add` and `Subtract` function to treat null input values as zero instead of throwing exception
* Made substitutions parameter optional in `NormalizeString` function.

#### Removed

* Deleted compiled referenced DLLs and EXEs until LCA provides a split licence.
	* Build and Deployment Wiki documents how to gather these files for building the solution.

#### Fixed

* There are no bug fixes in this release.

[AddFunction]: https://github.com/Microsoft/MIMWAL/wiki/Add-Function
[AfterFunction]: https://github.com/Microsoft/MIMWAL/wiki/After-Function
[AndFunction]: https://github.com/Microsoft/MIMWAL/wiki/And-Function
[BeforeFunction]: https://github.com/Microsoft/MIMWAL/wiki/Before-Function
[BitAndFunction]: https://github.com/Microsoft/MIMWAL/wiki/BitAnd-Function
[BitNotFunction]: https://github.com/Microsoft/MIMWAL/wiki/BitNot-Function
[BitOrFunction]: https://github.com/Microsoft/MIMWAL/wiki/BitOr-Function
[ConcatenateFunction]: https://github.com/Microsoft/MIMWAL/wiki/Concatenate-Function
[ConcatenateMultivaluedStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConcatenateMultivaluedString-Function
[ContainsFunction]: https://github.com/Microsoft/MIMWAL/wiki/Contains-Function
[ConvertFromBase64Function]: https://github.com/Microsoft/MIMWAL/wiki/ConvertFromBase64-Function
[ConvertNumberToListFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertNumberToList-Function
[ConvertSIDToStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertSIDToString-Function
[ConvertStringToGUIDFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertStringToGUID-Function
[ConvertToBase64Function]: https://github.com/Microsoft/MIMWAL/wiki/ConvertToBase64-Function
[ConvertToBooleanFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertToBoolean-Function
[ConvertToNumberFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertToNumber-Function
[ConvertToStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertToString-Function
[ConvertToUniqueIdentifierFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertToUniqueIdentifier-Function
[CountFunction]: https://github.com/Microsoft/MIMWAL/wiki/Count-Function
[CreateSqlParameterFunction]: https://github.com/Microsoft/MIMWAL/wiki/CreateSqlParameter-Function
[CreateSqlParameter2Function]: https://github.com/Microsoft/MIMWAL/wiki/CreateSqlParameter2-Function
[CRLFFunction]: https://github.com/Microsoft/MIMWAL/wiki/CRLF-Function
[DateTimeAddFunction]: https://github.com/Microsoft/MIMWAL/wiki/DateTimeAdd-Function
[DateTimeFormatFunction]: https://github.com/Microsoft/MIMWAL/wiki/DateTimeFormat-Function
[DateTimeFromFileTimeUTCFunction]: https://github.com/Microsoft/MIMWAL/wiki/DateTimeFromFileTimeUTC-Function
[DateTimeFromStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/DateTimeFromString-Function
[DateTimeNowFunction]: https://github.com/Microsoft/MIMWAL/wiki/DateTimeNow-Function
[DateTimeSubtractFunction]: https://github.com/Microsoft/MIMWAL/wiki/DateTimeSubtract-Function
[DateTimeToFileTimeUTCFunction]: https://github.com/Microsoft/MIMWAL/wiki/DateTimeToFileTimeUTC-Function
[DivideFunction]: https://github.com/Microsoft/MIMWAL/wiki/Divide-Function
[EqFunction]: https://github.com/Microsoft/MIMWAL/wiki/Eq-Function
[EscapeDNComponentFunction]: https://github.com/Microsoft/MIMWAL/wiki/EscapeDNComponent-Function
[EvaluateExpressionFunction]: https://github.com/Microsoft/MIMWAL/wiki/EvaluateExpression-Function
[ExecuteSqlScalarFunction]: https://github.com/Microsoft/MIMWAL/wiki/ExecuteSqlScalar-Function
[ExecuteSqlNonQueryFunction]: https://github.com/Microsoft/MIMWAL/wiki/ExecuteSqlNonQuery-Function
[FirstFunction]: https://github.com/Microsoft/MIMWAL/wiki/First-Function
[FormatMultivaluedListFunction]: https://github.com/Microsoft/MIMWAL/wiki/FormatMultivaluedList-Function
[GenerateRandomPasswordFunction]: https://github.com/Microsoft/MIMWAL/wiki/GenerateRandomPassword-Function
[GreaterThanFunction]: https://github.com/Microsoft/MIMWAL/wiki/GreaterThan-Function
[IIFFunction]: https://github.com/Microsoft/MIMWAL/wiki/IIF-Function
[InsertValuesFunction]: https://github.com/Microsoft/MIMWAL/wiki/InsertValues-Function
[IsPresentFunction]: https://github.com/Microsoft/MIMWAL/wiki/IsPresent-Function
[LastFunction]: https://github.com/Microsoft/MIMWAL/wiki/Last-Function
[LeftFunction]: https://github.com/Microsoft/MIMWAL/wiki/Left-Function
[LeftPadFunction]: https://github.com/Microsoft/MIMWAL/wiki/LeftPad-Function
[LengthFunction]: https://github.com/Microsoft/MIMWAL/wiki/Length-Function
[LessThanFunction]: https://github.com/Microsoft/MIMWAL/wiki/LessThan-Function
[LowerCaseFunction]: https://github.com/Microsoft/MIMWAL/wiki/LowerCase-Function
[LTrimFunction]: https://github.com/Microsoft/MIMWAL/wiki/LTrim-Function
[MidFunction]: https://github.com/Microsoft/MIMWAL/wiki/Mid-Function
[ModFunction]: https://github.com/Microsoft/MIMWAL/wiki/Mod-Function
[MultiplyFunction]: https://github.com/Microsoft/MIMWAL/wiki/Multiply-Function
[NormalizeStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/NormalizeString-Function
[NotFunction]: https://github.com/Microsoft/MIMWAL/wiki/Not-Function
[NullFunction]: https://github.com/Microsoft/MIMWAL/wiki/Null-Function
[OrFunction]: https://github.com/Microsoft/MIMWAL/wiki/Or-Function
[ParametersContainFunction]: https://github.com/Microsoft/MIMWAL/wiki/ParametersContain-Function
[ParametersListFunction]: https://github.com/Microsoft/MIMWAL/wiki/ParametersList-Function
[ParametersTableFunction]: https://github.com/Microsoft/MIMWAL/wiki/ParametersTable-Function
[ParameterValueFunction]: https://github.com/Microsoft/MIMWAL/wiki/ParameterValue-Function
[ParameterValueAddedFunction]: https://github.com/Microsoft/MIMWAL/wiki/ParameterValueAdded-Function
[ParameterValueRemovedFunction]: https://github.com/Microsoft/MIMWAL/wiki/ParameterValueRemoved-Function
[ProperCaseFunction]: https://github.com/Microsoft/MIMWAL/wiki/ProperCase-Function
[RandomNumFunction]: https://github.com/Microsoft/MIMWAL/wiki/RandomNum-Function
[RegexMatchFunction]: https://github.com/Microsoft/MIMWAL/wiki/RegexMatch-Function
[RegexReplaceFunction]: https://github.com/Microsoft/MIMWAL/wiki/RegexReplace-Function
[RemoveDuplicatesFunction]: https://github.com/Microsoft/MIMWAL/wiki/RemoveDuplicates-Function
[RemoveValuesFunction]: https://github.com/Microsoft/MIMWAL/wiki/RemoveValues-Function
[ReplaceStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/ReplaceString-Function
[RightFunction]: https://github.com/Microsoft/MIMWAL/wiki/Right-Function
[RightPadFunction]: https://github.com/Microsoft/MIMWAL/wiki/RightPad-Function
[RTrimFunction]: https://github.com/Microsoft/MIMWAL/wiki/RTrim-Function
[SortListFunction]: https://github.com/Microsoft/MIMWAL/wiki/SortList-Function
[SplitStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/SplitString-Function
[SubtractFunction]: https://github.com/Microsoft/MIMWAL/wiki/Subtract-Function
[TitleCaseFunction]: https://github.com/Microsoft/MIMWAL/wiki/TitleCase-Function
[TrimFunction]: https://github.com/Microsoft/MIMWAL/wiki/Trim-Function
[UpperCaseFunction]: https://github.com/Microsoft/MIMWAL/wiki/UpperCase-Function
[ValueByIndexFunction]: https://github.com/Microsoft/MIMWAL/wiki/ValueByIndex-Function
[ValueByKeyFunction]: https://github.com/Microsoft/MIMWAL/wiki/ValueByKey-Function
[ValueTypeFunction]: https://github.com/Microsoft/MIMWAL/wiki/ValueType-Function
[WordFunction]: https://github.com/Microsoft/MIMWAL/wiki/Word-Function
[WrapXPathFilterFunction]: https://github.com/Microsoft/MIMWAL/wiki/WrapXPathFilter-Function
[MIMWalFunctionsTable]: https://github.com/Microsoft/MIMWAL/wiki/Functions-Table
[GenerateUniqueValueActivity]: https://github.com/Microsoft/MIMWAL/wiki/Generate-Unique-Value-Activity
