using System;
using System.ComponentModel;
using System.Linq;

namespace MarsRover.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Cast object to type by T Class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToType<T>(this string value) where T : struct
        {
            try
            {
                var conv = TypeDescriptor.GetConverter(typeof(T));
                return (T)conv.ConvertFrom(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Convert Type Of ToType Extension Is Not Valid : {ex.Message}");
                return default(T);
            }
        }
        /// <summary>
        /// Split string to array
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string[] SplitToArray(this string value)
        {
            return value.Split(" ").Where(p => !string.IsNullOrEmpty(p)).ToArray();
        }
        public static string ConfirmBox(this string message, Func<String> readProvider = null, bool continuallyTry = true)
        {
            var IsValidChoose = false;
            string result;
            do
            {
                Console.Write(message);
                readProvider = readProvider ?? Console.ReadLine;
                result = readProvider().Trim().ToUpper();

                if (result.Length == 1)
                {
                    foreach (var command in result)
                    {
                        IsValidChoose = Constants.ValidChooses.Contains(command);
                        if (!IsValidChoose)
                        {
                            Console.WriteLine(RoverResources.CommandNotValid);
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(RoverResources.CommandNotValid);
                    IsValidChoose = false;
                }
                Console.WriteLine(string.Empty);
            }
            while (!IsValidChoose && continuallyTry);

            return result;
        }
    }
}
