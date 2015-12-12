//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="LogOnUser.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// Class to implement impersonation.
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Principal;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;

    /// <summary>
    /// Class to represent Impersonated Identity
    /// </summary>
    public class LogOnUser : IDisposable
    {
        #region Private Fields

        /// <summary>
        /// Flag to keep track of the instance's disposed status
        /// </summary>
        private bool disposed;

        /// <summary>
        /// LogonProvider used to Impersonation
        /// </summary>
        private LogOnProvider logonProvider;

        /// <summary>
        /// LogonType used to Impersonation
        /// </summary>
        private LogOnType logonType;

        /// <summary>
        /// SafeHandle to the user's primary token
        /// </summary>
        private SafeTokenHandle primaryToken;

        /// <summary>
        /// SafeHandle to the user's profile
        /// </summary>
        private SafeRegistryHandle profileHandle;

        #endregion Private Fields

        /// <summary>
        /// Initializes a new instance of the LogOnUser class
        /// Logs on the specified user as a interactive logon without loading user profile.
        /// The caller must have Impersonate a client after authentication (SeImpersonatePrivilege) privilege
        /// </summary>
        /// <param name="userName">The userName of the user to be impersonated</param>
        /// <param name="domain">The domain of the user</param>
        /// <param name="password">The password of the user</param>
        public LogOnUser(string userName, string domain, string password)
            : this(userName, domain, password, false)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LogOnUserConstructor, "Domain: {0}. UserName: {1}.", domain, userName);

            try
            {
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LogOnUserConstructor, "Domain: {0}. UserName: {1}.", domain, userName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the LogOnUser class
        /// Logs on the specified user as a interactive logon
        /// The caller must have Impersonate a client after authentication (SeImpersonatePrivilege) privilege
        /// When loadProfile = true, the caller must have following additional privileges:
        /// Restore files and directories (SeRestorePrivilege), Back up files and directories (SeBackupPrivilege)
        /// Replace a process-level token (SeAssignPrimaryTokenPrivilege)
        /// Starting with Windows XP Service Pack 2 (SP2) and Windows Server 2003, the caller must be an administrator or the LocalSystem account. 
        /// It is not sufficient for the caller to merely impersonate the administrator or LocalSystem account to load user profile.
        /// </summary>
        /// <param name="userName">The userName of the user to be impersonated</param>
        /// <param name="domain">The domain of the user</param>
        /// <param name="password">The password of the user</param>
        /// <param name="loadProfile">Indicates whether to load user profile or not</param>
        public LogOnUser(string userName, string domain, string password, bool loadProfile)
            : this(userName, domain, password, LogOnType.LogOnInteractive, LogOnProvider.ProviderWinNT50, loadProfile)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LogOnUserConstructor, "Domain: {0}. UserName: {1}. LoadProfile: {2}.", domain, userName, loadProfile);

            try
            {
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LogOnUserConstructor, "Domain: {0}. UserName: {1}. LoadProfile: {2}.", domain, userName, loadProfile);
            }
        }

        /// <summary>
        /// Initializes a new instance of the LogOnUser class
        /// </summary>
        /// <param name="userName">The userName of the user to be impersonated</param>
        /// <param name="domain">The domain of the user</param>
        /// <param name="password">The password of the user</param>
        /// <param name="logOnType">LogonType to use while impersonating</param>
        /// <param name="logOnProvider">LogonProvider to use while impersonating</param>
        /// <param name="loadProfile">Indicates whether to load user profile or not</param>
        public LogOnUser(string userName, string domain, string password, LogOnType logOnType, LogOnProvider logOnProvider, bool loadProfile)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LogOnUserConstructor, "Domain: {0}. UserName: {1}. LogOnType: {2}. LoadProfile: {3}.", domain, userName, logOnType, loadProfile);

            try
            {
                this.logonProvider = logOnProvider;
                this.logonType = logOnType;
                this.LogOn(userName, domain, password, loadProfile);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LogOnUserConstructor, "Domain: {0}. UserName: {1}. LogOnType: {2}. LoadProfile: {3}.", domain, userName, logOnType, loadProfile);
            }
        }

        /// <summary>
        /// Finalizes an instance of the LogOnUser class
        /// </summary>
        ~LogOnUser()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the WindowsIdentity object of the impersonated user
        /// </summary>
        public WindowsIdentity Identity
        {
            get
            {
                return new WindowsIdentity(this.Token);
            }
        }

        /// <summary>
        /// Gets the unsafe handle of the impersonated user
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Justification = "Safe to supress as the user handle is tested for vallidity.")]
        public IntPtr Token
        {
            get
            {
                IntPtr handle = IntPtr.Zero;

                if (!this.UserIsInvalid)
                {
                    handle = this.primaryToken.DangerousGetHandle();
                }

                return handle;
            }
        }

        /// <summary>
        /// Gets the unsafe handle of the impersonated user
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Justification = "Safe to supress as the user handle is tested for vallidity.")]
        public IntPtr UserProfileHandle
        {
            get
            {
                IntPtr handle = IntPtr.Zero;

                if (!this.UserProfileIsInvalid)
                {
                    handle = this.profileHandle.DangerousGetHandle();
                }

                return handle;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the user's handle is valid
        /// </summary>
        private bool UserIsInvalid
        {
            get
            {
                if (this.primaryToken != null)
                {
                    return this.primaryToken.IsInvalid;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the user's profile's handle is valid
        /// </summary>
        private bool UserProfileIsInvalid
        {
            get
            {
                if (this.profileHandle != null)
                {
                    return this.profileHandle.IsInvalid;
                }

                return true;
            }
        }

        /// <summary>
        /// Impersonates the user
        /// </summary>
        /// <returns>A WindowsImpersonationContext object that represents the Windows user prior to impersonation; this can be used to revert to the original user's context.</returns>
        public WindowsImpersonationContext Impersonate()
        {
            return this.Identity.Impersonate();
        }

        /// <summary>
        /// Releases all resources used by the class
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the class and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LogOnUserDispose, "Disposing: {0}.", disposing);

            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        if (this.profileHandle != null)
                        {
                            NativeMethods.UnloadUserProfile(this.Token, this.UserProfileHandle);
                            this.profileHandle.Dispose();
                            this.profileHandle = null;
                        }

                        if (this.primaryToken != null)
                        {
                            this.primaryToken.Dispose();
                            this.primaryToken = null;
                        }
                    }
                }

                this.disposed = true;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LogOnUserDispose, "Disposing: {0}.", disposing);
            }
        }

        /// <summary>
        /// Logs the specified user on to the local computer
        /// </summary>
        /// <param name="userName">The userName of the user to be impersonated</param>
        /// <param name="domain">The domain of the user</param>
        /// <param name="password">The password of the user</param>
        /// <param name="loadUserProfile">Indicates whether to load user profile or not</param>
        private void LogOn(string userName, string domain, string password, bool loadUserProfile)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.LogOnUserLogOn, "Domain: {0}. UserName: {1}. LoadProfile: {2}.", domain, userName, loadUserProfile);

            try
            {
                if (!NativeMethods.LogonUser(userName, domain, password, this.logonType, this.logonProvider, out this.primaryToken))
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }

                if (!loadUserProfile)
                {
                    return;
                }

                NativeMethods.PROFILEINFO profileInfo = new NativeMethods.PROFILEINFO();
                profileInfo.dwSize = Marshal.SizeOf(profileInfo);
                profileInfo.lpUserName = userName;
                profileInfo.lpServerName = domain;
                profileInfo.dwFlags = NativeMethods.PI_NOUI;

                if (!NativeMethods.LoadUserProfile(this.Token, ref profileInfo) || profileInfo.hProfile == IntPtr.Zero)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }

                this.profileHandle = new SafeRegistryHandle(profileInfo.hProfile, true);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.LogOnUserLogOn, "Domain: {0}. UserName: {1}. LoadProfile: {2}.", domain, userName, loadUserProfile);
            }
        }
    }
}
