using System;
using TMPro;

namespace GN.ShooterAssessment.ObserverPattern
{
    /// <summary>
    /// The concrete Observer - specifically for UI.
    /// </summary>
    [Serializable]
    public class UIObsever : Observer
    {
        private ConcreteSubject subject;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI totalTime;

        public override void Update()
        {
            if (scoreText != null)
            {
                scoreText.text = subject.Score.ToString();
            }

            if (timeText != null)
            {
                timeText.text = subject.TimeSpan.ToString(@"dd\.hh\:mm\:ss");
            }

            if (totalTime != null && subject.MaxReached)
            {
                totalTime.text = subject.TimeTaken.ToString(@"dd\:hh\:mm\:ss")+ " days";
            }
        }

        public void Init(ConcreteSubject subject)
        {
            this.subject = subject;
            subject.AttachObserver(this);
        }
    }
}