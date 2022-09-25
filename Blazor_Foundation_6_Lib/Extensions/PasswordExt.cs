using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class PasswordExt
    {
        public const string Characters_Capital_Only = "QWERTYUIOPASDFGHJKLZXCVBNM";
        public const string Characters_Lower_Only = "qwertyuiopasdfghjklzxcvbnm";
        public const string Charaters_Digit_Only = "0123456789";
        public const string Characters_Special_Only = @"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";
        public const string Characters_Cap_Digit = Characters_Capital_Only + Charaters_Digit_Only;
        public const string Characters_Low_Digit = Characters_Lower_Only + Charaters_Digit_Only;
        public const string Characters_Low_Special = Characters_Lower_Only + Characters_Special_Only;
        public const string Characters_Digit_Special = Charaters_Digit_Only + Characters_Special_Only;
        public const string Characters_Cap_Special = Characters_Capital_Only + Characters_Special_Only;
        public const string Characters_Cap_Low = Characters_Capital_Only + Characters_Lower_Only;
        public const string Characters_All = Characters_Capital_Only + Characters_Lower_Only + Charaters_Digit_Only + Characters_Special_Only;

        /// <summary>
        /// True: when password contains only allowed characters.
        /// </summary>
        public static bool ContainAllowedOnly(this string password)
        {
            return !password.Where(p => !Characters_All.Contains(p)).Any();
        }

        /// <summary>
        /// True when password contains at least 1 or more of the special characters.
        /// </summary>
        public static bool ContainSpecial(this string password, int atLeast = 1)
        {
            if (atLeast < 0) { return false; }
            return Characters_Special_Only.Count(p => password.Contains(p)) >= atLeast;
        }

        /// <summary>
        /// True when password contains at least 1 or more of the upper case characters.
        /// </summary>
        public static bool ContainUpperCase(this string password, int atLeast = 1)
        {
            if (atLeast < 0) { return false; }
            return Characters_Capital_Only.Count(p => password.Contains(p)) >= atLeast;
        }

        /// <summary>
        /// True when password contains at least 1 or more of the uower case characters.
        /// </summary>
        public static bool ContainLowerCase(this string password, int atLeast = 1)
        {
            if (atLeast < 0) { return false; }
            return Characters_Lower_Only.Count(p => password.Contains(p)) >= atLeast;
        }

        /// <summary>
        /// True when password contains at least 1 or more of the digit characters.
        /// </summary>
        public static bool ContainDigit(this string password, int atLeast = 1)
        {
            if (atLeast < 0) { return false; }
            return Charaters_Digit_Only.Count(p => password.Contains(p)) >= atLeast;
        }

        static RandomNumberGenerator provider;
        private static char GenerateChar(string availableChars)
        {
            var byteArray = new byte[1];
            char c;
            do
            {
                provider.GetBytes(byteArray);
                c = (char)byteArray[0];

            } while (!availableChars.Any(x => x == c));

            return c;
        }

        /// <summary>
        /// String must be empty of null to get a salt.
        /// </summary>
        public static string GenerateSalt()
        {
            return GenerateStrongPassword(14, 4, 4, 2, 2);
        }

        /// <summary>
        /// Generate a Random Password with the given constraint. Note: minX = 0 means no characters will be selected
        /// </summary>
        /// <param name="length"></param>
        /// <param name="minCapital"></param>
        /// <param name="minLower"></param>
        /// <param name="minDigit"></param>
        /// <param name="minSpecial"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GenerateStrongPassword(int length = 12, int minCapital = 1, int minLower = 5, int minDigit = 5, int minSpecial = 1)
        {
            minCapital = minCapital < 0 ? 0 : minCapital;
            minLower = minCapital < 0 ? 0 : minLower;
            minDigit = minCapital < 0 ? 0 : minDigit;
            minSpecial = minCapital < 0 ? 0 : minSpecial;
            int minTotal = (minCapital + minDigit + minLower + minSpecial);
            if (length < minTotal) { throw new ArgumentOutOfRangeException("Length cannot be lesser than a minimum of each char set"); }
            if (minTotal <= 0) { throw new ArgumentOutOfRangeException("min cannot be 0 or lesser everywhere... 0 = no characters."); }
            int anythingTotal = length - minTotal;
            int setCapital = minCapital, setLower = minLower, setDigit = minDigit, setSpecial = minSpecial;

            Random r = new Random();
            int breakSecurity = 0;
            // Assign randomly anything can happen
            while (anythingTotal > 0)
            {
                // in cas infinite loop happen kill after 1000 iterations
                if (breakSecurity > 1000) { break; }
                breakSecurity++;
                int next = 0;
                if (minCapital > 0)
                {
                    next = r.Next(0, anythingTotal + 1);
                    setCapital += next;
                    anythingTotal -= next;
                }

                if (minLower > 0)
                {
                    next = r.Next(0, anythingTotal + 1);
                    setLower += next;
                    anythingTotal -= next;
                }

                if (minDigit > 0)
                {
                    next = r.Next(0, anythingTotal + 1);
                    setDigit += next;
                    anythingTotal -= next;
                }

                if (minSpecial > 0)
                {
                    next = r.Next(0, anythingTotal + 1);
                    setSpecial += next;
                    anythingTotal -= next;
                }


            };
            Console.WriteLine($"over");
            return CreatePassword(setSpecial, setCapital, setLower, setDigit);
        }

        /// <summary>
        /// Generate a random password with a given length and no constraint about the number of each chars
        /// </summary>
        private static string CreatePassword(int special = 1, int capital = 5, int lower = 5, int digit = 1)
        {
            // Loop to Build Password with correct lenght using random characters
            int curSpecial = 0, curDigit = 0, curCap = 0, curLow = 0;
            int totalConst = special + capital + lower + digit; // Calculate total of constraints
            provider = RandomNumberGenerator.Create();
            StringBuilder sb = new StringBuilder();
            for (int n = 0; n < totalConst; n++)
            {
                string characters = "";
                // Add only remaining required char
                if (curSpecial < special) { characters += Characters_Special_Only; }
                if (curDigit < digit) { characters += Charaters_Digit_Only; }
                if (curCap < capital) { characters += Characters_Capital_Only; }
                if (curLow < lower) { characters += Characters_Lower_Only; }
                char genChar = GenerateChar(characters);
                sb = sb.Append(genChar);
                // Track Progress
                curSpecial = Characters_Special_Only.Contains(genChar) ? curSpecial + 1 : curSpecial;
                curDigit = Charaters_Digit_Only.Contains(genChar) ? curDigit + 1 : curDigit;
                curCap = Characters_Capital_Only.Contains(genChar) ? curCap + 1 : curCap;
                curLow = Characters_Lower_Only.Contains(genChar) ? curLow + 1 : curLow;
                Console.WriteLine($"Special: {curSpecial}/{special}, Digit: {curDigit}/{digit}, Capital: {curCap}/{capital}, Lower: {curLow}/{lower}");

            }
            return sb.ToString();
        }




    }
}
