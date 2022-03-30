using MarsRover.Common;
using MarsRover.Infrastructures;
using MarsRover.Infrastructures.Console;
using MarsRover.Models.Observers;
using System;

namespace MarsRover.Manager
{
    static class Program
    {
        static void Main(string[] args)
        {
            var workingArea = new Table();
            var _roverObserver = new RoverObserver(workingArea);

            Console.WriteLine("###### EXAMPLE ######");
            Console.WriteLine("> Rectangle Size : 5 5");
            Console.WriteLine("> Test Input:");
            Console.WriteLine("> 1 2 N");
            Console.WriteLine("> LMLMLMLMM");
            Console.WriteLine("> Expected Output: 1 3 N");
            Console.WriteLine("> 3 3 E");
            Console.WriteLine("> MMRMMRMRRM");
            Console.WriteLine("> Expected Output: 5 1 E");
            Console.WriteLine(string.Empty);

            //Setup Working Area Size
            var workingAreaSizes = Input.EnterWorkingAreaSize();
            _roverObserver.SetupWorkingArea(workingAreaSizes);

            while (true)
            {
                //This Try Catch Block Is Added To Avoid Crash
                try
                {
                    //Read Rover Position
                    var roverPosition = Input.EnterRoverPosition();
                    var roverSubject = new RoverSubject();
                    roverSubject.Attach(_roverObserver);

                    //Set A Rover To A Position
                    roverSubject.SetRoverPosition(roverPosition);

                    //Predict Movements
                    var roverMovements = Input.EnterRoverCommands();
                    roverSubject.PredictMovement(roverMovements);                

                    //Change Rover Position And Direction
                    while (RoverResources.MessageChangeRoverPosition.ConfirmBox() == "Y")
                    {
                        roverPosition = Input.EnterRoverPosition();
                        var selectedRover = _roverObserver.GetRoverSubject(roverPosition);
                        if (selectedRover != null)
                        {
                            Console.WriteLine($"{RoverResources.MessageFoundRover} : {roverPosition}");

                            //Predict Movements
                            roverMovements = Input.EnterRoverCommands();
                            roverSubject.PredictMovement(roverMovements);
                        }
                        else
                            Console.WriteLine($"{RoverResources.MessageNotFoundRover} : {roverPosition}");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{RoverResources.ErrorMessage} : {ex.Message}");
                }

                //Check If There Is Another Rover ?
                if (RoverResources.MessageAnotherRoute.ConfirmBox() == "N")
                {
                    break;
                }

            }
        }
    }
}
