using MarsRover.Common;
using MarsRover.Infrastructures;
using MarsRover.Infrastructures.Console;
using MarsRover.Interfaces;
using MarsRover.Models.Entities;
using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;

namespace MarsRover.Models.Observers
{
    public class RoverObserver : IRoverObserver, IObserver
    {
        private readonly Table _workingArea; 
        public Vector2Int WorkingAreaSize { get; set; }
        public List<RoverSubject> RoverSubjects { get; set; } = new List<RoverSubject>();
        public RoverObserver(Table workingArea)
        {
            _workingArea = workingArea;
        }

        /// <summary>
        /// Setup Working Area Size And Drawing Table
        /// </summary>
        /// <returns></returns>
        public void SetupWorkingArea(Vector2Int workingAreaSize)
        {
            WorkingAreaSize = workingAreaSize;
            _workingArea.SetAreaSize(workingAreaSize);
            _workingArea.DrawTable();
        }

        /// <summary>
        /// Rover Position Is Valid For Working Area Size ?
        /// </summary>
        /// <returns></returns>
        public bool IsRoverInWorkingArea(Vector2Rover roverPosition)
        {
            return  roverPosition.Position.X >= 0 &&
                    roverPosition.Position.Y >= 0 &&
                    roverPosition.Position.X <= WorkingAreaSize.X && 
                    roverPosition.Position.Y <= WorkingAreaSize.Y;
        }

        /// <summary>
        /// Rover Position Is Valid ?
        /// </summary>
        /// <returns></returns>
        public bool IsPositionEmpty(Vector2Rover roverPosition)
        {
            return _workingArea.IsPositionEmpty(roverPosition.Position);
        }

        #region Observer
        public void Update(ISubject subject)
        {
            var castedSubject = (subject as RoverSubject);
            if (castedSubject.State == Enums.MoveStates.Moved)
            {
                _workingArea.SetRoverPointTable(castedSubject.Position);
                _workingArea.DrawTable();
            }

            castedSubject.ChangeState(Enums.MoveStates.Standing);
        }

        public void Update<T>(ISubject subject, T arguments)
        {
            var castedSubject = (subject as RoverSubject);
            if(castedSubject.State == Enums.MoveStates.Predicting && (arguments is Vector2Rover))
            {
                var newPosition = arguments as Vector2Rover;

                if (!IsRoverInWorkingArea(newPosition))
                    throw new ArgumentOutOfRangeException($"{newPosition.ToString()} / {WorkingAreaSize.ToString()}", RoverResources.CommandResultOutOfRange);

                if (!newPosition.Position.Equals(castedSubject.Position) && !IsPositionEmpty(newPosition))
                    throw new InvalidOperationException($"{newPosition.ToString()} : {RoverResources.CommandResultOutOfRange}");

                _workingArea.SetRoverPointTable(newPosition, castedSubject.Position);
                castedSubject.SetRoverPosition(newPosition);
            }

            castedSubject.ChangeState(Enums.MoveStates.Standing);
        }

        public void AddSubject(ISubject subject)
        {
            RoverSubjects.Add(subject as RoverSubject);
        }

        public void RemoveSubject(ISubject subject)
        {
            RoverSubjects.Remove(subject as RoverSubject);
        }

        public ISubject GetRoverSubject(string position)
        {
            var splitPosition = position.SplitToArray();
            if (splitPosition.Length != 3)
                throw new InvalidOperationException($"{RoverResources.CommandNotValid} (Valid Format : X Y D)");

            var IsValidChoose = int.TryParse(splitPosition[0], out var x) && int.TryParse(splitPosition[1], out var y) && Enum.TryParse<Directions>(splitPosition[2], out var dir);
            if (!IsValidChoose)
            {
                throw new InvalidOperationException($"{RoverResources.CommandNotValid} (Valid Format : X Y D)");
            }

            var positionVector = new Vector2Rover(splitPosition[0].ToType<int>(), splitPosition[1].ToType<int>(), splitPosition[2].ToType<Directions>());
            return RoverSubjects.Find(p => p.Position.Equals(positionVector));
        }
        #endregion Observer
    }
}
