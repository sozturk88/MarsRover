using MarsRover.Models.Entities;

namespace MarsRover.Infrastructures.Test.Models
{    public class WorkingAreaMockModel
    {
        public Vector2Int Size { get; set; }
        public RoverSubjectMockModel[] Subjects { get; set; }
    }
}
