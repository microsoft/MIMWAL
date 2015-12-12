//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordGenerator.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// PasswordGenerator class
// </summary>
//-----------------------------------------------------------------------------------------------------------------------

namespace MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common
{
    #region Namespaces Declarations

    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;

    #endregion

    /// <summary>
    /// Generates random password 
    /// </summary>
    public class PasswordGenerator
    {
        #region Declarations

        /// <summary>
        /// Max iteration count limit
        /// </summary>
        private const int MaxIterationLimit = 24; // even for a 4 char length password, 3 attempts seem to be enough

        /// <summary>
        /// The array of standard uppercase password characters
        /// </summary>
        private const string PwdCharsUpperCase = "ABCDEFGHJKLMNPQRSTUVWXYZ";

        /// <summary>
        /// The array of standard lowercase password characters
        /// </summary>
        private const string PwdCharsLowerCase = "abcdefghjkmnpqrstuvwxyz";

        /// <summary>
        /// The array of standard alphabetic password characters
        /// </summary>
        private const string PwdChars = PwdCharsUpperCase + PwdCharsLowerCase;

        /// <summary>
        /// The array of special password characters
        /// </summary>
        private const string PwdSpecialChars = "!@#$&";

        /// <summary>
        /// The array of numeric password characters
        /// </summary>
        private const string PwdNumbers = "23456789";

        /// <summary>
        /// Current iteration count
        /// </summary>
        private int currentIterationCount = 0;

        #endregion

        #region Methods

        /// <summary>
        /// Generates the random password.
        /// </summary>
        /// <param name="length">The length of the password.</param>
        /// <returns>The radom password string.</returns>
        public string GenerateRandomPassword(int length)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.PasswordGeneratorGenerateRandomPassword, "Length: {0}.", length);

            try
            {
                StringBuilder password = new StringBuilder();

                // For each available char index in the password buffer,
                // generate a new random character
                for (int i = 0; i < length; i++)
                {
                    char nextCharacter = GetRandomCharacter(PasswordCharType.Standard);
                    password.Append(nextCharacter);
                }

                int numberIndex = 0;
                int specialIndex = 0;

                if (length != 1)
                {
                    // To ensure complexity, the password must contain at least  1 number and 1 special character
                    // Select indexes from the constructed password at random to determine which will be replaced
                    Random randomNumber = new Random(); // OK to use simple random number generated here as GetRandomCharacter uses RNG anyway.

                    numberIndex = randomNumber.Next(0, length - 1);
                    do
                    {
                        // Find an index to replace with a special character
                        // that is not the same index used for the number
                        specialIndex = randomNumber.Next(0, length - 1);
                    }
                    while (numberIndex == specialIndex);
                }

                // Replace the number index with a random number
                // Replace the special char index with a random special character
                password[numberIndex] = GetRandomCharacter(PasswordCharType.Number);
                password[specialIndex] = GetRandomCharacter(PasswordCharType.Special);

                if (length >= 4)
                {
                    // To ensure complexity, the password must contain at least  1 uppercase and 1 lowercase character
                    string[] patternParts = new string[] { PwdCharsUpperCase, PwdCharsLowerCase, PwdSpecialChars, PwdNumbers, length.ToString(CultureInfo.InvariantCulture) };
                    string pattern = string.Format(CultureInfo.InvariantCulture, "^(?=.*[{0}])(?=.*[{1}])(?=.*[{2}])(?=.*[{3}]).{{{4}}}$", patternParts);
                    Regex pwdExpression = new Regex(pattern);
                    if (pwdExpression.Matches(password.ToString()).Count == 0)
                    {
                        ++this.currentIterationCount;
                        if (this.currentIterationCount < MaxIterationLimit)
                        {
                            Logger.Instance.WriteVerbose(EventIdentifier.PasswordGeneratorGenerateRandomPassword, "Length: {0}. Iteration Count: {1}.", length, this.currentIterationCount);
                            return this.GenerateRandomPassword(length);
                        }
                        else
                        {
                            Logger.Instance.WriteWarning(EventIdentifier.PasswordGeneratorGenerateRandomPasswordWarning, Messages.PasswordGenerator_LoopCountWarning, length);
                            return password.ToString();
                        }
                    }
                }

                // Return the random password
                return password.ToString();
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.PasswordGeneratorGenerateRandomPassword, "Length: {0}.", length);
            }
        }

        /// <summary>
        /// Returns a cryptographically strong random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue; </returns>
        private static int GetNextRandomNumber(int minValue, int maxValue)
        {
            if (minValue < byte.MinValue)
            {
                throw Logger.Instance.ReportError(new ArgumentOutOfRangeException("minValue"));
            }

            if (maxValue > byte.MaxValue)
            {
                throw Logger.Instance.ReportError(new ArgumentOutOfRangeException("maxValue"));
            }

            if (minValue >= maxValue)
            {
                throw Logger.Instance.ReportError(new ArgumentException(Messages.PasswordGenerator_MaxValueLessThanMinValueError));
            }

            int randomNumber;

            byte[] random = new byte[1];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            do
            {
                rng.GetBytes(random);
                randomNumber = random[0];
            }
            while (randomNumber < minValue || randomNumber >= maxValue);

            return randomNumber;
        }

        /// <summary>
        /// Gets the random character.
        /// </summary>
        /// <param name="type">One of the <see cref="PowerShellReturnType"/> enumeration value.</param>
        /// <returns>A random character that corresponds to the specified <see cref="PowerShellReturnType"/> enumeration value.</returns>
        private static char GetRandomCharacter(PasswordCharType type)
        {
            Logger.Instance.WriteMethodEntry(EventIdentifier.PasswordGeneratorGetRandomCharacter, "PasswordCharType: '{0}'.", type);

            try
            {
                // Determine if a standard, special, or numeric character
                // will be returned based on the supplied Boolean value
                int randomIndex;
                switch (type)
                {
                    case PasswordCharType.Special:

                        // Generate a random number which represents an
                        // index of the password special character array
                        randomIndex = GetNextRandomNumber(0, PwdSpecialChars.Length - 1);
                        return PwdSpecialChars[randomIndex];

                    case PasswordCharType.Number:

                        // Generate a random number which represents an
                        // index of the password number array
                        randomIndex = GetNextRandomNumber(0, PwdNumbers.Length - 1);
                        return PwdNumbers[randomIndex];

                    default:

                        // Generate a random number which represents an
                        // index of the password character array
                        randomIndex = GetNextRandomNumber(0, PwdChars.Length - 1);
                        return PwdChars[randomIndex];
                }
            }
            finally
            {
                Logger.Instance.WriteMethodExit(EventIdentifier.PasswordGeneratorGetRandomCharacter, "PasswordCharType: '{0}'.", type);
            }
        }

        #endregion
    }
}