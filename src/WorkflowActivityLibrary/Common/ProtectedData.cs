//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtectedData.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ProtectedData class 
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System;
    using System.Configuration;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

    #endregion

    /// <summary>
    /// Crypto functions
    /// </summary>
    public static class ProtectedData
    {
        /// <summary>
        /// Searches the certificate store of the current user and returns the matching certificate.
        /// </summary>
        /// <param name="thumbprint">Thumbprint of the certificate</param>
        /// <param name="storeName">Name of the certificate store</param>
        /// <param name="storeLocation">Location of the certificate store</param>
        /// <returns>The matching certificate</returns>
        public static X509Certificate2 GetCertificate(string thumbprint, StoreName storeName, StoreLocation storeLocation)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataGetCertificate, "Thumbprint: {0}.", thumbprint);

            try
            {
                if (string.IsNullOrEmpty(thumbprint))
                {
                    throw new ArgumentNullException("thumbprint");
                }

                X509Store store = new X509Store(storeName, storeLocation);
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2 certificate = store.Certificates.Cast<X509Certificate2>().FirstOrDefault(cert => cert.Thumbprint.Equals(thumbprint, StringComparison.OrdinalIgnoreCase));

                if (certificate == null)
                {
                    Logger.Instance.ReportError(new Exceptions.CryptographicException(Messages.ProtectedData_EncryptionCertificateNotFoundError, thumbprint));
                }

                return certificate;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataGetCertificate, "Thumbprint: {0}.", thumbprint);
            }
        }

        /// <summary>
        /// Searches the certificate store of the current user and returns the matching certificate's public key xml
        /// </summary>
        /// <param name="thumbprint">Thumbprint of the certificate</param>
        /// <param name="storeName">Name of the certificate store</param>
        /// <param name="storeLocation">Location of the certificate store</param>
        /// <returns>The base64 encoded public key xml string</returns>
        public static string GetCertificatePublicKeyXml(string thumbprint, StoreName storeName, StoreLocation storeLocation)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataGetCertificatePublicKeyXml, "Thumbprint: {0}.", thumbprint);

            try
            {
                if (string.IsNullOrEmpty(thumbprint))
                {
                    throw new ArgumentNullException("thumbprint");
                }

                X509Certificate2 certificate = GetCertificate(thumbprint, storeName, storeLocation);

                string publicKeyXml = certificate.PublicKey.Key.ToXmlString(false);

                return Convert.ToBase64String(Encoding.Unicode.GetBytes(publicKeyXml));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataGetCertificatePublicKeyXml, "Thumbprint: {0}.", thumbprint);
            }
        }

        /// <summary>
        /// Encrypts the secret and returns a byte array
        /// </summary>
        /// <param name="secret">Secret to encrypt</param>
        /// <param name="scope">One of the DataProtectionScope values</param>
        /// <returns>The base64 encoded encrypted data</returns>
        public static string EncryptData(SecureString secret, DataProtectionScope scope)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataEncryptData, "Scope: {0}.", scope);

            try
            {
                if (secret == null)
                {
                    throw new ArgumentNullException("secret");
                }

                byte[] dataToEncrypt = Encoding.Unicode.GetBytes(ConvertToUnsecureString(secret));

                return Convert.ToBase64String(System.Security.Cryptography.ProtectedData.Protect(dataToEncrypt, null, scope));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataEncryptData, "Scope: {0}.", scope);
            }
        }

        /// <summary>
        /// Encrypts the secret data using the public key of a X.509 certificate.
        /// </summary>
        /// <param name="secret">Sensitive data</param>
        /// <param name="base64EncodedPublicKeyXml">Base64 encoded RSA public key xml for encrypting the data</param>
        /// <returns>The encrypted data</returns>
        public static string EncryptData(SecureString secret, string base64EncodedPublicKeyXml)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataEncryptData, "PublicKeyXml: {0}.", base64EncodedPublicKeyXml);

            try
            {
                if (secret == null)
                {
                    throw new ArgumentNullException("secret");
                }

                if (string.IsNullOrEmpty(base64EncodedPublicKeyXml))
                {
                    throw new ArgumentNullException("base64EncodedPublicKeyXml");
                }

                string base64EncodedEncryptedData = null;

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    byte[] publicKeyBytes = Convert.FromBase64String(base64EncodedPublicKeyXml);
                    string publicKeyXml = Encoding.Unicode.GetString(publicKeyBytes);
                    rsa.FromXmlString(publicKeyXml);

                    byte[] encryptedData = rsa.Encrypt(Encoding.Unicode.GetBytes(ConvertToUnsecureString(secret)), true);
                    base64EncodedEncryptedData = Convert.ToBase64String(encryptedData);
                }

                return base64EncodedEncryptedData;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataEncryptData, "PublicKeyXml: {0}.", base64EncodedPublicKeyXml);
            }
        }

        /// <summary>
        /// Encrypts the secret data using the public key of a X.509 certificate.
        /// </summary>
        /// <param name="secret">Sensitive data</param>
        /// <param name="certificate">Certificate to be use for encrypting the data</param>
        /// <returns>The encrypted data</returns>
        public static string EncryptData(SecureString secret, X509Certificate2 certificate)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataEncryptData, "Certificate: {0}.", certificate);

            try
            {
                if (secret == null)
                {
                    throw new ArgumentNullException("secret");
                }

                if (certificate == null)
                {
                    throw new ArgumentNullException("certificate");
                }

                string base64EncodedEncryptedData = null;

                using (RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key)
                {
                    byte[] encryptedData = rsa.Encrypt(Encoding.Unicode.GetBytes(ConvertToUnsecureString(secret)), true);

                    base64EncodedEncryptedData = Convert.ToBase64String(encryptedData);
                }

                return base64EncodedEncryptedData;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataEncryptData, "Certificate: {0}.", certificate);
            }
        }

        /// <summary>
        /// Decrypts the data using DPAPI
        /// </summary>
        /// <param name="base64EncodedEncryptedData">Encrypted data</param>
        /// <param name="scope">One of the DataProtectionScope values</param>
        /// <returns>Decrypted Value</returns>
        public static SecureString DecryptData(string base64EncodedEncryptedData, DataProtectionScope scope)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataDecryptData, "EncryptedData: {0}. Scope: {1}.", base64EncodedEncryptedData, scope);

            try
            {
                if (string.IsNullOrEmpty(base64EncodedEncryptedData))
                {
                    throw new ArgumentNullException("base64EncodedEncryptedData");
                }

                byte[] encryptedData = Convert.FromBase64String(base64EncodedEncryptedData);

                return ConvertToSecureString(Encoding.Unicode.GetString(System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, scope)));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataDecryptData, "EncryptedData: {0}. Scope: {1}.", base64EncodedEncryptedData, scope);
            }
        }

        /// <summary>
        /// Decrypts the encrypted data using the private key of the X.509 certificate.
        /// </summary>
        /// <param name="base64EncodedEncryptedData">Encrypted Data</param>
        /// <param name="certificate">Certificate used for encrypting the data</param>
        /// <returns>The decrypted data</returns>
        public static SecureString DecryptData(string base64EncodedEncryptedData, X509Certificate2 certificate)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataDecryptData, "EncryptedData: {0}. Certificate: {0}.", base64EncodedEncryptedData, certificate);

            try
            {
                if (string.IsNullOrEmpty(base64EncodedEncryptedData))
                {
                    throw new ArgumentNullException("base64EncodedEncryptedData");
                }

                if (certificate == null)
                {
                    throw new ArgumentNullException("certificate");
                }

                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PrivateKey;

                byte[] secret = rsa.Decrypt(Convert.FromBase64String(base64EncodedEncryptedData), true);

                return ConvertToSecureString(Encoding.Unicode.GetString(secret));
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataDecryptData, "EncryptedData: {0}. Certificate: {0}.", base64EncodedEncryptedData, certificate);
            }
        }

        /// <summary>
        /// Decrypts the encrypted data using the information provided in the encryptedDataConfig configuration.
        /// For app configuration, expected format is: app:\appSettings\[key],[LocalMachine|CurrentUser]
        /// For certificate based configuration, expected format is: cert:\[LocalMachine|CurrentUser]\my\[thumbprint],base64EncodedEncryptedData
        /// NOT Implemented: For registry based configuration, expected format is: hkcu:\software\Microsoft Services\Identity Management\[valueKey],[user|machine]
        /// The default is: base64EncodedEncryptedData using RSA machine keys.
        /// </summary>
        /// <param name="encryptedDataConfig">Configuration describing the encrypted data</param>
        /// <returns>The decrypted data</returns>
        public static SecureString DecryptData(string encryptedDataConfig)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataDecryptData, "EncryptedDataConfig: {0}.", encryptedDataConfig);

            try
            {
                if (string.IsNullOrEmpty(encryptedDataConfig))
                {
                    throw new ArgumentNullException("encryptedDataConfig");
                }

                if (encryptedDataConfig.StartsWith(@"cert:\", StringComparison.OrdinalIgnoreCase))
                {
                    return DecryptDataUsingCertificate(encryptedDataConfig);
                }

                if (encryptedDataConfig.StartsWith(@"app:\", StringComparison.OrdinalIgnoreCase))
                {
                    // expected input format is: app:\appSettings\[key],[LocalMachine|CurrentUser]
                    int firstPathSeparator = encryptedDataConfig.IndexOf(@"\", StringComparison.OrdinalIgnoreCase);
                    int secondPathSeparator = encryptedDataConfig.IndexOf(@"\", firstPathSeparator + 1, StringComparison.OrdinalIgnoreCase);
                    int keySeparator = encryptedDataConfig.IndexOf(@",", secondPathSeparator + 1, StringComparison.OrdinalIgnoreCase);
                    int scopeSeparator = encryptedDataConfig.IndexOf(@",", keySeparator + 1, StringComparison.OrdinalIgnoreCase);

                    string appKey = encryptedDataConfig.Substring(secondPathSeparator + 1, keySeparator - secondPathSeparator - 1);
                    string base64EncodedEncryptedData = ConfigurationManager.AppSettings[appKey];

                    string scope = scopeSeparator == -1
                        ? "LocalMachine"
                        : encryptedDataConfig.Substring(scopeSeparator + 1);

                    DataProtectionScope dataProtectionScope = (DataProtectionScope)Enum.Parse(typeof(DataProtectionScope), scope, true);

                    return DecryptData(base64EncodedEncryptedData, dataProtectionScope);
                }

                if (encryptedDataConfig.StartsWith(@"HKCU:\", StringComparison.OrdinalIgnoreCase))
                {
                    // expected input format is: hkcu:\software\Microsoft Services\Identity Management\[valueKey],base64EncodedEncryptedData
                    throw new ArgumentException("Unable to parse encryptedDataConfig.");
                }

                if (encryptedDataConfig.Contains("LocalMachine"))
                {
                    return DecryptData(encryptedDataConfig.Split(new string[] { "LocalMachine" }, StringSplitOptions.None)[0].Trim(), DataProtectionScope.LocalMachine);
                }

                if (encryptedDataConfig.Contains("CurrentUser"))
                {
                    return DecryptData(encryptedDataConfig.Split(new string[] { "CurrentUser" }, StringSplitOptions.None)[0].Trim(), DataProtectionScope.CurrentUser);
                }

                // default is base64EncodedEncryptedData using RSA machine keys
                return DecryptData(encryptedDataConfig, DataProtectionScope.LocalMachine);
            }
            catch (WorkflowActivityLibraryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exceptions.CryptographicException(e.Message, e);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataDecryptData, "EncryptedDataConfig: {0}.", encryptedDataConfig);
            }
        }

        /// <summary>
        /// Converts the clear text secret to a SecureString
        /// </summary>
        /// <param name="secret">clear text sensitive data</param>
        /// <returns>Returns a SecureString object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "False warning. SecurePassword is returned and must not be disposed.")]
        public static SecureString ConvertToSecureString(string secret)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataConvertToSecureString);

            try
            {
                if (string.IsNullOrEmpty(secret))
                {
                    throw new ArgumentNullException("secret");
                }

                SecureString securePassword = new SecureString();
                foreach (char c in secret)
                {
                    securePassword.AppendChar(c);
                }

                return securePassword;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataConvertToSecureString);
            } 
        }

        /// <summary>
        /// Converts a SecureString to clear text
        /// </summary>
        /// <param name="secret">sensitive data</param>
        /// <returns>Returns clear text string</returns>
        public static string ConvertToUnsecureString(SecureString secret)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataConvertToUnsecureString);

            try
            {
                if (secret == null)
                {
                    throw new ArgumentNullException("secret");
                }

                IntPtr unmanagedString = IntPtr.Zero;
                try
                {
                    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secret);
                    return Marshal.PtrToStringUni(unmanagedString);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataConvertToUnsecureString);
            }
        }

        /// <summary>
        /// Decrypts data using certificate
        /// </summary>
        /// <param name="encryptedDataConfig">Configuration describing the encrypted data</param>
        /// <returns>The decrypted data</returns>
        private static SecureString DecryptDataUsingCertificate(string encryptedDataConfig)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.ProtectedDataDecryptDataUsingCertificate, "EncryptedDataConfig: {0}.", encryptedDataConfig);

            try
            {
                StoreLocation storeLocation;
                StoreName storeName;
                string thumbprint;
                string base64EncodedEncryptedData;

                try
                {
                    // expected input format is: cert:\currentuser\my\[thumbprint],base64EncodedEncryptedData
                    int firstPathSeparator = encryptedDataConfig.IndexOf(@"\", StringComparison.OrdinalIgnoreCase);
                    int secondPathSeparator = encryptedDataConfig.IndexOf(@"\", firstPathSeparator + 1, StringComparison.OrdinalIgnoreCase);
                    int thirdPathSeparator = encryptedDataConfig.IndexOf(@"\", secondPathSeparator + 1, StringComparison.OrdinalIgnoreCase);
                    int keySeparator = encryptedDataConfig.IndexOf(@",", thirdPathSeparator + 1, StringComparison.OrdinalIgnoreCase);

                    string firstToken = encryptedDataConfig.Substring(firstPathSeparator + 1, secondPathSeparator - firstPathSeparator - 1);
                    storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), firstToken, true);

                    string secondToken = encryptedDataConfig.Substring(secondPathSeparator + 1, thirdPathSeparator - secondPathSeparator - 1);
                    storeName = (StoreName)Enum.Parse(typeof(StoreName), secondToken, true);

                    thumbprint = encryptedDataConfig.Substring(thirdPathSeparator + 1, keySeparator - thirdPathSeparator - 1);
                    base64EncodedEncryptedData = encryptedDataConfig.Substring(keySeparator + 1);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Unable to parse encryptedDataConfig.", e);
                }

                X509Certificate2 cert = GetCertificate(thumbprint, storeName, storeLocation);

                return DecryptData(base64EncodedEncryptedData, cert);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.ProtectedDataDecryptDataUsingCertificate, "EncryptedDataConfig: {0}.", encryptedDataConfig);
            }
        }
    }
}
