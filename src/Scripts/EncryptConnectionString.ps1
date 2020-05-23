<#
    This script demonstrates how to encrypt connection strings used by WAL ExecutSql* functions.
	
	If a connection string contains SQL user's password information, it's highly recommended that you do not leave them unencrypted in the app config file.

	For more information, see: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/connection-strings-and-configuration-files#encrypting-configuration-file-sections-using-protected-configuration

	NOTE: This script will need to to be run on each FIMService server.
#>

param (
	[string] $sectionName = "connectionStrings",
	[string] $dataProtectionProvider = "DataProtectionConfigurationProvider"
)

$Error.Clear()

#The System.Configuration assembly must be loaded
$configurationAssembly = "System.Configuration, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a"
[void] [Reflection.Assembly]::Load($configurationAssembly)

function TestIsAdministrator 
{ 
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent() 
    (New-Object Security.Principal.WindowsPrincipal $currentUser).IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator) 
}

if(!(TestIsAdministrator))  
{
    throw $("Admin rights are required to run this script.")
}

Write-Host "Encrypting configuration section: '$sectionName'.."

$appService = "FIMService"
$appPath = [string](Get-WmiObject -Query "Select * from Win32_Service Where Name='$appService'").PathName

if ($appPath -eq $null)
{
	Write-Error "Unable to find get application path for windows service '$appService'."
	return
}
else
{
    $appPath = $appPath.Trim('"')
}

Write-Host "The app config file path is: '$appPath'."

$appConfig = [System.Configuration.ConfigurationManager]::OpenExeConfiguration($appPath)
$section = $appConfig.GetSection($sectionName)

if (!$section.SectionInformation.IsProtected)
{
	$section.SectionInformation.ProtectSection($dataProtectionProvider);
	$section.SectionInformation.ForceSave = [System.Boolean]::True;
	$appConfig.Save([System.Configuration.ConfigurationSaveMode]::Modified);

    if ($Error.Count -eq 0)
    {
        Write-Host "Success!! Encrypted the config section '$sectionName' in the app config file '$appPath'."
    }
}
else
{
	Write-Host "The config section '$sectionName' in the app config file '!$appPath' is already encrypted."
}

if ($Error)
{
	Write-Host "There were errors executing the script."
}