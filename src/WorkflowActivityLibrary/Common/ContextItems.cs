//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextItems.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Microsoft makes no warranties, expressed or implied.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// ContextItems class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    using System;
    using System.Collections;
    using System.Runtime.Remoting.Messaging;
    using System.Security;

    /// <summary>
    /// Provides methods to maintain a key/value dictionary that is stored in the <see cref="CallContext"/>.
    /// </summary>
    /// <remarks>
    /// A context item represents a key/value that needs to be logged with each message
    /// on the same CallContext.
    /// </remarks>
    internal static class ContextItems
    {
        /// <summary>
        /// The name of the data slot in the <see cref="CallContext"/> used by the application block.
        /// </summary>
        private const string CallContextSlotName = "MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common.ContextItems";

        /// <summary>
        /// Adds a key/value pair to a dictionary in the <see cref="CallContext"/>.  
        /// Each context item is recorded with every log entry.
        /// </summary>
        /// <param name="key">Hashtable key.</param>
        /// <param name="value">Value of the context item.  Byte arrays will be base64 encoded.</param>
        /// <example>The following example demonstrates use of the AddContextItem method.
        /// <code>Logger.SetContextItem("SessionID", myComponent.SessionId);</code></example>
        [SecurityCritical]
        public static void SetContextItem(object key, object value)
        {
            Hashtable contextItems = (Hashtable)CallContext.GetData(CallContextSlotName) ?? new Hashtable();

            contextItems[key] = value;

            CallContext.SetData(CallContextSlotName, contextItems);
        }

        /// <summary>
        /// Empties the context items dictionary.
        /// </summary>
        [SecurityCritical]
        public static void FlushContextItems()
        {
            CallContext.FreeNamedDataSlot(CallContextSlotName);
        }

        /// <summary>
        /// Gets the context items.
        /// </summary>
        /// <returns>The Hashtable of items on the call context.</returns>
        [SecurityCritical]
        public static Hashtable GetContextItems()
        {
            return (Hashtable)CallContext.GetData(CallContextSlotName);
        }

        /// <summary>
        /// Gets the context item value.
        /// </summary>
        /// <param name="contextData">The context data.</param>
        /// <returns>The context item value.</returns>
        public static string GetContextItemValue(object contextData)
        {
            string value = string.Empty;

            if (contextData != null)
            {
                value = contextData.GetType() == typeof(byte[]) ? Convert.ToBase64String((byte[])contextData) : contextData.ToString();
            }

            return value;
        }
    }
}
