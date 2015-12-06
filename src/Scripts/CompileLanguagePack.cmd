@echo off
setlocal
pushd "%~dp0"

set walAssembly="%~dp0..\SolutionOutput\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.dll"
set walUIAssembly="%~dp0..\SolutionOutput\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.dll"
set activitySettingsResx="%~dp0..\LanguagePacks\ActivitySettings.*.resx"

if not exist %walAssembly% echo ERROR: File '%walAssembly%' Not Found. You need to compile WAL solution first! & (goto exit_error)
if not exist %walUIAssembly% echo ERROR: File '%walUIAssembly%' Not Found. You need to compile WAL solution first! & (goto exit_error)
if not exist %activitySettingsResx% echo ERROR: File '%activitySettingsResx%' Not Found. No language packs to compile! & (goto exit_error)

:: Compile resource files ...
for %%a in (%activitySettingsResx%) do (call :CompileResources "%%a")

@pause
goto end_of_file

:CompileResources

echo Processing '%1'

set file=%~nx1
set dir=%~p1
set lang=%file:~17,-5%
set messagesResx="%dir%Messages.%lang%.resx"

:: Check messages resource file exist ...
if not exist %messagesResx% echo ERROR: Unable to find corresponding %messagesResx% file! & (goto exit_error)

echo Compiling Resources %lang% ...
"%~dp0resgen.exe" "%dir%ActivitySettings.%lang%.resx"
if not "%errorlevel%"=="0" echo ERROR: resgen.exe returned %errorlevel% error! & (goto exit_error)
"%~dp0resgen.exe" "%dir%Messages.%lang%.resx"
if not "%errorlevel%"=="0" echo ERROR: resgen.exe returned %errorlevel% error! & (goto exit_error)

if not exist "..\SolutionOutput\Localization\%lang%\" md "..\SolutionOutput\Localization\%lang%\"

"%~dp0al.exe" /t:lib /embed:"%dir%ActivitySettings.%lang%.resources",MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.ActivitySettings.%lang%.resources /culture:%lang% /out:"..\SolutionOutput\Localization\%lang%\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.resources.dll" /template:"..\SolutionOutput\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.dll" /keyfile:"..\WAL.snk"
if not "%errorlevel%"=="0" echo ERROR: al.exe returned %errorlevel% error! & (goto exit_error)
"%~dp0al.exe" /t:lib /embed:"%dir%Messages.%lang%.resources",MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Messages.%lang%.resources /culture:%lang% /out:"..\SolutionOutput\Localization\%lang%\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.resources.dll" /template:"..\SolutionOutput\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.dll" /keyfile:"..\WAL.snk"
if not "%errorlevel%"=="0" echo ERROR: al.exe returned %errorlevel% error! & (goto exit_error)
del "%dir%ActivitySettings.%lang%.resources" >nul
del "%dir%Messages.%lang%.resources" >nul

goto end_of_file

:exit_error
echo Aborting script execution...

@pause

:end_of_file
popd
endlocal
