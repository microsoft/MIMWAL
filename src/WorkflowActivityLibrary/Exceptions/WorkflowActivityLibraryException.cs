//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkflowActivityLibraryException.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// WorkflowActivityLibraryException class. Base Exception class for all WAL custom exceptions.
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions
{
    #region Namespaces Declarations

    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    /// The exception the base class for all WAL custom exceptions.
    /// </summary>
    [Serializable]
    public class WorkflowActivityLibraryException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowActivityLibraryException"/> class.
        /// </summary>
        public WorkflowActivityLibraryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowActivityLibraryException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WorkflowActivityLibraryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowActivityLibraryException"/> class with a specified error message and a reference to the inner exception that caused the current exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that caused the current exception.</param>
        public WorkflowActivityLibraryException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowActivityLibraryException"/> class.
        /// </summary>
        /// <param name="message">The message format string that describes the error.</param>
        /// <param name="args">The object array that contains zero or more objects to format.</param>
        public WorkflowActivityLibraryException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowActivityLibraryException"/> class.
        /// </summary>
        /// <param name="message">The message format string that describes the error.</param>
        /// <param name="inner">The exception that caused the current exception.</param>
        /// <param name="args">The object array that contains zero or more objects to format.</param>
        public WorkflowActivityLibraryException(string message, Exception inner, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args), inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowActivityLibraryException"/> class with the serialized object data and the contextual information about the source or destination.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected WorkflowActivityLibraryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}