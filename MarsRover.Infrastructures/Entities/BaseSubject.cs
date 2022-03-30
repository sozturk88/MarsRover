using MarsRover.Interfaces;
using System.Collections.Generic;

namespace MarsRover.Infrastructures
{
    /// <summary>
    /// Direction and position of Rover
    /// </summary>
    public class BaseSubject : ISubject
    {

        // List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        protected readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            this._observers.Add(observer);
            (observer as IRoverObserver).AddSubject(this);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            (observer as IRoverObserver).RemoveSubject(this);
        }

        // Trigger an update in each subscriber.
        public virtual void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public virtual void Notify<T>(T arguments)
        {
            foreach (var observer in _observers)
            {
                observer.Update<T>(this, arguments);
            }
        }
    }
}
