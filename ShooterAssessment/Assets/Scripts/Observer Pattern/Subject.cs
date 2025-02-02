
using System.Collections.Generic;

namespace GN.ShooterAssessment.ObserverPattern
{
    /// <summary>
    /// The Subject base class in the pattern
    /// </summary>
    public abstract class Subject 
    {
        private List<Observer> observers = new List<Observer>();

        public void AttachObserver(Observer observer)
        {
            if (observers.Find(i => i == observer) == null)
                observers.Add(observer);
        }

        public void DettachObserver(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (Observer observer in observers)
            {
                observer.Update();
            }
        }
    }
}