using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTargeting : Enemy
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float spawnInterval = 7f;
    [SerializeField] private float minSpawnDistance = 12f;
    [SerializeField] private float maxSpawnDistance = 15f;
    [SerializeField] private int maxEnemies = 2;

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

        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 spawnDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.right;
        Vector3 spawnPosition = player.transform.position + (spawnDirection * randomDistance);

        EnemyTargeting newEnemy = Instantiate(this, spawnPosition, Quaternion.identity);
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}