using GN.ShooterAssessment.ObserverPatter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment.ObserverPatter
{
    public abstract class Subject //: MonoBehaviour
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