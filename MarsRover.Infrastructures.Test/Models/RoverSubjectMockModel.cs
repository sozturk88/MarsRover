using MarsRover.Models.Entities;
using MarsRover.Models.Enums;

namespace MarsRover.Infrastructures.Test.Models
{
    public class RoverSubjectMockModel
    {
        public RoverSubjectMockModel(int x, int y, Directions dir)
        {
            Position = new Vector2Rover(x, y, dir);
        }
        public Vector2Rover Position { get; private set; }

        public string[] Movements { get; set; }
        public Vector2Rover ExpectedResult { get; set; }
    }
}
