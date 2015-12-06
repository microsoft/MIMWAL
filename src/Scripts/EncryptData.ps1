<#
    This script demonstrates how the credentials can be encrypted for the RunAs user in the RunPowerShellScript Activity.

    If the password is encrypted using certificate based encryption, please make sure that you deploy that certificate on the the FIMService nodes. It can be an auto-genenrated cert.
    If the password is encrypted using DPAPI, ensure that you export and import the RSA machine keys. Follow the steps documented for web farm scenario mentioned in the article
    at http://msdn.microsoft.com/en-us/library/ms998283.aspx (How To: Encrypt Configuration Sections in ASP.NET 2.0 Using RSA), but for the FIM Service account.
#>

Add-Type -AssemblyName "System.Security"
Add-Type -AssemblyName "MicrosoftServices.IdentityManagement.WorkflowActivityLibrary, Version=2.14.611.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"


function TestCertificateBasedEncryptionDecryption
{
    $thumbprint = "9C697919FB2FB2D6324ADE42D5F8CB49E8778C08" # cert to be used for encryption.

    $base64EncodedPublicKeyXml = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ProtectedData]::GetCertificatePublicKeyXml($thumbprint, "My", "LocalMachine")

    $secretToEncrypt = "Pass@word1"

    $secret = ConvertTo-SecureString -AsPlainText $secretToEncrypt -Force

    $encryptedData = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ProtectedData]::EncryptData($secret, $base64EncodedPublicKeyXml)

    $encryptedData | Out-File cert-p.txt

    $encryptedDataConfig = "cert:\localmachine\my\$thumbprint,$encryptedData"

    $decryptedData = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ProtectedData]::DecryptData($encryptedDataConfig)

    $plainText = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ProtectedData]::ConvertToUnsecureString($decryptedData)

    if ($plainText -eq $secretToEncrypt)
    {
        Write-Host "`nEncryption and Decryption test using certificate '$thumbprint' succeeded!`n"
    }
    else
    {
        Write-Error "`nEncryption and Decryption test using certificate '$thumbprint' failed!`n"
    }
}

function TestDPAPIBasedEncryptionDecryption
{
    $secretToEncrypt = "Pass@word1"

    $secret = ConvertTo-SecureString -AsPlainText $secretToEncrypt -Force

    $encryptedData = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ProtectedData]::EncryptData($secret, [System.Security.Cryptography.DataProtectionScope]::LocalMachine)

    $encryptedData | Out-File dpapi-p.txt

    $decryptedData = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ProtectedData]::DecryptData($encryptedData, [System.Security.Cryptography.DataProtectionScope]::LocalMachine)

    $plainText = [MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ProtectedData]::ConvertToUnsecureString($decryptedData)

    if ($plainText -eq $secretToEncrypt)
    {
        Write-Host "`nEncryption and Decryption test using DPAPI succeeded!`n"
    }
    else
    {
        Write-Error "`nEncryption and Decryption test using DPAPI failed!`n"
    }
}

TestDPAPIBasedEncryptionDecryption

#TestCertificateBasedEncryptionDecryption