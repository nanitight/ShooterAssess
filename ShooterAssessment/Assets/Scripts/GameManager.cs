using GN.ShooterAssessment.ObserverPatter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment
{
    public class GameManager : MonoBehaviour
    {
        public ConcreteSubject PlayerSubject;
        public UIObsever UiObserver;
        public int maxPlayingScore = 10;

        public static System.Action OnMaxScoreReached;

        private void Start()
        {
            EnemyManager.OnEnemyDestroyed += IncreaseScoreByOne;
            OnMaxScoreReached += PlayerSubject.SetTotalTimeTaken;
            UiObserver.Init(PlayerSubject);
        }
        private void Update()
        {
            // Make sure there's always 3 enemies
            if (EnemyManager.SpawnedEnemies != 3)
            {
                EnemyManager.SpawnEnemy?.Invoke();
            }
            PlayerSubject.UpdateTime(Time.deltaTime);
        }

        void IncreaseScoreByOne()
        {
            PlayerSubject.SetScore(PlayerSubject.Score + 1);
            if (PlayerSubject.Score >= maxPlayingScore)
            {
                OnMaxScoreReached?.Invoke();
            }
        }


    }
}
