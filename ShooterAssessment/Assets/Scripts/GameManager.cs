using GN.ShooterAssessment.ObserverPattern;
using GN.ShooterAssessment.ScriptTags;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment
{
    /// <summary>
    /// The Game Manager used to Control the flow of the game 
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int maxPlayingScore = 10;
        [SerializeField] private List<GameView> gameViews = new List<GameView>();
        [SerializeField] private GameView currentGameView;
        private ConcreteSubject PlayerSubject;
        [SerializeField]
        private UIObsever UiObserver;

        public static System.Action OnMaxScoreReached, OnStartGame, OnRestartGame;

        private void Start()
        {
            PlayerSubject = new ConcreteSubject();

            EnemyManager.OnEnemyDestroyed += IncreaseScoreByOne;
            OnMaxScoreReached += PlayerSubject.SetTotalTimeTaken;
            OnMaxScoreReached += MoveToTheNextGameView;
            OnMaxScoreReached += ()=> PlayerSubject.SetMaxReached(true);
            OnStartGame += MoveToTheNextGameView;
            OnStartGame += PlayerSubject.Reset;
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
              currentGameView =  currentGameView.SwitchNext();
            }
        }

        //Referenced from Unity
        public void StartTheShooting()
        {
            OnStartGame?.Invoke();
        }

        public void RestartTheGame()
        {
            OnRestartGame?.Invoke();
        }
    }
}
