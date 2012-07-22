using System;
using System.Security.Cryptography;

namespace UCosmic.Domain.Identity
{
    internal class GenerateRandomSecretQuery : IDefineQuery<string>
    {
        internal GenerateRandomSecretQuery(int minimumLength, int maximumLength)
        {
            if (minimumLength < 1)
                throw new ArgumentException(
                    "Random secret length must be at least 1.", "minimumLength");
            if (maximumLength < minimumLength)
                throw new ArgumentException(
                    "Random secret maximumLength must be greater than or equal to minimumLength.", "maximumLength");

            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }

        internal GenerateRandomSecretQuery(int exactLength)
            : this(exactLength, exactLength)
        {
        }

        internal int MinimumLength { get; private set; }
        internal int MaximumLength { get; private set; }
    }

    internal class GenerateRandomSecretHandler
    {
        internal static string Handle(GenerateRandomSecretQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return RandomSecretGenerator.CreateSecret(query.MinimumLength, query.MaximumLength);
        }

        // code from http://stackoverflow.com/questions/4768712/create-a-12-character-secret
        private static class RandomSecretGenerator
        {
            // Define default min and max password lengths.
            //private const int DefaultMinPasswordLength = 8;
            //private const int DefaultMaxPasswordLength = 10;

            // Define supported password characters divided into groups.
            // You can add (or remove) characters to (from) these groups.
            private const string PasswordCharsLcase = "abcdefgijkmnopqrstwxyz";
            private const string PasswordCharsUcase = "ABCDEFGHJKLMNPQRSTWXYZ";
            private const string PasswordCharsNumeric = "23456789";

            /// <summary>
            /// Generates a random password.
            /// </summary>
            /// <param name="minimumLength">
            /// Minimum password length.
            /// </param>
            /// <param name="maximumLength">
            /// Maximum password length.
            /// </param>
            /// <returns>
            /// Randomly generated password.
            /// </returns>
            /// <remarks>
            /// The length of the generated password will be determined at
            /// random and it will fall with the range determined by the
            /// function parameters.
            /// </remarks>
            internal static string CreateSecret(int minimumLength, int maximumLength)
            {
                // Make sure that input parameters are valid.
                if (minimumLength <= 0 || maximumLength <= 0 || minimumLength > maximumLength)
                    return null;

                // Create a local array containing supported password characters
                // grouped by types. You can remove character groups from this
                // array, but doing so will weaken the password strength.
                var charGroups = new[]
            {
                PasswordCharsLcase.ToCharArray(),
                PasswordCharsUcase.ToCharArray(),
                PasswordCharsNumeric.ToCharArray()
            };

                // Use this array to track the number of unused characters in each
                // character group.
                var charsLeftInGroup = new int[charGroups.Length];

                // Initially, all characters in each group are not used.
                for (var i = 0; i < charsLeftInGroup.Length; i++)
                    charsLeftInGroup[i] = charGroups[i].Length;

                // Use this array to track (iterate through) unused character groups.
                var leftGroupsOrder = new int[charGroups.Length];

                // Initially, all character groups are not used.
                for (var i = 0; i < leftGroupsOrder.Length; i++)
                    leftGroupsOrder[i] = i;

                // Because we cannot use the default randomizer, which is based on the
                // current time (it will produce the same "random" number within a
                // second), we will use a random number generator to seed the
                // randomizer.

                // Use a 4-byte array to fill it with random bytes and convert it then
                // to an integer value.
                var randomBytes = new byte[4];

                // Generate 4 random bytes.
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(randomBytes);

                // Convert 4 bytes into a 32-bit integer value.
                var seed = (randomBytes[0] & 0x7f) << 24 |
                            randomBytes[1] << 16 |
                            randomBytes[2] << 8 |
                            randomBytes[3];

                // Now, this is real randomization.
                var random = new Random(seed);

                // This array will hold password characters.
                // Allocate appropriate memory for the password.
                var password = minimumLength < maximumLength ? new char[random.Next(minimumLength, maximumLength + 1)] : new char[minimumLength];

                // Index of the last non-processed group.
                var lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

                // Generate password characters one at a time.
                for (var i = 0; i < password.Length; i++)
                {
                    // Index which will be used to track not processed character groups.
                    // If only one character group remained unprocessed, process it;
                    // otherwise, pick a random character group from the unprocessed
                    // group list. To allow a special character to appear in the
                    // first position, increment the second parameter of the Next
                    // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                    int nextLeftGroupsOrderIdx;
                    if (lastLeftGroupsOrderIdx == 0)
                        nextLeftGroupsOrderIdx = 0;
                    else
                        nextLeftGroupsOrderIdx = random.Next(0,
                                                             lastLeftGroupsOrderIdx);

                    // Index of the next character group to be processed.
                    // Get the actual index of the character group, from which we will
                    // pick the next character.
                    var nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                    // Index of the last non-processed character in a group.
                    // Get the index of the last unprocessed characters in this group.
                    var lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                    // Index of the next character to be added to password.
                    // If only one unprocessed character is left, pick it; otherwise,
                    // get a random character from the unused character list.
                    var nextCharIdx = lastCharIdx == 0 ? 0 : random.Next(0, lastCharIdx + 1);

                    // Add this character to the password.
                    password[i] = charGroups[nextGroupIdx][nextCharIdx];

                    // If we processed the last character in this group, start over.
                    if (lastCharIdx == 0)
                        charsLeftInGroup[nextGroupIdx] =
                                                  charGroups[nextGroupIdx].Length;
                    // There are more unprocessed characters left.
                    else
                    {
                        // Swap processed character with the last unprocessed character
                        // so that we don't pick it until we process all characters in
                        // this group.
                        if (lastCharIdx != nextCharIdx)
                        {
                            var temp = charGroups[nextGroupIdx][lastCharIdx];
                            charGroups[nextGroupIdx][lastCharIdx] =
                                        charGroups[nextGroupIdx][nextCharIdx];
                            charGroups[nextGroupIdx][nextCharIdx] = temp;
                        }
                        // Decrement the number of unprocessed characters in
                        // this group.
                        charsLeftInGroup[nextGroupIdx]--;
                    }

                    // If we processed the last group, start all over.
                    if (lastLeftGroupsOrderIdx == 0)
                        lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                    // There are more unprocessed groups left.
                    else
                    {
                        // Swap processed group with the last unprocessed group
                        // so that we don't pick it until we process all groups.
                        if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                        {
                            var temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                            leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                        leftGroupsOrder[nextLeftGroupsOrderIdx];
                            leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                        }
                        // Decrement the number of unprocessed groups.
                        lastLeftGroupsOrderIdx--;
                    }
                }
                return new string(password);
            }
        }
    }
}
