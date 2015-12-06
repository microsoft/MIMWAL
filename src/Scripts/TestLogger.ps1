function Get-ExtendedLogProperties
{
    # WAL RunPowerShellScript activity will pass these AEC variables as session variables.
    $extendedLogProperties = "Extended Properties:`nRequestId:{0}`nActorId:{1}`nTargetId:{2}`nWorkflowDefinitionId:{3}" -f $AECRequestId, $AECActorId, $AECTargetId, $AECWorkflowDefinitionId

    return $extendedLogProperties
}

$Logger = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.Logger]::Instance #No need to Add-Type "MSWAL.DLL" as PS runtime is loaded in-process which already loaded MSWAL.DLL.

$extendedLogProperties = Get-ExtendedLogProperties

$warning = "This is a test warning message. $extendedLogProperties"
Write-Warning($warning)
$Logger.WriteWarning($warning)

$debug = "This is a test debug message. $extendedLogProperties" 
Write-Debug($debug)
$Logger.WriteInfo($debug)

$verbose = "This is a test verbose message. $extendedLogProperties"
Write-Verbose($verbose)
$Logger.WriteVerbose($verbose)

if ($credential -ne $null)
{
    Write-Warning ("Credential Information: Domain={0} UserName={1} Password={2}" -f $credential.GetNetworkCredential().Domain, $credential.GetNetworkCredential().UserName, $credential.GetNetworkCredential().Password)
}
else
{
    Write-Error "No credential information received."
}