using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [SerializeField] private float leftBound = -10f;
    [SerializeField] private float rightBound = 10f;
    [SerializeField] private float minSpawnY = -5f;
    [SerializeField] private float maxSpawnY = 5f;
    [SerializeField] private float speed = 3f;

    private static int enemyCount = 0;
    private bool movingLeft = true;

    private void Start()
    {
        if (enemyCount < 1)
        {         
            float spawnY = Random.Range(minSpawnY, maxSpawnY);
            transform.position = new Vector3(spawnY, movingLeft ? rightBound : leftBound, 0f);
            enemyCount++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        MoveEnemy();
        CheckScreenBounds();
    }

    private void MoveEnemy()
    {
        if (movingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
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
