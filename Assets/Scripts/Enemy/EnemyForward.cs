using UnityEngine;

public class EnemyForward : Enemy
{
    [SerializeField] private float topBound = 5f;
    [SerializeField] private float bottomBound = -5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minSpawnX = -8f;
    [SerializeField] private float maxSpawnX = 8f;
    [SerializeField] private int maxEnemies = 1;

    private static int enemyCount = 0;
    private bool movingUp; // Default to false for downward movement

    private void Start()
    {
        if (enemyCount < maxEnemies)
        {
            enemyCount++;
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        
        MoveEnemy();
        CheckScreenBounds();
    }

    private void SpawnEnemy()
    {
        if (enemyCount >= maxEnemies)
            return;

        // Always spawn at the top with a random X position
        float spawnX = Random.Range(minSpawnX, maxSpawnX);
        float spawnY = topBound;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // Instantiate the enemy at the top and set it to move downward
        EnemyForward newEnemy = Instantiate(this, spawnPosition, Quaternion.identity);
        newEnemy.movingUp = false; // Ensure the new enemy moves downward
    }

    private void MoveEnemy()
    {
        Vector3 direction = movingUp ? Vector3.up : Vector3.down;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckScreenBounds()
    {
        if (transform.position.y <= bottomBound)
        {
            movingUp = true; // Change direction when reaching the bottom
        }
        else if (transform.position.y >= topBound)
        {
            movingUp = false; // Ensure it moves downward if it reaches the top
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}
