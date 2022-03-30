using MarsRover.Models.Enums;
using Xunit;

namespace MarsRover.Common.Test
{
    public class ExtensionsTest
    {
        //[Fact]
        [Theory]
        [InlineData("5 5")]
        [InlineData("1 2 N")]
        [InlineData("LMLMLMLMM")]
        [InlineData("3 3 E")]    
        [InlineData("MMRMMRMRRM")]
        public void Test_SplitToArray_Method(string command)
        {
            var result = command.SplitToArray();
            Assert.True(result.Length > 0, "Expected to be greater than 0");
        }

        [Theory]
        [InlineData("5 5", 2)]
        [InlineData("1 2 N", 3)]
        [InlineData("LMLMLMLMM", 1)]
        [InlineData("3 3 E", 3)]
        [InlineData("MMRMMRMRRM", 1)]
        public void Test_SplitToArray_Equal_To_ExceptedOutput(string command, int exceptedLength)
        {
            var result = command.SplitToArray();
            Assert.Equal(exceptedLength, result.Length);
        }


        /// <summary>
        /// Only Y,N are accepted
        /// </summary>
        /// <param name="input"></param>
        /// <param name="exceptedOutput"></param>
        [Theory]
        [InlineData("Do you confirm that (Y/N) ?", "Y", "Y", false)]
        [InlineData("Are you ready (Y/N)", "N", "N", false)]
        public void Test_ConfirmBox(string input, string confirmInput, string exceptedOutput, bool continuallyTry)
        {
            var result = input.ConfirmBox(() => confirmInput, continuallyTry);
            Assert.Equal(exceptedOutput, result);
        }

        [Theory]
        [InlineData("L", Movements.L)]
        [InlineData("R", Movements.R)]
        [InlineData("M", Movements.M)]
        public void Test_ToType_Method_For_MovementEnum(string movement, Movements exceptedOutput)
        {
            var result = movement.ToType<Movements>();
            Assert.Equal(exceptedOutput, result);
        }


        [Theory]
        [InlineData("N", Directions.N)]
        [InlineData("E", Directions.E)]
        [InlineData("S", Directions.S)]
        [InlineData("W", Directions.W)]
        public void Test_ToType_Method_For_DirectionEnum(string direction, Directions exceptedOutput)
        {
            var result = direction.ToType<Directions>();
            Assert.Equal(exceptedOutput, result);
        }


        [Theory]
        [InlineData("Standing", MoveStates.Standing)]
        [InlineData("Predicting", MoveStates.Predicting)]
        [InlineData("Moved", MoveStates.Moved)]
        public void Test_ToType_Method_For_MoveStatesEnum(string movement, MoveStates exceptedOutput)
        {
            var result = movement.ToType<MoveStates>();
            Assert.Equal(exceptedOutput, result);
        }

    }
}
