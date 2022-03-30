using MarsRover.Infrastructures.Console;
using MarsRover.Infrastructures.Test.MockObjects;
using MarsRover.Infrastructures.Test.Models;
using MarsRover.Models.Entities;
using MarsRover.Models.Observers;
using Moq;
using Xunit;

namespace MarsRover.Infrastructures.Test
{
    public class InputTest
    {
        //[Fact]
        [Theory]
        [MemberData(nameof(MockData.GetRoverDatas), MemberType = typeof(MockData))]
        public void Test_EnterWorkingAreaSize_Method(WorkingAreaMockModel input)
        {
            var table = Mock.Of<Table>();
            var _roverObserver = new Mock<RoverObserver>(table).Object;
            _roverObserver.SetupWorkingArea(new Vector2Int(input.Size.X, input.Size.Y));

            var roverSubject = new Mock<RoverSubject>().Object;
            roverSubject.Attach(_roverObserver);

            //Subject List
            foreach (var subject in input.Subjects)
            {
                //Set A Rover To A Position
                roverSubject.SetRoverPosition(subject.Position);

                //Predict Movements
                Vector2Rover result = null;
                foreach (var mov in subject.Movements)
                {
                    result = roverSubject.PredictMovement(mov);

                    bool isInWorkingArea = _roverObserver.IsRoverInWorkingArea(result);
                    Assert.True(isInWorkingArea);

                    bool isPositionEmpty = _roverObserver.IsPositionEmpty(result);
                    Assert.True(isPositionEmpty);

                    roverSubject.SetRoverPosition(result);
                }

                Assert.Equal(subject.ExpectedResult, result);
            }
        }
    }
}
