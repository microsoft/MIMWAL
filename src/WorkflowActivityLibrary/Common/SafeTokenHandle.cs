//---------------------------------------------------------------------
// <copyright file="SafeTokenHandle.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// Win32 safe handle implementation
// </summary>
//---------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.ConstrainedExecution;
    using System.Security;
    using Microsoft.Win32.SafeHandles;
    
    /// <summary>
    /// Win32 safe handle implementation.
    /// </summary>
    internal sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the SafeTokenHandle class
        /// </summary>
        public SafeTokenHandle()
            : base(true)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SafeTokenHandleConstructor);

            try
            {
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SafeTokenHandleConstructor);
            }
        }

        /// <summary>
        /// Initializes a new instance of the SafeTokenHandle class
        /// </summary>
        /// <param name="handle">The pre-existing handle to use</param>
        public SafeTokenHandle(IntPtr handle)
            : base(true)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SafeTokenHandleConstructor);

            try
            {
                this.SetHandle(handle);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SafeTokenHandleConstructor);
            }
        }

        /// <summary>
        /// An implicit user-defined type conversion operator
        /// </summary>
        /// <param name="handle">The pre-existing handle to use</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "False warning.")]
        public static implicit operator SafeTokenHandle(IntPtr handle)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SafeTokenHandleConstructor);

            try
            {
                return new SafeTokenHandle(handle);
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SafeTokenHandleConstructor);
            }
        }

        /// <summary>
        /// Executes the code required to free the handle.
        /// </summary>
        /// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false</returns>
        [SecurityCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.SafeTokenHandleReleaseHandle);

            try
            {
                if (this.IsInvalid)
                {
                    return true;
                }

                NativeMethods.CloseHandle(this.handle);
                this.handle = IntPtr.Zero;

                return true;
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.SafeTokenHandleReleaseHandle);
            }
        }
    }
}
