using System;
using System.Linq;

namespace PasswordPhilosophy
{
    class Program
    {
        private enum ValidatorRule
        {
            ORIGINAL,
            POSITIONAL
        };

        static void Main(string[] args)
        {
            FindValidPasswords(ValidatorRule.ORIGINAL);
            FindValidPasswords(ValidatorRule.POSITIONAL);
        }

        private static void FindValidPasswords(ValidatorRule ruleType)
        {
            Func<string, bool> validatorRule = null;

            switch (ruleType)
            {
                case ValidatorRule.ORIGINAL:
                    validatorRule = PasswordValidator.ValidateOriginalRule;
                    break;

                case ValidatorRule.POSITIONAL:
                    validatorRule = PasswordValidator.ValidatePositionalRule;
                    break;

                default:
                    throw new ArgumentException("Validator rule not supported");
            };

            var passwords = DataSeed.GetData();
            var validCount = 0;

            foreach (var password in passwords)
            {
                var valid = validatorRule(password);

                if (valid) validCount++;

                Console.WriteLine($"{password} => {valid}");
            }

            Console.WriteLine($"\nTotal passwords: {passwords.Count()}");
            Console.WriteLine($"Total valid passwords: {validCount}");
        }
    }

    public static class PasswordValidator
    {
        public static bool ValidatePositionalRule(string input)
        {
            // 1-3 a: abcde => the numbers describe the expected positions of the supplied character.
            // the character must exist in exactly one of these positions to be valid (NOT zero indexed).
            var details = GetPasswordDetails(input);
            var charArray = details.password.ToCharArray();

            // avoid potential out of bounds exception
            if (charArray.Count() < details.upper)
            {
                return false;
            }

            if (charArray[details.lower - 1] == details.character ^ charArray[details.upper - 1] == details.character)
            {
                return true;
            }

            return false;
        }

        public static bool ValidateOriginalRule(string input)
        {
            var details = GetPasswordDetails(input);

            // find count of chars in password
            var charArray = details.password.ToCharArray();
            var charCount = charArray.Count(c => c == details.character);

            if (charCount >= details.lower && charCount <= details.upper)
            {
                return true;
            }

            return false;
        }

        public static (int lower, int upper, char character, string password) GetPasswordDetails(string policyAndPassword)
        {
            var parts = policyAndPassword.Split(' ');
            var lower = int.Parse(parts[0].Split('-')[0]);
            var upper = int.Parse(parts[0].Split('-')[1]);
            var c = char.Parse(parts[1].Trim(':'));

            return (lower, upper, c, parts[2]);
        }
    }
}
