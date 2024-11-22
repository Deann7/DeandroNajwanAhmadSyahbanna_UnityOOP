using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [SerializeField] private float leftBound = -10f;
    [SerializeField] private float rightBound = 10f;
    [SerializeField] private float minSpawnY = 0f;
    [SerializeField] private float maxSpawnY = 5f;
    [SerializeField] private float speed = 2f;

    private bool movingLeft = true;

    private void Start()
    {
        level = 1;
        SpawnEnemy();
    }

    private void Update()
    {
        MoveEnemy();
        CheckScreenBounds();
    }

    private void SpawnEnemy()
    {
        float spawnX = Random.value < 0.5f ? leftBound : rightBound;
        float spawnY = Random.Range(minSpawnY, maxSpawnY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        transform.position = spawnPosition;
        movingLeft = spawnX == rightBound; 
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

}
