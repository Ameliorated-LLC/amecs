using System;
using System.Collections.Generic;
using System.Linq;

namespace Ameliorated.ConsoleUtils
{
    public static class Extensions
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        /// <summary>
        /// Checks if number is divisble by a divisor.
        /// <param name="divisor">Divisor.</param>
        /// <param name="returnFalseOnZeroOrNegative">Indicates whether to return false on numbers that are zero or below. Default is true.</param>
        /// </summary>
        public static bool IsDivisibleBy(this int number, int divisor, bool returnFalseOnZeroOrNegative = true)
        {
            if (returnFalseOnZeroOrNegative && number <= 0) return false;
            return number % divisor == 0;
        }

        public static int RoundToEven(this double number)
        {
            var rounded = (int)Math.Round(number);
            if (!rounded.IsEven()) return rounded - 1;
            return rounded;
        }

        public static int RoundToEven(this int number)
        {
            if (!number.IsEven()) return number - 1;
            return number;
        }

        public static int RoundToOdd(this double number)
        {
            if (!((int)Math.Ceiling(number)).IsEven()) return (int)Math.Ceiling(number);
            if (!((int)Math.Floor(number)).IsEven()) return (int)Math.Floor(number);
            return (int)Math.Truncate(number) - 1;
        }

        public static int RoundToOdd(this int number)
        {
            if (!number.IsEven()) return number - 1;
            return number;
        }

        public static string[] SplitByLine(this string text, StringSplitOptions options = StringSplitOptions.None)
        {
            return text.Split(new[]
            { "\r\n", "\n" }, options);
        }

        public static string LastLine(this string text, StringSplitOptions options = StringSplitOptions.None)
        {
            return text.SplitByLine().Last();
        }

        public static void ReplaceItem(this List<Menu.MenuItem> list, Menu.MenuItem oldItem, Menu.MenuItem newItem)
        {
            var index = list.IndexOf(oldItem);
            if (index == -1) throw new ArgumentException("Could not find item index.");
            
            list.RemoveAt(index);
            list.Insert(index, newItem);
        }
        public static Menu.MenuItem Clone(this Menu.MenuItem item)
        {
            return new Menu.MenuItem(item.PrimaryText, item.ReturnValue)
            {
                AddBetweenSpace = item.AddBetweenSpace,
                IsEnabled = item.IsEnabled,
                IsNextButton = item.IsNextButton,
                IsPreviousButton = item.IsPreviousButton,
                IsStatic = item.IsStatic,
                PrimaryTextBackground = item.PrimaryTextBackground,
                PrimaryTextForeground = item.PrimaryTextForeground,
                SecondaryText = item.SecondaryText,
                SecondaryTextBackground = item.SecondaryTextBackground,
                SecondaryTextForeground = item.SecondaryTextForeground,
                StretchSelection = item.StretchSelection,
            };
        }
    }
}