using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    public int totalKills = 0;
    [SerializeField]
    private int killsRequiredToIncreaseSpawn = 3;

    private int waveKills = 0;

    [SerializeField] private float spawnDelay = 3f;

    private List<Enemy> activeEnemies = new List<Enemy>();

    [Header("Enemy Spawn Settings")]
    public int spawnCount = 0;
    public int baseSpawnCount = 1;
    public int spawnMultiplier = 1;
    public int multiplierStep = 1;

    public CombatManager combatManager;
    public bool isCurrentlySpawning = false;

    private Vector2 spawnBounds; // Batas layar untuk spawn posisi

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
        if (spawnedEnemy == null || !isCurrentlySpawning)
        {
            Debug.LogWarning("Spawning is not allowed or enemy prefab is missing.");
            yield break;
        }

        spawnCount = baseSpawnCount;
        Debug.Log($"Spawning {spawnCount} enemies...");

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GenerateRandomSpawnPosition();

            Enemy newEnemy = Instantiate(spawnedEnemy, spawnPosition, Quaternion.identity);

            if (newEnemy != null)
            {
                activeEnemies.Add(newEnemy);
                newEnemy.SetSpawner(this);
            }
            else
            {
                Debug.LogError("Failed to instantiate enemy prefab.");
            }

            yield return new WaitForSeconds(spawnDelay);
        }

        isCurrentlySpawning = false;
        Debug.Log("Finished spawning enemies.");
    }

    private Vector3 GenerateRandomSpawnPosition()
    {
        float spawnX = Random.Range(-spawnBounds.x, spawnBounds.x); 
        float spawnY = Random.Range(-spawnBounds.y, spawnBounds.y); 
        return new Vector3(spawnX, spawnY, 0f); 
    }

    public void NotifyEnemyDestroyed(Enemy enemy)
    {
          activeEnemies.Remove(enemy);
        waveKills++;
      
        totalKills++;
  
    }

    private void Update()
    {
        bool thresholdReached = false;

        if (totalKills >= killsRequiredToIncreaseSpawn && !thresholdReached)
        {
            killsRequiredToIncreaseSpawn += killsRequiredToIncreaseSpawn;
            baseSpawnCount += spawnMultiplier;

            if (waveKills >= multiplierStep)
            {
                spawnMultiplier++;
                waveKills = 0;
            }
        }
    }

    public int GetRemainingEnemyCount()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);
        Debug.Log("Active enemies remaining: " + activeEnemies.Count);
        return activeEnemies.Count;
    }

  
}
