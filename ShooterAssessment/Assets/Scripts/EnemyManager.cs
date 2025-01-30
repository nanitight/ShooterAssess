using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment {
    /// <summary>
    /// Responsible for spawning enemies at random locations
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        public GameObject EnemyPrefab;
        public int MaxSpawnableAtATime = 3;
        public List<Transform> SpawnLocations = new List<Transform>();
        public List<int> OccupiedIndexes = new List<int>();
        public static int SpawnedEnemies = 0;

        public static System.Action SpawnEnemy, OnEnemyDestroyed;
        public static System.Action<Enemy> DestroyEnemy;

        public void Start()
        {
            SpawnEnemy += SpawnEnemyPrefab;
            DestroyEnemy += DestroyEnemyKilled;
        }

        protected void SpawnEnemyPrefab()
        {
            int location = SpawnLocations.Count > 0 ? Random.Range(0, SpawnLocations.Count) : -1 ;
            if (location>=0)
            {
                if (SpawnedEnemies < MaxSpawnableAtATime && OccupiedIndexes.Count < MaxSpawnableAtATime) //can still spawn the enemy
                {
                    var f = OccupiedIndexes.FindIndex(i => i == location);
                    while (OccupiedIndexes.FindIndex(i => i == location) != -1 && OccupiedIndexes.Count>0) //if occupied, try again
                    {
                        location = Random.Range(0, SpawnLocations.Count);
                    }
                    GameObject instantiated = Instantiate(EnemyPrefab, SpawnLocations[location]);
                    if (instantiated != null)
                    {
                        Enemy enemy = instantiated.GetComponent<Enemy>();
                        enemy.LocationIndex = location;
                        OccupiedIndexes.Add(location);
                        SpawnedEnemies++;
                    }
                }
            }
        }

        protected void DestroyEnemyKilled(Enemy killedEnemy)
        {
            if (killedEnemy != null)
            {
                SpawnedEnemies--;
                OccupiedIndexes.Remove(killedEnemy.LocationIndex);
                Destroy(killedEnemy.gameObject);
                OnEnemyDestroyed?.Invoke();
            }

        }
    }

    
}