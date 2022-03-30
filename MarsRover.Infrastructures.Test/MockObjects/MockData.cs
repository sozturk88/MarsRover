using MarsRover.Infrastructures.Test.Models;
using MarsRover.Models.Entities;
using MarsRover.Models.Enums;
using System.Collections.Generic;

namespace MarsRover.Infrastructures.Test.MockObjects
{
    public static class MockData
    {        public static IEnumerable<object[]> GetRoverDatas()
        {
            yield return new object[]
            {
                new WorkingAreaMockModel {

                    Size = new Vector2Int(5, 5),

                    Subjects = new RoverSubjectMockModel[] {
                        new RoverSubjectMockModel(1, 1, Directions.N) {
                            ExpectedResult = new Vector2Rover(4, 2, Directions.N),
                            Movements = new string[] { "RMMMML", "LMRM" }
                        },
                        new RoverSubjectMockModel(5, 5, Directions.N) {
                            ExpectedResult = new Vector2Rover(1, 3, Directions.W),
                            Movements = new string[] { "LMMMLMLMM", "RMRMMM" }
                        },
                        new RoverSubjectMockModel(3, 3, Directions.N) {
                            ExpectedResult = new Vector2Rover(5, 1, Directions.E),
                            Movements = new string[] { "LMMLM", "LMRMLMM", "M" }
                        }
                    },
                }
            };

            yield return new object[]
            {
                new WorkingAreaMockModel {

                    Size = new Vector2Int(10, 5),

                    Subjects = new RoverSubjectMockModel[] {
                        new RoverSubjectMockModel(1, 1, Directions.N) {
                            ExpectedResult = new Vector2Rover(4, 2, Directions.N),
                            Movements = new string[] { "RMMMMMMMLM", "LMRM" }
                        },
                        new RoverSubjectMockModel(5, 5, Directions.N) {
                            ExpectedResult = new Vector2Rover(1, 3, Directions.W),
                            Movements = new string[] { "LMMMM", "RMRMMM" }
                        },
                        new RoverSubjectMockModel(3, 3, Directions.N) {
                            ExpectedResult = new Vector2Rover(5, 1, Directions.E),
                            Movements = new string[] { "LMMLM", "LMRMLMM", "M" }
                        }
                    },
                }
            };
        }
    }
}
