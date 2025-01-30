using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment
{
    public class GameManager : MonoBehaviour
    {
        private void Update()
        {
            // Make sure there's always 3 enemies
            if (EnemyManager.SpawnedEnemies != 3)
            {
                EnemyManager.SpawnEnemy.Invoke();
            }
        }

    }
}
