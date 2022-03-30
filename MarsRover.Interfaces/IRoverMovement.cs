using MarsRover.Models.Entities;
using MarsRover.Models.Enums;

namespace MarsRover.Interfaces
{
    public interface IRoverMovement
    {
        /// <summary>
        /// Change Rover Movement State (Standing, Predicting, Moved)
        /// </summary>
        /// <returns></returns>
        void ChangeState(MoveStates newState);

        /// <summary>
        /// Rover Movement
        /// </summary>
        /// <returns></returns>
        void SetRoverPosition(string nextPosition);

        /// <summary>
        /// Rover Movement
        /// </summary>
        /// <returns></returns>
        void SetRoverPosition(Vector2Rover nextPosition);

        /// <summary>
        /// Rover Move Forward To Current Direction
        /// </summary>
        /// <returns></returns>
        void MoveForward(Vector2Rover currentPosition);
    }
}
