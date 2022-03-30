using MarsRover.Models.Entities;

namespace MarsRover.Interfaces
{
    public interface IRoverObserver
    {
        bool IsPositionEmpty(Vector2Rover roverPosition);
        void SetupWorkingArea(Vector2Int workingAreaSize);
        bool IsRoverInWorkingArea(Vector2Rover roverPosition);


        void AddSubject(ISubject subject);
        void RemoveSubject(ISubject subject);
        ISubject GetRoverSubject(string position);
    }
}
