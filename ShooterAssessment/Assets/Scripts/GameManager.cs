using GN.ShooterAssessment.ObserverPatter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment
{
    public class GameManager : MonoBehaviour
    {
        public int maxPlayingScore = 10;
        public List<GameView> gameViews = new List<GameView>();
        public GameView currentGameView;
        public ConcreteSubject PlayerSubject;
        public UIObsever UiObserver;

        public static System.Action OnMaxScoreReached, OnStartGame, OnRestartGame;

        private void Start()
        {
            EnemyManager.OnEnemyDestroyed += IncreaseScoreByOne;
            OnMaxScoreReached += PlayerSubject.SetTotalTimeTaken;
            OnMaxScoreReached += MoveToTheNextGameView;
            OnStartGame += MoveToTheNextGameView;
            UiObserver.Init(PlayerSubject);
            if (gameViews.Count > 0 )
            {
                foreach( var gameView in gameViews )
                {
                    gameView.TurnOff();
                }

                currentGameView = gameViews[0];
                currentGameView.TurnOn();
            }
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

        private void OnDestroy()
        {

            EnemyManager.OnEnemyDestroyed -= IncreaseScoreByOne;
            OnMaxScoreReached -= PlayerSubject.SetTotalTimeTaken;
            OnMaxScoreReached -= MoveToTheNextGameView;
            OnStartGame -= MoveToTheNextGameView;
        }

        void IncreaseScoreByOne()
        {
            PlayerSubject.SetScore(PlayerSubject.Score + 1);
            if (PlayerSubject.Score >= maxPlayingScore)
            {
                OnMaxScoreReached?.Invoke();
            }
        }

        public void MoveToTheNextGameView()
        {
            if (currentGameView != null)
            {                
               currentGameView.SwitchNext();
            }
        }

        public void StartTheShooting()
        {
            OnStartGame?.Invoke();
        }

    }
}
