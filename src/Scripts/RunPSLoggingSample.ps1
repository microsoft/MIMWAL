<#
    This script will be passed following session variables automatically by the RunPowerShellScript Activity:

    Activity Execution Context:
        AECWorkflowInstanceId
        AECRequestId
        AECActorId
        AECTargetId
        AECWorkflowDefinitionId
    Tracing Settings
        ProgressPreference (when the Tracing Level is at Information)
        DebugPreference (when the Tracing Level is at Information)
        VerbosePreference (when the Tracing Level is at Verbose)
    RunAs User Credentials
        Credential (PSCredential object if configured or $null)
#>

param
(
    # script params go here..
)

#-------------------------------------------------------------------------------------------------------------------------------

Set-StrictMode -Version "2.0"

trap 
{ 
    Write-Error "Error: $($_.Exception.Message). $($_.InvocationInfo.PositionMessage). $extendedLogProperties"

    throw $_
}


function GetExtendedLogProperties
{
    $insideFIM = (Get-Variable -Name AECRequestId -ErrorAction SilentlyContinue) -ne $null

    $extendedLogProperties = "Extended Properties:`nScript Developement"
    if ($insideFIM)
    {
        $extendedLogProperties = @($AECRequestId, $AECActorId, $AECTargetId, $AECWorkflowDefinitionId, $AECWorkflowInstanceId)
        $extendedLogProperties = "Extended Properties:`nRequestId:{0}`nActorId:{1}`nTargetId:{2}`nWorkflowDefinitionId:{3}`nWorkflowInstanceId:{4}" -f $extendedLogProperties
    }

    return $extendedLogProperties
}

$extendedLogProperties = GetExtendedLogProperties

function WriteError ([string] $Message)
{
    Write-Error "$Message`n$extendedLogProperties"
}

function WriteWarning ([string] $Message)
{
    Write-Warning "$Message`n$extendedLogProperties"
}

function WriteDebug ([string] $Message)
{
    Write-Debug "$Message`n$extendedLogProperties"
}

function WriteVerbose ([string] $Message)
{
    Write-Verbose "$Message`n$extendedLogProperties"
}

#-------------------------------------------------------------------------------------------------------------------------------

$warning = "This is a test warning message."
WriteWarning($warning)

$debug = "This is a test debug message." 
WriteDebug($debug)

$verbose = "This is a test verbose message."
WriteVerbose($verbose)

if ($credential -ne $null)
{
    WriteWarning ("Credential Information: Domain={0} UserName={1} Password={2}" -f $credential.GetNetworkCredential().Domain, $credential.GetNetworkCredential().UserName, $credential.GetNetworkCredential().Password)
}
else
{
    WriteError "No credential information received."
}