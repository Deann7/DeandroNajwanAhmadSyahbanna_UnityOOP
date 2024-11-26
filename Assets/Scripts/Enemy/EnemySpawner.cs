using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    public int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    private List<Enemy> activeEnemies = new List<Enemy>();
    private Vector2 spawnBounds;

    public bool isSpawning = false;
    public CombatManager combatManager;

    [SerializeField] private float minSpawnY = 0f; // Batas minimum Y
    [SerializeField] private float maxSpawnY = 5f; // Batas maksimum Y

    public int pointEarned = 0;

    private void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        CalculateScreenBounds();
    }

    private void CalculateScreenBounds()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spawnBounds = new Vector2(screenBounds.x, screenBounds.y);
    }

    public IEnumerator SpawnEnemies()
    {
        if (spawnedEnemy == null)
        {
            Debug.LogWarning("Enemy prefab is missing!");
            yield break;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GenerateRandomSpawnPosition();
            Enemy newEnemy = Instantiate(spawnedEnemy, spawnPosition, Quaternion.identity);
            if (newEnemy != null)
            {
                activeEnemies.Add(newEnemy);
                combatManager?.EnemySpawned();
                newEnemy.OnEnemyDefeated += NotifyEnemyDestroyed;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GenerateRandomSpawnPosition()
    {
        float spawnX = Random.Range(-spawnBounds.x, spawnBounds.x);
        float spawnY = Mathf.Max(Random.Range(minSpawnY, maxSpawnY), 0);

        return new Vector3(spawnX, spawnY, 0f);
    }

    public void NotifyEnemyDestroyed(Enemy defeatedEnemy)
    {
        if (activeEnemies.Contains(defeatedEnemy))
        {
            activeEnemies.Remove(defeatedEnemy);
            OnEnemyKilled();
            combatManager?.EnemyDefeated(this);
        }
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;
        pointEarned += spawnedEnemy.level;

        Debug.Log($"{spawnedEnemy.name} dikalahkan. Total kill untuk spawner ini: {totalKill}");

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);
            multiplierIncreaseCount++;
            Debug.Log($"Spawn count meningkat: {spawnCount} (Multiplier: {multiplierIncreaseCount})");
        }
    }

    public bool IsWaveComplete()
    {
        return activeEnemies.Count == 0;
    }
}
