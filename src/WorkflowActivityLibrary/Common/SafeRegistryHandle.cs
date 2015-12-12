//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeRegistryHandle.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// Win32 safe handle implementation for registry operations
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    using System;
    using System.Security;
    using Microsoft.Win32.SafeHandles;
    
    /// <summary>
    /// Win32 safe handle implementation.
    /// </summary>
    [SecurityCritical]
    public sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the SafeRegistryHandle class
        /// </summary>
        /// <param name="preexistingHandle">An object that represents the pre-existing handle to use.</param>
        /// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release.</param>
        [SecurityCritical]
        public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SafeRegistryHandleConstructor, "OwnsHandle: {0}.", ownsHandle);

            try
            {
                this.SetHandle(preexistingHandle);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SafeRegistryHandleConstructor, "OwnsHandle: {0}.", ownsHandle);
            }
        }

        /// <summary>
        /// Initializes a new instance of the SafeRegistryHandle class
        /// </summary>
        [SecurityCritical]
        internal SafeRegistryHandle()
            : base(true)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SafeRegistryHandleConstructor);

            try
            {
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SafeRegistryHandleConstructor);
            }
        }

        /// <summary>
        /// Frees the handle by closing the open registry key.
        /// </summary>
        /// <returns>Returns true if the handle was successfully released</returns>
        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SafeRegistryHandleReleaseHandle);

            try
            {
                return NativeMethods.RegCloseKey(this.handle) == 0;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SafeRegistryHandleReleaseHandle);
            }
        }
    }
}
