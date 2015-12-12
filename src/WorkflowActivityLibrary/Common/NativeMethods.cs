//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// Native Method calls
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;

    /// <summary>
    /// Class to call Win32 API's
    /// </summary>
    internal static class NativeMethods
    {
        #region Impersonation API's

        /// <summary>
        /// DPAPI flag: Prevents the display of profile error messages
        /// </summary>
        public const int PI_NOUI = 1;

        /// <summary>
        /// Registry Access Parameter
        /// </summary>
        public const int KEY_ALL_ACCESS = 0xF003F;

        /// <summary>
        /// WINAPI error code for too small buffer to receive the data
        /// </summary>
        public const int ERROR_MORE_DATA = 0xEA;

        /// <summary>
        /// Advapi32 function used for logging in as a user
        /// </summary>
        /// <param name="lpszUsername">The user name of the user to log in as</param>
        /// <param name="lpszDomain">The domain the user is in</param>
        /// <param name="lpszPassword">The users password</param>
        /// <param name="dwLogonType">The type of logon operation to perform</param>
        /// <param name="dwLogonProvider">Specifies the logon provider</param>
        /// <param name="phToken">A pointer to a handle variable that receives a handle to a token that represents the specified user</param>
        /// <returns>If the function succeeds, the function returns nonzero</returns>
        [SuppressMessage("Microsoft.Security", "CA5122:PInvokesShouldNotBeSafeCriticalFxCopRule", Justification = "CA5122 is firing in this case because the module is build against 3.5, which causes the entire assembly to be safe critical as the security transparency features of the .NET framework began in 4.0")]
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Using case as defined in the implemented interface")]
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, LogOnType dwLogonType, LogOnProvider dwLogonProvider, out SafeTokenHandle phToken);

        /// <summary>
        /// The DuplicateToken function creates an impersonation token,
        /// which you can use in functions such as SetThreadToken and ImpersonateLoggedOnUser.
        /// The token created by DuplicateToken cannot be used in the CreateProcessAsUser function,
        /// which requires a primary token.
        /// To create a token that you can pass to CreateProcessAsUser,
        /// use the DuplicateTokenEx function.
        /// </summary>
        /// <param name="hToken">A handle to an access token opened with TOKEN_DUPLICATE access</param>
        /// <param name="impersonationLevel">Specifies the impersonation level of the new token.</param>
        /// <param name="hNewToken">A pointer to a variable that receives a handle to the duplicate token</param>
        /// <returns>If the function succeeds, the return value is nonzero</returns>
        ////[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        ////[return: MarshalAs(UnmanagedType.Bool)]
        ////internal static extern bool DuplicateToken(IntPtr hToken, LogOnUser.ImpersonationLevel impersonationLevel, ref IntPtr hNewToken);

        /// <summary>
        /// Advapi32 function used to close the handle to impersonated user token
        /// </summary>
        /// <param name="handle">Token returned from LogOnUser</param>
        /// <returns>Returns true if error</returns>
        [SuppressMessage("Microsoft.Security", "CA5122:PInvokesShouldNotBeSafeCriticalFxCopRule", Justification = "CA5122 is firing in this case because the module is build against 3.5, which causes the entire assembly to be safe critical as the security transparency features of the .NET framework began in 4.0")]
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr handle);

        [SuppressMessage("Microsoft.Security", "CA5122:PInvokesShouldNotBeSafeCriticalFxCopRule", Justification = "CA5122 is firing in this case because the module is build against 3.5, which causes the entire assembly to be safe critical as the security transparency features of the .NET framework began in 4.0")]
        [DllImport("userenv.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LoadUserProfile(IntPtr hToken, ref PROFILEINFO lpProfileInfo);

        [SuppressMessage("Microsoft.Security", "CA5122:PInvokesShouldNotBeSafeCriticalFxCopRule", Justification = "CA5122 is firing in this case because the module is build against 3.5, which causes the entire assembly to be safe critical as the security transparency features of the .NET framework began in 4.0")]
        [DllImport("Userenv.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnloadUserProfile(IntPtr hToken, IntPtr hProfile);

        #endregion Impersonation API's

        #region Registry API's

        /// <summary>
        /// Closes the open registry key.
        /// </summary>
        /// <param name="hKey">A handle to the open key to be closed.</param>
        /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
        [SuppressMessage("Microsoft.Security", "CA5122:PInvokesShouldNotBeSafeCriticalFxCopRule", Justification = "CA5122 is firing in this case because the module is build against 3.5, which causes the entire assembly to be safe critical as the security transparency features of the .NET framework began in 4.0")]
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern int RegCloseKey(IntPtr hKey);

        #endregion Registry API's

        #region Impersonation Structures

        /// <summary>
        /// Contains information about a user profile
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct PROFILEINFO
        {
            /// <summary>
            /// Specifies the size of the structure, in bytes.
            /// </summary>
            public int dwSize;

            /// <summary>
            /// This member can be one of the following flags: 
            /// PI_NOUI or PI_APPLYPOLICY
            /// PI_NOUI: Prevents the display of profile error messages.
            /// PI_APPLYPOLICY : Apply Microsoft Windows NT 4.0-style policy.
            /// </summary>
            public int dwFlags;

            /// <summary>
            /// Pointer to the name of the user.
            /// This member is used as the base name of the directory in which to scope a new profile.
            /// </summary>
            public string lpUserName;

            /// <summary>
            /// Pointer to the roaming user profile path.
            /// If the user does not have a roaming profile, this member can be NULL.
            /// </summary>
            public string lpProfilePath;

            /// <summary>
            /// Pointer to the default user profile path. This member can be NULL.
            /// </summary>
            public string lpDefaultPath;

            /// <summary>
            /// Pointer to the name of the validating domain controller, in NetBIOS format.
            /// If this member is NULL, the Windows NT 4.0-style policy will not be applied.
            /// </summary>
            public string lpServerName;

            /// <summary>
            /// Pointer to the path of the Windows NT 4.0-style policy file. 
            /// This member can be NULL.
            /// </summary>
            public string lpPolicyPath;

            /// <summary>
            /// Handle to the HKEY_CURRENT_USER registry key.
            /// </summary>
            public IntPtr hProfile;
        }

        #endregion Impersonation Structures
    }
}
