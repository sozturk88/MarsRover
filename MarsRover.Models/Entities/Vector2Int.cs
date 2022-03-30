using System;

namespace MarsRover.Models.Entities
{
    /// <summary>
    /// Position Of Rover
    /// </summary>
    public class Vector2Int : IEquatable<Vector2Int>
    {

        /// <summary>
        /// Horizontal Position
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Vertical Position
        /// </summary>
        public int Y { get; set; }

        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString() => $"{X} {Y}";

        public bool Equals(Vector2Int other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}