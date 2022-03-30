using MarsRover.Common;
using MarsRover.Interfaces;
using MarsRover.Models.Entities;
using MarsRover.Models.Enums;
using System;

namespace MarsRover.Infrastructures
{
    /// <summary>
    /// Direction and position of Rover
    /// </summary>
    public class RoverSubject : BaseSubject, IRoverMovement
    {

        /// <summary>
        /// Movement Status
        /// </summary>
        public MoveStates State { get; private set; }

        public RoverSubject()
        {
            State = MoveStates.Standing;
        }

        #region Movement
        /// <summary>
        /// Rover Current Direction and Position
        /// </summary>
        public Vector2Rover Position { get; set; }

        /// <summary>
        /// Rover Move Forward To Current Direction
        /// </summary>
        /// <returns></returns>
        public void ChangeState(MoveStates newState)
        {
            State = newState;
        }

        /// <summary>
        /// Rover Move Forward To Current Direction
        /// </summary>
        /// <returns></returns>
        public void MoveForward(Vector2Rover currentPosition)
        {
            var nextPosition = currentPosition.Position;
            var verticalMovement = 0;
            var horizontalMovement = 0;

            switch (currentPosition.Direction)
            {
                case Directions.N:
                    verticalMovement = 1;
                    break;
                case Directions.E:
                    horizontalMovement = 1;
                    break;
                case Directions.S:
                    verticalMovement = -1;
                    break;
                case Directions.W:
                    horizontalMovement = -1;
                    break;
            }

            currentPosition.Position.X = nextPosition.X + horizontalMovement;
            currentPosition.Position.Y = nextPosition.Y + verticalMovement;
        }

        /// <summary>
        /// Predict Last Position and Direction
        /// </summary>
        /// <param name="movement">Example : LLLMMRMM</param>
        /// <returns></returns>
        public Vector2Rover PredictMovement(string movements)
        {
            var predictPosition = (Vector2Rover)Position.Clone();

            var nextDirection = predictPosition.Direction.GetHashCode();
            State = MoveStates.Predicting;

            foreach (var mov in movements)
            {
                var nextMov = mov.ToString().ToType<Movements>();
                switch (nextMov)
                {
                    case Movements.L:
                        nextDirection--;
                        if (nextDirection < 0)
                            nextDirection = Directions.W.GetHashCode();

                        predictPosition.Direction = (Directions)nextDirection;
                        break;
                    case Movements.R:
                        nextDirection = (nextDirection + 1) % 4;

                        predictPosition.Direction = (Directions)nextDirection;
                        break;
                    case Movements.M:
                        MoveForward(predictPosition);
                        break;
                }
            }

            this.Notify<Vector2Rover>(predictPosition);
            return predictPosition;
        }

        /// <summary>
        /// Rover Movement (Object)
        /// </summary>
        /// <returns></returns>
        public void SetRoverPosition(Vector2Rover nextPosition)
        {
            Position = nextPosition;
            State = MoveStates.Moved;
            this.Notify();
        }


        /// <summary>
        /// Rover Movement With String (1 3 N)
        /// </summary>
        /// <returns></returns>
        public void SetRoverPosition(string nextPosition)
        {
            var splitPosition = nextPosition.SplitToArray();
            if (splitPosition.Length != 3)
                throw new InvalidOperationException($"{RoverResources.CommandNotValid} (Valid Format : X Y D)");

            var IsValidChoose = int.TryParse(splitPosition[0], out var x) && int.TryParse(splitPosition[1], out var y) && Enum.TryParse<Directions>(splitPosition[2], out var dir);
            if (!IsValidChoose)
            {
                throw new InvalidOperationException($"{RoverResources.CommandNotValid} (Valid Format : X Y D)");                
            }

            this.Position = new Vector2Rover(splitPosition[0].ToType<int>(), splitPosition[1].ToType<int>(), splitPosition[2].ToType<Directions>());
            State = MoveStates.Moved;
            this.Notify();
        }
        #endregion Movement
    }
}
