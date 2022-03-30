namespace MarsRover.Interfaces
{
    /// <summary>
    /// Direction and position of Rover
    /// </summary>
    public interface ISubject
    {
        // Attach an observer to the subject.
        void Attach(IObserver observer);

        // Detach an observer from the subject.
        void Detach(IObserver observer);

        // Notify all observers about an event.
        void Notify();
        // Notify all observers with parameter about an event.
        void Notify<T>(T arguments);
    }
}
