@echo off
setlocal
pushd "%~dp0"

set walAssembly="%~dp0\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.dll"
set walUIAssembly="%~dp0\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.dll"
set snkFile="%~dp0..\WAL.snk"
set snExe="%~dp0sn.exe"

if not exist %walAssembly% echo ERROR: File '%walAssembly%' Not Found. You need to compile WAL solution first! & (goto exit_error)
if not exist %walUIAssembly% echo ERROR: File '%walUIAssembly%' Not Found. You need to compile WAL solution first! & (goto exit_error)
if not exist %snkFile% echo ERROR: File '%snkFile%' Not Found. You need to specify correct path to your strong name file! & (goto exit_error)

%snExe% -Ra %walAssembly% %snkFile%
%snExe% -Ra %walUIAssembly% %snkFile%

goto end_of_file

:exit_error
echo Aborting script execution...

:end_of_file
popd
endlocal
@pause
