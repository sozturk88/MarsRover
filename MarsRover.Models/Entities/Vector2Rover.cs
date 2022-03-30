using MarsRover.Models.Enums;
using System;

namespace MarsRover.Models.Entities
{
    [Serializable]
    /// <summary>
    /// Position Of Rover
    /// </summary>
    public class Vector2Rover : IEquatable<Vector2Rover>, ICloneable
    {
        /// <summary>
        /// Horizontal/Vertical Position
        /// </summary>
        public Vector2Int Position { get; set; }

        /// <summary>
        /// Direction Of Rover
        /// </summary>
        public Directions Direction { get; set; }

        public Vector2Rover()
        {

        }

        public Vector2Rover(int x, int y, Directions direction)
        {
            this.Position = new Vector2Int(x, y);
            this.Direction = direction;
        }

        public Vector2Rover(Vector2Int position, Directions direction)
        {
            this.Position = position;
            this.Direction = direction;
        }

        public bool Equals(Vector2Rover other)
        {
            return Position.X == other.Position.X && Position.Y == other.Position.Y && Direction == other.Direction;
        }

        public override string ToString() => $"{this.Position.X} {this.Position.Y} {Direction.ToString()}";

        public object Clone()
        {
            return new Vector2Rover(this.Position.X, this.Position.Y, this.Direction);
        }
    }
}
