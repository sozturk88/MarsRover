namespace MarsRover.Interfaces
{
    public interface IObserver
    {
        void Update(ISubject subject);
        void Update<T>(ISubject subject, T arguments);
    }
}
