using MarsRover.Models.Entities;
using System.Collections.Generic;

namespace MarsRover.Common.Test.MockObjects
{
    public static class MockData
    {
        public static IEnumerable<object[]> GetWorkingAreaSize()
        {
            yield return new object[] { "5 5", new Vector2Int(5, 5), false };
            yield return new object[] { "1 5", new Vector2Int(1, 5), false };
        }

        public static IEnumerable<object[]> GetEnterRoverPosition()
        {
            yield return new object[] { "3 4 N", "3 4 N", false };
            yield return new object[] { "5 2 S", "5 2 S", false };
        }

        public static IEnumerable<object[]> GetEnterRoverCommands()
        {
            yield return new object[] { "LLMMRMRM", "LLMMRMRM", false };
            yield return new object[] { "RMMRMRLM", "RMMRMRLM", false };
        }
    }
}
