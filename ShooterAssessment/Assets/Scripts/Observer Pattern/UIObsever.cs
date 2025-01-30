using System;
using TMPro;
using UnityEngine.UI;

namespace GN.ShooterAssessment.ObserverPatter
{
    [Serializable]
    public class UIObsever : Observer
    {
        private ConcreteSubject subject;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timeText;

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
        }

        public void Init(ConcreteSubject subject)
        {
            this.subject = subject;
            subject.AttachObserver(this);
        }
    }
}