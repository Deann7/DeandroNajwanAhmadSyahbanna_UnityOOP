using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public UIControl uiControl;
    public float timer = 0f;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
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
        totalEnemies = 0;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner.spawnedEnemy.level <= waveNumber)
            {
                spawner.isSpawning = true;
                spawner.spawnCount = spawner.defaultSpawnCount + (waveNumber - 1);
                StartCoroutine(spawner.SpawnEnemies());
            }
        }

        timer = 0f;
    }

    public void EnemySpawned()
    {
        totalEnemies++;
    }

public void EnemyDefeated()
{
    totalEnemies--;

    
    if (enemySpawners != null)
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner != null && spawner.spawnedEnemy != null)
            {
                int enemyLevel = spawner.spawnedEnemy.level;
                uiControl.totalPoints += enemyLevel;
                Debug.Log($"Enemy Level: {enemyLevel}, Points Added: {enemyLevel}, Total Points: {uiControl.totalPoints}");
            }
        }
    }

    if (totalEnemies <= 0)
    {
        isWaveComplete = true;
        waveNumber++;
    }
}
    public void EnemyDefeated(EnemySpawner spawner)
    {
        totalEnemies--;
        uiControl.totalPoints += spawner.pointEarned; 
        spawner.pointEarned = 0; 

        if (totalEnemies <= 0)
        {
            isWaveComplete = true;
            waveNumber++;
        }
    }
}