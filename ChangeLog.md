## MIMWAL Change Log

All notable changes to MIMWAL project will be documented in this file. The "Unreleased" section at the top is for keeping track of important changes that might make it to upcoming releases.

### Version [Unreleased]

* Support for multi-valued attributes in `[//Effective]` lookup in AuthZ workflows.
* Implement Approve Request Activity.

### Version 2.17.0414.0

#### Added

* Support for executing SQL stored procedures and queries for SQL Server and ODBC Data Sources with the implementation of following new functions: 
	* New function [CreateSqlParameter][CreateSqlParameterFunction] 
	* New function [CreateSqlParameter2][CreateSqlParameter2Function]
	* New function [ExecuteSqlNonQuery][ExecuteSqlNonQueryFunction]
	* New function [ExecuteSqlScalar][ExecuteSqlScalarFunction]
	* New function [ValueByKey][ValueByKeyFunction]

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

[NormalizeStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/NormalizeString-Function
[RunPowerShellScriptActivity]: https://github.com/Microsoft/MIMWAL/wiki/Run-PowerShell-Script-Activity
[UpdateResourcesActivity]: https://github.com/Microsoft/MIMWAL/wiki/Update-Resources-Activity
[EvaluateExpressionFunction]: https://github.com/Microsoft/MIMWAL/wiki/EvaluateExpression-Function
[FormatMultivaluedListFunction]: https://github.com/Microsoft/MIMWAL/wiki/FormatMultivaluedList-Function
[ConvertToUniqueIdentifierFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertToUniqueIdentifier-Function
[ConvertToStringFunction]: https://github.com/Microsoft/MIMWAL/wiki/ConvertToString-Function
[CreateSqlParameterFunction]: https://github.com/Microsoft/MIMWAL/wiki/CreateSqlParameter-Function
[CreateSqlParameter2Function]: https://github.com/Microsoft/MIMWAL/wiki/CreateSqlParameter2-Function
[ExecuteSqlNonQueryFunction]: https://github.com/Microsoft/MIMWAL/wiki/ExecuteSqlNonQuery-Function
[ExecuteSqlScalarFunction]: https://github.com/Microsoft/MIMWAL/wiki/ExecuteSqlScalar-Function
[ValueByKeyFunction]: https://github.com/Microsoft/MIMWAL/wiki/ValueByKey-Function
