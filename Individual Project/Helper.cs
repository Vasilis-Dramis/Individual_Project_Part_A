using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    static class Helper
    {
        public static bool yesInput()
        {
            string answer;
            do
            {
                answer = Console.ReadLine();
            } while (!answer.Equals("y", StringComparison.OrdinalIgnoreCase) && !answer.Equals("n", StringComparison.OrdinalIgnoreCase));
            return answer.Equals("y", StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        public static bool exitChecker(string input)
        {
            return input.Equals("exit", StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        public static int menuSelectionChecker(int choises, bool exitEnable)
        {
            int selection = 0;
            string input;
            do
            {
                input = Console.ReadLine();
                if (exitEnable)
                {
                    if (exitChecker(input))
                        return -1;
                }
            } while (!int.TryParse(input, out selection) || !(selection > 0 && selection <= choises));
            return selection;
        }
        public static string noEmptyStringInputChecker()
        {
            string input;
            bool flag = true;
            do
            {
                input = Console.ReadLine().Trim();
                if (input.Equals(string.Empty))
                {
                    Console.WriteLine("You have not entered a value.");
                }
                else
                    flag = false;
            } while (flag);
            return input;
        }
        public static DateTime validDateTimeInput()
        {
            DateTime dt;
            while (!DateTime.TryParse(Console.ReadLine(), out dt))
            {
                Console.WriteLine("Please give a valid Date.");
            }
            return dt;
        }
        public static DateTime validDateTimeInput(DateTime minDateTime)
        {
            DateTime dt;
            while (!DateTime.TryParse(Console.ReadLine(), out dt) || dt < minDateTime)
            {
                Console.WriteLine("Please give a valid Date that is after {0}.", minDateTime.ToString("dd / MM / yyyy"));
            }
            return dt;
        }
        public static DateTime validDateTimeInput(DateTime minDateTime, DateTime maxDateTime)
        {
            DateTime dt;
            while (!DateTime.TryParse(Console.ReadLine(), out dt) || dt < minDateTime || dt > maxDateTime)
            {
                Console.WriteLine("Please give a valid Date that is between {0} and {1}.", minDateTime.ToString("dd / MM / yyyy"), maxDateTime.ToString("dd / MM / yyyy"));
            }
            return dt;
        }
        public static DateTime validDateTimeInput(DateTime minDateTime, DateTime maxDateTime, bool dayOfWeek)
        {
            DateTime dt;
            while (!DateTime.TryParse(Console.ReadLine(), out dt) || dt < minDateTime || dt > maxDateTime || dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine("Please give a valid Date that is between {0} and {1} and it is not in weekend.", minDateTime.ToString("dd / MM / yyyy"), maxDateTime.ToString("dd / MM / yyyy"));
            }
            return dt;
        }
        public static DateTime startOfCalendarWeek(DateTime dateTime)
        {
            DateTime startOfCalendarWeek;
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    startOfCalendarWeek = dateTime.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    startOfCalendarWeek = dateTime.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    startOfCalendarWeek = dateTime.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    startOfCalendarWeek = dateTime.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    startOfCalendarWeek = dateTime.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    startOfCalendarWeek = dateTime.AddDays(-6);
                    break;
                default:
                    startOfCalendarWeek = dateTime;
                    break;
            }
            return startOfCalendarWeek;
        }
        public static int intInput()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please give an integer.");
            }
            return input;
        }
        public static int intInput(int minValue)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < minValue)
            {
                Console.WriteLine("Please give an integer bigger than {0}.", minValue);
            }
            return input;
        }
        public static int intInput(int minValue, int maxValue)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < minValue || input > maxValue)
            {
                Console.WriteLine("Please give an integer between {0} and {1}.", minValue, maxValue);
            }
            return input;
        }
        public static double doubleInput()
        {
            double input;
            while (!double.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please give a number.");
            }
            return input;
        }
        public static double doubleInput(double minValue)
        {
            double input;
            while (!double.TryParse(Console.ReadLine(), out input) || input < minValue)
            {
                Console.WriteLine("Please give a number bigger than {0}.", minValue);
            }
            return input;
        }
        public static bool checkListEmpty<T>(IList<T> list)
        {
            return list.Count() == 0 ? true : false;
        }
        public static bool checkListEmpty<T>(List<T> list)
        {
            return list.Count() == 0 ? true : false;
        }
    }
}
