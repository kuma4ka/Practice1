using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardening.Logic.ValidatorForPlant
{
    internal class PlantValidator
    {
        public static bool IsSpeciesValid(string input)
        {
            const double minLength = 4;
            const double maxLength = 15;

            return input.Length > minLength
                   && input.Length <= maxLength
                   && IsLatinLettersOnly(input);
        }

        public static bool IsAgeValid(int input)
        {
            const double minAge = 0;
            const double maxAge = 40;
            return (input > minAge && input <= maxAge) || input == -1;
        }

        public static bool IsHeightValid(double input)
        {
            const int minHeight = 0;
            const int maxHeight = 25;
            return (input >= minHeight && input <= maxHeight) || input == -1;
        }

        private static bool IsLatinLettersOnly(string input)
        {
            foreach (char c in input)
            {
                if (!(c >= 'a' && c <= 'z') && !(c >= 'A' && c <= 'Z'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}