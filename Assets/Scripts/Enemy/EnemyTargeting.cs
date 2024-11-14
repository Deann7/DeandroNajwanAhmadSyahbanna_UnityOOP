using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTargeting : Enemy
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float spawnInterval = 7f;
    [SerializeField] private float minSpawnDistance = 12f;  // Minimum spawn distance from player
    [SerializeField] private float maxSpawnDistance = 15f;  // Maximum spawn distance from player
    [SerializeField] private int maxEnemies = 2;           // Fewer max enemies since they're more aggressive

    private static int enemyCount = 0;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (enemyCount < maxEnemies)
        {
            enemyCount++;
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }
    }

    void Update()
    {
        if (player == null) return;

        // Check if the player is within the enemy's detection radius
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
        
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (transform.position == player.transform.position)
            {
                Destroy(gameObject);
            }
        }
    }

    private void SpawnEnemy()
    {
        if (player == null || enemyCount >= maxEnemies) return;

        // Generate a random angle
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        // Calculate spawn position in a circle around the player
        Vector3 spawnDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.right;
        Vector3 spawnPosition = player.transform.position + (spawnDirection * randomDistance);

        // Spawn new enemy
        EnemyTargeting newEnemy = Instantiate(this, spawnPosition, Quaternion.identity);
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}