using UnityEngine;

public class CombatManager : MonoBehaviour
{
     public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private void Update()
    {
        if (totalEnemies <= 0)
        {
            timer += Time.deltaTime;

            if (timer >= waveInterval)
            {
                timer = 0;
                waveNumber++;
                totalEnemies = 0;

                Debug.Log("Wave started: " + waveNumber);

                foreach (EnemySpawner spawner in enemySpawners)
                {
                    if (waveNumber >= spawner.spawnedEnemy.level)
                    {
                        spawner.isCurrentlySpawning = true;
                        StartCoroutine(spawner.SpawnEnemies());
                        spawner.spawnCount = spawner.baseSpawnCount;
                        totalEnemies += spawner.spawnCount;
                    }
                }
            }
          
        }
    }

    public void DecrementEnemyCount()
    {
        totalEnemies--;
    }
}
