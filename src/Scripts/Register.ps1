<#
    This script is used to register WAL assemblies on the FIM / MIM Service and Portal servers.

    This script installs the WAL assemblies in GAC.
    It also creates or updates the Activity Information Configurations with the latest version of WAL assemblies.
    In addition, it also configures the app.config and web.config with the assembly binding redirects and a diagnostics source for WAL.

    To avoid reusing the previous versions of the assemblies that might be already loaded in the PowerShell session,
    you are required start on a fresh PowerShell prompt.

	On the latest version of the WAL is deployed, you are strongly recommended to update the assembly version in workflow XOMLs
	(by using UpdateWorkflowXoml.ps1 script). Once all the FIM Service requests are processed, you can then optionally unregister
	older version from GAC.
#>

param (
	[string] $PortalSiteName = $(throw "Parameter PortalSiteName is mandatory. Please specify the name of the MIM / FIM Portal site in IIS.")
)

Set-StrictMode -version 2.0

$Error.Clear()

if (@(Get-PSSnapin | Where-Object { $_.Name -eq "FIMAutomation" }).Count -eq 0)
{
    Add-PSSnapin "FIMAutomation" -ErrorAction Stop
}

$DebugPreference = "Continue"
$VerbosePreference = "Continue"
$ProgressPreference = "SilentlyContinue"

$walAssemblyName = "MicrosoftServices.IdentityManagement.WorkflowActivityLibrary"
$walAssemblyPath = Join-Path $PWD -ChildPath "$walAssemblyName.dll"
$walUIAssemblyPath = Join-Path $PWD -ChildPath "$walAssemblyName.UI.dll"
$gacUtilExePath = Join-Path $PWD -ChildPath "gacutil.exe"
$snExePath = Join-Path $PWD -ChildPath "sn.exe"

function TestIsAssemblyLoaded
{
    param([string]$assemblyName)

    foreach ($asm in [AppDomain]::CurrentDomain.GetAssemblies())
    {
        if ($asm.GetName().Name -eq $assemblyName)
        {
            return $true
        }
    }

    return $false
 }
 
function TestIsAdministrator 
{ 
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent() 
    (New-Object Security.Principal.WindowsPrincipal $currentUser).IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator) 
}

function RegisterAssembly
{
    param
    (
        [string] $assemblyName,
        [switch] $skipVerification
    )

    Write-Debug "Registering assembly in GAC: '$assemblyName'"
    $executionStatus = Invoke-Expression -Command "& '$gacUtilExePath' /i '$assemblyName'" | Out-String
    Write-Debug $executionStatus

    if ($executionStatus -match "Fail" -or $executionStatus -match "Error")
    {
        if (-not $skipVerification)
        {
            throw ("Error Registering assembly in GAC: '$assemblyName' " )
        }

        Write-Debug "Registering assembly to skip strong name verification: '$assemblyName'"
        $executionStatus = Invoke-Expression -Command "& '$snExePath' -Vr '$assemblyName'" | Out-String
        Write-Debug $executionStatus

        if ($executionStatus -match "Fail" -or $executionStatus -match "Error")
        {
            throw ("Error Registering assembly to skip strong name verification: '$assemblyName' " )
        }
    }
 }

function RegisterActivity
{
    param($displayName, $description, $activityName, $typeName, $assemblyName, $authenticationActivity, $authorizationActivity, $actionActivity)
    end
    {
		$fimwalPrefix = "FIMWAL2: "
		$mimwalPrefix = "WAL: "
        $existing = Export-FIMConfig –OnlyBaseResources -CustomConfig ("/ActivityInformationConfiguration[DisplayName = '$mimwalPrefix$displayName']", "/ActivityInformationConfiguration[DisplayName = '$fimwalPrefix$displayName']")
        if($existing -eq $Null)
        {
            $aic = New-Object Microsoft.ResourceManagement.Automation.ObjectModel.ImportObject
            $aic.ObjectType = "ActivityInformationConfiguration"
            $aic.SourceObjectIdentifier = [System.Guid]::NewGuid().ToString()
            
            SetAttribute -object $aic -attributeName "DisplayName" -attributeValue "$mimwalPrefix$displayName"
            SetAttribute -object $aic -attributeName "Description" -attributeValue $description
            SetAttribute -object $aic -attributeName "ActivityName" -attributeValue $activityName
            SetAttribute -object $aic -attributeName "TypeName" -attributeValue $typeName
            SetAttribute -object $aic -attributeName "AssemblyName" -attributeValue $assemblyName
            SetAttribute -object $aic -attributeName "IsAuthenticationActivity" -attributeValue $authenticationActivity
            SetAttribute -object $aic -attributeName "IsAuthorizationActivity" -attributeValue $authorizationActivity
            SetAttribute -object $aic -attributeName "IsActionActivity" -attributeValue $actionActivity
            SetAttribute -object $aic -attributeName "IsConfigurationType" -attributeValue $true

            $aic | Import-FIMConfig
        }
        else
        {
            $existingAssemblyName = $existing.ResourceManagementObject.ResourceManagementAttributes | Where-Object { $_.AttributeName -eq "AssemblyName" }
            $existingDisplayName = $existing.ResourceManagementObject.ResourceManagementAttributes | Where-Object { $_.AttributeName -eq "DisplayName" }

            if ($existingAssemblyName.Value -ne $assemblyName)
            {
				if ($existingDisplayName.Value.ToUpper().StartsWith("FIMWAL2"))
				{
					Write-Warning "AIC '$fimwalPrefix$displayName' already exisits, but the assembly name does not match. The AIC will be updated for the assembly name."
				}
				else
				{
					Write-Warning "AIC '$mimwalPrefix$displayName' already exisits, but the assembly name does not match. The AIC will be updated for the assembly name."
				}

                $aic = New-Object Microsoft.ResourceManagement.Automation.ObjectModel.ImportObject
                $aic.ObjectType = "ActivityInformationConfiguration"
                $aic.TargetObjectIdentifier = $existing.ResourceManagementObject.ObjectIdentifier
                $aic.SourceObjectIdentifier = $existing.ResourceManagementObject.ObjectIdentifier
                $aic.State = 1

                SetAttribute -object $aic -attributeName "AssemblyName" -attributeValue $assemblyName
				SetAttribute -object $aic -attributeName "DisplayName" -attributeValue "$mimwalPrefix$displayName"
                $aic | Import-FIMConfig
            }
            else
            {
                Write-Debug "AIC '$displayName' already exists and the assembly name matches. No updates will be made for this AIC."
            }
        }
    }
}

function SetAttribute
{
    param($object, $attributeName, $attributeValue)
    end
    {
        $importChange = New-Object Microsoft.ResourceManagement.Automation.ObjectModel.ImportChange
        $importChange.Operation = 1
        $importChange.AttributeName = $attributeName
        $importChange.AttributeValue = $attributeValue
        $importChange.FullyResolved = 1
        $importChange.Locale = "Invariant"

        if ($object.Changes -eq $null)
        {
            $object.Changes = (,$importChange)
        }
        else
        {
            $object.Changes += $importChange
        }
    }
}

function UpdateAssemblyBindings
{
    param
    (
        [Parameter(Mandatory = $true,
                   ValueFromPipelineByPropertyName = $true,
                   Position = 0)]
        [ValidateSet("App","Web")]
        $ConfigType
    )
    
    if ($ConfigType -eq "Web")
    {
        try
        {
            Import-Module "WebAdministration"
            
            $portalSite = Get-WebSite | Where-Object { $_.Name -eq $PortalSiteName }
            
            if ($portalSite -eq $null)
            {
                Write-Warning "Error updating FIM / MIM Portal web.config. WebSite '$portalSiteName' is not found. You can safely ignore this warning if the FIM / MIM Portal is not installed on this server."
                return
            }
            
            $appConfigPath = Join-Path $portalSite.PhysicalPath -ChildPath "web.config"
        }
        catch
        {
            Write-Warning "Error updating FIM / MIM Portal web.config. Error $_ You can safely ignore this error if IIS is not installed on this server."
            
            $Error.Clear()
            
            return
        }
    }
    else
    {
        $keyName = "HKLM:\SYSTEM\CurrentControlSet\Services\FIMService"
        $valueName = "ImagePath"
        $imagePath = Get-ItemProperty -Path $keyName -Name $valueName
        
        if ($imagePath -eq $null)
        {
            Write-Warning "FIMService is not installed on the current machine. Please update the assembly binding manually in the FIMService app.config file."
            return
        }

        $appConfigPath = $imagePath.($valueName).Trim("`"") + ".config"
    }


    Write-Debug "Updating Assembly Bindings in the config file: $appConfigPath"

    $appConfig = [xml] (Get-Content -Path $appConfigPath)

    $assemblyBindingNode = $appConfig.SelectSingleNode("/configuration/runtime").assemblyBinding

    $assemblies = @($walAssembly.FullName, $walUIAssembly.FullName)
    $assemblies | foreach {
        $assemblyInfo = $_ -Split ","
        
        $assemblyName = $assemblyInfo[0].Trim()
        $assemblyVersion = ($assemblyInfo[1].Trim() -Split "Version=")[1].Trim()
        $assemblyCulture = ($assemblyInfo[2].Trim() -Split "Culture=")[1].Trim()
        $assemblyPublicKeyToken = ($assemblyInfo[3].Trim() -Split "PublicKeyToken=")[1].Trim()

        $dependentAssemblyNode = $assemblyBindingNode.dependentAssembly | Where-Object { $_.assemblyIdentity.Name -eq $assemblyName }

        if ($dependentAssemblyNode -eq $null)
        {
            $dependentAssemblyNode = $assemblyBindingNode.dependentAssembly[0].CloneNode($true)
            [void] $assemblyBindingNode.AppendChild($dependentAssemblyNode)
        }

        $dependentAssemblyNode.assemblyIdentity.Name = $assemblyName
        $dependentAssemblyNode.assemblyIdentity.publicKeyToken = $assemblyPublicKeyToken
        $dependentAssemblyNode.assemblyIdentity.culture = $assemblyCulture

        $dependentAssemblyNode.bindingRedirect.oldVersion = ("{0}.0.0.0-{0}.65535.65535.65535" -f $assemblyVersion.Substring(0,1))
        $dependentAssemblyNode.bindingRedirect.newVersion = $assemblyVersion
    }

    $appConfig.Save($appConfigPath)
    
    Write-Debug "Updated Assembly Bindings in the config file: $appConfigPath"

    Write-Debug "Configuring WAL event source in the config file: $appConfigPath"

    $diagnosticsSourceName = "MicrosoftServices.IdentityManagement.WorkflowActivityLibrary"
    $diagnosticsSourceXml = @'
          <source name="{0}" switchValue="Warning">
      <listeners>
        <add initializeData="{0}" type="System.Diagnostics.EventLogTraceListener, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" name="WALEventLogListener" traceOutputOptions="LogicalOperationStack, DateTime, Timestamp, Callstack">
          <filter type="" />
        </add>
      </listeners>
    </source>
'@ -f $diagnosticsSourceName

    $diagnosticsSourcesNode = $appConfig.SelectSingleNode('//system.diagnostics/sources')
    
    if ($diagnosticsSourcesNode)
    {
        $diagnosticsSourceXml = "`r`n" + $diagnosticsSourceXml + "`r`n "
        
        $xmlFragment = $appConfig.CreateDocumentFragment()
        $xmlFragment.InnerXML = $diagnosticsSourceXml

        $walSourceNode = $diagnosticsSourcesNode.source | Where-Object { $_.Name -eq $diagnosticsSourceName }

        if ($walSourceNode -eq $null)
        {
            [void] $diagnosticsSourcesNode.AppendChild($xmlFragment)
        }
        
        $appConfig.Save($appConfigPath)

        Write-Debug "Configured WAL event logging in the config file: $appConfigPath"
    }
    elseif ($appConfig.SelectSingleNode('//system.diagnostics') -eq $null)
    {
        $diagnosticsSourceXml = @'
  <system.diagnostics>
    <sources>
{0}
    </sources>
  </system.diagnostics>
'@ -f $diagnosticsSourceXml

        $diagnosticsSourceXml = "`r`n" + $diagnosticsSourceXml + "`r`n "
    
        $xmlFragment = $appConfig.CreateDocumentFragment()
        $xmlFragment.InnerXML = $diagnosticsSourceXml

        [void] $appConfig.configuration.AppendChild($xmlFragment)
        
        $appConfig.Save($appConfigPath)

        Write-Debug "Configured WAL event logging in the config file: $appConfigPath"
    }
}

function RegisterEventSource
{
    $eventSources = @{
        "MicrosoftServices.IdentityManagement.WorkflowActivityLibrary" = "WAL"
    }

	# Delete deprecated FIMWAL event log. 
	Remove-EventLog -LogName "FIMWAL" -ErrorAction SilentlyContinue

    foreach($source in $eventSources.Keys)
    {
        $logName = $eventSources[$source]
    
        Write-Host "Creating event source $source in event log $logName"
        
        if ([System.Diagnostics.EventLog]::SourceExists($source) -eq $false) {
            New-EventLog -Source $source -LogName $logName
            Write-Host -ForegroundColor green "Event source $source created in event log $logName"
        }
        else
        {
            $eventLog = Get-EventLog -List | Where-Object {$_.Log -eq $logName}

            if ($eventLog -ne $null)
            {
                Write-Host -ForegroundColor yellow "Warning: Event source $source already exists in event log $logName"
            }
            else
            {
                Write-Host -ForegroundColor yellow "Warning: Event source $source already exists, but not in event log $logName. It will be deleted and recreated."
                [System.Diagnostics.EventLog]::DeleteEventSource($source)
                New-EventLog -Source $source -LogName $logName
            }
        }

        Limit-EventLog -LogName $logName -MaximumSize 20480KB

        Write-Host -ForegroundColor green "Writing a test event in the event log '$logName'"

        [System.Diagnostics.EventLog]::WriteEntry($logName, "Test Event")
    }
}

if(!(TestIsAdministrator))  
{
    throw $("Admin rights are required to run this script.")
}

if (!(Test-Path $walAssemblyPath))
{
    throw ("Could not find file: {0}" -f $walAssemblyPath)
}

if (!(Test-Path $walUIAssemblyPath))
{
    throw ("Could not find file: {0}" -f $walUIAssemblyPath)
}

if (TestIsAssemblyLoaded $walAssemblyName)
{
    throw "The WAL assemblies were previously loaded in this PowerShell session. Please run the script on a fresh new command-line."
}

RegisterAssembly $walAssemblyPath
RegisterAssembly $walUIAssemblyPath

try
{
    Write-Debug "Loading Assembly: $walAssemblyPath"
        
    $walAssembly = [Reflection.Assembly]::LoadFile($walAssemblyPath)
}
catch
{
    throw ("Could not load assembly: {0}. Error: {1}" -f $walAssemblyPath, $_)
}

try
{
    Write-Debug "Loading Assembly: $walUIAssemblyPath"
        
    $walUIAssembly = [Reflection.Assembly]::LoadFile($walUIAssemblyPath)
}
catch
{
    throw ("Could not load assembly: {0}. Error: {1}" -f $walUIAssemblyPath, $_)
}

UpdateAssemblyBindings -ConfigType "App"
UpdateAssemblyBindings -ConfigType "Web"

$assemblyName = $walUIAssembly.FullName

Write-Debug ("AIC's will attempted to be created using the assembly full name: {0}" -f $assemblyName)

$activitiesNamespace = "MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Activities"
$formsNamespace = "MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.UI.Forms"

RegisterActivity -displayName "Update Resources" -description "Update multiple attributes on one or more resources." -activityName "$activitiesNamespace.UpdateResources" -typeName "$formsNamespace.UpdateResourcesForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $true -actionActivity $true
RegisterActivity -displayName "Create Resource" -description "Create a new resource and assign attribute values." -activityName "$activitiesNamespace.CreateResource" -typeName "$formsNamespace.CreateResourceForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $false -actionActivity $true
RegisterActivity -displayName "Delete Resources" -description "Delete one or more resources." -activityName "$activitiesNamespace.DeleteResources" -typeName "$formsNamespace.DeleteResourcesForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $false -actionActivity $true
RegisterActivity -displayName "Generate Unique Value" -description "Generate a unique string value based on defined criteria." -activityName "$activitiesNamespace.GenerateUniqueValue" -typeName "$formsNamespace.GenerateUniqueValueForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $false -actionActivity $true
RegisterActivity -displayName "Run PowerShell Script" -description "Run a PowerShell script from workflow." -activityName "$activitiesNamespace.RunPowerShellScript" -typeName "$formsNamespace.RunPowerShellScriptForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $true -actionActivity $true
RegisterActivity -displayName "Verify Request" -description "Enforce a defined set of conditions for a request and return a customizable denial message when a condition is not satisfied." -activityName "$activitiesNamespace.VerifyRequest" -typeName "$formsNamespace.VerifyRequestForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $true -actionActivity $false
RegisterActivity -displayName "Add Delay" -description "Add a delay / wait to the workflow execution." -activityName "$activitiesNamespace.AddDelay" -typeName "$formsNamespace.AddDelayForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $true -actionActivity $true
RegisterActivity -displayName "Send Email Notification" -description "Send Email Notification" -activityName "$activitiesNamespace.SendEmailNotification" -typeName "$formsNamespace.SendEmailNotificationForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $true -actionActivity $true
RegisterActivity -displayName "Request Approval" -description "Seek approval from specific approvers by mail." -activityName "$activitiesNamespace.RequestApproval" -typeName "$formsNamespace.RequestApprovalForm" -assemblyName $assemblyName -authenticationActivity $false -authorizationActivity $true -actionActivity $false

RegisterEventSource

net stop FIMService
net start FIMService

iisreset

Write-Host -ForegroundColor green "Review script console output for any errors. Once the deployment is successful on all the servers, update the assembly version in MIMWAL XOMLs by executing UpdateWorkflowXoml.ps1 script."
