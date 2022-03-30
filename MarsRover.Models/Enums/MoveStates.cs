namespace MarsRover.Models.Enums
{
    /// <summary>
    /// Direction Of Rover (Default = N)
    /// </summary>
    public enum MoveStates
    {
        /// <summary>
        /// Just Standing
        /// </summary>
        Standing = 0,

        /// <summary>
        /// Guess To Last Position
        /// </summary>
        Predicting,

        /// <summary>
        /// Moved To Position
        /// </summary>
        Moved
    }
}
