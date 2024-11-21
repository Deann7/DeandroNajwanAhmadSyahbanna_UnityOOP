using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [SerializeField] private float leftBound = -10f;
    [SerializeField] private float rightBound = 10f;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minSpawnY = -5f;
    [SerializeField] private float maxSpawnY = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int maxEnemies = 1;

    private static int enemyCount = 0;
    private bool movingLeft = true;

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
        MoveEnemy();
        CheckScreenBounds();
    }

    private void SpawnEnemy()
    {
        if (enemyCount >= maxEnemies)
            return;

        float spawnX = Random.value < 0.5f ? leftBound : rightBound;
        float spawnY = Random.Range(minSpawnY, maxSpawnY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        EnemyHorizontal newEnemy = Instantiate(this, spawnPosition, Quaternion.identity);
        newEnemy.movingLeft = spawnX == rightBound;
    }

    private void MoveEnemy()
    {
        Vector3 direction = movingLeft ? Vector3.left : Vector3.right;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckScreenBounds()
    {
        if (transform.position.x <= leftBound)
        {
            movingLeft = false;
        }
        else if (transform.position.x >= rightBound)
        {
            movingLeft = true;
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}
