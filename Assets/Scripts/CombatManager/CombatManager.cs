using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0f;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private bool isWaveComplete = true;

    void Start()
    {
        Debug.Log("CombatManager initialized.");
    }

    private void Update()
    {
        if (isWaveComplete)
        {
            timer += Time.deltaTime;

            if (timer >= waveInterval)
            {
                timer = 0f;
                StartNextWave();
            }
        }
    }

    private void StartNextWave()
    {
        Debug.Log($"Starting wave {waveNumber}...");
        isWaveComplete = false;

        totalEnemies = 0; // Reset totalEnemies sebelum menghitung ulang

        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner.spawnedEnemy.level <= waveNumber)
            {
                spawner.isSpawning = true;
                spawner.spawnCount = spawner.defaultSpawnCount;
                StartCoroutine(spawner.SpawnEnemies());
            }
        }

        Debug.Log($"Wave {waveNumber} started with {totalEnemies} enemies.");
    }

    public void EnemySpawned()
    {
        totalEnemies++;
    }

    public void EnemyDefeated()
    {
        totalEnemies--;

        Debug.Log($"Enemy defeated. Remaining enemies: {totalEnemies}");

        // Jika semua musuh telah dikalahkan, tandai wave selesai
        if (totalEnemies <= 0)
        {
            Debug.Log($"Wave {waveNumber} completed! Preparing for next wave...");
            waveNumber++;
            isWaveComplete = true;
        }
    }
}
