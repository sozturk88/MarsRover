using MarsRover.Models.Entities;
using MarsRover.Models.Enums;
using System;
using System.Linq;

namespace MarsRover.Common
{
    public static class Input
    {       

        /// <summary>
        /// Enter Working Area Size (5 5)
        /// </summary>
        /// <returns></returns>
        public static Vector2Int EnterWorkingAreaSize(Func<String> readProvider = null, bool continuallyTry = true)
        {
            var x = 0;
            var y = 0;
            var IsValidChoose = false;
            do
            {
                Console.Write(RoverResources.MessageWorkingAreaSize);

                readProvider = readProvider ?? Console.ReadLine;
                var roverWorkingArea = readProvider().Trim().ToUpper().SplitToArray();
                if (roverWorkingArea.Length == 2)
                {

                    IsValidChoose = int.TryParse(roverWorkingArea[0], out x) && int.TryParse(roverWorkingArea[1], out y);
                    if (!IsValidChoose)
                    {
                        Console.WriteLine(RoverResources.CommandNotValid);
                        break;
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

            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Enter Rover Position (3 4 N)
        /// </summary>
        /// <returns></returns>
        public static string EnterRoverPosition(Func<String> readProvider = null, bool continuallyTry = true)
        {

            bool IsValidChoose = false;
            var x = 0;
            var y = 0;
            var dir = Directions.N;

            do
            {
                Console.Write(RoverResources.MessageNewRoverPosition);
                readProvider = readProvider ?? Console.ReadLine;
                var roverPosition = readProvider().Trim().ToUpper().SplitToArray();
                if (roverPosition.Length == 3)
                {

                    IsValidChoose = int.TryParse(roverPosition[0], out x) && int.TryParse(roverPosition[1], out y) && Enum.TryParse<Directions>(roverPosition[2], out dir);
                    if (!IsValidChoose)
                    {
                        Console.WriteLine(RoverResources.CommandNotValid);
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

            return $"{x} {y} {dir}";
        }

        /// <summary>
        /// Get Rover Command From Console (LLRRMMRMLRM
        /// </summary>
        /// <returns></returns>
        public static string EnterRoverCommands(Func<String> readProvider = null, bool continuallyTry = true)
        {

            bool IsValidChoose = true;
            string roverCommands = string.Empty;

            do
            {
                Console.Write(RoverResources.MessageRoverCommand);
                readProvider = readProvider ?? Console.ReadLine;
                roverCommands = readProvider().Trim().ToUpper();
                if (roverCommands.Length > 0)
                {
                    foreach (var command in roverCommands)
                    {
                        IsValidChoose = Constants.ValidMovementChooses.Contains(command);
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

            return roverCommands;
        }
    }
}
