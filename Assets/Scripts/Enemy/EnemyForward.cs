using UnityEngine;

public class EnemyForward : Enemy
{
    [SerializeField] private float topBound = 5f;
    [SerializeField] private float bottomBound = -5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float minSpawnX = -8f;
    [SerializeField] private float maxSpawnX = 8f;
    [SerializeField] private int maxEnemies = 1;

    private static int enemyCount = 0;
    private bool movingUp = true;

    private void Start()
    {
        if (enemyCount < maxEnemies)
        {
            float spawnX = Random.Range(minSpawnX, maxSpawnX);
            transform.position = new Vector3(spawnX, movingUp ? bottomBound : topBound, 0f);
            enemyCount++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        
        MoveEnemy();
        CheckScreenBounds();
    }

    private void MoveEnemy()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    private void CheckScreenBounds()
    {
        if (transform.position.y >= topBound)
        {
            movingUp = false;
        }
        else if (transform.position.y <= bottomBound)
        {
            movingUp = true;
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}
