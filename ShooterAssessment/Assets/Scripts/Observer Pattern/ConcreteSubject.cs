using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment.ObserverPattern
{
    /// <summary>
    /// The concrete Subject. The represents the main context of the game play.
    /// </summary>
    [Serializable]
    public class ConcreteSubject : Subject
    {
        private int score = 0;
        private TimeSpan timeTaken, timeSpan; 
        public int Score { get { return score; } }
        public TimeSpan TimeTaken { get {  return timeTaken; } }
        public TimeSpan TimeSpan { get { return timeSpan; } }

        private bool maxReached = false;
        public bool MaxReached { get { return maxReached; } }

        public void SetScore(int score)
        {
            this.score = score;
            Notify();
        }

        public void UpdateTime(float deltaSeconds)
        {
            timeSpan = timeSpan.Add( new TimeSpan(0,0,0,0,(int)(deltaSeconds*1000)));
            Notify();
        }

        public void SetTotalTimeTaken()
        {
            timeTaken = new TimeSpan(timeSpan.Ticks);
            Notify();
        }

        public void Reset()
        {
            SetScore(0);
            SetMaxReached(false);
            timeSpan = new TimeSpan();
        }

        public void SetMaxReached(bool maxReached)
        {
            this.maxReached = maxReached;
            Notify();
        }
    }
}
