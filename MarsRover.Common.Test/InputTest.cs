using MarsRover.Common.Test.MockObjects;
using MarsRover.Models.Entities;
using Xunit;

namespace MarsRover.Common.Test
{
    public class InputTest
    {
        //[Fact]
        [Theory]
        [MemberData(nameof(MockData.GetWorkingAreaSize), MemberType = typeof(MockData))]
        public void Test_EnterWorkingAreaSize_Method(string input, Vector2Int expectedResult, bool continuallyTry)
        {
            var result = Input.EnterWorkingAreaSize(() => input, continuallyTry);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(MockData.GetEnterRoverPosition), MemberType = typeof(MockData))]
        public void Test_EnterRoverPosition_Method(string input, string expectedResult, bool continuallyTry)
        {
            var result = Input.EnterRoverPosition(() => input, continuallyTry);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(MockData.GetEnterRoverCommands), MemberType = typeof(MockData))]
        public void Test_EnterRoverCommands_Method(string input, string expectedResult, bool continuallyTry)
        {
            var result = Input.EnterRoverCommands(() => input, continuallyTry);
            Assert.Equal(expectedResult, result);
        }

    }
}
